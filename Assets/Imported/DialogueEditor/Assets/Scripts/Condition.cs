namespace DialogueEditor
{
    public abstract class Condition
    {
        public enum eConditionType
        {
            IntCondition,
            BoolCondition
        }

        public string ParameterName;

        public abstract eConditionType ConditionType { get; }
    }

    public class IntCondition : Condition
    {
        public enum eCheckType
        {
            equal,
            lessThan,
            greaterThan
        }

        public eCheckType CheckType;
        public int RequiredValue;

        public override eConditionType ConditionType
        {
            get { return eConditionType.IntCondition; }
        }
    }

    public class BoolCondition : Condition
    {
        public enum eCheckType
        {
            equal,
            notEqual
        }

        public eCheckType CheckType;
        public bool RequiredValue;

        public override eConditionType ConditionType
        {
            get { return eConditionType.BoolCondition; }
        }
    }
}