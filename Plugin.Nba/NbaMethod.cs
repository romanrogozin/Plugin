using Plugin.Core;

namespace Plugin.Nba
{
    [Interface("IGameConfigurationService")]
    public class NbaMethod : PluginParameter<NbaMethodResult>
    {
        public string Parameter { get; set; }
    }
}
