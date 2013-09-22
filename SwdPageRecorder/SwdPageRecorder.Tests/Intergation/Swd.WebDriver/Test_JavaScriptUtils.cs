using System;
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
using SwdPageRecorder.WebDriver.SwdBrowserUtils;

namespace SwdPageRecorder.Tests.Intergation.Swd.WebDriver
{
    
    public class Test_JavaScriptUtils
    {
        [TestFixture]
        public class DeserializeAttributesFromJson
        {
            [Test]
            public void When_JSON_Value_is_empty()
            {
                string json =
@"[
    { ""Key"": ""id"", ""Value"": ""nb_char_size""},
    { ""Key"": ""onchange"", ""Value"": ""maskChange(event);""},
    { ""Key"": ""value"", ""Value"": """"},

]";
                
                ElementAttributesList attributes = JavaScriptUtils.DeserializeAttributesFromJson(json);
                ElementAttributesList expected = new ElementAttributesList()
                {
                    new KeyValuePair<string, string>("id", "nb_char_size"),
                    new KeyValuePair<string, string>("onchange", "maskChange(event);"),
                    new KeyValuePair<string, string>("value", ""),
                };

                attributes.Should().BeEquivalentTo(expected);


            }

        }
    }
}
