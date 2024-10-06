namespace DialogueEditor
{
    public abstract class SetParamAction
    {
        public enum eParamActionType
        {
            Int,
            Bool
        }

        public string ParameterName;

        public abstract eParamActionType ParamActionType { get; }
    }

    public class SetIntParamAction : SetParamAction
    {
        public int Value;

        public override eParamActionType ParamActionType
        {
            get { return eParamActionType.Int; }
        }
    }

    public class SetBoolParamAction : SetParamAction
    {
        public bool Value;

        public override eParamActionType ParamActionType
        {
            get { return eParamActionType.Bool; }
        }
    }
}