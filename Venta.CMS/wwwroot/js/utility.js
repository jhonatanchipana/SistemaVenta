function showAlert(title, text, btnCancelText, btnConfirmedtext) {
    
    return Swal.fire({
        title: title ?? '¿Seguro que desea realizar esta acción?',
        text: text ?? "¡No podra revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: btnCancelText ?? 'Aceptar',
        cancelButtonText: btnConfirmedtext ?? 'Cancelar'
    });
}

function alertConfirmation(title, text, btnText, icon) {
    Swal.fire(
        title ?? 'Acción realizado',
        text ?? 'El registro ha sido eliminado.',
        btnText ?? 'success'
    );
}

function alertError(title, text) {
    Swal.fire({
        title: title ?? 'Error',
        text: text ?? 'La acción puede ser realizado.',
        icon: 'error'
    });
}

function rowStatusFormatter(value, row, index) {
    return value ? "Activado" : "Desactivado";
}