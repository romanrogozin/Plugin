using System;

namespace Hydra.JsonSchema
{
    public class JsonSchemaNameAttribute : Attribute
    {
        public string Name { get; set; }

        public JsonSchemaNameAttribute(string name)
        {
            Name = name;
        }
    }
}