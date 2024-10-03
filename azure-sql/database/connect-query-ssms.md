---
title: "SSMS: Connect and query data"
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn how to connect to Azure SQL Database or SQL Managed Instance using SQL Server Management Studio (SSMS). Then run Transact-SQL (T-SQL) statements to query and edit data.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: wiassaf, mathoma
ms.date: 09/27/2024
ms.service: azure-sql
ms.subservice: connect
ms.topic: quickstart
ms.custom:
  - sqldbrb=2
  - mode-other
keywords:
  - "connect to sql database"
  - "sql server management studio"
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

In this quickstart, you'll learn how to use SQL Server Management Studio (SSMS) to connect to Azure SQL Database or Azure SQL Managed Instance and run queries.

## Prerequisites

Completing this quickstart requires the following items:

- [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms/).

- A database in Azure SQL Database, Azure SQL Managed Instance or SQL Server on Azure VM. You can use one of these quickstarts to create and then configure your resource:

  | Action | SQL Database | SQL Managed Instance | SQL Server on Azure VM |
  | :--- | :--- | :--- | :--- |
  | Create | [Portal](single-database-create-quickstart.md) | [Portal](../managed-instance/instance-create-quickstart.md) | [Portal](../virtual-machines/windows/sql-vm-create-portal-quickstart.md) |
  | | [CLI](scripts/create-and-configure-database-cli.md) | [CLI](https://medium.com/azure-sqldb-managed-instance/working-with-sql-managed-instance-using-azure-cli-611795fe0b44) |
  | | [PowerShell](scripts/create-and-configure-database-powershell.md) | [PowerShell](../managed-instance/scripts/create-configure-managed-instance-powershell.md) | [PowerShell](../virtual-machines/windows/sql-vm-create-powershell-quickstart.md) |
  | Configure| [Server-level IP firewall rule](firewall-create-server-level-portal-quickstart.md) <br /> [Microsoft Entra authentication](authentication-aad-configure.md)<sup>1</sup> | [Connectivity from a VM](../managed-instance/connect-vm-instance-configure.md)  <br /> [Connectivity from on-site](../managed-instance/point-to-site-p2s-configure.md)<br /> [Microsoft Entra authentication](authentication-aad-configure.md)<sup>1</sup>   | [Connectivity to SQL Server](../virtual-machines/windows/sql-vm-create-portal-quickstart.md) <br /> [Microsoft Entra authentication](../virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm.md)<sup>1</sup> |
  | Sample database | [AdventureWorksLT sample](single-database-create-quickstart.md?view=azuresql-db&preserve-view=true&tabs=azure-portal) | Restore or import Adventure Works from [BACPAC](database-import.md) file from [GitHub](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) | Restore or import Adventure Works from [BACPAC](database-import.md) file from [GitHub](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) |
  | | | or [Restore Wide World Importers](../managed-instance/restore-sample-database-quickstart.md) | or [Restore Wide World Importers](../managed-instance/restore-sample-database-quickstart.md) |

<sup>1</sup> This tutorial uses [Microsoft Entra multifactor authentication (MFA)](authentication-mfa-ssms-overview.md), which requires configuring a Microsoft Entra admin for your resource. If you haven't configured Microsoft Entra authentication for your resource, you can use SQL Server Authentication instead, though it is less secure. 

  > [!IMPORTANT]  
  > The scripts in this article are written to use the Adventure Works database. With a managed instance, you must either import the Adventure Works database into an instance database or modify the scripts in this article to use the Wide World Importers database.

If you simply want to run some ad hoc queries in Azure SQL Database without installing SSMS, use the [Azure portal's query editor to query a database](query-editor.md).

## Get server connection information

Get the connection information you need to connect to your resource. You'll need the fully qualified [server](logical-servers.md) name (for Azure SQL Database) or host name (for Azure SQL Managed Instance), database name, and login information to complete this quickstart.

1. Sign in to the [Azure portal](https://portal.azure.com/).

1. Navigate to the **database** or **managed instance** you want to query.

1. On the **Overview** page, review the fully qualified server name next to **Server name** for your database in SQL Database or the fully qualified server name (or IP address) next to **Host** for your managed instance in SQL Managed Instance or your SQL Server instance on your VM. To copy the server name or host name, hover over it and select the **Copy** icon.

> [!IMPORTANT]  
> - For connection information for SQL Server on Azure VM, see [Connect to SQL Server](../virtual-machines/windows/sql-vm-create-portal-quickstart.md#connect-to-sql-server)
> - A server listens on port 1433. To connect to a server from behind a corporate firewall, the firewall must have this port open.

## Connect to your database

In SQL Server Management Studio (SSMS), connect to your database.

1. Open SSMS.

1. The **Connect to Server** dialog box appears. Enter the following information on the **Login** tab:

   | Setting | Suggested value | Details |
   | --- | --- | --- |
   | **Server type** | Database Engine | Select **Database Engine** (usually the default option). |
   | **Server name** | The fully qualified server name | Enter the name of your *Azure SQL Database* server or *Azure SQL Managed Instance* host name. |
   | **Authentication** |Microsoft Entra MFA / SQL Server Authentication | This quickstart uses the recommended [Microsoft Entra multifactor authentication](/azure/azure-sql/database/authentication-mfa-ssms-overview) but other [authentication options](#authentication-options) are available. |
   | **Login** | Server account user ID | The user ID from the server account used to create the server. A login is not required if you selected Microsoft Entra MFA.|
   | **Password** | Server account password | The password from the server account used to create the server. A password is not required if you selected Microsoft Entra MFA. | 
   | **Encryption** | Strict (SQL Server 2022 and Azure SQL) | Starting with SQL Server Management Studio 20, *Strict* is required to connect to an Azure SQL resource. |
   | **Trust server certificate** | Trust Server Certificate | Check this option to bypass server certificate validation. The default value is *False* (unchecked), which promotes better security using trusted certificates. This option is selected by default when you choose *Strict* encryption.  |
   | **Host Name in Certificate** | Host name of the server | The value provided in this option is used to specify a different, but expected, CN or SAN in the server certificate. |

   :::image type="content" source="media/connect-query-ssms/connect-to-azure-sql-object-explorer-ssms20.png" alt-text="Screenshot of connection dialog for Azure SQL." lightbox="media/connect-query-ssms/connect-to-azure-sql-object-explorer-ssms20.png":::


1. Select the **Connection Properties** tab in the **Connect to Server** dialog box. 
1. In the **Connect to database** dropdown list menu, select **mySampleDatabase**. Completing the quickstart in the [Prerequisites section](#prerequisites) creates an AdventureWorksLT database named mySampleDatabase. If your working copy of the AdventureWorks database has a different name than mySampleDatabase, then select it instead.

   :::image type="content" source="media/connect-query-ssms/options-connect-to-db.png" alt-text="Screenshot of connect to db on server." lightbox="media/connect-query-ssms/options-connect-to-db.png":::

1. Select **Connect**. The Object Explorer window opens.

1. To view the database's objects, expand **Databases** and then expand your database node.

   :::image type="content" source="media/connect-query-ssms/connected.png" alt-text="Screenshot of mySampleDatabase objects." lightbox="media/connect-query-ssms/connected.png":::

## Query data

Run this [SELECT](/sql/t-sql/queries/select-transact-sql/) Transact-SQL code to query for the top 20 products by category.

1. In Object Explorer, right-click **mySampleDatabase** and select **New Query**. A new query window connected to your database opens.

1. In the query window, paste the following SQL query:

   ```sql
   SELECT pc.Name AS CategoryName,
          p.name AS ProductName
   FROM [SalesLT].[ProductCategory] AS pc
        INNER JOIN
        [SalesLT].[Product] AS p
        ON pc.productcategoryid = p.productcategoryid;
   ```

1. On the toolbar, select **Execute** to run the query and retrieve data from the `Product` and `ProductCategory` tables.

   :::image type="content" source="media/connect-query-ssms/query2.png" alt-text="Screenshot of query to retrieve data from table Product and ProductCategory." lightbox="media/connect-query-ssms/query2.png":::

### Insert data

Run this [INSERT](/sql/t-sql/statements/insert-transact-sql/) Transact-SQL code to create a new product in the `SalesLT.Product` table.

1. Replace the previous query with this one.

   ```sql
   INSERT INTO [SalesLT].[Product] ([Name], [ProductNumber], [Color], [ProductCategoryID], [StandardCost], [ListPrice], [SellStartDate])
   VALUES                         ('myNewProduct', 123456789, 'NewColor', 1, 100, 100, GETDATE());
   ```

1. Select **Execute** to insert a new row in the `Product` table. The **Messages** pane displays **(1 row affected)**.

#### View the result

1. Replace the previous query with this one.

   ```sql
   SELECT *
   FROM [SalesLT].[Product]
   WHERE Name = 'myNewProduct';
   ```

1. Select **Execute**. The following result appears.

   :::image type="content" source="media/connect-query-ssms/result.png" alt-text="Screenshot of result of Product table query." lightbox="media/connect-query-ssms/result.png":::

### Update data

Run this [UPDATE](/sql/t-sql/queries/update-transact-sql) Transact-SQL code to modify your new product.

1. Replace the previous query with this one that returns the new record created previously:

   ```sql
   UPDATE [SalesLT].[Product]
       SET [ListPrice] = 125
   WHERE Name = 'myNewProduct';
   ```

1. Select **Execute** to update the specified row in the `Product` table. The **Messages** pane displays **(1 row affected)**.

### Delete data

Run this [DELETE](/sql/t-sql/statements/delete-transact-sql/) Transact-SQL code to remove your new product.

1. Replace the previous query with this one.

   ```sql
   DELETE [SalesLT].[Product]
   WHERE Name = 'myNewProduct';
   ```

1. Select **Execute** to delete the specified row in the `Product` table. The **Messages** pane displays **(1 row affected)**.

## Authentication options

Although this quickstart uses Microsoft Entra MFA, other authentication options are available, such as: 

- **Default**: The default option can be used when connecting using any Microsoft Entra authentication mode that's passwordless and noninteractive.
- [Microsoft Entra multifactor authentication](authentication-mfa-ssms-overview.md):  Uses an interactive prompt for authentication.
- [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md): Supports two types of managed identities: system-assigned managed identity (SMI) and user-assigned managed identity (UMI). If you want to use a managed identity to connect to any SQL product from SSMS, install SSMS to an Azure VM. SSMS needs to be within an Azure context where it has access to request a token for that managed identity. The SQL product must have a [principal](authentication-aad-service-principal.md) for that managed identity. See [Use a Windows VM system-assigned managed identity to access Azure SQL](/entra/identity/managed-identities-azure-resources/tutorial-windows-managed-identities-vm-access).
- [Microsoft Entra service principals](authentication-aad-service-principal.md): Use a service principal to authenticate to a SQL product from SSMS by using its application client ID and [secret](/entra/identity-platform/howto-create-service-principal-portal#option-3-create-a-new-client-secret).
- **Microsoft Entra password**: Uses a Microsoft Entra user name and password to connect to the SQL product. 
- **Microsoft Entra integrated**: Uses the current Windows user's credentials to connect to the SQL product.
- [SQL Server Authentication](/sql/relational-databases/security/choose-an-authentication-mode#connecting-through-sql-server-authentication): Uses a SQL Server login and password to connect to the SQL product. This option is less secure than Microsoft Entra authentication.


## Related content

- [SQL Server Management Studio](/sql/ssms/sql-server-management-studio-ssms/)
- [Azure portal query editor for Azure SQL Database](query-editor.md)
- [Quickstart: Use Visual Studio Code to connect and query Azure SQL Database or Azure SQL Managed Instance](connect-query-vscode.md)
- [Quickstart: Use .NET and C# in Visual Studio to connect to and query a database](connect-query-dotnet-visual-studio.md)
- [Quickstart: Use PHP to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-php.md)
- [Quickstart: Use Node.js to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-nodejs.md)
- [Use Java and JDBC with Azure SQL Database](connect-query-java.md)
- [Quickstart: Use Python to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-python.md)
- [Quickstart: Use Ruby to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-ruby.md)
