@echo off

docker run -it --rm -p 8080:8080 -v %cd%:/usr/local/structurizr --name structurizr-local structurizr/lite

PAUSE