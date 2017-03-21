---
title: "sys.sp_flush_log (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_flush_log_TSQL"
  - "sys.sp_flush_log"
  - "sys.sp_flush_log_TSQL"
  - "sp_flush_log"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_flush_log"
ms.assetid: 75cc9f52-3b1f-4754-b1e7-ce0dd3323bc9
caps.latest.revision: 6
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sys.sp_flush_log (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Flushes to disk the  transaction log of the current database, thereby hardening all previously committed delayed durable transactions.  
  
 If you choose to use delayed transaction durability because of the performance benefits, but you also want to have a guaranteed limit on the amount of data that is lost on server crash or failover, then execute `sys.sp_flush_log` on a regular schedule. For example, if you want to make sure you don’t lose more than x seconds worth of data, you would execute `sp_flush_log` every x seconds.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
 Executing `sys.sp_flush_log` guarantees that all previously committed delayed durable transactions are made durable. See the conceptual topic [Control Transaction Durability](../../relational-databases/logs/control-transaction-durability.md) for more information.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```tsql  
  
sys.sp_flush_log  
  
```  
  
#### Parameters  
 None.  
  
## Return Code Values  
 A return code of 1 indicates success.  Any other value indicates failure.  
  
## Result Sets  
 None.  
  
## Sample code  
  
```tsql  
.  
EXECUTE sys.sp_flush_log  
  
```  
  
  