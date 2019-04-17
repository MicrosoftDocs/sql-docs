---
title: "MSSQLSERVER_41368 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "41368 (Database Engine error)"
ms.assetid: abc71559-4c4d-4cce-a08f-3299dd167842
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_41368
    
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41368|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQL_IMPLICIT_AND_EXPLICIT_TX_NOT_SUPPORTED|  
|Message Text|Accessing memory optimized tables using the READ COMMITTED isolation level is supported only for autocommit transactions. It is not supported for explicit or implicit transactions. Provide a supported isolation level for the memory optimized table using a table hint, such as WITH (SNAPSHOT).|  
  
## Explanation  
 Accessing memory-optimized tables using the READ COMMITTED isolation level is supported only for autocommit transactions. For more information, see [Transaction Isolation Levels](../../database-engine/transaction-isolation-levels.md).  
  
 When accessing a memory-optimized table from an explicit transaction that was started with BEGIN TRANSACTION, or from an implicit transaction, if IMPLICIT_TRANSACTIONS is set to ON, the READ COMMITTED isolation level is not supported.  
  
## User Action  
 When accessing a memory-optimized table from an explicit or implicit READ COMMITTED transaction, use SNAPSHOT to access the table. This can be achieved by using the table hint WITH (SNAPSHOT) (for more information, see [Guidelines for Transaction Isolation Levels with Memory-Optimized Tables](../in-memory-oltp/memory-optimized-tables.md)) or by setting the database option MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT to ON (for more information, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options)).  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
