using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RazorEngine;
using SwdPageRecorder.UI.CodeGeneration;

namespace SwdPageRecorder.UI
{
    public class CSharpPageObjectGenerator
    {
        internal string[] Generate(WebElementDefinition[] definitions, string fullTemplatePath)
        {
            var template = File.ReadAllText(fullTemplatePath);
            var result = Razor.Parse(template, 
                new {
                        WebElementDefinitions = definitions,
                        ExternalGenerator = new ExternalGenerator(),
                
                    });
            
            return Utils.SplitSingleLineToMultyLine(result);
        }
    }
}
