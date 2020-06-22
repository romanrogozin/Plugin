//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.1.21.0 (Newtonsoft.Json v9.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------

namespace SchemaConfiguration
{
    #pragma warning disable // Disable all warnings

    
    public partial class ServiceParameters 
    {
    
    }
    
    
    public enum OfferAclType
    {
        None = 0,
    
        Game = 1,
    
        DedicatedServer = 2,
    
        WebPortal = 4,
    
        Services = 8,
    
        Internal = 14,
    
        Public = 15,
    
    }
    
    
    public enum ServiceAccessRole
    {
        Debug = 1,
    
        Guest = 2,
    
        Services = 4,
    
        GameClient = 8,
    
        DedicatedServer = 16,
    
        Game = 24,
    
        DsmAgent = 32,
    
        WebPortal = 64,
    
        BotManager = 128,
    
        CrashAnalyzer = 256,
    
        Tool = 304,
    
        ESL = 512,
    
        BILookup = 1024,
    
        Diagnostics = 2048,
    
    }
    
    /// <summary>BLABLA</summary>
    
    public partial class OfferParameters : ServiceParameters
    {
        [Newtonsoft.Json.JsonProperty("OfferAccessRoleMap", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.IDictionary<OfferAclType, ServiceAccessRole> OfferAccessRoleMap { get; set; }
    
        [Newtonsoft.Json.JsonProperty("SteamTransactionsEnabled", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool SteamTransactionsEnabled { get; set; }
    
        [Newtonsoft.Json.JsonProperty("DigitalRiverTransactionsEnabled", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool DigitalRiverTransactionsEnabled { get; set; }
    
        [Newtonsoft.Json.JsonProperty("GooglePlayTransactionsEnabled", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool GooglePlayTransactionsEnabled { get; set; }
    
        [Newtonsoft.Json.JsonProperty("AppStoreTransactionsEnabled", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool AppStoreTransactionsEnabled { get; set; }
    
        [Newtonsoft.Json.JsonProperty("DisableAppStoreReceiptVerification", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool DisableAppStoreReceiptVerification { get; set; }
    
        [Newtonsoft.Json.JsonProperty("DisableGooglePlayTransactionVerification", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool DisableGooglePlayTransactionVerification { get; set; } = true;
    
        /// <summary>BLABLABLA descrption</summary>
        [Newtonsoft.Json.JsonProperty("MassOffersCacheInvalidationInterval", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.TimeSpan MassOffersCacheInvalidationInterval { get; set; }
    
    
    }
}