class SWD_Page_Recorder

    # ComputedProperties
    getMainWinElement : -> document.getElementById "SwdPR_PopUp"


    # TODO: Description 
    displaySwdForm : (x, y) ->

        el = @getMainWinElement();

        el.style.background = "white"
        el.style.position = "absolute"
        el.style.left = x + "px"
        el.style.top = y + "px"
        el.style.display = "block"
        el.style.border = "3px solid black"
        el.style.padding = "5px 5px 5px 5px"
        el.style.zIndex = 2147483647
        console.log "Should be displayed";

    # TODO: Description 
    showPos : (event, xpath) ->
        
        if window.event
            x = window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft
            y = window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop
        else 
            x = event.clientX + window.scrollX
            y = event.clientY + window.scrollY

        x -= 2 
        y -= 2
        y = y+15

        @displaySwdForm(x, y)
       
        document.getElementById("SwdPR_PopUp_XPathLocator").innerHTML = xpath
        document.getElementById("SwdPR_PopUp_ElementText").innerHTML = pseudoGuid()
        document.getElementById("SwdPR_PopUp_CodeIDText").value = ''
        console.log(x + ";" + y)
    
    # TODO: Description    
    closeForm : () ->
        document.getElementById('SwdPR_PopUp').style.display = 'none'
        

    # TODO: Description 
    createElementForm : () ->
        console.log "createElementForm <";
        # Create an input type dynamically.
        element = document.createElement("div")
        # Assign different attributes to the element. 
        element.id = 'SwdPR_PopUp'
        document.getElementsByTagName('body')[0].appendChild(element)

        closeClickHandler = ""
        element.innerHTML = 
        '
        <table id="SWDTable" onmousemove="window.SWD_Page_Recorder.doDragging(event) onmouseup="window.SWD_Page_Recorder.stopDragging()" onmousedown="window.SWD_Page_Recorder.startDragging(event,this)">
            <tr>
              <td>Code identifier</td>
              <td>
                    <div id="SwdPR_PopUp_Element_Name">
                        <span id="SwdPR_PopUp_CodeID">
                            <input type="text" id="SwdPR_PopUp_CodeIDText">
                        </span>
                        <span id="SwdPR_PopUp_CodeClose"></span>
                        <span id="SwdPR_PopUp_CloseButton" onclick="window.SWD_Page_Recorder.closeForm()">X</span>
                     </div>
              </td>
            </tr>
            <tr>
              <td>Element</td>
              <td><span id="SwdPR_PopUp_ElementName">Element</span></td>
            </tr>
            <tr>
              <td>Text:</td>
              <td><span id="SwdPR_PopUp_ElementText">Element</span></td>
            </tr>
            <tr>
              <td>XPathLocator</td>
              <td><span id="SwdPR_PopUp_XPathLocator">Element</span></td>
            </tr>
            </table>
        <input type="button" value="Add element" onclick="window.SWD_Page_Recorder.addElement()">
        '

    # TODO: Description  !!!  GLOBAL
    addElement : () -> 
        codeIDTextElement   = document.getElementById "SwdPR_PopUp_CodeIDText"
        XPathLocatorElement = document.getElementById "SwdPR_PopUp_XPathLocator"
        
        JsonData = 
                   "Command"   : "AddElement"
                   "Caller"    : "addElement"
                   "CommandId" : pseudoGuid()

                   "ElementCodeName" : codeIDTextElement.value
                   "ElementXPath"    : XPathLocatorElement.firstChild.nodeValue

        createCommand(JsonData)

#======== Dragging ======================

    @dragging = false
    @dragTop = 0
    @dragLeft = 0

    doDragging : (e) ->
        return unless @Draggable
        return unless @dragging

        parent_win = @getMainWinElement();

        top = parseInt(parent_win.style.top.replace("px", ""))
        left = parseInt(parent_win.style.left.replace("px", ""))

        currentX = 0
        currentY = 0
        
        if e.pageX?
           currentX = e.pageX 
           currentY = e.pageY 
        
        else if e.x?
           currentX = e.x; 
           currentY = e.y; 


        top  = if currentY > @dragTop  then top  + currentY - @dragTop  else top - @dragTop - currentY
        left = if currentX > @dragLeft then left + currentX - @dragLeft else left - @dragLeft - currentX

        @dragTop = currentY;
        @dragLeft = currentX;

        try
            parent_win.style.top  = top + "px";
            parent_win.style.left = left + "px";
        catch error
            console.log "SWD Error: " + error


    startDragging : (e) ->
        return unless @Draggable
        @dragging = true
        @dragTop = e.pageY
        @dragLeft = e.pageX
        @getMainWinElement().style.cursor = "move"

    stopDragging : () ->
        return unless @Draggable
        @dragging = false
        @getMainWinElement().style.cursor = "default"

