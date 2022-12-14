---
title: "CURRENT_REQUEST_ID (Transact-SQL)"
description: "CURRENT_REQUEST_ID (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CURRENT_REQUEST_ID_TSQL"
  - "CURRENT_REQUEST_ID"
helpviewer_keywords:
  - "CURRENT_REQUEST_ID"
dev_langs:
  - "TSQL"
---
# CURRENT_REQUEST_ID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This function returns the ID of the current request within the current session.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
CURRENT_REQUEST_ID()  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return types
**smallint**
  
## Remarks  
To find exact information about the current session, use @@SPID. For exact information about the current request, use CURRENT_REQUEST_ID().
  
## See also
[@@SPID &#40;Transact-SQL&#41;](../../t-sql/functions/spid-transact-sql.md)
  
  
