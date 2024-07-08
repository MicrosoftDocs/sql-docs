---
title: "SQLParamOptions Mapping"
description: "SQLParamOptions Mapping"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "mapping deprecated functions [ODBC], SQLParamOptions"
  - "SQLParamOptions function [ODBC], mapping"
---
# SQLParamOptions Mapping
When an application calls **SQLParamOptions** through an ODBC *3.x* driver, the call  
  
```  
SQLParamOptions(hstmt, crow, piRow);  
```  
  
 will be mapped to two calls of **SQLSetStmtAttr** as follows:  
  
```  
SQLSetStmtAttr(hstmt, SQL_ATTR_PARAMSET_SIZE, crow, 0);  
SQLSetStmtAttr(hstmt, SQL_ATTR_PARAMS_PROCESSED_PTR, piRow, 0);  
```
