showPos = (event, xpath) ->

    el = document.getElementById('SwdPR_PopUp');
    
    if window.event
        x = window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft
        y = window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop
    else 
        x = event.clientX + window.scrollX
        y = event.clientY + window.scrollY

    x -= 2 
    y -= 2
    y = y+15

    el.style.background = "white"
    el.style.position = "absolute"
    el.style.left = x + "px"
    el.style.top = y + "px"
    el.style.display = "block"
    el.style.border = "3px solid black"
    el.style.padding = "5px 5px 5px 5px"
    el.style.zIndex = 2147483647
    
    document.getElementById("SwdPR_PopUp_XPathLocator").innerHTML = xpath
    document.getElementById("SwdPR_PopUp_ElementText").innerHTML = pseudoGuid()
    document.getElementById("SwdPR_PopUp_CodeIDText").value = ''
    console.log(x + ";" + y)
