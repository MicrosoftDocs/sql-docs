---
title: "Common language runtime (CLR) overview"
description: CLR integration with SQL Server allows you to implement some functionalities using any .NET Framework language as SQL Server server-side modules.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "managed code [SQL Server]"
  - "common language runtime [SQL Server], about CLR integration"
  - "cross-language integration"
  - "integrating CLR [SQL Server]"
  - ".NET Framework [SQL Server], common language runtime"
  - "code access security [CLR integration]"
  - "managed code [SQL Server], CLR integration"
---
# Common language runtime (CLR) integration

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-index) enable you to implement some of the functionalities with .NET languages using the native common language runtime (CLR) integration as SQL Server server-side modules (procedures, functions, and triggers). The CLR supplies managed code with services such as cross-language integration, code access security, object lifetime management, and debugging and profiling support.

For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] users and application developers, CLR integration means that you can write stored procedures, triggers, user-defined types, user-defined functions (scalar and table valued), and user-defined aggregate functions using any .NET Framework language, including Visual Basic .NET and Visual C#. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] includes the .NET Framework version 4 preinstalled.

This 6-minute video shows you how to use CLR in Azure SQL Managed Instance:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Its-just-SQL-CLR-in-Azure-SQL-Database-Managed-Instance/player?WT.mc_id=dataexposed-c9-niner]

## Code access security no longer supported

[!INCLUDE [code-access-security](../../database-engine/includes/code-access-security.md)]

## When to use CLR modules

CLR Integration enables you to implement complex features that are available in .NET Framework such as regular expressions, code for accessing external resources (servers, web services, databases), custom encryption, etc. Some of the benefits of the server-side CLR integration are:

- **A better programming model.** The .NET Framework languages are in many respects richer than Transact-SQL, offering constructs and capabilities previously not available to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] developers. Developers can also use the power of the .NET Framework Library, which provides an extensive set of classes that can be used to quickly and efficiently solve programming problems.

- **Improved safety and security.** Managed code runs in a common language run-time environment, hosted by the Database Engine. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses this to provide a safer and more secure alternative to the extended stored procedures available in earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- **Ability to define data types and aggregate functions.** User-defined types and user-defined aggregates are two new managed database objects that expand the storage and querying capabilities of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- **Streamlined development through a standardized environment.** Database development is integrated into future releases of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Visual Studio .NET development environment. Developers use the same tools for developing and debugging database objects and scripts as they use to write middle-tier or client-tier .NET Framework components and services.

- **Potential for improved performance and scalability.** In many situations, the .NET Framework language compilation and execution models deliver improved performance over Transact-SQL.

[SQL Server Language Extensions](../../language-extensions/language-extensions-overview.md) provide an alternative execution environment for runtimes close to the database engine. For a discussion of the differences between SQL CLR and SQL language extensions, see [Compare SQL Server Language Extensions to SQL CLR](../../language-extensions/concepts/compare-extensibility-to-clr.md).

The following table lists the articles in this section.

| Article | Description |
| --- | --- |
| [CLR integration overview](clr-integration-overview.md) | Describes the kinds of objects that can be built using CLR integration. Also reviews the requirements for building database objects using CLR integration. |
| [CLR integration - What's new](clr-integration-what-s-new.md) | Describes the new features in this release. |
| [CLR integration Architecture - CLR hosted environment](clr-integration-architecture-clr-hosted-environment.md) | Describes the design goals of CLR integration. |
| [Enabling CLR integration](clr-integration-enabling.md) | Describes how to enable CLR integration. |

## Related content

- [.NET Framework installation guide](/dotnet/framework/install/)
- [Performance of CLR integration architecture](clr-integration-architecture-performance.md)
