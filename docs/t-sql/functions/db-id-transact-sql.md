---
title: "DB_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DB_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the database identification (ID) number.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DB_ID ( [ 'database_name' ] )   
```  
  
## Arguments  
'*database_name*'  
Is the database name used to return the corresponding database ID. *database_name* is **sysname**. If *database_name* is omitted, the current database ID is returned.
  
## Return types
**int**
  
## Permissions  
If the caller of **DB_ID** is not the owner of the database and the database is not **master** or **tempdb**, the minimum permissions required to see the corresponding row are ALTER ANY DATABASE or VIEW ANY DATABASE server-level permission, or CREATE DATABASE permission in the **master** database. The database to which the caller is connected can always be viewed in **sys.databases**.
  
> [!IMPORTANT]  
>  By default, the public role has the VIEW ANY DATABASE permission, allowing all logins to see database information. To block a login from the ability to detect a database, REVOKE the VIEW ANY DATABASE permission from public, or DENY the VIEW ANY DATABASE permission for individual logins.  
  
## Examples  
  
### A. Returning the database ID of the current database  
The following example returns the database ID of the current database.
  
```sql
SELECT DB_ID() AS [Database ID];  
GO  
```  
  
### B. Returning the database ID of a specified database  
The following example returns the database ID of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.
  
```sql
SELECT DB_ID(N'AdventureWorks2008R2') AS [Database ID];  
GO  
```  
  
### C. Using DB_ID to specify the value of a system function parameter  
The following example uses `DB`_`ID` to return the database ID of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database in the system function `sys.dm_db`\_`index`\_`operational`\_`stats`. The function takes a database ID as the first parameter.
  
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
The following example returns the database ID of the current database.
  
```sql
SELECT DB_ID();  
```  
  
### E. Return the ID of a named database.  
The following example returns the database ID of the AdventureWorksDW2012 database.
  
```sql
SELECT DB_ID('AdventureWorksPDW2012');  
```  
  
## See also
[DB_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/db-name-transact-sql.md)  
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)  
[sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
[sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)
  
  

