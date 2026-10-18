---
title: "Use Common Language Runtime (CLR) Integration to Build Database Objects"
description: Build database objects using the SQL Server integration with the .NET Framework common language runtime (CLR).
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "routines [CLR integration]"
  - "database objects [CLR integration], building"
  - "common language runtime [SQL Server], building database objects"
  - "managed code [SQL Server], database objects"
  - "building database objects [CLR integration]"
  - ".NET Framework routines [SQL Server]"
---
# Build database objects with common language runtime (CLR) integration

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

You can build database objects using the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] integration with the [!INCLUDE [dnprdnshort-md](../../../includes/dnprdnshort-md.md)] common language runtime (CLR). Managed code that runs inside of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] is called a *CLR routine*. These routines include:

- Scalar-valued user-defined functions (scalar UDFs)
- Table-valued user-defined functions (TVFs)
- User-defined procedures (UDPs)
- User-defined triggers

CLR routines have the same structure in managed code. They're mapped to public, static (shared in Visual Basic .NET) methods of a class. In addition to routines, user-defined types (UDTs) and user-defined aggregate functions can also be defined using the .NET Framework. UDTs and user-defined aggregates are mapped to entire .NET Framework classes.

Each type of .NET Framework routine has a [!INCLUDE [tsql](../../../includes/tsql-md.md)] declaration and can be used anywhere in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that the [!INCLUDE [tsql](../../../includes/tsql-md.md)] equivalent can be used. For instance, scalar UDFs can be used in any scalar expression. A TVF can be used in any `FROM` clause. A procedure can be invoked in an `EXEC` statement or invoked from a client application.

Execution of a CLR object (user-defined function, user-defined type, or trigger) on the common language runtime can take place on multiple threads (parallel plan), if the query optimizer decides it's beneficial. However, if a user-defined function accesses data, execution is on a serial plan.

The following table lists the articles covered in this section.

| Article | Description |
| --- | --- |
| [Get started with CLR integration](getting-started-with-clr-integration.md) | Provides a brief overview of the libraries and namespaces required to compile object using CLR integration with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. Includes an example "Hello World" CLR stored procedure. |
| [Supported .NET Framework libraries](supported-net-framework-libraries.md) | Provides information on the .NET Framework libraries supported by CLR integration. |
| [CLR integration programming model restrictions](clr-integration-programming-model-restrictions.md) | Provides information about CLR integration programming model restrictions. |
| [SQL Server data types in the .NET Framework](../../clr-integration-database-objects-types-net-framework/sql-server-data-types-in-the-net-framework.md) | An overview of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] data types and their .NET Framework equivalents. |
| [CLR integration: custom attributes for CLR routines](clr-integration-custom-attributes-for-clr-routines.md) | Provides information about CLR integration custom attributes. |
| [CLR user-defined functions](../../clr-integration-database-objects-user-defined-functions/clr-user-defined-functions.md) | Describes how to implement and use the various types of CLR functions: table-valued, scalar, and user-defined aggregate functions. |
| [CLR user-defined types](../../clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) | Describes how to implement and use CLR user-defined types. |
| [CLR stored procedures](/dotnet/framework/data/adonet/sql/clr-stored-procedures) | Describes how to implement and use CLR stored procedures. |
| [CLR triggers](/dotnet/framework/data/adonet/sql/clr-triggers) | Describes how to implement and use CLR triggers. |

## Related content

- [Common language runtime (CLR) integration](../common-language-runtime-integration-overview.md)
