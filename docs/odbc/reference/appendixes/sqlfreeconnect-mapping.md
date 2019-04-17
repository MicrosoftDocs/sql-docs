---
title: "SQLFreeConnect Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "mapping deprecated functions [ODBC], SQLFreeConnect"
  - "SQLFreeConnect function [ODBC], mapping"
ms.assetid: 8a844538-93c0-4709-bab6-35c45e771d80
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLFreeConnect Mapping
When an application calls **SQLFreeConnect** through an ODBC 3*.x* driver, the call to  
  
```  
SQLFreeConnect(hdbc)   
```  
  
 is mapped to  
  
```  
SQLFreeHandle(SQL_HANDLE_DBC,Handle)  
```  
  
 with the *Handle* argument set to the value in *hdbc*.
