---
title: "Fast Parse | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "fast parse [Integration Services]"
ms.assetid: 6688707d-3c5b-404e-aa2f-e13092ac8d95
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Fast Parse
  Fast parse provides a fast, simple set of routines for parsing data. These routines are not locale-sensitive and they support only a subset of date, time, and integer formats.  
  
## Requirements and Limitations  
 By implementing fast parse, a package forfeits its ability to interpret date, time, and numeric data in locale-specific formats and many frequently used ISO 8601 basic and extended formats, but the package enhances its performance. For example, fast parse supports only the most commonly used date format representations such as YYYYMMDD and YYYY-MM-DD, does not perform locale-specific parsing, does not recognize special characters in currency data, and cannot convert hexadecimal or scientific representation of integers.  
  
 Fast parse is available only when you use the Flat File source or the Data Conversion transformation. The increase in performance can be significant, and you should consider using fast parse in these data flow components if you can.  
  
 If the data flow in the package requires locale-sensitive parsing, standard parse is recommended instead of fast parse. For example, fast parse does not recognize locale-sensitive data that includes decimal symbols such as the comma, date formats other than year-month-date formats, and currency symbols.  
  
 Truncated representations that imply one or more date parts, such as a century, a year, or a month, are not recognized by fast parse. For example, fast parse recognizes neither the '**-YYMM**' format, which specifies a year and month in an implied century, nor '**--MM**', which specifies a month in an implied year. However, some representations with reduced precision are recognized. For example, fast parse recognizes the 'hhmm;' format, which indicates hour and minute only, and '**YYYY**', which indicates year only.  
  
 Fast parse is specified at the column level. In the Flat File source and the Data Conversion transformation, you can specify Fast parse on output columns. Inputs and outputs can include both locale-sensitive and locale-insensitive columns.  
  
 For more information about the data formats that Fast parse supports, see [Numeric Data Formats](../../2014/integration-services/numeric-data-formats.md) and [Date and Time Formats](../../2014/integration-services/date-and-time-formats.md).  
  
## Related Tasks  
 [Set Fast Parse](../../2014/integration-services/set-fast-parse.md)  
  
  
