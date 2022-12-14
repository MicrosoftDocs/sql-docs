---
title: "OBJECT_ID (Transact-SQL)"
description: "OBJECT_ID (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OBJECT_ID"
  - "OBJECT_ID_TSQL"
helpviewer_keywords:
  - "objects [SQL Server], IDs"
  - "identification numbers [SQL Server], database objects"
  - "checking object exists"
  - "IDs [SQL Server], database objects"
  - "OBJECT_ID function"
  - "database objects [SQL Server], IDs"
  - "displaying object IDs"
  - "viewing object IDs"
  - "verifying object exists"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# OBJECT_ID (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the database object identification number of a schema-scoped object.  
  
> [!IMPORTANT]  
>  Objects that are not schema-scoped, such as DDL triggers, cannot be queried by using OBJECT_ID. For objects that are not found in the [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md) catalog view, obtain the object identification numbers by querying the appropriate catalog view. For example, to return the object identification number of a DDL trigger, use `SELECT OBJECT_ID FROM sys.triggers WHERE name = 'DatabaseTriggerLog``'`.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
OBJECT_ID ( '[ database_name . [ schema_name ] . | schema_name . ]   
  object_name' [ ,'object_type' ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 **'** *object_name* **'**  
 Is the object to be used. *object_name* is either **varchar** or **nvarchar**. If *object_name* is **varchar**, it is implicitly converted to **nvarchar**. Specifying the database and schema names is optional.  
  
 **'** *object_type* **'**  
 Is the schema-scoped object type. *object_type* is either **varchar** or **nvarchar**. If *object_type* is **varchar**, it is implicitly converted to **nvarchar**. For a list of object types, see the **type** column in [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).  
  
## Return Types  
 **int**  
  
## Exceptions  
 For a spatial index, OBJECT_ID returns NULL.  
  
 Returns NULL on error.  
  
 A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as OBJECT_ID may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Remarks  
 When the parameter to a system function is optional, the current database, host computer, server user, or database user is assumed. Built-in functions must always be followed by parentheses.  
  
 When a temporary table name is specified, the database name must come before the temporary table name, unless the current database is **tempdb**. For example: `SELECT OBJECT_ID('tempdb..#mytemptable')`.  
  
 System functions can be used in the select list, in the WHERE clause, and anywhere an expression is allowed. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md) and [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md).  
  
## Examples  
  
### A. Returning the object ID for a specified object  
 The following example returns the object ID for the `Production.WorkOrder` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
USE master;  
GO  
SELECT OBJECT_ID(N'AdventureWorks2012.Production.WorkOrder') AS 'Object ID';  
GO  
```  
  
### B. Verifying that an object exists  
 The following example checks for the existence of a specified table by verifying that the table has an object ID. If the table exists, it is deleted. If the table does not exist, the `DROP TABLE` statement is not executed.  
  
```sql  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID (N'dbo.AWBuildVersion', N'U') IS NOT NULL  
DROP TABLE dbo.AWBuildVersion;  
GO  
```  
  
### C. Using OBJECT_ID to specify the value of a system function parameter  
 The following example returns information for all indexes and partitions of the `Person.Address` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database by using the [sys.dm_db_index_operational_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md) function.  
 
> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
> [!IMPORTANT]  
>  When you are using the [!INCLUDE[tsql](../../includes/tsql-md.md)] functions DB_ID and OBJECT_ID to return a parameter value, always make sure that a valid ID is returned. If the database or object name cannot be found, such as when they do not exist or are spelled incorrectly, both functions will return NULL. The **sys.dm_db_index_operational_stats** function interprets NULL as a wildcard value that specifies all databases or all objects. Because this can be an unintentional operation, the examples in this section demonstrate the safe way to determine database and object IDs.
  
```sql  
DECLARE @db_id INT;  
DECLARE @object_id INT;  
SET @db_id = DB_ID(N'AdventureWorks2012');  
SET @object_id = OBJECT_ID(N'AdventureWorks2012.Person.Address');  
IF @db_id IS NULL   
  BEGIN;  
    PRINT N'Invalid database';  
  END;  
ELSE IF @object_id IS NULL  
  BEGIN;  
    PRINT N'Invalid object';  
  END;  
ELSE  
  BEGIN;  
    SELECT * FROM sys.dm_db_index_operational_stats(@db_id, @object_id, NULL, NULL);  
  END;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### D: Returning the object ID for a specified object  
 The following example returns the object ID for the `FactFinance` table in the [!INCLUDE[ssawPDW](../../includes/ssawpdw-md.md)] database.  
  
```sql  
SELECT OBJECT_ID('AdventureWorksPDW2012.dbo.FactFinance') AS 'Object ID';  
```  
  
## See Also  
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)   
 [OBJECT_DEFINITION &#40;Transact-SQL&#41;](../../t-sql/functions/object-definition-transact-sql.md)   
 [OBJECT_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/object-name-transact-sql.md)  
  
  

