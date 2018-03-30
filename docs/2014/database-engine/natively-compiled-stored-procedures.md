---
title: "Natively Compiled Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "natively compiled stored procedures"
ms.assetid: d5ed432c-10c5-4e4f-883c-ef4d1fa32366
caps.latest.revision: 54
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Natively Compiled Stored Procedures
  Natively compiled stored procedures are [!INCLUDE[tsql](../includes/tsql-md.md)] stored procedures compiled to native code that access memory-optimized tables. Natively compiled stored procedures allow for efficient execution of queries and business logic in the stored procedure. For more details about the native compilation process, see [Native Compilation of Tables and Stored Procedures](../../2014/database-engine/native-compilation-of-tables-and-stored-procedures.md). For more information about migrating disk-based stored procedures to natively compiled stored procedures, see [Migration Issues for Natively Compiled Stored Procedures](../../2014/database-engine/migration-issues-for-natively-compiled-stored-procedures.md).  
  
> [!NOTE]  
>  One difference between interpreted (disk-based) stored procedures and natively compiled stored procedures is that an interpreted stored procedure is compiled at first execution whereas a natively compiled stored procedure is compiled when it is created. With natively compiled stored procedures, many error conditions (arithmetic overflow, type conversion, and some divide-by-zero conditions) can be detected at create time and will cause creation of the natively compiled stored procedure to fail. With interpreted stored procedures, these error conditions will typically not cause a failure when the stored procedure is created, but all executions will fail.  
  
 Topics in this section:  
  
-   [Creating Natively Compiled Stored Procedures](../../2014/database-engine/creating-natively-compiled-stored-procedures.md)  
  
-   [Atomic Blocks](../../2014/database-engine/atomic-blocks.md)  
  
-   [Supported Constructs in Natively Compiled Stored Procedures](../../2014/database-engine/supported-constructs-in-natively-compiled-stored-procedures.md)  
  
-   [Using Try..Catch in Natively Compiled Stored Procedures](../../2014/database-engine/using-try-catch-in-natively-compiled-stored-procedures.md)  
  
-   [Supported Constructs on Natively Compiled Stored Procedures](../../2014/database-engine/supported-constructs-on-natively-compiled-stored-procedures.md)  
  
-   [Natively Compiled Stored Procedures and Execution Set Options](../../2014/database-engine/natively-compiled-stored-procedures-and-execution-set-options.md)  
  
-   [Best Practices for Calling Natively Compiled Stored Procedures](../../2014/database-engine/best-practices-for-calling-natively-compiled-stored-procedures.md)  
  
-   [Monitoring Performance of Natively Compiled Stored Procedures](../../2014/database-engine/monitoring-performance-of-natively-compiled-stored-procedures.md)  
  
-   [Calling Natively Compiled Stored Procedures from Data Access Applications](../../2014/database-engine/calling-natively-compiled-stored-procedures-from-data-access-applications.md)  
  
## See Also  
 [Memory-Optimized Tables](../../2014/database-engine/memory-optimized-tables.md)  
  
  