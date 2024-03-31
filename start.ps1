# Generate and initialize env variables

# Start the project
cd src
docker build -t excel-tools-api -f .\Services\ExcelTools.API\Dockerfile .
docker run -it --entrypoint /bin/bash excel-tools-api -c "dotnet ExcelTools.API.dll"
