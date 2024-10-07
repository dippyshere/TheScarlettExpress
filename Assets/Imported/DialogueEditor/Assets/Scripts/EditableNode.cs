#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEditor;
using UnityEngine;

#endregion

namespace DialogueEditor
{
    [DataContract, KnownType(typeof(EditableSpeechConnection)), KnownType(typeof(EditableOptionConnection)),
     KnownType(typeof(EditableSetIntParamAction)), KnownType(typeof(EditableSetBoolParamAction))]
    public abstract class EditableConversationNode
    {
        public enum eNodeType
        {
            Speech,
            Option
        }

        [DataMember] public List<EditableConnection> Connections;

        // ----
        // Serialized Editor vars
        [DataMember] public EditorArgs EditorInfo;
        [DataMember] public int ID;
        [DataMember] public List<EditableSetParamAction> ParamActions;
        public List<EditableConversationNode> parents;
        [DataMember] public List<int> parentUIDs;

        // ----
        // Serialized Node data
        [DataMember] public string Text;

        // ----
        // Volatile
        public TMP_FontAsset TMPFont;

        /// <summary> Deprecated as of V1.03 </summary>
        [DataMember] public string TMPFontGUID;

        public EditableConversationNode()
        {
            parents = new List<EditableConversationNode>();
            Connections = new List<EditableConnection>();
            ParamActions = new List<EditableSetParamAction>();
            parentUIDs = new List<int>();
            EditorInfo = new EditorArgs { xPos = 0, yPos = 0, isRoot = false };
        }

        public abstract eNodeType NodeType { get; }


        // ------------------------

        public void RegisterUIDs()
        {
            if (parentUIDs != null)
            {
                parentUIDs.Clear();
            }

            parentUIDs = new List<int>();

            for (int i = 0; i < parents.Count; i++)
            {
                parentUIDs.Add(parents[i].ID);
            }
        }

        public void RemoveSelfFromTree()
        {
            // This speech is no longer the parent of any children
            for (int i = 0; i < Connections.Count; i++)
            {
                switch (Connections[i])
                {
                    case EditableSpeechConnection:
                    {
                        EditableSpeechConnection speechCon = Connections[i] as EditableSpeechConnection;
                        speechCon.Speech.parents.Remove(this);
                        break;
                    }
                    case EditableOptionConnection:
                    {
                        EditableOptionConnection optionCon = Connections[i] as EditableOptionConnection;
                        optionCon.Option.parents.Remove(this);
                        break;
                    }
                }
            }

            // This speech is no longer any of my parents child speech 
            for (int i = 0; i < parents.Count; i++)
            {
                parents[i].DeleteConnectionChild(this);
            }
        }

