$(document).ready(function () {

    $(".autocomplete-input").autocomplete({
        source: sourceMaterial,
        select: selectMaterial
    });

    $(".deleteMaterial").on('click', deleteMaterial);

    addRowMaterial();
    disabledBtnForm();
    validacionCountMaterial();
    multiselectSize();

    $("#form").data("validator").settings.ignore = "";
    
});

let currentRequest = null;

function addRowMaterial() {
    addInputButton.click(function () {

        let newRow = $('<tr>');

        let cell1 = $('<td>');
        let cell2 = $('<td>');
        let cell3 = $('<td>');
        let cell5 = $('<td>').attr({class: 'text-center'});

        let inputId = $('<input>').attr({
            type: 'hidden',
            name: `PostClothingMaterial[${count}].Id`,
            value: '0'
        });

        let inputMaterialId = $('<input>').attr({
            type: 'hidden',
            name: `PostClothingMaterial[${count}].MaterialId`,
            value: '',
            'data-rule-required': 'true',
            'data-msg-required': 'El campo Material es requerido.'
        });

        let inputMaterialAutocomplete = $('<input>').attr({
            type: 'text',
            name: `Material_${count}`,
            value: '',
            class: 'form-control autcomplet-input',
        });

        let inputMaterial = $('<input>').attr({
            type: 'hidden',
            name: `PostClothingMaterial[${count}].MaterialName`,
            value: '',
            class: 'form-control',
        });

        inputMaterialAutocomplete.autocomplete({
            source: sourceMaterial,
            select: selectMaterial
        });

        let inputMaterialCost = $('<input>').attr({
            type: 'hidden',
            name: `PostClothingMaterial[${count}].Cost`,
            value: '',
            class: 'form-control',
        });

        let inputMaterialUnitQuantity = $('<input>').attr({
            type: 'hidden',
            name: `PostClothingMaterial[${count}].UnitQuantity`,
            value: '',
            class: 'form-control',
        });

        let inputMaterialUnitMeasurement = $('<input>').attr({
            type: 'hidden',
            name: `PostClothingMaterial[${count}].UnitMeasurement`,
            value: '',
            class: 'form-control',
        });

        let divQuantity = $('<div>').attr({
            class : 'd-flex'
        });

        let inputQuantity = $('<input>')
            .attr({
                type: 'text',
                name: `PostClothingMaterial[${count}].Quantity`,            
                value: '',
                class: 'form-control',
                'data-rule-required': 'true',
                'data-msg-required': 'El campo Cantidad es obligatorio.',
                'data-rule-number': 'true',
                'data-msg-number': 'Debe ingresar solo números.'
            })
            .css({
                'width': '80%'
            });

        let labelQuantity = $('<label>').attr({
            id: `labelQuantity_${count}`,         
            class: 'm-auto p-2'
        });

        divQuantity.append(inputQuantity);
        divQuantity.append(labelQuantity);

        let inputDelete = $('<a>').attr({ 'data-materialid': -1, id: `deleteIndex_${count}` })
            .css('cursor', 'pointer')
            .text('X')
            .on('click', deleteMaterial);

        let validationMaterialId = $('<span>').attr({
            'data-valmsg-for': `PostClothingMaterial[${count}].MaterialId`,
            'data-valmsg-replace': 'true',
            'class': 'text-danger'
        });

        let validationQuantity = $('<span>').attr({
            'data-valmsg-for': `PostClothingMaterial[${count}].Quantity`,
            'data-valmsg-replace': 'true',
            'class': 'text-danger'
        });
           
        cell1.append(count + 1);
        cell2.append(inputId);
        cell2.append(inputMaterialId);
        cell2.append(inputMaterialAutocomplete);
        cell2.append(inputMaterial);
        cell2.append(inputMaterialCost);
        cell2.append(inputMaterialUnitQuantity);
        cell2.append(inputMaterialUnitMeasurement);
        cell2.append(validationMaterialId);
        cell3.append(divQuantity);
        cell3.append(validationQuantity);
        cell5.append(inputDelete);

        newRow.append(cell1);
        newRow.append(cell2);
        newRow.append(cell3);
        newRow.append(cell5);

        tableBody.append(newRow);

        count++;

        $(`input[name='PostClothingMaterialCount']`).valid();
    });

}

function deleteMaterial() {
    $(this).closest('tr').remove();
    let materialId = $(this).data('materialid');
    let index = idAutocompleteSelect.indexOf(materialId);
    if (index != -1) {
        idAutocompleteSelect.splice(index, 1);
    }
    count--;
    refreshNumeration();
}

