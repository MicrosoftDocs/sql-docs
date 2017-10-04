---
title: "PARSENAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "PARSENAME_TSQL"
  - "PARSENAME"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PARSENAME function"
  - "parsing [SQL Server], PARSENAME function"
  - "names [SQL Server], objects"
  - "objects [SQL Server], names"
  - "part of object names [SQL Server]"
ms.assetid: abf34f99-9ee9-460b-85b2-930ca5c4b5ae
caps.latest.revision: 38
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# PARSENAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all_md](../../includes/tsql-appliesto-ss2012-all-md.md)]

  Returns the specified part of an object name. The parts of an object that can be retrieved are the object name, owner name, database name, and server name.  
  
> [!NOTE]  
>  The PARSENAME function does not indicate whether an object by the specified name exists. PARSENAME just returns the specified part of the specified object name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
PARSENAME ( 'object_name' , object_piece )   
```  
  
## Arguments  
 '*object_name*'  
 Is the name of the object for which to retrieve the specified object part. *object_name* is **sysname**. This parameter is an optionally-qualified object name. If all parts of the object name are qualified, this name can have four parts: the server name, the database name, the owner name, and the object name.  
  
 *object_piece*  
 Is the object part to return. *object_piece* is of type **int**, and can have these values:  
  
 1 = Object name  
  
 2 = Schema name  
  
 3 = Database name  
  
 4 = Server name  
  
## Return Types  
 **nchar**  
  
## Remarks  
 PARSENAME returns NULL if one of the following conditions is true:  
  
-   Either *object_name* or *object_piece* is NULL.  
  
-   A syntax error occurs.  
  
 The requested object part has a length of 0 and is not a valid [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier. A zero-length object name renders the complete qualified name as not valid.  
  
## Examples  
 The following example uses `PARSENAME` to return information about the `Person` table in the `AdventureWorks2012` database.  
  
```  
USE AdventureWorks2012;  
SELECT PARSENAME('AdventureWorks2012..Person', 1) AS 'Object Name';  
SELECT PARSENAME('AdventureWorks2012..Person', 2) AS 'Schema Name';  
SELECT PARSENAME('AdventureWorks2012..Person', 3) AS 'Database Name';  
SELECT PARSENAME('AdventureWorks2012..Person', 4) AS 'Server Name';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `Object Name`  
  
 `------------------------------`  
  
 `Person`  
  
 `(1 row(s) affected)`  
  
 `Schema Name`  
  
 `------------------------------`  
  
 `(null)`  
  
 `(1 row(s) affected)`  
  
 `Database Name`  
  
 `------------------------------`  
  
 `AdventureWorks2012`  
  
 `(1 row(s) affected)`  
  
 `Server Name`  
  
 `------------------------------`  
  
 `(null)`  
  
 `(1 row(s) affected)`  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example uses `PARSENAME` to return information about the `Person` table in the `AdventureWorks2012` database.  
  
```  
-- Uses AdventureWorks  
  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 1) AS 'Object Name';  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 2) AS 'Schema Name';  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 3) AS 'Database Name';  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 4) AS 'Server Name';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `Object Name`  
  
 `------------------------------`  
  
 `DimCustomer`  
  
 `(1 row(s) affected)`  
  
 `Schema Name`  
  
 `------------------------------`  
  
 `dbo`  
  
 `(1 row(s) affected)`  
  
 `Database Name`  
  
 `------------------------------`  
  
 `AdventureWorksPDW2012`  
  
 `(1 row(s) affected)`  
  
 `Server Name`  
  
 `------------------------------`  
  
 `(null)`  
  
 `(1 row(s) affected)`  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)  
  
  

