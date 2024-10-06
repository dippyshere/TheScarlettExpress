namespace DialogueEditor
{
    public abstract class Parameter
    {
        public string ParameterName;

        public Parameter(string name)
        {
            ParameterName = name;
        }
    }

    public class BoolParameter : Parameter
    {
        public bool BoolValue;

        public BoolParameter(string name, bool defaultValue) : base(name)
        {
            BoolValue = defaultValue;
        }
    }

    public class IntParameter : Parameter
    {
        public int IntValue;

        public IntParameter(string name, int defalutValue) : base(name)
        {
            IntValue = defalutValue;
        }
    }
}