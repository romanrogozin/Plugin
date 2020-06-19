using System;
using System.Collections.Generic;

namespace Hydra.JsonSchema
{
    public partial class AuthorizationFactProcessing
    {
        public AuthorizationFactProcessing()
        {
            Delay = TimeSpan.FromMinutes(15);
        }
    }

    [JsonSchemaName("auth.json")]
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

}
