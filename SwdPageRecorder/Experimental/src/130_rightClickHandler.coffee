# Ctrl + Right button
rightClickHandler = (event) -> 
     if event.ctrlKey
         event = window.event unless event?  #IE hack
         target = if 'target' of event then event.target else event.srcElement # another IE hack

         root = if document.compatMode is 'CSS1Compat' then document.documentElement else document.body
         mxy  = [event.clientX + root.scrollLeft, event.clientY + root.scrollTop]

         path = getPathTo(target)
         txy  = getPageXY(target)

         body = document.getElementsByTagName('body')[0]
         xpath = path

         JsonData = 
             "Command":   "GetXPathFromElement"
             "Caller":    "EventListener : mousedown"
             "CommandId":  pseudoGuid()
             "XPathValue": xpath

         createCommand(JsonData)

         showPos(event, xpath)

         return preventEvent(event)
