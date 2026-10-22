---
title: "SQL Server Data Types in the .NET Framework"
description: The SqlTypes library is part of the Microsoft .NET Framework. It provides data types with the same semantics and precision as data types in the SQL Server database.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "System.Data library"
  - "System.Data.SqlTypes namespace"
  - "data types [CLR integration]"
  - "SqlTypes library"
  - "database objects [CLR integration], data types"
  - "common language runtime [SQL Server], data types"
  - "building database objects [CLR integration], data types"
  - "mapping data types [CLR integration]"
---
# SQL Server data types in the .NET Framework

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `SqlTypes` library is part of the base class library of the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)]. It provides data types with the same semantics and precision as data types found in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. This article describes the new semantics to [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] programmers, and introduces the types implemented in the `System.Data.SqlTypes` namespace that is included in the `System.Data` library.

## In this section

This following table lists the articles in this section.

| Article | Description |
| --- | --- |
| [Nullability and three-value logic comparisons](nullability-and-three-value-logic-comparisons.md) | Discusses how `NULL` values are handled with common language runtime (CLR) integration data types. |
| [Collation and CLR integration data types](collation-and-clr-integration-data-types.md) | Describes how collations are handled with CLR integration. |
| [Handle large object (LOB) parameters in the CLR](handling-large-object-lob-parameters-in-the-clr.md) | Describes how to pass LOB types between [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and the CLR. |
| [Map CLR parameter data](mapping-clr-parameter-data.md) | Shows data type mappings between [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], CLR integration, and the .NET Framework. |
