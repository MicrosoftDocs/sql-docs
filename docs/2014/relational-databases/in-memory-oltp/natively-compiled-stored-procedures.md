---
title: "Natively Compiled Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
helpviewer_keywords: 
  - "natively compiled stored procedures"
ms.assetid: d5ed432c-10c5-4e4f-883c-ef4d1fa32366
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Natively Compiled Stored Procedures
  Natively compiled stored procedures are [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures compiled to native code that access memory-optimized tables. Natively compiled stored procedures allow for efficient execution of queries and business logic in the stored procedure. For more details about the native compilation process, see [Native Compilation of Tables and Stored Procedures](native-compilation-of-tables-and-stored-procedures.md). For more information about migrating disk-based stored procedures to natively compiled stored procedures, see [Migration Issues for Natively Compiled Stored Procedures](migration-issues-for-natively-compiled-stored-procedures.md).  
  
> [!NOTE]  
>  One difference between interpreted (disk-based) stored procedures and natively compiled stored procedures is that an interpreted stored procedure is compiled at first execution whereas a natively compiled stored procedure is compiled when it is created. With natively compiled stored procedures, many error conditions (arithmetic overflow, type conversion, and some divide-by-zero conditions) can be detected at create time and will cause creation of the natively compiled stored procedure to fail. With interpreted stored procedures, these error conditions will typically not cause a failure when the stored procedure is created, but all executions will fail.  
  
 Topics in this section:  
  
-   [Creating Natively Compiled Stored Procedures](creating-natively-compiled-stored-procedures.md)  
  
-   [Atomic Blocks](atomic-blocks-in-native-procedures.md)  
  
-   [Supported Constructs in Natively Compiled Stored Procedures](supported-features-for-natively-compiled-t-sql-modules.md)  
  
-   [Using Try..Catch in Natively Compiled Stored Procedures](../../database-engine/using-try-catch-in-natively-compiled-stored-procedures.md)  
  
-   [Supported Constructs on Natively Compiled Stored Procedures](supported-ddl-for-natively-compiled-t-sql-modules.md)  
  
-   [Natively Compiled Stored Procedures and Execution Set Options](natively-compiled-stored-procedures-and-execution-set-options.md)  
  
-   [Best Practices for Calling Natively Compiled Stored Procedures](best-practices-for-calling-natively-compiled-stored-procedures.md)  
  
-   [Monitoring Performance of Natively Compiled Stored Procedures](monitoring-performance-of-natively-compiled-stored-procedures.md)  
  
-   [Calling Natively Compiled Stored Procedures from Data Access Applications](calling-natively-compiled-stored-procedures-from-data-access-applications.md)  
  
## See Also  
 [Memory-Optimized Tables](memory-optimized-tables.md)  
  
  
