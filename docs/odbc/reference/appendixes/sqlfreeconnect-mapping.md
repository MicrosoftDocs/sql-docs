---
description: "SQLFreeConnect Mapping"
title: "SQLFreeConnect Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "mapping deprecated functions [ODBC], SQLFreeConnect"
  - "SQLFreeConnect function [ODBC], mapping"
ms.assetid: 8a844538-93c0-4709-bab6-35c45e771d80
author: David-Engel
ms.author: v-davidengel
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
