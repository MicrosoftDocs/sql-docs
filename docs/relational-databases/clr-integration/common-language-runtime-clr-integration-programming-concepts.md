---
title: "Common language runtime (CLR) programming"
description: This article provides resources for using CLR integration with SQL Server, which allows you to write server-side modules using any .NET Framework language.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 11/25/2022
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "CLR [SQL Server] See common language runtime [SQL Server]"
  - "Database Engine [SQL Server], .NET Framework"
  - ".NET Framework [SQL Server], Database Engine programming"
  - "common language runtime [SQL Server]"
  - ".NET Framework [SQL Server]"
---
# Common language runtime (CLR) integration programming concepts

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features the integration of the common language runtime (CLR) component of the .NET Framework for [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows.

You can write stored procedures, triggers, user-defined types, user-defined functions, user-defined aggregates, and streaming table-valued functions, using any .NET Framework language, including Visual Basic and C#.

## Remarks

- SQL Server CLR integration doesn't support .NET Core, or .NET 5 and later versions.

- You can load CLR database objects for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions on Linux, but they must be built with the .NET Framework. Also, CLR assemblies with the `EXTERNAL_ACCESS` or `UNSAFE` permission set aren't supported on Linux.

- By default, the .NET Framework *runtime* is installed with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but the .NET Framework SDK is not. To install the latest version of the .NET Framework SDK, see [Download .NET Framework Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework).

- The `Microsoft.SqlServer.Server` namespace includes core functionality for CLR programming in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For documentation on the `Microsoft.SqlServer.Server` namespace, see [Microsoft.SqlServer.Server Namespace (.NET Framework 4.8)](/dotnet/api/microsoft.sqlserver.server?view=netframework-4.8&preserve-view=true).

- CLR functionality, such as CLR user functions, aren't supported for Azure SQL Database.

## In this section

The following table lists the articles in this section.

| Article | Description |
| --- | --- |
| [Common Language Runtime (CLR) Integration Overview](../../relational-databases/clr-integration/common-language-runtime-integration-overview.md) | Provides a brief overview of the CLR, and describes how and why this technology has been used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Describes the benefits of using the CLR to create database objects. |
| [Assemblies (Database Engine)](../../relational-databases/clr-integration/assemblies-database-engine.md) | Describes how assemblies are used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to deploy functions, stored procedures, triggers, user-defined aggregates, and user-defined types that are written in one of the managed code languages hosted by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework common language runtime (CLR), and not written in [!INCLUDE[tsql](../../includes/tsql-md.md)]. |
| [Building Database Objects with Common Language Runtime (CLR) Integration](../../relational-databases/clr-integration/database-objects/building-database-objects-with-common-language-runtime-clr-integration.md) | Describes the kinds of objects that can be built using the CLR, and reviews the requirements for building CLR database objects. |
| [Data Access from CLR Database Objects](../../relational-databases/clr-integration/data-access/data-access-from-clr-database-objects.md) | Describes how a CLR routine can access data stored in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [CLR Integration Security](../../relational-databases/clr-integration/security/clr-integration-security.md) | Describes the CLR integration security model. |
| [Debugging CLR Database Objects](../../relational-databases/clr-integration/debugging-clr-database-objects.md) | Describes limitations of and requirements for debugging CLR database objects. |
| [Deploying CLR Database Objects](../../relational-databases/clr-integration/deploying-clr-database-objects.md) | Describes deploying assemblies to production servers. |
| [Managing CLR Integration Assemblies](../../relational-databases/clr-integration/assemblies/managing-clr-integration-assemblies.md) | Describes how to create and drop CLR integration assemblies. |
| [Monitoring and Troubleshooting Managed Database Objects](../../relational-databases/clr-integration/monitoring-and-troubleshooting-managed-database-objects.md) | Provides information about the tools that can be used to monitor and troubleshoot managed database objects and assemblies running in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Usage Scenarios and Examples for Common Language Runtime (CLR) Integration](/previous-versions/sql/sql-server-2016/ms131078(v=sql.130)) | Describes usage scenarios and code samples using CLR objects. |

## See also

- [Assemblies (Database Engine)](../../relational-databases/clr-integration/assemblies-database-engine.md)
- [Install the .NET Framework SDK](https://dotnet.microsoft.com/download/dotnet-framework)
