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

        public string GetXmlnsFromSchema(XDocument schema)
        {
            string xmlns = null;

            if (schema.Root != null)
            {
                var xmlnsAttribute = schema.Root.Attribute("targetNamespace");
                if (xmlnsAttribute != null)
                {
                    xmlns = xmlnsAttribute.Value;
                }
            }

            return xmlns;
        }

        public string GetXmlnsFromDocument(XDocument doc)
        {
            string xmlns = null;

            if (doc.Root != null)
            {
                xmlns = doc.Root.Name.Namespace.NamespaceName;
            }

            return xmlns;
        }

        public void Validate(XDocument doc, XDocument schema, string xmlns = "")
        {
            if (string.IsNullOrEmpty(xmlns))
            {
                xmlns = GetXmlnsFromSchema(schema);
            }

            // 1. Verify that the document has a namespace
            if (!string.IsNullOrEmpty(xmlns) && string.IsNullOrEmpty(GetXmlnsFromDocument(doc)))
            {
                throw new Exception("Xml document does not have a schema");
            }

            // 2. Validate against schema
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
                xmlns = GetXmlnsFromSchema(schema);
            }

            // 1. Verify that the document has a namespace
            if (!string.IsNullOrEmpty(xmlns) && string.IsNullOrEmpty(GetXmlnsFromDocument(doc)))
            {
                return false;
            }

            // 2. Validate against schema
            var schemas = new XmlSchemaSet();
            schemas.Add(xmlns, XmlReader.Create(new StringReader(schema.ToString())));
            doc.Validate(schemas, (sender, args) =>
            {
                throw new Exception(args.Message, args.Exception);
            }, true);

            return isValid;
        }
    }
}