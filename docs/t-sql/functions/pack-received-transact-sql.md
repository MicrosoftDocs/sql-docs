---
title: "@@PACK_RECEIVED (Transact-SQL)"
description: "@@PACK_RECEIVED (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@PACK_RECEIVED_TSQL"
  - "@@PACK_RECEIVED"
helpviewer_keywords:
  - "@@PACK_RECEIVED function"
  - "number of packets read"
  - "packets [SQL Server], number read"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;PACK_RECEIVED (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the number of input packets read from the network by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] since it was last started.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
@@PACK_RECEIVED  
```  
  
## Return Types
 **integer**  
  
## Remarks  
 To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including packets sent and received, run **sp_monitor**.  
  
## Examples  
 The following example shows the usage of `@@PACK_RECEIVED`.  
  
```sql  
SELECT @@PACK_RECEIVED AS 'Packets Received';   
```  
  
 Here is a sample result set.  
  
```  
Packets Received  
----------------  
128  
```  
  
## See Also  
 [@@PACK_SENT](../../t-sql/functions/pack-sent-transact-sql.md)   
 [sp_monitor](../../relational-databases/system-stored-procedures/sp-monitor-transact-sql.md)   
 [System Statistical Functions](../../t-sql/functions/system-statistical-functions-transact-sql.md)  
  
  
