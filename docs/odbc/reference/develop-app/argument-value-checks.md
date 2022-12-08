---
description: "Argument Value Checks"
title: "Argument Value Checks | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], driver manager error checking"
  - "argument value checks [ODBC]"
  - "driver manager [ODBC], error checking"
ms.assetid: 37a65f8b-83aa-456c-b7cf-500404abb38a
author: David-Engel
ms.author: v-davidengel
---
# Argument Value Checks
The Driver Manager checks the following types of arguments. Unless otherwise noted, the Driver Manager returns SQL_ERROR for errors in argument values.  
  
-   Environment, connection, and statement handles usually cannot be null pointers. The Driver Manager returns SQL_INVALID_HANDLE when it finds a null handle.  
  
-   Required pointer arguments, such as *OutputHandlePtr* in **SQLAllocHandle** and *CursorName* in **SQLSetCursorName**, cannot be null pointers.  
  
-   Option flags that do not support driver-specific values must be a legal value. For example, *Operation* in **SQLSetPos** must be SQL_POSITION, SQL_REFRESH, SQL_UPDATE, SQL_DELETE, or SQL_ADD.  
  
-   Option flags must be supported in the version of ODBC supported by the driver. For example, *InfoType* in **SQLGetInfo** cannot be SQL_ASYNC_MODE (introduced in ODBC 3.0) when calling an ODBC 2.0 driver.  
  
-   Column and parameter numbers must be greater than 0 or greater than or equal to 0, depending on the function. The driver must check the upper limit of these argument values based on the current result set or SQL statement.  
  
-   Length/indicator arguments and data buffer length arguments must contain appropriate values. For example, the argument that specifies the length of a table name in **SQLColumns** (*NameLength3*) must be SQL_NTS or a value greater than 0; *BufferLength* in **SQLDescribeCol** must be greater than or equal to 0. The driver might also need to check these arguments. For example, it might check that *NameLength3* is less than or equal to the maximum length of a table name in the data source.
