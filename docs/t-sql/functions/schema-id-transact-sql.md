---
title: "SCHEMA_ID (Transact-SQL) | Microsoft Docs"
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
  - "SCHEMA_ID"
  - "SCHEMA_ID_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "identification numbers [SQL Server], schemas"
  - "schemas [SQL Server], IDs"
  - "SCHEMA_ID function"
  - "IDs [SQL Server], schemas"
  - "default schema IDs"
ms.assetid: c8e34df5-3eea-459f-ae40-050909ce9fda
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# SCHEMA_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the schema ID associated with a schema name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
SCHEMA_ID ( [ schema_name ] )   
```  
  
## Arguments  
  
|Term|Definition|  
|----------|----------------|  
|*schema_name*|Is the name of the schema. *schema_name* is a **sysname**. If *schema_name* is not specified, SCHEMA_ID will return the ID of the default schema of the caller.|  
  
## Return Types  
 **int**  
  
 NULL will be returned if *schema_name* is not a valid schema.  
  
## Remarks  
 SCHEMA_ID will return IDs of system schemas and user-defined schemas. SCHEMA_ID can be called in a select list, in a WHERE clause, and anywhere an expression is allowed.  
  
## Examples  
  
### A. Returning the default schema ID of a caller  
  
```  
SELECT SCHEMA_ID();  
GO  
```  
  
### B. Returning the schema ID of a named schema  
  
```  
USE AdventureWorks2012;  
GO  
SELECT SCHEMA_ID('HumanResources');  
GO   
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Returning the default schema ID of a caller  
  
```  
SELECT SCHEMA_ID();  
```  
  
### D. Returning the schema ID of a named schema  
  
```  
SELECT SCHEMA_ID('dbo');  
```  
  
## See Also  
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [SCHEMA_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/schema-name-transact-sql.md)   
 [sys.schemas &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md)  
  
  

