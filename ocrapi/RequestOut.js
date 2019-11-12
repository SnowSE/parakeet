const request = require('request')

const RequestOut={
    sendrequest: async function(url,data){
        request.post(url, {
        json: {
            "data":data
        }
        }, (error, res, body) => {
        if (error) {
            console.error(error)
            return
        }
        console.log(`statusCode: ${res.statusCode}`)
        console.log(body)
        })
    }
}

module.exports = RequestOut;

