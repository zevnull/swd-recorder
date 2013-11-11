# TODO: Description 
pseudoGuid = () ->
    result = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'
    result = result.replace /[xy]/g, (re_match) ->
                        random_value = Math.random() * 16 | 0
                        replacement = if re_match is 'x' then  random_value else random_value & 0x3 | 0x8
                        return replacement.toString(16)

    return result

# TODO: Description 
getInputElementsByTypeAndValue = (inputType, inputValue) ->
    allDocumentInputElements = document.getElementsByTagName('input')

    result = new Array();

    for inputElement in allDocumentInputElements
        if inputElement.type is inputType and inputElement.value is inputValue
            result.push inputElement

    return result;

# TODO: Description 
getPathTo = (element) ->
    elementTagName = element.tagName.toLowerCase()

    # Check if node has ID and this ID is unique over the document
    if element.id and document.getElementById(element.id) is element
        return "id(\"#{element.id}\")"

    # Check element name
    else if element.name and document.getElementsByName(element.name).length is 1
        return "//#{elementTagName}[@name='#{element.name}']"
    
    # Submit value
    else if elementTagName is "input" and getInputElementsByTypeAndValue("submit", element.value).length is 1
        ### 
        Avoid using Russian in @value. Allow only English
        Bug #18 - ERROR Object reference not set to an instance of an object.
        ### 
        
        if element.value.match(/^[a-zA-Z0-9 \!\"\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`\{\|\}\~]*$/) 
            return "input[@type='submit' and @value='#{element.value}']"
        else 
            console.log("SWD: Value skipped: #{element.value}")
   

    if element is document.body
        return "/html/#{elementTagName}"

    ix = 0
    siblings = element.parentNode.childNodes

    ELEMENT_NODE_TYPE = 1

    for sibling in siblings
        
        continue if sibling.nodeType isnt ELEMENT_NODE_TYPE

        if sibling is element
            return "#{getPathTo(element.parentNode)}/#{elementTagName}[#{ix + 1}]"
        
        siblingTagName = sibling.tagName.toLowerCase()
        elementTagName = element.tagName.toLowerCase()

        ix++ if sibling.nodeType is 1 and siblingTagName is elementTagName

# TODO: Description 
getPageXY = (element) ->
    x = 0
    y = 0;
    while element
        x += element.offsetLeft
        y += element.offsetTop
        element = element.offsetParent
    return [x, y]

# TODO: Description
createCommand = (jsonData) ->
    myJSONText = JSON.stringify(jsonData, null, 2)
    window.swdpr_command = myJSONText

# TODO: Description
addStyle = (str) ->
    el = document.createElement('style')
    if el.styleSheet 
        el.styleSheet.cssText = str
    else 
        el.appendChild(document.createTextNode(str))

    return document.getElementsByTagName('head')[0].appendChild(el)

# TODO: Description
preventEvent = (event) -> 

    event.preventDefault() if (event.preventDefault) 
    event.returnValue = false

    # IE9 & Other Browsers
    if event.stopPropagation
        event.stopPropagation()
    
    # IE8 and Lower
    else
        event.cancelBubble = true

    return false

# Globals
prev = undefined
window.Swd_prevActiveElement = undefined

# TODO: Description
handler = (event) ->
   return if event.target is document.body or (prev and prev is event.target)

   if prev
       prev.className = prev.className.replace(/\bhighlight\b/, '')
       prev = undefined

   if event.target and event.ctrlKey
       prev = event.target
       prev.className += " highlight"

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

         window.SWD_Page_Recorder.showPos(event, xpath)

         return preventEvent(event)