---
title: "SQLBindParam Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLBindparam function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLBindParam"
ms.assetid: 375f8f24-36de-4946-916e-c75abc6f070d
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLBindParam Mapping
**SQLBindParam** cannot truly be called deprecated because it was never there in ODBC; however, it still represents duplicated functionality - the Driver Manager needs to export it because ISO and Open Group-compliant applications will be using it. Because **SQLBindParameter** contains all the functionality of **SQLBindParam**, **SQLBindParam** will be mapped on top of **SQLBindParameter** (when the underlying driver is an ODBC 3*.x* driver). An ODBC 3*.x* driver does not need to implement **SQLBindParam**.  
  
## Remarks  
 When the following call to **SQLBindParam** is made:  
  
```  
SQLBindParam(   StatementHandle,    ParameterNumber,    ValueType,    ParameterType,    ColumnSize,    DecimalDigits,    ParameterValuePtr,    StrLen_or_IndPtr)  
```  
  
 the Driver Manager calls **SQLBindParameter** in the driver as follows:  
  
```  
SQLBindParameter(   StatementHandle,    ParameterNumber,    SQL_PARAM_INPUT,    ValueType,    ParameterType,    ColumnSize,    DecimalDigits,    ParameterValuePtr,    BufferLength,    StrLen_or_IndPtr)  
```  
  
 See [ODBC 64-Bit Information](../../../odbc/reference/odbc-64-bit-information.md), if your application will run on a 64-bit operating system.  
  
## See Also  
 [Mapping Deprecated Functions](../../../odbc/reference/appendixes/mapping-deprecated-functions.md)
