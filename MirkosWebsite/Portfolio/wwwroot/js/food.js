$("#add").click(function () {
    var i = $(".clonable").length;
    var clone = $("#clonableContainer div.clonable").eq(0).clone();
    clone.find('select').each(function () {
        this.name = this.name.replace('[0]', '[' + i + ']');
        this.id = this.id.replace('0', i);
    });
    clone.find('input').each(function () {
        this.name = this.name.replace('[0]', '[' + i + ']');
        this.id = this.id.replace('0', i);
    });
    clone.find('textarea').each(function () {
        this.name = this.name.replace('[0]', '[' + i + ']');
        this.id = this.id.replace('0', i);
    });

    $('#clonableContainer').append(clone);

    $(document).bind("viewtransfer", function () {

        $('form').removeData('validator');

        $('form').removeData('unobtrusiveValidation');

        $.validator.unobtrusive.parse('form');

    });

});

$('#remove').click(function () {
    var i = $(".clonable").length - 1;
    if (i > 0) {
        var remove = $("#clonableContainer div.clonable").eq(i).remove();
    }
});

$('#recipeForm').submit(function () {
    var x = $(".clonable").length;
    for (var i = 0; i < x; i++) {
        var id = "#notEmpty" + i;
        if ($.trim($(id).val()) === "") {
            $('#error').modal('show');
            $(id).css({ backgroundColor: '#fdaaaa' });
            $('#alertEmptyField').text('Please select an ingredient.');
            return false;
        }
        else {
            $(id).css({ backgroundColor: 'white' });
        }

        id = "#quantity" + i;
        if ($.trim($(id).val()) === "") {
            $('#error').modal('show');
            $(id).css({ backgroundColor: '#fdaaaa' });
            $('#alertEmptyField').text('Please enter a number in the Quantity field.');
            return false;
        }

        else if ($.trim($(id).val()) === "0") {
            $('#error').modal('show');
            $(id).css({ backgroundColor: '#fdaaaa' });
            $('#alertEmptyField').text('The Quantity field must be a number higher than 0.');
            return false;
        }
        else if (isNaN($.trim($(id).val()))) {
            $('#error').modal('show');
            $(id).css({ backgroundColor: '#fdaaaa' });
            $('#alertEmptyField').text('Please only enter valid numbers in the Quantity field.');
            return false;
        }
        else {
            $(id).css({ backgroundColor: 'white' });
        }
    }
});

$('#recipeStepsForm').submit(function () {
    var x = $(".clonable").length;
    for (var i = 0; i < x; i++) {
        var id = "#notEmpty" + i;
        if ($.trim($(id).val()) === "") {
            $('#error').modal('show');
            $('#alertEmptyField').text('Step title cannot be empty.');
            return false;
        }
        id = "#notEmptyText" + i;
        if ($.trim($(id).val()) === "") {
            $('#error').modal('show');
            $('#alertEmptyField').text('Step body cannot be empty.');
            return false;
        }
    }
});

$(function () {

    $('.list-group-item').on('click', function () {
        $('.fa', this)
            .toggleClass('fa-chevron-right ')
            .toggleClass('fa-chevron-down ');
    });

});