# BackEnd Web API for a School registration system  Web application 

### Introduction
For this project, 
create web api using .net core 
data modeling using  EntityFrameworkCore
using Jwt Tokens for scure login 


### Dependencies
.net core 3.1
database : MSSQL

### Getting Started
1. Clone this repository
2. change database connection string in "appsettings.json" file 
2. Excute Add-migration
3. Excute update-database
5. to avoid Cors restriction, you can change frontend url in "Startup.cs" in services.AddCors


### Description  
- For initial using, you can using following users detail:
        HR email = "hr@school.com";
        Hr password = "Abc@123*";
        staf email = "staf@school.com";
        staf password = "Abc@123*";

- you can do more customization for initial data by modifing "Contexts\ApplicationDbContextSeed"

- system by defult will set new register user as Student Role and his status to pending 

- Hr user can accept or reject the registered user 

- user with staf role can veiw all user

- user with student role can not veiw all user
