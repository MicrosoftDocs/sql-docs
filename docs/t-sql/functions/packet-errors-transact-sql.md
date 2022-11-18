---
title: "@@PACKET_ERRORS (Transact-SQL)"
description: "&#x40;&#x40;PACKET_ERRORS (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "@@PACKET_ERRORS"
  - "@@PACKET_ERRORS_TSQL"
helpviewer_keywords:
  - "@@PACKET_ERRORS function"
  - "number of packet errors"
  - "packets [SQL Server], errors"
  - "networking [SQL Server], packet errors"
  - "connections [SQL Server], packets"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;PACKET_ERRORS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the number of network packet errors that have occurred on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@PACKET_ERRORS  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **integer**  
  
## Remarks  
 To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including packet errors, run **sp_monitor**.  
  
## Examples  
 The following example shows using `@@PACKET_ERRORS`.  
  
```sql  
SELECT @@PACKET_ERRORS AS 'Packet Errors';  
```  
  
 Here is a sample result set.  
  
```  
Packet Errors  
-------------  
0  
```  
  
## See Also  
 [@@PACK_RECEIVED &#40;Transact-SQL&#41;](../../t-sql/functions/pack-received-transact-sql.md)   
 [@@PACK_SENT &#40;Transact-SQL&#41;](../../t-sql/functions/pack-sent-transact-sql.md)   
 [sp_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)   
 [System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)  
  
  
