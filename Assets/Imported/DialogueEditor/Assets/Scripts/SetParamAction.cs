namespace DialogueEditor
{
    public abstract class SetParamAction
    {
        public string ParameterName;

        public abstract eParamActionType ParamActionType { get; }

        public enum eParamActionType
        {
            Int,
            Bool
        }
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