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
            formatter: rowNumFormatter
        },
        {
            field: 'name',
            title: 'Nombre'
        },
        {
            field: 'cost',
            title: 'Costo',
            sortable: true
        },
        {
            field: 'stock',
            title: 'Stock',
            sortable: true
        },
        {
            field: 'id',
            formatter: optionsFormatter,
            title: 'Opciones'
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
        sortName: 'Id',
        locale: 'es-MX',
        /*formatRecordsPerPage: function (pageNumber) {
            return pageNumber + 'registros por pagina';
        },*/
        queryParams: function (p) {
            offset = p.offset;
            limit = p.limit;
            return {
                filter: "",
                isActive: -1,
                unitMeasurement: 0,
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

function optionsFormatter(value, row, index) {        
    let html = `<div class="dropdown">
                    <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                        <i class="bx bx-dots-vertical-rounded"></i>
                    </button>
                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="${UrlUpdate}/${value}"><i class="bx bx-edit-alt me-1"></i> Editar</a>
                        <a class="dropdown-item" href="javascript:mostrarAlert(${value})"><i class="bx bx-trash me-1"></i> Eliminar</a>
                     </div>
                </div>`;
    return html;
}