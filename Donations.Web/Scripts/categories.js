$(function () {
    $("#btnNew").on('click', function () {
        $('#categoryName').text("");
        $('.modal').modal('show');
    });
    $("#btnAdd").on('click', function () {
        $.post("/admin/addCategory", { name: $('#categoryName').val() }, function (result) {
            $("#catList").append(`<li>${result.name}</li>`);
            $('.modal').modal('hide');
        })
    });
});