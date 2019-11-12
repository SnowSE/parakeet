FROM node:10
WORKDIR /usr/src/app
RUN apt-get update
RUN apt-get install tesseract-ocr -y

COPY package*.json ./

RUN npm install


COPY . .

CMD [ "node", "index.js" ]

