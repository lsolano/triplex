#!/bin/bash

if [ -z $1 ]; then
    echo "The Nuget API Key was not set (first parameter)."
    exit 1
else
    echo "The Nuget API Key was set."
fi

SOURCE=$2
if [ -z "$SOURCE" ]
    echo "Working with provided Nuget Source '$SOURCE'."
then
   SOURCE="https://api.nuget.org/v3/index.json"
   echo "Working with default Nuget Source '$SOURCE'."
   echo "Use the second command line argument to set the source if you like to set override the default."
fi

dotnet clean
dotnet build --configuration Release
cd src/Validations/bin/Release
dotnet nuget push *.nupkg -k $1 -s $SOURCE