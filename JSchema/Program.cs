using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MyNamespace;
using Newtonsoft.Json;

namespace MyNamespace
{
    public partial class AuthorizationFactProcessing
    {
        public AuthorizationFactProcessing()
        {
            Delay = TimeSpan.FromMinutes(15);
        }
    }

    public partial class AuthorizationParameters
    {
        private static readonly Dictionary<UserProfession, AccountFlagRule> _profPermissions = new Dictionary<UserProfession, AccountFlagRule>
            {
                { UserProfession.Developer,             AccountFlagRule.SignIn },
                { UserProfession.Bot,                   AccountFlagRule.Allow },
                { UserProfession.InternalTester,        AccountFlagRule.Allow },
                { UserProfession.InternalPlayTester,    AccountFlagRule.Allow },

                { UserProfession.BnetTester,            AccountFlagRule.Allow },
                { UserProfession.BnetPlayer,            AccountFlagRule.Allow },

                { UserProfession.SteamTester,           AccountFlagRule.Allow },
                { UserProfession.SteamPlayer,           AccountFlagRule.Allow },

                { UserProfession.XboxTester,            AccountFlagRule.Allow },
                { UserProfession.XboxPlayer,            AccountFlagRule.Allow },

                { UserProfession.PsnTester,             AccountFlagRule.Allow },
                { UserProfession.PsnPlayer,             AccountFlagRule.Allow },

                { UserProfession.EpicTester,            AccountFlagRule.Allow },
                { UserProfession.EpicPlayer,            AccountFlagRule.Allow },

                { UserProfession.FirebaseTester,        AccountFlagRule.Allow },
                { UserProfession.FirebasePlayer,        AccountFlagRule.Allow },

                { UserProfession.Player,                AccountFlagRule.Allow },
            };

        private static readonly List<string> _retailBuilds = new List<string> { "Retail" };

        private static readonly Dictionary<int, int> _signInTimeouts = new Dictionary<int, int>
            {
                { 95, 1000 },
                { 96, 2000 },
                { 97, 4000 }
            };

        public AuthorizationParameters()
        {
            SignInExpiration = TimeSpan.FromMinutes(1);
            WorkerSleepTimeout = TimeSpan.FromSeconds(30);
            SignInTimeouts = _signInTimeouts;
            QueueReaderSleepTimeout = TimeSpan.FromSeconds(1);
            RetailBuilds = _retailBuilds;
            AllowedProfessions = _profPermissions;
            SignInRateLimiterInterval = TimeSpan.FromSeconds(2);
        }
    }
  

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
           // var schema = JsonSchema.FromType<AuthorizationParameters>();
          //  var schemaData = schema.ToJson(); 
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
}
