# GamesListApp API

## Overview

GamesListApp is a monolithic API built using **ASP.NET Core** with **Entity Framework Core** and **SQL Server** as the database. The API includes Swagger for testing and documentation.

---

## Project Structure

### Models
- **Game**: Represents a game.
- **Category**: Represents a category that games can belong to.
- **User**: Represents a user of the system.
- **Review**: Represents user reviews for games.
- **GameCategory**: Handles the many-to-many relationship between games and categories.
- **GameUser**: Handles the many-to-many relationship between games and users.

### Data Layer
- **DataContext**: 
  - Acts as the bridge between the application and the database.
  - Configures the relationships:
    - Many-to-Many: `GameCategory` and `GameUser`.
    - One-to-Many: `Reviews` for both `Games` and `Users`.

---

## Database Design

### Tables

| Table Name       | Description                                                  |
|------------------|--------------------------------------------------------------|
| `Games`          | Stores information about games.                              |
| `Categories`     | Stores information about game categories.                    |
| `Users`          | Stores information about users of the application.           |
| `Reviews`        | Stores user reviews for specific games.                      |
| `GameCategories` | Junction table for many-to-many relationship between games and categories. |
| `GameUsers`      | Junction table for many-to-many relationship between games and users. |

### Relationships
- **Games ↔ Categories**: Many-to-Many through `GameCategories`.
- **Games ↔ Users**: Many-to-Many through `GameUsers`.
- **Games ↔ Reviews**: One-to-Many.
- **Users ↔ Reviews**: One-to-Many.
