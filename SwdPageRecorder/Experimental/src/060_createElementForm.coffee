createElementForm = () ->
    # Create an input type dynamically.
    element = document.createElement("div")
    # Assign different attributes to the element. 
    element.id = 'SwdPR_PopUp'
    document.getElementsByTagName('body')[0].appendChild(element)

    closeClickHandler = "document.getElementById('SwdPR_PopUp').style.display = 'none';"
    element.innerHTML = 
    """
    <table id="SWDTable">
        <tr>
          <td>Code identifier</td>
          <td>
                <div id="SwdPR_PopUp_Element_Name">
                    <span id="SwdPR_PopUp_CodeID">
                        <input type="text" id="SwdPR_PopUp_CodeIDText">
                    </span>
                    <span id="SwdPR_PopUp_CodeClose"></span>
                    <span id="SwdPR_PopUp_CloseButton" onclick="#{closeClickHandler}">X</span>
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
    <input type="button" value="Add element" onclick="window.Swd_addElement()">
    """
