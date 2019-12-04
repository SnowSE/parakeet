docker build . -t snowcollege/parakeet:sentence-generator-v3.1

docker push snowcollege/parakeet:sentence-generator-v3.1

kubectl apply -f ..\kube\parakeet.yml

kubectl get pods