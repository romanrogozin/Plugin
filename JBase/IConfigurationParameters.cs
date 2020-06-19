using System;
using System.Collections.Generic;

namespace Hydra.JsonSchema
{
    public interface IConfigurationParameters
    {
        IDictionary<string, string> Parameters { get; }

        IConfigurationParameters Init(IDictionary<string, string> parameters);

        void SetKeyLogger(Action<string> keyLogger);

        T Get<T>(string key, Func<string, T> convert, T defaultValue);

        T Get<T>(string key, Func<string, T> convert, Func<T> defaultValue = null);
    }
}