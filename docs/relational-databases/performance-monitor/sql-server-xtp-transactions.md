---
title: "SQL Server XTP Transactions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 443d67e4-1c7f-41d7-b18d-2d657f58c22a
caps.latest.revision: 7
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SQL Server XTP Transactions
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  The SQL Server XTP Transactions performance object contains counters related to transactions involving In-Memory OLTP in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 This table describes the **SQL Server XTP Transactions** counters.  
  
|Counter|Description|  
|-------------|-----------------|  
|**Cascading aborts/sec**|The number of transactions that rolled back to due a commit dependency rollback (on average), per second.|  
|**Commit dependencies taken/sec**|The number of commit dependencies taken by transactions (on average), per second.|  
|**Read-only transactions prepared/sec**|The number of read-only transactions that were prepared for commit processing, per second.|  
|**Save point refreshes/sec**|The number of times a savepoint was "refreshed", (on average), per second. A savepoint refresh is when an existing savepoint is reset to the current point in the transaction's lifetime.|  
|**Save point rollbacks/sec**|The number of times a transaction rolled back to a save point (on average), per second.|  
|**Save points created /sec**|The number of save points created (on average), per second.|  
|**Transaction validation failure/sec**|The number of transactions that failed validation processing (on average), per second.|  
|**Transactions aborted by user/sec**|The number of transactions that were aborted by the user (on average), per second.|  
|**Transactions aborted/sec**|The number of transactions that aborted (both by the user and the system, on average), per second.|  
|**Transactions created/sec**|The number of transactions created in the system (on average), per second.<br /><br /> XTP transactions are counted differently than disk-based transactions (as reflected in Databases:Transactions/sec). For example, Transactions created/sec counts read/only transactions, while Databases:Transactions/sec does not.|  
  
## See Also  
 [SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)  
  
  