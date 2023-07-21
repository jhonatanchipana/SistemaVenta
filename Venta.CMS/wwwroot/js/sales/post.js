$(document).ready(function () {

    $(".autocomplete-input").autocomplete({
        source: sourceClothing,
        select: selectClothing
    });

    $(".deleteClothing").on('click', deleteClothing);

    addRowClothing();
    disabledBtnForm();
    validacionCountClothing();

    $("#form").data("validator").settings.ignore = "";
    
});

let currentRequest = null;

function addRowClothing() {
    addInputButton.click(function () {       

        let newRow = $('<tr>');

        let cell1 = $('<td>');
        let cell2 = $('<td>');
        let cell3 = $('<td>');
        let cell4 = $('<td>');
        let cell5 = $('<td>');
        let cell6 = $('<td>');
        let cell7 = $('<td>').attr({ class: 'text-center' });

        let inputId = $('<input>').attr({
            type: 'hidden',
            name: `PostSalesClothesSize[${count}].Id`,
            value: '0'
        });

        let inputClothingId = $('<input>').attr({
            type: 'hidden',
            name: `PostSalesClothesSize[${count}].ClothingId`,
            value: '',
            'data-rule-required': 'true',
            'data-msg-required': 'El campo  es requerido.'
        });

        let inputClothingAutocomplete = $('<input>').attr({
            type: 'text',
            name: `Clothing_${count}`,
            value: '',
            class: 'form-control autcomplet-input',
        }).autocomplete({
            source: sourceClothing,
            select: selectClothing
        });

        let inputClothingName = $('<input>').attr({
            type: 'hidden',
            name: `PostSalesClothesSize[${count}].ClothingName`,
            value: '',
            class: 'form-control',
        });

        let inputClothingSizeId = $('<input>').attr({
            type: 'hidden',
            name: `PostSalesClothesSize[${count}].ClothingSizeId`,
            value: '',
            class: 'form-control',
        });

        let inputClothingSizeStock = $('<input>').attr({
            type: 'text',
            value: '0',
            disabled: true,            
            name: `PostSalesClothesSize[${count}].ClothingSizeStock`,
            class: 'form-control',
        });

        //let selectClothingSize = $('<select>').attr({
        //    name: `PostSalesClothesSize[${count}].ClothingSizeId`,
        //    class: 'form-control',
        //    'data-rule-required': 'true',
        //    'data-msg-required': 'Seleccione una talla.',
        //});

        //let optionClothingSize = $('<option>').text('Seleccione una talla').val('');

        //selectClothingSize.append(optionClothingSize);

        let inputClothingInvestmentUnit = $('<input>').attr({
            type: 'hidden',
            name: `PostSalesClothesSize[${count}].InvestmentUnit`,
            value: '',
            class: 'form-control',
        });

        let inputClothingQuantity = $('<input>')
            .attr({
                type: 'text',
                name: `PostSalesClothesSize[${count}].Quantity`,
                value: '',
                class: 'form-control',
                'data-rule-required': 'true',
                'data-msg-required': 'El campo Cantidad es obligatorio.',
                'data-rule-number': 'true',
                'data-msg-number': 'Debe ingresar solo números.',
                'data-rule-min': '1',
                'data-msg-min': 'El campo Cantidad debe ser mayor a 0.',
                'data-rule-quantityLessThanStock': count,
                'data-msg-quantityLessThanStock': 'El campo Cantidad debe ser menor o igual al stock.'
                
            });

        let inputClothingPriceUnit = $('<input>')
            .attr({
                type: 'text',
                name: `PostSalesClothesSize[${count}].PriceUnit`,
                value: '',
                class: 'form-control',
                'data-rule-required': 'true',
                'data-msg-required': 'El campo Precio/Unidad es obligatorio.',
            });

        let inputClothingInvestmentUnitLabel = $('<input>')
            .attr({
                type: 'text',
                name: `InvestmentUnitLabel_${count}`,
                value: '',
                class: 'form-control',
            })
            .prop('disabled', true);

        let inputDelete = $('<a>')
            .css('cursor', 'pointer')
            .text('X')
            .on('click', deleteClothing);

        let validationClothingId = $('<span>').attr({
            'data-valmsg-for': `PostSalesClothesSize[${count}].ClothingId`,
            'data-valmsg-replace': 'true',
            'class': 'text-danger'
        });

        //let validationClothingSizeId = $('<span>').attr({
        //    'data-valmsg-for': `PostSalesClothesSize[${count}].ClothingSizeId`,
        //    'data-valmsg-replace': 'true',
        //    'class': 'text-danger'
        //});

        let validationClothingQuantity = $('<span>').attr({
            'data-valmsg-for': `PostSalesClothesSize[${count}].Quantity`,
            'data-valmsg-replace': 'true',
            'class': 'text-danger'
        });

        let validationClothingPriceUnit = $('<span>').attr({
            'data-valmsg-for': `PostSalesClothesSize[${count}].PriceUnit`,
            'data-valmsg-replace': 'true',
            'class': 'text-danger'
        });

        cell1.append(count + 1);
        cell2.append(inputId);
        cell2.append(inputClothingId);
        cell2.append(inputClothingAutocomplete);
        cell2.append(inputClothingName);
        cell2.append(validationClothingId);
        cell2.append(inputClothingInvestmentUnit);
        cell2.append(inputClothingSizeId);
        cell3.append(inputClothingSizeStock);
        cell4.append(inputClothingQuantity);
        cell4.append(validationClothingQuantity);
        cell5.append(inputClothingPriceUnit);
        cell5.append(validationClothingPriceUnit);
        cell6.append(inputClothingInvestmentUnitLabel);
        cell7.append(inputDelete);

        newRow.append(cell1);
        newRow.append(cell2);
        newRow.append(cell3);
        newRow.append(cell4);
        newRow.append(cell5);
        newRow.append(cell6);
        newRow.append(cell7);

        tableBody.append(newRow);

        count++;

        $(`input[name='PostSalesClothesSizeCount']`).valid();
    });

}

