---
title: "DB_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DB_ID_TSQL"
  - "DB_ID"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "viewing database ID numbers"
  - "IDs [SQL Server], databases"
  - "database IDs [SQL Server]"
  - "identification numbers [SQL Server], databases"
  - "displaying database ID numbers"
  - "DB_ID function"
ms.assetid: 7b3aef89-a6fd-4144-b468-bf87ebf381b8
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DB_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

This function returns the database identification (ID) number of a specified database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DB_ID ( [ 'database_name' ] )   
```  
  
## Arguments  
'*database_name*'  
The name of the database whose database ID number `DB_ID` will return. If the call to `DB_ID` omits *database_name*, `DB_ID` returns the ID of the current database.
  
## Return types
**int**

## Remarks
`DB_ID` may only be used to return the database identifier of the current database in Azure SQL Database. NULL is returned if the specified database name is other than the current database.
  
## Permissions  
If the caller of `DB_ID` does not own a specific non-**master** or non-**tempdb** database, `ALTER ANY DATABASE` or `VIEW ANY DATABASE` server-level permissions at minimum are required to see the corresponding `DB_ID` row. For the **master** database, `DB_ID` needs `CREATE DATABASE` permission at minimum. The database to which the caller connects will always appear in **sys.databases**.
  
> [!IMPORTANT]  
>  By default, the public role has the `VIEW ANY DATABASE` permission, which allows all logins to see database information. To prevent a login from detecting a database, `REVOKE` the `VIEW ANY DATABASE` permission from public, or `DENY` the `VIEW ANY DATABASE` permission for individual logins.  
  
## Examples  
  
### A. Returning the database ID of the current database  
This example returns the database ID of the current database.
  
```sql
SELECT DB_ID() AS [Database ID];  
GO  
```  
  
### B. Returning the database ID of a specified database  
This example returns the database ID of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.
  
```sql
SELECT DB_ID(N'AdventureWorks2008R2') AS [Database ID];  
GO  
```  
  
### C. Using DB_ID to specify the value of a system function parameter  
This example uses `DB_ID` to return the database ID of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database in the system function `sys.dm_db_index_operational_stats`. The function takes a database ID as the first parameter.
  
```sql
DECLARE @db_id int;  
DECLARE @object_id int;  
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
  
### D. Return the ID of the current database  
This example returns the database ID of the current database.
  
```sql
SELECT DB_ID();  
```  
  
### E. Return the ID of a named database.  
This example returns the database ID of the AdventureWorksDW2012 database.
  
```sql
SELECT DB_ID('AdventureWorksPDW2012');  
```  
  
## See also
[DB_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/db-name-transact-sql.md)  
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)  
[sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
[sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)
  
  

