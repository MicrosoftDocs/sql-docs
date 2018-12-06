---
title: "Set or Change the Database Collation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "collations [SQL Server], database"
  - "database collations [SQL Server]"
ms.assetid: 1379605c-1242-4ac8-ab1b-e2a2b5b1f895
author: stevestein
ms.author: sstein
manager: craigg
---
# Set or Change the Database Collation
  This topic describes how set and change the database collation in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. If no collation is specified, the server collation is used.  
  
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
  
-   Windows Unicode-only collations can only be used with the COLLATE clause to apply collations to the `nchar`, `nvarchar`, and `ntext` data types on column level and expression-level data. They cannot be used with the COLLATE clause to change the collation of a database or server instance.  
  
-   If the specified collation or the collation used by the referenced object uses a code page that is not supported by Windows, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] displays an error.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   You can find the supported collation names in [Windows Collation Name &#40;Transact-SQL&#41;](/sql/t-sql/statements/windows-collation-name-transact-sql) and [SQL Server Collation Name &#40;Transact-SQL&#41;](/sql/t-sql/statements/sql-server-collation-name-transact-sql); or you can use the [sys.fn_helpcollations &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-helpcollations-transact-sql) system function.  
  
-   When you change the database collation, you change the following:  
  
    -   Any `char`, `varchar`, `text`, `nchar`, `nvarchar`, or `ntext` columns in system tables are changed to the new collation.  
  
    -   All existing `char`, `varchar`, `text`, `nchar`, `nvarchar`, or `ntext` parameters and scalar return values for stored procedures and user-defined functions are changed to the new collation.  
  
    -   The `char`, `varchar`, `text`, `nchar`, `nvarchar`, or `ntext` system data types, and all user-defined data types based on these system data types, are changed to the new default collation.  
  
-   You can change the collation of any new objects that are created in a user database by using the COLLATE clause of the [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql) statement. This statement does not change the collation of the columns in any existing user-defined tables. These can be changed by using the COLLATE clause of [ALTER TABLE](/sql/t-sql/statements/alter-table-transact-sql).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 CREATE DATABASE  
 Requires CREATE DATABASE permission in the **master** database, or requires CREATE ANY DATABASE, or ALTER ANY DATABASE permission.  
  
 ALTER DATABASE  
 Requires ALTER permission on the database.  
  
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
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use the [COLLATE](/sql/t-sql/statements/collations) clause to specify a collation name. The example creates the database `MyOptionsTest` that uses the `Latin1_General_100_CS_AS_SC` collation. After you create the database, execute the `SELECT` statement to verify the setting.  
  
```tsql  
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
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use the [COLLATE](/sql/t-sql/statements/collations) clause in an [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql) statement to change the collation name. Execute the `SELECT` statement to verify the change.  
  
```tsql  
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
 [Collation and Unicode Support](collation-and-unicode-support.md)   
 [sys.fn_helpcollations &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-helpcollations-transact-sql)   
 [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql)   
 [SQL Server Collation Name &#40;Transact-SQL&#41;](/sql/t-sql/statements/sql-server-collation-name-transact-sql)   
 [Windows Collation Name &#40;Transact-SQL&#41;](/sql/t-sql/statements/windows-collation-name-transact-sql)   
 [COLLATE &#40;Transact-SQL&#41;](/sql/t-sql/statements/collations)   
 [Collation Precedence &#40;Transact-SQL&#41;](/sql/t-sql/statements/collation-precedence-transact-sql)   
 [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql)   
 [ALTER TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-table-transact-sql)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)  
  
  
