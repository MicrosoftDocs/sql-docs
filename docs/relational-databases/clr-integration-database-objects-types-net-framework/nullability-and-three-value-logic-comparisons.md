---
title: "Nullability and Three-Value Logic Comparisons"
description: This article covers how SQL Server data types differ from types in System.Data.SqlTypes in the .NET Framework, which have similar semantics and precision.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "precision [CLR integration]"
  - "overflow detections [CLR integration]"
  - "null values [CLR integration]"
  - "three-value logic comparisons [CLR integration]"
  - "data types [CLR integration]"
  - "SqlBoolean data type"
---
# Nullability and three-value logic comparisons

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

If you're familiar with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data types, you find similar semantics and precision in the `System.Data.SqlTypes` namespace in the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)]. There are some differences, however, and this article covers the most important of these differences.

## Null values

A primary difference between native common language runtime (CLR) data types and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data types is that the former don't allow for `NULL` values, while the latter provide full `NULL` semantics.

Comparisons are affected by `NULL` values. When you compare two values `x` and `y`, if either `x` or `y` is `NULL`, then some logical comparisons evaluate to an `UNKNOWN` value rather than true or false.

## SqlBoolean data type

The `System.Data.SqlTypes` namespace introduces a `SqlBoolean` type to represent this three-value logic. Comparisons between any `SqlTypes` return a `SqlBoolean` value type. The `UNKNOWN` value is represented by the null value of the `SqlBoolean` type. The properties `IsTrue`, `IsFalse`, and `IsNull` are provided to check the value of a `SqlBoolean` type.

## Operations, functions, and null values

All arithmetic operators (`+`, `-`, `*`, `/`, `%`), bitwise operators (`~`, `&`, and `|`), and most functions return `NULL` if any of the operands or arguments of `SqlTypes` are null. The `IsNull` property always returns a `true` or `false` value.

## Precision

Decimal data types in the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] CLR have different maximum values than numeric and decimal data types in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. In addition, in the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] CLR decimal data types assume the maximum precision. In the CLR for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], however, `SqlDecimal` provides the same maximum precision and scale, and the same semantics as the decimal data type in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Overflow detection

In the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] CLR, the addition of two very large numbers might not throw an exception. Instead, if no check operator is used, the returned result might *wrap around* as a negative integer. In `System.Data.SqlTypes`, exceptions are thrown for all overflow and underflow errors, and divide-by-zero errors.

## Related content

- [SQL Server data types in the .NET Framework](sql-server-data-types-in-the-net-framework.md)
