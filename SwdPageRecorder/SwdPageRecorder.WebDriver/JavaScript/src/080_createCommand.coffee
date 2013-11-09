createCommand = (jsonData) ->
    myJSONText = JSON.stringify(jsonData, null, 2)
    window.swdpr_command = myJSONText
