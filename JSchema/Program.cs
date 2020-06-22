using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SchemaConfiguration;
using Newtonsoft.Json;

namespace JSchema
{
    class Program
    {


        static async Task Main(string[] args)
        {
            // var schema = JsonSchema.FromType<AuthorizationParameters>();
            //  var schemaData = schema.ToJson(); 
            foreach (Type p in AppDomain.CurrentDomain.GetAssemblies().SelectMany(g => g.GetTypesWithSchemaBindingAttribute()))
            {
                {
                    Console.WriteLine(p.ExtractMeta());
                }
            }



            var t = new AuthorizationParameters();

            var serializeObject = JsonConvert.SerializeObject(t);
            await File.WriteAllTextAsync("out.json", serializeObject);
            var text = File.ReadAllText("out.json");
            var obj = JsonConvert.DeserializeObject<OfferParameters>(text);
            Console.WriteLine(text);
            Console.WriteLine(obj.MassOffersCacheInvalidationInterval);
            Console.ReadKey();
        }
    }

    public static class MyClass
    {
        public static IEnumerable<Type> GetTypesWithSchemaBindingAttribute(this Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(SchemaBinding), false).Length > 0)
                {
                    yield return type;
                }
            }
        }

        public static string ExtractMeta(this Type type)
        {
            var attribute = type.GetCustomAttribute<SchemaBinding>();
            if (attribute == null) return null;
            
            var assembly = Assembly.GetAssembly(type);
            if (assembly == null) return null;

            var resource = assembly.GetManifestResourceNames().SingleOrDefault(s => s.Contains(attribute.SchemaName));
            if (resource == null) return null;

            using (var stream = assembly.GetManifestResourceStream(resource))
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        return result;
                    }

            return null;
        }
    }
}
