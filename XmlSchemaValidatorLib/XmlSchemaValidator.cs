using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace XmlSchemaValidatorLib
{
    public sealed class XmlSchemaValidator
    {
        #region Singleton

        private static readonly Lazy<XmlSchemaValidator> Lazy =
            new Lazy<XmlSchemaValidator>(() => new XmlSchemaValidator());

        public static XmlSchemaValidator Instance
        {
            get { return Lazy.Value; }
        }

        private XmlSchemaValidator()
        {
        }

        #endregion

        public string GetFirstXmlnsFromSchema(XDocument schema)
        {
            string xmlns = null;

            if (schema.Root != null)
            {
                var xmlnsAttribute = schema.Root.Attribute("targetNamespace");
                if (xmlnsAttribute != null && string.IsNullOrEmpty(xmlnsAttribute.Value))
                {
                    xmlns = xmlnsAttribute.Value;
                }
            }

            return xmlns;
        }

        public void Validate(XDocument doc, XDocument schema, string xmlns = "")
        {
            if (string.IsNullOrEmpty(xmlns))
            {
                xmlns = GetFirstXmlnsFromSchema(schema);
            }

            var schemas = new XmlSchemaSet();
            schemas.Add(xmlns, XmlReader.Create(new StringReader(schema.ToString())));

            doc.Validate(schemas, (sender, args) =>
                                      {
                                          throw new Exception(args.Message, args.Exception);
                                      }, true);
        }

        public bool IsValid(XDocument doc, XDocument schema, string xmlns = "")
        {
            var isValid = true;

            if (string.IsNullOrEmpty(xmlns))
            {
                xmlns = GetFirstXmlnsFromSchema(schema);
            }

            var schemas = new XmlSchemaSet();
            schemas.Add(xmlns, XmlReader.Create(new StringReader(schema.ToString())));

            doc.Validate(schemas, (sender, args) =>
                                      {
                                          isValid = false;
                                      }, true);

            return isValid;
        }
    }
}