#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#endregion

namespace DialogueEditor
{
    public enum eSaveVersion
    {
        V1_03 = 103, // Initial save data
        V1_10 = 110 // Parameters
    }


    //--------------------------------------
    // Conversation Monobehaviour (Serialized)
    //--------------------------------------

    [Serializable, DisallowMultipleComponent]
    public class NPCConversation : MonoBehaviour
    {
        // Consts
        /// <summary> Version 1.10 </summary>
        public const int CurrentVersion = (int)eSaveVersion.V1_10;

        [SerializeField] public TMP_FontAsset ContinueFont;

        // Serialized data
        [SerializeField] public int CurrentIDCounter = 1;
        [SerializeField] public TMP_FontAsset DefaultFont;
        [SerializeField] public string DefaultName;
        [SerializeField] public Sprite DefaultSprite;
        [SerializeField] public TMP_FontAsset EndConversationFont;

        // Runtime vars
        public UnityEvent Event;
        [SerializeField] string json;

        [FormerlySerializedAs("Events"), SerializeField]
        List<NodeEventHolder> NodeSerializedDataList;

        [SerializeField] int saveVersion;

        readonly string CHILD_NAME = "ConversationEventInfo";

        public List<EditableParameter> ParameterList; // Serialized into the json string

        // Getters
        public int Version
        {
            get { return saveVersion; }
        }


        //--------------------------------------
        // Util
        //--------------------------------------

        public NodeEventHolder GetNodeData(int id)
        {
            // Create list if none
            if (NodeSerializedDataList == null)
            {
                NodeSerializedDataList = new List<NodeEventHolder>();
            }

            // Look through list to find by ID
            for (int i = 0; i < NodeSerializedDataList.Count; i++)
            {
                if (NodeSerializedDataList[i].NodeID == id)
                {
                    return NodeSerializedDataList[i];
                }
            }

            // If none exist, create a new GameObject
            Transform EventInfo = transform.Find(CHILD_NAME);
            if (EventInfo == null)
            {
                GameObject obj = new(CHILD_NAME);
                obj.transform.SetParent(transform);
            }

            EventInfo = transform.Find(CHILD_NAME);

            // Add a new Component for this node
            NodeEventHolder h = EventInfo.gameObject.AddComponent<NodeEventHolder>();
            h.NodeID = id;
            h.Event = new UnityEvent();
            NodeSerializedDataList.Add(h);
            return h;
        }

        public void DeleteDataForNode(int id)
        {
            if (NodeSerializedDataList == null)
            {
                return;
            }

            for (int i = 0; i < NodeSerializedDataList.Count; i++)
            {
                if (NodeSerializedDataList[i].NodeID == id)
                {
                    DestroyImmediate(NodeSerializedDataList[i]);
                    NodeSerializedDataList.RemoveAt(i);
                }
            }
        }

        public EditableParameter GetParameter(string name)
        {
            for (int i = 0; i < ParameterList.Count; i++)
            {
                if (ParameterList[i].ParameterName == name)
                {
                    return ParameterList[i];
                }
            }

            return null;
        }


        //--------------------------------------
        // Serialize and Deserialize
        //--------------------------------------

        public void Serialize(EditableConversation conversation)
        {
            saveVersion = CurrentVersion;

            conversation.Parameters = ParameterList;
            json = Jsonify(conversation);
        }

        public Conversation Deserialize()
        {
            // Deserialize an editor-version (containing all info) that 
            // we will use to construct the user-facing Conversation data structure. 
            EditableConversation ec = DeserializeForEditor();

            return ConstructConversationObject(ec);
        }

        public EditableConversation DeserializeForEditor()
        {
            // Dejsonify 
            EditableConversation conversation = Dejsonify();

            if (conversation != null)
            {
                // Copy the param list
                ParameterList = conversation.Parameters;

                // Deserialize the indivudual nodes
                {
                    if (conversation.SpeechNodes != null)
                    {
                        for (int i = 0; i < conversation.SpeechNodes.Count; i++)
                        {
                            conversation.SpeechNodes[i].DeserializeAssetData(this);
                        }
                    }

                    if (conversation.Options != null)
                    {
                        for (int i = 0; i < conversation.Options.Count; i++)
                        {
                            conversation.Options[i].DeserializeAssetData(this);
                        }
                    }
                }
            }
            else
            {
                conversation = new EditableConversation();
            }

            conversation.SaveVersion = saveVersion;

            // Clear our dummy event
            Event = new UnityEvent();

            // Reconstruct
            ReconstructEditableConversation(conversation);

            return conversation;
        }

