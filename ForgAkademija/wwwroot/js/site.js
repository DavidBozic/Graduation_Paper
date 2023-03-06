/* ------------------------
    Data tables 
------------------------ */

$(document).ready(function () {
    $('#courseTable').DataTable();
});

$(document).ready(function () {
    $('#usersTable').DataTable();
});

/* ------------------------
    Modals
------------------------ */

ShowInModal = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal").find(".modal-body").html(res);
            $("#form-modal").find(".modal-title").html(title);
            $("#form-modal").modal("show");
        }
    })
}

VideoModal = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#video-modal").find(".modal-body").html(res);
            $("#video-modal").find(".modal-title").html(title);
            $("#video-modal").modal("show");
        }
    })
}