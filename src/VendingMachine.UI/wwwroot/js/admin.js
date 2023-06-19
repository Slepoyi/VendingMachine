function deleteDrink(id) {
    $.ajax({
        type: "POST",
        url: '/admin/deletedrink/',
        data: {
            drinkId: id
        },
        success: getDrinksAdmin,
        error: function () { }
    });
}

function getDrinksAdmin() {
    $('#divPartial').load('/admin/drinkspartial/');
}