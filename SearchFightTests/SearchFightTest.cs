using System;
using NUnit.Framework;
using SearchFight.SearchEngine;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void IntegrationTestConnectivity()
        {
           

            HttpTextClient client = new HttpTextClient();
            string result = string.Empty;

            try
            {
                result = client.GetResponseText(new System.Uri("https://www.google.com"));
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);

            }

            Assert.AreNotEqual(result, string.Empty);

        }


        [Test]
        public void QueryFormatterTest()
        {

            DefaultQueryFormatter formatter = new DefaultQueryFormatter();
           string result= formatter.FormatQuery("q");


            Assert.AreEqual(result, "q");
        
        }

        [Test]
        public void ConfigurationEngineTest()
        {

            ConfigurationFactory factory = new ConfigurationFactory();
            Assert.Greater(factory.CreateEngines().Count, 0);



        }

 

    }
}