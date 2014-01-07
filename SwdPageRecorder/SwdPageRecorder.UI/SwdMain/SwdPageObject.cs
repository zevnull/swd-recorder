using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SwdPageRecorder.UI
{
    [Serializable]
    public class SwdPageObject
    {
        public string PageObjectName { get; set; }

        public int Version { get; set; }

        public List<WebElementDefinition> Items { get; set; }

        public SwdPageObject()
        {
            PageObjectName = "SwdPageObjectClass";
            Items = new List<WebElementDefinition>();
            Version = 1;
        }
    }
}
