using System;
using System.Collections.Generic;

namespace Hydra.JsonSchema
{

    [JsonSchemaName("example2.json")]
    public partial class AbstractDataServiceParameters
    {

    }

    [JsonSchemaName("example.json")]
    public partial class OfferParameters
    {
        private static readonly Dictionary<OfferAclType, ServiceAccessRole> _default = new Dictionary<OfferAclType, ServiceAccessRole>
        {
            { OfferAclType.Game,      ServiceAccessRole.GameClient },
            { OfferAclType.DedicatedServer, ServiceAccessRole.DedicatedServer },
            { OfferAclType.WebPortal, ServiceAccessRole.WebPortal },
            { OfferAclType.Services,  ServiceAccessRole.Services | ServiceAccessRole.Debug }
        };

        public OfferParameters()
        {
            OfferAccessRoleMap = _default;
            SteamTransactionsEnabled = true;
            DigitalRiverTransactionsEnabled = true;
            GooglePlayTransactionsEnabled = true;
            AppStoreTransactionsEnabled = true;
            DisableAppStoreReceiptVerification = false;
            DisableGooglePlayTransactionVerification = false; // true by DEFAULT!
            MassOffersCacheInvalidationInterval = TimeSpan.FromMinutes(30);
        }
    }
}
