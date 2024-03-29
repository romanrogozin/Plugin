﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Plugin.Core
{
    public class PluginManager
    {
        private readonly IConfigurationProvider _configurationProvider;
        readonly Dictionary<string, object> _plugins = new Dictionary<string, object>();

        public PluginManager(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            InitializePlugins();
        }

        private void InitializePlugins()
        {
            var baseType = typeof(IPluginBase);
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            foreach (string path in Directory.GetFiles(basePath, "*.dll", SearchOption.TopDirectoryOnly))
            {
                var assembly = Assembly.LoadFile(path);
                var types = GetTypesImplementingGenericType(baseType, assembly);

                foreach (var type in types)
                {
                    //var instance = Activator.CreateInstance(assembly.FullName, type.FullName);
                    Type genericArg = null;
                    foreach (var intType in type.GetInterfaces())
                    {
                        if (intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IPlugin<>))
                        {
                            genericArg = intType.GetGenericArguments().FirstOrDefault();
                            if (genericArg != null)
                                break;
                        }
                    }

                    if (genericArg != null)
                    {
                        var instance = Activator.CreateInstance(type);
                        if (!_plugins.ContainsKey(genericArg.FullName))
                        {
                            if (type.GetInterfaces().Any(f => f.GetGenericTypeDefinition() == typeof(IPluginGameConfiguration<>)))
                            {
                                var methodInfo = type.GetMethod("Initialize");
                                if (methodInfo != null)
                                {
                                    methodInfo.Invoke(instance, new object[] { _configurationProvider });
                                }
                            }
                            _plugins.Add(genericArg.FullName, instance);
                        }
                    }

                }
            }
        }

        public Task ExecuteCommand<T>(T method) where T : BasePluginCommand
        {
            return GetPlugin<T>().Execute(method);
        }

        public IPlugin<T> GetPlugin<T>() where T : BasePluginCommand
        {
            var type = typeof(T);
            if (_plugins.TryGetValue(type.FullName, out var pluginInstance))
                // return (IPlugin<T>)((ObjectHandle)pluginInstance).Unwrap();
                return (IPlugin<T>)(pluginInstance);
            throw new KeyNotFoundException(type.FullName);
        }

        private static IEnumerable<Type> GetTypesImplementingGenericType(Type genericType, Assembly assembly)
        {
            return from x in SafeGetTypes(assembly)
                   from z in x.GetInterfaces()
                   let y = x.BaseType
                   where
                       (y != null && y.IsGenericType &&
                        genericType.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
                       (z.IsGenericType &&
                        genericType.IsAssignableFrom(z.GetGenericTypeDefinition()))
                   select x;
        }

        private static IEnumerable<Type> SafeGetTypes(Assembly asm)
        {
            try
            {
                return asm.GetTypes();
            }
            catch
            {
                // ignore
                return Enumerable.Empty<Type>();
            }
        }
    }

}