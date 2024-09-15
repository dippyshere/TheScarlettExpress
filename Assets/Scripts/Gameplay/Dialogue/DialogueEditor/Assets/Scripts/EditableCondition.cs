#region

using System.Runtime.Serialization;

#endregion

namespace DialogueEditor
{
    [DataContract]
    public abstract class EditableCondition
    {
        [DataMember] public string ParameterName;

        public EditableCondition(string name)
        {
            ParameterName = name;
        }

        public abstract eConditionType ConditionType { get; }

        public enum eConditionType
        {
            IntCondition,
            BoolCondition
        }
    }

    [DataContract]
    public class EditableIntCondition : EditableCondition
    {
        [DataMember] public eCheckType CheckType;
        [DataMember] public int RequiredValue;

        public EditableIntCondition(string name) : base(name)
        {
        }

        public override eConditionType ConditionType
        {
            get { return eConditionType.IntCondition; }
        }

        public enum eCheckType
        {
            equal,
            lessThan,
            greaterThan
        }
    }

    [DataContract]
    public class EditableBoolCondition : EditableCondition
    {
        [DataMember] public eCheckType CheckType;
        [DataMember] public bool RequiredValue;

        public EditableBoolCondition(string name) : base(name)
        {
        }

        public override eConditionType ConditionType
        {
            get { return eConditionType.BoolCondition; }
        }

        public enum eCheckType
        {
            equal,
            notEqual
        }
    }
}