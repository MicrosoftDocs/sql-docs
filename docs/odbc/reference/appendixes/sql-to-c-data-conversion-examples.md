---
title: "SQL to C Data Conversion Examples | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data conversions from SQL to C types [ODBC], examples"
  - "converting data from SQL to C types [ODBC], examples"
ms.assetid: 0190c76c-7f9b-42f4-be9d-cef7284840fd
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL to C Data Conversion Examples

The examples shown in the following table illustrate how the driver converts SQL data to C data :  
  
|SQL type<br /><br /> identifier|SQL data<br /><br /> value|C type<br /><br /> identifier|Buffer<br /><br /> length|**TargetValuePtr*|SQLSTATE|  
|-----------------------------|------------------------|---------------------------|-----------------------|------------------------|--------------|  
|SQL_CHAR|abcdef|SQL_C_CHAR|7|abcdef\0[a]|n/a|  
|SQL_CHAR|abcdef|SQL_C_CHAR|6|abcde\0[a]|01004|  
|SQL_DECIMAL|1234.56|SQL_C_CHAR|8|1234.56\0[a]|n/a|  
|SQL_DECIMAL|1234.56|SQL_C_CHAR|5|1234\0[a]|01004|  
|SQL_DECIMAL|1234.56|SQL_C_CHAR|4|----|22003|  
|SQL_DECIMAL|1234.56|SQL_C_FLOAT|ignored|1234.56|n/a|  
|SQL_DECIMAL|1234.56|SQL_C_SSHORT|ignored|1234|01S07|  
|SQL_DECIMAL|1234.56|SQL_C_STINYINT|ignored|----|22003|  
|SQL_DOUBLE|1.2345678|SQL_C_DOUBLE|ignored|1.2345678|n/a|  
|SQL_DOUBLE|1.2345678|SQL_C_FLOAT|ignored|1.234567|n/a|  
|SQL_DOUBLE|1.2345678|SQL_C_STINYINT|ignored|1|n/a|  
|SQL_TYPE_DATE|1992-12-31|SQL_C_CHAR|11|1992-12-31\0[a]|n/a|  
|SQL_TYPE_DATE|1992-12-31|SQL_C_CHAR|10|-----|22003|  
|SQL_TYPE_DATE|1992-12-31|SQL_C_TIMESTAMP|ignored|1992,12,31, 0,0,0,0[b]|n/a|  
|SQL_TYPE_TIMESTAMP|1992-12-31 23:45:55.12|SQL_C_CHAR|23|1992-12-31 23:45:55.12\0[a]|n/a|  
|SQL_TYPE_TIMESTAMP|1992-12-31 23:45:55.12|SQL_C_CHAR|22|1992-12-31 23:45:55.1\0[a]|01004|  
|SQL_TYPE_TIMESTAMP|1992-12-31 23:45:55.12|SQL_C_CHAR|18|----|22003|  
  
 [a]   "\0" represents a null-termination byte. The driver always null-terminates SQL_C_CHAR data.  
  
 [b]   The numbers in this list are the numbers stored in the fields of the TIMESTAMP_STRUCT structure.
