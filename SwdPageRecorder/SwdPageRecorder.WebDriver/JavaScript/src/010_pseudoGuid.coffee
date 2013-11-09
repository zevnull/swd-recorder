pseudoGuid = () ->
    result = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'
    result = result.replace /[xy]/g, (re_match) ->
                        random_value = Math.random() * 16 | 0
                        replacement = if re_match is 'x' then  random_value else random_value & 0x3 | 0x8
                        return replacement.toString(16)

    return result
