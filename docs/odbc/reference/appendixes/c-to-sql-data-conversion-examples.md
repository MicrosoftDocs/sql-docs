---
description: "C to SQL Data Conversion Examples"
title: "C to SQL Data Conversion Examples | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "converting data from c to SQL types [ODBC], examples"
  - "data conversions from C to SQL types [ODBC], examples"
ms.assetid: 9f390afc-d8b8-4286-b559-98b3b8781f3d
author: David-Engel
ms.author: v-davidengel
---
# C to SQL Data Conversion Examples
The following examples illustrate how the driver converts C data to SQL data :  
  
|C type identifier|C data value|SQL type<br /><br /> identifier|Column<br /><br /> length|SQL data<br /><br /> value|SQLSTATE|  
|-----------------------|------------------|-----------------------------|-----------------------|------------------------|--------------|  
|SQL_C_CHAR|abcdef\0[a]|SQL_CHAR|6|abcdef|n/a|  
|SQL_C_CHAR|abcdef\0[a]|SQL_CHAR|5|abcde|22001|  
|SQL_C_CHAR|1234.56\0[a]|SQL_DECIMAL|8[b]|1234.56|n/a|  
|SQL_C_CHAR|1234.56\0[a]|SQL_DECIMAL|7[b]|1234.5|22001|  
|SQL_C_CHAR|1234.56\0[a]|SQL_DECIMAL|4|----|22003|  
|SQL_C_FLOAT|1234.56|SQL_FLOAT|n/a|1234.56|n/a|  
|SQL_C_FLOAT|1234.56|SQL_INTEGER|n/a|1234|22001|  
|SQL_C_FLOAT|1234.56|SQL_TINYINT|n/a|----|22003|  
|SQL_C_TYPE_DATE|1992,12,31[c]|SQL_CHAR|10|1992-12-31|n/a|  
|SQL_C_TYPE_DATE|1992,12,31[c]|SQL_CHAR|9|----|22003|  
|SQL_C_TYPE_DATE|1992,12,31[c]|SQL_TIMESTAMP|n/a|1992-12-31 00:00:00.0|n/a|  
|SQL_C_TYPE_TIMESTAMP|1992,12,31, 23,45,55, 120000000[d]|SQL_CHAR|22|1992-12-31 23:45:55.12|n/a|  
|SQL_C_TYPE_TIMESTAMP|1992,12,31, 23,45,55, 120000000[d]|SQL_CHAR|21|1992-12-31 23:45:55.1|22001|  
|SQL_C_TYPE_TIMESTAMP|1992,12,31, 23,45,55, 120000000[d]|SQL_CHAR|18|----|22003|  
  
 [a]   "\0" represents a null-termination byte. The null-termination byte is required only if the length of the data is SQL_NTS.  
  
 [b]   In addition to bytes for numbers, one byte is required for a sign and another byte is required for the decimal point.  
  
 [c]   The numbers in this list are the numbers stored in the fields of the SQL_DATE_STRUCT structure.  
  
 [d]   The numbers in this list are the numbers stored in the fields of the SQL_TIMESTAMP_STRUCT structure.
