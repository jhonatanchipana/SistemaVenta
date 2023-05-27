$(document).ready(function () {
    disabledBtnForm();
});

function disabledBtnForm() {
    $('#formAuthentication').on('submit', function (event) {
        var form = $(this);
        var submitButton = $('#btn-login');

        if (form.valid()) {
            submitButton.prop('disabled', true);
        }
    });
}