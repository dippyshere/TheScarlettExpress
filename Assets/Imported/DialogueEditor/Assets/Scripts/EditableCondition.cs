#region

using System.Runtime.Serialization;

#endregion

namespace DialogueEditor
{
    [DataContract]
    public abstract class EditableCondition
    {
        public enum eConditionType
        {
            IntCondition,
            BoolCondition
        }

        [DataMember] public string ParameterName;

        public EditableCondition(string name)
        {
            ParameterName = name;
        }

        public abstract eConditionType ConditionType { get; }
    }

    [DataContract]
    public class EditableIntCondition : EditableCondition
    {
        public enum eCheckType
        {
            equal,
            lessThan,
            greaterThan
        }

        [DataMember] public eCheckType CheckType;
        [DataMember] public int RequiredValue;

        public EditableIntCondition(string name) : base(name)
        {
        }

        public override eConditionType ConditionType
        {
            get { return eConditionType.IntCondition; }
        }
    }

    [DataContract]
    public class EditableBoolCondition : EditableCondition
    {
        public enum eCheckType
        {
            equal,
            notEqual
        }

        [DataMember] public eCheckType CheckType;
        [DataMember] public bool RequiredValue;

        public EditableBoolCondition(string name) : base(name)
        {
        }

        public override eConditionType ConditionType
        {
            get { return eConditionType.BoolCondition; }
        }
    }
}