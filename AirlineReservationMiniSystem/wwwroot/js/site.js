// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/reservationhub")
        .build();
    
    connection.on("ReceiveReservationUpdate", (update) => {
        const  statusDiv = document.getElementById("Status "+ update.reservationId);
        const  freeSeatsDiv = document.getElementById("FreeSeats "+ update.reservationId);
        const btnApprove = document.getElementById("Approve " + update.reservationId);
        const btnCancel = document.getElementById("Cancel " + update.reservationId);
        
        console.log(update);
        console.log(update.reservationId);
        freeSeatsDiv.innerHTML = update.flight.numberOfFreeSeats;
        switch (update.status) {
            case 0:
                statusDiv.innerHTML = "PENDING";
                btnApprove.style.display = 'block';
                btnCancel.style.display = 'none';
                break;
            case 1:
                statusDiv.innerHTML = "CONFIRMED";
                btnApprove.style.display = 'none';
                btnCancel.style.display = 'block';
                break;
            case 2:
                statusDiv.innerHTML = "DECLINED";
                btnApprove.style.display = 'block';
                btnCancel.style.display = 'none';
                break;
        }
        console.log(statusDiv);
    });
    
    connection.on("NewReservation", function (reservationRequest) {
        var statusDiv = document.getElementById("status");
        statusDiv.innerHTML = "Someone requests a reservation for flight " + reservationRequest.flightId;
    });

    connection.on("finished", function () {
        connection.stop();
    });

    connection.start()
        .catch(err => console.error(err.toString()));
}

setupConnection();