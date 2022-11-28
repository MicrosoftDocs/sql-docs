---
title: "SCHEMA_NAME (Transact-SQL)"
description: "SCHEMA_NAME (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SCHEMA_NAME"
  - "SCHEMA_NAME_TSQL"
helpviewer_keywords:
  - "SCHEMA_NAME function"
  - "schemas [SQL Server], names"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SCHEMA_NAME (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the schema name associated with a schema ID.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SCHEMA_NAME ( [ schema_id ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
```sql
SELECT SCHEMA_NAME();  
```  
  
### B. Returning the name of a schema by using an ID  
  
```sql
SELECT SCHEMA_NAME(1);  
```  
  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [SCHEMA_ID &#40;Transact-SQL&#41;](../../t-sql/functions/schema-id-transact-sql.md)   
 [sys.schemas &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  

