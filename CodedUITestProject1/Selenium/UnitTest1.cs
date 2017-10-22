using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium;//引用Selenium
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UITesting;//支持Chrome

namespace CodedUITestProject1.Selenium
{
    [TestClass]
    public class SeleniumTestClass
    {
        [TestMethod]
        public void TestMethod1()
        {
            IWebDriver selenium = new ChromeDriver();

            selenium.Navigate().GoToUrl("http://url.cn/5rUmF4D");
            var btn = selenium.FindElement(By.Id("enterButton"));
            btn.Click();
            Thread.Sleep(1000);
 
            selenium.Navigate().GoToUrl("http://www.baidu.com");
        }
    }
}