        void ReconstructEditableConversation(EditableConversation conversation)
        {
            if (conversation == null)
            {
                conversation = new EditableConversation();
            }

            // Get a list of every node in the conversation
            List<EditableConversationNode> allNodes = new();
            for (int i = 0; i < conversation.SpeechNodes.Count; i++)
            {
                allNodes.Add(conversation.SpeechNodes[i]);
            }

            for (int i = 0; i < conversation.Options.Count; i++)
            {
                allNodes.Add(conversation.Options[i]);
            }

            // For every node: 
            // Find the children and parents by UID
            for (int i = 0; i < allNodes.Count; i++)
            {
                // New parents list 
                allNodes[i].parents = new List<EditableConversationNode>();

                // Get parents by UIDs
                //-----------------------------------------------------------------------------
                // UPDATE:  This behaviour has now been removed. Later in this function, 
                //          the child->parent connections are constructed by using the 
                //          parent->child connections. Having both of these behaviours run 
                //          results in each parent being in the "parents" list twice. 
                // 
                // for (int j = 0; j < allNodes[i].parentUIDs.Count; j++)
                // {
                //     allNodes[i].parents.Add(conversation.GetNodeByUID(allNodes[i].parentUIDs[j]));
                // }
                //-----------------------------------------------------------------------------

                // Construct the parent->child connections
                //
                // V1.03
                if (conversation.SaveVersion <= (int)eSaveVersion.V1_03)
                {
                    // Construct Connections from the OptionUIDs and SpeechUIDs (which are now deprecated)
                    // This supports upgrading from V1.03 +

                    allNodes[i].Connections = new List<EditableConnection>();
                    allNodes[i].ParamActions = new List<EditableSetParamAction>();

                    if (allNodes[i].NodeType == EditableConversationNode.eNodeType.Speech)
                    {
                        EditableSpeechNode thisSpeech = allNodes[i] as EditableSpeechNode;

                        // Speech options
                        int count = thisSpeech.OptionUIDs.Count;
                        for (int j = 0; j < count; j++)
                        {
                            int optionUID = thisSpeech.OptionUIDs[j];
                            EditableOptionNode option = conversation.GetOptionByUID(optionUID);

                            thisSpeech.Connections.Add(new EditableOptionConnection(option));
                        }

                        // Speech following speech
                        {
                            int speechUID = thisSpeech.SpeechUID;
                            EditableSpeechNode speech = conversation.GetSpeechByUID(speechUID);

                            if (speech != null)
                            {
                                thisSpeech.Connections.Add(new EditableSpeechConnection(speech));
                            }
                        }
                    }
                    else if (allNodes[i] is EditableOptionNode)
                    {
                        int speechUID = (allNodes[i] as EditableOptionNode).SpeechUID;
                        EditableSpeechNode speech = conversation.GetSpeechByUID(speechUID);

                        if (speech != null)
                        {
                            allNodes[i].Connections.Add(new EditableSpeechConnection(speech));
                        }
                    }
                }
                //
                // V1.10 +
                else
                {
                    // For each node..  Reconstruct the connections
                    for (int j = 0; j < allNodes[i].Connections.Count; j++)
                    {
                        switch (allNodes[i].Connections[j])
                        {
                            case EditableSpeechConnection:
                            {
                                EditableSpeechNode speech =
                                    conversation.GetSpeechByUID(allNodes[i].Connections[j].NodeUID);
                                (allNodes[i].Connections[j] as EditableSpeechConnection).Speech = speech;
                                break;
                            }
                            case EditableOptionConnection:
                            {
                                EditableOptionNode option =
                                    conversation.GetOptionByUID(allNodes[i].Connections[j].NodeUID);
                                (allNodes[i].Connections[j] as EditableOptionConnection).Option = option;
                                break;
                            }
                        }
                    }
                }
            }

            // For every node: 
            // Tell any of the nodes children that the node is the childs parent
            for (int i = 0; i < allNodes.Count; i++)
            {
                EditableConversationNode thisNode = allNodes[i];

                for (int j = 0; j < thisNode.Connections.Count; j++)
                {
                    switch (thisNode.Connections[j].ConnectionType)
                    {
                        case EditableConnection.eConnectiontype.Speech:
                            (thisNode.Connections[j] as EditableSpeechConnection).Speech.parents.Add(thisNode);
                            break;
                        case EditableConnection.eConnectiontype.Option:
                            (thisNode.Connections[j] as EditableOptionConnection).Option.parents.Add(thisNode);
                            break;
                    }
                }
            }
        }

        string Jsonify(EditableConversation conversation)
        {
            if (conversation == null || conversation.Options == null)
            {
                return "";
            }

            MemoryStream ms = new();

            DataContractJsonSerializer ser = new(typeof(EditableConversation));
            ser.WriteObject(ms, conversation);
            byte[] jsonData = ms.ToArray();
            ms.Close();
            string toJson = Encoding.UTF8.GetString(jsonData, 0, jsonData.Length);

            return toJson;
        }

