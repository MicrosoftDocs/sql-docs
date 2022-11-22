---
description: "SQLGetConnectOption Mapping"
title: "SQLGetConnectOption Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "mapping deprecated functions [ODBC], SQLGetConnectOption"
  - "SQLGetConnectOption function [ODBC], mapping"
ms.assetid: e3792fe4-a955-473a-a297-c1b2403660c4
author: David-Engel
ms.author: v-davidengel
---
# SQLGetConnectOption Mapping
When an application calls **SQLGetConnectOption** through an ODBC *3.x* driver, the call to  
  
```  
SQLGetConnectOption(hdbc, fOption, pvParam)   
```  
  
 is mapped as follows:  
  
-   If *fOption* indicates an ODBC-defined connection option that returns a string, the Driver Manager calls  
  
    ```  
    SQLGetConnectAttr(ConnectionHandle, Attribute, ValuePtr, BufferLength, NULL)  
    ```  
  
-   If *fOption* indicates an ODBC-defined connection option that returns a 32-bit integer value, the Driver Manager calls  
  
    ```  
    SQLGetConnectAttr(ConnectionHandle, Attribute, ValuePtr, 0, NULL)  
    ```  
  
-   If *fOption* indicates a driver-defined statement option, the Driver Manager calls  
  
    ```  
    SQLGetConnectAttr(ConnectionHandle, Attribute, ValuePtr, BufferLength, NULL)  
    ```  
  
 In the preceding three cases, the *ConnectionHandle* argument is set to the value in *hdbc*, the *Attribute* argument is set to the value in *fOption*, and the *ValuePtr* argument is set to the same value as *pvParam*.  
  
 For ODBC-defined string connection options, the Driver Manager sets the *BufferLength* argument in the call to **SQLGetConnectAttr** to the predefined maximum length (SQL_MAX_OPTION_STRING_LENGTH); for a nonstring connection option, *BufferLength* is set to 0.  
  
 For an ODBC *3.x* driver, the Driver Manager no longer checks to see if *Option* is in between SQL_CONN_OPT_MIN and SQL_CONN_OPT_MAX, or is greater than SQL_CONNECT_OPT_DRVR_START. The driver must check the validity of the option values.
