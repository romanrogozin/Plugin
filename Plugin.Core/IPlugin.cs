using System.Threading.Tasks;

namespace Plugin.Core
{
    public interface IPluginBase
    {
        public string Name { get; }
    }

    public interface IPlugin<in TCommand> : IPluginBase where TCommand : BasePluginCommand
    {
        Task Execute(TCommand command);
    }

    public interface IPluginGameConfiguration<in TCommand> : IPlugin<TCommand> where TCommand : BasePluginCommand
    {
        void Initialize(IConfigurationProvider configurationProvider);
    }

    public interface IConfigurationProvider
    {
        string GetJsonConfiguration();
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        public string GetJsonConfiguration()
        {
            return @"
{
    ""CommonProperty"": ""Common"",
	""NbaClass"": {
		""NbaProperty"": ""NBA""
	},
	""WwzProperty"": 1
}
";
        }
    }
}
