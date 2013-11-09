handler = (event) ->
    return if event.target is document.body or (prev and prev is event.target)

    if prev
        prev.className = prev.className.replace(/\bhighlight\b/, '')
        prev = undefined

    if event.target and event.ctrlKey
        prev = event.target
        prev.className += " highlight"
