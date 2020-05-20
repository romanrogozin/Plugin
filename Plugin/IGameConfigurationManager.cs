using System.Threading.Tasks;
using Plugin.Nba;

namespace Plugin
{
    public interface IGameConfigurationManager
    {
        public Task<string> GmMethod();
        public Task<NbaMethodResult> GmNbaMethod(string s);
        public Task<string> GmWwzMethod();
    }
}