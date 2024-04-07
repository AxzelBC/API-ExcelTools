#!/bin/bash

# Run container
docker start ExcelTools.database
# Wait 5 seconds
sleep 5s
# Restore DB
cat $1 | docker exec -i ExcelTools.database psql -U postgres
# Stop Container
docker stop ExcelTools.database