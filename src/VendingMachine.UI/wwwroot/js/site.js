let sum = 0;

function addNRuble(n) {
    sum += n;
    console.log(sum);
    updateBalance();
}

function getChange() {
    sum = 0;
    updateBalance();
}

function buyDrink(price) {
    if (sum < price) {
        alert("Not enough money!");
        return;
    }

    sum -= price;
    updateBalance();
}

function updateBalance() {
    document.getElementById("balance").textContent = sum;
}
