getInputElementsByTypeAndValue = (inputType, inputValue) ->
    allDocumentInputElements = document.getElementsByTagName('input')

    result = new Array();

    for inputElement in allDocumentInputElements
        if inputElement.type is inputType and inputElement.value is inputValue
            result.push inputElement

    return result;
