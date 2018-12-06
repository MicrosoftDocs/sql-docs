---
title: "Decimal Digits | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "size of data types [ODBC]"
  - "decimal digits of data types [ODBC]"
  - "data types [ODBC], decimal digits"
  - "SQL data types [ODBC], column characteristics"
ms.assetid: 07f3d1fc-b4ee-4693-b342-330b2231b6d0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Decimal Digits
The *decimal digits* of decimal and numeric data types is defined as the maximum number of digits to the right of the decimal point, or the scale of the data. For approximate floating-point number columns or parameters, the scale is undefined because the number of digits to the right of the decimal point is not fixed. For datetime or interval data that contains a seconds component, the decimal digits is defined as the number of digits to the right of the decimal point in the seconds component of the data.  
  
 For the SQL_DECIMAL and SQL_NUMERIC data types, the maximum scale is usually the same as the maximum precision. However, some data sources impose a separate limit on the maximum scale. To determine the minimum and maximum scales allowed for a data type, an application calls **SQLGetTypeInfo**.  
  
 The decimal digits defined for each concise SQL data type is shown in the following table.  
  
|SQL type|Decimal digits|  
|--------------|--------------------|  
|All character and binary types[a]|n/a|  
|SQL_DECIMAL<br />SQL_NUMERIC|The defined number of digits to the right of the decimal point. For example, the scale of a column defined as NUMERIC(10,3) is 3. This can be a negative number to support storage of very large numbers without using exponential notation; for example, "12000" could be stored as "12" with a scale of -3.|  
|All exact numeric types other than SQL_DECIMAL and SQL_NUMERIC[a]|0|  
|All approximate data types[a]|n/a|  
|SQL_TYPE_DATE, and all interval types with no seconds component[a]|n/a|  
|All datetime types except SQL_TYPE_DATE, and all interval types with a seconds component|The number of digits to the right of the decimal point in the seconds part of the value (fractional seconds). This number cannot be negative.|  
|SQL_GUID|n/a|  
  
 [a]   The *DecimalDigits* argument of **SQLBindParameter** is ignored for this data type.  
  
 The values returned for the decimal digits do not correspond to the values in any one descriptor field. The values can come from either the SQL_DESC_SCALE or the SQL_DESC_PRECISION field, depending on the data type, as shown in the following table.  
  
|SQL type|Descriptor field corresponding to<br /><br /> decimal digits|  
|--------------|----------------------------------------------------------|  
|All character and binary types|n/a|  
|All exact numeric types|SCALE|  
|SQL_BIT|n/a|  
|All approximate numeric types|n/a|  
|All datetime types|PRECISION|  
|All interval types with a seconds component|PRECISION|  
|All interval types with no seconds component|n/a|
