---
title: "Supported .NET Framework Libraries"
description: With the CLR hosted in SQL Server, you can author using supported .NET Framework class libraries and unsupported libraries that you register with a database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "common language runtime [SQL Server], .NET Framework libraries"
  - ".NET Framework [CLR Integration]"
---
# Supported .NET Framework libraries

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

With the common language runtime (CLR) hosted in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], you can author stored procedures, triggers, user-defined functions, user-defined types, and user-defined aggregates in managed code. With the functionality found in the .NET Framework class libraries, you have access to prebuilt classes that provide functionality for string manipulation, advanced math operations, file access, cryptography, and more. These classes can be accessed from any managed stored procedure, user-defined type, trigger, user-defined function, or user-defined aggregate.

If you service or upgrade unsupported assemblies in the global assembly cache (GAC), your [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] application can stop working. This is because servicing or upgrading libraries in the GAC doesn't update those assemblies inside [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. If an assembly exists both in a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] database and in the GAC, the two copies of the assembly must exactly match. If they don't match, an error occurs when the assembly is used by [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] CLR integration. If you service or upgrade any assemblies in the GAC that are also registered in the database, including unsupported .NET Framework assemblies, make sure to also service or upgrade the copy of the assembly inside your [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] databases with the `ALTER ASSEMBLY` statement. For more information, see [MSSQLSERVER_6522](../../errors-events/mssqlserver-6522-database-engine-error.md).

## Supported libraries

[!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] has a list of supported .NET Framework libraries that are tested to ensure that they meet reliability and security standards for interaction with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. Supported libraries don't need to be explicitly registered on the server before they can be used in your code; [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] loads them directly from the Global Assembly Cache (GAC).

The libraries/namespaces supported by CLR integration in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] are:

[!INCLUDE [clr-integration-namespaces](../../includes/clr-integration-namespaces.md)]

## Unsupported libraries

Unsupported libraries can still be called from your managed stored procedures, triggers, user-defined functions, user-defined types, and user-defined aggregates. The unsupported library must first be registered in the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] database, using the `CREATE ASSEMBLY` statement, before it can be used in your code. Any unsupported library that is registered and run on the server should be reviewed and tested for security and reliability.

For example, the `System.DirectoryServices` namespace isn't supported. You must register the System.DirectoryServices.dll assembly with `UNSAFE` permissions before you can call it from your code. The `UNSAFE` permission is necessary because classes in the `System.DirectoryServices` namespace don't meet the requirements for `SAFE` or `EXTERNAL_ACCESS`. For more information, see [CLR integration programming model restrictions](clr-integration-programming-model-restrictions.md) and [CLR integration Code Access Security](../security/clr-integration-code-access-security.md).

## Related content

- [Create an assembly](../assemblies/creating-an-assembly.md)
- [CLR integration code access security](../security/clr-integration-code-access-security.md)
- [CLR integration programming model restrictions](clr-integration-programming-model-restrictions.md)
