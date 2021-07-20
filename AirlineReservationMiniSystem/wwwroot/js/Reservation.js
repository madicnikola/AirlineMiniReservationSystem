// Modify code below for AJAX call to controller for Flight Reservation Request

document.getElementById("submitReservation").addEventListener("click", e => {
    e.preventDefault();
    const flightId = parseInt(document.getElementById("FlightId").value);
    const numberOfSeats = parseInt(document.getElementById("NumberOfSeats").value);
    console.log(flightId);
    console.log(numberOfSeats);
    
    fetch("/Reservation/RequestReservation",
        {
            method: "POST",
            body: JSON.stringify({ flightId, numberOfSeats }),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => {
            console.log(id);
            connection.invoke("JoinRoom", "Client");
            toastr.success("Reservation request sent");
        });
});
