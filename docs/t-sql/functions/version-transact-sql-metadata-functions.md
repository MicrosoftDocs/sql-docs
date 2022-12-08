---
title: "VERSION (Transact-SQL)"
description: "Version - Transact SQL Metadata functions"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest"
---
# Version - Transact SQL Metadata functions
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

 Returns the version of [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] or [!INCLUDE[ssPDW_md](../../includes/sspdw-md.md)] running on the appliance.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Azure Synapse Analytics and Parallel Data Warehouse  
VERSION ( )  
```  
  
## Arguments  
  
## General Remarks  
A table name must be specified in a [FROM](../../t-sql/queries/from-transact-sql.md) clause for this function to return results. A result row will be returned for each row in the result set for the query; use [TOP (Transact-SQL)](../../t-sql/queries/top-transact-sql.md) to limit the number of returned rows.  
  
## Examples  
The following example returns the version number.  
  
```sql
SELECT VERSION();  
```  
  
## See Also 
[SESSION_ID (Transact-SQL)](../../t-sql/functions/session-id-transact-sql.md)  
[DB_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/db-name-transact-sql.md)  
  
  
