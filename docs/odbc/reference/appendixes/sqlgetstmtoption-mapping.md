---
title: "SQLGetStmtOption Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLGetStmtOption function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLGetStmtOption"
ms.assetid: fa599517-3f3e-4dad-a65a-b8596ae3f330
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetStmtOption Mapping
When an application calls **SQLGetStmtOption** to an ODBC 3*.x* driver that does not support it, the call to  
  
```  
SQLGetStmtOption(hstmt, fOption, pvParam)  
```  
  
 will result as follows:  
  
-   If *fOption* indicates an ODBC-defined statement option that returns a string, the Driver Manager calls  
  
    ```  
    SQLGetStmtAttr(StatementHandle, Attribute, ValuePtr, BufferLength, NULL)  
    ```  
  
-   If *fOption* indicates an ODBC-defined statement option that returns a 32-bit integer value, the Driver Manager calls  
  
    ```  
    SQLGetStmtAttr(StatementHandle, Attribute, ValuePtr, 0, NULL)  
    ```  
  
-   If *fOption* indicates a driver-defined statement option, the Driver Manager calls  
  
    ```  
    SQLGetStmtAttr(StatementHandle, Attribute, ValuePtr, BufferLength, NULL)  
    ```  
  
 In the preceding three cases, the *StatementHandle* argument is set to the value in *hstmt*, the *Attribute* argument is set to the value in *fOption*, and the *ValuePtr* argument is set to the same value as *pvParam*.  
  
 For ODBC-defined string connection options, the Driver Manager sets the *BufferLength* argument in the call to **SQLGetConnectAttr** to the predefined maximum length (SQL_MAX_OPTION_STRING_LENGTH); for a nonstring connection option, *BufferLength* is set to 0.  
  
 The SQL_GET_BOOKMARK statement option has been deprecated in ODBC 3*.x*. For an ODBC 3*.x* driver to work with ODBC 2.*x* applications that use SQL_GET_BOOKMARK, it must support SQL_GET_BOOKMARK. For an ODBC 3*.x* driver to work with ODBC 2.*x* applications, it must support setting SQL_USE_BOOKMARKS to SQL_UB_ON and should expose fixed-length bookmarks. If an ODBC 3*.x* driver supports only variable-length bookmarks, not fixed-length bookmarks, it must return SQLSTATE HYC00 (Optional feature not implemented) if an ODBC 2.*x* application attempts to set SQL_USE_BOOKMARKS to SQL_UB_ON.  
  
 For an ODBC 3*.x* driver, the Driver Manager no longer checks to see whether *Option* is in between SQL_STMT_OPT_MIN and SQL_STMT_OPT_MAX, or is greater than SQL_CONNECT_OPT_DRVR_START. The driver must check this.
