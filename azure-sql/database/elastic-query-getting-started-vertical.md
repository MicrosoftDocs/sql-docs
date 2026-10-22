---
title: Get Started With Cross-Database Queries
description: How to use elastic database query with vertically partitioned databases
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: bogavril
ms.date: 12/09/2024
ms.service: azure-sql-database
ms.subservice: scale-out
ms.topic: how-to
ms.custom:
  - sqldbrb=1
---
# Get started with cross-database queries (vertical partitioning) (preview)
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Elastic database query (preview) for Azure SQL Database allows you to run T-SQL queries that span multiple databases using a single connection point. This article applies to [vertically partitioned databases](elastic-query-vertical-partitioning.md). In this article, learn how to configure and use an Azure SQL Database to perform queries that span multiple related databases.

For more information about the elastic database query feature, see [Azure SQL Database elastic query overview (preview)](elastic-query-overview.md).

## Prerequisites

The permission ALTER ANY EXTERNAL DATA SOURCE is required. This permission is included with the ALTER DATABASE permission. ALTER ANY EXTERNAL DATA SOURCE permissions are needed to refer to the underlying data source.

## Create the sample databases

To start with, create two databases, `Customers` and `Orders`, either in the same or different logical servers.

Execute the following queries on the `Orders` database to create the `OrderInformation` table and input the sample data.

```sql
CREATE TABLE [dbo].[OrderInformation](
    [OrderID] [int] NOT NULL,
    [CustomerID] [int] NOT NULL
    )
INSERT INTO [dbo].[OrderInformation] ([OrderID], [CustomerID]) VALUES (123, 1)
INSERT INTO [dbo].[OrderInformation] ([OrderID], [CustomerID]) VALUES (149, 2)
INSERT INTO [dbo].[OrderInformation] ([OrderID], [CustomerID]) VALUES (857, 2)
INSERT INTO [dbo].[OrderInformation] ([OrderID], [CustomerID]) VALUES (321, 1)
INSERT INTO [dbo].[OrderInformation] ([OrderID], [CustomerID]) VALUES (564, 8)
```

Now, execute following query on the `Customers` database to create the `CustomerInformation` table and input the sample data.

```sql
CREATE TABLE [dbo].[CustomerInformation](
    [CustomerID] [int] NOT NULL,
    [CustomerName] [varchar](50) NULL,
    [Company] [varchar](50) NULL
    CONSTRAINT [CustID] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
)
INSERT INTO [dbo].[CustomerInformation] ([CustomerID], [CustomerName], [Company]) VALUES (1, 'Jack', 'ABC')
INSERT INTO [dbo].[CustomerInformation] ([CustomerID], [CustomerName], [Company]) VALUES (2, 'Steve', 'XYZ')
INSERT INTO [dbo].[CustomerInformation] ([CustomerID], [CustomerName], [Company]) VALUES (3, 'Lylla', 'MNO')
```

## Create database objects

### Database scoped master key and credentials

1. Open SQL Server Management Studio or SQL Server Data Tools in Visual Studio.
1. Connect to the Orders database and execute the following T-SQL commands:

    ```sql
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<master_key_password>';
    CREATE DATABASE SCOPED CREDENTIAL ElasticDBQueryCred
    WITH IDENTITY = '<username>',
    SECRET = '<password>';  
    ```

    - The `master_key_password` is a strong password of your choosing used to encrypt the connection credentials. 
    - The `username` and `password` should be the username and password used to sign in into the Customers database (create a new user in Customers database if one doesn't already exists).
    - Authentication using Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) with elastic queries isn't currently supported.

### External data sources

To create an external data source, execute the following command on the `Orders` database to connect to the `Customers` database. Provide the Azure SQL logical server of the `Customers` database in the `LOCATION`.

```sql
CREATE EXTERNAL DATA SOURCE MyElasticDBQueryDataSrc WITH
    (TYPE = RDBMS,
    LOCATION = '<server_name>.database.windows.net',
    DATABASE_NAME = 'Customers',
    CREDENTIAL = ElasticDBQueryCred
);
```

### External tables

Create an external table on the `Orders` database, which matches the definition of the `CustomerInformation` table:

```sql
CREATE EXTERNAL TABLE [dbo].[CustomerInformation]
( [CustomerID] [int] NOT NULL,
    [CustomerName] [varchar](50) NOT NULL,
    [Company] [varchar](50) NOT NULL)
WITH
( DATA_SOURCE = MyElasticDBQueryDataSrc)
```

### Remote queries

Use the [sp_execute_remote](/sql/relational-databases/system-stored-procedures/sp-execute-remote-azure-sql-database) stored procedure to execute a Transact-SQL statement on a single remote Azure SQL Database or set of databases serving as shards in a horizontal partitioning scheme. The following remote T-SQL query returns data from the external `OrderInformation` table.

```sql
EXEC sp_execute_remote
    N'MyElasticDBQueryDataSrc',
    N'SELECT COUNT(CustomerID) AS customer_count FROM CustomerInformation';
```


## Execute a sample elastic database T-SQL query

Once you define your external data source and your external tables, you can now use T-SQL to query your external tables. Execute this query on the `Orders` database:

```sql
SELECT OrderInformation.CustomerID, OrderInformation.OrderId, CustomerInformation.CustomerName, CustomerInformation.Company
FROM OrderInformation
INNER JOIN CustomerInformation
ON CustomerInformation.CustomerID = OrderInformation.CustomerID;
```

## Cost

Currently, the elastic database query feature is included into the cost of your Azure SQL Database.  

For pricing information, see [SQL Database Pricing](https://azure.microsoft.com/pricing/details/sql-database).

## Related content

- [Azure SQL Database elastic query overview (preview)](elastic-query-overview.md)
- [Query across cloud databases with different schemas (preview)](elastic-query-vertical-partitioning.md)
- [Report across scaled-out cloud databases (preview)](elastic-query-getting-started.md)
- [Reporting across scaled-out cloud databases (preview)](elastic-query-horizontal-partitioning.md)
- [sp_execute_remote](/sql/relational-databases/system-stored-procedures/sp-execute-remote-azure-sql-database)
