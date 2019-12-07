var express = require('express');
var app = express();
var path = require('path');
const request = require('request');
const bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({ extended: true }));

// viewed at http://localhost:8080
app.get('/', function(req, res) {
    res.sendFile(path.join(__dirname + '/index.html'));
    console.log("served index.html");
});

app.post('/image-url', function(req, res) {
    console.log("recieved image url")
    console.log("request: " + req)
    console.log("body: " + req.body.imageurl)
    var clientServerOptions = {
        uri: process.env.OCR_API_URL,
        body: JSON.stringify(req.body),
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }
    console.log("request options: " + JSON.stringify(clientServerOptions));
    request(clientServerOptions, function (error, response) {
        console.log(error,response.body);
        return;
    });

    res.redirect('/');
});

app.listen(80);
console.log("started server, listening on port 80");