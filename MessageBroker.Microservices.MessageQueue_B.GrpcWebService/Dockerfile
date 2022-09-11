FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MessageBroker.Microservices.MessageQueue_B.GrpcWebService/MessageBroker.Microservices.MessageQueue_B.GrpcWebService.csproj", "MessageBroker.Microservices.MessageQueue_B.GrpcWebService/"]
RUN dotnet restore "MessageBroker.Microservices.MessageQueue_B.GrpcWebService/MessageBroker.Microservices.MessageQueue_B.GrpcWebService.csproj"
COPY . .
WORKDIR "/src/MessageBroker.Microservices.MessageQueue_B.GrpcWebService"
RUN dotnet build "MessageBroker.Microservices.MessageQueue_B.GrpcWebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageBroker.Microservices.MessageQueue_B.GrpcWebService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageBroker.Microservices.MessageQueue_B.GrpcWebService.dll"]
