---
description: "The Driver Manager"
title: "The Driver Manager | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "driver manager [ODBC]"
  - "ODBC architecture [ODBC], driver manager"
  - "driver manager [ODBC], about driver manager"
  - "ODBC driver manager [ODBC]"
ms.assetid: 559e4de1-16c9-4998-94f5-6431122040cd
author: David-Engel
ms.author: v-davidengel
---
# The Driver Manager
The *Driver Manager* is a library that manages communication between applications and drivers. For example, on Microsoft® Windows® platforms, the Driver Manager is a dynamic-link library (DLL) that is written by Microsoft and can be redistributed by users of the redistributable MDAC 2.8 SP1 SDK.  
  
 The Driver Manager exists mainly as a convenience to application writers and solves a number of problems common to all applications. These include determining which driver to load based on a data source name, loading and unloading drivers, and calling functions in drivers.  
  
 To see why the latter is a problem, consider what would happen if the application called functions in the driver directly. Unless the application was linked directly to a particular driver, it would have to build a table of pointers to the functions in that driver and call those functions by pointer. Using the same code for more than one driver at a time would add yet another level of complexity. The application would first have to set a function pointer to point to the correct function in the correct driver, and then call the function through that pointer.  
  
 The Driver Manager solves this problem by providing a single place to call each function. The application is linked to the Driver Manager and calls ODBC functions in the Driver Manager, not the driver. The application identifies the target driver and data source with a *connection handle*. When it loads a driver, the Driver Manager builds a table of pointers to the functions in that driver. It uses the connection handle passed by the application to find the address of the function in the target driver and calls that function by address.  
  
 For the most part, the Driver Manager just passes function calls from the application to the correct driver. However, it also implements some functions (**SQLDataSources**, **SQLDrivers**, and **SQLGetFunctions**) and performs basic error checking. For example, the Driver Manager checks that handles are not null pointers, that functions are called in the correct order, and that certain function arguments are valid. For a complete description of the errors checked by the Driver Manager, see the reference section for each function and [Appendix B: ODBC State Transition Tables](../../odbc/reference/appendixes/appendix-b-odbc-state-transition-tables.md).  
  
 The final major role of the Driver Manager is loading and unloading drivers. The application loads and unloads only the Driver Manager. When it wants to use a particular driver, it calls a connection function (**SQLConnect**, **SQLDriverConnect**, or **SQLBrowseConnect**) in the Driver Manager and specifies the name of a particular data source or driver, such as "Accounting" or "SQL Server." Using this name, the Driver Manager searches the data source information for the driver's file name, such as Sqlsrvr.dll. It then loads the driver (assuming it is not already loaded), stores the address of each function in the driver, and calls the connection function in the driver, which then initializes itself and connects to the data source.  
  
 When the application is done using the driver, it calls **SQLDisconnect** in the Driver Manager. The Driver Manager calls this function in the driver, which disconnects from the data source. However, the Driver Manager keeps the driver in memory in case the application reconnects to it. It unloads the driver only when the application frees the connection used by the driver or uses the connection for a different driver, and no other connections use the driver. For a complete description of the Driver Manager's role in loading and unloading drivers, see [Driver Manager's Role in the Connection Process](../../odbc/reference/develop-app/driver-manager-s-role-in-the-connection-process.md).