        EditableConversation Dejsonify()
        {
            if (json == null || json == "")
            {
                return null;
            }

            EditableConversation conversation = new();
            MemoryStream ms = new(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new(conversation.GetType());
            conversation = ser.ReadObject(ms) as EditableConversation;
            ms.Close();

            return conversation;
        }


        //--------------------------------------
        // Construct User-Facing Conversation Object and Nodes
        //--------------------------------------

        Conversation ConstructConversationObject(EditableConversation ec)
        {
            // Create a conversation object
            Conversation conversation = new();

            // Construct the parameters
            CreateParameters(ec, conversation);

            // Construct the Conversation-Based variables (not node-based)
            conversation.ContinueFont = ContinueFont;
            conversation.EndConversationFont = EndConversationFont;

            // Create a dictionary to store our created nodes by UID
            Dictionary<int, SpeechNode> speechByID = new();
            Dictionary<int, OptionNode> optionsByID = new();

            // Create a Dialogue and Option node for each in the conversation
            // Put them in the dictionary
            for (int i = 0; i < ec.SpeechNodes.Count; i++)
            {
                SpeechNode node = CreateSpeechNode(ec.SpeechNodes[i]);
                speechByID.Add(ec.SpeechNodes[i].ID, node);
            }

            for (int i = 0; i < ec.Options.Count; i++)
            {
                OptionNode node = CreateOptionNode(ec.Options[i]);
                optionsByID.Add(ec.Options[i].ID, node);
            }

            // Now that we have every node in the dictionary, reconstruct the tree 
            // And also look for the root
            ReconstructTree(ec, conversation, speechByID, optionsByID);

            return conversation;
        }

        void CreateParameters(EditableConversation ec, Conversation conversation)
        {
            for (int i = 0; i < ec.Parameters.Count; i++)
            {
                switch (ec.Parameters[i].ParameterType)
                {
                    case EditableParameter.eParamType.Bool:
                    {
                        EditableBoolParameter editableParam = ec.Parameters[i] as EditableBoolParameter;
                        BoolParameter boolParam = new(editableParam.ParameterName, editableParam.BoolValue);
                        conversation.Parameters.Add(boolParam);
                        break;
                    }
                    case EditableParameter.eParamType.Int:
                    {
                        EditableIntParameter editableParam = ec.Parameters[i] as EditableIntParameter;
                        IntParameter intParam = new(editableParam.ParameterName, editableParam.IntValue);
                        conversation.Parameters.Add(intParam);
                        break;
                    }
                }
            }
        }

        SpeechNode CreateSpeechNode(EditableSpeechNode editableNode)
        {
            SpeechNode speech = new();
            speech.Name = editableNode.Name;
            speech.Text = editableNode.Text;
            speech.AutomaticallyAdvance = editableNode.AdvanceDialogueAutomatically;
            speech.AutoAdvanceShouldDisplayOption = editableNode.AutoAdvanceShouldDisplayOption;
            speech.TimeUntilAdvance = editableNode.TimeUntilAdvance;
            speech.TMPFont = editableNode.TMPFont;
            speech.Icon = editableNode.Icon;
            speech.CharacterImage = editableNode.CharacterIcon;
            speech.Audio = editableNode.Audio;
            speech.Volume = editableNode.Volume;

            CopyParamActions(editableNode, speech);

            NodeEventHolder holder = GetNodeData(editableNode.ID);
            if (holder != null)
            {
                speech.Event = holder.Event;
            }

            return speech;
        }

        OptionNode CreateOptionNode(EditableOptionNode editableNode)
        {
            OptionNode option = new();
            option.Text = editableNode.Text;
            option.TMPFont = editableNode.TMPFont;

            CopyParamActions(editableNode, option);

            NodeEventHolder holder = GetNodeData(editableNode.ID);
            if (holder != null)
            {
                option.Event = holder.Event;
            }

            return option;
        }

        public void CopyParamActions(EditableConversationNode editable, ConversationNode node)
        {
            node.ParamActions = new List<SetParamAction>();

            for (int i = 0; i < editable.ParamActions.Count; i++)
            {
                switch (editable.ParamActions[i].ParamActionType)
                {
                    case EditableSetParamAction.eParamActionType.Int:
                    {
                        EditableSetIntParamAction setIntEditable =
                            editable.ParamActions[i] as EditableSetIntParamAction;

                        SetIntParamAction setInt = new();
                        setInt.ParameterName = setIntEditable.ParameterName;
                        setInt.Value = setIntEditable.Value;
                        node.ParamActions.Add(setInt);
                        break;
                    }
                    case EditableSetParamAction.eParamActionType.Bool:
                    {
                        EditableSetBoolParamAction setBoolEditable =
                            editable.ParamActions[i] as EditableSetBoolParamAction;

                        SetBoolParamAction setBool = new();
                        setBool.ParameterName = setBoolEditable.ParameterName;
                        setBool.Value = setBoolEditable.Value;
                        node.ParamActions.Add(setBool);
                        break;
                    }
                }
            }
        }

        void ReconstructTree(EditableConversation ec, Conversation conversation, Dictionary<int, SpeechNode> dialogues,
            Dictionary<int, OptionNode> options)
        {
            // Speech nodes
            List<EditableSpeechNode> editableSpeechNodes = ec.SpeechNodes;
            for (int i = 0; i < editableSpeechNodes.Count; i++)
            {
                EditableSpeechNode editableNode = editableSpeechNodes[i];
                SpeechNode speechNode = dialogues[editableNode.ID];

                // Connections
                List<EditableConnection> editableConnections = editableNode.Connections;
                for (int j = 0; j < editableConnections.Count; j++)
                {
                    int childID = editableConnections[j].NodeUID;

                    switch (editableConnections[j].ConnectionType)
                    {
                        // Construct node->Speech
                        case EditableConnection.eConnectiontype.Speech:
                        {
                            SpeechConnection connection = new(dialogues[childID]);
                            CopyConnectionConditions(editableConnections[j], connection);
                            speechNode.Connections.Add(connection);
                            break;
                        }
                        // Construct node->Option
                        case EditableConnection.eConnectiontype.Option:
                        {
                            OptionConnection connection = new(options[childID]);
                            CopyConnectionConditions(editableConnections[j], connection);
                            speechNode.Connections.Add(connection);
                            break;
                        }
                    }
                }

                // Root?
                if (editableNode.EditorInfo.isRoot)
                {
                    conversation.Root = dialogues[editableNode.ID];
                }
            }


            // Option nodes
            List<EditableOptionNode> editableOptionNodes = ec.Options;
            for (int i = 0; i < editableOptionNodes.Count; i++)
            {
                EditableOptionNode editableNode = editableOptionNodes[i];
                OptionNode optionNode = options[editableNode.ID];

                // Connections
                List<EditableConnection> editableConnections = editableNode.Connections;
                for (int j = 0; j < editableConnections.Count; j++)
                {
                    int childID = editableConnections[j].NodeUID;

                    // Construct node->Speech
                    if (editableConnections[j].ConnectionType == EditableConnection.eConnectiontype.Speech)
                    {
                        SpeechConnection connection = new(dialogues[childID]);
                        CopyConnectionConditions(editableConnections[j], connection);
                        optionNode.Connections.Add(connection);
                    }
                }
            }
        }

        void CopyConnectionConditions(EditableConnection editableConnection, Connection connection)
        {
            List<EditableCondition> editableConditions = editableConnection.Conditions;
            for (int i = 0; i < editableConditions.Count; i++)
            {
                switch (editableConditions[i].ConditionType)
                {
                    case EditableCondition.eConditionType.BoolCondition:
                    {
                        EditableBoolCondition ebc = editableConditions[i] as EditableBoolCondition;

                        BoolCondition bc = new();
                        bc.ParameterName = ebc.ParameterName;
                        switch (ebc.CheckType)
                        {
                            case EditableBoolCondition.eCheckType.equal:
                                bc.CheckType = BoolCondition.eCheckType.equal;
                                break;
                            case EditableBoolCondition.eCheckType.notEqual:
                                bc.CheckType = BoolCondition.eCheckType.notEqual;
                                break;
                        }

                        bc.RequiredValue = ebc.RequiredValue;

                        connection.Conditions.Add(bc);
                        break;
                    }
                    case EditableCondition.eConditionType.IntCondition:
                    {
                        EditableIntCondition eic = editableConditions[i] as EditableIntCondition;

                        IntCondition ic = new();
                        ic.ParameterName = eic.ParameterName;
                        switch (eic.CheckType)
                        {
                            case EditableIntCondition.eCheckType.equal:
                                ic.CheckType = IntCondition.eCheckType.equal;
                                break;
                            case EditableIntCondition.eCheckType.greaterThan:
                                ic.CheckType = IntCondition.eCheckType.greaterThan;
                                break;
                            case EditableIntCondition.eCheckType.lessThan:
                                ic.CheckType = IntCondition.eCheckType.lessThan;
                                break;
                        }

                        ic.RequiredValue = eic.RequiredValue;

                        connection.Conditions.Add(ic);
                        break;
                    }
                }
            }
        }
    }
}