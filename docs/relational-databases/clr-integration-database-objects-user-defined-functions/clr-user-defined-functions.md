---
title: "CLR User-Defined Functions"
description: SQL Server CLR integration allows you to create user-defined scalar-valued, table-valued, and aggregate functions in any .NET Framework programming language.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "building database objects [CLR integration], user-defined functions"
  - "functions [CLR integration]"
  - "common language runtime [SQL Server], user-defined functions"
  - "database objects [CLR integration], user-defined functions"
  - "user-defined functions [CLR integration]"
---
# CLR user-defined functions

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

User-defined functions are routines that can take parameters, perform calculations or other actions, and return a result. You can write user-defined functions in any .NET Framework programming language, such as [!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)] or [!INCLUDE [visual-basic-md](../../includes/visual-basic-md.md)] .NET, to use on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

There are two types of functions: *scalar*, which returns a single value, and *table-valued*, which returns a set of rows.

## In this section

The following table lists the articles in this section.

| Article | Description |
| --- | --- |
| [CLR scalar-valued functions](clr-scalar-valued-functions.md) | Covers implementation requirements and examples of scalar-valued functions. |
| [CLR table-valued functions](clr-table-valued-functions.md) | Discusses how to implement and use table-valued functions (TVFs), as well as differences between [!INCLUDE [tsql](../../includes/tsql-md.md)] and common language runtime (CLR) TVFs. |
| [CLR user-defined aggregates](clr-user-defined-aggregates.md) | Describes how to implement and use user-defined aggregates. |

## Related content

- [User-defined functions](../user-defined-functions/user-defined-functions.md)
