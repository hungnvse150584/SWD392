# ASP.NET Core 4.8 project from TEDU
## Technologies
- ASP.NET Core 4.8
- Entity Framework Core 4.8
- version 8.0.201
## Install Package
- Microsoft.EntityFrameworkCore.Sql
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design
## Install database
- dotnet ef migrations add InitialCreate
- dotnet ef database update
## Install dotnet .8
dotnet new globaljson --sdk-version 8.0.201