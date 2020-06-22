using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fclp;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace JSchemaGenerator
{
    class Program
    {
        class Args
        {
            public List<string> Schemas { get; set; }
            public string Namespace { get; set; }
        }

        static async Task<int> Main(string[] args)
        {
            var arguments = new Args();
            var parser = new FluentCommandLineParser<Args>();
            parser.Setup(a => a.Namespace)
                .As('n', "ns")
                .SetDefault("Schema")
                .WithDescription("Generate csharp namespace name")
                .Callback(@namespace => arguments.Namespace = @namespace);

            parser.Setup(a => a.Schemas)
                .As('s', "schema")
                .Required()
                .WithDescription("Single/multiple schema file names")
                .Callback(schemas => arguments.Schemas = schemas);

            parser.SetupHelp("?", "h", "help")
                .Callback(text => Console.WriteLine(text));

            var result = parser.Parse(args);
            
            if (result.HelpCalled)
                return 0;

            if (result.HasErrors)
            {
                parser.HelpOption.ShowHelp(parser.Options);
                return -1;
            }

            await Task.WhenAll(arguments.Schemas.Select(name=> GenerateCSharpFileFromSchema(name, arguments)));
            return 0;
        }

        private static async Task GenerateCSharpFileFromSchema(string fileName, Args arguments)
        {
            var path = Path.GetFullPath(fileName);
            var fName = $"{Path.GetFileNameWithoutExtension(fileName)}.cs";
            var sc = await JsonSchema.FromFileAsync(path);
            var generator = new CSharpGenerator(sc, new CSharpGeneratorSettings()
            {
                Namespace = arguments.Namespace
            });
            var generateFile = generator.GenerateFile();
            generateFile = generateFile.Replace(
                @"[System.CodeDom.Compiler.GeneratedCode(""NJsonSchema"", ""10.1.21.0 (Newtonsoft.Json v9.0.0.0)"")]",
                string.Empty);
            await File.WriteAllTextAsync($"{fName}", generateFile);
        }
    }
}
