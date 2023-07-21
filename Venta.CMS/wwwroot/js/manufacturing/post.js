$(document).ready(function () {

    $(".autocomplete-input").autocomplete({
        source: sourceClothing,
        select: selectClothing
    });

    disabledBtnForm();
    eventCloseModal();
    customValidation();

    $(".ui-autocomplete").css('z-index', '1090');

    $("#form").validate();
    $("#form").data("validator").settings.ignore = "";

    $("#btn-post").click(Register);


});

let currentRequestPost = null;

function disabledBtnForm() {
    $('#form').on('submit', function (event) {
        var form = $(this);
        var submitButton = $('#btn-post');
        if (form.valid()) {
            submitButton.prop('disabled', true);
        }
    });
}

async function sourceClothing(request, response) {
    let data = await getDataAutocomplete(request.term, 10);

    response(data.map(function (item) {
        return {
            value: item.description,
            id: item.id,
        };
    }));
}

async function getDataAutocomplete(filter, limit) {

    if (currentRequestPost) {
        currentRequestPost.abort();
    }

    let params = new URLSearchParams({
        filter: filter,
        limit: limit
    });

    let urlConsulta = `${urlAutocompleteClothing}?${params}`;

    currentRequestPost = $.ajax({
        url: urlConsulta,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            return data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error('Error al consultar los datos:', textStatus, errorThrown);
        }
    });

    return await currentRequestPost;
}

function selectClothing(event, ui) {
    let id = ui.item.id;
    $("#clothingId").val(id);
    listClothingSize(id);
    addInputQuantityValidation();
}

function listClothingSize(id) {
    $tableClothingSize.bootstrapTable('destroy');

    let columns = [
        {
            title: '#',
            formatter: rowNumFormatter,
            align: 'center',
            width: 20,
            widthUnit: '%'
        },
        {
            field: 'sizeName',
            title: 'Talla',
            width: 40,
            widthUnit: '%'
        },
        {
            field: '',
            title: 'Cantidad',
            formatter: inputQuantityFormatter,
            width: 40,
            widthUnit: '%'
        },
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
            return {
                clothingId: id,
            };
        },
        responseHandler: function (res) {
            return res;
        }
    });
}

function inputQuantityFormatter(value, row, index) {
    return `<input type="number" class="form-control valor-quantity" value="0" name="Quantity_${index}" id="Quantity_${index}"
            data-rule-min="0" data-msg-min="Debe ingresar un valor mayor a 0."/>
            <span data-valmsg-for="Quantity_${index}" data-valmsg-replace="true" class="text-center"></span>`;
}

function customValidation() {
    $.validator.addMethod("quantityGreaterThan0", function (value, element) {
        let arrayQuantity = Array.from($(".valor-quantity").map((index, element2) => element2.value));

        let exists = arrayQuantity.some(x => x > 0);

        return exists;
    }, "Debe ingresar una cantidad mayor a 0 en al menos una talla.");
}

function addInputQuantityValidation() {
    let inputMajor = $("#validationCustom");
    let inputQuantities = $('<input>').attr({
        type: 'hidden',
        value: '0',
        name: 'quantities',
        'data-rule-quantityGreaterThan0': 'true',
        'data-msg-quantityGreaterThan0': 'Debe ingresar una cantidad mayor a 0 en al menos una talla.'
    });
    let spanQuantitiesValidate = $('<span>').attr({
        'data-valmsg-for': `quantities`,
        'data-valmsg-replace': 'true',
        'class': 'text-danger'
    });

    inputMajor.append(inputQuantities);
    inputMajor.append(spanQuantitiesValidate);
}

function Register() {

    let data = getDataClothing();
    if (!$("#form").valid()) {
        return;
    }

    $.ajax({
        url: URLCREATE,
        type: 'POST',
        data: data,
        dataType: 'json',
        contentType: "application/x-www-form-urlencoded",
        success: function (response) {
            if (response.success) {
                toastr.success("Registro exitoso");
                $("#modalManufacturingPost").modal("hide");
                resetForm();
                Listado();
            } else {
                var errors = response.errors;
                for (var i = 0; i < errors.length; i++) {
                    
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error('Error al consultar los datos:', textStatus, errorThrown);
        }
    });
}

function getDataClothing() {
    let dataTable = $tableClothingSize.bootstrapTable('getData');

    dataTable.forEach(function (fila, index) {
        console.log(index);
        fila.quantity = $("#Quantity_" + index).val();
    });

    let manufacturingClothingSize = dataTable
        .filter(x => x.quantity > 0)
        .map((value) => {
            return {
                id: 0,
                clothingId: value.clothingId,
                clothingSizeId: value.id,
                quantity: value.quantity
            };
        });

    let manufacturing = {
        id: 0,
        postManufacturingClothings: manufacturingClothingSize
    };

    return manufacturing;
}

function resetForm() {

    $tableClothingSize.bootstrapTable('destroy');
    $("#form").validate().resetForm();
    $("#validationCustom").empty();

    //limpiar inptus
    $("#clothingId").val("");
    $("#clothingAutocomplete").val("");
}

function eventCloseModal() {
    $('#modalManufacturingPost').on('hidden.bs.modal', function (e) {       
        resetForm();
    });
}

