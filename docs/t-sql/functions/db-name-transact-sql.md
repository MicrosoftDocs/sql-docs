---
title: "DB_NAME (Transact-SQL) | Microsoft Docs"
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
  - "DB_NAME"
  - "DB_NAME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database names [SQL Server], DB_NAME"
  - "names [SQL Server], databases"
  - "viewing database names"
  - "displaying database names"
  - "DB_NAME function"
ms.assetid: e21fb33a-a3ea-49b0-bb6b-8f789a675a0e
caps.latest.revision: 37
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DB_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the database name.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
DB_NAME ( [ database_id ] )  
```  
  
## Arguments  
*database_id*  
Is the identification number (ID) of the database to be returned. *database_id* is **int**, with no default. If no ID is specified, the current database name is returned.
  
## Return types
**nvarchar(128)**
  
## Permissions  
If the caller of **DB_NAME** is not the owner of the database and the database is not **master** or **tempdb**, the minimum permissions required to see the corresponding row are ALTER ANY DATABASE or VIEW ANY DATABASE server-level permission, or CREATE DATABASE permission in the **master** database. The database to which the caller is connected can always be viewed in **sys.databases**.
  
> [!IMPORTANT]  
>  By default, the public role has the VIEW ANY DATABASE permission, allowing all logins to see database information. To block a login from the ability to detect a database, REVOKE the VIEW ANY DATABASE permission from public, or DENY the VIEW ANY DATABASE permission for individual logins.  
  
## Examples  
  
### A. Returning the current database name  
The following example returns the name of the current database.
  
```sql
SELECT DB_NAME() AS [Current Database];  
GO  
```  
  
### B. Returning the database name of a specified database ID  
The following example returns the database name for database ID `3`.
  
```sql
USE master;  
GO  
SELECT DB_NAME(3)AS [Database Name];  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Return the current database name  
  
```sql
SELECT DB_NAME() AS [Current Database];  
```  
  
### D. Return the name of a database by using the database ID  
The following example returns the database name and database_id for each database.
  
```sql
SELECT DB_NAME(database_id) AS [Database], database_id  
FROM sys.databases;  
```  
  
## See also
[DB_ID &#40;Transact-SQL&#41;](../../t-sql/functions/db-id-transact-sql.md)  
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)  
[sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
  
  

