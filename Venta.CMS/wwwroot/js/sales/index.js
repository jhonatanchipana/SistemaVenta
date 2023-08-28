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
            field: 'saleDate',
            title: 'Fecha Venta',
            formatter: dateFormatter,
            width: 14,
            widthUnit: '%'
        },
        {
            field: 'quantityTotal',
            title: 'Cantidad Total',
            sortable: true,
            width: 14,
            widthUnit: '%'
        },
        {
            field: 'priceTotal',
            title: 'Precio Total',
            formatter: rowMoneyFormatter,
            sortable: true,
            width: 14,
            widthUnit: '%'
        },
        {
            field: 'investment',
            title: 'Inversion Total',
            formatter: rowMoneyFormatter,
            sortable: true,
            width: 14,
            widthUnit: '%'
        },
        {
            formatter: revenueFormatter,
            title: 'Ganancia',
            width: 14,
            widthUnit: '%'
        },
        {
            field: 'id',
            formatter: seeDetail,
            title: 'Detalle',
            width: 12,
            'widthUnit': '%'
        },
        {
            field: 'id',
            formatter: optionsFormatter,
            title: 'Opciones',
            width: 12,
            widthUnit: '%'
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

function dateFormatter(value, row, index) {
    let date = moment(value).format("DD/MM/YYYY hh:mm:ss");
    return date;
}

function revenueFormatter(value, row, index) {
    let revenue = row.priceTotal - row.investment;
    return `S/. ${revenue}`;
}

function seeDetail(value, row, index) {
    return `<a href='javascript:showModalDetail(${value})'>Ver detalle</a>`;
}

function optionsFormatter(value, row, index) {
    let html = `<div class="dropdown">
                    <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                        <i class="bx bx-dots-vertical-rounded"></i>
                    </button>
                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="javascript:showAlertDelete(${value})"><i class="bx bx-trash me-1"></i> Eliminar</a>
                     </div>
                </div>`;
    return html;
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
    }
}

function showModalDetail(id) {
    getDataDetail(id);
}

async function getDataDetail(id) {

    let response = await fetch(`${URLDETAIL}/${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (response.ok) {
        let data = await response.json();
        showData(data);
        $("#modalSalesDetail").modal("show");
    }
}

function showData(data) {
    let $tableDetail = $('#tableSalesDetail');

    $tableDetail.bootstrapTable('destroy');

    let columns = [
        {
            title: '#',
            formatter: rowNumFormatter,
            align: 'center',
            width: 10,
            'widthUnit': '%'
        },
        {
            field: 'clothingName',
            title: 'Prenda',
            width: 30,
            'widthUnit': '%'
        },
        {
            field: 'sizeName',
            title: 'Talla',
            width: 20,
            'widthUnit': '%'
        },
        {
            field: 'quantity',
            title: 'Cantidad Vendido',
            width: 20,
            'widthUnit': '%'
        },
        {
            field: 'priceUnit',
            title: 'Precio/Unidad',
            formatter: rowMoneyFormatter,
            width: 20,
            'widthUnit': '%'
        }
    ];

    $tableDetail.bootstrapTable({
        data: data.salesClothings,
        columns: columns,
        pagination: true
    });

}