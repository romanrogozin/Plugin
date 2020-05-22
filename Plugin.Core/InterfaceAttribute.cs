using System;

namespace Plugin.Core
{
    public class InterfaceAttribute : Attribute
    {
        public Type Type { get; set; }

        public InterfaceAttribute(Type type)
        {
            Type = type;
        }
    }
}