---
title: "Quickstart: Run Your First Query"
titleSuffix: MSSQL Extension for Visual Studio Code
description: Use the MSSQL extension for Visual Studio Code to connect to an existing database or create one, add a sample table, and run your first Transact-SQL query.
author: cwebb-msft
ms.author: chweb
ms.reviewer: roblescarlos
ms.date: 04/23/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: quickstart
ai-usage: ai-assisted
ms.collection:
  - data-tools
ms.custom:
  - sfi-image-nochange
  - ignite-2025
---

# Quickstart: Run your first query with the MSSQL extension for Visual Studio Code

In this quickstart, you use the MSSQL extension for Visual Studio Code to run your first Transact-SQL (T-SQL) statements against a database. Use an existing development database or create one, define a table, insert sample data, and query the results.

## Prerequisites

To complete this quickstart, you need:

- [Visual Studio Code](https://code.visualstudio.com/).
- The [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md).
- Access to a development database in Azure SQL Database, SQL database in Fabric, Azure SQL Managed Instance, or SQL Server, with permission to create tables and modify sample data.

If you already have a development database, reuse it. Otherwise, choose a setup path:

- **Azure SQL Database free tier**: [Create a database in the MSSQL extension](mssql-azure-integration.md).
- **Microsoft Fabric (SaaS)**: [Create a SQL database in your Fabric workspace](/fabric/database/sql/tutorial-create-database).
- **SQL Server container**: [Run SQL Server in Docker](../../../linux/install-upgrade/quickstart-install-docker.md).
- **SQL Server locally or on-premises**: [Install SQL Server Developer Edition](https://www.microsoft.com/sql-server/sql-server-downloads).

## Connect to your database

Before you run a query, connect to the database from the **Object Explorer**. The Connection Dialog walks you through entering a server name, picking an authentication type, and (optionally) selecting a database. For a full walkthrough of input types, authentication methods, and connection management, see [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md).

After you connect, your server appears in the Object Explorer and you can start running queries.

### Use an existing database

If you already provisioned an Azure SQL Database free tier database, connect directly to that database. In the Connection Dialog, select or enter its name in the **Database** field rather than leaving the default database selected. You can also use this path for another existing development database, including a SQL database in Fabric.

1. Open a new query editor connected to your chosen database.
1. Run this query to check the editor's active database:

   ```sql
   SELECT DB_NAME() AS current_database;
   ```

1. Confirm that the result matches the database you intend to use. If it doesn't, select the correct database connection and check again before running any sample statements.
1. Skip [Create a database](#create-a-database) and continue with [Create a table](#create-a-table). Keep the same database connection for the remaining steps.

> [!IMPORTANT]
> The following `CREATE DATABASE Library` statement doesn't provision a database under the Azure SQL Database free tier. Don't run it for the free tier path. Create the tutorial table inside the free database you already provisioned; don't restore or import another database.

## Create a database

If you need a new database on SQL Server or Azure SQL Managed Instance, create a database called `Library`. If you already have a development database, use [Use an existing database](#use-an-existing-database) instead.

> [!IMPORTANT]
> This step creates a generic database, not a free tier database. Adding another cloud database can incur charges. Review the service's pricing and your permissions before continuing.

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

Create the `Authors` table within your existing development database or the `Library` database you created.

> [!IMPORTANT]
> Run each step separately and stop if a statement fails. If `dbo.Authors` already exists, don't use or delete it for this quickstart. Choose a different development database or change the table name consistently in all sample statements before continuing.

1. Make sure the query editor is connected to your chosen database. For the free tier path, use the free database's name, not `Library` or `master`. Run `SELECT DB_NAME() AS current_database;` again if you open another editor or change connections.

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
FROM dbo.Authors
ORDER BY id;
```

After one successful insert into the new table, expect five rows in this order:

| id | first_name | middle_name | last_name |
| --- | --- | --- | --- |
| 1 | Isaac | Yudovick | Asimov |
| 2 | Arthur | Charles | Clarke |
| 3 | Herbert | George | Wells |
| 4 | Jules | Gabriel | Verne |
| 5 | Philip | Kindred | Dick |

## Clean up resources

Remove only the sample table you created in this quickstart.

1. Run `SELECT DB_NAME() AS current_database;` and confirm that you're connected to the database where you created the table.
1. Confirm that you created `dbo.Authors` during this walkthrough and no longer need its sample data. Don't continue if table creation failed or the table belongs to another project.
1. Run the following statement to remove that table:

   ```sql
   DROP TABLE dbo.Authors;
   ```

Don't drop an existing database, including the free tier database you provisioned, as part of this sample cleanup.

If you use the free tier, [track your remaining allowance](/azure/azure-sql/database/free-tier#track-free-usage) and review the [pause and overage behavior](/azure/azure-sql/database/free-tier#considerations-and-limitations). Disconnect query editors and the Object Explorer connection when you're done. Open sessions can prevent idle auto-pause; disconnecting doesn't restore vCore seconds already consumed. A successful query doesn't establish workload compatibility or guarantee that future usage stays within the free allowance.


## Related content

- Explore the [Azure SQL Database container](https://microsoft.github.io/azure-sql-database-container/getting-started.html) for local development (private preview). You need preview access. Review the [GUI tooling limitations](https://microsoft.github.io/azure-sql-database-container/known-limitations.html#4-gui-tooling-compatibility-mssql-extension-and-ssms) before using the MSSQL extension.
- [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md)
- [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md)
- [Tutorial: Write Transact-SQL statements](../../../t-sql/tutorial-writing-transact-sql-statements.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
