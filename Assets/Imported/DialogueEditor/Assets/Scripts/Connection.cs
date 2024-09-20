#region

using System.Collections.Generic;

#endregion

namespace DialogueEditor
{
    public abstract class Connection
    {
        public enum eConnectionType
        {
            None,
            Speech,
            Option
        }

        public List<Condition> Conditions;

        public Connection()
        {
            Conditions = new List<Condition>();
        }

        public abstract eConnectionType ConnectionType { get; }
    }

    public class SpeechConnection : Connection
    {
        public SpeechNode SpeechNode;

        public SpeechConnection(SpeechNode node)
        {
            SpeechNode = node;
        }

        public override eConnectionType ConnectionType
        {
            get { return eConnectionType.Speech; }
        }
    }

    public class OptionConnection : Connection
    {
        public OptionNode OptionNode;

        public OptionConnection(OptionNode node)
        {
            OptionNode = node;
        }

        public override eConnectionType ConnectionType
        {
            get { return eConnectionType.Option; }
        }
    }
}