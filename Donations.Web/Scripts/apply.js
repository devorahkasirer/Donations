$(function () {
    $("form").on('keyup', function () {
        if ($("#category").val() === 0 || $("#amount").val() === "") {
            $(".btn").prop('disabled', true);
        } else {
            $(".btn").prop('disabled', false);
        }
    });
});