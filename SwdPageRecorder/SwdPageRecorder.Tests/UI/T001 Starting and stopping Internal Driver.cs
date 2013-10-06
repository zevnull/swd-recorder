﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using TestStack.BDDfy;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;

using FluentAssertions;

using SwdPageRecorder.UI;
using System.Windows.Forms;

using System.Threading;

using SwdPageRecorder.WebDriver;
using System.Diagnostics;

namespace SwdPageRecorder.Tests.UI.BrowserSettings
{
    
    public static class Helper 
    {
        public static void WaitFor(Func<bool> testAction, TimeSpan waitTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (sw.Elapsed < waitTime)
            {
                try
                {
                    bool successful = testAction();
                    if (successful)
                    {
                        return;
                    }
                }
                catch { };
                Thread.Sleep(1);
            }

            if (sw.Elapsed >= waitTime) throw new TimeoutException();

        }

        public static void WFAction<T>(T control, Action action) where T : Control
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

    }
    
    [TestFixture]
    public class T001_Starting_and_stopping_Internal_Driver
    {

        SwdMainView _mainForm = null;
        Thread thread;


        SwdMainView MainForm
        {
            get
            {

                if (_mainForm == null)
                {

                    ManualResetEvent started = new ManualResetEvent(false);
                    thread = new Thread(delegate()
                        {
                            _mainForm = new SwdMainView(started);
                            SWDRecorder_Program.Run(_mainForm);
                        });

                    thread.Start();
                    started.WaitOne(TimeSpan.FromSeconds(30));
                }
                
                return _mainForm;
            }
        }



        public IEnumerable<Control> ExpectedControls()
        {
            yield return MainForm.txtBrowserUrl;
            yield return MainForm.btnBrowser_Go;
            yield return MainForm.txtVisualSearchResult;
            yield return MainForm.btnStartVisualSearch;
            yield return MainForm.htmlDomTesterView1.btnTestLocator;
            yield return MainForm.selectorsEditView.btnHighlightWebElementInBrowser;
            yield return MainForm.fullHtmlSourceTabView1.btnGetHtmlSource;
            yield return MainForm.fullHtmlSourceTabView1.txtHtmlPageSource;
        }

        
        
        [Test]
        public void Before_Driver_was_started_the_depending_UI_elements_should_be_disabled()
        {
            
            this.When(_ => PrepareApplication(),        "When application is running")
                .And(_  => EmptyStep(),                 "But Driver was not started")
                .Then(_ => EnsureElementDisabled(ExpectedControls()), "Then ensure elements are disabled")
                .BDDfy();
        }

        [Test]
        public void When_driver_is_started_for_first_time_all_depending_UI_controls_should_be_enabled()
        {

            this.Given(_ => PrepareApplication(), "Given the application is running")
                .When(_ => StartDriver(), "When Driver was started")
                .Then(_ => EnsureElementEnabled(ExpectedControls()), "Then ensure depending UI controls are enabled")
                .And(_ => CheckDriverStartButton("Stop"), "And Driver Start button should change it’s caption to {0}")
                .BDDfy();
        }

        [Test]
        public void When_driver_was_stopped_then_depending_UI_controls_should_be_disabled()
        {

            this.Given(_ => PrepareApplication(), "Given the application is running")
                .And(_ => StartDriver(), "And Driver was started")
                .When( _=> StopDriver(), "When I stop the Driver")
                .Then(_ => EnsureElementDisabled(ExpectedControls()), "Then ensure depending UI controls are disabled")
                .And(_ => CheckDriverStartButton("Start"), "And Driver Start button should change it’s caption to {0}")
                .BDDfy();
        }

        private void StopDriver()
        {
            MainForm.browserSettingsTab1.Presenter.StopDriver();
        }

        private void CheckDriverStartButton(string expectedButtonCaption)
        {
            _mainForm.browserSettingsTab1.btnStartWebDriver.Text.Should().Be(expectedButtonCaption);
        }

        private void StartDriver()
        {
            var browserOptions = new WebDriverOptions()
            {
                BrowserName = WebDriverOptions.browser_PhantomJS,
                IsRemote = false,
                RemoteUrl = "",
            };

            MainForm.browserSettingsTab1.Presenter.SetBrowserStartupSettings(browserOptions);
            MainForm.browserSettingsTab1.Presenter.ClickStart();
            
            //MainForm.browserSettingsTab1.Presenter.StartNewBrowser(browserOptions);
            
        }

        private void EnsureElementDisabled(IEnumerable<Control> controls)
        {
            foreach (var control in controls)
            {
                control.Enabled.Should().BeFalse(@"{0} expected to be disabled, but was enabled", control.Name);
            }
        }

        private void EnsureElementEnabled(IEnumerable<Control> controls)
        {
            foreach (var control in controls)
            {
                control.Enabled.Should().BeTrue(@"{0} expected to be enabled, but was disabled", control.Name);
            }
        }


        private void EmptyStep()
        {
            
        }

        private void PrepareApplication()
        {
            MainForm.Visible = true;
        }

        [TearDown]
        public void TearDown()
        {

            try
            {
                SWDRecorder_Program.CloseApplication();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _mainForm = null;
                if (thread != null) thread.Abort();
                thread = null;
                GC.WaitForFullGCComplete();
            }

        }


    }
}
