using System;

namespace Plugin.Core
{
    public class InterfaceAttribute : Attribute
    {
        public string Name { get; set; }

        public InterfaceAttribute(string name)
        {
            Name = name;
        }
    }
}