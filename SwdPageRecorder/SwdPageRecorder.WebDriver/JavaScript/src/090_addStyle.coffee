addStyle = (str) ->
    el = document.createElement('style')
    if el.styleSheet 
        el.styleSheet.cssText = str
    else 
        el.appendChild(document.createTextNode(str))

    return document.getElementsByTagName('head')[0].appendChild(el)
