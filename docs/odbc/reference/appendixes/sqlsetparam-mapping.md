---
title: "SQLSetParam Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "mapping deprecated functions [ODBC], SQLSetParam"
  - "SQLSetParam function [ODBC], mapping"
ms.assetid: 022dfbc0-8d18-4c35-8a28-d9eb16063188
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetParam Mapping
**SQLSetParam** continues to be mapped on top of **SQLBindParameter** as in ODBC 2.*x*. Even though it is conceptually similar to **SQLBindParam**, the Driver Manager does not map **SQLSetParam** to **SQLBindParam**. This is because certain existing ODBC 2.*x* drivers use the special value of *BufferLength* (SQL_SETPARAM_VALUE_MAX) that the Driver Manager generates when it maps **SQLSetParam** on top of **SQLBindParameter** to determine when it is called by a 1.*x* ODBC application.  
  
 A call to  
  
```  
SQLSetParam(hstmt, ipar, fCType, fSqlType, cbColDef, ibScale, rgbValue, pcbValue)  
```  
  
 will result in the following:  
  
```  
SQLBindParameter(StatementHandle, ParameterNumber, SQL_PARAM_INPUT_OUTPUT, ValueType, ParameterType, ColumnSize, DecimalDigits, ParameterValuePtr, SQL_SETPARAM_VALUE_MAX, StrLen_or_IndPtr)  
```
