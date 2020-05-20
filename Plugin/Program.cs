using System;
using System.Threading.Tasks;
using Plugin.Core;
using Plugin.Nba;

namespace Plugin
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // old approach
            IGameConfigurationService service = new GameConfigurationService(new GameConfigurationManager());
            Console.WriteLine(await service.Method());
            Console.WriteLine((await service.NbaMethod("test")).StringProperty);
            Console.WriteLine(await service.WwzMethod());


            // new approach
            var pluginManager = new PluginManager();
            pluginManager.Load();
            var method = new NbaMethod() {Parameter = "test"};
            await pluginManager.Invoke(method);
            Console.WriteLine(method.Result?.StringProperty);
            Console.ReadKey();
        }
    }
}
