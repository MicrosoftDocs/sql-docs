---
title: "Accessing User-Defined Types in ADO.NET"
description: UDTs, written in .NET Framework CLR languages, allow a SQL Server database to store objects and custom data structures. In ADO.NET, a provider exposes UDTs.
author: uc-msft
ms.author: umajay
ms.date: 03/19/2026
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "ADO.NET [CLR integration]"
  - "UDTs [CLR integration], ADO.NET"
  - "user-defined types [CLR integration], ADO.NET"
---
# Access user-defined types in ADO.NET

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

User-defined types (UDTs) are written using any of the languages supported by the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] common language runtime (CLR) that produce verifiable code. This includes [!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)] and [!INCLUDE [visual-basic-md](../../includes/visual-basic-md.md)] .NET. UDTs allow objects and custom data structures to be stored in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database.

The data is exposed as public members of a .NET Framework class or structure, and behaviors are defined by methods of the class or structure. A UDT can be used as the column definition of a table, as a variable in a [!INCLUDE [tsql](../../includes/tsql-md.md)] batch, or as an argument of a [!INCLUDE [tsql](../../includes/tsql-md.md)] function or stored procedure.

In ADO.NET, the `Microsoft.Data.SqlClient` provider exposes UDTs in the following ways:

- Through the `Microsoft.Data.SqlClient.SqlDataReader` as an object.
- Through the `SqlDataReader` as raw bytes.
- As a parameter of a `Microsoft.Data.SqlClient.SqlParameter` object.

## In this section

| Article | Description |
| --- | --- |
| [Retrieve user-defined type (UDT) data in ADO.NET](accessing-user-defined-types-retrieving-udt-data.md) | Describes how to retrieve UDT data and how to specify parameters. |
| [Update user-defined type (UDT) columns with DataAdapters](accessing-user-defined-types-updating-udt-columns-with-dataadapters.md) | Describes how to work with UDTs in a `DataSet` and how to update UDT data using a `DataAdapter`. |

## Related content

- [CLR user-defined types](clr-user-defined-types.md)
