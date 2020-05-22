using System.Threading.Tasks;
using Plugin.Nba.Contracts;

namespace Plugin
{
    public interface IGameConfigurationService
    {
        public Task<string> Method();
        public Task<NbaMethodResult> NbaMethod(string s);
        public Task<string> WwzMethod();
    }
}