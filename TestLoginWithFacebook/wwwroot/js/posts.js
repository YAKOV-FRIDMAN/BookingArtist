// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = (event) => {
   
};

var PlaceHolderHereElement = $('#PlaceHolderHere');

$('button[data-toggle="ajax-model"]').click(function (event) {
    var url = $(this).data('url');
    $.get(url).done(function (data) {
        PlaceHolderHereElement.html(data);
        PlaceHolderHereElement.find('.modal').modal('show');
        postPost();
    })
})

//function preview() {
//    frame.src = URL.createObjectURL(event.target.files[0]);
//}

function preview() {
    var file = event.target.files[0];
    var fileType = file['type'];
    var validImageTypes = ['image/gif', 'image/jpeg', 'image/png'];
    var validVideoTypes = ['video/mp4', 'video/webm', 'video/ogg'];

    if (validImageTypes.includes(fileType)) {
        // אם זו תמונה
        var imageFrame = document.getElementById('imageFrame');
        imageFrame.src = URL.createObjectURL(file);
        imageFrame.style.display = 'block';

        var videoFrame = document.getElementById('videoFrame');
        videoFrame.style.display = 'none'; // הסתר את הווידאו
    } else if (validVideoTypes.includes(fileType)) {
        // אם זה וידאו
        var videoFrame = document.getElementById('videoFrame');
        videoFrame.src = URL.createObjectURL(file);
        videoFrame.style.display = 'block';

        var imageFrame = document.getElementById('imageFrame');
        imageFrame.style.display = 'none'; // הסתר את התמונה
    }
}

function postPost() {
    $("#createPost").click(() => {

        let post = {

            Title: $("#Title").val(),
            Description: $("#Description").val(),
            ImageFile: $("#ImageFile").val(),

        }

        //var formData = new FormData();
        var formData = new FormData();
        formData.append('Title', $("#Title").val());
        formData.append('Description', $("#Description").val());

        // הוספת הקובץ
        var fileInput = $("#ImageFile")[0];
        if (fileInput.files && fileInput.files[0]) {
            formData.append('ImageFile', fileInput.files[0]);
        }

        $.ajax({
            url: '/Artist/CreateNewPostJson',
            type: 'POST',
            data: formData,
            processData: false, // אין עיבוד אוטומטי של הנתונים
            contentType: false, // לא להגדיר סוג תוכן כי אנחנו משתמשים בFormData
            success: function (response) {
                console.log('Success:', response);
                $("#closeMoelPost").click();
                window.location.reload();
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });

        //$.post("/Artist/CreateNewPostJson", post, function (response) {
        //    if (response == "ok") {
        //        alert("ok");
        //    }
        //    else {
        //        alert("error");
        //    }
        //})
    })
}