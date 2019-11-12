var express = require('express');
var app = express();
const bodyParser = require('body-parser');
app.use(bodyParser.json());
var cors = require('cors');
const port = 80;
var whitelist = "http:/144.17.24.16";
const TesseractRunner = require("./tesseractRunner.js")
const RequestOut = require("./RequestOut.js")


var corsOptions={
    origin:function(origin,callback){
      if(whitelist.indexOf(origin)===-1){
        callback(null,true)
      }else{
        console.log("blocked by cors")
        callback(new Error('not allowed by cors'))
      }
    }
  }


app.post('/ocrapi',cors(corsOptions),async function(req,res){
    console.log(process.env.PARSING_URL)
    var result = await TesseractRunner.tesseract(req.body.imageurl)
    RequestOut.sendrequest(process.env.PARSING_URL,result.file);
    res.write(result.file);
    res.end();
});


app.listen(port, () => console.log(`Example app listening on port ${port}!`))