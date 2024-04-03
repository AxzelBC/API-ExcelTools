#!/bin/bash

# Run Container
docker run -p 127.0.0.1:5033:8080/tcp -it --entrypoint /bin/bash excel-tools-api -c "dotnet ExcelTools.API.dll"
