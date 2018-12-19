---
title: "Common Language Runtime (CLR) Integration Overview | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "managed code [SQL Server]"
  - "common language runtime [SQL Server], about CLR integration"
  - "cross-language integration"
  - "integrating CLR [SQL Server]"
  - ".NET Framework [SQL Server], common language runtime"
  - "code access security [CLR integration]"
  - "managed code [SQL Server], CLR integration"
ms.assetid: 7be9e644-36a2-48fc-9206-faf59fdff4d7
author: rothja
ms.author: jroth
manager: craigg
---
# Common Language Runtime (CLR) Integration Overview
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] now features the integration of the common language runtime (CLR) component of the .NET Framework for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows. The CLR supplies managed code with services such as cross-language integration, code access security, object lifetime management, and debugging and profiling support. For [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] users and application developers, CLR integration means that you can now write stored procedures, triggers, user-defined types, user-defined functions (scalar and table-valued), and user-defined aggregate functions using any .NET Framework language, including [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Basic .NET and [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual C#. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] includes the .NET Framework version 4 pre-installed.  
  
 Among the major benefits of this integration are:  
  
-   **A better programming model.** The .NET Framework languages are in many respects richer than Transact-SQL, offering constructs and capabilities previously not available to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] developers. Developers may also leverage the power of the .NET Framework Library, which provides an extensive set of classes that can be used to quickly and efficiently solve programming problems.  
  
-   **Improved safety and security.** Managed code runs in a common language run-time environment, hosted by the Database Engine. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] leverages this to provide a safer and more secure alternative to the extended stored procedures available in earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
-   **Ability to define data types and aggregate functions.** User defined types and user defined aggregates are two new managed database objects which expand the storage and querying capabilities of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
-   **Streamlined development through a standardized environment.** Database development is integrated into future releases of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Studio .NET development environment. Developers use the same tools for developing and debugging database objects and scripts as they use to write middle-tier or client-tier .NET Framework components and services.  
  
-   **Potential for improved performance and scalability.** In many situations, the .NET Framework language compilation and execution models deliver improved performance over Transact-SQL.  
  
 This following table lists the topics in this section.  
  
 [Overview of CLR Integration](clr-integration-overview.md)  
 Describes the kinds of objects that can be built using CLR integration, and reviews the requirements for building database objects using CLR integration.  
  
 [What's New in CLR Integration](clr-integration-what-s-new.md)  
 Describes the new features in this release.  
  
 [Architecture of CLR Integration](../../database-engine/dev-guide/architecture-of-clr-integration.md)  
 Describes the design goals of CLR integration.  
  
 [Enabling CLR Integration](clr-integration-enabling.md)  
 Describes how to enable CLR integration.  
  
## See Also  
 [Installing the .NET Framework](https://technet.microsoft.com/library/ms166014\(v=SQL.105\).aspx)   
 [Performance of CLR Integration](clr-integration-architecture-performance.md)  
  
  
