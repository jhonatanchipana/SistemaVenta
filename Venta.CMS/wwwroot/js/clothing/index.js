$(document).ready(function () {
    let offset = 0;
    let limit = 0;
    Listado();
});

function Listado() {

    $tableMain.bootstrapTable('destroy');

    let columns = [
        {
            title: '#',
            formatter: rowNumFormatter,
            align: 'center',
            width: 6,
            'widthUnit': '%'
        },
        {
            field: 'name',
            title: 'Nombre',
            width: 19,
            'widthUnit': '%'
        },
        {
            field: 'stock',
            title: 'Stock',
            sortable: true,
            width: 15,
            'widthUnit': '%'
        },
        {
            field: 'priceSuggested',
            title: 'Precio Sugerido',
            sortable: true,
            width: 15,
            'widthUnit': '%'
        },       
        {
            field: 'isActive',
            title: 'Estado',
            formatter: rowStatusFormatter,
            width: 15,
            'widthUnit': '%'
        },
        {
            field: 'id',
            title: 'Tallas',
            formatter: sizeFormatter,
            width: 12,
            'widthUnit': '%'
        },
        {
            field: 'id',
            formatter: optionsFormatter,
            title: 'Opciones',
            width: 18,
            'widthUnit': '%'
        }
    ];

    $tableMain.bootstrapTable({
        url: UrlBase,
        method: 'GET',
        pagination: true,
        sidePagination: 'server',
        queryParamsType: 'limit',
        pageSize: 5,
        //pageNumber: 2, //indica en que pagina se inicializara
        pageList: [5, 10, 20],
        //smartDisplay: false, mostrar si o si el combo de las paginas
        dataField: 'results',
        totalField: 'rows',
        columns: columns,
        sortOrder: 'desc',
        sortName: 'CreationDate',
        locale: 'es-MX',
        /*formatRecordsPerPage: function (pageNumber) {
            return pageNumber + 'registros por pagina';
        },*/
        queryParams: function (p) {
            offset = p.offset;
            limit = p.limit;
            return {
                filter: "",
                offset: p.offset,                               
                limit: p.limit,
                sortBy: p.sort,
                orderBy: p.order
            };
        },
        responseHandler: function (res) {
            return res;
        }
    });
}

function rowNumFormatter(value, row, index) {
    return offset + index + 1;
}

function sizeFormatter(value, row, index) {
    return `<a href="#" onclick="showModalClothingSize(${value})">Ver Tallas</a>`;
}

function showModalClothingSize(id) {
    $('#modalClothingSize').modal('show');
    ListadoClothingSize(id);
}

function optionsFormatter(value, row, index) {
    let html = `<div class="dropdown">
                    <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                        <i class="bx bx-dots-vertical-rounded"></i>
                    </button>
                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="${UrlUpdate}/${value}"><i class="bx bx-edit-alt me-1"></i> Editar</a>
                        <a class="dropdown-item" href="#" onclick="ShowAlertChangeStatus(${index},${row.id},${row.isActive});"><i class="bx bx-edit-alt me-1"></i> Cambiar Estado</a>
                        <a class="dropdown-item" href="javascript:showAlertDelete(${value})"><i class="bx bx-trash me-1"></i> Eliminar</a>
                     </div>
                </div>`;
    return html;
}

async function ShowAlertChangeStatus(index, id, status) {
    let alert = showAlert(
        "¿Seguro que desea cambiar el estado?",
        "¡No podra revertir esto!",
        "Cambiar Estado",
        "Cancelar");

    alert.then((result) => {
        if (result.isConfirmed) {
            changeStatusRecord(index, id, status);

        }
    })
}

async function changeStatusRecord(index, id, status) {

    let params = new URLSearchParams({
        id: id,
        isActive: !status
    });
    let response = await fetch(`${UrlChangeStatus}?${params}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (response.ok) {
        alertConfirmation("Estado Cambiado", "El estado a sido cambiado");
        var fila = $(`#table-clothing tr[data-index="${index}"]`);

        let nameStatus = !status ? "Activado" : "Desactivado";
        //fila.find('td:eq(4)').text(nameStatus);
        Listado();
    }

}

async function showAlertDelete(id) {
    let alert = showAlert("¿Seguro que desea eliminar el registro?",
        "¡No podra revertir esto!",
        "Eliminar",
        "Cancelar");

    alert.then((result) => {
        if (result.isConfirmed) {
            deleteRecord(id);
        }
    })
}

async function deleteRecord(id) {
    let response = await fetch(`${UrlDelete}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (response.ok) {
        alertConfirmation("Eliminado", "El registro ha sido eliminado.");
        Listado();
    } else {
        if (response.status == 401) {
            alertError("Error", "La prenda esta en uso, no puede ser eliminado.");
        }
    }
}

function ListadoClothingSize(id) {

    $tableClothingSize.bootstrapTable('destroy');

    let columns = [
        {
            title: '#',
            formatter: rowNumFormatter,
            align: 'center',
            width: 6,
            'widthUnit': '%'
        },
        {
            field: 'sizeName',
            title: 'Talla',
            width: 19,
            'widthUnit': '%'
        },
        {
            field: 'stock',
            title: 'Stock',
            width: 15,
            'widthUnit': '%'
        }
    ];

    $tableClothingSize.bootstrapTable({
        url: UrlClothingSize,
        method: 'GET',
        pagination: true,
        sidePagination: 'client',
        queryParamsType: 'limit',
        pageSize: 5,
        //pageNumber: 2, //indica en que pagina se inicializara
        pageList: [5, 10, 20],
        //smartDisplay: false, mostrar si o si el combo de las paginas
        dataField: 'results',
        totalField: 'rows',
        columns: columns,
        sortOrder: 'desc',
        sortName: 'CreationDate',
        locale: 'es-MX',
        /*formatRecordsPerPage: function (pageNumber) {
            return pageNumber + 'registros por pagina';
        },*/
        queryParams: function (p) {
            offsetSize = p.offset;
            return {
                clothingId: id,
            };
        },
        responseHandler: function (res) {
            return res;
        }
    });
}