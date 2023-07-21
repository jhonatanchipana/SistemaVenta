$(document).ready(function () {
    disabledBtnForm();
});

function disabledBtnForm() {
    $('#form').on('submit', function (event) {
        var form = $(this);
        var submitButton = $('#btn-post');

        if (form.valid()) {
            submitButton.prop('disabled', true);
        }
    });
}