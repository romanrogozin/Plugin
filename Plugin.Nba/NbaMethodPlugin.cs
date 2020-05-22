using System.Threading.Tasks;
using Plugin.Core;
using Plugin.Nba.Contracts;

namespace Plugin.Nba
{
    public class NbaMethodPlugin : IPlugin<NbaMethod>
    {
        public string Name { get; } = nameof(NbaMethodPlugin);

        public Task Execute(NbaMethod command)
        {
            command.Result = new NbaMethodResult
            {
                StringProperty = $"{Name}, {command.Parameter}"
            };

            return Task.CompletedTask;
        }
    }
}