FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MessageBroker.Microservices.MessageQueue_B.WebAPI/MessageBroker.Microservices.MessageQueue_B.WebAPI.csproj", "MessageBroker.Microservices.MessageQueue_B.WebAPI/"]
RUN dotnet restore "MessageBroker.Microservices.MessageQueue_B.WebAPI/MessageBroker.Microservices.MessageQueue_B.WebAPI.csproj"
COPY . .
WORKDIR "/src/MessageBroker.Microservices.MessageQueue_B.WebAPI"
RUN dotnet build "MessageBroker.Microservices.MessageQueue_B.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageBroker.Microservices.MessageQueue_B.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageBroker.Microservices.MessageQueue_B.WebAPI.dll"]
