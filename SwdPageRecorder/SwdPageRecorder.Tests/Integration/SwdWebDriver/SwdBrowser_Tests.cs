using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SwdPageRecorder.WebDriver;

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
                 BrowserName = WebDriverOptions.browser_PhantomJS,
                 IsRemote = true,
                 RemoteUrl = "http://localhost:4444/wd/hub/",
            };

            SwdBrowser.RunStandaloneServer("start_selenium_server.bat");


            //SwdBrowser.Initialize(options);
        }

    }
}
