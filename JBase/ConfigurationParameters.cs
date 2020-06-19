using System;
using System.Collections.Generic;
using System.IO;

namespace Hydra.JsonSchema
{
    public class ConfigurationParameters : IConfigurationParameters
    {
        public IDictionary<string, string> Parameters
        {
            get { return _parameters; }
        }

        public ConfigurationParameters()
        {
            _keyLogger = key => { };
        }

        public IConfigurationParameters Init(IDictionary<string, string> parameters)
        {
            _parameters = parameters;
            return this;
        }

        public void SetKeyLogger(Action<string> keyLogger)
        {
            if (keyLogger != null)
            {
                _keyLogger = keyLogger;
            }
        }

        public T Get<T>(string key, T defaultValue)
        {
            return Get(key, null, () => defaultValue);
        }

        public T Get<T>(string key, Func<string, T> convert, T defaultValue)
        {
            return Get(key, convert, () => defaultValue);
        }

        public T Get<T>(string key, Func<string, T> convert = null, Func<T> defaultValue = null)
        {
            _keyLogger(key);

            if (_parameters == null)
            {
                return defaultValue != null ? defaultValue() : default(T);
            }

            var formatted = FormatKey(key);

            var value = GetValue(formatted) ?? GetValue(key);

            if (value != null)
            {
                return GetTypedValue(key, value, convert);
            }

            return defaultValue != null ? defaultValue() : default(T);
        }

        protected virtual string FormatKey(string key)
        {
            return key;
        }

        private IDictionary<string, string> _parameters;
        private Action<string> _keyLogger;

        private string GetValue(string key)
        {
            string value;
            return _parameters.TryGetValue(key, out value)
                ? value
                : null;
        }

        private static T GetTypedValue<T>(string key, string value, Func<string, T> convert)
        {
            try
            {
                return convert != null
                    ? convert(value)
                    : (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                throw new InvalidCastException(string.Format("Cannot convert {0} key value from {1} value to type {2}", key, value, typeof(T)));
            }
        }
    }
}