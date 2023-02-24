---
description: "MSSQLSERVER_41368"
title: "MSSQLSERVER_41368 | Microsoft Docs"
ms.custom: ""
ms.date: "05/25/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "41368 (Database Engine error)"
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_41368
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41368|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQL_IMPLICIT_AND_EXPLICIT_TX_NOT_SUPPORTED|  
|Message Text|Accessing memory optimized tables using the READ COMMITTED isolation level is supported only for autocommit transactions. It is not supported for explicit or implicit transactions. Provide a supported isolation level for the memory optimized table using a table hint, such as WITH (SNAPSHOT).|  
  
## Explanation  
Accessing memory-optimized tables using the READ COMMITTED isolation level is supported only for autocommit transactions. For more information, see [Transactions with In-Memory Tables and Procedures](~/relational-databases/in-memory-oltp/transactions-with-memory-optimized-tables.md).  
  
When accessing a memory-optimized table from an explicit transaction that was started with BEGIN TRANSACTION, or from an implicit transaction, if IMPLICIT_TRANSACTIONS is set to ON, the READ COMMITTED isolation level is not supported.  
  
## User Action  
When accessing a memory-optimized table from an explicit or implicit READ COMMITTED transaction, use SNAPSHOT to access the table. This can be achieved by using the table hint WITH (SNAPSHOT) (for more information, see [Transactions with In-Memory Tables and Procedures](~/relational-databases/in-memory-oltp/transactions-with-memory-optimized-tables.md)) or by setting the database option MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT to ON (for more information, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](~/t-sql/statements/alter-database-transact-sql.md)).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
