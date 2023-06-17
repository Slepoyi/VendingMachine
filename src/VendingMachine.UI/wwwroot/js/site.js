function addNRuble(n) {
    $.ajax({
        type: "POST",
        url: '/customer/addcoin/',
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
        url: '/customer/getchange/',
        success: updateBalance,
        error: function () { }
    });
    updateBalance();
}

function getDrinks() {
    $.ajax({
        type: "GET",
        url: '/customer/drinkspartial/',
        success: updateBalance,
        error: function () { }
    });
}

function buyDrink(id) {
    $.ajax({
        type: "POST",
        url: '/customer/orderdrink/',
        data: {
            drinkId: id
        },
        success: updateBalance,
        error: function () { }
    });
}

function updateBalance() {
    $.ajax({
        url: '/customer/getbalance/',
        type: 'GET',
        success: function (response) {
            document.getElementById("balance").textContent = response;
        },
        error: function (error) { }
    });
}