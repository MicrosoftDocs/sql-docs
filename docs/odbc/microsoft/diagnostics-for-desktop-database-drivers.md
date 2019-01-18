---
title: "Diagnostics for Desktop Database Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Jet-based ODBC drivers [ODBC], diagnostic information"
  - "desktop database drivers [ODBC], diagnostic information"
  - "ODBC desktop database drivers [ODBC], diagnostic information"
  - "diagnostic information [ODBC], desktop database drivers"
ms.assetid: 1c3740eb-62c6-4009-b4b2-570fcf5661e4
author: MightyPen
ms.author: genemi
manager: craigg
---
# Diagnostics for Desktop Database Drivers
All errors and warnings not checked or partially checked by the Driver Manager are handled by the driver. The driver also maps native errors, or errors returned by the data source, to SQLSTATEs. Each function listed in the *ODBC Programmer's Reference* contains a "Diagnostics" section that specifies conditions and messages.  
  
 Applications call **SQLGetDiagRec** to retrieve SQLSTATE, native error code, and diagnostic messages. Calling **SQLGetDiagField** and specifying the field retrieves individual diagnostic fields. The support level of the diagnostic identifiers is listed in the following table.  
  
|DiagIdentifiers|Support level|  
|---------------------|-------------------|  
|SQL_DIA_DYNAMIC_FUNCTION|Not supported|  
|SQL_DIAG_CLASS_ORIGIN|Supported. Always "ODBC 3.0" for versions 3.0 and later of this driver.|  
|SQL_DIAG_COLUMN_NUMBER|Supported|  
|SQL_DIAG_CURSOR_ROW_COUNT|Not supported|  
|SQL_DIAG_DYNAMIC_FUNCTION_CODE|Not supported|  
|SQL_DIAG_MESSAGE_TEXT|Supported|  
|SQL_DIAG_NATIVE|Supported|  
|SQL_DIAG_NUMBER|Supported|  
|SQL_DIAG_RETURNCODE|Supported but implemented by the Driver Manager|  
|SQL_DIAG_ROW_COUNT|Supported|  
|SQL_DIAG_ROW_NUMBER|Supported|  
|SQL_DIAG_SERVER_NAME|Not supported|  
|SQL_DIAG_SQLSTATE|Supported|  
|SQL_DIAG_SUBCLASS_ORIGIN|Supported|
