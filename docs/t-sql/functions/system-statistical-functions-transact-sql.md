---
title: "System Statistical Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "statistical functions [SQL Server]"
  - "system statistical functions [SQL Server]"
  - "functions [SQL Server], statistical"
ms.assetid: 45828c67-1b9a-4653-bb24-86246084d8ba
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# System Statistical Functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  The following scalar functions return statistical information about the system:  
  
|||  
|-|-|  
|[@@CONNECTIONS](../../t-sql/functions/connections-transact-sql.md)|[@@PACK_RECEIVED](../../t-sql/functions/pack-received-transact-sql.md)|  
|[@@CPU_BUSY](../../t-sql/functions/cpu-busy-transact-sql.md)|[@@PACK_SENT](../../t-sql/functions/pack-sent-transact-sql.md)|  
|[fn_virtualfilestats](../../relational-databases/system-functions/sys-fn-virtualfilestats-transact-sql.md)|[@@TIMETICKS](../../t-sql/functions/timeticks-transact-sql.md)|  
|[@@IDLE](../../t-sql/functions/idle-transact-sql.md)|[@@TOTAL_ERRORS](../../t-sql/functions/total-errors-transact-sql.md)|  
|[@@IO_BUSY](../../t-sql/functions/io-busy-transact-sql.md)|[@@TOTAL_READ](../../t-sql/functions/total-read-transact-sql.md)|  
|[@@PACKET_ERRORS](../../t-sql/functions/packet-errors-transact-sql.md)|[@@TOTAL_WRITE](../../t-sql/functions/total-write-transact-sql.md)|  
  
 All system statistical functions are nondeterministic. This means these functions do not always return the same results every time they are called, even with the same set of input values. For more information about function determinism, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
  
