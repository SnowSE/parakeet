var express = require('express');
var app = express();
var path = require('path');

// viewed at http://localhost:8080
app.get('/', function(req, res) {
    res.sendFile(path.join(__dirname + '/index.html'));
    console.log("served index.html");
});

app.get('/image-url', function(req, res) {
    console.log("recieved image url")
    console.log(req.body)
    request.post(process.env.OCR_API_URL, {
        json: {
            "imageurl":req.body.imageurl
        }
        }, (error, res, body) => {
        if (error) {
            console.error(error)
            return
        }
        console.log(`statusCode: ${res.statusCode}`)
        console.log(body)
        })

    //res.sendFile(path.join(__dirname + '/index.html'));
    //console.log("served index.html");
});

app.listen(80);
console.log("started server, listening on port 80");