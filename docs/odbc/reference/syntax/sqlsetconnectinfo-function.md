---
title: "SQLSetConnectInfo Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetConnectInfo function [ODBC]"
ms.assetid: 0782a1c3-c5d1-499b-a8ba-134162db9990
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetConnectInfo Function
**Conformance**  
 Version Introduced: ODBC 3.81 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLSetConnectInfo** is used to set the data source, user ID, and password into the connection info token for an application's [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) call.  
  
## Syntax  
  
```  
SQLRETURN  SQLSetConnectInfo(  
                SQLHDBC_INFO_TOKEN   TokenHandle,  
                WCHAR *              ServerName,  
                SQLSMALLINT          NameLength1,  
                WCHAR *              UserName,  
                SQLSMALLINT          NameLength2,  
                WCHAR *              Authentication,  
                SQLSMALLINT          NameLength3 );  
```  
  
## Arguments  
 *TokenHandle*  
 [Input] Token handle.  
  
 *ServerName*  
 [Input] Data source name. The data might be located on the same computer as the program, or on another computer somewhere on a network. For information about how an application chooses a data source, see [Choosing a Data Source or Driver](../../../odbc/reference/develop-app/choosing-a-data-source-or-driver.md).  
  
 *NameLength1*  
 [Input] Length of **ServerName* in characters.  
  
 *UserName*  
 [Input] User identifier.  
  
 *NameLength2*  
 [Input] Length of **UserName* in characters.  
  
 *Authentication*  
 [Input] Authentication string (typically the password).  
  
 *NameLength3*  
 [Input] Length of **Authentication* in characters.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 Same as [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) for input validation errors, except that the Driver Manager will use a **HandleType** of SQL_HANDLE_DBC_INFO_TOKEN and a **Handle** of *hDbcInfoToken*.  
  
## Remarks  
 Whenever a driver returns SQL_ERROR or SQL_INVALID_HANDLE, the Driver Manager returns the error to the application (in [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) or [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md)).  
  
 Whenever a driver returns SQL_SUCCESS_WITH_INFO, the Driver Manager will obtain the diagnostic information from *hDbcInfoToken*, and return SQL_SUCCESS_WITH_INFO to the application in [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) and [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md).  
  
 Applications should not call this function directly. An ODBC driver that supports driver-aware connection pooling must implement this function.  
  
 Include sqlspi.h for ODBC driver development.  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)   
 [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md)   
 [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md)
