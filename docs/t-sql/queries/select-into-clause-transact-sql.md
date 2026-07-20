---
title: "INTO Clause (Transact-SQL)"
description: "SELECT - INTO Clause (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 02/02/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "INTO_TSQL"
  - "INSERT_INTO_TSQL"
  - "INSERT INTO"
  - "INTO"
  - "INTO clause"
  - "INTO_clause_TSQL"
helpviewer_keywords:
  - "copying data [SQL Server], into a new table"
  - "INTO clause"
  - "moving data, to a new table"
  - "table creation [SQL Server], INTO clause"
  - "SELECT INTO statement"
  - "inserting rows"
  - "clauses [SQL Server], INTO"
  - "row additions [SQL Server], INTO clause"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SELECT - INTO clause (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

The `SELECT...INTO` statement creates a new table in the default filegroup and inserts the resulting rows from the query into it. For the complete `SELECT` syntax, see [SELECT](select-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[ INTO new_table ]
[ ON filegroup ]
```

## Arguments

#### *new_table*

Specifies the name of a new table to create, based on the columns in the select list and the rows chosen from the data source.

The format of *new_table* is determined by evaluating the expressions in the select list. The columns in *new_table* are created in the order specified by the select list. Each column in *new_table* has the same name, data type, nullability, and value as the corresponding expression in the select list. The `IDENTITY` property of a column is transferred except under the conditions defined in "Working with Identity Columns" in the Remarks section.

To create the table in another database on the same instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], specify *new_table* as a fully qualified name in the form *database.schema.table_name*.

You can't create *new_table* on a remote server. However, you can populate *new_table* from a remote data source. To create *new_table* from a remote source table, specify the source table using a four-part name in the form *linked_server*.*catalog*.*schema*.*object* in the `FROM` clause of the `SELECT` statement. Alternatively, you can use the [OPENQUERY](../functions/openquery-transact-sql.md) function or the [OPENDATASOURCE](../functions/opendatasource-transact-sql.md) function in the `FROM` clause to specify the remote data source.

#### *filegroup*

Specifies the name of the filegroup in which to create the new table. The filegroup must exist in the database, or the SQL Server engine returns an error.

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2 and later.

## Data types

The FILESTREAM attribute doesn't transfer to the new table. FILESTREAM BLOBs are copied and stored in the new table as **varbinary(max)** BLOBs. Without the FILESTREAM attribute, the **varbinary(max)** data type has a limitation of 2 GB. If a FILESTREAM BLOB exceeds this value, error 7119 is raised and the statement stops.

When you select an existing identity column into a new table, the new column inherits the `IDENTITY` property, unless one of the following conditions is true:

- The `SELECT` statement contains a join.
- Multiple `SELECT` statements are joined by using `UNION`.
- The identity column is listed more than one time in the select list.
- The identity column is part of an expression.
- The identity column is from a remote data source.

If any one of these conditions is true, the column is created `NOT NULL` instead of inheriting the `IDENTITY` property. If an identity column is required in the new table but such a column isn't available, or you want a seed or increment value that's different than the source identity column, define the column in the select list by using the `IDENTITY` function. See "Creating an identity column using the `IDENTITY` function" in the Examples section.

## Remarks

The `SELECT...INTO` statement operates in two parts: the new table is created, and then rows are inserted. This two-step process means that if the inserts fail, the operation rolls back all the inserts, but the new (empty) table remains. If you need the entire operation to succeed or fail as a whole, use an [explicit transaction](../language-elements/begin-transaction-transact-sql.md).

[!INCLUDE [fabricdw](../../includes/fabric-dw.md)] doesn't support filegroups. References and examples in this article to filegroups don't apply to [!INCLUDE [fabricdw](../../includes/fabric-dw.md)].

## Limitations

You can't specify a table variable or table-valued parameter as the new table.

You can't use `SELECT...INTO` to create a partitioned table, even when the source table is partitioned. `SELECT...INTO` doesn't use the partition scheme of the source table. Instead, the new table is created in the default filegroup. To insert rows into a partitioned table, you must first create the partitioned table and then use the `INSERT INTO...SELECT...FROM` statement.

Indexes, constraints, and triggers defined in the source table aren't transferred to the new table, nor can you specify them in the `SELECT...INTO` statement. If you need these objects, you can create them after executing the `SELECT...INTO` statement.

Specifying an `ORDER BY` clause doesn't guarantee the rows are inserted in the specified order.

When you include a sparse column in the select list, the sparse column property doesn't transfer to the column in the new table. If you need this property in the new table, alter the column definition after executing the `SELECT...INTO` statement to include this property.

When you include a computed column in the select list, the corresponding column in the new table isn't a computed column. The values in the new column are the values that were computed at the time `SELECT...INTO` was executed.

<a id="logging-behavior"></a>

## Log behavior

The amount of logging for `SELECT...INTO` depends on the recovery model in effect for the database. Under the simple recovery model or bulk-logged recovery model, bulk operations are minimally logged. With minimal logging, the `SELECT...INTO` statement can be more efficient than creating a table and then populating the table with an `INSERT` statement. For more information, see the [transaction log](../../relational-databases/logs/the-transaction-log-sql-server.md) article.

`SELECT...INTO` statements that contain user-defined functions (UDFs) are fully logged operations. If the user-defined functions used by the `SELECT...INTO` statement don't perform any data access operations, you can specify the `SCHEMABINDING` clause for the user-defined functions. This clause sets the derived `UserDataAccess` property for those user-defined functions to `0`. After this change, `SELECT...INTO` statements are minimally logged. If the `SELECT...INTO` statement still references at least one user-defined function that has this property set to `1`, the operation is fully logged.

## Permissions

Requires `CREATE TABLE` permission in the database and `ALTER` permission on the schema in which the table is being created.

## Examples

<a id="a-creating-a-table-by-specifying-columns-from-multiple-sources"></a>

### A. Create a table by specifying columns from multiple sources

The following example creates the table `dbo.EmployeeAddresses` in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database by selecting seven columns from various employee-related and address-related tables.

```sql
SELECT c.FirstName,
       c.LastName,
       e.JobTitle,
       a.AddressLine1,
       a.City,
       sp.Name AS [State/Province],
       a.PostalCode
INTO dbo.EmployeeAddresses
FROM Person.Person AS c
     INNER JOIN HumanResources.Employee AS e
         ON e.BusinessEntityID = c.BusinessEntityID
     INNER JOIN Person.BusinessEntityAddress AS bea
         ON e.BusinessEntityID = bea.BusinessEntityID
     INNER JOIN Person.Address AS a
         ON bea.AddressID = a.AddressID
     INNER JOIN Person.StateProvince AS sp
         ON sp.StateProvinceID = a.StateProvinceID;
```

<a id="b-inserting-rows-using-minimal-logging"></a>

### B. Insert rows using minimal logging

The following example creates the table `dbo.NewProducts` and inserts rows from the `Production.Product` table. The example assumes that the recovery model of the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database is set to `FULL`. To ensure minimal logging, the recovery model of the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database is set to `BULK_LOGGED` before inserting rows, and resets it to `FULL` after the `SELECT...INTO` statement. This process ensures that the `SELECT...INTO` statement uses minimal space in the transaction log and performs efficiently.

```sql
ALTER DATABASE AdventureWorks2025
SET RECOVERY BULK_LOGGED;
GO

SELECT *
INTO dbo.NewProducts
FROM Production.Product
WHERE ListPrice > $25
      AND ListPrice < $100;
GO

ALTER DATABASE AdventureWorks2025
SET RECOVERY FULL;
GO
```

<a id="c-creating-an-identity-column-using-the-identity-function"></a>

### C. Create an identity column using the identity function

The following example uses the `IDENTITY` function to create an identity column in the new table `Person.USAddress` in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. This step is required because the `SELECT` statement that defines the table contains a join, which prevents the `IDENTITY` property from transferring to the new table. The seed and increment values specified in the `IDENTITY` function are different from values of the `AddressID` column in the source table `Person.Address`.

```sql
-- Determine the IDENTITY status of the source column AddressID.
SELECT OBJECT_NAME(object_id) AS TableName,
       name AS column_name,
       is_identity,
       seed_value,
       increment_value
FROM sys.identity_columns
WHERE name = 'AddressID';

-- Create a new table with columns from the existing table Person.Address.
-- A new IDENTITY column is created by using the IDENTITY function.
SELECT IDENTITY (INT, 100, 5) AS AddressID,
       a.AddressLine1,
       a.City,
       b.Name AS State,
       a.PostalCode
INTO Person.USAddress
FROM Person.Address AS a
     INNER JOIN Person.StateProvince AS b
         ON a.StateProvinceID = b.StateProvinceID
WHERE b.CountryRegionCode = N'US';

-- Verify the IDENTITY status of the AddressID columns in both tables.
SELECT OBJECT_NAME(object_id) AS TableName,
       name AS column_name,
       is_identity,
       seed_value,
       increment_value
FROM sys.identity_columns
WHERE name = 'AddressID';
```

<a id="d-creating-a-table-by-specifying-columns-from-a-remote-data-source"></a>

### D. Create a table by specifying columns from a remote data source

The following example demonstrates three methods of creating a new table on the local server from a remote data source. The example begins by creating a link to the remote data source. The linked server name, `MyLinkServer,` is then specified in the `FROM` clause of the first `SELECT...INTO` statement and in the `OPENQUERY` function of the second `SELECT...INTO` statement. The third `SELECT...INTO` statement uses the `OPENDATASOURCE` function, which specifies the remote data source directly instead of using the linked server name.

```sql
USE master;
GO

-- Create a link to the remote data source.
-- Specify a valid server name for @datasrc as 'server_name'
-- or 'server_name\instance_name'.
EXECUTE sp_addlinkedserver
    @server = N'MyLinkServer',
    @srvproduct = N' ',
    @provider = N'SQLNCLI',
    @datasrc = N'server_name',
    @catalog = N'AdventureWorks2025';

USE AdventureWorks2025;
GO

-- Specify the remote data source in the FROM clause using a four-part name
-- in the form linked_server.catalog.schema.object.
SELECT DepartmentID,
       Name,
       GroupName,
       ModifiedDate
INTO dbo.Departments
FROM MyLinkServer.AdventureWorks2025.HumanResources.Department;
GO

-- Use the OPENQUERY function to access the remote data source.
SELECT DepartmentID,
       Name,
       GroupName,
       ModifiedDate
INTO dbo.DepartmentsUsingOpenQuery
FROM OPENQUERY (
    MyLinkServer,
    'SELECT * FROM AdventureWorks2025.HumanResources.Department'
);
GO

-- Use the OPENDATASOURCE function to specify the remote data source.
-- Specify a valid server name for Data Source using the format
-- server_name or server_name\instance_name.
SELECT DepartmentID,
       Name,
       GroupName,
       ModifiedDate
INTO dbo.DepartmentsUsingOpenDataSource
FROM OPENDATASOURCE (
    'SQLNCLI',
    'Data Source = server_name;Integrated Security = SSPI'
).AdventureWorks2025.HumanResources.Department;
```

### E. Import from an external table created with PolyBase

This example imports data from Hadoop or Azure Storage into SQL Server for persistent storage. It then uses `SELECT INTO` to import data referenced by an external table for persistent storage in SQL Server. Finally, it creates a relational table on the fly, and then creates a columnstore index on the table.

**Applies to**: [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)].

```sql
-- Import data for car drivers into SQL Server to do more in-depth analysis.
SELECT DISTINCT Insured_Customers.FirstName,
                Insured_Customers.LastName,
                Insured_Customers.YearlyIncome,
                Insured_Customers.MaritalStatus
INTO Fast_Customers
FROM Insured_Customers
     INNER JOIN (SELECT *
      FROM CarSensor_Data
      WHERE Speed > 35) AS SensorD
     ON Insured_Customers.CustomerKey = SensorD.CustomerKey
ORDER BY YearlyIncome;
```

<a id="f-copying-the-data-from-one-table-to-another-and-create-the-new-table-on-a-specified-filegroup"></a>

### F. Copy the data from one table to another and create the new table on a specified filegroup

The following example demonstrates creating a new table as a copy of another table and loading it into a specified filegroup different from the default filegroup of the user.

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2 and later.

```sql
ALTER DATABASE [AdventureWorksDW2022]
ADD FILEGROUP FG2;
GO

ALTER DATABASE [AdventureWorksDW2022]
ADD FILE (
    NAME = 'FG2_Data',
    FILENAME = '/var/opt/mssql/data/AdventureWorksDW2022_Data1.mdf'
) TO FILEGROUP FG2;
GO

SELECT *
INTO [dbo].[FactResellerSalesXL] ON FG2
FROM [dbo].[FactResellerSales];
```

## Related content

- [SELECT (Transact-SQL)](select-transact-sql.md)
- [SELECT examples (Transact-SQL)](select-examples-transact-sql.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [IDENTITY (Function) (Transact-SQL)](../functions/identity-function-transact-sql.md)
