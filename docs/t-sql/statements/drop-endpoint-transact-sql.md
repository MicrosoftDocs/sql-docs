---
title: "DROP ENDPOINT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_ENDPOINT_TSQL"
  - "DROP ENDPOINT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "removing endpoints"
  - "endpoints [SQL Server], removing"
  - "deleting endpoints"
  - "DROP ENDPOINT statement"
  - "dropping endpoints"
ms.assetid: 6aca7412-66a5-4fa4-86b2-061512ff2080
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP ENDPOINT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops an existing endpoint.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP ENDPOINT endPointName  
```  
  
## Arguments  
 *endPointName*  
 Is the name of the endpoint to be removed.  
  
## Remarks  
 The ENDPOINT DDL statements cannot be executed inside a user transaction.  
  
## Permissions  
 User must be a member of the **sysadmin** fixed server role, the owner of the endpoint, or have been granted CONTROL permission on the endpoint.  
  
## Examples  
 The following example removes a previously created endpoint called `sql_endpoint`.  
  
```  
DROP ENDPOINT sql_endpoint;  
```  
  
## See Also  
 [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md)   
 [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
