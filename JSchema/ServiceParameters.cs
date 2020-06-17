using System.Collections.Generic;
using System.Linq;

namespace MyNamespace
{
    public partial class ServiceParameters : ConfigurationParameters
    {
        public ServiceParameters InitBackendParameter(string key, string value)
        {
            return InitBackendParameters(new Dictionary<string, string>
            {
                { key, value }
            });
        }

        public ServiceParameters InitBackendParameters(IDictionary<string, string> parameters)
        {
            return InitBackendParameters(parameters, null);
        }

        public ServiceParameters InitBackendParameters(IDictionary<string, string> parameters, string ipAddress)
        {
            _ipAddress = ipAddress;
            Init(parameters);
            return this;
        }

        public static string[] SplitKey(string key)
        {
            return !key.Any()
                ? new[] { string.Empty, string.Empty }
                : key.Split('_');
        }

        public static string CombineKey(string key, string overrideKey)
        {
            return string.Format("{0}_{1}", key, overrideKey);
        }

        protected override string FormatKey(string key)
        {
            return CombineKey(key, _ipAddress);
        }

        protected string _ipAddress;
    }
}