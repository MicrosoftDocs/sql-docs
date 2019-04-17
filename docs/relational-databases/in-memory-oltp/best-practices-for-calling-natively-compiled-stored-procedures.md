---
title: "Best Practices for Calling Natively Compiled Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "03/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: f39fc1c7-cfec-4a95-97f6-6b95954694bb
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Best Practices for Calling Natively Compiled Stored Procedures
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Natively compiled stored procedures are:  
  
-   Used typically in performance-critical parts of an application.  
  
-   Frequently executed.  
  
-   Expected to be very fast.  
  
 The performance benefit of using a natively compiled stored procedure increases with the number of rows and the amount of logic that is processed by the procedure. For example, a natively compiled stored procedure will exhibit better performance if it uses one or more of the following:  
  
-   Aggregation.  
  
-   Nested-loops joins.  
  
-   Multi-statement select, insert, update, and delete operations.  
  
-   Complex expressions.  
  
-   Procedural logic, such as conditional statements and loops.  
  
 If you need to process only a single row, using a natively compiled stored procedure may not provide a performance benefit.  
  
 To avoid the server having to map parameter names and convert types:  
  
-   Match the types of the parameters passed to the procedure with the types in the procedure definition.  
  
-   Use ordinal (nameless) parameters when calling natively compiled stored procedures. For the most efficient execution, do not use named parameters.  
  
 Inefficiencies in parameters with natively compiled stored procedures can be detected through the XEvent **natively_compiled_proc_slow_parameter_passing**:
 - Mismatched types: **reason=parameter_conversion**
 - Named parameters: **reason=named_parameters**
 - DEFAULT values: **reason=default** 
  
## See Also  
 [Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md)  
