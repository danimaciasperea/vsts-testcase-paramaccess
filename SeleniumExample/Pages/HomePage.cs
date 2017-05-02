using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace SeleniumExample.Pages
{
    class HomePage : BasePage
    {
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public HomePage VerifyHomePageReached()
        {
            try
            {
                var homePageTitleElement = _driver.FindElement(By.Id("sb_form_q"));
            }
            catch(Exception e)
            {
                Assert.Fail("Could not find ID sb_form_q: " + e.Message);
            }
            return this;
        }

        public HomePage SearchForCalculator()
        {
            try
            {
                var searchBox = _driver.FindElement(By.Id("sb_form_q"));
                searchBox.SendKeys("Calculator");

                var searchBoxSubmit = _driver.FindElement(By.Id("sb_form_go"));
                searchBoxSubmit.Click();

                var zeroButton = _driver.FindElement(By.Id("rcOne"));
        }
            catch(Exception e)
            {
                Assert.Fail("Calculator Not Loaded: " + e.Message);
            }
            return this;
        }

        public HomePage OperateKeypad(string n)
        {
            try
            {
                string eId = "";
                switch(n)
                {
                    case "0":
                        {
                            eId = "rcZero";
                            break;
                        }
                    case "1":
                        {
                            eId = "rcOne";
                            break;
                        }
                    case "2":
                        {
                            eId = "rcTwo";
                            break;
                        }
                    case "3":
                        {
                            eId = "rcThree";
                            break;
                        }
                    case "4":
                        {
                            eId = "rcFour";
                            break;
                        }
                    case "5":
                        {
                            eId = "rcFive";
                            break;
                        }
                    case "6":
                        {
                            eId = "rcSix";
                            break;
                        }
                    case "7":
                        {
                            eId = "rcSeven";
                            break;
                        }
                    case "8":
                        {
                            eId = "rcEight";
                            break;
                        }
                    case "9":
                        {
                            eId = "rcNine";
                            break;
                        }
                    case "+":
                        {
                            eId = "rcAdd";
                            break;
                        }
                    case "-":
                        {
                            eId = "rcSub";
                            break;
                        }
                    case "*": case "x": case "X":
                        {
                            eId = "rcMul";
                            break;
                        }
                    case "/":
                        {
                            eId = "rcDiv";
                            break;
                        }
                    case "=":
                        {
                            eId = "rcEquals";
                            break;
                        }
                    default:
                        {
                            eId = "";
                            break;
                        }
                }
                var calcButton = _driver.FindElement(By.Id(eId));
                calcButton.Click();

            }
            catch(Exception e)
            {

            }
            return this;
        }

        public HomePage EnterEquasion(string eq)
        {
            var a = _driver.FindElement(By.ClassName("rcABP"));
            a.Click();

            Actions test = new Actions(_driver);
            test.SendKeys(eq).Build().Perform();

            return this;
     
        }

        public HomePage EvaluateResult(string r)
        {
            try
            {
                var calcResults = _driver.FindElement(By.Id("rcTB"));

                Assert.AreEqual(calcResults.Text, r);
             
            }
            catch(Exception e)
            {
                Assert.Fail("Problem getting results: " + e.Message);
            }
            return this;
        }
    }
}
