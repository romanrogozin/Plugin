using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Core;
using Plugin.Nba.Contracts;

namespace Plugin
{
    public class GameConfigurationService : IGameConfigurationService
    {
        private readonly IConfigurationProvider _configurationProvider;

        public GameConfigurationService(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public Task<string> CommonMethod()
        {
            var json = _configurationProvider.GetJsonConfiguration();
            return Task.FromResult($"{nameof(CommonMethod)}: {JToken.Parse(json)["CommonProperty"]}");
        }

        public Task<NbaMethodResult> NbaMethod(string s)
        {
            var json = _configurationProvider.GetJsonConfiguration();
            var nbaClass = JToken.Parse(json)["NbaClass"].ToObject<NbaClass>();
            var result = new NbaMethodResult
            {
                OutputString = $"{nameof(NbaMethod)}: {nbaClass.NbaProperty}; {s}"
            };

            return Task.FromResult(result);
        }

        public Task<string> WwzMethod()
        {
            var json = _configurationProvider.GetJsonConfiguration();
            var wwzClass = JsonConvert.DeserializeObject<WwzClass>(json);
            return Task.FromResult($"{nameof(WwzMethod)}: {wwzClass.WwzProperty}");
        }
    }
}