function refreshNumeration() {
    tableBody.children('tr').each(function (index, element) {

        let firstColumn = $(this).find('td').eq(0);
        let column = $(this).find('td:nth-child(2),td:nth-child(3),td:nth-child(4),td:nth-child(5)');       

        column.children('input,span,div,a').each(function (indexColumn, elementColumn) {            
            if ($(elementColumn).is(':input') && $(elementColumn).attr('name').includes('PostClothingMaterial')) {
                let name = $(elementColumn).attr('name').toString().replace(/\d/g, index);
                $(elementColumn).attr('name', name);
            }

            if ($(elementColumn).is('div')) {
                $(elementColumn).children('input,label').each(function (indexColumn, divElementColumn) {                    
                    if ($(divElementColumn).is(':input')) {
                        let name = $(divElementColumn).attr('name').toString().replace(/\d/g, index);
                        $(divElementColumn).attr('name', name);
                    } else {
                        let idLabel = $(divElementColumn).attr("id").toString().replace(/\d/g, index);
                        $(divElementColumn).attr('id', idLabel);
                    }                   
                });
            }

            if ($(elementColumn).is('a') && $(elementColumn).attr('id').includes('deleteIndex_')) {
                let name = $(elementColumn).attr('id').toString().replace(/\d/g, index);
                $(elementColumn).attr('id', name);
            }

            if ($(elementColumn).is('span') && $(elementColumn).attr('data-valmsg-for').includes('PostClothingMaterial')) {
                let name = $(elementColumn).attr('data-valmsg-for').toString().replace(/\d/g, index);
                $(elementColumn).attr('data-valmsg-for', name);
            }

        });

        firstColumn.text(index + 1);
    });

}

function disabledBtnForm() {
    $('#form').on('submit', function (event) {
        var form = $(this);
        var submitButton = $('#btn-post');

        if (form.valid()) {
            submitButton.prop('disabled', true);
        }
    });
}

function validacionCountMaterial() {
    $.validator.addMethod("countMaterial", function (value, element) {
        //count variable global
        return count > 0;
    }, "Debe agregar al menos un material");
}

async function sourceMaterial(request, response) {
    let data = await getDataAutocomplete(request.term, 10);
    
    response(data.map(function (item) {
        return {
            value: item.name,
            id: item.id,
            unitMeasurementId: item.unitMeasurementId,
            unitMeasurementDescription: item.unitMeasurementDescription,
            cost: item.cost,
            unitQuantity: item.unitQuantity
        };
    }));
}

async function getDataAutocomplete(filter, limit) {

    if (currentRequest) {
        currentRequest.abort();
    }

    let params = new URLSearchParams({
        filter: filter,
        limit: limit
    });

    //let urlConsulta = `${urlAutocompleteMaterial}?${params}`;
    let urlConsulta = urlAutocompleteMaterial + "?filter=" + filter + "&limit=" + limit + "&ignoreId=" + idAutocompleteSelect.join("&ignoreId=");

    currentRequest = $.ajax({
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

    return await currentRequest;
}

function selectMaterial(event, ui) {
    let input = $(this);
    let id = input.attr('name').replace('Material_', '');
    let inputMaterialId = `input[name="PostClothingMaterial[${id}].MaterialId"]`;
    let inputMaterialName = `input[name="PostClothingMaterial[${id}].MaterialName"]`;
    let inputMaterialCost = `input[name="PostClothingMaterial[${id}].Cost"]`;
    let inputMaterialUnitQuantity = `input[name="PostClothingMaterial[${id}].UnitQuantity"]`;
    let inputMaterialUnitMeasurement = `input[name="PostClothingMaterial[${id}].UnitMeasurement"]`;
    let labelQuantity = `#labelQuantity_${id}`;
    let deleteIndex = `#deleteIndex_${id}`;

    $(inputMaterialId).val(ui.item.id);
    $(inputMaterialName).val(ui.item.value);
    $(inputMaterialCost).val(ui.item.cost);
    $(inputMaterialUnitQuantity).val(ui.item.unitQuantity);
    $(inputMaterialUnitMeasurement).val(ui.item.unitMeasurementId);
    $(labelQuantity).text(ui.item.unitMeasurementDescription);    
    $(deleteIndex).attr('data-materialid', ui.item.id);

    input.prop('disabled', true);    
    $(`input[name='PostClothingMaterial[${id}].MaterialId']`).valid();

    idAutocompleteSelect.push(ui.item.id);
}

function multiselectSize() {
    VirtualSelect.init({
        ele: '#SizeShow',
        selectAllText: 'Seleccionar todo'
    });


    document.querySelector('#SizeShow').addEventListener('change', function () {
        if (sizeInitial.length == 0) {
            $("#SizeIds").val(this.value);
        } else {
            sizeInitial = [];
        }
    });

    if (sizeInitial.length != 0) {
        document.querySelector('#SizeShow').setValue(sizeInitial); 
    }

}


/*
 *

// Variable para almacenar la referencia a la solicitud actual
var currentRequest;

// Función para realizar la consulta
function fetchData() {
  // Abortar la solicitud en curso si existe
  if (currentRequest) {
    $.fn.bootstrapTable.utils.abortRequest(currentRequest);
  }

  // Realizar la nueva consulta
  currentRequest = $('#tabla').bootstrapTable('refresh', {
    url: 'https://ejemplo.com/api/datos',
    silent: true
  });
}

// Configurar la tabla con la propiedad url
$('#tabla').bootstrapTable({
  url: 'https://ejemplo.com/api/datos'
});

// Ejecutar la función fetchData para la primera consulta
fetchData();

// Después de un breve retraso, realizar una segunda consulta
setTimeout(function() {
  fetchData(); // Realizar una segunda consulta
}, 1000); // Esperar 1 segundo antes de la segunda consulta 


 * */