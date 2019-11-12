const download = require('image-downloader')
const util = require('util');
const exec = util.promisify(require('child_process').exec);
var fs = require('fs');
var request = require('request');

const TesseractRunner = {
    getimagefromweb: async function(imageurl){
        options = {
            url: imageurl,
            dest: './photo.jpg'
          }
           
          await download.image(options)
            .then(({ filename, image }) => {
            })
            .catch((err) => console.error(err))
    },
    runtesseract: async function(){
        const { stdout, stderr } = await exec('tesseract photo.jpg out');
    },
    getdataout: async function(){
        file = fs.readFileSync("out.txt", 'utf8')
        return file;
    },
    tesseract: async function(imageurl){
        await this.getimagefromweb(imageurl);
        await this.runtesseract();
        file = await this.getdataout();
        fileobject = {"file":file}
        return fileobject
    }
}

module.exports = TesseractRunner;


