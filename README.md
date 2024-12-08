# Card Management System

## 📋 Project Overview
This project is a **Card Management System** built using .NET 9, Entity Framework Core, and PostgreSQL. The system includes functionalities to manage card types, seed initial data, and implement CRUD operations for card entities. The task includes adhering to best practices and layered clean architecture for a coding interview assignment.

---

## 🔑️ Key Features and Assumptions
1. **Seeding Initial Data:**
   - Two default card types, `Air` and `Water`, are seeded into the database using `HasData` in Entity Framework Core.
   - Static GUIDs and UTC timestamps are used for deterministic and reproducible migrations.

2. **Database Context:**
   - PostgreSQL is used as the database engine.
   - Connection strings are provided in `appsettings.json`.

3. **Date Handling:**
   - All date and time values are stored in UTC format to ensure consistency across time zones.
   - `timestamp with time zone` is used for storing datetime values in PostgreSQL.

4. **Error Handling:**
   - Comprehensive error messages are provided for database connection issues, validation errors, and other runtime exceptions.

5. **Testing:**
   - Unit tests are written using TDD principles to validate the functionality of controllers and services.

6. **Identity Server Integration:**
   - IdentityServer4 is used for authentication and authorization, enabling secure user management and token-based access.
   - For larger-scale applications, IdentityServer can be extracted into a separate **Auth API** to act as a dedicated authentication and authorization service.

7. **DTO and Object Mapping**:
   - API endpoints use DTOs for request validation and response structuring, while AutoMapper simplifies transformations between DTOs and domain models.

---

## 💡 Design Patterns Used
1. **Repository Pattern:**
   - Centralizes **CRUD operations** in a single location, ensuring that all data access logic is consistent and reusable.
   - Simplifies the management of database interactions by providing a unified interface for all CRUD operations.
   - Combined with the `UnitOfWork` pattern, it ensures better transaction handling and separation of concerns.
   
2. **Unit of Work Pattern:**
   - Entity Framework Core executes a separate transaction for each `SaveChanges` call, which may lead to inconsistent states if multiple operations are performed in sequence.
   - To address this, the **Unit of Work** pattern is used to group related operations into a single transaction, ensuring that either all operations succeed or none of them are applied.
   - **Example Usage:** While `Unit of Work` was initially demonstrated with an update method, but a better use case involves managing multiple operations, such as adding and updating entities in sequence.


3. **Dependency Injection:**
   - All services and DbContext are injected using the built-in DI container in .NET
   - Example: `ICardService`, `IUnitOfWork`.

4. **Layered Architecture:**
   - The project is organized into the following layers:
     - **Presentation Layer:** API endpoints (Controllers).
     - **Business Layer:** Application logic (Services).
     - **Data Access Layer:** Repositories, EF Core configurations, migrations.
     - **Domain Layer:** Core entities, enums, and interfaces.
     - **Test Layer:** Contains unit tests.
     - **Infrastructure Layer:** Provides cross-cutting utilities and external integrations (JwtHelper, WorkContext).
	 

5. **Exception Handling Middleware:**
   - A global exception handling middleware ensures consistent error responses.


