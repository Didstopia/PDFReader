#!/bin/bash

set -e
set -o pipefail

BUILD_CONFIG=$1
NUGET_API_KEY=$2

if [[ "$BUILD_CONFIG" == "Release" ]]; then
	dotnet restore
	dotnet build --configuration ${BUILD_CONFIG}
	dotnet pack --configuration ${BUILD_CONFIG}
	nuget push Didstopia.PDFReader/bin/$BUILD_CONFIG/*.nupkg -ApiKey $NUGET_API_KEY;
fi
