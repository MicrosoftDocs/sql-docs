---
description: "C to SQL: Timestamp"
title: "C to SQL: Timestamp | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "data conversions from C to SQL types [ODBC], timestamp"
  - "timestamp data type [ODBC]"
  - "converting data from c to SQL types [ODBC], timestamp"
ms.assetid: 0e08bfff-68f9-4648-9558-09b57fea08ad
author: David-Engel
ms.author: v-davidengel
---
# C to SQL: Timestamp
The identifier for the timestamp ODBC C data type is:  
  
 SQL_C_TYPE_TIMESTAMP  
  
 The following table shows the ODBC SQL data types to which timestamp C data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md).  
  
|SQL type identifier|Test|SQLSTATE|  
|-------------------------|----------|--------------|  
|SQL_CHAR<br /><br /> SQL_VARCHAR<br /><br /> SQL_LONGVARCHAR|Column byte length >= Character byte length<br /><br /> 19 <= Column byte length < Character byte length<br /><br /> Column byte length < 19<br /><br /> Data value is not a valid timestamp|n/a<br /><br /> 22001<br /><br /> 22001<br /><br /> 22008|  
|SQL_WCHAR<br /><br /> SQL_WVARCHAR<br /><br /> SQL_WLONGVARCHAR|Column character length >= Character length of data<br /><br /> 19 <= Column character length < Character length of data<br /><br /> Column character length < 19<br /><br /> Data value is not a valid timestamp|n/a<br /><br /> 22001<br /><br /> 22001<br /><br /> 22008|  
|SQL_TYPE_DATE|Time fields are zero<br /><br /> Time fields are nonzero<br /><br /> Data value does not contain a valid date|n/a<br /><br /> 22008<br /><br /> 22007|  
|SQL_TYPE_TIME|Fractional seconds fields are zero[a]<br /><br /> Fractional seconds fields are nonzero[a]<br /><br /> Data value does not contain a valid time|n/a<br /><br /> 22008<br /><br /> 22007|  
|SQL_TYPE_TIMESTAMP|Fractional seconds fields are not  truncated<br /><br /> Fractional seconds fields are truncated<br /><br /> Data value is not a valid timestamp|n/a<br /><br /> 22008<br /><br /> 22007|  
  
 [a]   The date fields of the timestamp structure are ignored.  
  
 For information about what values are valid in a SQL_C_TIMESTAMP structure, see [C Data Types](../../../odbc/reference/appendixes/c-data-types.md), earlier in this appendix.  
  
 When timestamp C data is converted to character SQL data, the resulting character data is in the "*yyyy*-*mm*-*dd* *hh*:*mm*:*ss*[.*f...*]" format.  
  
 The driver ignores the length/indicator value when converting data from the timestamp C data type and assumes that the size of the data buffer is the size of the timestamp C data type. The length/indicator value is passed in the *StrLen_or_Ind* argument in **SQLPutData** and in the buffer specified with the *StrLen_or_IndPtr* argument in **SQLBindParameter**. The data buffer is specified with the *DataPtr* argument in **SQLPutData** and the *ParameterValuePtr* argument in **SQLBindParameter**.
