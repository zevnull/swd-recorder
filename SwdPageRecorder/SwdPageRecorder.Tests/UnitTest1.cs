using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;


namespace SwdPageRecorder.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Application app = Application.Launch(@"D:\projects_current\swd-recorder\Bin\SwdPageRecorder.UI\SwdPageRecorder.UI.exe");
            var mainWin = app.GetWindows().First( w => w.Title.StartsWith("SWD"));

            var button = mainWin.Get<Button>(SearchCriteria.ByText("Start"));

            button.Click();





        }
    }
}
