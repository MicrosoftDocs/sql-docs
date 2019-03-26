---
title: "SQLSetDriverConnectInfo Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetDriverConnectInfo function [ODBC]"
ms.assetid: bfd4dfc2-fbca-4ef3-81e5-2706f2389256
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetDriverConnectInfo Function
**Conformance**  
 Version Introduced: ODBC 3.81 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLSetDriverConnectInfo** is used to set the connection string into the connection info token for an application's **SQLDriverConnect** call.  
  
## Syntax  
  
```  
SQLRETURN SQLSetDriverConnectInfo(  
                SQLHDBC_INFO_TOKEN   hDbcInfoToken,  
                WCHAR *              InConnectionString,  
                SQLSMALLINT          StringLength1 );  
```  
  
## Arguments  
 *TokenHandle*  
 [Input] Token handle.  
  
 *InConnectionString*  
 [Input] A full connection string (see the syntax in "Comments" in [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md)), a partial connection string, or an empty string.  
  
 *StringLength1*  
 [Input] Length of **InConnectionString*, in characters if the string is Unicode, or bytes if string is ANSI or DBCS.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 Same as [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md) related to any input validation error, except that the Driver Manager will use a **HandleType** of SQL_HANDLE_DBC_INFO_TOKEN and a **Handle** of *hDbcInfoToken*.  
  
## Remarks  
 Whenever a driver returns SQL_ERROR or SQL_INVALID_HANDLE, the Driver Manager returns the error to the application (in [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) or [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md)).  
  
 Whenever a driver returns SQL_SUCCESS_WITH_INFO, the Driver Manager will obtain the diagnostic information from *hDbcInfoToken*, and return SQL_SUCCESS_WITH_INFO to the application in [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) and [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md).  
  
 Applications should not call this function directly. An ODBC driver that supports driver-aware connection pooling must implement this function.  
  
 Include sqlspi.h for ODBC driver development.  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)   
 [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md)   
 [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md)
