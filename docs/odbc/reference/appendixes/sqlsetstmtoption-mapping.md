---
title: "SQLSetStmtOption Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "mapping deprecated functions [ODBC], SQLSetStmtOption"
  - "SQLSetStmtOption function [ODBC], mapping"
ms.assetid: 6a9921aa-8a53-4668-9b13-87164062f1e5
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetStmtOption Mapping
When an application calls **SQLSetStmtOption** through an ODBC 3*.x* driver, the call to  
  
```  
SQLSetStmtOption(StatementHandle, fOption, vParam)  
```  
  
 will result as follows:  
  
-   If *fOption* indicates an ODBC-defined statement attribute that is a string, the Driver Manager calls  
  
    ```  
    SQLSetStmtAttr(StatementHandle, fOption, ValuePtr, SQL_NTS)  
    ```  
  
-   If *fOption* indicates an ODBC-defined statement attribute that returns a 32-bit integer value, the Driver Manager calls  
  
    ```  
    SQLSetStmtAttr(StatementHandle, fOption, ValuePtr, 0)  
    ```  
  
-   If *fOption* indicates a driver-defined statement attribute, the Driver Manager calls  
  
    ```  
    SQLSetStmtAttr(StatementHandle, fOption, ValuePtr, BufferLength)  
    ```  
  
 In the preceding three cases, the **StatementHandle** argument is set to the value in *hstmt*, the *Attribute* argument is set to the value in *fOption*, and the *ValuePtr* argument is set to the value as *vParam*.  
  
 Because the Driver Manager does not know whether the driver-defined statement attribute needs a string or 32-bit integer value, it has to pass in a valid value for the *StringLength* argument of **SQLSetStmtAttr**. If the driver has defined special semantics for driver-defined statement attributes and needs to be called using **SQLSetStmtOption**, it must support **SQLSetStmtOption**.  
  
 If an application calls **SQLSetStmtOption** to set a driver-specific statement option in an ODBC 3*.x* driver and the option was defined in an ODBC 2.*x* version of the driver, a new manifest constant should be defined for the option in the ODBC 3*.x* driver. If the old manifest constant is used in the call to **SQLSetStmtOption**, the Driver Manager will call **SQLSetStmtAttr** with the *StringLength* argument set to 0.  
  
 When an application calls **SQLSetStmtAttr** to set SQL_ATTR_USE_BOOKMARKS to SQL_UB_ON in an ODBC 3*.x* driver, the SQL_ATTR_USE_BOOKMARKS statement attribute is set to SQL_UB_FIXED. SQL_UB_ON is the same constant as SQL_UB_FIXED. The Driver Manager passes SQL_UB_FIXED through to the driver. SQL_UB_FIXED has been deprecated in ODBC 3*.x*, but an ODBC 3*.x* driver must implement it to work with ODBC 2.*x* applications that use fixed-length bookmarks.  
  
 For an ODBC 3*.x* driver, the Driver Manager no longer checks to see if *Option* is in between SQL_STMT_OPT_MIN and SQL_STMT_OPT_MAX, or is greater than SQL_CONNECT_OPT_DRVR_START.
