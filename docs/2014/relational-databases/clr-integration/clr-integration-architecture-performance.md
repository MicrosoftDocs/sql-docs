---
title: "Performance of CLR Integration | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "common language runtime [SQL Server], performance"
  - "common language runtime [SQL Server], compilation process"
  - "performance [CLR integration]"
ms.assetid: 7ce2dfc0-4b1f-4dcb-a979-2c4f95b4cb15
author: rothja
ms.author: jroth
manager: craigg
---
# Performance of CLR Integration
  This topic discusses some of the design choices that enhance the performance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] integration with the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework common language runtime (CLR).  
  
## The Compilation Process  
 During compilation of SQL expressions, when a reference to a managed routine is encountered, a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] intermediate language (MSIL) stub is generated. This stub includes code to marshal the routine parameters from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to the CLR, invoke the function, and return the result. This "glue" code is based on the type of parameter and on parameter direction (in, out, or reference).  
  
 The glue code enables type-specific optimizations and ensures efficient enforcement of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] semantics, such as nullability, constraining facets, by-value, and standard exception handling. By generating code for the exact types of the arguments, you avoid type coercion or wrapper object creation costs (called "boxing") across the invocation boundary.  
  
 The generated stub is then compiled to native code and optimized for the particular hardware architecture on which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] executes, using the JIT (just-in-time) compilation services of the CLR. The JIT services are invoked at the method level and allow the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] hosting environment to create a single compilation unit that spans both [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and CLR execution. Once the stub is compiled, the resulting function pointer becomes the run-time implementation of the function. This code generation approach ensures that there are no additional invocation costs related to reflection or metadata access at run time.  
  
### Fast Transitions Between SQL Server and CLR  
 The compilation process yields a function pointer that can be called at run time from native code. In the case of scalar-valued user-defined functions, this function invocation happens on a per-row basis. To minimize the cost of transitioning between [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and the CLR, statements that contain any managed invocation have a startup step to identify the target application domain. This identification step reduces the cost of transitioning for each row.  
  
## Performance Considerations  
 The following summarizes performance considerations specific to CLR integration in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. More detailed information can be found in "[Using CLR Integration in SQL Server 2005](https://go.microsoft.com/fwlink/?LinkId=50332)" on the MSDN Web site. General information regarding managed code performance can be found in "[Improving .NET Application Performance and Scalability](https://go.microsoft.com/fwlink/?LinkId=50333)" on the MSDN Web site.  
  
### User-Defined Functions  
 CLR functions benefit from a quicker invocation path than that of [!INCLUDE[tsql](../../../includes/tsql-md.md)] user-defined functions. Additionally, managed code has a decisive performance advantage over [!INCLUDE[tsql](../../../includes/tsql-md.md)] in terms of procedural code, computation, and string manipulation. CLR functions that are computing-intensive and that do not perform data access are better written in managed code. [!INCLUDE[tsql](../../../includes/tsql-md.md)] functions do, however, perform data access more efficiently than CLR integration.  
  
### User-Defined Aggregates  
 Managed code can significantly outperform cursor-based aggregation. Managed code generally performs slightly slower than built-in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] aggregate functions. We recommend that if a native built-in aggregate function exists, you should use it. In cases in which the needed aggregation is not natively supported, consider a CLR user-defined aggregate over a cursor-based implementation for performance reasons.  
  
### Streaming Table-Valued Functions  
 Applications often need to return a table as a result of invoking a function. Examples include reading tabular data from a file as part of an import operation, and converting comma-separated-values to a relational representation. Typically, you can accomplish this by materializing and populating the result table before it can be consumed by the caller. The integration of the CLR into [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] introduces a new extensibility mechanism called a streaming table-valued function (STVF). Managed STVFs perform better than comparable extended stored procedure implementations.  
  
 STVFs are managed functions that return an `IEnumerable` interface. `IEnumerable` has methods to navigate the result set returned by the STVF. When the STVF is invoked, the returned `IEnumerable` is directly connected to the query plan. The query plan calls `IEnumerable` methods when it needs to fetch rows. This iteration model allows results to be consumed immediately after the first row is produced, instead of waiting until the entire table is populated. It also significantly reduces the memory consumed by invoking the function.  
  
### Arrays vs. Cursors  
 When [!INCLUDE[tsql](../../../includes/tsql-md.md)] cursors must traverse data that is more easily expressed as an array, managed code can be used with significant performance gains.  
  
### String Data  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] character data, such as `varchar`, can be of the type SqlString or SqlChars in managed functions. SqlString variables create an instance of the entire value into memory. SqlChars variables provide a streaming interface that can be used to achieve better performance and scalability by not creating an instance of the entire value into memory. This becomes particularly important for large object (LOB) data. Additionally, server XML data can be accessed through a streaming interface returned by `SqlXml.CreateReader()`.  
  
### CLR vs. Extended Stored Procedures  
 The Microsoft.SqlServer.Server application programming interfaces (APIs) that allow managed procedures to send result sets back to the client perform better than the Open Data Services (ODS) APIs used by extended stored procedures. Furthermore, the System.Data.SqlServer APIs support data types such as `xml`, `varchar(max)`, `nvarchar(max)`, and `varbinary(max)`, introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], while the ODS APIs have not been extended to support the new data types.  
  
 With managed code, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] manages use of resources such as memory, threads, and synchronization. This is because the managed APIs that expose these resources are implemented on top of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource manager. Conversely, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has no view or control over the resource usage of the extended stored procedure. For example, if an extended stored procedure consumes too much CPU or memory resources, there is no way to detect or control this with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. With managed code, however, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can detect that a given thread has not yielded for a long period of time, and then force the task to yield so that other work can be scheduled. Consequently, using managed code provides for better scalability and system resource usage.  
  
 Managed code may incur additional overhead necessary to maintain the execution environment and perform security checks. This is the case, for example, when running inside [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and numerous transitions from managed to native code are required (because [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] needs to do additional maintenance on thread-specific settings when moving out to native code and back). Consequently, extended stored procedures can significantly outperform managed code running inside [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] for cases in which there are frequent transitions between managed and native code.  
  
> [!NOTE]  
>  It is recommended that you do not develop new extended stored procedures, because this feature has been deprecated.  
  
### Native Serialization for User-Defined Types  
 User-defined types (UDTs) are designed as an extensibility mechanism for the scalar type system. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] implements a serialization format for UDTs called `Format.Native`. During compilation, the structure of the type is examined to generate MSIL that is customized for that particular type definition.  
  
 Native serialization is the default implementation for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. User-defined serialization invokes a method defined by the type author to do the serialization. `Format.Native` serialization should be used when possible for best performance.  
  
### Normalization of Comparable UDTs  
 Relational operations, such as sorting and comparing UDTs, operate directly on the binary representation of the value. This is accomplished by storing a normalized (binary ordered) representation of the state of the UDT on disk.  
  
 Normalization has two benefits: it makes the comparison operation considerably less expensive by avoiding the construction of the type instance and the method invocation overhead; and it creates a binary domain for the UDT, enabling the construction of histograms, indexes, and histograms for values of the type. Consequently, normalized UDTs have a very similar performance profile to the native built-in types for operations that do not involve method invocation.  
  
### Scalable Memory Usage  
 In order for managed garbage collection to perform and scale well in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], avoid large, single allocation. Allocations greater than 88 kilobytes (KB) in size will be placed on the Large Object Heap, which will cause garbage collection to perform and scale much worse than many smaller allocations. For example, if you need to allocate a large multi-dimensional array, it is better to allocate a jagged (scattered) array.  
  
## See Also  
 [CLR User-Defined Types](../clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)  
  
  
