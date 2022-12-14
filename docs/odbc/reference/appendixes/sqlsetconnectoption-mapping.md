---
description: "SQLSetConnectOption Mapping"
title: "SQLSetConnectOption Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLSetConnectOption function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLSetConnectOption"
ms.assetid: a1b325cf-0c42-41c1-b141-b5a4fee7e708
author: David-Engel
ms.author: v-davidengel
---
# SQLSetConnectOption Mapping
When an ODBC 2.*x* application calls **SQLSetConnectOption** through an ODBC 3*.x* driver, the call to  
  
```  
SQLSetConnectOption(hdbc, fOption, vParam)  
```  
  
 will result as follows:  
  
-   If *fOption* indicates an ODBC-defined connection attribute that requires a string, the Driver Manager calls  
  
    ```  
    SQLSetConnectAttr(ConnectionHandle, Attribute, ValuePtr, SQL_NTS)  
    ```  
  
-   If *fOption* indicates an ODBC-defined connection attribute that returns a 32-bit integer value, the Driver Manager calls  
  
    ```  
    SQLSetConnectAttr(ConnectionHandle, Attribute, ValuePtr, 0)  
    ```  
  
-   If *fOption* indicates a driver-defined connection attribute, the Driver Manager calls  
  
    ```  
    SQLSetConnectAttr(ConnectionHandle, Attribute, ValuePtr, BufferLength)  
    ```  
  
 In the preceding three cases, the *ConnectionHandle* argument is set to the value in *hdbc*, the *Attribute* argument is set to the value in *fOption*, and the *ValuePtr* argument is set to the same value as *vParam*.  
  
 Because the Driver Manager does not know whether the driver-defined connection attribute needs a string or 32-bit integer value, it has to pass in a valid value for the *BufferLength* argument of **SQLSetConnectAttr**. If the driver has defined special semantics for driver-defined connect attributes and needs to be called using **SQLSetConnectOption**, it must support **SQLSetConnectOption**.  
  
 If an ODBC 2.*x* application calls **SQLSetConnectOption** to set a driver-specific statement option in an ODBC 3*.x* driver, and the option was defined in an ODBC 2.*x* version of the driver, a new manifest constant should be defined for the option in the ODBC 3*.x* driver. If the old manifest constant is used in the call to **SQLSetConnectOption**, the Driver Manager will call **SQLSetConnectAttr** with the **StringLength** argument set to 0.  
  
 For an ODBC 3*.x* driver, the Driver Manager no longer checks to see if *fOption* is in between SQL_CONN_OPT_MIN and SQL_CONN_OPT_MAX, or is greater than SQL_CONNECT_OPT_DRVR_START.  
  
## Setting Statement Options on the Connection Level  
 In ODBC 2.*x*, an application could call **SQLSetConnectOption** to set a statement option. When that is done, the driver establishes the statement option as a default for any statements later allocated for that connection. It is driver-defined whether the driver sets the statement option for any existing statements associated with the specified connection.  
  
 This ability has been deprecated in ODBC 3*.x*. ODBC 3*.x* drivers need only support setting ODBC 2.*x* statement attributes at the connection level if they want to work with ODBC 2.*x* applications that do this. ODBC 3*.x* applications should never set statement attributes at the connection level. ODBC 3*.x* statement attributes cannot be set at the connection level, with the exception of the SQL_ATTR_METADATA_ID and SQL_ATTR_ASYNC_ENABLE attributes, which are both connection attributes and statement attributes, and can be set at either the connection level or the statement level.
