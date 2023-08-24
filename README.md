# Restaurant API Project

This project is an ASP.NET Core 3.1 Web API application designed to manage a restaurant's menu items, categories, tables, and reservations.

## Technologies and Principles Used

- ASP.NET Core 3.1 Web API
- Entity Framework Core
- Serilog
- Seq
- Onion Architecture
- JwtBearer Authentication Token
- Refresh Token
- Microsoft Identity
- Design following SOLID Principles
- AutoMapper
- Generic Custom Exception Handling
- Repository Pattern
- Unit of Work Pattern
- Generic Response Models
- Fluent Validation
- Soft Delete
- Global Exception Handler Middleware
- Cross-Origin Resource Sharing (CORS) Policies

## Project Objectives

This project aims to achieve the following objectives:

- Provide users with the ability to view restaurant menu items and categories.
- Display the status of existing tables (occupied, vacant, or reserved) to users.
- Allow unregistered users to view menu items, categories, and table statuses.
- Enable registered users to make reservations.
- Admin users can approve or reject reservations.
- Admin users can manage menu items, categories, tables, and other details.

## Installation

1. Clone this repository to your local machine.
2. Install the necessary dependencies (dotnet restore).
3. Create the database and seed sample data (dotnet ef database update and dotnet run seed).
4. Start the application by running the following command (dotnet run).
5. The API will start running at `https://localhost:7085`.

## API Documentation

For access and usage details of the API, refer to the [API Documentation](https://github.com/Ibbocs/RestaurantFinalAPI/wiki) page.

---

## Contact

For more information, questions, or if you'd like to contribute to the project, you can contact us through the following methods:

- Email: [ibrahusey0@gmail.com](mailto:ibrahusey0@gmail.com)
- LinkedIn: [İbrahim Huseynov](https://www.linkedin.com/in/ibrahim-huseynov)

We'd be happy to connect with you for any inquiries! Feel free to reach out to collaborate or learn more about our project.
