FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["SchoolManagment.csproj", "./"]
RUN dotnet restore "./SchoolManagment.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SchoolManagment.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SchoolManagment.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SchoolManagment.dll"]

#  RUN chmod +x . /begin.sh
# CMD /bin/bash . /begin.sh
