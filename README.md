# StudentExams - Database Configuration Guide

## Overview
This application uses **MySQL** database to store and manage student examination marks. The application is built with **.NET 10** and performs CRUD operations (Create, Read, Update, Delete) on student marks with an intuitive user interface.

## Database Requirements

### Prerequisites
- **MySQL Server** (version 5.7 or higher recommended)
- **MySQL.Data** NuGet package (already included in the project)
- **Visual Studio 2026** or compatible IDE

## Database Setup

### 1. Install MySQL Server

You dont need to install the MySQL Server database if you are using your Virtual Labs. This step is optional for those who want to run this on their own laptop

If you haven''t already, download and install MySQL Server from [https://dev.mysql.com/downloads/installer/](https://dev.mysql.com/downloads/installer/)

Download MySQL Workbench as well

[https://dev.mysql.com/downloads/workbench/](https://dev.mysql.com/downloads/workbench/)

During installation, remember your:
- **Root username** (usually `root`)
- **Root password**
- **Port number** (default is `3306`)

### 2. Create the Database and Tables

Execute the following SQL script to set up the complete database structure:

```sql
-- Create database if it doesn''t exist
CREATE DATABASE IF NOT EXISTS grp3_exam_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

-- Use the database
USE grp3_exam_db;

-- Create marks table if it doesn''t exist
CREATE TABLE IF NOT EXISTS marks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    StudentNumber VARCHAR(50) NOT NULL,
    Mark INT NOT NULL,
    Grade VARCHAR(5) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_student_number (StudentNumber),
    CONSTRAINT chk_mark_range CHECK (Mark >= 0 AND Mark <= 100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Display confirmation message
SELECT ''Database and tables created successfully!'' AS Status;

-- Display table structure
DESCRIBE marks;

-- Optional: Insert sample data (uncomment if you want sample data)
INSERT INTO marks (StudentNumber, Mark, Grade) VALUES
    (''ST001'', 85, ''A''),
    (''ST002'', 72, ''B''),
    (''ST003'', 55, ''C''),
    (''ST004'', 45, ''F''),
    (''ST005'', 90, ''A'')
    ON DUPLICATE KEY UPDATE StudentNumber = StudentNumber;
```

### 3. Configure Connection String

**IMPORTANT**: Before running the application, you **MUST** update the database password in the code.

#### Steps to Update Password:

1. Open the file: `StudentExams\Examination.cs`
2. Locate the `connString` variable at the top of the `Examination` class (line 10):
   ```csharp
   private string connString = "Server=localhost;Database=grp2_exam_db; Uid=root; Pwd=yourpassword;";
   ```
3. Replace `yourpassword` with your actual MySQL root password
4. Update the database name from `grp2_exam_db` to `grp3_exam_db` to match the database you created

**Example:**
```csharp
private string connString = "Server=localhost;Database=grp3_exam_db; Uid=root; Pwd=MySecurePassword123;";
```

#### Connection String Parameters:
- `Server` - MySQL server address (default: `localhost`)
- `Database` - Database name (should be `grp3_exam_db`)
- `Uid` - MySQL username (default: `root`)
- `Pwd` - **Your MySQL password (MUST BE CHANGED)**
- `Port` - MySQL port (default: `3306`, optional parameter)

## Database Schema

### Marks Table Structure

| Column | Type | Description | Constraints |
|--------|------|-------------|-------------|
| `Id` | INT | Primary key, auto-incrementing | PRIMARY KEY, AUTO_INCREMENT |
| `StudentNumber` | VARCHAR(50) | Unique identifier for student | NOT NULL, INDEXED |
| `Mark` | INT | Numerical score (0-100) | NOT NULL, CHECK (0-100) |
| `Grade` | VARCHAR(5) | Letter grade (A, B, C, D, F) | NOT NULL |
| `CreatedAt` | TIMESTAMP | Record creation timestamp | DEFAULT CURRENT_TIMESTAMP |
| `UpdatedAt` | TIMESTAMP | Record update timestamp | ON UPDATE CURRENT_TIMESTAMP |

### Grading Scale
The application uses the following grading scale:
- **A**: 75-100
- **B**: 65-74
- **C**: 50-64
- **D**: 40-49
- **F**: 0-39

## Running the Application

1. Ensure MySQL Server is running
2. Verify the database `grp3_exam_db` exists and the `marks` table is created
3. Configure the connection string in your application
4. Build and run the application from Visual Studio
5. Use the interface to:
   - Add new student marks
   - View all student records
   - Update existing marks
   - Delete records
   - Calculate grades automatically

## Troubleshooting

### Common Issues

**Connection Failed**: Verify MySQL service is running and credentials are correct
```powershell
# Check MySQL service status (Windows)
sc query MySQL

# Restart MySQL service (Windows)
net stop MySQL
net start MySQL
```

**Access Denied**: Ensure the MySQL user has proper permissions
```sql
GRANT ALL PRIVILEGES ON grp3_exam_db.* TO ''root''@''localhost'';
FLUSH PRIVILEGES;
```

**Table Not Found**: Re-run the database creation script

**Port Already in Use**: Check if another service is using port 3306 or change MySQL port

## Technology Stack

- **.NET 10** - Application framework
- **MySQL** - Database server
- **MySQL.Data** - MySQL connector for .NET
- **Visual Studio 2026** - Development IDE

## Features

- ? CRUD Operations for student marks
- ? Automatic grade calculation
- ? Data validation (marks 0-100)
- ? Indexed lookups for performance
- ? Timestamp tracking for audit trail
- ? UTF-8 character support

## Support

For issues or questions, please contact the development team.
