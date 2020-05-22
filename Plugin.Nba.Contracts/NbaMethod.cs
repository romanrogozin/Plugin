using Plugin.Core;

namespace Plugin.Nba.Contracts
{
    [Interface(typeof(INewGameConfigurationService))]
    public class NbaMethod : PluginCommand<NbaMethodResult>
    {
        public string Parameter { get; set; }
    }
}
