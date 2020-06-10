using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MyNamespace;
using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Annotations;
using NJsonSchema.CodeGeneration.CSharp;
using JSchema;

namespace MyNamespace
{
    // MANUAL!!!!
    public partial class OfferParameters
    {
        private static readonly Dictionary<OfferAclType, ServiceAccessRole> _default = new Dictionary<OfferAclType, ServiceAccessRole>
        {
            { OfferAclType.Game,      ServiceAccessRole.GameClient },
            { OfferAclType.DedicatedServer, ServiceAccessRole.DedicatedServer },
            { OfferAclType.WebPortal, ServiceAccessRole.WebPortal },
            { OfferAclType.Services,  ServiceAccessRole.Services | ServiceAccessRole.Debug }
        };

        public static OfferParameters Default()
        {
            return new Configurator<OfferParameters>()
                .Setup(parameters =>
                {
                    parameters.OfferAccessRoleMap = _default;
                    parameters.SteamTransactionsEnabled = true;
                    parameters.DigitalRiverTransactionsEnabled = true;
                    parameters.GooglePlayTransactionsEnabled = true;
                    parameters.AppStoreTransactionsEnabled = true;
                    parameters.DisableAppStoreReceiptVerification = false;
                    parameters.DisableGooglePlayTransactionVerification = false; // true by DEFAULT!
                    parameters.MassOffersCacheInvalidationInterval = TimeSpan.FromMinutes(30);
                });
        }
    }
}

namespace JSchema
{
    public class Configurator<T> where T : class, new()
    {
        private readonly T _parameter;
        public Configurator()
        {
            _parameter = new T();
        }
        public T Setup(Action<T> configure)
        {
            configure(_parameter);
            return _parameter;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var t = OfferParameters.Default();
            //var schema = JsonSchema.FromType<OfferParameters>();
            //var u = schema.ToJson();
            var sc = await JsonSchema.FromFileAsync("example.jschema");
            var generator = new CSharpGenerator(sc, new CSharpGeneratorSettings()
            {
                //GenerateDataAnnotations = true,
                //GenerateOptionalPropertiesAsNullable = true,
                //GenerateDefaultValues = true,
                //GenerateImmutableArrayProperties = true,
                //GenerateImmutableDictionaryProperties = true
            });
            var generateFile = generator.GenerateFile();
            await File.WriteAllTextAsync("..\\..\\..\\example.cs", generateFile);
            var serializeObject = JsonConvert.SerializeObject(t);
            await File.WriteAllTextAsync("..\\..\\..\\out.json", serializeObject);
            var text = File.ReadAllText("..\\..\\..\\out.json");
            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}
