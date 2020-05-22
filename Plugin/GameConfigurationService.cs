using System.Threading.Tasks;
using Plugin.Nba.Contracts;

namespace Plugin
{
    public class GameConfigurationService : IGameConfigurationService
    {
        private readonly IGameConfigurationManager _gameConfigurationManager;

        public GameConfigurationService(IGameConfigurationManager gameConfigurationManager)
        {
            _gameConfigurationManager = gameConfigurationManager;
        }

        public Task<string> Method()
        {
            return _gameConfigurationManager.GmMethod();
        }

        public Task<NbaMethodResult> NbaMethod(string s)
        {
            return _gameConfigurationManager.GmNbaMethod(s);
        }

        public Task<string> WwzMethod()
        {
            return _gameConfigurationManager.GmWwzMethod();
        }
    }
}