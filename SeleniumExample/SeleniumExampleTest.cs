using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using SeleniumExample.Pages;
using System.Data;
using ParamAccessHelper;
using System.Threading.Tasks;

namespace SeleniumExample
{
    [TestClass]
    public class SeleniumExampleTest
    {
        private static string _homePageUrl;
        private static HomePage _homePage;
        private static string _browserType;
        private static string _vstsPat;
        private static string _vstsUrl;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            var configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            _browserType = ConfigurationManager.AppSettings["browserType"];
            _homePageUrl = ConfigurationManager.AppSettings["appUrl"];
            _homePage = HomePage.Launch(_homePageUrl, _browserType);
            _vstsPat = ConfigurationManager.AppSettings["vstsPat"];
            _vstsUrl = ConfigurationManager.AppSettings["vstsUrl"];
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _homePage.Close();
        }

        [TestMethod]
        [TestCategory("SeleniumUITests")]
        public void TestCalculatorKeyPad_Add()
        {
            string x = "";
            string y = "";
            string r = "";

            //get the parameters from test case
            DataSet ds = GetParams("2333");

            //Loop over all the parameters
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                x = row["X"].ToString();
                y = row["Y"].ToString();
                r = row["R"].ToString();

                _homePage.BrowseToHomePage(_homePageUrl);
                _homePage.VerifyHomePageReached();
                _homePage.SearchForCalculator();

                //push a button for each numeral in the first number
                foreach(char c in x)
                {
                    _homePage.OperateKeypad(c.ToString());
                }

                _homePage.OperateKeypad("+");

                //push a button for each numeral in the second number
                foreach (char c in y)
                {
                    _homePage.OperateKeypad(c.ToString());
                }

                //Get the result
                _homePage.OperateKeypad("=");

                //Evaluate the result
                _homePage.EvaluateResult(r);
            }
        }

        [TestMethod]
        [TestCategory("SeleniumUITests")]
        public void TestCalculatorEquasionEntry_Add()
        {
            string x = "";
            string y = "";
            string r = "";

            //get the parameters from test case 2333
            DataSet ds = GetParams("2333");

            //Loop over all the parameters
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                x = row["X"].ToString();
                y = row["Y"].ToString();
                r = row["R"].ToString();
                //Code to get parameters from VSTS
                //Loop over param array
                _homePage.BrowseToHomePage(_homePageUrl);
                _homePage.VerifyHomePageReached();
                _homePage.SearchForCalculator();

                //constuct equasion
                _homePage.EnterEquasion(x + "+" + y + "=");

                //Check Answer
                _homePage.EvaluateResult(r);
            }
        }

        public DataSet GetParams(string testcaseID)
        {
            DataSet ds = new DataSet();
            GetTestCaseParams p = new GetTestCaseParams();
            p.VstsURI = _vstsUrl;
            p.Pat = _vstsPat;

            Task.Run(async () => { ds = await p.GetParams(testcaseID); }).GetAwaiter().GetResult();
            return ds;
        }

    }
}
