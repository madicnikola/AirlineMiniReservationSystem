const { signalR } = require("./signalr");

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/reservationhub")
        .build();
    connection.on("ReceiveReservationUpdate", (update) => {
        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = update;
    });
    connection.on("NewReservation", function (reservationRequest) {
        var statusDiv = document.getElementById("status");
        statusDiv.innerHTML = "Someone requests a reservation for flight " + reservationRequest.flight;
    });

    connection.on("finished", function () {
        connection.stop();
    });

    connection.start()
        .catch(err => console.error(err.toString()));
}

setupConnection();

// Modify code below for AJAX call to controller for Flight Reservation Request

document.getElementById("submitReservation").addEventListener("click", e => {
    e.preventDefault();
    const flightId = document.getElementById("FlightId").value;
    const numberOfSeats = document.getElementById("NumberOfSeats").value;

    fetch("/Reservation/RequestReservation",
        {
            method: "POST",
            body: JSON.stringify({ flightId, numberOfSeats }),
            headers: {
                'content- type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => connection.invoke("GetUpdateForReservation", id));
});
