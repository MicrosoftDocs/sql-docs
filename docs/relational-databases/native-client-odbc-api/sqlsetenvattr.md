---
title: "SQLSetEnvAttr | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLSetEnvAttr function"
ms.assetid: d4114571-feca-4330-b2e4-7bfd1050b812
caps.latest.revision: 32
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SQLSetEnvAttr
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [ODBC Programmer's Reference](http://go.microsoft.com/fwlink/?LinkId=45250) defines how ODBC drivers should interpret the **SQLSetEnvAttr** attribute specifications from applications written to either the ODBC 2.*x* or ODBC 3.*x* API. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver complies with those rules.  
  
 One of the attributes controlled by **SQLSetEnvAttr** is whether connection pooling is to be used. If connection pooling is used with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver, the *DriverCompletion* parameter must be set to SQL_DRIVER_NOPROMPT when connecting with either [SQLDriverConnect](../../relational-databases/native-client-odbc-api/sqldriverconnect.md) or **SQLConnect**.  
  
## See Also  
 [SQLSetEnvAttr Function](http://go.microsoft.com/fwlink/?LinkId=59369)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
  