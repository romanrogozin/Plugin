using System;

namespace SchemaConfiguration
{
    public class SchemaBinding : Attribute
    {
        public string SchemaName { get; set; }

        public SchemaBinding(string schemaName)
        {
            SchemaName = schemaName;
        }
    }
}