#!/bin/bash

# Generate and initialize env variables

# Start the project
cd src
docker buildx build -t excel-tools-api -f ./Services/ExcelTools.API/Dockerfile .
docker run -p 127.0.0.1:5033:8080/tcp --env ASPNETCORE_ENVIRONMENT=Development -it --entrypoint /bin/bash excel-tools-api -c "dotnet ExcelTools.API.dll"