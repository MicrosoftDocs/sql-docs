---
title: "SQLFreeEnv Mapping"
description: "SQLFreeEnv Mapping"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLFreeEnv function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLFreeEnv"
---
# SQLFreeEnv Mapping
When an application calls **SQLFreeEnv** through an ODBC *3.x* driver, the call to  
  
```  
SQLFreeEnv(henv)   
```  
  
 is mapped to  
  
```  
SQLFreeHandle(SQL_HANDLE_ENV,Handle)  
```  
  
 with the *Handle* argument set to the value in *henv*.
