---
title: "Building Database Objects with Common Language Runtime (CLR) Integration | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "routines [CLR integration]"
  - "database objects [CLR integration], building"
  - "common language runtime [SQL Server], building database objects"
  - "managed code [SQL Server], database objects"
  - "building database objects [CLR integration]"
  - ".NET Framework routines [SQL Server]"
ms.assetid: ce34132c-bfa3-447b-9131-b6e17c672efe
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Building Database Objects with Common Language Runtime (CLR) Integration
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You can build database objects using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] integration with the .NET Framework common language runtime (CLR). Managed code that runs inside of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is referred to as a "CLR routine." These routines include:  
  
-   Scalar-valued user-defined functions (scalar UDFs)  
  
-   Table-valued user-defined functions (TVFs)  
  
-   User-defined procedures (UDPs)  
  
-   User-defined triggers  
  
 CLR routines have the same structure in managed code. They are mapped to public, static (shared in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Basic .NET) methods of a class. In addition to routines, user-defined types (UDTs) and user-defined aggregate functions can also be defined using the .NET Framework. UDTs and user-defined aggregates are mapped to entire .NET Framework classes.  
  
 Each type of .NET Framework routine has a [!INCLUDE[tsql](../../../includes/tsql-md.md)] declaration and can be used anywhere in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that the [!INCLUDE[tsql](../../../includes/tsql-md.md)] equivalent can be used. For instance, scalar UDFs can be used in any scalar expression. A TVF can be used in any FROM clause. A procedure can be invoked in an EXEC statement or invoked from a client application.  
  
> [!NOTE]  
>  Execution of a CLR object (user-defined function, user-defined type, or trigger) on the common language runtime can take place on multiple threads (parallel plan), if the query optimizer decides it is beneficial. However, if a user-defined function accesses data, execution will be  on a serial plan. When executed on a server version prior to [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], if a user-defined function contains LOB parameters or return values, execution also must be on a serial plan.  
  
 The following table lists the topics covered in this section.  
  
 [Getting Started with CLR Integration](../../../relational-databases/clr-integration/database-objects/getting-started-with-clr-integration.md)  
 Provides a brief overview of the libraries and namespaces required to compile object using CLR integration with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Includes an example "Hello World" CLR stored procedure.  
  
 [Supported .NET Framework Libraries](../../../relational-databases/clr-integration/database-objects/supported-net-framework-libraries.md)  
 Provides information on the .NET Framework libraries supported by CLR integration.  
  
 [CLR Integration Programming Model Restrictions](../../../relational-databases/clr-integration/database-objects/clr-integration-programming-model-restrictions.md)  
 Provides information about CLR integration programming model restrictions.  
  
 [SQL Server Data Types in the .NET Framework](../../../relational-databases/clr-integration-database-objects-types-net-framework/sql-server-data-types-in-the-net-framework.md)  
 An overview of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types and their .NET Framework equivalents.  
  
 [Overview of CLR Integration Custom Attributes](https://msdn.microsoft.com/library/ecf5c097-0972-48e2-a9c0-b695b7dd2820)  
 Provides information about CLR integration custom attributes.  
  
 [CLR User-Defined Functions](../../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-functions.md)  
 Describes how to implement and use the various types of CLR functions: table-valued, scalar, and user-defined aggregate functions.  
  
 [CLR User-Defined Types](../../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)  
 Describes how to implement and use CLR user-defined types.  
  
 [CLR Stored Procedures](https://msdn.microsoft.com/library/bbdd51b2-a9b4-4916-ba6f-7957ac6c3f33)  
 Describes how to implement and use CLR stored procedures.  
  
 [CLR Triggers](https://msdn.microsoft.com/library/302a4e4a-3172-42b6-9cc0-4a971ab49c1c)  
 Describes how to implement and use CLR triggers.  
  
## See Also  
 [Common Language Runtime &#40;CLR&#41; Integration Overview](../../../relational-databases/clr-integration/common-language-runtime-integration-overview.md)  
  
  
