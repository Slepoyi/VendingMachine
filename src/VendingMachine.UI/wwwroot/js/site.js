function addNRuble(n) {
    $.ajax({
        type: "POST",
        url: '/Drinks/AddCoin/',
        data: {
            value: n
        },
        success: updateBalance,
        error: function () { }
    });
}

function getChange() {
    updateBalance();
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