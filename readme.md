# Account Service

📌 **Project Overview**

Account Service is a microservice designed for robust authentication and user management. It provides essential functionalities such as user registration, login, email confirmation, JWT and refresh token management, password changes/resets, and user information updates.

**Key Features:**

* **User Registration (Register):** Seamless user onboarding.
* **User Login (Login):** Secure authentication process.
* **Email Confirmation:** Verification of user email addresses.
* **JWT and Refresh Token Management:** Secure and efficient token-based authentication.
* **Change/Reset Password:** User-friendly password management.
* **Update User Information:** Ability to modify user details.

---

🏗️ **Architecture**

This service adopts a **Vertical Slice Architecture** combined with **CQRS (Command Query Responsibility Segregation)** to enhance maintainability and scalability.


📂 AccountService  
 ┣ 📂 Application (Business logic & CQRS commands)  
 ┃ ┣ 📂 Features (Each operation as a separate slice)  
 ┃ ┃ ┣ 📂 Users (User-related operations)  
 ┃ ┃ ┃ ┣ 📂 Commands (Register, Login, Update, Password Reset, etc.)  
 ┃ ┃ ┃ ┣ 📂 Queries (Fetching user details, etc.)  
 ┃ ┃ ┃ ┗ 📂 Validators (FluentValidation for input validation)  
 ┃ ┣ 📂 Common (Global utilities - JWT, hashing, etc.)  
 ┣ 📂 Infrastructure (Database & external dependencies)  
 ┃ ┣ 📂 Persistence (MSSQL, Entity Framework operations)  
 ┃ ┣ 📂 Authentication (JWT, Refresh Token, Identity management)  
 ┣ 📂 API (Controllers)  
 ┣ 📂 Domain (Core entities - e.g., User Entity)  

---

🚀 **Technologies Used**

* **ASP.NET Core Web API:** Building the RESTful API.
* **Entity Framework Core (MSSQL):** Object-relational mapping for database interactions.
* **MediatR (for CQRS pattern):** Implementing the CQRS pattern for clean separation of concerns.
* **FluentValidation (for input validation):** Ensuring data integrity through robust validation.
* **JWT + Refresh Token (Authentication):** Secure token-based authentication and authorization.

---

⚙️ **Installation & Setup**

**Steps to get the service running:**

1.  **Install Dependencies:**

    ```bash
    dotnet restore
    ```

2.  **Apply Database Migrations:**

    ```bash
    dotnet ef database update
    ```

3.  **Run the Service:**

    ```bash
    dotnet run
    ```

---

🔗 **API Endpoints**

**🟢 Authentication:**

| HTTP Method | Endpoint                 | Description                                    |
| :---------- | :----------------------- | :--------------------------------------------- |
| POST        | `/api/auth/register`      | Registers a new user.                               |
| POST        | `/api/auth/login`         | Authenticates a user and returns JWT.               |
| POST        | `/api/auth/refresh-token` | Generates a new JWT using a refresh token.          |
| POST        | `/api/auth/confirm-email` | Confirms user email address.                          |
| POST        | `/api/auth/change-password`| Changes user password.                                |

---



📜 **License**

This project is licensed under the **MIT License**.
