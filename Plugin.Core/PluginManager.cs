using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading.Tasks;

namespace Plugin.Core
{
    public class PluginManager
    {
        readonly Dictionary<string, object> _plugins = new Dictionary<string, object>();

        public void Load()
        {
            var baseType = typeof(IPluginBase);
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            foreach (string path in Directory.GetFiles(basePath, "*.dll", SearchOption.TopDirectoryOnly))
            {
                var assembly = Assembly.LoadFile(path);
                var types = GetTypesImplementingGenericType(baseType, assembly);

                foreach (var type in types)
                {
                    var instance = Activator.CreateInstance(assembly.FullName, type.FullName);
                    {
                        Type genericArg = null;
                        foreach (var intType in type.GetInterfaces())
                        {
                            if (intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IPlugin<>))
                            {
                                genericArg = intType.GetGenericArguments().SingleOrDefault();
                                if(genericArg!=null)
                                    break;
                            }
                        }

                        _plugins.Add(genericArg.FullName, instance);
                    }
                }
            }
        }

        public Task Invoke<T>(T method)
        {
            return GetPlugin<T>().Handle(method);
        }

        private IPlugin<T> GetPlugin<T>()
        {
            var type = typeof(T);
            if (_plugins.TryGetValue(type.FullName, out var pluginInstance))
                return (IPlugin<T>)((ObjectHandle)pluginInstance).Unwrap();
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
                // ign
                return Enumerable.Empty<Type>();
            }
        }
    }
}