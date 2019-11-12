#!/bin/bash
docker build . -t kyler 
docker run -p 80:80 -e "PARSING_URL=https://api.greenbeancooking.com/getallrecipes" kyler:latest 