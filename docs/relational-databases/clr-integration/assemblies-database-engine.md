---
title: "Assemblies (Database Engine)"
description: A SQL Server instance can host assemblies that deploy functions, procedures, triggers, and user-defined aggregates and types written in a CLR language.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "assemblies [CLR integration]"
  - "assemblies [CLR integration], about assemblies"
  - "managed code [SQL Server], assemblies"
---
# Assemblies (Database Engine)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The articles in this section provide information to help you understand, design, and implement assemblies.

Assemblies are DLL files used in an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to deploy functions, stored procedures, triggers, user-defined aggregates, and user-defined types. Assemblies are written in one of the managed code languages hosted by the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR), instead of in [!INCLUDE [tsql](../../includes/tsql-md.md)].

An assembly in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is an object that references a managed application module (.dll file) that was created in the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime. An assembly contains class metadata and managed code. Uploading an assembly to an instance of SQL Server is the first step toward creating any of the following database objects:

- **CLR functions.** For more information, see [Create CLR functions](../user-defined-functions/create-clr-functions.md).

- **CLR stored procedures.** For more information, see [CLR Stored Procedures](/dotnet/framework/data/adonet/sql/clr-stored-procedures).

- **CLR triggers.** For more information, see [Create CLR Triggers](../triggers/create-clr-triggers.md).

- User-defined aggregate functions. For more information, see [Create user-defined aggregates](../user-defined-functions/create-user-defined-aggregates.md).

- User-defined types. For more information, see [Using User-Defined Types in SQL Server Native Client](../native-client/features/using-user-defined-types.md).

Assemblies perform the following functions in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

- Contain the managed code that runs the functionality of one or more of the CLR database objects previously listed.

- Contain metadata that includes the version number and culture of the assembly, an optional public key that uniquely identifies the list of classes of the assembly, the methods that are defined in the assembly, and the processor architecture of the assembly.

- Manage the degree to which managed code can access outside resources by regulating code access permissions.

- Contain metadata about dependencies on other assemblies referenced by the assembly.

## In this section

| Article | Description |
| --- | --- |
| [Designing assemblies](assemblies-designing.md) | Explains what you have to consider before creating an assembly. This includes packaging assemblies, code access permissions, and other restrictions. |
| [Implementing assemblies](assemblies-implementing.md) | Explains how to create and drop assemblies, how and when to modify assemblies, and how to retrieve metadata about assemblies. |
| [Get information about assemblies](assemblies-getting-information.md) | Lists the catalog views and functions that can be queried for metadata about assemblies. |

## Related content

- [Common language runtime (CLR) integration programming concepts](common-language-runtime-clr-integration-programming-concepts.md)
