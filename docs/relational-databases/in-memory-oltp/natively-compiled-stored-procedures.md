---
title: "Natively Compiled Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
helpviewer_keywords: 
  - "natively compiled stored procedures"
ms.assetid: d5ed432c-10c5-4e4f-883c-ef4d1fa32366
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Natively Compiled Stored Procedures
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  Natively compiled stored procedures are [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures compiled to native code that access memory-optimized tables. Natively compiled stored procedures allow for efficient execution of the queries and business logic in the stored procedure. For more details about the native compilation process, see [Native Compilation of Tables and Stored Procedures](../../relational-databases/in-memory-oltp/native-compilation-of-tables-and-stored-procedures.md). For more information about migrating disk-based stored procedures to natively compiled stored procedures, see [Migration Issues for Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/migration-issues-for-natively-compiled-stored-procedures.md).  
  
> [!NOTE]  
>  One difference between interpreted (disk-based) stored procedures and natively compiled stored procedures is that an interpreted stored procedure is compiled at first execution, whereas a natively compiled stored procedure is compiled when it is created. With natively compiled stored procedures, many error conditions can be detected at create time and will cause creation of the natively compiled stored procedure to fail (such as arithmetic overflow, type conversion, and some divide-by-zero conditions). With interpreted stored procedures, these error conditions typically do not cause a failure when the stored procedure is created, but all executions will fail.  
  
 Topics in this section:  
  
-   [Creating Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/creating-natively-compiled-stored-procedures.md)  
  
-   [Atomic Blocks](../../relational-databases/in-memory-oltp/atomic-blocks-in-native-procedures.md)  
  
-   [Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md)  
  
-   [Supported DDL for Natively Compiled T-SQL modules](../../relational-databases/in-memory-oltp/supported-ddl-for-natively-compiled-t-sql-modules.md)  
  
-   [Natively Compiled Stored Procedures and Execution Set Options](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures-and-execution-set-options.md)  
  
-   [Best Practices for Calling Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/best-practices-for-calling-natively-compiled-stored-procedures.md)  
  
-   [Monitoring Performance of Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/monitoring-performance-of-natively-compiled-stored-procedures.md)  
  
-   [Calling Natively Compiled Stored Procedures from Data Access Applications](../../relational-databases/in-memory-oltp/calling-natively-compiled-stored-procedures-from-data-access-applications.md)  
  
## See Also  
 [Memory-Optimized Tables](../../relational-databases/in-memory-oltp/memory-optimized-tables.md)  
  
  
