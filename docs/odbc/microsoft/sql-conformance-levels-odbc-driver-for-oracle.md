---
description: "SQL Conformance Levels (ODBC Driver for Oracle)"
title: "SQL Conformance Levels (ODBC Driver for Oracle) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "conformance levels [ODBC], SQL"
  - "SQL conformance levels [ODBC]"
  - "ODBC driver for Oracle [ODBC], conformance levels"
ms.assetid: 077a6c6a-2c57-42c9-a4fd-4cf0e65cf7e2
author: David-Engel
ms.author: v-davidengel
---
# SQL Conformance Levels (ODBC Driver for Oracle)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 The ODBC Driver for Oracle supports the Minimum SQL grammar and Core SQL grammar and also supports the following ODBC extensions to SQL:  
  
-   Date, time, and timestamp data  
  
-   Left and right outer joins  
  
-   Numeric functions:  

    :::row:::
        :::column:::
            Abs  
            Ceiling  
            Cos  
            Exp  
            Floor  
        :::column-end:::
        :::column:::
            Log  
            Log10  
            Mod  
            Pi  
            Power  
        :::column-end:::
        :::column:::
            round  
            second  
            sign  
            sin  
            sqrt  
        :::column-end:::
        :::column:::
            tan  
            truncate  
        :::column-end:::
    :::row-end:::
    
-   Date functions:  

    :::row:::
        :::column:::
            Curdate  
            Curtime  
            Dayname  
            Dayofmonth  
        :::column-end:::
        :::column:::
            Dayofweek  
            Dayofyear  
            Hour  
            Month  
        :::column-end:::
        :::column:::
            monthname  
            minute  
            now  
            quarter  
        :::column-end:::
        :::column:::
            second  
            week  
            year  
        :::column-end:::
    :::row-end:::

-   String functions:  

    :::row:::
        :::column:::
            Ascii  
            Char  
            Concat  
            Lcase  
        :::column-end:::
        :::column:::
            Left  
            Length  
            Ltrim  
            Replace  
        :::column-end:::
        :::column:::
            right  
            rtrim  
            soundex  
            substring  
        :::column-end:::
        :::column:::
            ucase  
        :::column-end:::
    :::row-end:::

-   Type-conversion function:  

    Convert  

-   System functions:  
  
    Ifnull  
    User
