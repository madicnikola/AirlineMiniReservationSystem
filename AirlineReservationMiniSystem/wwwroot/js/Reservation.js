document.getElementById("submitReservation").addEventListener("click", e => {
    e.preventDefault();
    const flightId = parseInt(document.getElementById("FlightId").value);
    const numberOfSeats = parseInt(document.getElementById("NumberOfSeats").value);

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
            console.log("rez ID ---> " + id);
            alert("Reservation request sent successfully!");
            connection.invoke("JoinRoom", "Client");
        });
});
