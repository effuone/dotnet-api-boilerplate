# Bike Stores API

This is a template repository for beginners that follows a clean architecture pattern. The RESTful API allows clients to perform CRUD operations on the products, brands, and categories tables in the database. In addition, the API supports similar operations on other tables, such as orders, customers, and employees. The project includes separate layers for the presentation, application, and domain logic, as well as repositories and interfaces to abstract data access. The application also incorporates dependency injection and logging to improve maintainability and extensibility.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- MSSQL Server 
- AutoMapper
- Repository Pattern
- DTO Pattern
- Swagger UI

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