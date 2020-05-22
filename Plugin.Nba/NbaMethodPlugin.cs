using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Plugin.Core;
using Plugin.Nba.Contracts;

namespace Plugin.Nba
{
    public class NbaMethodPlugin : IPluginGameConfiguration<NbaMethod>
    {
        private IConfigurationProvider _configurationProvider;

        public void Initialize(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public string Name { get; } = nameof(NbaMethod);

        public Task Execute(NbaMethod command)
        {
            var json = _configurationProvider.GetJsonConfiguration();
            var nbaClass = JToken.Parse(json)["NbaClass"].ToObject<NbaClass>();
            var result = new NbaMethodResult
            {
                OutputString = $"{nameof(NbaMethod)}: {nbaClass.NbaProperty}; {command.Parameter}"
            };

            command.Result = result;
            return Task.CompletedTask;
        }
    }
}