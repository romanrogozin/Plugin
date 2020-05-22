using System.Threading.Tasks;

namespace Plugin.Core
{
    public interface IPluginBase
    {
        public string Name { get; }
    }

    public interface IPlugin<in TCommand> : IPluginBase where TCommand : BasePluginCommand
    {
        Task Execute(TCommand command);
    }
}
