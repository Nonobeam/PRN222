"use strict";
console.log("health.js loaded");


var connection = new signalR.HubConnectionBuilder().withUrl("/healthHub").build();

connection.start().then(function () {
    console.log("Connected to SignalR hub");
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("Delete_Medicine", function (id) {
    console.log("Delete Medicine ID:", id);

    $("#medicineList").find(`tr[id='${id}']`).remove()
})