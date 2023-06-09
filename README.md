# .NET REST API boilerplate

This is a boilerplate template repository for beginners, specifically designed for .NET API development, using the example of the BikeStores database for SQL practice. It follows a clean architecture pattern to ensure a well-structured codebase.

The RESTful API provided allows clients to perform CRUD (Create, Read, Update, Delete) operations on various tables in the database, such as products, brands, and categories. Furthermore, the API supports similar operations on additional tables, including orders, customers, and employees.

## Technologies Used

- ASP.NET Core 
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [MSSQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [AutoMapper](https://automapper.org/)
- [Repository Pattern](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
- [DTO Pattern](https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5)
- [Swagger UI](https://swagger.io/docs/)
- [Bike Stores database](https://www.sqlservertutorial.net/sql-server-sample-database/)

## Getting started
### Prerequisites

- .NET Core SDK
- Docker/Local SQL Server

Project requires [.NET](https://dotnet.microsoft.com/en-us/) v6.0.1 to run.

### Installation

#### 1. Clone this project
```bash
  git clone https://github.com/effuone/BikeStoresAPI.git
```
#### 2. Navigate the project directory
```bash
  cd BikeStoresAPI
```
#### 3. Set up database connection
#### 3.1 Docker
If you have Docker installed, follow these steps:
- Create a `.env` file by running `touch .env`
- Add the following environment variables to your `.env` file:

`DB_USER=[CustomUser]`

`DB_PASSWORD=[YourPassword]`

`DB_PORT=[YourPort]`

`DB_DATABASE=BikeStores`

- Run the project with the following command 
```sh
docker-compose up
```
#### 3.2 Local SQL Server
If you have a local SQL Server installed, follow these steps:
- Open the command prompt and navigate to the project directory
- Run the following command to set up your user secrets:
```sh
dotnet user-secrets set "DB_SERVER" "(localdb)\MSSQLLocalDB"
dotnet user-secrets set "DB_DATABASE" "BikeStores"
```
- Replace the connection string with your own database credentials
- Run the project with the following command:
```sh
dotnet run 
```
