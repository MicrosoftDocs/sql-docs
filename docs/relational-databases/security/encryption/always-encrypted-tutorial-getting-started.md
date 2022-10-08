---
title: "Tutorial: Getting started with Always Encrypted"
description: This tutorial teaches you how to create a basic environment for Always Encrypted.
ms.custom:
ms.date: 11/15/2022
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: vanto
ms.suite: "sql"
ms.technology: security
ms.tgt_pltfrm: ""
ms.topic: tutorial
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15"
---
# Tutorial: Getting started with Always Encrypted

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This tutorial teaches you how to get started with [Always Encrypted](always-encrypted-database-engine.md). It will show you:

> [!div class="checklist"]
> - How to encrypt selected columns in your database.
> - How to query encrypted columns.

## Prerequisites

For this tutorial, you need:

- An **empty** database in Azure SQL Database, Azure SQL Managed Instance, or SQL Server. The below instructions assume the database name is **ContosoHR**. You need to be an owner of the database (a member of the **db_owner** role). See [Quickstart: Create a single database - Azure SQL Database](../../../../azure-sql/database/single-database-create-quickstart.md) or [Create a database in SQL Server](../../databases/create-a-database.md).
- The latest version of [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md) or the latest version of the [SqlServer PowerShell module](../../../powershell/download-sql-server-ps-module.md).
- Optional, but recommended especially if your database is in Azure: a key vault in Azure Key Vault. See [Quickstart: Create a key vault using the Azure portal](https://learn.microsoft.com/azure/key-vault/general/quick-create-portal). 

## Step 1: Create and populate the database schema

# [SSMS](#tab/ssms)

1. Connect to your database. For instructions on how to connect to a database from SSMS, see [Quickstart: Connect and query an Azure SQL Database or an Azure Managed Instance using SQL Server Management Studio (SSMS)](../../../ssms/quickstarts/ssms-connect-query-azure-sql.md) or [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](../../../ssms/quickstarts/ssms-connect-query-sql-server.md).
1. Open a new query window for the **ContosoHR** database.
1. Paste in and execute the below statements to create a new table, named **Employees**.

    ```sql
    USE [ContosoHR];
    GO

    CREATE SCHEMA [HR];
    GO
    
    CREATE TABLE [HR].[Employees]
    (
        [EmployeeID] [int] IDENTITY(1,1) NOT NULL,
        [SSN] [char](11) NOT NULL,
        [FirstName] [nvarchar](50) NOT NULL,
        [LastName] [nvarchar](50) NOT NULL,
        [Salary] [money] NOT NULL
    ) ON [PRIMARY];
    ```

1. Paste in and execute the below statements to add a few employee records to the **Employees** table.

    ```sql
    INSERT INTO [HR].[Employees]
            ([SSN]
            ,[FirstName]
            ,[LastName]
            ,[Salary])
        VALUES
            ('795-73-9838'
            , N'Catherine'
            , N'Abel'
            , $31692);

    INSERT INTO [HR].[Employees]
            ([SSN]
            ,[FirstName]
            ,[LastName]
            ,[Salary])
        VALUES
            ('990-00-6818'
            , N'Kim'
            , N'Abercrombie'
            , $55415);
    ```

# [PowerShell](#tab/powershell)

In a PowerShell session, execute the following commands.


```powershell
Import-Module "SqlServer"

# Set your database connection string
$connectionString = "Data Source = .; Initial Catalog = ContosoHRT; Integrated Security = true";

# Create a new table, named Employees.
$query = @'
CREATE SCHEMA [HR];
GO

CREATE TABLE [HR].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[SSN] [char](11) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Salary] [money] NOT NULL
	PRIMARY KEY CLUSTERED ([EmployeeID] ASC) ON [PRIMARY] );
GO
'@
Invoke-SqlCmd -ConnectionString $connectionString -Query $query

#Add a few rows to the Employees table.
$query = @'
INSERT INTO [HR].[Employees]
            ([SSN]
            ,[FirstName]
            ,[LastName]
            ,[Salary])
        VALUES
            ('795-73-9838'
            , N'Catherine'
            , N'Abel'
            , $31692);

    INSERT INTO [HR].[Employees]
            ([SSN]
            ,[FirstName]
            ,[LastName]
            ,[Salary])
        VALUES
            ('990-00-6818'
            , N'Kim'
            , N'Abercrombie'
            , $55415);
'@
Invoke-SqlCmd -ConnectionString $connectionString -Query $query
```

---

