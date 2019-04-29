---
title: "Accessing Memory-Optimized Tables Using Interpreted Transact-SQL | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 92a44d4d-0e53-4fb0-b890-de264c65c95a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Accessing Memory-Optimized Tables Using Interpreted Transact-SQL
  With only a few exceptions, you can access memory-optimized tables using any [!INCLUDE[tsql](../../includes/tsql-md.md)] query or DML operation (SELECT, INSERT, UPDATE, or DELETE), ad hoc batches, and SQL modules such as stored procedures, table-value functions, triggers, and views.  
  
 Interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] refers to [!INCLUDE[tsql](../../includes/tsql-md.md)] batches or stored procedures other than a natively compiled stored procedure. Interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] access to memory-optimized tables is referred to as interop access.  
  
 Memory-optimized tables can also be accessed using a natively compiled stored procedure. Natively compiled stored procedures are recommended for performance-critical OLTP operations.  
  
 Interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] access is recommended for these scenarios:  
  
-   Ad hoc queries and administrative tasks.  
  
-   Reporting queries, which typically use constructs not available in natively compiled stored procedures (such as window functions).  
  
-   To migrate performance-critical parts of your application to memory-optimized tables, with minimal (or no) application code changes. You can potentially see performance improvements from migrating tables. If you then migrate stored procedures to natively compiled stored procedures, you may see further performance improvement.  
  
-   When a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement is not available for natively compiled stored procedures.  
  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] constructs are not supported in interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures that access data in a memory-optimized table.  
  
|Area|Unsupported|  
|----------|-----------------|  
|Access to tables|TRUNCATE TABLE<br /><br /> MERGE (memory-optimized table as target)<br /><br /> Dynamic and keyset cursors (these automatically degrade to static).<br /><br /> Access from CLR modules, using the context connection.<br /><br /> Referencing a memory-optimized table from an indexed view.|  
|Cross-database|Cross-database queries<br /><br /> Cross-database transactions<br /><br /> Linked servers|  
  
## Table Hints  
 For more information about table hints, see. [Table Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-table). SNAPSHOT isolation was added to support [!INCLUDE[hek_2](../../includes/hek-2-md.md)].  
  
 The following table hints are not supported when accessing a memory-optimized table using interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
|||||  
|-|-|-|-|  
|HOLDLOCK|IGNORE_CONSTRAINTS|IGNORE_TRIGGERS|NOWAIT|  
|PAGLOCK|READCOMMITTED|READCOMMITTEDLOCK|READPAST|  
|READUNCOMMITTED|ROWLOCK|SPATIAL_WINDOW_MAX_CELLS = *integer*|TABLOCK|  
|TABLOCKXX|UPDLOCK|XLOCK||  
  
 When accessing a memory-optimized table from an explicit or implicit transaction using interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] you must include either an isolation level table hint such as SNAPSHOT, REPEATABLEREAD, or SERIALIZABLE, or you can use MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT. For more information, see [Guidelines for Transaction Isolation Levels with Memory-Optimized Tables](memory-optimized-tables.md) and [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options).  
  
> [!NOTE]  
>  An isolation level table hint is not required for memory-optimized tables accessed by queries running in auto-commit mode.  
  
## See Also  
 [Transact-SQL Support for In-Memory OLTP](transact-sql-support-for-in-memory-oltp.md)   
 [Migrating to In-Memory OLTP](migrating-to-in-memory-oltp.md)  
  
  
