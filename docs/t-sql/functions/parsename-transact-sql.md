---
title: "PARSENAME (Transact-SQL)"
description: "PARSENAME (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "06/02/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "PARSENAME_TSQL"
  - "PARSENAME"
helpviewer_keywords:
  - "PARSENAME function"
  - "parsing [SQL Server], PARSENAME function"
  - "names [SQL Server], objects"
  - "objects [SQL Server], names"
  - "part of object names [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# PARSENAME (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the specified part of an object name. The parts of an object that can be retrieved are the object name, schema name, database name, and server name. 
  
> [!NOTE]  
>  The PARSENAME function does not indicate whether an object by the specified name exists. PARSENAME just returns the specified part of the specified object name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
PARSENAME ('object_name' , object_piece )
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*'object_name'*
Is the parameter that holds the name of the object for which to retrieve the specified object part. This parameter is an optionally-qualified object name. If all parts of the object name are qualified, this name can have four parts: the server name, the database name, the schema name, and the object name.  Each part of the 'object_name' string is type *sysname* which is equivalent to nvarchar(128) or 256 bytes. If any part of the string exceeds 256 bytes, PARSENAME will return NULL for that part as it is not a valid sysname.
  
*object_piece*  
Is the object part to return. *object_piece* is of type **int**, and can have these values:  
    1 = Object name  
    2 = Schema name  
    3 = Database name  
    4 = Server name  
  
## Return Type

 **sysname**
  
## Remarks

 PARSENAME returns NULL if one of the following conditions is true:  
  
-   Either *object_name* or *object_piece* is NULL.  
  
-   A syntax error occurs.  
  
 The requested object part has a length of 0 and is not a valid [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier. A zero-length object name renders the complete qualified name as not valid.  
  
## Examples

 The following example uses `PARSENAME` to return information about the `Person` table in the `AdventureWorks2012` database.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 1) AS 'Object Name';  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 2) AS 'Schema Name';  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 3) AS 'Database Name';  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 4) AS 'Server Name';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
```
Object Name
------------------------------
DimCustomer

(1 row(s) affected)

Schema Name
------------------------------
dbo

(1 row(s) affected)

Database Name
------------------------------
AdventureWorksPDW2012

(1 row(s) affected)

Server Name
------------------------------
(null)

(1 row(s) affected)
```
  
## See Also

- [QUOTENAME &#40;Transact-SQL&#41;](../../t-sql/functions/quotename-transact-sql.md)  
- [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
- [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
- [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)  
  
  

