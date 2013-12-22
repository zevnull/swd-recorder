﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.PhantomJS;
using System.Net;
using SwdPageRecorder.WebDriver.JsCommand;

using System.Xml;

using HtmlAgilityPack;

using Newtonsoft.Json;


namespace SwdPageRecorder.WebDriver.SwdBrowserUtils
{
    public static class JavaScriptUtils
    {


        public static string ReadJavaScriptFromFile(string filePath)
        {
            string contents = File.ReadAllText(filePath);
            return contents;
        }



        internal static void InjectVisualSearch(IWebDriver webDriver)
        {

            MyLog.Write("InjectVisualSearch: Started");
            string javaScript = ReadJavaScriptFromFile(Path.Combine("JavaScript", "ElementSearch.js"));
            IJavaScriptExecutor jsExec = webDriver as IJavaScriptExecutor;

            if (!IsVisualSearchScriptInjected(webDriver))
            {
                MyLog.Write("InjectVisualSearch: Was not injected. Perform Inject");
                jsExec.ExecuteScript(javaScript);
            }
            MyLog.Write("InjectVisualSearch: Finished");
        }

        public static bool IsVisualSearchScriptInjected(IWebDriver webDriver)
        {
            string jsCheckScript = @"return document.SWD_Page_Recorder === undefined ? 'false' : 'true';";
            IJavaScriptExecutor jsExec = webDriver as IJavaScriptExecutor;

            string isInjected = jsExec.ExecuteScript(jsCheckScript) as string;
            return isInjected == "true";
            
        }

        internal static void HighlightElement(By by)
        {
            throw new NotImplementedException();
        }

        internal static void HighlightElement(By by, IWebDriver webDriver)
        {
            var element = webDriver.FindElement(by);
            IJavaScriptExecutor jsExec = webDriver as IJavaScriptExecutor;
            jsExec.ExecuteScript(
            @"
                element = arguments[0];
                original_style = element.getAttribute('style');
                element.setAttribute('style', original_style + ""; background: yellow; border: 2px solid red;"");
                setTimeout(function(){
                    element.setAttribute('style', original_style);
                }, 300);

           ", element);
        }

        public static string GetElementXPath(IWebElement webElement, IWebDriver webDriver)
        {
            IJavaScriptExecutor jsExec = webDriver as IJavaScriptExecutor;
            return (string)jsExec.ExecuteScript(
@"
function getPathTo(element) {
    if (element === document.body)
        return '/html/' + element.tagName.toLowerCase();

    var ix = 0;
    var siblings = element.parentNode.childNodes;
    for (var i = 0; i < siblings.length; i++) {
        var sibling = siblings[i];
        if (sibling === element)
        {
            return getPathTo(element.parentNode) + '/' + element.tagName.toLowerCase() + '[' + (ix + 1) + ']';
        }
        if (sibling.nodeType === 1 && sibling.tagName === element.tagName)
            ix++;
    }
}

var element = arguments[0];
var xpath = '';
xpath = getPathTo(element);
return xpath;
", webElement);
        }

        internal static Dictionary<string, string> ReadElementAttributes(By by, IWebDriver webDriver)
        {
            /*
             * 
                [
                  {
                    "Key": "href",
                    "Value": "http://example.com"
                  },
                  {
                    "Key": "class",
                    "Value": " "
                  }
                ]
            */
            
            var result = new Dictionary<string, string>();

            var elements = webDriver.FindElements(by);

            if (elements.Count == 0)
            {
                throw new NotFoundException("ReadElementAttributes: Element was not found" + by.ToString());
            }

            var currentElement = elements[0];

            IJavaScriptExecutor jsExec = webDriver as IJavaScriptExecutor;
            string json = (string)jsExec.ExecuteScript(
            @"
                var jsonResult = ""[\n"";
                
                var attrs = arguments[0].attributes;
                for (var l = 0; l < attrs.length; ++l) {
                    var a = attrs[l]; 
                    
                    var name  = a.name.replace(/\\/g, ""\\\\"").replace(/\""/g, ""\\\"""");
                    var value = a.value.replace(/\\/g, ""\\\\"").replace(/\""/g, ""\\\"""");

                    jsonResult += '{ ""Key"": ""' + name + '"", ""Value"": ""' + value + '""},';

                }
                jsonResult += ""]\n"";
                
                return jsonResult;

            ", currentElement);

            MyLog.Write("JSON:\n" + json);
            
            var attributesList = DeserializeAttributesFromJson(json);

            foreach (var attr in attributesList)
            {
                result.Add(attr.Key, attr.Value);
            }

            result.Add("TagName", currentElement.TagName);

            return result;
            
        }

        public static ElementAttributesList DeserializeAttributesFromJson(string json)
        {
            var attributesList = JsonConvert.DeserializeObject<ElementAttributesList>(json);
            return attributesList;
        }

    }
}
