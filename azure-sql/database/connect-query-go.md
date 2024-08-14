---
title: Use Go to query
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: "Quickstart: Use Go to create a program that connects to a database in Azure SQL Database or Azure SQL Managed Instance, and runs queries."
author: dlevy-msft
ms.author: dlevy
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 12/01/2023
ms.service: azure-sql
ms.subservice: connect
ms.topic: quickstart
ms.custom:
  - sqldbrb=2
  - mode-api
ms.devlang: golang
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---
# Quickstart: Use Golang to query a database in Azure SQL Database or Azure SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

In this quickstart, you'll use the Golang programming language to connect to an Azure SQL database, or a database in [!INCLUDE [ssazuremi-md](../../docs/includes/ssazuremi-md.md)], with the [go-mssqldb](https://github.com/microsoft/go-mssqldb) driver. The sample queries and modifies data with explicit Transact-SQL (T-SQL) statements. [Golang](https://go.dev/) is an open-source programming language that makes it easy to build simple, reliable, and efficient software.

## Prerequisites

To complete this quickstart, you need:

- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/free/).
- An Azure SQL database or a database in [!INCLUDE [ssazuremi-md](../../docs/includes/ssazuremi-md.md)]. You can use one of these quickstarts to create a database:

  | SQL Database | SQL Managed Instance | SQL Server on Azure VM |
  | :--- | :--- | :--- |
  | **Create** | | |
  | [Portal](single-database-create-quickstart.md) | [Portal](../managed-instance/instance-create-quickstart.md) | [Portal](../virtual-machines/windows/sql-vm-create-portal-quickstart.md) |
  | [CLI](scripts/create-and-configure-database-cli.md) | [CLI](https://medium.com/azure-sqldb-managed-instance/working-with-sql-managed-instance-using-azure-cli-611795fe0b44) |
  | [PowerShell](scripts/create-and-configure-database-powershell.md) | [PowerShell](../managed-instance/scripts/create-configure-managed-instance-powershell.md) | [PowerShell](../virtual-machines/windows/sql-vm-create-powershell-quickstart.md) |
  | **Configure** | | |
  | [Server-level IP firewall rule](firewall-create-server-level-portal-quickstart.md) | [Connectivity from a VM](../managed-instance/connect-vm-instance-configure.md) |
  | | [Connectivity from on-premises](../managed-instance/point-to-site-p2s-configure.md) | [Connect to a SQL Server instance](../virtual-machines/windows/sql-vm-create-portal-quickstart.md) |
  | **Load data** | | |
  | `AdventureWorks2022` loaded per quickstart | [Restore WideWorldImporters](../managed-instance/restore-sample-database-quickstart.md) | [Restore WideWorldImporters](../managed-instance/restore-sample-database-quickstart.md) |
  | | Restore or import `AdventureWorks2022` from a [BACPAC](database-import.md) file from [GitHub](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) | Restore or import `AdventureWorks2022` from a [BACPAC](database-import.md) file from [GitHub](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) |

  > [!IMPORTANT]  
  > The scripts in this article are written to use the `AdventureWorks2022` database. With a SQL managed instance, you must either import the `AdventureWorks2022` database into an instance database or modify the scripts in this article to use the Wide World Importers database.

- [Go](https://go.dev/doc/install) and related software for your operating system installed.

- The latest version of **[sqlcmd](/sql/tools/sqlcmd/sqlcmd-utility?tabs=go#download-and-install-sqlcmd)** for your operating system installed.

- The [Azure PowerShell Az module](/powershell/azure/install-azure-powershell) for your operating system installed.

## Get server connection information

Get the connection information you need to connect to the database. You'll need the fully qualified server name or host name, database name, and login information for the upcoming procedures.

1. Sign in to the [Azure portal](https://portal.azure.com/).

1. Navigate to the **SQL Databases**  or **SQL Managed Instances** page.

1. On the **Overview** page, review the fully qualified server name next to **Server name** for a database in Azure SQL Database or the fully qualified server name (or IP address) next to **Host** for an Azure SQL Managed Instance or SQL Server on Azure VM. To copy the server name or host name, hover over it and select the **Copy** icon.

> [!NOTE]  
> For connection information for SQL Server on Azure VM, see [Connect to a SQL Server instance](../virtual-machines/windows/sql-vm-create-portal-quickstart.md#connect-to-sql-server).

## Create a new folder for the Golang project and dependencies

1. From the terminal, create a new project folder called `SqlServerSample`.

   ```bash
   mkdir SqlServerSample
   ```

## Create sample data

1. In a text editor, create a file called `CreateTestData.sql` in the `SqlServerSample` folder. In the file, paste this T-SQL code, which creates a schema, table, and inserts a few rows.

   ```sql
   CREATE SCHEMA TestSchema;
   GO
   
   CREATE TABLE TestSchema.Employees (
       Id INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
       Name NVARCHAR(50),
       Location NVARCHAR(50)
   );
   GO
   
   INSERT INTO TestSchema.Employees (Name, Location)
   VALUES (N'Jared', N'Australia'),
       (N'Nikita', N'India'),
       (N'Astrid', N'Germany');
   GO
   
   SELECT * FROM TestSchema.Employees;
   GO
   ```

1. At the command prompt, navigate to `SqlServerSample` and use `sqlcmd` to connect to the database and run your newly created Azure SQL script. Replace the appropriate values for your server and database.

   ```bash
   az login
   sqlcmd -S <your_server>.database.windows.net -G -d <your_database> -i ./CreateTestData.sql
   ```

## Insert code to query the database

1. Create a file named `sample.go` in the `SqlServerSample` folder.

1. In the file, paste this code. Add the values for your server and database. This example uses the Golang [context methods](https://go.dev/pkg/context/) to make sure there's an active connection.

   ```go
   package main

   import (
       "github.com/microsoft/go-mssqldb/azuread"
       "database/sql"
       "context"
       "log"
       "fmt"
       "errors"
   )

   var db *sql.DB

   var server = "<your_server.database.windows.net>"
   var port = 1433
   var database = "<your_database>"

   func main() {
       // Build connection string
       connString := fmt.Sprintf("server=%s;port=%d;database=%s;fedauth=ActiveDirectoryDefault;", server, port, database)

       var err error

       // Create connection pool
           db, err = sql.Open(azuread.DriverName, connString)
       if err != nil {
           log.Fatal("Error creating connection pool: ", err.Error())
       }
       ctx := context.Background()
       err = db.PingContext(ctx)
       if err != nil {
           log.Fatal(err.Error())
       }
       fmt.Printf("Connected!\n")

       // Create employee
       createID, err := CreateEmployee("Jake", "United States")
       if err != nil {
           log.Fatal("Error creating Employee: ", err.Error())
       }
       fmt.Printf("Inserted ID: %d successfully.\n", createID)

       // Read employees
       count, err := ReadEmployees()
       if err != nil {
           log.Fatal("Error reading Employees: ", err.Error())
       }
       fmt.Printf("Read %d row(s) successfully.\n", count)

       // Update from database
       updatedRows, err := UpdateEmployee("Jake", "Poland")
       if err != nil {
           log.Fatal("Error updating Employee: ", err.Error())
       }
       fmt.Printf("Updated %d row(s) successfully.\n", updatedRows)

       // Delete from database
       deletedRows, err := DeleteEmployee("Jake")
       if err != nil {
           log.Fatal("Error deleting Employee: ", err.Error())
       }
       fmt.Printf("Deleted %d row(s) successfully.\n", deletedRows)
   }

   // CreateEmployee inserts an employee record
   func CreateEmployee(name string, location string) (int64, error) {
       ctx := context.Background()
       var err error

       if db == nil {
           err = errors.New("CreateEmployee: db is null")
           return -1, err
       }

       // Check if database is alive.
       err = db.PingContext(ctx)
       if err != nil {
           return -1, err
       }

       tsql := `
         INSERT INTO TestSchema.Employees (Name, Location) VALUES (@Name, @Location);
         select isNull(SCOPE_IDENTITY(), -1);
       `

       stmt, err := db.Prepare(tsql)
       if err != nil {
          return -1, err
       }
       defer stmt.Close()

       row := stmt.QueryRowContext(
           ctx,
           sql.Named("Name", name),
           sql.Named("Location", location))
       var newID int64
       err = row.Scan(&newID)
       if err != nil {
           return -1, err
       }

       return newID, nil
   }

   // ReadEmployees reads all employee records
   func ReadEmployees() (int, error) {
       ctx := context.Background()

       // Check if database is alive.
       err := db.PingContext(ctx)
       if err != nil {
           return -1, err
       }

       tsql := fmt.Sprintf("SELECT Id, Name, Location FROM TestSchema.Employees;")

       // Execute query
       rows, err := db.QueryContext(ctx, tsql)
       if err != nil {
           return -1, err
       }

       defer rows.Close()

       var count int

       // Iterate through the result set.
       for rows.Next() {
           var name, location string
           var id int

           // Get values from row.
           err := rows.Scan(&id, &name, &location)
           if err != nil {
               return -1, err
           }

           fmt.Printf("ID: %d, Name: %s, Location: %s\n", id, name, location)
           count++
       }

       return count, nil
   }

   // UpdateEmployee updates an employee's information
   func UpdateEmployee(name string, location string) (int64, error) {
       ctx := context.Background()

       // Check if database is alive.
       err := db.PingContext(ctx)
       if err != nil {
           return -1, err
       }

       tsql := fmt.Sprintf("UPDATE TestSchema.Employees SET Location = @Location WHERE Name = @Name")

       // Execute non-query with named parameters
       result, err := db.ExecContext(
           ctx,
           tsql,
           sql.Named("Location", location),
           sql.Named("Name", name))
       if err != nil {
           return -1, err
       }

       return result.RowsAffected()
   }

   // DeleteEmployee deletes an employee from the database
   func DeleteEmployee(name string) (int64, error) {
       ctx := context.Background()

       // Check if database is alive.
       err := db.PingContext(ctx)
       if err != nil {
           return -1, err
       }

       tsql := fmt.Sprintf("DELETE FROM TestSchema.Employees WHERE Name = @Name;")

       // Execute non-query with named parameters
       result, err := db.ExecContext(ctx, tsql, sql.Named("Name", name))
       if err != nil {
           return -1, err
       }

       return result.RowsAffected()
   }
   ```

## Get Golang project dependencies and run the code

1. At the command prompt, navigate to `SqlServerSample` and install the SQL Server driver for Go by running the following commands.

   ```bash
   go mod init SqlServerSample
   go mod tidy
   ```

1. At the command prompt, run the following command.

   ```bash
   az login
   go run sample.go
   ```

1. Verify the output.

   ```output
   Connected!
   Inserted ID: 4 successfully.
   ID: 1, Name: Jared, Location: Australia
   ID: 2, Name: Nikita, Location: India
   ID: 3, Name: Astrid, Location: Germany
   ID: 4, Name: Jake, Location: United States
   Read 4 row(s) successfully.
   Updated 1 row(s) successfully.
   Deleted 1 row(s) successfully.
   ```

## Related content

- [Tutorial: Design a relational database in Azure SQL Database using SSMS](design-first-database-tutorial.md)
- [Golang driver for SQL Server](https://github.com/microsoft/go-mssqldb)
- [Report issues or ask questions](https://github.com/microsoft/go-mssqldb/issues)
