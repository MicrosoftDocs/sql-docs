---
description: "Allocating the Environment Handle"
title: "Allocating the Environment Handle | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC drivers [ODBC], environment handles"
  - "allocating environment handles [ODBC]"
  - "connecting to driver [ODBC], environment handles"
  - "environment handles [ODBC]"
  - "data sources [ODBC], environment handles"
  - "connecting to data source [ODBC], environment handles"
  - "handles [ODBC], environment"
ms.assetid: 77b5d1d6-7eb7-428d-bf75-a5c5a325d25c
author: David-Engel
ms.author: v-davidengel
---
# Allocating the Environment Handle
The first task for any ODBC application is to load the Driver Manager; how this is done is operating-system dependent. For example, on a computer running Microsoft® Windows NT® Server/Windows 2000 Server, Windows NT Workstation/Windows 2000 Professional, or Microsoft Windows® 95/98, the application either links to the Driver Manager library or calls **LoadLibrary** to load the Driver Manager DLL.  
  
 The next task, which must be done before an application can call any other ODBC function, is to initialize the ODBC environment and allocate an environment handle, as follows:  
  
1.  The application declares a variable of type SQLHENV. It then calls **SQLAllocHandle** and passes the address of this variable and the SQL_HANDLE_ENV option. For example:  
  
    ```  
    SQLHENV henv1;  
  
    SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv1);  
    ```  
  
2.  The Driver Manager allocates a structure in which to store information about the environment, and returns the environment handle in the variable.  
  
 The Driver Manager does not call **SQLAllocHandle** in the driver at this time because it does not know which driver to call. It delays calling **SQLAllocHandle** in the driver until the application calls a function to connect to a data source. For more information, see [Driver Manager's Role in the Connection Process](../../../odbc/reference/develop-app/driver-manager-s-role-in-the-connection-process.md), later in this section.  
  
 When the application has finished using ODBC, it frees the environment handle with **SQLFreeHandle**. After freeing the environment, it is an application programming error to use the environment's handle in a call to an ODBC function; doing so has undefined but probably fatal consequences.  
  
 When **SQLFreeHandle** is called, the driver releases the structure used to store information about the environment. Note that **SQLFreeHandle** cannot be called for an environment handle until after all connection handles on that environment handle have been freed.  
  
 For more information about the environment handle, see [Environment Handles](../../../odbc/reference/develop-app/environment-handles.md).
