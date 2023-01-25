---
description: "MSSQLSERVER_41359"
title: "MSSQLSERVER_41359 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "41359 (Database Engine error)"
ms.assetid: 02b717c7-9170-4676-b0f6-4dec9eb5b5d4
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_41359
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41359|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQL_READ_COMMITTED_SNAPSHOT_NOT_SUPPORTED|  
|Message Text|A query that accesses memory optimized tables using the READ COMMITTED isolation level, cannot access disk based tables when the database option READ_COMMITTED_SNAPSHOT is set to ON. Provide a supported isolation level for the memory optimized table using a table hint, such as WITH (SNAPSHOT).|  
  
## Explanation  
The database with READ_COMMITTED_SNAPSHOT=ON does not support the transactions that access both memory-optimized tables and disk based tables.  
  
## User Action  
Set READ_COMMITTED_SNAPSHOT to OFF or supply a supported isolation level for the memory-optimized table using a table-level hint, such as WITH (SNAPSHOT). For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
