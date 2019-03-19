---
title: "Common Language Runtime (CLR) Integration Overview | Microsoft Docs"
ms.custom: ""
ms.date: "06/20/2017"
ms.prod: sql
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
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Common Language Runtime Integration Overview
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] now features the integration of the common language runtime (CLR) component of the .NET Framework for [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows. The CLR supplies managed code with services such as cross-language integration, code access security, object lifetime management, and debugging and profiling support. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] users and application developers, CLR integration means that you can now write stored procedures, triggers, user-defined types, user-defined functions (scalar and table-valued), and user-defined aggregate functions using any .NET Framework language, including [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic .NET and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C#. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes the .NET Framework version 4 pre-installed.  

> [!WARNING]
>  CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` may be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. Beginning with [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)], an `sp_configure` option called `clr strict security` is introduced to enhance the security of CLR assemblies. `clr strict security` is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but this is not recommended. Microsoft recommends that all assemblies be signed by a certificate or asymmetric key with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the master database. For more information, see [CLR strict security](../../database-engine/configure-windows/clr-strict-security.md). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators can also add assemblies to a list of assemblies, which the Database Engine should trust. For more information, see [sys.sp_add_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md).

 Among the major benefits of this integration are:  
  
-   **A better programming model.** The .NET Framework languages are in many respects richer than Transact-SQL, offering constructs and capabilities previously not available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] developers. Developers may also leverage the power of the .NET Framework Library, which provides an extensive set of classes that can be used to quickly and efficiently solve programming problems.  
  
-   **Improved safety and security.** Managed code runs in a common language run-time environment, hosted by the Database Engine. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] leverages this to provide a safer and more secure alternative to the extended stored procedures available in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **Ability to define data types and aggregate functions.** User defined types and user defined aggregates are two new managed database objects which expand the storage and querying capabilities of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **Streamlined development through a standardized environment.** Database development is integrated into future releases of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio .NET development environment. Developers use the same tools for developing and debugging database objects and scripts as they use to write middle-tier or client-tier .NET Framework components and services.  
  
-   **Potential for improved performance and scalability.** In many situations, the .NET Framework language compilation and execution models deliver improved performance over Transact-SQL.  
  
 This following table lists the topics in this section.  
  
 [Overview of CLR Integration](../../relational-databases/clr-integration/clr-integration-overview.md)  
 Describes the kinds of objects that can be built using CLR integration, and reviews the requirements for building database objects using CLR integration.  
  
 [What's New in CLR Integration](../../relational-databases/clr-integration/clr-integration-what-s-new.md)  
 Describes the new features in this release.  
  
 [Architecture of CLR Integration](https://msdn.microsoft.com/library/05e4b872-3d21-46de-b4d5-739b5f2a0cf9)  
 Describes the design goals of CLR integration.  
  
 [Enabling CLR Integration](../../relational-databases/clr-integration/clr-integration-enabling.md)  
 Describes how to enable CLR integration.  
  
## See Also  
 [Installing the .NET Framework](https://technet.microsoft.com/library/ms166014\(v=SQL.105\).aspx)   
 [Performance of CLR Integration](../../relational-databases/clr-integration/clr-integration-architecture-performance.md)  
  
  
