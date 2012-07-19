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

        public void Validate(XDocument doc, XDocument schema, string xmlns = "")
        {
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