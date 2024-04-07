#!/bin/bash

# Obtenemos la ruta del script en ejecucion
scriptPath="$(dirname "$(readlink -f "$0")")"
# Obtenemos la carpeta padre del script
parentFolder="$(dirname "$scriptPath")"
# Me muevo a la carpeta del padre
cd "$parentFolder" || exit

# Run container
docker start ExcelTools.database
# Wait 5 seconds
sleep 5s
# Run backup in container and export to Database dir
docker exec -t ExcelTools.database pg_dumpall -c -U postgres > Database/dump_`date +%Y-%m-%d"_"%H_%M_%S`.sql
# Stop Container
docker stop ExcelTools.database