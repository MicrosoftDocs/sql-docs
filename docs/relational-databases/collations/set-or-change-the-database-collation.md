---
title: "Set or change the database collation"
description: "Set or change the database collation"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 09/27/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "collations [SQL Server], database"
  - "database collations [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Set or change the database collation

[!INCLUDE [SQL Server, Azure SQL Database Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to set or change the database [collation](collation-and-unicode-support.md) by using [SQL Server Management Studio (SSMS)](../../ssms/sql-server-management-studio-ssms.md) or [!INCLUDE [tsql](../../includes/tsql-md.md)]. 

If no database collation is specified, the [server collation](set-or-change-the-server-collation.md) is used.


## <a id="Recommendations"></a> Recommendations

You can find the supported collation names in [Windows Collation Name](../../t-sql/statements/windows-collation-name-transact-sql.md) and [SQL Server Collation Name](../../t-sql/statements/sql-server-collation-name-transact-sql.md); or you can use the [sys.fn_helpcollations](../system-functions/sys-fn-helpcollations-transact-sql.md) system function.

When you change the database collation, you change:

- Any **char**, **varchar**, **text**, **nchar**, **nvarchar**, or **ntext** columns in system tables are changed to the new collation.

- All existing **char**, **varchar**, **text**, **nchar**, **nvarchar**, or **ntext** parameters and scalar return values for stored procedures and user-defined functions are changed to the new collation.

- The **char**, **varchar**, **text**, **nchar**, **nvarchar**, or **ntext** system data types, and all user-defined data types based on these system data types, are changed to the new default collation.

You can change the collation of any new objects that are created in a user database by using the `COLLATE` clause of the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement. This statement **does not change** the collation of the columns in any existing user-defined tables. These can be changed by using the `COLLATE` clause of [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).

## <a id="Permissions"></a> Permissions

To create a new database, you need the `CREATE DATABASE` permission in the `master` database, or the `CREATE ANY DATABASE`, or `ALTER ANY DATABASE` permission.

To change the collation of an existing database, you need the `ALTER` permission on the database.

## Set or change the database collation

You can set or change the database collation using SQL Server Management Studio (SSMS), or Transact-SQL (T-SQL).

### [SQL Server Management Studio (SSMS)](#tab/ssms)
<a id="SSMSProcedure"></a>

You can specify the collation for a new database or update the collation for an existing database by using SQL Server Management Studio (SSMS). 



In SQL Server Management Studio, open **Object Explorer**, connect to an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], expand that instance, and then expand **Databases**:

- **For a new database**: Right-click **Databases** and then select **New Database**. If you don't want the default collation, select the **Options** page, and select a collation from the **Collation** dropdown list.
- **For an existing database**: Right-click the database that you want and select **Properties**. Select the **Options** page, and select a collation from the **Collation** dropdown list.


### [Transact-SQL](#tab/tsql)
<a id="TsqlProcedure"></a> 

Use Transact-SQL to set or change the database collation for a new or existing database. 

To set the database collation for a new database, use the [COLLATE clause](../../t-sql/statements/collations.md) in [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md) to specify a collation name. 

The following sample T-SQL creates a new database named `MyOptionsTest` that uses the `Latin1_General_100_CS_AS_SC` collation: 

```sql
USE master;
GO

IF DB_ID (N'MyOptionsTest') IS NOT NULL
    DROP DATABASE MyOptionsTest;
GO

CREATE DATABASE MyOptionsTest
    COLLATE Latin1_General_100_CS_AS_SC;
GO
```

To change the database collation using Transact-SQL, use the [COLLATE clause](../../t-sql/statements/collations.md) in an [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md)  statement. 

The following sample T-SQL updates an existing database named `MyOptionsTest` to use the `French_CI_AS` collation: 

```sql
USE master;  
GO

ALTER DATABASE MyOptionsTest  
    COLLATE French_CI_AS ;  
GO  
```

The following sample T-SQL checks the database collation for an existing database: 

```sql
SELECT name, collation_name  
FROM sys.databases  
WHERE name = N'MyOptionsTest';  
GO
```

---

## Data after changing the collation

> [!IMPORTANT]  
> Changing the collation of a database or individual columns **does not modify** the underlying data already stored in existing tables. Unless your application explicitly handles data conversion and comparison between different collations, it is recommended that you transition existing data in the database to the new collation. This removes the risk that applications might incorrectly modify data, resulting in possible wrong results or silent data loss.

When a database collation is changed, only new tables inherit the new database collation by default. There are several alternatives to convert existing data to the new collation:

-  Convert data in-place. To convert the collation for a column in an existing table, see [Set or Change the Column Collation](set-or-change-the-column-collation.md). This operation is easy to implement, but might become a blocking issue for large tables and busy applications. See the following example for an in-place conversion of the `MyString` column to a new collation:

   ```sql
   ALTER TABLE dbo.MyTable
       ALTER COLUMN MyString VARCHAR(50) COLLATE Latin1_General_100_CI_AI_SC_UTF8;
   ```

-  Copy data to new tables that use the new collation, and replace original tables in the same database. Create a new table in the current database that will inherit the database collation, copy the data between the old table and the new table, drop the original table, and rename the new table to the name of the original table. This is a faster operation than an in-place conversion, but might become a challenge when handling complex schemas with dependencies such as Foreign Key constraints, Primary Key constraints, and Triggers. It would also require a final data synchronization between the original and the new table before the final cut-off, if data continues to be changed by applications. See the following example for a "copy and replace" conversion of the `MyString` column to a new collation:

   ```sql
   CREATE TABLE dbo.MyTable2 (MyString VARCHAR(50) COLLATE Latin1_General_100_CI_AI_SC_UTF8);

   INSERT INTO dbo.MyTable2
   SELECT * FROM dbo.MyTable;

   DROP TABLE dbo.MyTable;

   EXEC sp_rename 'dbo.MyTable2', 'dbo.MyTable';
   ```

-  Copy data to a new database that uses the new collation, and replace the original database. Create a new database using the new collation, and transfer the data from the original database via tools like [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] or the Import/Export Wizard in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. This is a simpler approach for complex schemas. It would also require a final data synchronization between the original and the new databases before the final cut-off, if data continues to be changed by applications.

## <a id="Restrictions"></a> Limitations 

- Windows Unicode-only collations can only be used with the [COLLATE clause](../../t-sql/statements/collations.md) to apply collations to the **nchar**, **nvarchar**, and **ntext** data types on column level and expression-level data. They can't be used with the COLLATE clause to change the collation of a database or server instance.

- If the specified collation or the collation used by the referenced object uses a code page that isn't supported by Windows, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] displays an error.

- Server-level collation in Azure SQL Managed Instance can be specified when the instance is created and can't be changed later. Learn more in [Set or change the server collation](set-or-change-the-server-collation.md#set-the-server-collation-in-azure-sql-managed-instance).

> [!IMPORTANT]  
> The `ALTER DATABASE COLLATE` statement is not supported on Azure SQL Database. Specify database collation and catalog collation at the time of [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true#collation_name).


## Related content

- [Collation and Unicode support](collation-and-unicode-support.md)
- [sys.fn_helpcollations (Transact-SQL)](../system-functions/sys-fn-helpcollations-transact-sql.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [SQL Server Collation Name (Transact-SQL)](../../t-sql/statements/sql-server-collation-name-transact-sql.md)
- [Windows collation name (Transact-SQL)](../../t-sql/statements/windows-collation-name-transact-sql.md)
- [COLLATE (Transact-SQL)](~/t-sql/statements/collations.md)
- [Collation precedence](../../t-sql/statements/collation-precedence-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
