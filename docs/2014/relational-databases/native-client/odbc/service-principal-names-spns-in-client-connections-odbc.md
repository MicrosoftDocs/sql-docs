---
title: "Service Principal Names (SPNs) in Client Connections (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
ms.assetid: 1d60cb30-4c46-49b2-89ab-701e77a330a2
author: MightyPen
ms.author: genemi
manager: craigg
---
# Service Principal Names (SPNs) in Client Connections (ODBC)
  This topic describes ODBC attributes and functions that support service principal names (SPNs) in client applications. For more information about SPNs in client applications, see [Service Principal Name &#40;SPN&#41; Support in Client Connections](../features/service-principal-name-spn-support-in-client-connections.md) and [Get Mutual Kerberos Authentication](../../native-client-odbc-how-to/get-mutual-kerberos-authentication.md).  
  
## Connection String Keywords  
 The following connection string keywords enable client applications to specify an SPN.  
  
|Keyword|Value|  
|-------------|-----------|  
|`ServerSPN`|The SPN for the server. The default value is an empty string, which causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, driver-generated SPN.|  
|`FailoverPartnerSPN`|The SPN for the failover partner. The default value is an empty string, which causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, driver-generated SPN.|  
  
## Connection Attributes  
 The following connection attributes enable client applications to specify an SPN and query for the authentication method.  
  
|Name|Type|Usage|  
|----------|----------|-----------|  
|SQL_COPT_SS_SERVER_SPN<br /><br /> SQL_COPT_SS_FAILOVER_PARTNER_SPN|SQLTCHAR, read/write|Specifies the SPN for the server. The default value is an empty string, which causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, driver-generated SPN.<br /><br /> This attribute can be queried only after it has been set programmatically, or after a connection has been opened. If an attempt is made to query this attribute on a connection that is not open and the attribute has not been set programmatically, SQL_ERROR is returned, and a diagnostic record is logged with SQLState 08003 and the message "Connection not open".<br /><br /> If an attempt is made to set this attribute when a connection is open, SQL_ERROR is returned, and a diagnostic record is logged with SQLState HY011 and the message "Operation invalid at this time".|  
|SQL_COPT_SS_INTEGRATED_AUTHENTICATION_METHOD|SQLTCHAR, read-only|Returns the authentication method used for the connection. The value returned to the application is the value that Windows returns to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. Possible values are:<br /><br /> -   "NTLM", which is returned when a connection is opened using NTLM authentication.<br />-   "Kerberos", which is returned when a connection is opened using Kerberos authentication.<br /><br /> This attribute can only be read for an open connection that used Windows Authentication. If an attempt is made to read it before a connection has been opened, SQL_ERROR is returned, and an error is logged with SQLState 08003 and the message "Connection not open".<br /><br /> If this attribute is queried on a connection that did not use Windows Authentication, SQL_ERROR is returned, and an error is logged with SQLState HY092 and the message "Invalid attribute/option identifier (SQL_COPT_SS_INTEGRATED_AUTHENTICATION_METHOD is only available for Trusted Connections)".<br /><br /> If the authentication method cannot be determined, SQL_ERROR is returned, and an error is logged with SQLState HY000 and the message "General error".|  
|SQL_COPT_SS_MUTUALLY_AUTHENTICATED|SQLSMALLINT, read-only|Returns SQL_TRUE if the server in the connection was mutually authenticated; otherwise, returns SQL_FALSE.<br /><br /> This attribute can only be read for an open connection. If an attempt is made to read it before a connection has been opened, SQL_ERROR is returned, and an error is logged with SQLState 08003 and the message "Connection not open".<br /><br /> If this attribute is queried for a connection that did not use Windows Authentication, SQL_FALSE is returned.|  
  
## ODBC Function Support for Specifying SPNs  
 The following ODBC functions support client applications and SPNs:  
  
-   [SQLBrowseConnect](../../native-client-odbc-api/sqlbrowseconnect.md)  
  
-   [SQLConnect](../../native-client-odbc-api/sqlconnect.md)  
  
-   [SQLDriverConnect](../../native-client-odbc-api/sqldriverconnect.md)  
  
-   [SQLGetConnectAttr](../../native-client-odbc-api/sqlgetconnectattr.md)  
  
-   [SQLSetConnectAttr](../../native-client-odbc-api/sqlsetconnectattr.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](sql-server-native-client-odbc.md)  
  
  
