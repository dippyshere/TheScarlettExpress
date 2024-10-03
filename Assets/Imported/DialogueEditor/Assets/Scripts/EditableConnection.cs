#region

using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace DialogueEditor
{
    [DataContract, KnownType(typeof(EditableIntCondition)), KnownType(typeof(EditableBoolCondition))]
    public abstract class EditableConnection
    {
        public enum eConnectiontype
        {
            Speech,
            Option
        }

        [DataMember] public List<EditableCondition> Conditions;
        [DataMember] public int NodeUID;

        public EditableConnection()
        {
            Conditions = new List<EditableCondition>();
        }

        public abstract eConnectiontype ConnectionType { get; }

        public void AddCondition(EditableCondition condition)
        {
            Conditions.Add(condition);
        }
    }

    [DataContract]
    public class EditableSpeechConnection : EditableConnection
    {
        public EditableSpeechNode Speech;

        public EditableSpeechConnection(EditableSpeechNode node)
        {
            Speech = node;
            NodeUID = node.ID;
        }

        public override eConnectiontype ConnectionType
        {
            get { return eConnectiontype.Speech; }
        }
    }

    [DataContract]
    public class EditableOptionConnection : EditableConnection
    {
        public EditableOptionNode Option;

        public EditableOptionConnection(EditableOptionNode node)
        {
            Option = node;
            NodeUID = node.ID;
        }

        public override eConnectiontype ConnectionType
        {
            get { return eConnectiontype.Option; }
        }
    }
}