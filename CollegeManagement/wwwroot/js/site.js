//Dashboard SignalR
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/RealTimeInfo/")
    .build();

async function start() {
    try {
        await connection.start();
        connection.invoke("GetInfo");
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

// Start the connection.
start();


connection.on("getCoursesInfo", (message) => {

    $('#CoursesRtInfo').html("Courses: " + message.coursesQtd);
});

connection.on("getTeachersInfo", (message) => {

    $('#TeachersRtInfo').html("Teachers: " + message.teachersQtd);

});

connection.on("getStudentsInfo", (message) => {

    $('#StudentsRtInfo').html("Students: " + message.studentsQtd);

});

//connection.start().catch(function (err) {
//    return console.error(err.toString());
//});


