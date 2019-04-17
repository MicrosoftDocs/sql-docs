---
title: "C to SQL: Time | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data conversions from C to SQL types [ODBC], time"
  - "time data type [ODBC]"
  - "converting data from c to SQL types [ODBC], time"
ms.assetid: a8da43c9-d9a5-45e5-bd9a-1dd633db2ee0
author: MightyPen
ms.author: genemi
manager: craigg
---
# C to SQL: Time
The identifier for the time ODBC C data type is:  
  
 SQL_C_TYPE_TIME  
  
 The following table shows the ODBC SQL data types to which time C data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md).  
  
|SQL type identifier|Test|SQLSTATE|  
|-------------------------|----------|--------------|  
|SQL_CHAR<br /><br /> SQL_VARCHAR<br /><br /> SQL_LONGVARCHAR|Column byte length >= 8<br /><br /> Column byte length < 8<br /><br /> Data value is not a valid time|n/a<br /><br /> 22001<br /><br /> 22008|  
|SQL_WCHAR<br /><br /> SQL_WVARCHAR<br /><br /> SQL_WLONGVARCHAR|Column character length >= 8<br /><br /> Column character length < 8<br /><br /> Data value is not a valid time|n/a<br /><br /> 22001<br /><br /> 22008|  
|SQL_TYPE_TIME|Data value is a valid time<br /><br /> Data value is not a valid time|n/a<br /><br /> 22007|  
|SQL_TYPE_TIMESTAMP|Data value is a valid time[a]<br /><br /> Data value is not a valid time|n/a<br /><br /> 22007|  
  
 [a]   The date portion of the timestamp is set to the current date, and the fractional seconds portion of the timestamp is set to zero.  
  
 For information about what values are valid in a SQL_C_TYPE_TIME structure, see [C Data Types](../../../odbc/reference/appendixes/c-data-types.md), earlier in this appendix.  
  
 When time C data is converted to character SQL data, the resulting character data is in the "*hh*:*mm*:*ss*" format.  
  
 The driver ignores the length/indicator value when converting data from the time C data type and assumes that the size of the data buffer is the size of the time C data type. The length/indicator value is passed in the *StrLen_or_Ind* argument in **SQLPutData** and in the buffer specified with the *StrLen_or_IndPtr* argument in **SQLBindParameter**. The data buffer is specified with the *DataPtr* argument in **SQLPutData** and the *ParameterValuePtr* argument in **SQLBindParameter**.
