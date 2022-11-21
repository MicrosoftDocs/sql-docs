---
title: "@@PACK_SENT (Transact-SQL)"
description: "&#x40;&#x40;PACK_SENT (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "@@PACK_SENT"
  - "@@PACK_SENT_TSQL"
helpviewer_keywords:
  - "number of output packets written"
  - "@@PACK_SENT function"
  - "packets [SQL Server], output"
  - "networking [SQL Server], output packets"
  - "connections [SQL Server], packets"
  - "output packets written to network [SQL Server]"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;PACK_SENT (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the number of output packets written to the network by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] since it was last started.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@PACK_SENT  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **integer**  
  
## Remarks  
 To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including packets sent and received, run **sp_monitor**.  
  
## Examples  
 The following example shows the usage of `@@PACK_SENT`.  
  
```sql
SELECT @@PACK_SENT AS 'Pack Sent';  
```  
  
 Here is a sample result set.  
  
```  
Pack Sent  
-----------  
291  
```  
  
## See Also  
 [@@PACK_RECEIVED &#40;Transact-SQL&#41;](../../t-sql/functions/pack-received-transact-sql.md)   
 [sp_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)   
 [System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)  
  
  
