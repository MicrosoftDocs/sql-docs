---
title: ODBC DSN and connection string keywords
description: How to connect using the ODBC driver. Find keywords for connection strings and DSNs, and connection attributes for SQLSetConnectAttr and SQLGetConnectAttr.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chojas
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# DSN and Connection String Keywords and Attributes

This page lists the keywords for connection strings and DSNs, and connection attributes for SQLSetConnectAttr and SQLGetConnectAttr, available in the ODBC Driver for SQL Server.

## Supported DSN/Connection String Keywords and Connection Attributes

The following table lists the available keywords and the attributes for each platform (L: Linux; M: macOS; W: Windows). Select the keyword or attribute for more details.

| DSN / Connection String Keyword | Connection Attribute | Platform |
|-|-|-|
| [Addr](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Address](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [AnsiNPW](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_ANSI_NPW](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssansinpw) | LMW |
| [APP](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [ApplicationIntent](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_APPLICATION_INTENT](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssapplicationintent) | LMW |
| [AttachDBFileName](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_ATTACHDBFILENAME](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssattachdbfilename) | LMW |
| [Authentication](dsn-connection-string-attribute.md#authentication---sql_copt_ss_authentication) | [SQL_COPT_SS_AUTHENTICATION](dsn-connection-string-attribute.md#authentication---sql_copt_ss_authentication) | LMW |
| [AutoTranslate](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_TRANSLATE](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsstranslate) | LMW |
| [ColumnEncryption](dsn-connection-string-attribute.md#columnencryption---sql_copt_ss_column_encryption) | [SQL_COPT_SS_COLUMN_ENCRYPTION](dsn-connection-string-attribute.md#columnencryption---sql_copt_ss_column_encryption) | LMW |
| [ConnectRetryCount](connection-resiliency.md) | [SQL_COPT_SS_CONNECT_RETRY_COUNT](connection-resiliency.md) | LMW |
| [ConnectRetryInterval](connection-resiliency.md) | [SQL_COPT_SS_CONNECT_RETRY_INTERVAL](connection-resiliency.md) | LMW |
| [Database](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_ATTR_CURRENT_CATALOG](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | LMW |
| [Description](dsn-connection-string-attribute.md#description) | | LMW |
| [Driver](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [DSN](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Encrypt](#encrypt) | [SQL_COPT_SS_ENCRYPT](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssencrypt) | LMW |
| [Failover_Partner](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_FAILOVER_PARTNER](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssfailoverpartner) | W |
| [FailoverPartnerSPN](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_FAILOVER_PARTNER_SPN](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md) | W |
| [FileDSN](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [GetDataExtensions](windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md#getdataextensions) (v18.0+) | [SQL_COPT_SS_GETDATA_EXTENSIONS](windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md#getdataextensions) | LMW |
| [HostnameInCertificate](dsn-connection-string-attribute.md#hostnameincertificate) (v18.0+) | | LMW |
| [IpAddressPreference](dsn-connection-string-attribute.md#ipaddresspreference) (v18.1+) | | LMW |
| [KeepAlive](linux-mac/connection-string-keywords-and-data-source-names-dsns.md) (v17.4+; DSN only prior to 17.8)| | LMW |
| [KeepAliveInterval](linux-mac/connection-string-keywords-and-data-source-names-dsns.md) (v17.4+; DSN only prior to 17.8) | | LMW |
| [KeystoreAuthentication](using-always-encrypted-with-the-odbc-driver.md#connection-string-keywords) | | LMW |
| [KeystorePrincipalId](using-always-encrypted-with-the-odbc-driver.md#connection-string-keywords) | | LMW |
| [KeystoreSecret](using-always-encrypted-with-the-odbc-driver.md#connection-string-keywords) | | LMW |
| [Language](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [LongAsMax](windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md#longasmax) (v18.0+) | [SQL_COPT_SS_LONGASMAX](dsn-connection-string-attribute.md#sql_copt_ss_longasmax) | LMW |
| [MARS_Connection](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_MARS_ENABLED](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssmarsenabled) | LMW |
| [MultiSubnetFailover](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_MULTISUBNET_FAILOVER](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssmultisubnetfailover) | LMW |
| [Net](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Network](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [PWD](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [QueryLog_On](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_QUERY](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfquery) | W |
| [QueryLogFile](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_QUERY_LOG](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfquerylog) | W |
| [QueryLogTIme](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_QUERY_INTERVAL](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfqueryinterval) | W |
| [QuotedId](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_QUOTED_IDENT](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssquotedident) | LMW |
| [Regional](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Replication](dsn-connection-string-attribute.md#replication) | | LMW |
| [RetryExec](dsn-connection-string-attribute.md#retryexec) (18.1+) | | LMW |
| [SaveFile](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Server](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [ServerCertificate](dsn-connection-string-attribute.md#servercertificate) (v18.1+) | | LMW |
| [ServerSPN](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_SERVER_SPN](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md) | LMW |
| [StatsLog_On](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_DATA](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfdata) | W |
| [StatsLogFile](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_DATA_LOG](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfdatalog) | W |
| [TransparentNetworkIPResolution](dsn-connection-string-attribute.md#transparentnetworkipresolution---sql_copt_ss_tnir) | [SQL_COPT_SS_TNIR](dsn-connection-string-attribute.md#transparentnetworkipresolution---sql_copt_ss_tnir) | LMW |
| [Trusted_Connection](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_INTEGRATED_SECURITY](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssintegratedsecurity) | LMW |
| [TrustServerCertificate](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_TRUST_SERVER_CERTIFICATE](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsstrustservercertificate) | LMW |
| [UID](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [UseFMTONLY](dsn-connection-string-attribute.md#usefmtonly) | | LMW |
| [WSID](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| | [SQL_ATTR_ACCESS_MODE](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_ACCESS_MODE) | LMW |
| | [SQL_ATTR_ASYNC_DBC_EVENT](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | W |
| | [SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | W |
| | [SQL_ATTR_ASYNC_DBC_PCALLBACK](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | W |
| | [SQL_ATTR_ASYNC_DBC_PCONTEXT](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | W |
| | [SQL_ATTR_ASYNC_ENABLE](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | W |
| | [SQL_ATTR_AUTO_IPD](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | LMW |
| | [SQL_ATTR_AUTOCOMMIT](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_AUTOCOMMIT) | LMW |
| | [SQL_ATTR_CONNECTION_DEAD](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | LMW |
| | [SQL_ATTR_CONNECTION_TIMEOUT](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | LMW |
| | [SQL_ATTR_DBC_INFO_TOKEN](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | LMW |
| | [SQL_ATTR_LOGIN_TIMEOUT](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_LOGIN_TIMEOUT) | LMW |
| | [SQL_ATTR_METADATA_ID](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | LMW |
| | [SQL_ATTR_ODBC_CURSORS](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_ODBC_CURSORS) | LMW |
| | [SQL_ATTR_PACKET_SIZE](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_PACKET_SIZE) | LMW |
| | [SQL_ATTR_QUIET_MODE](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_QUIET_MODE) | LMW |
| | [SQL_ATTR_RESET_CONNECTION](../../odbc/reference/develop-driver/upgrading-a-3-5-driver-to-a-3-8-driver.md#connection-pooling) <br> (SQL_COPT_SS_RESET_CONNECTION) | LMW |
| | [SQL_ATTR_TRACE](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_OPT_TRACE) | LMW |
| | [SQL_ATTR_TRACEFILE](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_OPT_TRACEFILE) | LMW |
| | [SQL_ATTR_TRANSLATE_LIB](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_TRANSLATE_DLL) | LMW |
| | [SQL_ATTR_TRANSLATE_OPTION](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_TRANSLATE_OPTION) | LMW |
| | [SQL_ATTR_TXN_ISOLATION](../../odbc/reference/syntax/sqlsetconnectattr-function.md) <br> (SQL_TXN_ISOLATION) | LMW |
| | [SQL_COPT_SS_ACCESS_TOKEN](dsn-connection-string-attribute.md#sql_copt_ss_access_token) | LMW |
| | [SQL_COPT_SS_ANSI_OEM](dsn-connection-string-attribute.md#sql_copt_ss_ansi_oem)| W |
| | [SQL_COPT_SS_AUTOBEGINTXN](dsn-connection-string-attribute.md#sql_copt_ss_autobegintxn)| LMW |
| | [SQL_COPT_SS_BCP](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssbcp) | LMW |
| | [SQL_COPT_SS_BROWSE_CACHE_DATA](../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md) | LMW |
| | [SQL_COPT_SS_BROWSE_CONNECT](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssbrowseconnect) | LMW |
| | [SQL_COPT_SS_BROWSE_SERVER](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssbrowseserver) | LMW |
| | [SQL_COPT_SS_CEKEYSTOREDATA](dsn-connection-string-attribute.md#sql_copt_ss_cekeystoredata) | LMW |
| | [SQL_COPT_SS_CEKEYSTOREPROVIDER](dsn-connection-string-attribute.md#sql_copt_ss_cekeystoreprovider) | LMW |
| | [SQL_COPT_SS_CLIENT_CONNECTION_ID](../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md) | LMW |
| | [SQL_COPT_SS_CONCAT_NULL](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssconcatnull) | LMW |
| | [SQL_COPT_SS_CONNECTION_DEAD](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssconnectiondead) | LMW |
| | [SQL_COPT_SS_ENLIST_IN_DTC](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssenlistindtc) | W |
| | [SQL_COPT_SS_ENLIST_IN_XA](dsn-connection-string-attribute.md#sql_copt_ss_enlist_in_xa) | LMW |
| | [SQL_COPT_SS_FALLBACK_CONNECT](dsn-connection-string-attribute.md#sql_copt_ss_fallback_connect) | LMW |
| | [SQL_COPT_SS_INTEGRATED_AUTHENTICATION_METHOD](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md) | LMW |
| | [SQL_COPT_SS_MUTUALLY_AUTHENTICATED](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md) | LMW |
| | [SQL_COPT_SS_OLDPWD](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssoldpwd) | LMW |
| | [SQL_COPT_SS_PERF_DATA_LOG_NOW](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfdatalognow) | W |
| | [SQL_COPT_SS_PRESERVE_CURSORS](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsspreservecursors) | LMW |
| | [SQL_COPT_SS_SPID](dsn-connection-string-attribute.md#sql_copt_ss_spid) (v17.5+) | LMW |
| | [SQL_COPT_SS_TXN_ISOLATION](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsstxnisolation) | LMW |
| | [SQL_COPT_SS_USER_DATA](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssuserdata) | LMW |
| | [SQL_COPT_SS_WARN_ON_CP_ERROR](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsswarnoncperror) | LMW |
| [ClientCertificate](dsn-connection-string-attribute.md#clientcertificate) | | LMW |
| [ClientKey](dsn-connection-string-attribute.md#clientkey) | | LMW |

Here are some connection string keywords and connection attributes, which aren't documented in [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md), [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md), and [SQLSetConnectAttr Function](../../odbc/reference/syntax/sqlsetconnectattr-function.md).

### Description

Used to describe the data source.

### SQL_COPT_SS_ANSI_OEM

Controls ANSI to OEM conversion of data.

| Attribute Value | Description |
|-|-|
| SQL_AO_OFF | (Default) Translation isn't done. |
| SQL_AO_ON | Translation is done. |

### SQL_COPT_SS_AUTOBEGINTXN

Version 17.6+
While autocommit is off, controls automatic BEGIN TRANSACTION after ROLLBACK or COMMIT.

| Attribute Value | Description |
|-|-|
| SQL_AUTOBEGINTXN_ON | (Default) Automatic BEGIN TRANSACTION after ROLLBACK or COMMIT. |
| SQL_AUTOBEGINTXN_OFF | No automatic BEGIN TRANSACTION after ROLLBACK or COMMIT. |

### SQL_COPT_SS_FALLBACK_CONNECT

Controls the use of SQL Server Fallback Connections. This one is no longer supported.

| Attribute Value | Description |
|-|-|
| SQL_FB_OFF | (Default) Fallback connections are disabled. |
| SQL_FB_ON | Fallback connections are enabled. |

## New Connection String Keywords and Connection Attributes

### Authentication - SQL_COPT_SS_AUTHENTICATION

Sets the authentication mode to use when connecting to SQL Server. For more information, see [Using Azure Active Directory](using-azure-active-directory.md).

| Keyword Value | Attribute Value | Description |
|-|-|-|
| |SQL_AU_NONE|(Default) Not set. Combination of other attributes determines authentication mode.|
|SqlPassword|SQL_AU_PASSWORD|SQL Server authentication with username and password.|
|ActiveDirectoryIntegrated|SQL_AU_AD_INTEGRATED|Azure Active Directory Integrated authentication.|
|ActiveDirectoryPassword|SQL_AU_AD_PASSWORD|Azure Active Directory Password authentication.|
|ActiveDirectoryInteractive|SQL_AU_AD_INTERACTIVE|Azure Active Directory Interactive authentication.|
|ActiveDirectoryMsi|SQL_AU_AD_MSI|Azure Active Directory Managed Identity authentication. For user-assigned identity, UID is set to the object ID of the user identity. |
|ActiveDirectoryServicePrincipal|SQL_AU_AD_SPA|Azure Active Directory Service Principal authentication. UID is set to the client ID of the service principal. PWD is set to the client secret. |
| |SQL_AU_RESET|Unset. Overrides any DSN or connection string setting.|

> [!NOTE]
> When using `Authentication` keyword or attribute, explicitly specify `Encrypt` setting to the desired value in connection string / DSN / connection attribute. Refer to [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) for details.

### ColumnEncryption - SQL_COPT_SS_COLUMN_ENCRYPTION

Controls transparent column encryption (Always Encrypted). For more information, see [Using Always Encrypted (ODBC)](using-always-encrypted-with-the-odbc-driver.md).

| Keyword Value | Attribute Value | Description |
|-|-|-|
|Enabled|SQL_CE_ENABLED|Enables Always Encrypted.|
|Disabled|SQL_CE_DISABLED|(Default) Disables Always Encrypted.|
| |SQL_CE_RESULTSETONLY|Enables decryption only (results and return values).|

### Encrypt

Specifies whether connections use TLS encryption over the network. Possible values are `yes`/`mandatory`(18.0+), `no`/`optional`(18.0+), and `strict`(18.0+). The default value is `yes` in version 18.0+ and `no` in previous versions.

Regardless of the setting for `Encrypt`, the server login credentials (user name and password) are always encrypted.

`Encrypt`, `TrustServerCertificate`, and server-side `Force Encryption` settings play a part in whether connections are encrypted over the network. The following tables show the effect of these settings.

#### ODBC Driver 18 and newer

| **Encrypt Setting** | **Trust Server Certificate** | **Server Force Encryption** | **Result** |
|---------------------|------------------------------|--------------------------|------------|
| No  | No  | No  | Server certificate isn't checked.<br/>Data sent between client and server isn't encrypted. |
| No  | Yes | No  | Server certificate isn't checked.<br/>Data sent between client and server isn't encrypted. |
| Yes | No  | No  | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| Yes | Yes | No  | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| No  | No  | Yes | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| No  | Yes | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| Yes | No  | Yes | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| Yes | Yes | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| Strict | - | - | TrustServerCertificate is ignored. Server certificate is checked.<br/>Data sent between client and server is encrypted. |

> [!NOTE]
> Strict is only available against servers that support TDS 8.0 connections.

### ODBC Driver 17 and older

| **Encrypt Setting** | **Trust Server Certificate** | **Server Force Encryption** | **Result** |
|---------------------|------------------------------|--------------------------|------------|
| No  | No  | No  | Server certificate isn't checked.<br/>Data sent between client and server isn't encrypted. |
| No  | Yes | No  | Server certificate isn't checked.<br/>Data sent between client and server isn't encrypted. |
| Yes | No  | No  | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| Yes | Yes | No  | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| No  | No  | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| No  | Yes | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| Yes | No  | Yes | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| Yes | Yes | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |

### TransparentNetworkIPResolution - SQL_COPT_SS_TNIR

Controls the Transparent Network IP Resolution feature, which interacts with MultiSubnetFailover to allow faster reconnection attempts. For more information, see [Using Transparent Network IP Resolution](using-transparent-network-ip-resolution.md).

| Keyword Value | Attribute Value| Description |
|-|-|-|
|Enabled|SQL_IS_ON|(Default) Enables Transparent Network IP Resolution.|
|Disabled|SQL_IS_OFF|Disables Transparent Network IP Resolution.|

### UseFMTONLY

Controls the use of SET FMTONLY for metadata when connecting to SQL Server 2012 and newer.

| Keyword Value | Description |
|-|-|
|No|(Default) Use sp_describe_first_result_set for metadata if available. |
|Yes| Use SET FMTONLY for metadata. |

### Replication

Specifies the use of a replication login on ODBC Driver version 17.8 and newer.

| Keyword Value | Description |
|-|-|
|No|(Default) Replication login won't be used. |
|Yes| Triggers with the `NOT FOR REPLICATION` option won't fire on the connection. |

### RetryExec

Configurable retry logic is available starting in version 18.1. It automatically re-executes specific ODBC function calls based on configurable conditions. This feature can be enabled through the connection string using the **RetryExec** keyword, along with a list of retry rules. Each retry rule has three colon separated components: an error match, retry policy, and a query match.

The query match determines the retry rule to be used for a given execution, and is matched with the incoming command text (SQLExecDirect) or the prepared command text in the statement object (SQLExecute). If more than one rule matches, the first matching one in the list is used. This allows rules to be listed in order of increasing generality. If no rule matches, then no retry is applied.

When the execution results in an error, and there is an applicable retry rule, its error match is used to determine if the execution should be retried.

The value of the RetryExec keyword is a list of semicolon seperated retry rules.  
`RetryExec={rule1;rule2}`

A retry rule is as follows: `<errormatch>:<retrypolicy>:<querymatch>`

**Error Match:** A comma seperated list of error codes. For example, specifying **1000,2000** would be the error codes you want to retry.

**Retry Policy:** Specifies the delay until the next retry. The first parameter would be the number of retries while the second would be the delay. For example **3,10+7** would be 3 tries starting at 10 and each following retry would increment by 7 seconds. Note that if +7 isn't specified, then each following retry is exponentially doubled.

**Query Match:** Specifies the query you want to match with. If nothing is specified, then it applies to all queries. Specifying **SELECT** would mean for all queries that start with select.

Combining all 3 above components together to use in a connection string would be:

`RetryExec={1000,2000:3,10+7:SELECT}`

Which would mean: "For errors 1000 and 2000, on a query that starts with SELECT. Retry twice with an intial delay of 10 seconds and adding 7 seconds for each following attempt"

**Examples**

`40501,40540:4,5`

For errors 40501 and 40540, retry up to 4 times, with an initial delay of 5 seconds, and exponential doubling between each retry. This applies to all queries.

`49919:2,10+:CREATE`

For error 49919 on a query that starts with CREATE, retry at most twice, initially after 10s, and then 20s.

`49918,40501,10928:5,10+5:SELECT c1`

For errors, 49918, 40501, and 10928 on queries starting with SELECT c1, retry up to 5 times, waiting 10 seconds on the first retry and increasing the wait by 5 seconds thereafter.

The above 3 rules can be specified together in the connection string as follows: 

`RetryExec={49918,40501,10928:5,10+5:SELECT c1;49919:2,10+:CREATE;40501,40540:4,5}`

Note that the most general (match-all) rule has been placed at the end, to allow the two more specific rules before it to match their respective queries.

## ClientCertificate

Specifies the certificate to be used for authentication. The options are:

| Option Value | Description |
|-|-|
| `sha1:<hash_value>` | The ODBC driver uses SHA1 hash to locate a certificate in Windows Certificate Store |
| `subject:<subject>` | The ODBC driver uses subject to locate a certificate in Windows Certificate Store |
| `file:<file_location>[,password:<password>`] | The ODBC driver uses a certificate file. |

In case if certificate is in PFX format and private key inside the PFX certificate is password protected, the password keyword is required. For certificates in PEM and DER formats ClientKey attribute is required

## ClientKey

Specifies a file location of the private key for `PEM` or `DER` certificates that are specified by the ClientCertificate attribute. Format:

| Option Value | Description |
|-|-|
| `file:<file_location>[,password:<password>`] | Specifies location of the private key file. |

In case if private key file is password protected then password keyword is required. If the password contains any "`,`" characters, an extra "`,`" character is added immediately after each one. For example, if the password is "`a,b,c`", the escaped password present in the connection string is "`a,,b,,c`".


### HostnameInCertificate

Specifies the hostname to be expected in the server's certificate when [encryption](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md) is negotiated, if it's different from the default value derived from Addr/Address/Server.

### IpAddressPreference

Available starting with version 18.1, this option allows the user to specify the type of IP Address they want to prioritize for connections. The possible options are "IpAddress= [ IPv4First | IPv6First | UsePlatformDefault]." UsePlatformDefault connects to addresses in the order they are provided by the system call to resolve the server name. The default value is IPv4First, which corresponds to the behavior in previous versions.

### ServerCertificate

Available starting with version 18.1, this option can be used with the strict encryption mode. The **ServerCertificate** keyword is used to specify the path to a certificate file to match against the SQL Server TLS/SSL certificate. The accepted certificate formats are PEM, DER, and CER. If specified, the SQL Server certificate is checked by seeing if the **ServerCertificate** provided is an exact match.

### SQL_COPT_SS_ACCESS_TOKEN

Allows the use of an Azure Active Directory access token for authentication. For more information, see [Using Azure Active Directory](using-azure-active-directory.md).

| Attribute Value | Description |
|-|-|
| NULL | (Default) No access token is supplied. |
| ACCESSTOKEN* | Pointer to an access token. |

### SQL_COPT_SS_CEKEYSTOREDATA

Communicates with a loaded keystore provider library. See Controls transparent column encryption (Always Encrypted). This attribute has no default value. For more information, see [Custom Keystore Providers](custom-keystore-providers.md).

| Attribute Value | Description |
|-|-|
| CEKEYSTOREDATA * | Communication data structure for keystore provider library |

### SQL_COPT_SS_CEKEYSTOREPROVIDER

Loads a keystore provider library for Always Encrypted, or retrieves the names of loaded keystore provider libraries. For more information, see [Custom Keystore Providers](custom-keystore-providers.md). This attribute has no default value.

| Attribute Value | Description |
|-|-|
| char * | Path to a keystore provider library |

### SQL_COPT_SS_ENLIST_IN_XA

To enable XA transactions with an XA-compliant Transaction Processor (TP), the application needs to call **SQLSetConnectAttr** with SQL_COPT_SS_ENLIST_IN_XA and a pointer to an `XACALLPARAM` object. This option is supported on Windows (17.3 and above), Linux, and macOS.

```cpp
SQLSetConnectAttr(hdbc, SQL_COPT_SS_ENLIST_IN_XA, param, SQL_IS_POINTER);  // XACALLPARAM *param
```

To associate an XA transaction with an ODBC connection only, provide TRUE or FALSE with SQL_COPT_SS_ENLIST_IN_XA instead of the pointer when calling **`SQLSetConnectAttr`**. This setting is only valid on Windows and can't be used to specify XA operations through a client application.

```cpp
SQLSetConnectAttr(hdbc, SQL_COPT_SS_ENLIST_IN_XA, (SQLPOINTER)TRUE, 0);
```

|Value|Description|Platforms|
|-----------|-----------------|-----------------|
|XACALLPARAM object*|The pointer to `XACALLPARAM` object.|Windows, Linux, and macOS|
|TRUE|Associates the XA transaction with the ODBC connection. All related database activities will be performed under the protection of the XA transaction.|Windows|
|FALSE|Disassociates the transaction with the ODBC connection.|Windows|

For more information about XA transactions, see [Using XA Transactions](use-xa-with-dtc.md).

### SQL_COPT_SS_LONGASMAX

Allows long type data to be sent to servers as max type data.

| Attribute Value | Description |
|-|-|
|No|(Default) Don't convert long types to max types when sending. |
|Yes| Converts data from long types to max types when sending. |

### SQL_COPT_SS_SPID

Retrieves the server process ID of the connection. This property is equivalent to the T-SQL [@@SPID](../../t-sql/functions/spid-transact-sql.md) variable, except that it doesn't incur an extra round trip to the server.

| Attribute Value | Description |
|-|-|
| DWORD | SPID |
