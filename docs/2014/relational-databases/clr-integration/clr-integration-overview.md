---
title: "Overview of CLR Integration | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "common language runtime [SQL Server], about CLR integration"
  - "extended stored procedures [SQL Server], vs. managed code"
  - "objects [CLR integration]"
  - "Transact-SQL vs. managed code"
  - "managed code [SQL Server], vs. Transact-SQL"
  - "managed code [SQL Server], vs. extended stored procedures"
  - "execution at client vs. execution at server [CLR integration]"
ms.assetid: 5aa176da-3652-4afa-a742-4c40c77ce5c3
author: rothja
ms.author: jroth
manager: craigg
---
# Overview of CLR Integration
  The common language runtime (CLR) is the heart of the Microsoft .NET Framework and provides the execution environment for all .NET Framework code. Code that runs within the CLR is referred to as managed code. The CLR provides various functions and services required for program execution, including just-in-time (JIT) compilation, allocating and managing memory, enforcing type safety, exception handling, thread management, and security.  See the .NET Framework SDK for more information.  
  
 With the CLR hosted in Microsoft SQL Server (called CLR integration), you can author stored procedures, triggers, user-defined functions, user-defined types, and user-defined aggregates in managed code. Because managed code compiles to native code prior to execution, you can achieve significant performance increases in some scenarios.  
  
 Managed code uses Code Access Security (CAS) to prevent assemblies from performing certain operations. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses CAS to help secure the managed code and prevent compromise of the operating system or database server.  
  
## Advantages of CLR Integration  
 [!INCLUDE[tsql](../../../includes/tsql-md.md)] is specifically designed for direct data access and manipulation in the database. While [!INCLUDE[tsql](../../../includes/tsql-md.md)] excels at data access and management, it is not a full-fledged programming language. For example, [!INCLUDE[tsql](../../../includes/tsql-md.md)] does not support arrays, collections, for-each loops, bit shifting, or classes. While some of these constructs can be simulated in [!INCLUDE[tsql](../../../includes/tsql-md.md)], managed code has integrated support for these constructs. Depending on the scenario, these features can provide a compelling reason to implement certain database functionality in managed code.  
  
 Microsoft Visual Basic .NET and Microsoft Visual C# offer object-oriented capabilities such as encapsulation, inheritance, and polymorphism. Related code can now be easily organized into classes and namespaces. When you are working with large amounts of server code, this allows you to more easily organize and maintain your code.  
  
 Managed code is better suited than [!INCLUDE[tsql](../../../includes/tsql-md.md)] for calculations and complicated execution logic, and features extensive support for many complex tasks, including string handling and regular expressions. With the functionality found in the .NET Framework Library, you have access to thousands of pre-built classes and routines. These can be easily accessed from any stored procedure, trigger or user defined function. The Base Class Library (BCL) includes classes that provide functionality for string manipulation, advanced math operations, file access, cryptography, and more.  
  
> [!NOTE]  
>  While many of these classes are available for use from within CLR code in SQL Server, those that are not appropriate for server-side use (for example, windowing classes), are not available. For more information, see [Supported .NET Framework Libraries](database-objects/supported-net-framework-libraries.md).  
  
 One of the benefits of managed code is type safety, or the assurance that code accesses types only in well-defined, permissible ways. Before managed code is executed, the CLR verifies that the code is safe. For example, the code is checked to ensure that no memory is read that has not previously been written. The CLR can also help ensure that code does not manipulate unmanaged memory.  
  
 CLR integration offers the potential for improved performance. For information, see [Performance of CLR Integration](clr-integration-architecture-performance.md).  
  
## Choosing Between Transact-SQL and Managed Code  
 When writing stored procedures, triggers, and user-defined functions, one decision you must make is whether to use traditional [!INCLUDE[tsql](../../../includes/tsql-md.md)], or a .NET Framework language such as Visual Basic .NET or Visual C#. Use [!INCLUDE[tsql](../../../includes/tsql-md.md)] when the code will mostly perform data access with little or no procedural logic. Use managed code for CPU-intensive functions and procedures that feature complex logic, or when you want to make use of the BCL of the .NET Framework.  
  
### Choosing Between Execution in the Server and Execution in the Client  
 Another factor in your decision about whether to use [!INCLUDE[tsql](../../../includes/tsql-md.md)] or managed code is where you would like your code to reside, the server computer or the client computer. Both [!INCLUDE[tsql](../../../includes/tsql-md.md)] and managed code can be run on the server. This places code and data close together, and allows you to take advantage of the processing power of the server. On the other hand, you may wish to avoid placing processor intensive tasks on your database server. Most client computers today are very powerful, and you may wish to take advantage of this processing power by placing as much code as possible on the client. Managed code can run on a client computer, while [!INCLUDE[tsql](../../../includes/tsql-md.md)] cannot.  
  
## Choosing Between Extended Stored Procedures and Managed Code  
 Extended stored procedures can be built to perform functionality not possible with [!INCLUDE[tsql](../../../includes/tsql-md.md)] stored procedures. Extended stored procedures can, however, compromise the integrity of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] process, while managed code that is verified to be type-safe cannot. Further, memory management, scheduling of threads and fibers, and synchronization services are more deeply integrated between the managed code of the CLR and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. With CLR integration, you have a more secure way than extended stored procedures to write the stored procedures you need to perform tasks not possible in [!INCLUDE[tsql](../../../includes/tsql-md.md)]. For more information about CLR integration and extended stored procedures, see [Performance of CLR Integration](clr-integration-architecture-performance.md).  
  
## See Also  
 [Installing the .NET Framework](https://technet.microsoft.com/library/ms166014\(v=SQL.105\).aspx)   
 [Architecture of CLR Integration](../../database-engine/dev-guide/architecture-of-clr-integration.md)   
 [Data Access from CLR Database Objects](data-access/data-access-from-clr-database-objects.md)   
 [Getting Started with CLR Integration](database-objects/getting-started-with-clr-integration.md)  
  
  
