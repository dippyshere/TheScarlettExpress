#region

using System.Runtime.Serialization;

#endregion

namespace DialogueEditor
{
    [DataContract]
    public abstract class EditableParameter
    {
        public const int MAX_NAME_SIZE = 24;

        [DataMember] public string ParameterName;

        public EditableParameter(string name)
        {
            ParameterName = name;
        }

        public abstract eParamType ParameterType { get; }

        public enum eParamType
        {
            Bool,
            Int
        }
    }

    [DataContract]
    public class EditableBoolParameter : EditableParameter
    {
        [DataMember] public bool BoolValue;

        public EditableBoolParameter(string name) : base(name)
        {
        }

        public override eParamType ParameterType
        {
            get { return eParamType.Bool; }
        }
    }

    [DataContract]
    public class EditableIntParameter : EditableParameter
    {
        [DataMember] public int IntValue;

        public EditableIntParameter(string name) : base(name)
        {
        }

        public override eParamType ParameterType
        {
            get { return eParamType.Int; }
        }
    }
}