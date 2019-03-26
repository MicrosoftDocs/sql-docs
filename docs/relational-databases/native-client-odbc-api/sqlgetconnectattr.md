---
title: "SQLGetConnectAttr | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLGetConnectAttr function"
ms.assetid: 26e4e69a-44fd-45e3-b47a-ae39184f041b
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLGetConnectAttr
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver defines driver-specific connection attributes. Some of the attributes are available to **SQLGetConnectAttr**, and the function is used to report their current settings. The values reported for these attributes are not guaranteed until after a connection has been made or the attribute has been set using [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md).  
  
 This topic lists the read only attributes. For information about the other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver-specific connection attributes, see [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md).  
  
## SQL_COPT_SS_CONNECTION_DEAD  
 The SQL_COPT_SS_CONNECTION_DEAD attribute reports the state of a connection to a server. The driver queries the network for the current state of the connection.  
  
> [!NOTE]  
>  The standard ODBC connection attribute SQL_ATTR_CONNECTION_DEAD returns the most recent state of the connection. This might not be the current connection state.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_CD_TRUE|The connection to the server has been lost.|  
|SQL_CD_FALSE|The connection is open and available for statement processing.|  
  
## SQL_COPT_SS_CLIENT_CONNECTION_ID  
 The SQL_COPT_SS_CLIENT_CONNECTION_ID attribute retrieves the client connection ID, which can then be used to locate:  
  
-   Diagnostic information in the XEvents log, when enabled.  
  
-   Connection error information in the connection ring buffer.  
  
-   Diagnostic information in the data access tracing logs, when enabled.  
  
 For more information, see [Accessing Diagnostic Information in the Extended Events Log](../../relational-databases/native-client/features/accessing-diagnostic-information-in-the-extended-events-log.md).  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_ERROR|The connection failed.|  
|SQL_SUCCESS|The connection succeeded. The client connection ID will be found in the output buffer.|  
  
## SQL_COPT_SS_PERF_DATA  
 The SQL_COPT_SS_PERF_DATA attribute returns a pointer to a SQLPERF structure containing the current driver performance statistics. **SQLGetConnectAttr** will return NULL if performance logging is not enabled. The statistics in the SQLPERF structure are not dynamically updated by the driver. Call **SQLGetConnectAttr** each time the performance statistics need to be refreshed.  
  
|Value|Description|  
|-----------|-----------------|  
|NULL|Performance logging is not enabled.|  
|Any other value|A pointer to a SQLPERF structure.|  
  
## SQL_COPT_SS_PERF_QUERY  
 The SQL_COPT_SS_PERF_QUERY attribute returns TRUE if logging of long running queries is enabled. The request returns FALSE if query logging is not active.  
  
## SQL_COPT_SS_USER_DATA  
 The SQL_COPT_SS_USER_DATA attribute retrieves the user-data pointer. User data is stored in client-owned memory and recorded per connection. If the user-data pointer has not been set, SQL_UD_NOTSET, a NULL pointer, is returned.  
  
|Value|Description|  
|-----------|-----------------|  
|SQL_UD_NOTSET|No user-data pointer is set.|  
|Any other value|A pointer to the user data.|  
  
## SQLGetConnectAttr Support for Service Principal Names (SPNs)  
 SQLGetConnectAttr can be used to query the value of the new connection attributes SQL_COPT_SS_SERVER_SPN, SQL_COPT_SS_FAILOVER_PARTNER_SPN, SQL_COPT_SS_MUTUALLY_AUTHENTICATED, and SQL_COPT_SS_INTEGRATED_AUTHENTICATION_METHOD. (SQLGetConnectOption can also be used to query these values.)  
  
 SQL_COPT_SS_INTEGRATED_AUTHENTICATION_METHOD is only available for open connections that use Windows Authentication.  
  
 If SQL_COPT_SS_SERVER_SPN or SQL_COPT_SS_FAILOVER_PARTNER has not been set, the default value (an empty string) is returned.  
  
 For more information about SPNs, see [Service Principal Names &#40;SPNs&#41; in Client Connections &#40;ODBC&#41;](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md).  
  
## See Also  
 [SQLGetConnectAttr Function](https://go.microsoft.com/fwlink/?LinkId=59347)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)   
 [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md)   
 [SET ANSI_NULLS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-nulls-transact-sql.md)   
 [SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md)   
 [SET ANSI_WARNINGS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-warnings-transact-sql.md)  
  
  
