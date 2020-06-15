using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using Sample;

namespace Sample
{
    public partial class OfferParameters
    {
        private static readonly Dictionary<int, ServiceAccessRole> _default = new Dictionary<int, ServiceAccessRole>
        {
            { (int)OfferAclType.Game,      ServiceAccessRole.GameClient },
            { (int)OfferAclType.DedicatedServer, ServiceAccessRole.DedicatedServer },
            { (int)OfferAclType.WebPortal, ServiceAccessRole.WebPortal },
            { (int)OfferAclType.Services,  ServiceAccessRole.Services | ServiceAccessRole.Debug }
        };

        public static OfferParameters Default()
        {
            return new OfferParameters()
            {
                AppStoreTransactionsEnabled = true,
                DisableAppStoreReceiptVerification = true,
                OfferAccessRoleMap =
                {
                   _default
                },

                MassOffersCacheInvalidationInterval = Timestamp.Normalize(TimeSpan.FromSeconds(30).Seconds, 0)
            };
            //return new Configurator<OfferParameters>()
            //    .Setup(parameters =>
            //    {
            //     //   parameters.OfferAccessRoleMap = new MapField<int, ServiceAccessRole> {_default}; fuck it
            //        parameters.SteamTransactionsEnabled = true;
            //        parameters.DigitalRiverTransactionsEnabled = true;
            //        parameters.GooglePlayTransactionsEnabled = true;
            //        parameters.AppStoreTransactionsEnabled = true;
            //        parameters.DisableAppStoreReceiptVerification = false;
            //        parameters.DisableGooglePlayTransactionVerification = false; // true by DEFAULT!
            //        parameters.MassOffersCacheInvalidationInterval =
            //            Timestamp.Normalize(TimeSpan.FromSeconds(30).Seconds, 0);
            //    });
        }
    }
}


namespace Proto3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var record = OfferParameters.Default();
            var serializeObject = JsonConvert.SerializeObject(record);
            await File.WriteAllTextAsync("out.json", serializeObject);
            var text = File.ReadAllText("out.json");
            var parameters = JsonConvert.DeserializeObject<OfferParameters>(text);
            Console.WriteLine(text);
            Console.WriteLine(parameters.MassOffersCacheInvalidationInterval.Seconds);
        }
    }
}
