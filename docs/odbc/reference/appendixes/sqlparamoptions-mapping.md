---
title: "SQLParamOptions Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "mapping deprecated functions [ODBC], SQLParamOptions"
  - "SQLParamOptions function [ODBC], mapping"
ms.assetid: 57ed65f6-9620-4738-b331-19d2a2b5cae4
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLParamOptions Mapping
When an application calls **SQLParamOptions** through an ODBC 3*.x* driver, the call  
  
```  
SQLParamOptions(hstmt, crow, piRow);  
```  
  
 will be mapped to two calls of **SQLSetStmtAttr** as follows:  
  
```  
SQLSetStmtAttr(hstmt, SQL_ATTR_PARAMSET_SIZE, crow, 0);  
SQLSetStmtAttr(hstmt, SQL_ATTR_PARAMS_PROCESSED_PTR, piRow, 0);  
```