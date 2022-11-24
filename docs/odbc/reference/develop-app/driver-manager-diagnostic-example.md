---
description: "Driver Manager Diagnostic Example"
title: "Driver Manager Diagnostic Example | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "driver manager [ODBC], diagnostic messages"
  - "diagnostic information [ODBC], examples"
  - "error messages [ODBC], diagnostic messages"
ms.assetid: af8f2d35-d1bf-495c-af25-630654542b7d
author: David-Engel
ms.author: v-davidengel
---
# Driver Manager Diagnostic Example
The Driver Manager can also generate diagnostic messages. For example, if an application passed an invalid direction option to **SQLDataSources**, the Driver Manager might format and return the following values from **SQLGetDiagRec**:  
  
```  
SQLSTATE:         "HY103"  
Native Error:      0  
Diagnostic Msg:   "[Microsoft][ODBC Driver Manager]Direction option out of range"  
```  
  
 Because the error occurred in the Driver Manager, it added prefixes to the diagnostic message for its vendor ([Microsoft]) and its identifier ([ODBC Driver Manager]).
