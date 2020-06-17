using System;
using System.IO;
using System.Threading.Tasks;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace JSchemaGenerator
{
    class Program
    {
        static async Task  Main(string[] args)
        {
            foreach (var fileName in args)
            {
                string f = Path.GetFullPath(fileName);
                var fName = $"{Path.GetFileNameWithoutExtension(fileName)}.cs";
                var sc = await JsonSchema.FromFileAsync(f);
                var generator = new CSharpGenerator(sc, new CSharpGeneratorSettings());
                var generateFile = generator.GenerateFile();
                generateFile = generateFile.Replace(
                    @"[System.CodeDom.Compiler.GeneratedCode(""NJsonSchema"", ""10.1.21.0 (Newtonsoft.Json v9.0.0.0)"")]",
                    string.Empty);
                File.WriteAllText($"{fName}", generateFile);
            }
        }
    }
}
