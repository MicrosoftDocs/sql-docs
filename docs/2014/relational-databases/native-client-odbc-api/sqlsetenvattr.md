---
title: "SQLSetEnvAttr | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLSetEnvAttr function"
ms.assetid: d4114571-feca-4330-b2e4-7bfd1050b812
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetEnvAttr
  The [ODBC Programmer's Reference](https://go.microsoft.com/fwlink/?LinkId=45250) defines how ODBC drivers should interpret the **SQLSetEnvAttr** attribute specifications from applications written to either the ODBC 2.*x* or ODBC 3.*x* API. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver complies with those rules.  
  
 One of the attributes controlled by **SQLSetEnvAttr** is whether connection pooling is to be used. If connection pooling is used with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver, the *DriverCompletion* parameter must be set to SQL_DRIVER_NOPROMPT when connecting with either [SQLDriverConnect](sqldriverconnect.md) or **SQLConnect**.  
  
## See Also  
 [SQLSetEnvAttr Function](https://go.microsoft.com/fwlink/?LinkId=59369)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
