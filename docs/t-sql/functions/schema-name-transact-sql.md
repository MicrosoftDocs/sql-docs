---
title: "SCHEMA_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SCHEMA_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the schema name associated with a schema ID.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
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
```  
  
### B. Returning the name of a schema by using an ID  
  
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
  
  

