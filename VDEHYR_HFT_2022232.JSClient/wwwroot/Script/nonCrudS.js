let items = [];
var output;

const CountElement = document.getElementById('variableCount');
const YearElement = document.getElementById('variableYear');
const BIdElement = document.getElementById('variableBId');

const displayItemsElement = document.getElementById('displayItems');

var count;
var year;
var bid;

function getYear() {
    year = YearElement.value;
    if (!isNaN(year)) { return year; }
    else {
        year = null;
        console.log("Wrong input variables!");
    }
    return year;
}
function getCount() {
    count = CountElement.value;
    if (!isNaN(count)) { return count; }
    else {
        count = null;
        console.log("Wrong input variables!");
    }
    return count
}
function getBId() {
    bid = BIdElement.value;
    if (!isNaN(bid)) { return bid; }
    else {
        bid = null;
        console.log("Wrong input variables!");
    }
    return bid
}

function DogStats() {
    items = null;
    fetch('http://localhost:21058/ExtendMethodLogic/DogStats')
        .then(x => x.json())
        .then(y => {
            items = y;
            console.log(y);
            displayStats();
        });
}
function DogsBornBeforeIsBreed() {
    items = null;
    var year = getYear();
    var bid = getBId();
    if (year != null && year != '' && bid != null && bid != '') {
        fetch('http://localhost:21058/ExtendMethodLogic/DogsBornBeforeIsBreed/' + year + '/' + bid)
            .then(x => x.json())
            .then(y => {
                items = y;
                console.log(y);
                displayDogs();
            })
    }
    else if (year == '' && bid == '') {
        alert("Year and BreedID was null!");
    }
    else if (bid == '') {
        alert("BreedID was null!");
    }
    else {
        alert("Year was null!");
    }
}
function DogsBornAfterIsBreed() {
    items = null;
    var year = getYear();
    var bid = getBId();
    if (year != null && year != '' && bid != null && bid != '') {
        fetch('http://localhost:21058/ExtendMethodLogic/DogsBornAfterIsBreed/' + year + '/' + bid)
            .then(x => x.json())
            .then(y => {
                items = y;
                console.log(y);
                displayDogs();
            })
    }
    else if (year == '' && bid == '') {
        alert("Year and BreedID was null!");
    }
    else if (bid == '') {
        alert("BreedID was null!");
    }
    else {
        alert("Year was null!");
    }
}
function OwnerWithMoreDogsThanAndOlderThan() {
    items = null;
    var year = getYear();
    var count = getCount();
    if (year != null && year != '' && count != null && count != '') {
        fetch('http://localhost:21058/ExtendMethodLogic/OwnerWithMoreDogsThanAndOlderThan/' + count + '/' + year)
            .then(x => x.json())
            .then(y => {
                items = y;
                console.log(y);
                displayOwners();
            })
    }
    else if (year == '' && count == '') {
        alert("Year and count was null!");
    }
    else if (count == '') {
        alert("count was null!");
    }
    else {
        alert("Year was null!");
    }
}
function BreedWithDogsMoreThan() {
    items = null;
    var count = getCount();
    if (count != null && count != '') {
        fetch('http://localhost:21058/ExtendMethodLogic/BreedWithDogsMoreThan/' + count)
            .then(x => x.json())
            .then(y => {
                items = y;
                console.log(y);
                displayBreeds();
            })
    }
    else {
        alert("Count was null!");
    }
}
function OwnerWithMoreDogsThan() {
    items = null;
    var count = getCount();
    if (count != null && count != '') {
        fetch('http://localhost:21058/ExtendMethodLogic/OwnerWithMoreDogsThan/' + count)
            .then(x => x.json())
            .then(y => {
                items = y;
                console.log(y);
                displayOwners();
            })
    }
    else {
        alert("Count was null!");
    }
}
function displayStats() {
    displayItemsElement.innerHTML = null;
    output = "";
    items.forEach(t => {
        output +=
            "<tr><td>" + t.owner.id + " - " + t.owner.name + ", " + t.owner.age +" yo</td>" +
            "<td>Dogs Owned:</td><td>";
        t.dogs.forEach(u => {
            output +=
                "<label>ID: " + u.id + ", Name: " + u.name + ", Born: " + u.birthYear + ", Color Code: " + u.color + "</label><br/>";
        })
        output += "</td></tr>";
    })
    displayItemsElement.innerHTML = output;
}
function displayDogs() {
    displayItemsElement.innerHTML = null;
    items.forEach(t => {
        displayItemsElement.innerHTML += "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>Born: " + t.birthYear + "</td><td>" + t.weight + " kg</td><td>Color: " + t.color + "</td></tr>"
    });
}
function displayBreeds() {
    displayItemsElement.innerHTML = null;
    items.forEach(t => {
        displayItemsElement.innerHTML += "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>From: " + t.origin + "</td><td>Lifespan: " + t.lifespan + " years</td></tr>"
    });
}
function displayOwners() {
    displayItemsElement.innerHTML = null;
    items.forEach(t => {
        displayItemsElement.innerHTML += "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>Age: " + t.age + "</td></tr>"
    });
}