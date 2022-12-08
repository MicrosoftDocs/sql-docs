---
description: "Gateways Diagnostic Example"
title: "Gateways Diagnostic Example | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], examples"
  - "gateway diagnostic [ODBC]"
  - "error messages [ODBC], diagnostic messages"
ms.assetid: e0695fac-4593-4b3d-8675-cb8f73dab966
author: David-Engel
ms.author: v-davidengel
---
# Gateways Diagnostic Example
In a gateway architecture, a driver sends requests to a gateway that supports ODBC. The gateway sends the requests to a DBMS. Because it is the component that interfaces with the Driver Manager, the driver formats and returns arguments for **SQLGetDiagRec**.  
  
 For example, if Oracle based a gateway to Rdb on Microsoft Open Data Services and if Rdb could not find the table EMPLOYEE, the gateway might generate this diagnostic message:  
  
```  
"[42S02][-1][DEC][ODS Gateway][Rdb]%SQL-F-RELNOTDEF, Table EMPLOYEE is not defined "  
   "in schema."  
```  
  
 Because the error occurred in the data source, the gateway added a prefix for the data source identifier ([Rdb]) to the diagnostic message. Because the gateway was the component that interfaced with the data source, it added prefixes for its vendor ([DEC]) and identifier ([ODS Gateway]) to the diagnostic message. It also added the SQLSTATE value and the Rdb error code to the beginning of the diagnostic message. This permitted it to preserve the semantics of its own message structure and still supply the ODBC diagnostic information to the driver. The driver parses the error information attached to the error statement by the gateway.  
  
 Because the gateway driver is the component that interfaces with the Driver Manager, it would use the preceding diagnostic message to format and return the following values from **SQLGetDiagRec**:  
  
```  
SQLSTATE:         "42S02"  
Native Error:      -1  
Diagnostic Msg:   "[DEC][ODS Gateway][Rdb]%SQL-F-RELNOTDEF, Table EMPLOYEE is not "  
                  "defined in schema."  
```
