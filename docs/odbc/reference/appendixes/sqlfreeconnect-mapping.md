---
title: "SQLFreeConnect Mapping"
description: "SQLFreeConnect Mapping"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "mapping deprecated functions [ODBC], SQLFreeConnect"
  - "SQLFreeConnect function [ODBC], mapping"
---
# SQLFreeConnect Mapping
When an application calls **SQLFreeConnect** through an ODBC *3.x* driver, the call to  
  
```  
SQLFreeConnect(hdbc)   
```  
  
 is mapped to  
  
```  
SQLFreeHandle(SQL_HANDLE_DBC,Handle)  
```  
  
 with the *Handle* argument set to the value in *hdbc*.
