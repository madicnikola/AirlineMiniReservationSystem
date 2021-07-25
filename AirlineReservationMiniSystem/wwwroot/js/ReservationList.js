const onApprove = (reservationId,numberOfSeats) => {

    console.log(reservationId);
    console.log(numberOfSeats);
    fetch("/Reservation/ApproveReservation",
        {
            method: "POST",
            body: JSON.stringify({ReservationId: parseInt(reservationId), NumberOfSeats: parseInt(numberOfSeats)}),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text()).catch(e => console.log(e))
        .then(id => {
            if (id == "No free seats available") {
                alert("No free seats available");
            } else {
                console.log(id);
                console.log("Rez id:" + reservationId);
                console.log(numberOfSeats);
                connection.invoke("JoinRoom","Agent");
                connection.invoke("GetUpdateForReservation", parseInt(id));
            }

        });
}
const onCancel = (reservationId) => {

    console.log(reservationId);
    fetch("/Reservation/CancelReservation",
        {    
            method: "POST",
            body: JSON.stringify(parseInt(reservationId)),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => {
            console.log("Rez id:" + reservationId);
            connection.invoke("JoinRoom","Agent");
            connection.invoke("GetUpdateForReservation", parseInt(id));
        }).catch(e => console.log(e));
}