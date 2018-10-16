---
title: "Allocating a Connection Handle ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "allocating connection handles [ODBC]"
  - "data sources [ODBC], connection handles"
  - "connecting to data source [ODBC], connection handles"
  - "ODBC drivers [ODBC], connection handles"
  - "connecting to driver [ODBC], connection handles"
  - "connection handles [ODBC]"
  - "handles [ODBC], connection"
ms.assetid: c99a8159-7693-4f97-8dcf-401336550e77
author: MightyPen
ms.author: genemi
manager: craigg
---
# Allocating a Connection Handle ODBC
Before the application can connect to a data source or driver, it must allocate a connection handle, as follows:  
  
1.  The application declares a variable of type SQLHDBC. It then calls **SQLAllocHandle** and passes the address of this variable, the handle of the environment in which to allocate the connection, and the SQL_HANDLE_DBC option. For example:  
  
    ```  
    SQLHDBC hdbc1;  
  
    SQLAllocHandle(SQL_HANDLE_DBC, henv1, &hdbc1);  
    ```  
  
2.  The Driver Manager allocates a structure in which to store information about the statement and returns the connection handle in the variable.  
  
 The Driver Manager does not call **SQLAllocHandle** in the driver at this time because it does not know which driver to call. It delays calling **SQLAllocHandle** in the driver until the application calls a function to connect to a data source. For more information, see [Driver Manager's Role in the Connection Process](../../../odbc/reference/develop-app/driver-manager-s-role-in-the-connection-process.md), later in this section.  
  
 It is important to note that allocating a connection handle is not the same as loading a driver. The driver is not loaded until a connection function is called. Thus, after allocating a connection handle and before connecting to the driver or data source, the only functions the application can call with the connection handle are **SQLSetConnectAttr**, **SQLGetConnectAttr**, or **SQLGetInfo** with the SQL_ODBC_VER option. Calling other functions with the connection handle, such as **SQLEndTran**, returns SQLSTATE 08003 (Connection not open). For complete details, see [Appendix B: ODBC State Transition Tables](../../../odbc/reference/appendixes/appendix-b-odbc-state-transition-tables.md).  
  
 For more information about connection handles, see [Connection Handles](../../../odbc/reference/develop-app/connection-handles.md).
