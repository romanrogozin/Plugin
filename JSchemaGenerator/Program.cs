using System.Linq;
using System.Threading.Tasks;

namespace JSchemaGenerator
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var arguments = new Args();
            var parser = arguments.CreateParser();
            var result = parser.Parse(args);

            if (result.HelpCalled)
                return 0;

            if (result.HasErrors)
            {
                parser.HelpOption.ShowHelp(parser.Options);
                return -1;
            }

            await Task.WhenAll(arguments.Schemas.Select(name => SchemaGenerator.GenerateCSharpFileFromSchema(name, arguments)));
            return 0;
        }
    }
}
