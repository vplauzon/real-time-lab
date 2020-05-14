#	Use a Microsoft image with .NET core runtime (https://hub.docker.com/r/microsoft/dotnet/tags/)
FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS final

#	Copy package
COPY bin .

#	Define environment variables
#   None...

#	Run startup script
CMD ["dotnet", "SimulatorClient.dll"]