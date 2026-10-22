---
title: "Common Language Runtime (CLR) Programming"
description: This article provides resources for using CLR integration with SQL Server, which allows you to write server-side modules using any .NET Framework language.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "CLR [SQL Server] See common language runtime [SQL Server]"
  - "Database Engine [SQL Server], .NET Framework"
  - ".NET Framework [SQL Server], Database Engine programming"
  - "common language runtime [SQL Server]"
  - ".NET Framework [SQL Server]"
---
# Common language runtime (CLR) integration programming concepts

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] features the integration of the common language runtime (CLR) component of the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] for Windows.

You can write stored procedures, triggers, user-defined types, user-defined functions, user-defined aggregates, and streaming table-valued functions, using any language, including [!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)] and [!INCLUDE [visual-basic-md](../../includes/visual-basic-md.md)] .NET.

## Remarks

- SQL Server CLR integration doesn't support .NET Core, or .NET 5 and later versions.

- You can load CLR database objects for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions on Linux, but they must be built with the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)]. Also, CLR assemblies with the `EXTERNAL_ACCESS` or `UNSAFE` permission set aren't supported on Linux.

- By default, the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] *runtime* is installed with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], but the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] SDK isn't. To install the latest version of the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] SDK, see [Download .NET Framework Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework).

- The `Microsoft.SqlServer.Server` namespace includes core functionality for CLR programming in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For documentation on the `Microsoft.SqlServer.Server` namespace, see [Microsoft.SqlServer.Server Namespace (.NET Framework 4.8)](/dotnet/api/microsoft.sqlserver.server?view=netframework-4.8&preserve-view=true).

- CLR functionality, such as CLR user functions, aren't supported for Azure SQL Database.

## In this section

The following table lists the articles in this section.

| Article | Description |
| --- | --- |
| [Common language runtime (CLR) integration](common-language-runtime-integration-overview.md) | Provides a brief overview of the CLR, and describes how and why this technology is used in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Describes the benefits of using the CLR to create database objects. |
| [Assemblies (Database Engine)](assemblies-database-engine.md) | Describes how assemblies are used in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to deploy functions, stored procedures, triggers, user-defined aggregates, and user-defined types. These objects are written in one of the managed code languages hosted by the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] common language runtime (CLR), and not in [!INCLUDE [tsql](../../includes/tsql-md.md)]. |
| [Build database objects with common language runtime (CLR) integration](database-objects/building-database-objects-with-common-language-runtime-clr-integration.md) | Describes the kinds of objects that can be built using the CLR, and reviews the requirements for building CLR database objects. |
| [Data access from CLR database objects](data-access/data-access-from-clr-database-objects.md) | Describes how a CLR routine can access data stored in an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [CLR integration security](security/clr-integration-security.md) | Describes the CLR integration security model. |
| [How to debug CLR database objects](debugging-clr-database-objects.md) | Describes limitations of and requirements for debugging CLR database objects. |
| [Deploy CLR database objects](deploying-clr-database-objects.md) | Describes deploying assemblies to production servers. |
| [Manage CLR integration assemblies](assemblies/managing-clr-integration-assemblies.md) | Describes how to create and drop CLR integration assemblies. |
| [Monitor and troubleshoot managed database objects](monitoring-and-troubleshooting-managed-database-objects.md) | Provides information about the tools that can be used to monitor and troubleshoot managed database objects and assemblies running in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Usage Scenarios and Examples for Common Language Runtime (CLR) Integration](/previous-versions/sql/sql-server-2016/ms131078(v=sql.130)) | Describes usage scenarios and code samples using CLR objects. |

## Related content

- [Assemblies (Database Engine)](assemblies-database-engine.md)
- [Install the .NET Framework SDK](https://dotnet.microsoft.com/download/dotnet-framework)
