---
title: "DBMS-Based Driver Diagnostic Example | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "DBMS-based driver diagnostic [ODBC]"
  - "diagnostic information [ODBC], examples"
  - "error messages [ODBC], diagnostic messages"
ms.assetid: a80d54b0-43ff-4dfd-b6cb-f4694a5ed765
author: MightyPen
ms.author: genemi
manager: craigg
---
# DBMS-Based Driver Diagnostic Example
A DBMS-based driver sends requests to a DBMS and returns information to the application through the Driver Manager. Because the driver is the component that interfaces with the Driver Manager, it formats and returns arguments for **SQLGetDiagRec**.  
  
 For example, if, using SQL/Services, a Microsoft driver for Oracle Rdb encountered an invalid cursor name, it might return the following values from **SQLGetDiagRec**:  
  
```  
SQLSTATE:         "34000"  
Native Error:      0  
Diagnostic Msg:   "[Microsoft][ODBC Rdb Driver]Invalid cursor name: EMPLOYEE_CURSOR."  
```  
  
 Because the error occurred in the driver, it added prefixes to the diagnostic message for the vendor ([Microsoft]) and the driver ([ODBC Rdb Driver]).  
  
 If the DBMS could not find the table EMPLOYEE, the driver might format and return the following values from **SQLGetDiagRec**:  
  
```  
SQLSTATE:         "42S02"  
Native Error:      -1  
Diagnostic Msg:   "[Microsoft][ODBC Rdb Driver][Rdb] %SQL-F-RELNOTDEF, Table EMPLOYEE "  
                  "is not defined in schema."  
```  
  
 Because the error occurred in the data source, the driver added a prefix for the data source identifier ([Rdb]) to the diagnostic message. Because the driver was the component that interfaced with the data source, it added prefixes for its vendor ([Microsoft]) and identifier ([ODBC Rdb Driver]) to the diagnostic message.
