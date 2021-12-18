using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NewTestingProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void test()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            //Navigate To Olx
            driver.Url = "https://www.olx.com.pk/";

            //Select All Categories
            driver.FindElement(By.XPath("//img[contains(@alt, 'Dropdown arrow')]")).Click();
            Thread.Sleep(4000);
            
            //Select Mobile Phones & Tablet
            driver.FindElement(By.XPath("//a[contains(@class, '_7d3f8c9a')]")).Click();
            Thread.Sleep(7000);
        
            //Select Mobile and Categories
            driver.FindElements(By.XPath("//div[contains(@class, 'a09e74b3')]"))[4].Click();
            Thread.Sleep(5000);
           
            //Filter Price
            js.ExecuteScript("document.getElementsByClassName('fc60720d')[3].value='40000'");
            js.ExecuteScript("document.getElementsByClassName('fc60720d')[4].value='120000'");
            js.ExecuteScript("document.location.search+='?filter=price_between_40000_to_120000'");
            Thread.Sleep(5000);

            //Validate Price And Category of First 10 filtered Values
            for (int i = 0; i < 10; i++)
            {
             var MobileCards = driver.FindElements(By.XPath("//div[contains(@class, '_193d9c7f')]"));
             MobileCards[i].Click();
             Thread.Sleep(5000);
             string Price = driver.FindElement(By.XPath("//span[contains(@class, '_56dab877')]")).Text;
             string RealPrice = Price.Replace("Rs ", "");
             RealPrice = RealPrice.Replace(",", "");
             string Category = driver.FindElements(By.XPath("//a[contains(@class, '_151bd34b')]"))[2].Text;    
             Console.WriteLine("The value of Price and Category is:" + Category + Price);
             Assert.AreEqual(Category, "Mobile Phones", "Category Not Found");
             Assert.That(Decimal.Parse(RealPrice), Is.GreaterThanOrEqualTo(40000));
             Console.WriteLine("Price is Greater than 40000");
             Assert.That(Decimal.Parse(RealPrice), Is.LessThanOrEqualTo(120000));
             Console.WriteLine("Price is Less than 120000");
             driver.Navigate().Back();
             Thread.Sleep(5000);
            }
            Console.WriteLine("Pass");
            Thread.Sleep(5000);

        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }


    }
}
