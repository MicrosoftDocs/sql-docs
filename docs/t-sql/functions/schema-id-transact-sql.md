---
title: "SCHEMA_ID (Transact-SQL)"
description: "SCHEMA_ID (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SCHEMA_ID"
  - "SCHEMA_ID_TSQL"
helpviewer_keywords:
  - "identification numbers [SQL Server], schemas"
  - "schemas [SQL Server], IDs"
  - "SCHEMA_ID function"
  - "IDs [SQL Server], schemas"
  - "default schema IDs"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SCHEMA_ID (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the schema ID associated with a schema name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
SCHEMA_ID ( [ schema_name ] )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
```sql  
SELECT SCHEMA_ID();  
```  
  
### B. Returning the schema ID of a named schema  
  
```sql  
SELECT SCHEMA_ID('dbo');  
```  
  
## See Also  
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [SCHEMA_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/schema-name-transact-sql.md)   
 [sys.schemas &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md)  
  
  

