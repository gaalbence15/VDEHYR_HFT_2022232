let display = [];
let connection = null;
const itemDisplayElement = document.getElementById('displayItems');

const IdElement = document.getElementById('itemId');
const NameElement = document.getElementById('itemName');
const OriginElement = document.getElementById('itemOrigin');
const LifespanElement = document.getElementById('itemId');

const updateIdElement = document.getElementById('updateId');
const updateNameElement = document.getElementById('updateName');
const updateOriginElement = document.getElementById('updateOrigin');
const updateLifespanElement = document.getElementById('updateLifespan');

getData();
setupSignalR();
async function getData() {
    await fetch('http://localhost:21058/Breed')
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

    connection.on("BreedCreated", (user, message) => {
        getData();
    });

    connection.on("BreedDeleted", (user, message) => {
        getData();
    });

    connection.on("BreedUpdated", (user, message) => {
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
        itemDisplayElement.innerHTML += "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.origin + "</td><td>" + t.lifespan + "</td><td><button class='btn btn-success' type='button' onclick='deleteItem(" + t.id + ")'>Delete</button><button class='btn btn-success' type='button' onclick='showUpdate(" + t.id + ")'>Update</button></td></tr>"
    });
}
function deleteItem(id) {
    fetch('http://localhost:21058/Breed/' + id, {
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
    let origin = OriginElement.value;
    let lifespan = Number(LifespanElement.value);

    fetch('http://localhost:21058/Breed', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: id, name: name, origin: origin, lifespan: lifespan })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });

    IdElement.value = null;
    NameElement.value = null;
    OriginElement.value = null;
    LifespanElement.value = null;
}

function showUpdate(id) {
    var toBeUpdated = display.find(d => d['id'] == id);
    document.getElementById("updateDiv").style.display = null;
    document.getElementById("formDiv").style.display = "none";

    updateIdElement.value = toBeUpdated.id;
    updateNameElement.value = toBeUpdated.name;
    updateOriginElement.value = toBeUpdated.origin;
    updateLifespanElement.value = toBeUpdated.lifespan;
}

function update() {
    document.getElementById("updateDiv").style.display = "none";
    document.getElementById("formDiv").style.display = null;
    let id = Number(updateIdElement.value);
    let name = updateNameElement.value;
    let origin = updateOriginElement.value;
    let lifespan = Number(updateLifespanElement.value);

    fetch('http://localhost:21058/Breed', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: id, name: name, origin: origin, lifespan: lifespan })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });

    updateIdElement.value = null;
    updateNameElement.value = null;
    updateOriginElement.value = null;
    updateLifespanElement.value = null;
}