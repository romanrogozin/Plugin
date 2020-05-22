using System.Threading.Tasks;
using Plugin.Nba.Contracts;

namespace Plugin
{
    public interface IGameConfigurationManager
    {
        public Task<string> GmMethod();
        public Task<NbaMethodResult> GmNbaMethod(string s);
        public Task<string> GmWwzMethod();
    }
}