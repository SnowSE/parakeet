#!/bin/bash
version="1.10"

docker build -t snowcollege/parakeet:image-frontend-v$version . 
docker push snowcollege/parakeet:image-frontend-v$version

microk8s.kubectl delete all --all

microk8s.kubectl apply -f ../kube/parakeet.yml