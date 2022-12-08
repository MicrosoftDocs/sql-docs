---
title: "Common Language Runtime (CLR) Overview"
description: CLR integration with SQL Server allows you to implement some functionalities using any .NET Framework language as SQL Server server-side modules.
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/21/2021"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "managed code [SQL Server]"
  - "common language runtime [SQL Server], about CLR integration"
  - "cross-language integration"
  - "integrating CLR [SQL Server]"
  - ".NET Framework [SQL Server], common language runtime"
  - "code access security [CLR integration]"
  - "managed code [SQL Server], CLR integration"
ms.assetid: 7be9e644-36a2-48fc-9206-faf59fdff4d7
---
# Common Language Runtime Integration
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-index) enable you to implement some of the functionalities with .NET languages using the native common language runtime (CLR) integration as SQL Server server-side modules (procedures, functions, and triggers). The CLR supplies managed code with services such as cross-language integration, code access security, object lifetime management, and debugging and profiling support. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] users and application developers, CLR integration means that you can now write stored procedures, triggers, user-defined types, user-defined functions (scalar and table valued), and user-defined aggregate functions using any .NET Framework language, including [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic .NET and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C#. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes the .NET Framework version 4 pre-installed.  

> [!WARNING]
>  CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` may be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. Beginning with [!INCLUDE[sssql17](../../includes/sssql17-md.md)], an `sp_configure` option called `clr strict security` is introduced to enhance the security of CLR assemblies. `clr strict security` is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but this is not recommended. Microsoft recommends that all assemblies be signed by a certificate or asymmetric key with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the master database. For more information, see [CLR strict security](../../database-engine/configure-windows/clr-strict-security.md). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrators can also add assemblies to a list of assemblies, which the Database Engine should trust. For more information, see [sys.sp_add_trusted_assembly](../../relational-databases/system-stored-procedures/sys-sp-add-trusted-assembly-transact-sql.md).

This 6-minute video shows you how to use CLR in Azure SQL Managed Instance:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Its-just-SQL-CLR-in-Azure-SQL-Database-Managed-Instance/player?WT.mc_id=dataexposed-c9-niner]



## When to use CLR modules

CLR Integration enables you to implement complex features that are available in .NET Framework such as regular expressions, code for accessing external resources (servers, web services, databases), custom encryption, etc. Some of the benefits of the server-side CLR integration are:
  
-   **A better programming model.** The .NET Framework languages are in many respects richer than Transact-SQL, offering constructs and capabilities previously not available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] developers. Developers may also leverage the power of the .NET Framework Library, which provides an extensive set of classes that can be used to quickly and efficiently solve programming problems.  
  
-   **Improved safety and security.** Managed code runs in a common language run-time environment, hosted by the Database Engine. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] leverages this to provide a safer and more secure alternative to the extended stored procedures available in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **Ability to define data types and aggregate functions.** User-defined types and user-defined aggregates are two new managed database objects that expand the storage and querying capabilities of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **Streamlined development through a standardized environment.** Database development is integrated into future releases of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio .NET development environment. Developers use the same tools for developing and debugging database objects and scripts as they use to write middle-tier or client-tier .NET Framework components and services.  
  
-   **Potential for improved performance and scalability.** In many situations, the .NET Framework language compilation and execution models deliver improved performance over Transact-SQL.  

[SQL Server language extensions](../../language-extensions/language-extensions-overview.md) provide an alternative execution environment for runtimes close to the database engine. For a discussion of the differences between SQL CLR and SQL language extensions, see [Compare SQL Server Language Extensions to SQL CLR](../../language-extensions/concepts/compare-extensibility-to-clr.md).
  
 The following table lists the topics in this section.  
  
 [Overview of CLR Integration](../../relational-databases/clr-integration/clr-integration-overview.md)  
 Describes the kinds of objects that can be built using CLR integration. Also reviews the requirements for building database objects using CLR integration.  
  
 [What's New in CLR Integration](../../relational-databases/clr-integration/clr-integration-what-s-new.md)  
 Describes the new features in this release.  
  
 [Architecture of CLR Integration](./clr-integration-architecture-clr-hosted-environment.md)  
 Describes the design goals of CLR integration.  
  
 [Enabling CLR Integration](../../relational-databases/clr-integration/clr-integration-enabling.md)  
 Describes how to enable CLR integration.  
  
## See Also  
 [Installing the .NET Framework](https://technet.microsoft.com/library/ms166014\(v=SQL.105\).aspx)  ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only)   
 [Performance of CLR Integration](../../relational-databases/clr-integration/clr-integration-architecture-performance.md)  
  
