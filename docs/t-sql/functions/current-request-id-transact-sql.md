---
description: "CURRENT_REQUEST_ID (Transact-SQL)"
title: "CURRENT_REQUEST_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CURRENT_REQUEST_ID_TSQL"
  - "CURRENT_REQUEST_ID"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CURRENT_REQUEST_ID"
ms.assetid: 949f6e5f-bf5f-49d6-a763-c443d1d51fe2
author: markingmyname
ms.author: maghan
---
# CURRENT_REQUEST_ID (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This function returns the ID of the current request within the current session.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CURRENT_REQUEST_ID()  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return types
**smallint**
  
## Remarks  
To find exact information about the current session, use @@SPID. For exact information about the current request, use CURRENT_REQUEST_ID().
  
## See also
[@@SPID &#40;Transact-SQL&#41;](../../t-sql/functions/spid-transact-sql.md)
  
  
