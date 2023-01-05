---
description: "C to SQL: Year-Month Intervals"
title: "C to SQL: Year-Month Intervals | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "converting data from c to SQL types [ODBC], year-month intervals"
  - "intervals [ODBC], converting"
  - "year-month intervals [ODBC]"
  - "data conversions from C to SQL types [ODBC], year-month intervals"
ms.assetid: a0eb7b55-9db0-4375-9210-bddec4593880
author: David-Engel
ms.author: v-davidengel
---
# C to SQL: Year-Month Intervals
The identifiers for the year-month interval ODBC C data types are:  
  
 SQL_C_INTERVAL_MONTH SQL_C_INTERVAL_YEAR SQL_C_INTERVAL_YEAR_TO_MONTH  
  
 The following table shows the ODBC SQL data types to which year-month interval C data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md).  
  
|SQL type identifier|Test|SQLSTATE|  
|-------------------------|----------|--------------|  
|SQL_CHAR[a]<br /><br /> SQL_VARCHAR[a]<br /><br /> SQL_LONGVARCHAR[a]|Column byte length >= Character byte length<br /><br /> Column byte length < Character byte length[a]<br /><br /> Data value is not a valid interval literal|n/a<br /><br /> 22001<br /><br /> 22015|  
|SQL_WCHAR[a]<br /><br /> SQL_WVARCHAR[a]<br /><br /> SQL_WLONGVARCHAR[a]|Column character length >= Character length of data<br /><br /> Column character length < Character length of data[a]<br /><br /> Data value is not a valid interval literal|n/a<br /><br /> 22001<br /><br /> 22015|  
|SQL_TINYINT[b]<br /><br /> SQL_SMALLINT[b]<br /><br /> SQL_INTEGER[b]<br /><br /> SQL_BIGINT[b]<br /><br /> SQL_NUMERIC[b]<br /><br /> SQL_DECIMAL[b]|Conversion of a single-field interval did not result in truncation of whole digits<br /><br /> Conversion resulted in truncation of whole digits|n/a<br /><br /> 22003|  
|SQL_INTERVAL_MONTH<br /><br /> SQL_INTERVAL_YEAR<br /><br /> SQL_INTERVAL_YEAR_TO_MONTH|Data value was converted without truncation of any fields<br /><br /> One or more fields of data value were truncated during conversion|n/a<br /><br /> 22015|  
  
 [a]   All C interval data types can be converted to a character data type.  
  
 [b]   If the type field in the interval structure is such that the interval is a single field (SQL_YEAR or SQL_MONTH), the interval C type can be converted to any exact numeric (SQL_TINYINT, SQL_SMALLINT, SQL_INTEGER, SQL_BIGINT, SQL_DECIMAL, or SQL_NUMERIC).  
  
 The default conversion of an interval C type is to the corresponding year-month interval SQL type.  
  
 The driver ignores the length/indicator value when converting data from the interval C data type and assumes that the size of the data buffer is the size of the interval C data type. The length/indicator value is passed in the *StrLen_or_Ind* argument in **SQLPutData** and in the buffer specified with the *StrLen_or_IndPtr* argument in **SQLBindParameter**. The data buffer is specified with the *DataPtr* argument in **SQLPutData** and the *ParameterValuePtr* argument in **SQLBindParameter**.
