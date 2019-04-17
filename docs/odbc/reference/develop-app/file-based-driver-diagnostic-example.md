---
title: "File-Based Driver Diagnostic Example | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "file-based driver diagnostic [ODBC]"
  - "diagnostic information [ODBC], examples"
  - "error messages [ODBC], diagnostic messages"
ms.assetid: 0575fccd-4641-478d-a3cc-5a764e35bae2
author: MightyPen
ms.author: genemi
manager: craigg
---
# File-Based Driver Diagnostic Example
A file-based driver acts both as an ODBC driver and as a data source. It can therefore generate errors and warnings both as a component in an ODBC connection and as a data source. Because it also is the component that interfaces with the Driver Manager, it formats and returns arguments for **SQLGetDiagRec**.  
  
 For example, if a Microsoft® driver for dBASE could not allocate sufficient memory, it might return the following values from **SQLGetDiagRec**:  
  
```  
SQLSTATE:         "HY001"  
Native Error:      42052  
Diagnostic Msg:   "[Microsoft][ODBC dBASE Driver]Unable to allocate sufficient memory."  
```  
  
 Because this error was not related to the data source, the driver only added prefixes to the diagnostic message for the vendor ([Microsoft]) and the driver ([ODBC dBASE Driver]).  
  
 If the driver could not find the file Employee.dbf, it might return the following values from **SQLGetDiagRec**:  
  
```  
SQLSTATE:         "42S02"  
Native Error:      -1305  
Diagnostic Msg:   "[Microsoft][ODBC dBASE Driver][dBASE]No such table or object"  
```  
  
 Because this error was related to the data source, the driver added the file format of the data source ([dBASE]) as a prefix to the diagnostic message. Because the driver was also the component that interfaced with the data source, it added prefixes for the vendor ([Microsoft]) and the driver ([ODBC dBASE Driver]).
