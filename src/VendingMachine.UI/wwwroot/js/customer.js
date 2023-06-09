﻿function addNRuble(n) {
    $.ajax({
        type: "POST",
        url: '/customer/addcoin/',
        data: {
            value: n
        },
        success: getDrinksCustomer,
        error: function () { }
    });
}

function getChange() {
    $.ajax({
        type: "POST",
        url: '/customer/getchange/',
        success: function (response) {
            document.getElementById("changeMessage").textContent = response;
            getDrinksCustomer();
        },
        error: function () { }
    });
}

function getDrinksCustomer() {
    $('#divPartial').load('/customer/drinkspartial/'); 
    updateBalance();
}

function buyDrink(id) {
    $.ajax({
        type: "POST",
        url: '/customer/orderdrink/',
        data: {
            drinkId: id
        },
        success: function (response) {
            document.getElementById("drinksMessage").textContent = response;
            getDrinksCustomer();
        },
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