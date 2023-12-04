let display = [];
let connection = null;
const itemDisplayElement = document.getElementById('displayItems');

const IdElement = document.getElementById('itemId');
const NameElement = document.getElementById('itemName');
const AgeElement = document.getElementById('itemAge');

const updateIdElement = document.getElementById('updateId');
const updateNameElement = document.getElementById('updateName');
const updateAgeElement = document.getElementById('updateAge');

getData();
setupSignalR();

async function getData() {
    await fetch('http://localhost:21058/Owner')
        .then(x => x.json())
        .then(y => {
            display = y;
            console.log(y);
            displayItems();
        });
}
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:21058/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build()

    connection.on("OwnerCreated", (user, message) => {
        getData();
    });

    connection.on("OwnerDeleted", (user, message) => {
        getData();
    });

    connection.on("OwnerUpdated", (user, message) => {
        getData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}
function displayItems() {
    itemDisplayElement.innerHTML = null;
    display.forEach(t => {
        itemDisplayElement.innerHTML += "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.age + "</td><td><button class='btn btn-success' type='button' onclick='deleteItem(" + t.id + ")'>Delete</button><button class='btn btn-success' type='button' onclick='showUpdate(" + t.id + ")'>Update</button></td></tr>"
    });
}
function deleteItem(id) {
    fetch('http://localhost:21058/Owner/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null,
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });
}

function create() {
    let id = Number(IdElement.value);
    let name = NameElement.value;
    let age = Number(AgeElement.value);

    fetch('http://localhost:21058/Owner', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: id, name: name, age: age })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });

    IdElement.value = null;
    NameElement.value = null;
    AgeElement.value = null;
}

function showUpdate(id) {
    var toBeUpdated = display.find(d => d['id'] == id);
    document.getElementById("updateDiv").style.display = null;
    document.getElementById("formDiv").style.display = "none";

    updateIdElement.value = toBeUpdated.id;
    updateNameElement.value = toBeUpdated.name;
    updateAgeElement.value = toBeUpdated.age;
}

function update() {
    document.getElementById("updateDiv").style.display = "none";
    document.getElementById("formDiv").style.display = null;
    let id = Number(updateIdElement.value);
    let name = updateNameElement.value;
    let age = Number(updateAgeElement.value);

    fetch('http://localhost:21058/Owner', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: id, name: name, age: age })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });

    updateIdElement.value = null;
    updateNameElement.value = null;
    updateAgeElement.value = null;
}