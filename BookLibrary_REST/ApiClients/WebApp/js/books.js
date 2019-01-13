var serverUri = "http://localhost:65040/";

$(document).ready(function () {
    getBooks();
    $(document).on("click", '#nav-books-tab', getBooks);
    $(document).on("click", '#btnSaveBook', createBook);
    $(document).on("click", '.delete-book', deleteBook);
    $(document).on("click", '#button-search', findBookByTitle);
});

function getBooks() {

        $.get(serverUri + "api/books").done(function (result) {
            // remove all cards from before
            $("#book-template").parent().children("[id!=book-template]").remove();

            var bookTemplate = $("#book-template");
            //bookTemplate.parent().html("");

            for (var i = 0; i < result.length; i++) {

                var temp = bookTemplate.clone();
                temp.attr("id", "book_row_" + i);
                temp.removeAttr('hidden');

                var model = result[i];
                var str = temp[0].innerHTML;
                str = str.replace("$$ID$$", model.ID);
                str = str.replace("$$Title$$", model.Title);
                str = str.replace("$$Author$$", model.Author);
                str = str.replace("$$Genre$$", model.Genre);
                str = str.replace("$$Description$$", model.Description);
                str = str.replace("$$Quantity$$", model.Quantity);
                str = str.replace("$$DeleteID$$", model.ID);

                temp[0].innerHTML = str;


                temp.appendTo(bookTemplate.parent());
            }
        })
            .fail(function (res) {
                var errorMessage = res.responseText;
                if (res.responseJSON && res.responseJSON.Message) {
                    errorMessage = res.responseJSON.Message;
                }
                alert("Error when getting all books:\n" + errorMessage);
            });

}

function deleteBook() {
    var bookID = $(this).attr("data-book-id");

    $.ajax({
        url: serverUri + "api/books/" + bookID,
        type: 'DELETE',
        data: null,
        success: function (res) {
            getBooks();
            alert("Book is deleted succssully");
        },
        error: function (res) {
            var errorMessage = res.responseText;
            if (res.responseJSON && res.responseJSON.Message) {
                errorMessage = res.responseJSON.Message;
            }
            alert("Could not delete book:\n" + errorMessage);
        }
    });
}

function createBook() {
    var bookName = $("#bookName").val();
    var bookGenre = $("#bookGenre").val();
    var authorName = $("#authorName").val();
    var description = $("#bookDescription").val();
    var book = {
        Title: bookName,
        Genre: bookGenre,
        Author: authorName,
        Description: description
    };
    var x = JSON.stringify(book);

    $.ajax({
        url: serverUri + "api/books",
        contentType: "application/json",
        type: 'POST',
        data: JSON.stringify(book),
        success: function (res) {
            $("#createBookModal .close").click();
            clearCreateBookInputs();
            getBooks();
            alert("Book is created succssully");
        },
        error: function (res) {
            var errorMessage = res.responseText;
            if (res.responseJSON && res.responseJSON.Message) {
                errorMessage = res.responseJSON.Message;
            }
            alert("Could not create book:\n" + errorMessage);
        }
    });
}

function clearCreateBookInputs() {
    $("#bookName").val("");
    $("#bookGenre").val("");
    $("#authorName").val("");
    $("#bookDescription").val("");
}

function findBookByTitle() {
    var titleToSearch = $("#bookSearch").val();

    $.ajax({
        url: serverUri + "api/books/search?title=" + titleToSearch,
        type: 'GET',
        data: null,
        success: function (res) {
            displayBooks(res);
        },
        error: function (res) {
            var errorMessage = res.responseText;
            if (res.responseJSON && res.responseJSON.Message) {
                errorMessage = res.responseJSON.Message;
            }
            alert("An error occured while searching:\n" + errorMessage);
        }
    });
}

function displayBooks(books) {
    
        // remove all cards from before
        $("#book-template").parent().children("[id!=book-template]").remove();

        var bookTemplate = $("#book-template");
        //bookTemplate.parent().html("");

        for (var i = 0; i < books.length; i++) {

            var temp = bookTemplate.clone();
            temp.attr("id", "book_row_" + i);
            temp.removeAttr('hidden');

            var model = books[i];
            var str = temp[0].innerHTML;
            str = str.replace("$$ID$$", model.ID);
            str = str.replace("$$Title$$", model.Title);
            str = str.replace("$$Author$$", model.Author);
            str = str.replace("$$Genre$$", model.Genre);
            str = str.replace("$$Description$$", model.Description);
            str = str.replace("$$Quantity$$", model.Quantity);
            str = str.replace("$$DeleteID$$", model.ID);

            temp[0].innerHTML = str;

            temp.appendTo(bookTemplate.parent());
        }
}