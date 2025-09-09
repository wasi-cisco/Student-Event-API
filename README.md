# Student Event Management System

## Overview
A web-based API built with ASP.NET Core and Entity Framework Core that allows:
- Admins to create, update, delete, and search events
- Students to register for events
- Students to provide feedback (after the event date)

## Technologies
- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Swagger UI for API testing
- Visual Studio Code

## Setup Instructions
1. Clone the repository:
   ```bash
   git clone https://github.com/<YourUsername>/StudentEvents.git
   cd StudentEvents
   
2. Update your connection string in appsettings.json.
3. Run database migrations:
    dotnet ef database update
4.Start the API:
 dotnet run

5.Open Swagger UI:
http://localhost:5058/swagger
