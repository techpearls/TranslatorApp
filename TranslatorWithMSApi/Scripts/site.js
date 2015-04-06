$(function () {
    /// <summary>
    /// Handles all application errors
    /// </summary>
    $.ajaxSetup({
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#dialog').modal('show');
        }
    });

    /// <summary>
    /// Initializes the dropdown
    /// </summary>
    $('.selectpicker').selectpicker({
        size: false
    });
    
    /// <summary>
    /// Handles translate click action
    /// </summary>
    $('#btnTranslate').click(function () {
        var source = $('#sourceLang').val();
        var target = $('#targetLang').val();
        var textToTranslate = $('#txtToTranslate').val();
        if (textToTranslate.length > 0) {
            translate(textToTranslate, source, target);

            return false;
        }
        return true;
    });


    /// <summary>
    /// Clears the text boxes.
    /// </summary>
    $('#btnClear').click(function () {
        $('#txtToTranslate').val('');
        $('#translatedText').val('');
        return false;
    });

    /// <summary>
    /// Makes the ajax call and displays translated text
    /// </summary>
    function translate(text, source, target) {
        $.ajax({
            type: "GET",
            url: "Translate/DoTranslate",
            datatype: "json",
            data: { text : text, source : source, target: target }
        }).done(function (data) {
            $('#translatedText').val(data.replace(/['"]+/g, ''));
        });
        
    }
});