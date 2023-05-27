async function mostrarAlert(id){
    Swal.fire({
        title: '¿Seguro que desea eliminar el registro?',
        text: "¡No podra revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            let ok = deleteMaterial(id);

            if (ok) {
                alertConfirmationDelete();
                Listado();
            }            
        }
    })
}

async function deleteMaterial(id){
    let response = await fetch(`${UrlDelete}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (response.ok) {
        return true;
    }

    return false;
}

function alertConfirmationDelete() {
    Swal.fire(
        'Eliminado',
        'El registro ha sido eliminado.',
        'Ok'
    );
}