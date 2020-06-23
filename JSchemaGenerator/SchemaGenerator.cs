using System;
using System.IO;
using System.Threading.Tasks;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace JSchemaGenerator
{
    internal static class SchemaGenerator
    {
        public static async Task GenerateCSharpFileFromSchema(string filename, Args args)
        {
            Console.WriteLine($@"Generate from: {filename}");
            var input = Path.GetFullPath(filename);
            Console.WriteLine($@"In: {input}");
            var outFilename = $"{Path.GetFileNameWithoutExtension(filename)}.cs";
            var output = Path.Combine((String.IsNullOrWhiteSpace(args.OutputDir) ? Path.GetDirectoryName(input) : Path.GetFullPath(args.OutputDir)) ?? String.Empty, outFilename);
            Console.WriteLine($@"Out: {output}");
            var jsonSchema = await JsonSchema.FromFileAsync(input);
            var generator = new CSharpGenerator(jsonSchema, new CSharpGeneratorSettings()
            {
                Namespace = args.Namespace
            });
            var generateFile = generator.GenerateFile();
            generateFile = generateFile.Replace(
                @"[System.CodeDom.Compiler.GeneratedCode(""NJsonSchema"", ""10.1.21.0 (Newtonsoft.Json v9.0.0.0)"")]",
                String.Empty);
            await File.WriteAllTextAsync($"{output}", generateFile);
        }
    }
}