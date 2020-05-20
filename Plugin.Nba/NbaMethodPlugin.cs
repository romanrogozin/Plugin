using System.Threading.Tasks;
using Plugin.Core;

namespace Plugin.Nba
{
    public class NbaMethodPlugin : IPlugin<NbaMethod>
    {
        public string Name { get; } = nameof(NbaMethodPlugin);

        public Task Handle(NbaMethod parameter)
        {
            parameter.Result = new NbaMethodResult
            {
                StringProperty = $"{Name}, {parameter.Parameter}"
            };

            return Task.CompletedTask;
        }
    }
}