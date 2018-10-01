---
title: "Display Size | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "display size of data types [ODBC]"
  - "size of data types [ODBC]"
  - "data types [ODBC], display size"
  - "SQL data types [ODBC], column characteristics"
ms.assetid: 9f7f766f-2492-463c-aab7-f2476e222042
author: MightyPen
ms.author: genemi
manager: craigg
---
# Display Size
The display size of a column is the maximum number of characters needed to display data in character form. The following table defines the display size for each ODBC SQL data type.  
  
|SQL type identifier|Display size|  
|-------------------------|------------------|  
|All character types[a]|The defined (for fixed types) or maximum (for variable types) number of characters needed to display the data in character form.|  
|SQL_DECIMAL SQL_NUMERIC|The precision of the column plus 2 (a sign, *precision* digits, and a decimal point). For example, the display size of a column defined as NUMERIC(10,3) is 12.|  
|SQL_BIT|1 (1 digit).|  
|SQL_TINYINT|4 if signed (a sign and 3 digits) or 3 if unsigned (3 digits).|  
|SQL_SMALLINT|6 if signed (a sign and 5 digits) or 5 if unsigned (5 digits).|  
|SQL_INTEGER|11 if signed (a sign and 10 digits) or 10 if unsigned (10 digits).|  
|SQL_BIGINT|20 (a sign and 19 digits if signed or 20 digits if unsigned).|  
|SQL_REAL|14 (a sign, 7 digits, a decimal point, the letter *E*, a sign, and 2 digits).|  
|SQL_FLOAT SQL_DOUBLE|24 (a sign, 15 digits, a decimal point, the letter *E*, a sign, and 3 digits).|  
|All binary types[a]|The defined or maximum (for variable types) length of the column times 2. (Each binary byte is represented by a 2-digit hexadecimal number.)|  
|SQL_TYPE_DATE|10 (a date in the format *yyyy-mm-dd*).|  
|SQL_TYPE_TIME|8 (a time in the format *hh:mm:ss*)<br /><br /> - or -<br /><br /> 9 + *s* (a time in the format *hh:mm:ss*[.fff...], where *s* is the fractional seconds precision).|  
|SQL_TYPE_TIMESTAMP|19 (for a timestamp in the *yyyy-mm-dd hh:mm:ss* format)<br /><br /> - or -<br /><br /> 20 + *s* (for a timestamp in the *yyyy-mm-dd hh:mm:ss*[.fff...] format, where *s* is the fractional seconds precision).|  
|All interval data types|See [Interval Data Type Length](../../../odbc/reference/appendixes/interval-data-type-length.md).|  
|SQL_GUID|36 (the number of characters in the *aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee* format|  
  
 [a]   If the driver cannot determine the column or parameter length of variable types, it returns SQL_NO_TOTAL.
