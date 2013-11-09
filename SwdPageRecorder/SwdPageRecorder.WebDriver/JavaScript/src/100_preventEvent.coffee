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
