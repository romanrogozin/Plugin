using System;
using System.Collections.Generic;
using Fclp;

namespace JSchemaGenerator
{
    public class Args
    {
        public List<string> Schemas { get; set; }
        public string Namespace { get; set; }
        public string OutputDir { get; set; }
    }

    internal static class ArgumentsHelper
    {
        public static FluentCommandLineParser<Args> CreateParser(this Args arguments)
        {
            var parser = new FluentCommandLineParser<Args>();

            parser.Setup(a => a.Namespace)
                .As('n', "ns")
                .SetDefault("Schema")
                .WithDescription("Generate csharp namespace name")
                .Callback(@namespace => arguments.Namespace = @namespace);

            parser.Setup(a => a.OutputDir)
                .As('o', "output")
                .WithDescription("Output directory name")
                .Callback(output => arguments.OutputDir = output);

            parser.Setup(a => a.Schemas)
                .As('s', "schema")
                .Required()
                .WithDescription("Single/multiple schema file names")
                .Callback(schemas => arguments.Schemas = schemas);

            parser.SetupHelp("?", "h", "help")
                .Callback(text => Console.WriteLine(text));

            return parser;
        }
    }
}