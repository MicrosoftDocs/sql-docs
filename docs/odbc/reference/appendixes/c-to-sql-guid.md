---
title: "C to SQL: GUID"
description: "C to SQL: GUID"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "converting data from c to SQL types [ODBC], guid"
  - "data conversions from C to SQL types [ODBC], guid"
  - "GUID data type [ODBC]"
---
# C to SQL: GUID
The identifier for the GUID ODBC C data type is:  
  
 SQL_C_GUID  
  
 The following table shows the ODBC SQL data types to which GUID C data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md).  
  
|SQL type identifier|Test|SQLSTATE|  
|-------------------------|----------|--------------|  
|SQL_CHAR|Column byte length >= 36|n/a|  
|SQL_VARCHAR|Column byte length < 36|22001|  
|SQL_LONGVARCHAR|Data value is not a valid GUID|22018|  
|SQL_WCHAR|Column character length >= 36|n/a|  
|SQL_WVARCHAR|Column character length < 36|22001|  
|SQL_WLONGVARCHAR|Data value is not a valid GUID|22018|  
|SQL_GUID|None[a]|n/a|  
  
 [a]   All hexadecimal values are valid as a GUID.  
  
 The driver ignores the length/indicator value when converting data from the GUID C data type and assumes that the size of the data buffer is the size of the GUID C data type. The length/indicator value is passed in the *StrLen_or_Ind* argument in **SQLPutData** and in the buffer specified with the *StrLen_or_IndPtr* argument in **SQLBindParameter**. The data buffer is specified with the *DataPtr* argument in **SQLPutData** and the *ParameterValuePtr* argument in **SQLBindParameter**.
