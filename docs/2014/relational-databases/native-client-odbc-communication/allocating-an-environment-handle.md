---
title: "Allocating an Environment Handle | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, environment handles"
  - "ODBC applications, connections"
  - "handles [SQL Server Native Client]"
  - "environment handles [SQLNCLI]"
ms.assetid: 15c1b428-ea6d-4672-894c-f0e289e2da3f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Allocating an Environment Handle
  Before an application can call any ODBC function, it must initialize the ODBC environment and allocate an environment handle. This is the global context handle and placeholder for the other handles in ODBC. You do this by calling **SQLAllocHandle** with the *HandleType* parameter set to SQL_HANDLE_ENV and *InputHandle* set to SQL_NULL_HANDLE.  
  
 After allocating the environment handle, the application must set environment attributes to indicate which version of ODBC function calls it will be using. To use the ODBC 3.*x* functions, call [SQLSetEnvAttr](../native-client-odbc-api/sqlsetenvattr.md) with the *Attribute* parameter set to SQL_ATTR_ODBC_VERSION and *ValuePtr* set to SQL_OV_ODBC3.  
  
## See Also  
 [Communicating with SQL Server &#40;ODBC&#41;](communicating-with-sql-server-odbc.md)  
  
  
