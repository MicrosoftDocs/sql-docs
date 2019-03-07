---
title: "@@PACKET_ERRORS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@PACKET_ERRORS"
  - "@@PACKET_ERRORS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "@@PACKET_ERRORS function"
  - "number of packet errors"
  - "packets [SQL Server], errors"
  - "networking [SQL Server], packet errors"
  - "connections [SQL Server], packets"
ms.assetid: f7da1b80-5cbe-42fa-be71-40c6af16383a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;PACKET_ERRORS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the number of network packet errors that have occurred on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
@@PACKET_ERRORS  
```  
  
## Return Types  
 **integer**  
  
## Remarks  
 To display a report containing several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics, including packet errors, run **sp_monitor**.  
  
## Examples  
 The following example shows using `@@PACKET_ERRORS`.  
  
```  
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
  
  
