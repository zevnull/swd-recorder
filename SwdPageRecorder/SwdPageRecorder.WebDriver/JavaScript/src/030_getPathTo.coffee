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

            