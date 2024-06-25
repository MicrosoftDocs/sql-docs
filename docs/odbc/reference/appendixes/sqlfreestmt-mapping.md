---
title: "SQLFreeStmt Mapping"
description: "SQLFreeStmt Mapping"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLFreeStmt function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLFreeStmt"
---
# SQLFreeStmt Mapping
When an application calls **SQLFreeStmt** with an *Option* argument of SQL_DROP through an ODBC *3.x* driver, the call to  
  
```  
SQLFreeStmt(hstmt, SQL_DROP)   
```  
  
 is mapped to  
  
```  
SQLFreeHandle(SQL_HANDLE_STMT,Handle)  
```  
  
 with the *Handle* argument set to the value in *hstmt*.
