---
description: "SQLParamOptions Mapping"
title: "SQLParamOptions Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "mapping deprecated functions [ODBC], SQLParamOptions"
  - "SQLParamOptions function [ODBC], mapping"
ms.assetid: 57ed65f6-9620-4738-b331-19d2a2b5cae4
author: David-Engel
ms.author: v-davidengel
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
