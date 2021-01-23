using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCarlistTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (IWebDriver driver = new ChromeDriver())
                {
                    WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(10));
                    driver.Navigate().GoToUrl("http://carlist.my");

                    //click on the Condition/Type dropdown 
                    IWebElement dropElement = driver.FindElement(By.CssSelector(".selectize-input.items.input-readonly.not-full.has-options"));
                    dropElement.Click();

                    //click on the Used option in the dropdwon to select
                    driver.FindElement(By.XPath("//*[@data-value='used']")).Click();

                    //click on the Search button
                    IWebElement btnElement = driver.FindElement(By.CssSelector(".btn.btn--primary.one-whole"));
                    btnElement.Click();

                    wait.Until(webDriver => webDriver.FindElement(By.CssSelector(".listing__img.valign--top.lazy-loaded")).Displayed);
                    IWebElement firstResult = driver.FindElement(By.CssSelector(".listing__img.valign--top.lazy-loaded"));
                    firstResult.Click();

                    wait.Until(webDriver => webDriver.FindElement(By.CssSelector(".listing__price.delta.weight--bold")).Displayed);
                    string firstPricing = driver.FindElement(By.CssSelector(".listing__price.delta.weight--bold")).Text;

                    decimal value = -1;
                    CultureInfo currentCulture = CultureInfo.CreateSpecificCulture("en-MY");

                    if (System.Decimal.TryParse(firstPricing.Trim(), NumberStyles.Number | NumberStyles.AllowCurrencySymbol, currentCulture, out value))
                    {
                        Assert.IsTrue(value > 1000, "Expected car price to be greater than 1000.");
                        Console.WriteLine("=====================");
                        Console.WriteLine("Amount is more than 1000. Assertion passed.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("============= Catch Error or Assert failed =============");
                Console.WriteLine(e);
            }
            Console.Read();
        }                               
    }
}
