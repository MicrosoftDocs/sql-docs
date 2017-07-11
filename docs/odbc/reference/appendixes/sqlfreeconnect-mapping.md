---
title: "SQLFreeConnect Mapping | Microsoft Docs"
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
  - "mapping deprecated functions [ODBC], SQLFreeConnect"
  - "SQLFreeConnect function [ODBC], mapping"
ms.assetid: 8a844538-93c0-4709-bab6-35c45e771d80
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
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