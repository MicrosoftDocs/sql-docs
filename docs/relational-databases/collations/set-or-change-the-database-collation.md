---
description: "Set or Change the Database Collation"
title: "Set or Change the Database Collation | Microsoft Docs"
ms.custom: ""
ms.date: "10/27/2020"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "collations [SQL Server], database"
  - "database collations [SQL Server]"
ms.assetid: 1379605c-1242-4ac8-ab1b-e2a2b5b1f895
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Set or Change the Database Collation
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how set and change the database collation by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. If no collation is specified, the server collation is used.  
  
> [!IMPORTANT]
> `ALTER DATABASE COLLATE` statement on Azure SQL Database is not supported.

 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To set or change the database collation, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Windows Unicode-only collations can only be used with the COLLATE clause to apply collations to the **nchar**, **nvarchar**, and **ntext** data types on column level and expression-level data. They cannot be used with the COLLATE clause to change the collation of a database or server instance.  
  
-   If the specified collation or the collation used by the referenced object uses a code page that is not supported by Windows, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] displays an error.  

###  <a name="Recommendations"></a> Recommendations  
  
You can find the supported collation names in [Windows Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/windows-collation-name-transact-sql.md) and [SQL Server Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/sql-server-collation-name-transact-sql.md); or you can use the [sys.fn_helpcollations &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md) system function.  
  
When you change the database collation, you change the following:  
  
-   Any **char**, **varchar**, **text**, **nchar**, **nvarchar**, or **ntext** columns in system tables are changed to the new collation.  
  
-   All existing **char**, **varchar**, **text**, **nchar**, **nvarchar**, or **ntext** parameters and scalar return values for stored procedures and user-defined functions are changed to the new collation.  
  
-   The **char**, **varchar**, **text**, **nchar**, **nvarchar**, or **ntext** system data types, and all user-defined data types based on these system data types, are changed to the new default collation.  
  
You can change the collation of any new objects that are created in a user database by using the `COLLATE` clause of the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement. This statement **does not change** the collation of the columns in any existing user-defined tables. These can be changed by using the `COLLATE` clause of [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  

> [!IMPORTANT]
> Changing the collation of a database or individual columns **does not modify** the underlying data already stored in existing tables. Unless your application explicitly handles data conversion and comparison between different collations, it is recommended that you transition existing data in the database to the new collation. This removes the risk that applications may incorrectly modify data, resulting in possible wrong results or silent data loss.   

When a database collation is changed, only new tables will inherit the new database collation by default. There are several alternatives to convert existing data to the new collation:
-  Convert data in-place. To convert the collation for a column in an existing table, see [Set or Change the Column Collation](../../relational-databases/collations/set-or-change-the-column-collation.md). This operation is easy to implement, but may become a blocking issue for large tables and busy aplications. See the following example for an in-place conversion of the `MyString` column to a new collation:

   ```sql
   ALTER TABLE dbo.MyTable
   ALTER COLUMN MyString VARCHAR(50) COLLATE Latin1_General_100_CI_AI_SC_UTF8;
   ```

-  Copy data to new tables that use the new collation, and replace original tables in the same database. Create a new table in the current database that will inherit the database collation, copy the data between the old table and the new table, drop the original table, and rename the new table to the name of the original table. This is a faster operation than an in-place conversion, but may become a challenge when handling complex schemas with dependencies such as Foreign Key contraints, Primary Key constraints, and Triggers. It would also require a final data syncronization between the original and the new table before the final cut-off, if data continues to be changed by applications. See the following example for a "copy and replace" conversion of the `MyString` column to a new collation:

   ```sql
   CREATE TABLE dbo.MyTable2 (MyString VARCHAR(50) COLLATE Latin1_General_100_CI_AI_SC_UTF8); 
   
   INSERT INTO dbo.MyTable2 
   SELECT * FROM dbo.MyTable; 
   
   DROP TABLE dbo.MyTable; 
   
   EXEC sp_rename 'dbo.MyTable2', 'dbo.MyTable’;
   ```

-  Copy data to a new database that uses the new collation, and replace the original database. Create a new database using the new collation, and transfer the data from the original database via tools like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] or the Import/Export Wizard in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. This is a simpler approach for complex schemas. It would also require a final data syncronization between the original and the new databases before the final cut-off, if data continues to be changed by applications.
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To create a new database, requires `CREATE DATABASE` permission in the **master** database, or requires `CREATE ANY DATABASE`, or `ALTER ANY DATABASE` permission.  
  
 To change the collation of an existing database, requires `ALTER` permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To set or change the database collation  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], expand that instance, and then expand **Databases**.  
  
2.  If you are creating a new database, right-click **Databases** and then click **New Database**. If you do not want the default collation, click the **Options** page, and select a collation from the **Collation** drop-down list.  
  
     Alternatively, if the database already exists, right-click the database that you want and click **Properties**. Click the **Options** page, and select a collation from the **Collation** drop-down list.  
  
3.  After you are finished, click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To set the database collation  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use the [COLLATE](~/t-sql/statements/collations.md) clause to specify a collation name. The example creates the database `MyOptionsTest` that uses the `Latin1_General_100_CS_AS_SC` collation. After you create the database, execute the `SELECT` statement to verify the setting.  
  
```sql  
USE master;  
GO  
IF DB_ID (N'MyOptionsTest') IS NOT NULL  
DROP DATABASE MyOptionsTest;  
GO  
CREATE DATABASE MyOptionsTest  
COLLATE Latin1_General_100_CS_AS_SC;  
GO  
  
--Verify the collation setting.  
SELECT name, collation_name  
FROM sys.databases  
WHERE name = N'MyOptionsTest';  
GO  
```  
  
#### To change the database collation  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use the [COLLATE](~/t-sql/statements/collations.md) clause in an [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement to change the collation name. Execute the `SELECT` statement to verify the change.  
  
```sql  
USE master;  
GO  
ALTER DATABASE MyOptionsTest  
COLLATE French_CI_AS ;  
GO  
  
--Verify the collation setting.  
SELECT name, collation_name  
FROM sys.databases  
WHERE name = N'MyOptionsTest';  
GO  
```  
  
## See Also  
 [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)   
 [sys.fn_helpcollations &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [SQL Server Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/sql-server-collation-name-transact-sql.md)   
 [Windows Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/windows-collation-name-transact-sql.md)   
 [COLLATE &#40;Transact-SQL&#41;](~/t-sql/statements/collations.md)   
 [Collation Precedence &#40;Transact-SQL&#41;](../../t-sql/statements/collation-precedence-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
  