function deleteClothing() {

    $(this).closest('tr').remove();
    count--;
    refreshNumeration();

}

function refreshNumeration() {
    tableBody.children('tr').each(function (index, element) {

        let firstColumn = $(this).find('td').eq(0);
        let column = $(this).find('td:nth-child(2),td:nth-child(3),td:nth-child(4),td:nth-child(5)');

        column.children('input,span').each(function (indexColumn, elementColumn) {
            if ($(elementColumn).is(':input') && $(elementColumn).attr('name').includes('PostSalesClothesSize')) {
                let name = $(elementColumn).attr('name').toString().replace(/\d/g, index);
                $(elementColumn).attr('name', name);
            }

            if ($(elementColumn).is('span') && $(elementColumn).attr('data-valmsg-for').includes('PostSalesClothesSize')) {
                let name = $(elementColumn).attr('data-valmsg-for').toString().replace(/\d/g, index);
                $(elementColumn).attr('data-valmsg-for', name);
            }

            if ($(elementColumn).is(':input') && $(elementColumn).attr('name').includes('Clothing_')) {
                let name = $(elementColumn).attr('name').toString().replace(/\d/g, index);
                $(elementColumn).attr('name', name);
            }

            if ($(elementColumn).is(':input') && $(elementColumn).attr('name').includes('InvestmentUnitLabel_')) {
                let name = $(elementColumn).attr('name').toString().replace(/\d/g, index);
                $(elementColumn).attr('name', name);
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

function validacionCountClothing() {
    $.validator.addMethod("countClothing", function (value, element) {
        //count variable global
        return count > 0;
    }, "Debe agregar al menos una prenda");

    $.validator.addMethod("quantityLessThanStock", function (value, element, params) {
        let index = params;
        let stock = $(`input[name='PostSalesClothesSize[${index}].ClothingSizeStock']`).val();
        console.log(index);
        console.log(stock);
        return value <= stock;
    }, "El campo cantidad debe ser menor o igual al stock");

}

async function sourceClothing(request, response) {
    let data = await getDataAutocomplete(request.term, 10);

    response(data.map(function (item) {
        console.log(item);
        return {
            value: item.name + ' | ' + item.sizeName,
            id: item.id,
            investmentUnit: item.investmentUnit,
            clothingSizeId: item.clothingSizeId,
            clothingSizeStock: item.clothingSizeStock
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

    let urlConsulta = `${urlAutocompleteClothing}?${params}`;

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

function selectClothing(event, ui) {
    
    let input = $(this);
    let id = input.attr('name').replace('Clothing_', '');
    let inputClothingId = `input[name="PostSalesClothesSize[${id}].ClothingId"]`;
    let inputClothingName = `input[name="PostSalesClothesSize[${id}].ClothingName"]`;
    let inputClothingInvestmentUnit = `input[name="PostSalesClothesSize[${id}].InvestmentUnit"]`;
    let inputClothingSizeId = `input[name="PostSalesClothesSize[${id}].ClothingSizeId"]`;
    let inputClothingSizeStock = `input[name="PostSalesClothesSize[${id}].ClothingSizeStock"]`;
    let ClothingInvestmentUnitLabel = `input[name="InvestmentUnitLabel_${id}"`;
    //let selectClothingSize = `select[name="PostSalesClothesSize[${id}].ClothingSizeId"]`;
    console.log(ui.item);

    let sizes = ui.item.clothingSizes;

    /*sizes.forEach(item => {
        console.log($(selectClothingSize));
        let option = $('<option>').text(item.description).val(item.id);
        console.log(option);
        $(selectClothingSize).append(option);
    });*/

    $(inputClothingId).val(ui.item.id);
    $(inputClothingName).val(ui.item.value);
    $(inputClothingInvestmentUnit).val(ui.item.investmentUnit);
    $(inputClothingSizeId).val(ui.item.clothingSizeId);
    $(inputClothingSizeStock).val(ui.item.clothingSizeStock);
    $(ClothingInvestmentUnitLabel).val(ui.item.investmentUnit);
    

    input.prop('disabled', true);

    $(`input[name='PostSalesClothesSize[${id}].ClothingId']`).valid();
}

