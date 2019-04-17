---
title: "SET ANSI_NULL_DFLT_ON (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/04/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ANSI_NULL_DFLT_ON"
  - "ANSI_NULL_DFLT_ON_TSQL"
  - "SET ANSI_NULL_DFLT_ON"
  - "SET_ANSI_NULL_DFLT_ON_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SET ANSI_NULL_DFLT_ON statement"
  - "ANSI_NULL_DFLT_ON option"
  - "default nullability"
  - "null values [SQL Server], overriding"
  - "overriding default nullability"
ms.assetid: 8c925924-a466-4c8b-aeb2-7e0d341f32db
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET ANSI_NULL_DFLT_ON (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Modifies the behavior of the session to override default nullability of new columns when the **ANSI null default** option for the database is **false**. For more information about setting the value for **ANSI null default**, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```
-- Syntax for SQL Server and Azure SQL Database

SET ANSI_NULL_DFLT_ON {ON | OFF}
```

```
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse

SET ANSI_NULL_DFLT_ON ON
```

## Remarks  
 This setting only affects the nullability of new columns when the nullability of the column is not specified in the CREATE TABLE and ALTER TABLE statements. When SET ANSI_NULL_DFLT_ON is ON, new columns created by using the ALTER TABLE and CREATE TABLE statements allow null values if the nullability status of the column is not explicitly specified. SET ANSI_NULL_DFLT_ON does not affect columns created with an explicit NULL or NOT NULL.  
  
 Both SET ANSI_NULL_DFLT_OFF and SET ANSI_NULL_DFLT_ON cannot be set ON at the same time. If one option is set ON, the other option is set OFF. Therefore, either ANSI_NULL_DFLT_OFF or ANSI_NULL_DFLT_ON can be set ON, or both can be set OFF. If either option is ON, that setting (SET ANSI_NULL_DFLT_OFF or SET ANSI_NULL_DFLT_ON) takes effect. If both options are set OFF, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the value of the **is_ansi_null_default_on** column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
 For a more reliable operation of [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts that are used in databases with different nullability settings, it is better to specify NULL or NOT NULL in CREATE TABLE and ALTER TABLE statements.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically set ANSI_NULL_DFLT_ON to ON when connecting. The default for SET ANSI_NULL_DFLT_ON is OFF for connections from DB-Library applications.  
  
 When SET ANSI_DEFAULTS is ON, SET ANSI_NULL_DFLT_ON is enabled.  
  
 The setting of SET ANSI_NULL_DFLT_ON is set at execute or run time and not at parse time.  
  
 The setting of SET ANSI_NULL_DFLT_ON does not apply when tables are created using the SELECT INTO statement.  
  
 To view the current setting for this setting, run the following query.  
  
```  
DECLARE @ANSI_NULL_DFLT_ON VARCHAR(3) = 'OFF';  
IF ( (1024 & @@OPTIONS) = 1024 ) SET @ANSI_NULL_DFLT_ON = 'ON';  
SELECT @ANSI_NULL_DFLT_ON AS ANSI_NULL_DFLT_ON;  
  
```  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example shows the effects of `SET ANSI_NULL_DFLT_ON` with both settings for the **ANSI null default** database option.  
  
```  
USE AdventureWorks2012;  
GO  
  
-- The code from this point on demonstrates that SET ANSI_NULL_DFLT_ON  
-- has an effect when the 'ANSI null default' for the database is false.  
-- Set the 'ANSI null default' database option to false by executing  
-- ALTER DATABASE.  
ALTER DATABASE AdventureWorks2012 SET ANSI_NULL_DEFAULT OFF;  
GO  
-- Create table t1.  
CREATE TABLE t1 (a TINYINT) ;  
GO   
-- NULL INSERT should fail.  
INSERT INTO t1 (a) VALUES (NULL);  
GO  
  
-- SET ANSI_NULL_DFLT_ON to ON and create table t2.  
SET ANSI_NULL_DFLT_ON ON;  
GO  
CREATE TABLE t2 (a TINYINT);  
GO   
-- NULL insert should succeed.  
INSERT INTO t2 (a) VALUES (NULL);  
GO  
  
-- SET ANSI_NULL_DFLT_ON to OFF and create table t3.  
SET ANSI_NULL_DFLT_ON OFF;  
GO  
CREATE TABLE t3 (a TINYINT);  
GO  
-- NULL insert should fail.  
INSERT INTO t3 (a) VALUES (NULL);  
GO  
  
-- The code from this point on demonstrates that SET ANSI_NULL_DFLT_ON   
-- has no effect when the 'ANSI null default' for the database is true.  
-- Set the 'ANSI null default' database option to true.  
ALTER DATABASE AdventureWorks2012 SET ANSI_NULL_DEFAULT ON  
GO  
  
-- Create table t4.  
CREATE TABLE t4 (a TINYINT);  
GO   
-- NULL INSERT should succeed.  
INSERT INTO t4 (a) VALUES (NULL);  
GO  
  
-- SET ANSI_NULL_DFLT_ON to ON and create table t5.  
SET ANSI_NULL_DFLT_ON ON;  
GO  
CREATE TABLE t5 (a TINYINT);  
GO   
-- NULL INSERT should succeed.  
INSERT INTO t5 (a) VALUES (NULL);  
GO  
  
-- SET ANSI_NULL_DFLT_ON to OFF and create table t6.  
SET ANSI_NULL_DFLT_ON OFF;  
GO  
CREATE TABLE t6 (a TINYINT);  
GO   
-- NULL INSERT should succeed.  
INSERT INTO t6 (a) VALUES (NULL);  
GO  
  
-- Set the 'ANSI null default' database option to false.  
ALTER DATABASE AdventureWorks2012 SET ANSI_NULL_DEFAULT ON;  
GO  
  
-- Drop tables t1 through t6.  
DROP TABLE t1,t2,t3,t4,t5,t6;  
```  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET ANSI_DEFAULTS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-defaults-transact-sql.md)   
 [SET ANSI_NULL_DFLT_OFF &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-null-dflt-off-transact-sql.md)  
  
  
