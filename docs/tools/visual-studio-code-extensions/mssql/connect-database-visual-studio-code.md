---
title: "Quickstart: Connect to and Query a Database with the MSSQL Extension for Visual Studio Code"
description: Learn how to connect to a database using the MSSQL extension for Visual Studio Code, and execute Transact-SQL (T-SQL) statements to interact with your database.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: roblescarlos
ms.date: 02/21/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: quickstart
ms.collection:
  - data-tools
ms.custom:
  - sfi-image-nochange
  - ignite-2025
---

# Quickstart: Connect to and query a database with the MSSQL extension for Visual Studio Code

In this quickstart, you learn how to use the MSSQL extension for Visual Studio Code to connect to a database, whether it's running locally, in a container, or in the cloud. Then you learn how to use Transact-SQL (T-SQL) statements to create a database, define a table, insert data, and query results.

## Prerequisites

To complete this quickstart, you must have:

- **Visual Studio Code**: If you don't have Visual Studio Code installed, download and install it from the [official Visual Studio Code website](https://code.visualstudio.com/).

- **MSSQL extension for Visual Studio Code**: In Visual Studio Code, open the Extensions view by selecting the Extensions icon in the Activity Bar on the side of the window. Search for `mssql` and select **Install** to add the extension.

- **Access to a database**: If you don't have access to a database instance, you can use one by selecting one of the following options:

  - **Containerized SQL Server**: Run SQL Server in a Docker container for easy setup and portability. For more information, see [Quickstart: Run SQL Server Linux container images with Docker](../../../linux/quickstart-install-connect-docker.md).

  - **Azure SQL Database**: If you prefer a cloud-based option, create a free Azure account and set up an Azure SQL Database. For more information, see [Quickstart: Create a single database - Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart).

  - **SQL database in Fabric**: If you need a simple, autonomous, and secure, and optimized for AI database, create a SQL database in Fabric. For more information, see [Create a SQL database in Microsoft Fabric](/fabric/database/sql/tutorial-create-database).

  - **Local SQL Server**: Alternatively, download and install SQL Server 2022 Developer Edition on your local machine. For more information, see [Microsoft SQL Server website](https://www.microsoft.com/sql-server/sql-server-downloads).

  - **Azure SQL Managed Instance**: If you need a fully managed SQL Server instance, create an Azure SQL Managed Instance. For more information, see [Quickstart: Create Azure SQL Managed Instance](/azure/azure-sql/managed-instance/instance-create-quickstart).

> [!NOTE]  
> If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).

## Connect to a database instance

1. **Start Visual Studio Code**: Open the MSSQL extension for Visual Studio Code by selecting the server viewlet on the left side of the window, or use the **Ctrl**+**Alt**+**D** keyboard shortcut.

   The first time you run the MSSQL extension for Visual Studio Code, the **Enable Experiences and Reload** button appears when the extension is loaded for the first time.

1. **Connect to database**:

   This article uses the **Parameter** input type and **SQL Login** for the authentication type.

   Follow the prompts to specify the properties for the new connection profile. Complete each field as follows:

   | Connection property | Value | Description |
   | --- | --- | --- |
   | **Profile Name** (optional) | Leave this field blank. | Type a name for the connection profile, such as `localhost profile`. |
   | **Connection Group** (optional) | Leave this field blank or select an existing group. | Organize this connection under a group folder for easier management. |
   | **Server name** | Enter the server name here. For example, `localhost` | Specify the SQL Server instance name. Use `localhost` to connect to a SQL Server instance on your local machine. To connect to a remote SQL Server, enter the name of the target SQL Server, or its IP address. To connect to a SQL Server container, specify the IP address of the container's host machine. If you need to specify a port, use a comma to separate it from the name. For example, for a server listening on port 1401, enter `<servername or IP>,1401`.<br /><br />By default, the connection string uses port 1433. A default instance of SQL Server uses 1433 unless modified. If your instance is listening on 1433, you don't need to specify the port.<br />As an alternative, you can enter the ADO connection string for your database here. |
   | **Trust Server Certificate** | Check this field. | Select this option to trust the server certificate. |
   | **Input type** | Parameter. | Choose from **Parameter**, **Connection String**, or **Browse Azure**. |
   | **Database name** (optional) | \<Default>. | The database that you want to use. To connect to the default database, don't specify a database name here. |
   | **Authentication Type** | SQL login. | Choose either **SQL Login**, **Windows Authentication**, or **Microsoft Entra ID**. |
   | **User name** | Enter your *\<username\>* for the SQL Server. | If you selected **SQL Login**, enter the name of a user with access to a database on the server. |
   | **Password** | Enter your *\<password\>* for the SQL Server. | Enter the password for the specified user. |
   | **Save Password** | Check this field to save the password for future connections. | Press **Enter** to select **Yes** and save the password. Select **No** to be prompted for the password each time the connection profile is used. |
   | **Encrypt** | **Mandatory**. | Choose from **Yes**, **No**, or **Mandatory**. |

   After you enter all values and select **Enter**, Visual Studio Code creates the connection profile and connects to the SQL Server.

   :::image type="content" source="media/connect-database-visual-studio-code/mssql-connection-dialog-parameters.png" alt-text="Screenshot of the Connection Dialog window." lightbox="media/connect-database-visual-studio-code/mssql-connection-dialog-parameters.png":::

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

- [What is the MSSQL extension for Visual Studio Code?](mssql-extension-visual-studio-code.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Tutorial: Write Transact-SQL statements](../../../t-sql/tutorial-writing-transact-sql-statements.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
