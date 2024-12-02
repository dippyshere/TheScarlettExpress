#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#endregion

//--------------------------------------
// Node C# class - User Facing
//--------------------------------------

namespace DialogueEditor
{
    public abstract class ConversationNode
    {
        public enum eNodeType
        {
            Speech,
            Option
        }

        /// <summary> The child connections this node has. </summary>
        public List<Connection> Connections;

        /// <summary> This nodes parameter actions. </summary>
        public List<SetParamAction> ParamActions;

        /// <summary> The body text of the node. </summary>
        public string Text;

        /// <summary> The Text Mesh Pro FontAsset for the text of this node. </summary>
        public TMP_FontAsset TMPFont;

        public ConversationNode()
        {
            Connections = new List<Connection>();
            ParamActions = new List<SetParamAction>();
        }

        public abstract eNodeType NodeType { get; }

        public Connection.eConnectionType ConnectionType
        {
            get
            {
                if (Connections.Count > 0)
                {
                    return Connections[0].ConnectionType;
                }

                return Connection.eConnectionType.None;
            }
        }
    }


    public class SpeechNode : ConversationNode
    {

        public enum eCharacter
        {
            Chihuahua,
            Eve,
            Sterling,
            Brown,
            Green,
            Pink,
            Red,
            Yellow,
            Joseph
        }
        
        public enum eVoiceBank
        {
            None,
            Chihuahua,
            Ebony,
            Eve,
            Joseph,
            Matilda
        }
        
        public AudioClip Audio;

        /// <summary>
        ///     Should this speech node, althought auto-advance, also display a "continue" or "end" option, for users to
        ///     click through quicker?
        /// </summary>
        public bool AutoAdvanceShouldDisplayOption;

        /// <summary> Should this speech node go onto the next one automatically? </summary>
        public bool AutomaticallyAdvance;

        /// <summary> UnityEvent, to betriggered when this Node starts. </summary>
        public UnityEvent Event;

        /// <summary> The Icon of the speaking NPC </summary>
        public Sprite Icon;
        
        /// <summary>
        /// Bug workaround because the character sprite doesnt save in builds apparently i guess
        /// </summary>
        public eCharacter CharacterImage;
        
        /// <summary>
        /// sound bank to use for voices when playing dialogue
        /// </summary>
        public eVoiceBank VoiceBank;

        /// <summary> The name of the NPC who is speaking. </summary>
        public string Name;

        /// <summary>
        ///     If AutomaticallyAdvance==True, how long should this speech node
        ///     display before going onto the next one?
        /// </summary>
        public float TimeUntilAdvance;

        public float Volume;

        public override eNodeType NodeType
        {
            get { return eNodeType.Speech; }
        }
    }


    public class OptionNode : ConversationNode
    {
        /// <summary> UnityEvent, to betriggered when this Option is chosen. </summary>
        public UnityEvent Event;

        public override eNodeType NodeType
        {
            get { return eNodeType.Option; }
        }
    }
}