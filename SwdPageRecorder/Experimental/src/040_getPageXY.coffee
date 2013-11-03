getPageXY = (element) ->
    x = 0
    y = 0;
    while element
        x += element.offsetLeft
        y += element.offsetTop
        element = element.offsetParent
    return [x, y]
