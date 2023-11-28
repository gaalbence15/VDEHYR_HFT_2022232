let display = [];
let connection = null;
const itemDisplayElement = document.getElementById('displayItems');
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
        itemDisplayElement.innerHTML += "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.origin + "</td><td>" + t.lifespan + "</td><td><button class='btn btn-success' type='button' onclick='deleteItem(" + t.id + ")'>Delete</button></td></tr>"
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