        public void DeleteConnectionChild(EditableConversationNode node)
        {
            if (Connections.Count == 0)
            {
                return;
            }

            if (node.NodeType == eNodeType.Speech && Connections[0] is EditableSpeechConnection)
            {
                EditableSpeechNode toRemove = node as EditableSpeechNode;

                for (int i = 0; i < Connections.Count; i++)
                {
                    EditableSpeechConnection con = Connections[i] as EditableSpeechConnection;
                    if (con.Speech == toRemove)
                    {
                        Connections.RemoveAt(i);
                        return;
                    }
                }
            }
            else if (node is EditableOptionNode && Connections[0] is EditableOptionConnection)
            {
                EditableOptionNode toRemove = node as EditableOptionNode;

                for (int i = 0; i < Connections.Count; i++)
                {
                    EditableOptionConnection con = Connections[i] as EditableOptionConnection;
                    if (con.Option == toRemove)
                    {
                        Connections.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public virtual void SerializeAssetData(NPCConversation conversation)
        {
            conversation.GetNodeData(ID).TMPFont = TMPFont;
        }

        public virtual void DeserializeAssetData(NPCConversation conversation)
        {
            TMPFont = conversation.GetNodeData(ID).TMPFont;

#if UNITY_EDITOR
            // If under V1.03, Load from database via GUID, so data is not lost for people who are upgrading
            if (conversation.Version < (int)eSaveVersion.V1_03)
            {
                if (TMPFont == null)
                {
                    if (!string.IsNullOrEmpty(TMPFontGUID))
                    {
                        string path = AssetDatabase.GUIDToAssetPath(TMPFontGUID);
                        TMPFont = (TMP_FontAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TMP_FontAsset));
                    }
                }
            }
#endif
        }

        /// <summary> Info used internally by the editor window. </summary>
        [DataContract]
        public class EditorArgs
        {
            [DataMember] public bool isRoot;
            [DataMember] public float xPos;
            [DataMember] public float yPos;
        }
    }


    [DataContract]
    public class EditableSpeechNode : EditableConversationNode
    {
        /// <summary>
        ///     If this dialogue leads onto another dialogue...
        ///     Should the dialogue advance automatially?
        /// </summary>
        [DataMember] public bool AdvanceDialogueAutomatically;

        /// <summary> The Audio Clip acompanying this Speech. </summary>
        public AudioClip Audio;

        /// <summary> Deprecated as of V1.03 </summary>
        [DataMember] public string AudioGUID;

        /// <summary>
        ///     If this dialogue automatically advances, should it also display an
        ///     "end" / "continue" button?
        /// </summary>
        [DataMember] public bool AutoAdvanceShouldDisplayOption;

        /// <summary> The NPC Icon </summary>
        public Sprite Icon;

        [DataMember] public SpeechNode.eCharacter CharacterIcon;

        /// <summary> Deprecated as of V1.03 </summary>
        [DataMember] public string IconGUID;

        // ----
        // Serialized Node data

        /// <summary> The NPC Name </summary>
        [DataMember] public string Name;


        //--------
        // Deprecated

        /// <summary> Deprecated as of V1.1 </summary>
        public List<EditableOptionNode> Options;

        /// <summary> Deprecated as of V1.1 </summary>
        [DataMember] public List<int> OptionUIDs;

        /// <summary> Deprecated as of V1.1 </summary>
        public EditableSpeechNode Speech;

        /// <summary> Deprecated as of V1.1 </summary>
        [DataMember] public int SpeechUID;

        /// <summary>  The time it will take for the Dialogue to automaically advance </summary>
        [DataMember] public float TimeUntilAdvance;

        /// <summary> The Volume for the AudioClip; </summary>
        [DataMember] public float Volume;

        public override eNodeType NodeType
        {
            get { return eNodeType.Speech; }
        }


        // ------------------------------

        public void AddOption(EditableOptionNode newOption)
        {
            // Remove any speech connections I may have
            if (Connections.Count > 0 && Connections[0] is EditableSpeechConnection)
            {
                // I am no longer a parent of these speechs'
                for (int i = 0; i < Connections.Count; i++)
                {
                    (Connections[0] as EditableSpeechConnection).Speech.parents.Remove(this);
                }

                Connections.Clear();
            }

            // Connection to this option already exists
            if (Connections.Count > 0 && Connections[0] is EditableOptionConnection)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    if ((Connections[0] as EditableOptionConnection).Option == newOption)
                    {
                        return;
                    }
                }
            }

            // Setup option connection
            Connections.Add(new EditableOptionConnection(newOption));
            if (!newOption.parents.Contains(this))
            {
                newOption.parents.Add(this);
            }
        }

        public void AddSpeech(EditableSpeechNode newSpeech)
        {
            // Remove any option connections I may have
            if (Connections.Count > 0 && Connections[0] is EditableOptionConnection)
            {
                // I am no longer a parent of these speechs'
                for (int i = 0; i < Connections.Count; i++)
                {
                    (Connections[0] as EditableOptionConnection).Option.parents.Remove(this);
                }

                Connections.Clear();
            }

            // Connection to this speech already exists
            if (Connections.Count > 0 && Connections[0] is EditableSpeechConnection)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    if ((Connections[0] as EditableSpeechConnection).Speech == newSpeech)
                    {
                        return;
                    }
                }
            }

            // If a relationship the other-way-around between these speechs already exists, swap it. 
            // A 2way speech<->speech relationship cannot exist.
            if (parents.Contains(newSpeech))
            {
                parents.Remove(newSpeech);
                newSpeech.DeleteConnectionChild(this);
            }

            // Setup option connection
            Connections.Add(new EditableSpeechConnection(newSpeech));
            if (!newSpeech.parents.Contains(this))
            {
                newSpeech.parents.Add(this);
            }
        }

        public override void SerializeAssetData(NPCConversation conversation)
        {
            base.SerializeAssetData(conversation);

            conversation.GetNodeData(ID).Audio = Audio;
            conversation.GetNodeData(ID).Icon = Icon;
        }

        public override void DeserializeAssetData(NPCConversation conversation)
        {
            base.DeserializeAssetData(conversation);

            Audio = conversation.GetNodeData(ID).Audio;
            Icon = conversation.GetNodeData(ID).Icon;

#if UNITY_EDITOR
            // If under V1.03, Load from database via GUID, so data is not lost for people who are upgrading
            if (conversation.Version < (int)eSaveVersion.V1_03)
            {
                if (Audio == null)
                {
                    if (!string.IsNullOrEmpty(AudioGUID))
                    {
                        string path = AssetDatabase.GUIDToAssetPath(AudioGUID);
                        Audio = (AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));
                    }
                }

                if (Icon == null)
                {
                    if (!string.IsNullOrEmpty(IconGUID))
                    {
                        string path = AssetDatabase.GUIDToAssetPath(IconGUID);
                        Icon = (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
                    }
                }
            }
#endif
        }
    }


    [DataContract]
    public class EditableOptionNode : EditableConversationNode
    {
        /// <summary> Deprecated as of V1.1 </summary>
        public EditableSpeechNode Speech;

        /// <summary> Deprecated as of V1.1 </summary>
        [DataMember] public int SpeechUID;

        public EditableOptionNode()
        {
            SpeechUID = EditableConversation.INVALID_UID;
        }

        public override eNodeType NodeType
        {
            get { return eNodeType.Option; }
        }


        // ------------------------------

        public void AddSpeech(EditableSpeechNode newSpeech)
        {
            // Add new speech connection
            Connections.Add(new EditableSpeechConnection(newSpeech));
            newSpeech.parents.Add(this);
        }
    }
}