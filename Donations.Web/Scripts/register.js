$(function () {
    $("#password2").on('keyup', function () {
        if ($(this).val() !== $("#password1").val()) {
            $(".btn").prop('disabled', true);
        } else {
            $(".btn").prop('disabled', false);
        }
    });
});