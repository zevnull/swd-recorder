using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using SwdPageRecorder.WebDriver;

using FluentAssertions;

namespace SwdPageRecorder.Tests.Integration.SwdWebDriver
{
    [TestClass]
    public class SwdBrowser_Tests : MyTest    
    {
        [TestMethod]
        public void Initialize_should_be_able_to_start_new_browser()
        {
            WebDriverOptions options = new WebDriverOptions()
            {
                 BrowserName = WebDriverOptions.browser_HtmlUnitWithJavaScript,
                 IsRemote = true,
                 RemoteUrl = "http://localhost:4444/wd/hub/",
            };

            SwdBrowser.RunStandaloneServer("start_selenium_server.bat");


            SwdBrowser.Initialize(options);

            var rempteDriver = (RemoteWebDriver) SwdBrowser.GetDriver();

            rempteDriver.Capabilities.BrowserName.Should().Be("htmlunit");
        }

    }
}
