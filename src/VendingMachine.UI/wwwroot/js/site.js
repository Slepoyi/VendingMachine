function addNRuble(n) {
    $.ajax({
        type: "POST",
        url: '/Drinks/AddCoin/',
        data: {
            value: n
        },
        success: getDrinks,
        error: function () { }
    });
}

function getChange() {
    $.ajax({
        type: "POST",
        url: '/Drinks/GetChange/',
        data: {
            value: n
        },
        success: updateBalance,
        error: function () { }
    });
    updateBalance();
}

function getDrinks() {
    $.ajax({
        type: "GET",
        url: '/Drinks/GetDrinksPartial/',
        success: updateBalance,
        error: function () { }
    });
}

function buyDrink(id) {
    $.ajax({
        type: "POST",
        url: '/Drinks/OrderDrink/',
        data: {
            id: id
        },
        success: updateBalance,
        error: function () { }
    });
}

function updateBalance() {
    $.ajax({
        url: '/Drinks/GetBalance/',
        type: 'GET',
        success: function (response) {
            document.getElementById("balance").textContent = response;
        },
        error: function (error) { }
    });
}