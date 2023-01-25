---
description: "Set or Change the Column Collation"
title: "Set or Change the Column Collation | Microsoft Docs"
ms.custom: ""
ms.date: "12/05/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "tempdb database [SQL Server], collations"
  - "collations [SQL Server], column"
ms.assetid: d7a9638b-717c-4680-9b98-8849081e08be
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Set or Change the Column Collation
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  You can override the database collation for **char**, **varchar**, **text**, **nchar**, **nvarchar**, and **ntext** data by specifying a different collation for a specific column of a table and using one of the following:  
  
-   The COLLATE clause of [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) and [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md), as seen in the examples below. 

    -   **In-place conversion.** Consider one of the existing tables defined below:

        ```sql
        -- NVARCHAR column is encoded in UTF-16 because a supplementary character enabled collation is used
        CREATE TABLE dbo.MyTable (CharCol NVARCHAR(50) COLLATE Latin1_General_100_CI_AI_SC);

        -- VARCHAR column is encoded the Latin code page and therefore is not Unicode capable
        CREATE TABLE dbo.MyTable (CharCol VARCHAR(50) COLLATE Latin1_General_100_CI_AI);
        ```

        To convert the column in-place to use UTF-8, run an `ALTER COLUMN` statement that sets the required data type and a UTF-8 enabled collation:

        ```sql 
        ALTER TABLE dbo.MyTable 
        ALTER COLUMN CharCol VARCHAR(50) COLLATE Latin1_General_100_CI_AI_SC_UTF8
        ```

        This method is easy to implement, however it's a possibly blocking operation which may become an issue for large tables and busy applications.

    -   **Copy and replace.** Consider one of the existing tables defined below:

        ```sql
        -- NVARCHAR column is encoded in UTF-16 because a supplementary character enabled collation is used
        CREATE TABLE dbo.MyTable (CharCol NVARCHAR(50) COLLATE Latin1_General_100_CI_AI_SC);
        GO

        -- VARCHAR column is encoded using the Latin code page and therefore is not Unicode capable
        CREATE TABLE dbo.MyTable (CharCol VARCHAR(50) COLLATE Latin1_General_100_CI_AI);
        GO
        ```

        To convert the column to use UTF-8, copy the data to a new table where the target column is already the required data type and a UTF-8 enabled collation, and then replace the old table:

        ```sql
        CREATE TABLE dbo.MyTableNew (CharCol VARCHAR(50) COLLATE Latin1_General_100_CI_AI_SC_UTF8);
        GO
        INSERT INTO dbo.MyTableNew 
        SELECT * FROM dbo.MyTable;
        GO
        DROP TABLE dbo.MyTable;
        GO
        EXEC sp_rename 'dbo.MyTableNew', 'dbo.MyTable';
        GO
        ```

        This method is much faster than in-place conversion, but handling complex schemas with many dependencies (FKs, PKs, Triggers, DFs) and syncing the tail of the table (if the database is in use) requires more planning.
        
    For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Modify Columns (Database Engine)](../../relational-databases/tables/modify-columns-database-engine.md#SSMSProcedure).  
  
-   Using the **Column.Collation** property in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO).  
  
 You cannot change the collation of a column that is currently referenced by any one of the following:  
  
-   A computed column  
-   An index  
-   Distribution statistics, either generated automatically or by the `CREATE STATISTICS` statement  
-   A CHECK constraint  
-   A FOREIGN KEY constraint  
  
 When you work with **tempdb**, the [COLLATE](~/t-sql/statements/collations.md) clause includes a *database_default* option to specify that a column in a temporary table uses the collation default of the current user database for the connection instead of the collation of **tempdb**.  
  
## Collations and text Columns  
 You can insert or update values in a **text** column whose collation is different from the code page of the default collation of the database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implicitly converts the values to the collation of the column.  
  
## Collations and tempdb  
 The **tempdb** database is built every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started and has the same default collation as the **model** database. This is typically the same as the default collation of the instance. If you create a user database and specify a different default collation than **model**, the user database has a different default collation than **tempdb**. All temporary stored procedures or temporary tables are created and stored in **tempdb**. This means that all implicit columns in temporary tables and all coercible-default constants, variables, and parameters in temporary stored procedures have collations that are different from comparable objects created in permanent tables and stored procedures.  
  
 This could lead to problems with a mismatch in collations between user-defined databases and system database objects. For example, an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the Latin1_General_CS_AS collation and you execute the following statements:  
  
```sql  
CREATE DATABASE TestDB COLLATE Estonian_CS_AS;  
USE TestDB;  
CREATE TABLE TestPermTab (PrimaryKey int PRIMARY KEY, Col1 nchar );  
```  
  
 In this system, the **tempdb** database uses the Latin1_General_CS_AS collation with code page 1252, and `TestDB` and `TestPermTab.Col1` use the `Estonian_CS_AS` collation with code page 1257. For example:  
  
```sql  
USE TestDB;  
GO  
-- Create a temporary table with the same column declarations  
-- as TestPermTab  
CREATE TABLE #TestTempTab (PrimaryKey int PRIMARY KEY, Col1 nchar );  
INSERT INTO #TestTempTab  
         SELECT * FROM TestPermTab;  
GO  
```  
  
 With the previous example, the **tempdb** database uses the Latin1_General_CS_AS collation, and `TestDB` and `TestTab.Col1` use the `Estonian_CS_AS` collation. For example:  
  
```sql  
SELECT * FROM TestPermTab AS a INNER JOIN #TestTempTab on a.Col1 = #TestTempTab.Col1;  
```  
  
 Because **tempdb** uses the default server collation and `TestPermTab.Col1` uses a different collation, SQL Server returns this error: "Cannot resolve collation conflict between 'Latin1_General_CI_AS_KS_WS' and 'Estonian_CS_AS' in equal to operation."  
  
 To prevent the error, you can use one of the following alternatives:  
  
-   Specify that the temporary table column use the default collation of the user database, not **tempdb**. This enables the temporary table to work with similarly formatted tables in multiple databases, if that is required of your system.  
  
    ```sql  
    CREATE TABLE #TestTempTab  
       (PrimaryKey int PRIMARY KEY,  
        Col1 nchar COLLATE database_default  
       );  
    ```  
  
-   Specify the correct collation for the `#TestTempTab` column:  
  
    ```sql  
    CREATE TABLE #TestTempTab  
       (PrimaryKey int PRIMARY KEY,  
        Col1 nchar COLLATE Estonian_CS_AS  
       );  
    ```  
  
## See Also  
 [Set or Change the Server Collation](../../relational-databases/collations/set-or-change-the-server-collation.md)   
 [Set or Change the Database Collation](../../relational-databases/collations/set-or-change-the-database-collation.md)   
 [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)  
  
  
