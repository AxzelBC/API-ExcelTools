#!/bin/bash

# Build Image

# Obtenemos la ruta del script en ejecucion
scriptPath="$(dirname "$(readlink -f "$0")")"
# Obtenemos la carpeta padre del script
parentFolder="$(dirname "$scriptPath")"
# Me muevo a la carpeta del padre
cd "$parentFolder" || exit

sudo docker build -t excel-tools-api -f ./Services/ExcelTools.API/Dockerfile .
