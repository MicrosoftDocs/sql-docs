---
title: "Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "10/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 14dddf81-b502-49dc-a6b6-d18b1ae32d2b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Memory-Optimized Tables
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] In-Memory OLTP helps improve performance of OLTP applications through efficient, memory-optimized data access, native compilation of business logic, and lock- and latch free algorithms. The In-Memory OLTP feature includes memory-optimized tables and table types, as well as native compilation of [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures for efficient access to these tables.  
  
 For more information about memory-optimized tables, see:  
  
-   [Introduction to Memory-Optimized Tables](memory-optimized-tables.md)  
  
     Details what memory-optimized tables are and provides information about data durability, accessing data in memory-optimized tables, and performance and scalability.  
  
-   [Native Compilation of Tables and Stored Procedures](../in-memory-oltp/natively-compiled-stored-procedures.md)  
  
     Details how memory-optimized tables and natively compiled stored procedures are compiled into DLLs, and provides related security considerations.  
  
-   [Altering Memory-Optimized Tables](altering-memory-optimized-tables.md)  
  
     Guidelines for updating memory-optimized tables (including changing table columns, indexes, and bucket_count).  
  
-   [Understanding Transactions on Memory-Optimized Tables](../../database-engine/understanding-transactions-on-memory-optimized-tables.md)  
  
     This section provides several topics related to performing transactions on memory-optimized tables including transaction isolation levels, and cross-container transactions.  
  
-   [Application Pattern for Partitioning Memory-Optimized Tables](application-pattern-for-partitioning-memory-optimized-tables.md)  
  
     Detailed code sample that shows how to emulate partitioned tables when using memory-optimized tables.  
  
-   [Statistics for Memory-Optimized Tables](statistics-for-memory-optimized-tables.md)  
  
     Details how statistics are compiled for memory-optimized tables and how to maintain and manually update statistics for memory-optimized tables.  
  
-   [Collations and Code Pages](../../database-engine/collations-and-code-pages.md)  
  
     Details the restrictions on supported collations and code pages for memory-optimized tables.  
  
-   [Table and Row Size in Memory-Optimized Tables](table-and-row-size-in-memory-optimized-tables.md)  
  
     Details the 8060 byte limit on memory-optimized table rows, and provides an example for calculating table and row sizes.  
  
-   [A Guide to Query Processing for Memory-Optimized Tables](a-guide-to-query-processing-for-memory-optimized-tables.md)  
  
     Provides an overview of query processing for both memory-optimized tables, and natively compiled stored procedures.  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](in-memory-oltp-in-memory-optimization.md)  
  
  
