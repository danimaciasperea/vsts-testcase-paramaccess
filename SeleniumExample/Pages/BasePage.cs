using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.IE;
using System.Drawing;

namespace SeleniumExample.Pages
{

    class BasePage
    {
        protected IWebDriver _driver;

        protected BasePage()
        {

        }

        public void Close()
        {
            _driver.Close();
            _driver.Dispose();
        }

        public HomePage BrowseToHomePage (String homePageUrl)
        {
            _driver.Navigate().GoToUrl(homePageUrl);
            return new HomePage(_driver);
        }

        public static HomePage Launch(string homePageUrl, string browser)
        {
            //based on the browser passed in, create your web driver
            IWebDriver driver;
            if (browser.Equals("chrome"))
            {
                driver = new ChromeDriver();
            }
            else if (browser.Equals("phantomjs"))
            {
                driver = new PhantomJSDriver();
            }
            else
            {
                driver = new InternetExplorerDriver();
            }

            //set the window size of the browser and browse to the home page
            driver.Manage().Window.Size = new Size(1366, 768);
            driver.Navigate().GoToUrl("http://" + homePageUrl);
            return new HomePage(driver);
        }
    }
}
