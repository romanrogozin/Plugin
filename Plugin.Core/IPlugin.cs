using System;
using System.Threading.Tasks;

namespace Plugin.Core
{
    public interface IPluginBase
    {
        public string Name { get; }
    }

    public interface IPlugin<in TParameter> : IPluginBase
    {
        Task Handle(TParameter parameter);
    }
}
