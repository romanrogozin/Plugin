using System;
using System.Threading.Tasks;
using Plugin.Core;
using Plugin.Nba.Contracts;

namespace Plugin
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // old approach
            IGameConfigurationService service = new GameConfigurationService(new ConfigurationProvider());
            Console.WriteLine(await service.CommonMethod());
            Console.WriteLine((await service.NbaMethod("test")).OutputString);
            Console.WriteLine(await service.WwzMethod());
            
            
            Console.WriteLine();

            // new approach
            var pluginManager = new PluginManager(new ConfigurationProvider());
            pluginManager.Load();
            var command = new NbaMethod { Parameter = "test" };
            await pluginManager.ExecuteCommand(command);
            Console.WriteLine(command.Result?.OutputString);
            Console.ReadKey();
        }
    }
}
