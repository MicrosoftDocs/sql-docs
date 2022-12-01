---
title: "Best practices - natively compiled stored procedures"
description: Learn about best practices for natively compiled stored procedures that are typically used in performance-critical parts of an application.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/24/2017"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.custom: seo-dt-2019
ms.assetid: f39fc1c7-cfec-4a95-97f6-6b95954694b
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Best Practices for Calling Natively Compiled Stored Procedures
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  Natively compiled stored procedures are:  
  
-   Used typically in performance-critical parts of an application.  
  
-   Frequently executed.  
  
-   Expected to be fast.  
  
 The performance benefit of using a natively compiled stored procedure increases with the number of rows and the amount of logic that is processed by the procedure. For example, a natively compiled stored procedure will exhibit better performance if it uses one or more of the following components:  
  
-   Aggregation.  
  
-   Nested-loops joins.  
  
-   Multi-statement select, insert, update, and delete operations.  
  
-   Complex expressions.  
  
-   Procedural logic, such as conditional statements and loops.  
  
 If you need to process only a single row, using a natively compiled stored procedure may not provide a performance benefit.  
  
 To avoid the server having to map parameter names and convert types, make sure that you:  
  
-   Match the types of the parameters passed to the procedure with the types in the procedure definition.  
  
-   Use ordinal (nameless) parameters when calling natively compiled stored procedures. For the most efficient execution, don't use named parameters.  
  
 Inefficiencies in parameters with natively compiled stored procedures can be detected through the XEvent **natively_compiled_proc_slow_parameter_passing**:
 - Mismatched types: **reason=parameter_conversion**
 - Named parameters: **reason=named_parameters**
 - DEFAULT values: **reason=default** 
  
## See Also  
 [Natively Compiled Stored Procedures](./a-guide-to-query-processing-for-memory-optimized-tables.md)
