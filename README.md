# Organization API

This is a project that built on DDD approach and represent management of organizations.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

In order to run this project, you'll need to have .NET Core 2.2 or higher on your local development machine.

### Installing

First of all you'll need to complete following commands:

```bash
git clone https://github.com/Kicctoll/OrganizationAPI.git
cd OrganizationAPI
dotnet restore
```

## Build and run

Inside the newly created project, you can run some built-in commands:

```bash
dotnet ef migrations add Initial -p src/Web/Web.csproj
dotnet ef database update -p src/Web/Web.csproj
dotnet build
```

In order to create migration, apply it and build the Web project and all it's dependencies.

```bash
dotnet run -p src/Web/Web.csproj
```

In order to run the web server. Open [http://localhost:5000/swagger](http://localhost:5000/swagger) or [https://localhost:5001/swagger](https://localhost:5001/swagger) to view it in the browser.

## Running the tests

To run the unit tests complete the following command:

```bash
dotnet test
```

And you'll see the results of unit testing in console.

## Built With

* [ASP.NET Core 2.2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2) - Web framework to build the REST Api
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM to data access
* [XUnit](https://rometools.github.io/rome/) - Testing tool for unit tests
