---
title: "IDENTITY (Property) (Transact-SQL)"
description: CREATE TABLE (Transact-SQL) IDENTITY (Property)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "IDENTITY_TSQL"
  - "IDENTITY"
helpviewer_keywords:
  - "IDENTITY property"
  - "columns [SQL Server], creating"
  - "identity columns [SQL Server], IDENTITY property"
  - "autonumbers, identity numbers"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE TABLE (Transact-SQL) IDENTITY (Property)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  Creates an identity column in a table. This property is used with the CREATE TABLE and ALTER TABLE [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
> [!NOTE]  
>  The IDENTITY property is different from the SQL-DMO **Identity** property that exposes the row identity property of a column.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
IDENTITY [ (seed , increment) ]
```  
  
> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *seed*  
 Is the value that is used for the very first row loaded into the table.  
  
 *increment*  
 Is the incremental value that is added to the identity value of the previous row that was loaded.

 > [!NOTE]
 > In Azure Synapse Analytics values for identity are not incremental due to the distributed architecture of the data warehouse. Please see [Using IDENTITY to create surrogate keys in a Synapse SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-identity#allocation-of-values) for more information.
  
 You must specify both the seed and increment or neither. If neither is specified, the default is (1,1).  
  
## Remarks  
 Identity columns can be used for generating key values. The identity property on a column guarantees the following:  
  
-   Each new value is generated based on the current seed & increment.  
  
-   Each new value for a particular transaction is different from other concurrent transactions on the table.  
  
 The identity property on a column does not guarantee the following:  
  
-   **Uniqueness of the value** - Uniqueness must be enforced by using a **PRIMARY KEY** or **UNIQUE** constraint or **UNIQUE** index.  
 
> [!NOTE]
> Azure Synapse Analytics does not support **PRIMARY KEY** or **UNIQUE** constraint or **UNIQUE** index. Please see [Using IDENTITY to create surrogate keys in a Synapse SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-identity#what-is-a-surrogate-key) for more information.

-   **Consecutive values within a transaction** - A transaction inserting multiple rows is not guaranteed to get consecutive values for the rows because other concurrent inserts might occur on the table. If values must be consecutive then the transaction should use an exclusive lock on the table or use the **SERIALIZABLE** isolation level.  
  
-   **Consecutive values after server restart or other failures** - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might cache identity values for performance reasons and some of the assigned values can be lost during a database failure or server restart. This can result in gaps in the identity value upon insert. If gaps are not acceptable then the application should use its own mechanism to generate key values. Using a sequence generator with the **NOCACHE** option can limit the gaps to transactions that are never committed.  
  
-   **Reuse of values** - For a given identity property with specific seed/increment, the identity values are not reused by the engine. If a particular insert statement fails or if the insert statement is rolled back then the consumed identity values are lost and will not be generated again. This can result in gaps when the subsequent identity values are generated.  
  
 These restrictions are part of the design in order to improve performance, and because they are acceptable in many common situations. If you cannot use identity values because of these restrictions, create a separate table holding a current value and manage access to the table and number assignment with your application.  
  
 If a table with an identity column is published for replication, the identity column must be managed in a way that is appropriate for the type of replication used. For more information, see [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md).  
  
 Only one identity column can be created per table.  
  
 In memory-optimized tables the seed and increment must be set to 1,1. Setting the seed or increment to a value other than 1 results in the following error: The use of seed and increment values other than 1 is not supported with memory optimized tables.  
  
## Examples  
  
### A. Using the IDENTITY property with CREATE TABLE  
 The following example creates a new table using the `IDENTITY` property for an automatically incrementing identification number.  
  
```sql  
USE AdventureWorks2012;  
  
IF OBJECT_ID ('dbo.new_employees', 'U') IS NOT NULL  
   DROP TABLE new_employees;  
GO  
CREATE TABLE new_employees  
(  
 id_num int IDENTITY(1,1),  
 fname varchar (20),  
 minit char(1),  
 lname varchar(30)  
);  
  
INSERT new_employees  
   (fname, minit, lname)  
VALUES  
   ('Karin', 'F', 'Josephs');  
  
INSERT new_employees  
   (fname, minit, lname)  
VALUES  
   ('Pirkko', 'O', 'Koskitalo');  
```  
  
### B. Using generic syntax for finding gaps in identity values  
 The following example shows generic syntax for finding gaps in identity values when data is removed.  
  
> [!NOTE]  
>  The first part of the following [!INCLUDE[tsql](../../includes/tsql-md.md)] script is designed for illustration only. You can run the [!INCLUDE[tsql](../../includes/tsql-md.md)] script that starts with the comment: `-- Create the img table`.  
  
```sql 
-- Here is the generic syntax for finding identity value gaps in data.  
-- The illustrative example starts here.  
SET IDENTITY_INSERT tablename ON;  
DECLARE @minidentval column_type;  
DECLARE @maxidentval column_type;  
DECLARE @nextidentval column_type;  
SELECT @minidentval = MIN($IDENTITY), @maxidentval = MAX($IDENTITY)  
    FROM tablename  
IF @minidentval = IDENT_SEED('tablename')  
   SELECT @nextidentval = MIN($IDENTITY) + IDENT_INCR('tablename')  
   FROM tablename t1  
   WHERE $IDENTITY BETWEEN IDENT_SEED('tablename') AND   
      @maxidentval AND  
      NOT EXISTS (SELECT * FROM tablename t2  
         WHERE t2.$IDENTITY = t1.$IDENTITY +   
            IDENT_INCR('tablename'))  
ELSE  
   SELECT @nextidentval = IDENT_SEED('tablename');  
SET IDENTITY_INSERT tablename OFF;  
-- Here is an example to find gaps in the actual data.  
-- The table is called img and has two columns: the first column   
-- called id_num, which is an increasing identification number, and the   
-- second column called company_name.  
-- This is the end of the illustration example.  
  
-- Create the img table.  
-- If the img table already exists, drop it.  
-- Create the img table.  
IF OBJECT_ID ('dbo.img', 'U') IS NOT NULL  
   DROP TABLE img;  
GO  
CREATE TABLE img (id_num INT IDENTITY(1,1), company_name sysname);  
INSERT img(company_name) VALUES ('New Moon Books');  
INSERT img(company_name) VALUES ('Lucerne Publishing');  
-- SET IDENTITY_INSERT ON and use in img table.  
SET IDENTITY_INSERT img ON;  
  
DECLARE @minidentval SMALLINT;  
DECLARE @nextidentval SMALLINT;  
SELECT @minidentval = MIN($IDENTITY) FROM img  
 IF @minidentval = IDENT_SEED('img')  
    SELECT @nextidentval = MIN($IDENTITY) + IDENT_INCR('img')  
    FROM img t1  
    WHERE $IDENTITY BETWEEN IDENT_SEED('img') AND 32766 AND  
      NOT    EXISTS (SELECT * FROM img t2  
          WHERE t2.$IDENTITY = t1.$IDENTITY + IDENT_INCR('img'))  
 ELSE  
    SELECT @nextidentval = IDENT_SEED('img');  
SET IDENTITY_INSERT img OFF;  
```  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [DBCC CHECKIDENT &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkident-transact-sql.md)   
 [IDENT_INCR &#40;Transact-SQL&#41;](../../t-sql/functions/ident-incr-transact-sql.md)   
 [@@IDENTITY &#40;Transact-SQL&#41;](../../t-sql/functions/identity-transact-sql.md)   
 [IDENTITY &#40;Function&#41; &#40;Transact-SQL&#41;](../../t-sql/functions/identity-function-transact-sql.md)   
 [IDENT_SEED &#40;Transact-SQL&#41;](../../t-sql/functions/ident-seed-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [SET IDENTITY_INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/set-identity-insert-transact-sql.md)   
 [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md)  
  
  
