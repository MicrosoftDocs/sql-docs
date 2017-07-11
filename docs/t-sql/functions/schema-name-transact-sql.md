---
title: "SCHEMA_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "SCHEMA_NAME"
  - "SCHEMA_NAME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SCHEMA_NAME function"
  - "schemas [SQL Server], names"
ms.assetid: 20071b77-2b6e-4ce7-a8e3-fa71480baf73
caps.latest.revision: 27
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# SCHEMA_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the schema name associated with a schema ID.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
SCHEMA_NAME ( [ schema_id ] )  
```  
  
## Arguments  
  
|Term|Definition|  
|----------|----------------|  
|*schema_id*|The ID of the schema. *schema_id* is an **int**. If *schema_id* is not defined, SCHEMA_NAME will return the name of the default schema of the caller.|  
  
## Return Types  
 **sysname**  
  
 Returns NULL when *schema_id* is not a valid ID.  
  
## Remarks  
 SCHEMA_NAME returns names of system schemas and user-defined schemas. SCHEMA_NAME can be called in a select list, in a WHERE clause, and anywhere an expression is allowed.  
  
## Examples  
  
### A. Returning the name of the default schema of the caller  
  
```  
SELECT SCHEMA_NAME();  
GO  
```  
  
### B. Returning the name of a schema by using an ID  
  
```  
USE AdventureWorks2012;  
GO  
SELECT SCHEMA_NAME(5);  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Returning the name of the default schema of the caller  
  
```  
SELECT SCHEMA_NAME();  
```  
  
### D. Returning the name of a schema by using an ID  
  
```  
SELECT SCHEMA_NAME(1);  
```  
  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [SCHEMA_ID &#40;Transact-SQL&#41;](../../t-sql/functions/schema-id-transact-sql.md)   
 [sys.schemas &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  

