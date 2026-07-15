---
title: "Quickstart: Run Your First Query"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Use the MSSQL extension for Visual Studio Code to create a database and table, insert data, and run your first Transact-SQL query.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: roblescarlos
ms.date: 04/23/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: quickstart
ms.collection:
  - data-tools
ms.custom:
  - sfi-image-nochange
  - ignite-2025
---

# Quickstart: Run your first query with the MSSQL extension for Visual Studio Code

In this quickstart, you use the MSSQL extension for Visual Studio Code to run your first Transact-SQL (T-SQL) statements against a database. You create a database, define a table, insert data, and query the results.

## Prerequisites

To complete this quickstart, you must have:

- **Visual Studio Code**: If Visual Studio Code isn't installed, download it from the [official Visual Studio Code website](https://code.visualstudio.com/).

- **MSSQL extension for Visual Studio Code**: In Visual Studio Code, open the Extensions view by selecting the Extensions icon in the Activity Bar on the side of the window. Search for `mssql` and select **Install** to add the extension.

- **Access to a database**: If you don't have access to a database instance, you can use one by selecting one of the following options:

  - **Containerized SQL Server**: Run SQL Server in a Docker container for easy setup and portability. For more information, see [Quickstart: Run SQL Server Linux container images with Docker](../../../linux/quickstart-install-connect-docker.md).

  - **Azure SQL Database**: If you prefer a cloud-based option, create a free Azure account and set up an Azure SQL Database. For more information, see [Quickstart: Create a single database - Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart).

  - **SQL database in Microsoft Fabric**: If you need an autonomous, secure database optimized for AI, create a SQL database in Microsoft Fabric. For more information, see [Create a SQL database in Microsoft Fabric](/fabric/database/sql/tutorial-create-database).

  - **Local SQL Server**: Alternatively, download and install [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] Developer Edition on your local machine. For more information, see [Microsoft SQL Server website](https://www.microsoft.com/sql-server/sql-server-downloads).

  - **Azure SQL Managed Instance**: If you need a fully managed SQL Server instance, create an Azure SQL Managed Instance. For more information, see [Quickstart: Create Azure SQL Managed Instance](/azure/azure-sql/managed-instance/instance-create-quickstart).

> [!NOTE]  
> If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).

## Connect to your database

Before you run a query, connect to the database from the **Object Explorer**. The Connection Dialog walks you through entering a server name, picking an authentication type, and (optionally) selecting a database. For a full walkthrough of input types, authentication methods, and connection management, see [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md).

After you connect, your server appears in the Object Explorer and you can start running queries.

## Create a database

Create a database called `Library`.

1. Open a new query editor: Press **Ctrl**+**N** to open a new query editor, or right-click on your server and select **New Query**.

1. Create the database. Paste the following snippet into the query editor and select **Run**:

   ```sql
   IF NOT EXISTS (SELECT name
                  FROM sys.databases
                  WHERE name = N'Library')
       CREATE DATABASE Library;
   ```

   This script creates a new database called `Library` if it doesn't already exist.

The new `Library` database appears in the list of databases. If you don't see it immediately, refresh the Object Explorer.

## Create a table

Create the `Authors` table within the `Library` database.

1. Open a new query editor and make sure the connection context is set to the `Library` database.

1. Create the table. Replace the text in the query window with the following snippet and select **Run**:

   ```sql
   CREATE TABLE dbo.Authors
   (
       id INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
       first_name NVARCHAR (100) NOT NULL,
       middle_name NVARCHAR (100) NULL,
       last_name NVARCHAR (100) NOT NULL
   );
   ```

This script creates the `Authors` table with an `IDENTITY` column for the `id`, which automatically generates unique IDs.

## Insert rows

Next, insert data into the `Authors` table.

1. Replace the text in the query window with the following snippet and select **Run**:

   ```sql
   INSERT INTO dbo.Authors (first_name, middle_name, last_name)
   VALUES ('Isaac', 'Yudovick', 'Asimov'),
          ('Arthur', 'Charles', 'Clarke'),
          ('Herbert', 'George', 'Wells'),
          ('Jules', 'Gabriel', 'Verne'),
          ('Philip', 'Kindred', 'Dick');
   ```

The sample data is added to the `Authors` table.

## View the data

To verify the data in the `Authors` table, run the following query:

```sql
SELECT *
FROM dbo.Authors;
```

This query returns all records in the `Authors` table, showing the data you inserted.

## Related content

- [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md)
- [What is the MSSQL extension for Visual Studio Code?](mssql-extension-visual-studio-code.md)
- [Tutorial: Write Transact-SQL statements](../../../t-sql/tutorial-writing-transact-sql-statements.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
