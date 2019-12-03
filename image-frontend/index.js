var express = require('express');
var app = express();
var path = require('path');

// viewed at http://localhost:8080
app.get('/', function(req, res) {
    res.sendFile(path.join(__dirname + '/index.html'));
    console.log("served index.html");
});

app.listen(80);
console.log("started server, listening on port 80");