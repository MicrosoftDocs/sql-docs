---
title: "DB_NAME (Transact-SQL)"
description: "DB_NAME (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "07/30/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DB_NAME"
  - "DB_NAME_TSQL"
helpviewer_keywords:
  - "database names [SQL Server], DB_NAME"
  - "names [SQL Server], databases"
  - "viewing database names"
  - "displaying database names"
  - "DB_NAME function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DB_NAME (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the name of a specified database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DB_NAME ( [ database_id ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*database_id*  

The identification number (ID) of the database whose name `DB_NAME` will return. If the call to `DB_NAME` omits *database_id*, `DB_NAME` returns the name of the current database.
  
## Return types
**nvarchar(128)**
  
## Permissions  

If the caller of `DB_NAME` does not own a specific non-**master** or non-**tempdb** database, `ALTER ANY DATABASE` or `VIEW ANY DATABASE` server-level permissions at minimum are required to see the corresponding `DB_ID` row. For the **master** database, `DB_ID` needs `CREATE DATABASE` permission at minimum. The database to which the caller connects will always appear in **sys.databases**.
  
> [!IMPORTANT]  
>  By default, the public role has the `VIEW ANY DATABASE` permission, which allows all logins to see database information. To prevent a login from detecting a database, `REVOKE` the `VIEW ANY DATABASE` permission from public, or `DENY` the `VIEW ANY DATABASE` permission for individual logins.
  
## Examples  
  
### A. Returning the current database name  
This example returns the name of the current database.
  
```sql
SELECT DB_NAME() AS [Current Database];  
GO  
```  
  
### B. Returning the database name of a specified database ID  
This example returns the database name for database ID `3`.
  
```sql
USE master;  
GO  
SELECT DB_NAME(3) AS [Database Name];  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Return the current database name  
  
```sql
SELECT DB_NAME() AS [Current Database];  
```  
  
### D. Return the name of a database by using the database ID  
This example returns the database name and database_id for each database.
  
```sql
SELECT DB_NAME(database_id) AS [Database], database_id  
FROM sys.databases;  
```  
  
## See also
[DB_ID &#40;Transact-SQL&#41;](../../t-sql/functions/db-id-transact-sql.md)  
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)  
[sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
  
  

