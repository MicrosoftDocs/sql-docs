---
title: "SQLPoolConnect Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLPoolConnect function [ODBC]"
ms.assetid: 41322737-890d-4a81-aed2-06cc3d546962
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLPoolConnect Function
**Conformance**  
 Version Introduced: ODBC 3.8 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLPoolConnect** is used to create a new connection if no connection in the pool can be reused.  
  
## Syntax  
  
```  
SQLRETURN  SQLPoolConnect(  
                SQLHDBC              hDbc,  
                SQLHDBC_INFO_TOKEN   hDbcInfoToken,  
                WCHAR *              wszOutConnectString,  
                SQLSMALLINT          cchConnectStringBuffer,  
                SQLSMALLINT *        cchConnectStringLen );  
```  
  
## Arguments  
 *hDbc*  
 [Input] The connection handle.  
  
 *hDbcInfoToken*  
 [Input] The token handle for the new application connection request.  
  
 *wszOutConnectString*  
 [Output] Pointer to a buffer for the completed connection string. Upon successful connection to the target data source, this buffer contains the completed connection string. Applications should allocate at least 1,024 characters for this buffer.  
  
 If *wszOutConnectString* is NULL, *cchConnectStringLen* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *wszOutConnectString*.  
  
 *cchConnectStringBuffer*  
 [Input] Length of the **wszOutConnectString* buffer, in characters.  
  
 *cchConnectStringLen*  
 [Output] Pointer to a buffer in which to return the total number of characters (excluding the null-termination character) available to return in \**wszOutConnectString*. If the number of characters available to return is greater than or equal to *cchConnectStringBuffer*, the completed connection string in \**wszOutConnectString* is truncated to *cchConnectStringBuffer* minus the length of a null-termination character.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or, SQL_INVALID_HANDLE.  
  
## Diagnostics  
 Similar to [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md) for any input validation error, except that the Driver Manager will use a **HandleType** of SQL_HANDLE_DBC_INFO_TOKEN and a **Handle** of *hDbcInfoToken*.  
  
## Remarks  
 The Driver Manager guarantees that the parent HENV handle of *hDbc* and *hDbcInfoToken* are the same.  
  
 Unlike [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md), there is no *DriverCompletion* argument to prompt users to enter connection information. A prompting dialog is disallowed in the pooling scenario.  
  
 Applications should not call this function directly. An ODBC driver that supports driver-aware connection pooling must implement this function.  
  
 Whenever a driver returns SQL_ERROR or SQL_INVALID_HANDLE, the Driver Manager returns the error to the application (in [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) or [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md)).  
  
 Whenever a driver returns SQL_SUCCESS_WITH_INFO, the Driver Manager will obtain the diagnostic information from *hDbcInfoToken*, and return SQL_SUCCESS_WITH_INFO to the application in [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md) and [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md).  
  
 When an application uses [SQLConnect](../../../odbc/reference/syntax/sqlconnect-function.md), *wszOutConnectString* will be a NULL buffer (the last three parameters will all be set to NULL, 0, NULL). Otherwise, the driver must return the output connection string, which will be returned to application's [SQLDriverConnect Function](../../../odbc/reference/syntax/sqldriverconnect-function.md) call.  
  
 Include sqlspi.h for ODBC driver development.  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)   
 [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md)   
 [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md)
