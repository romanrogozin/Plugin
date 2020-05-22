using System.Threading.Tasks;
using Plugin.Nba.Contracts;

namespace Plugin
{
    public class GameConfigurationManager : IGameConfigurationManager
    {
        public Task<string> GmMethod()
        {
            return Task.FromResult($"{nameof(GmMethod)}");
        }

        public Task<NbaMethodResult> GmNbaMethod(string s)
        {
            var result = new NbaMethodResult()
            {
                StringProperty = $"{nameof(GmNbaMethod)}: {s}"
            };

            return Task.FromResult(result);
        }

        public Task<string> GmWwzMethod()
        {
            return Task.FromResult($"{nameof(GmWwzMethod)}");
        }
    }
}