window.Swd_addElement = () -> 
    codeIDTextElement   = document.getElementById "SwdPR_PopUp_CodeIDText"
    XPathLocatorElement = document.getElementById "SwdPR_PopUp_XPathLocator"
    
    JsonData = 
               "Command"   : "AddElement"
               "Caller"    : "addElement"
               "CommandId" : pseudoGuid()

               "ElementCodeName" : codeIDTextElement.value
               "ElementXPath"    : XPathLocatorElement.firstChild.nodeValue

    createCommand(JsonData)
