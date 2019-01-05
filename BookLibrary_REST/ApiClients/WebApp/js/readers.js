
var serverUri = "http://localhost:65040/";

// https://chrome.google.com/webstore/detail/allow-control-allow-origi/nlfbmbojpeacfghkpbjhddihlkkiljbi?hl=en-US

$(document).ready(function () {

    $(document).on("click", '#nav-readers-tab', getReaders);
    $(document).on("click", '.delete-reader', deleteReader);
    
});


function getReaders() {
    
    $.get(serverUri + "api/readers").done(function (result) {
        // remove all cards from before
        $("#reader-template").parent().children("[id!=reader-template]").remove();

        var readerTemplate = $("#reader-template");
        //readerTemplate.parent().html("");

        for (var i = 0; i < result.length; i++) {

            var temp = readerTemplate.clone();
            temp.attr("id", "reader_row_" + i);
            temp.removeAttr('hidden');

            var model = result[i];
            var str = temp[0].innerHTML;
            str = str.replace("$$ID$$", model.ID);
            str = str.replace("$$FirstName$$", model.FirstName);
            str = str.replace("$$LastName$$", model.LastName);
            str = str.replace("$$PhoneNumber$$", model.PhoneNumber);
            str = str.replace("$$DeleteID$$", model.ID);

            temp[0].innerHTML = str;


            temp.appendTo(readerTemplate.parent());
        }
    })
    .fail(function (res) {
        var errorMessage = res.responseText;
        if (res.responseJSON && res.responseJSON.Message) {
            errorMessage = res.responseJSON.Message;
        }
        alert("Error when getting all readers:\n" + errorMessage);
    })
    ;

}

function deleteReader() {
    var readerID = $(this).attr("data-reader-id");
    
    $.ajax({
        url: serverUri + "api/readers/" + readerID,
        type: 'DELETE',
        data: null,
        success: function (res) {
            alert("Reader is deleted succssully");
            getReaders();
        },
        error: function (res) {
            var errorMessage = res.responseText;
            if (res.responseJSON  && res.responseJSON.Message) {
                errorMessage = res.responseJSON.Message;
            }
            alert("Could not delete reader:\n" + errorMessage);
        }
    });
}