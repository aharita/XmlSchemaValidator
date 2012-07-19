using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmlSchemaValidatorLib.Tests
{
    /// <summary>
    ///This is a test class for XmlSchemaValidatorTest and is intended
    ///to contain all XmlSchemaValidatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlSchemaValidatorTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for Validate
        ///</summary>
        [TestMethod()]
        public void ValidateTest()
        {
            var xmlDoc = XDocument.Load("books.xml");
            var xmlSchema = XDocument.Load("books.xsd");
            const string xmlns = "urn:books";

            XmlSchemaValidator.Instance.Validate(xmlDoc, xmlSchema, xmlns);

            Assert.IsTrue(true);
        }

        /// <summary>
        ///A test for Validate
        ///</summary>
        [TestMethod()]
        public void IsValidTest()
        {
            var xmlDoc = XDocument.Load("books.xml");
            var xmlSchema = XDocument.Load("books.xsd");
            const string xmlns = "urn:books";

            var result = XmlSchemaValidator.Instance.IsValid(xmlDoc, xmlSchema, xmlns);

            Assert.IsTrue(result);
        }
    }
}
