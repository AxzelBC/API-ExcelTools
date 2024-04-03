# Build docker image

# Obtenemos la ruta del script actual
$scriptPath = $MyInvocation.MyCommand.Path
# Obtenemos la carpeta padre del script
$parentFolder = Split-Path -Path $scriptPath -Parent
$parentFolder = Split-Path -Path $parentFolder -Parent
# Nos movemos a la carpeta padre
Set-Location -Path $parentFolder

# Ejecutamos el comando deseado, por ejemplo, un comando de ejemplo
Write-Host "***** Generando imagen *****"
docker buildx build -t excel-tools-api -f ./Services/ExcelTools.API/Dockerfile .