---
description: "Driver Manager's Role in the Connection Process"
title: "Driver Manager's Role in the Connection Process | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "driver manager [ODBC], role in connection process"
  - "connecting to data source [ODBC], driver manager"
  - "connecting to driver [ODBC], driver manager"
  - "ODBC driver manager [ODBC]"
ms.assetid: 77c05630-5a8b-467d-b80e-c705dc06d601
author: David-Engel
ms.author: v-davidengel
---
# Driver Manager's Role in the Connection Process
Remember that applications do not call driver functions directly. Instead, they call Driver Manager functions with the same name and the Driver Manager calls the driver functions. Usually, this happens almost immediately. For example, the application calls **SQLExecute** in the Driver Manager and after a few error checks, the Driver Manager calls **SQLExecute** in the driver.  
  
 The connection process is different. When the application calls **SQLAllocHandle** with the SQL_HANDLE_ENV and SQL_HANDLE_DBC options, the function allocates handles only in the Driver Manager. The Driver Manager does not call this function in the driver because it does not know which driver to call. Similarly, if the application passes the handle of an unconnected connection to **SQLSetConnectAttr** or **SQLGetConnectAttr**, only the Driver Manager executes the function. It stores or gets the attribute value from its connection handle and returns SQLSTATE 08003 (Connection not open) when getting a value for an attribute that has not been set and for which ODBC does not define a default value.  
  
 When the application calls **SQLConnect**, **SQLDriverConnect**, or **SQLBrowseConnect**, the Driver Manager first determines which driver to use. It then checks to determine whether a driver is currently loaded on the connection:  
  
-   If no driver is loaded on the connection, the Driver Manager checks whether the specified driver is loaded on another connection in the same environment. If not, the Driver Manager loads the driver on the connection and calls **SQLAllocHandle** in the driver with the SQL_HANDLE_ENV option.  
  
     The Driver Manager then calls **SQLAllocHandle** in the driver with the SQL_HANDLE_DBC option, whether or not it was just loaded. If the application set any connection attributes, the Driver Manager calls **SQLSetConnectAttr** in the driver; if an error occurs, the Driver Manager's connection function returns SQLSTATE IM006 (Driver's **SQLSetConnectAttr** failed). Finally, the Driver Manager calls the connection function in the driver.  
  
-   If the specified driver is loaded on the connection, the Driver Manager calls only the connection function in the driver. In this case, the driver must make sure that all connection attributes on the connection maintain their current settings.  
  
-   If a different driver is loaded on the connection, the Driver Manager calls **SQLFreeHandle** in the driver to free the connection. If there are no other connections that use the driver, the Driver Manager calls **SQLFreeHandle** in the driver to free the environment and unloads the driver. The Driver Manager then performs the same operations as when a driver is not loaded on the connection.  
  
 The Driver Manager will lock the environment handle (*henv*) before calling a driver's **SQLAllocHandle** and **SQLFreeHandle** when *HandleType* is set to **SQL_HANDLE_DBC**.  
  
 When the application calls **SQLDisconnect**, the Driver Manager calls **SQLDisconnect** in the driver. However, it leaves the driver loaded in case the application reconnects to the driver. When the application calls **SQLFreeHandle** with the SQL_HANDLE_DBC option, the Driver Manager calls **SQLFreeHandle** in the driver. If the driver is not used by any other connections, the Driver Manager then calls **SQLFreeHandle** in the driver with the SQL_HANDLE_ENV option and unloads the driver.
