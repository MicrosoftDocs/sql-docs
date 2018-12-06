---
title: "Nullability and Three-Value Logic Comparisons | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "precision [CLR integration]"
  - "overflow detections [CLR integration]"
  - "null values [CLR integration]"
  - "three-value logic comparisons [CLR integration]"
  - "data types [CLR integration]"
  - "SqlBoolean data type"
ms.assetid: 13da4c7f-1010-4b2d-a63c-c69b6bfd96f1
author: rothja
ms.author: jroth
manager: craigg
---
# Nullability and Three-Value Logic Comparisons
  If you are familiar with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, you will find similar semantics and precision in the `System.Data.SqlTypes` namespace in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. There are some differences, however, and this topic covers the most important of these differences.  
  
## NULL Values  
 A primary difference between native common language runtime (CLR) data types and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types is that the former do not allow for NULL values, while the latter provide full NULL semantics.  
  
 Comparisons are affected by NULL values. When comparing two values x and y, if either x or y is NULL, then some logical comparisons evaluate to an UNKNOWN value rather than true or false.  
  
## SqlBoolean Data Type  
 The `System.Data.SqlTypes` namespace introduces a `SqlBoolean` type to represent this 3-value logic. Comparisons between any `SqlTypes` return a `SqlBoolean` value type. The UNKNOWN value is represented by the null value of the `SqlBoolean` type. The properties `IsTrue`, `IsFalse`, and `IsNull` are provided to check the value of a `SqlBoolean` type.  
  
## Operations, Functions, and NULL Values  
 All arithmetic operators (+, -, \*, /, %), bitwise operators (~, &, and |), and most functions return NULL if any of the operands or arguments of `SqlTypes` are NULL. The `IsNull` property always returns a true or false value.  
  
## Precision  
 Decimal data types in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] CLR have different maximum values than those of the numeric and decimal data types in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. In addition, in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] CLR decimal data types assume the maximum precision. In the CLR for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], however, `SqlDecimal` provides the same maximum precision and scale, and the same semantics as the decimal data type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Overflow Detection  
 In the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] CLR, the addition of two very large numbers may not throw an exception. Instead, if no check operator has been used, the returned result may "wrap around" as a negative integer. In `System.Data.SqlTypes`, exceptions are thrown for all overflow and underflow errors, and divide-by-zero errors.  
  
## See Also  
 [SQL Server Data Types in the .NET Framework](sql-server-data-types-in-the-net-framework.md)  
  
  
