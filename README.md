# dotnet-microservices
Trying to setup multiple connected microservices. All commands noted below are executed in the root project directory. This guide currently contains
- [Creating a WebApi](#users-api)
- [Setting up Visual Studio Code for debugging](#setting-up-visual-studio-code)
- [Fetching and updating data using Entity Framework](#using-entity-framework)
- Docker
    - [WebApi](#Dockerize-users-api)
    - [SQL Server](#dockerize-sql-server)

## Overview

![Services overview](./assets/readme/services.png "Services overview")

| Service | URL |
| ------- | --- |
| [Users API](#users-api) | [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html) <br/> [http://localhost:50010/swagger/index.html](http://localhost:50010/swagger/index.html) |

## Users API
Let's create the first API. The first microservice is all about users. Paste the commands below in the command line to create the project.

```shell
dotnet new webapi -o ./src/Users.Api
```

### Setting up Visual Studio Code
After adding some controllers and Swagger configuration ([commit 313ec11](https://github.com/Thijs5/dotnet-microservices/commit/313ec11b3bf834e50ee32134ea2eca0b53421136)), the Users API is live on port 5001 (https) and 50010 (http). Running the API is as simple as running the command below.
```shell
dotnet run --project ./src/Users.Api/Users.Api.csproj
```
Using the command line to run the application is an insult to our IDE since it has a button for it. By adding a `launch.json` and a `tasks.json` (both generated by Visual Studio Code), we can run the users api by the press of a button ([commit 201bfd1](https://github.com/Thijs5/dotnet-microservices/commit/201bfd18bfd7a145e4fbd1abdb2a6443c19b909c)).

### Using Entity Framework
The Users Api will store user data. We'll be using a combination of [Entity Framework](https://docs.microsoft.com/en-us/aspnet/entity-framework) and [SQL Server](https://www.microsoft.com/nl-nl/sql-server/sql-server-2019). We start by installing the necessary Nuget packages to the solution. Using the [Nuget Package Manager extension](https://marketplace.visualstudio.com/items?itemName=jmrog.vscode-nuget-package-manager), installing packages is a breeze. We need the following packages:
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`

After configuring EF and setting up the CRUD actions in the controllers, we still need to create the database and migrations. Open a terminal window and paste the commands below.
```shell
dotnet tool install --global dotnet-ef
dotnet ef migrations add CreateUsersDB --project ./src/Users.Api/Users.Api.csproj 
```
The first commands installs the EF tools needed to run migrations. If they are already installed, you can skip this command. The second one will migrate the database to a version that corresponds with how it is configured in the code. After running the `dotnet ef migrations`-command, notice there is a Migrations-folder added to the project.

After verifying the migration looks good, It is time to actually update the database scheme. Running all migrations can be done using the command below.
```shell
dotnet ef database update --project ./src/Users.Api/Users.Api.csproj
```
[Commit 9a3f636](https://github.com/Thijs5/dotnet-microservices/commit/9a3f6366609dd65ce4ec0a6b798fc9ae2dda8daa)

### Docker
To develop using Docker a lot easier, it's recommended to install [the Docker extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker).

Also because of some https issues, the following commands need to be run beforehand.
```shell
# Mac
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Password
dotnet dev-certs https --trust

# Windows
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Password
dotnet dev-certs https --trust

# Linux
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Password
# dotnet dev-certs https --trust is only supported on macOS and Windows. You need to trust certs on Linux in the way that is supported by your distribution.
```

#### Dockerize Users API
The first step is creating a `Dockerfile`. This is easy using Visual Studio Code. Use the `Add Dockerfile to Workspace`-command to start the wizard. Because the Users API is the only thing running in this container, we map the ports the the default http (80) and https (443) ports.

Verify the image by building and running it.
```shell
docker-compose build
docker-compose up
```
Debugging is possible using a new Visual Studio Code configuration. Since the API is running in Docker, it is impossible to access the local SQL Server so no request will currently succeed. The only way forward is Dockerize the SQL Server.

[Commit 2eed146](https://github.com/Thijs5/dotnet-microservices/commit/2eed1461086a4ba56625769647a155a1ab2cacbc)

#### Dockerize SQL Server
To let the Users API communicate with SQL Server, we also need to create a container. By quickly adding some lines in the `docker-compose.yml` we have a container ready ([commit 69db2c4](https://github.com/Thijs5/dotnet-microservices/commit/69db2c4d1c1e02c09edf1350700544edcc9d577a)).

Normally the EF migrations run on startup. Because of the Docker setup, it is possible the SQL container isn't ready yet at the moment the API setups. To fix this quick and dirty, I've added a migration controller. By using this controller it is possible to migrate the database manually.
