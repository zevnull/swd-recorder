﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using System.Collections.ObjectModel;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using SwdPageRecorder.WebDriver;
using SwdPageRecorder.WebDriver.JsCommand;

using System.Xml;
using System.Xml.Linq;

using System.Windows.Forms;
using System.Diagnostics;

namespace SwdPageRecorder.UI
{
    public class FullHtmlSourceTabPresenter: IPresenter<FullHtmlSourceTabView>
    {
        private FullHtmlSourceTabView view;

        public void InitWithView(FullHtmlSourceTabView view)
        {
            this.view = view;
        }

        internal void DisplayHtmlPageSource()
        {
            string singleLineSource = SwdBrowser.GetTidyHtml();
            string[] htmlLines = SplitSingleLineToMultyLine(singleLineSource);
            view.FillHtmlCodeBox(htmlLines);
        }

        private string[] SplitSingleLineToMultyLine(string singleLineSource)
        {
            string[] result = singleLineSource.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }


    }
}