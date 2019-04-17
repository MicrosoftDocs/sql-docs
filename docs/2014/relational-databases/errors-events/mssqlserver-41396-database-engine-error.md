---
title: "MSSQLSERVER_41396 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "41396 (Database Engine error)"
ms.assetid: 4ff04042-8367-46f3-8a16-c94682d6eedb
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_41396
    
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41396|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|MAX_SORT_ROWS_EXCEEDED|  
|Message Text|The sort operation exceeded the buffer limit. The stored procedure execution was aborted. Consult SQL Server Books Online for more information.|  
  
## Explanation  
 Natively compiled stored procedures perform sort operations in memory. There is a limit on the size of the sort buffer. This error means that the size of the sort buffer exceeds this limit. The sort operation and the stored procedure execution aborted.  
  
 The size of each row or entry in the sort buffer is determined by the number of rows sorted as well as the number of joins and the number and type of aggregate functions in the query. By simplifying the query, you can reduce the size of each row thereby fitting more rows in the sort buffer. The size of the rows in the base tables does not affect the size of each row or entry in the sort buffer.  
  
## User Action  
 Select fewer rows or decrease the complexity of the query by removing joins or aggregate functions.  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
