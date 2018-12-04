---
title: "SQL Conformance Levels (ODBC Driver for Oracle) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "conformance levels [ODBC], SQL"
  - "SQL conformance levels [ODBC]"
  - "ODBC driver for Oracle [ODBC], conformance levels"
ms.assetid: 077a6c6a-2c57-42c9-a4fd-4cf0e65cf7e2
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL Conformance Levels (ODBC Driver for Oracle)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 The ODBC Driver for Oracle supports the Minimum SQL grammar and Core SQL grammar and also supports the following ODBC extensions to SQL:  
  
-   Date, time, and timestamp data  
  
-   Left and right outer joins  
  
-   Numeric functions:  
  
    |||||  
    |-|-|-|-|  
    |Abs|Log|round|tan|  
    |Ceiling|Log10|second|truncate|  
    |Cos|Mod|sign||  
    |Exp|Pi|sin||  
    |Floor|Power|sqrt||  
  
-   Date functions:  
  
    |||||  
    |-|-|-|-|  
    |Curdate|Dayofweek|monthname|second|  
    |Curtime|Dayofyear|minute|week|  
    |Dayname|Hour|now|year|  
    |Dayofmonth|Month|quarter||  
  
-   String functions:  
  
    |||||  
    |-|-|-|-|  
    |Ascii|Left|right|ucase|  
    |Char|Length|rtrim||  
    |Concat|Ltrim|soundex||  
    |Lcase|Replace|substring||  
  
-   Type-conversion function:  
  
    ||  
    |-|  
    |Convert|  
  
-   System functions:  
  
    ||  
    |-|  
    |Ifnull|  
    |User|
