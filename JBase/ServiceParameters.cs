using System;
using System.Collections.Generic;
using System.Linq;

namespace SchemaConfiguration
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