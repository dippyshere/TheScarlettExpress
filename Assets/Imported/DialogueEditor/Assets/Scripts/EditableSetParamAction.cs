#region

using System.Runtime.Serialization;

#endregion

namespace DialogueEditor
{
    [DataContract]
    public abstract class EditableSetParamAction
    {
        public enum eParamActionType
        {
            Int,
            Bool
        }

        [DataMember] public string ParameterName;

        public EditableSetParamAction(string paramName)
        {
            ParameterName = paramName;
        }

        public abstract eParamActionType ParamActionType { get; }
    }

    [DataContract]
    public class EditableSetIntParamAction : EditableSetParamAction
    {
        [DataMember] public int Value;

        public EditableSetIntParamAction(string paramName) : base(paramName)
        {
        }

        public override eParamActionType ParamActionType
        {
            get { return eParamActionType.Int; }
        }
    }

    [DataContract]
    public class EditableSetBoolParamAction : EditableSetParamAction
    {
        [DataMember] public bool Value;

        public EditableSetBoolParamAction(string paramName) : base(paramName)
        {
        }

        public override eParamActionType ParamActionType
        {
            get { return eParamActionType.Bool; }
        }
    }
}