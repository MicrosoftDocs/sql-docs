---
title: ODBC DSN and Connection String Keywords
description: How to connect using the ODBC driver. Find keywords for connection strings and DSNs, and connection attributes for SQLSetConnectAttr and SQLGetConnectAttr.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, sunilbs, mcimfl, randolphwest
ms.date: 11/05/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# DSN and connection string keywords and attributes

This article lists the keywords that the Microsoft ODBC Driver for SQL Server accepts in data source names (DSNs) and connection strings. It also lists connection attributes for `SQLSetConnectAttr` and `SQLGetConnectAttr`.

## Choose a DSN or DSN-less connection

For guidance on choosing connection methods, authentication, encryption, routing, retries, and application settings, see [Configure ODBC connections to Microsoft SQL](configure-connections.md). Use this article to look up exact keywords and connection attributes.

## Supported DSN and connection string keywords and connection attributes

The following table lists the available keywords and attributes for each platform (L: Linux; M: macOS; W: Windows). Select the keyword or attribute for more details.

| DSN / connection string keyword | Connection attribute | Platform |
| --- | --- | --- |
| [Addr](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Address](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [AnsiNPW](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_ANSI_NPW](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssansinpw) | LMW |
| [APP](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [ApplicationIntent](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_APPLICATION_INTENT](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssapplicationintent) | LMW |
| [AttachDBFileName](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_ATTACHDBFILENAME](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssattachdbfilename) | LMW |
| [Authentication](dsn-connection-string-attribute.md#authentication---sql_copt_ss_authentication) | [SQL_COPT_SS_AUTHENTICATION](dsn-connection-string-attribute.md#authentication---sql_copt_ss_authentication) | LMW |
| [AutoTranslate](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_TRANSLATE](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsstranslate) | LMW |
| [ClientCertificate](dsn-connection-string-attribute.md#clientcertificate) | | LMW |
| [ClientKey](dsn-connection-string-attribute.md#clientkey) | | LMW |
| [ColumnEncryption](dsn-connection-string-attribute.md#columnencryption---sql_copt_ss_column_encryption) | [SQL_COPT_SS_COLUMN_ENCRYPTION](dsn-connection-string-attribute.md#columnencryption---sql_copt_ss_column_encryption) | LMW |
| [ConcatNullYieldsNull](#concatnullyieldsnull---sql_copt_ss_concat_null) (v18.6 and later versions) | [SQL_COPT_SS_CONCAT_NULL](#concatnullyieldsnull---sql_copt_ss_concat_null) | LMW |
| [ConnectRetryCount](connection-resiliency.md) | [SQL_COPT_SS_CONNECT_RETRY_COUNT](connection-resiliency.md) | LMW |
| [ConnectRetryInterval](connection-resiliency.md) | [SQL_COPT_SS_CONNECT_RETRY_INTERVAL](connection-resiliency.md) | LMW |
| [ConnectTimeout](#connecttimeout-keyword) (v18.7 and later versions) | [SQL_ATTR_LOGIN_TIMEOUT](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | LMW |
| [Database](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_ATTR_CURRENT_CATALOG](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | LMW |
| [Description](dsn-connection-string-attribute.md#description) | | LMW |
| [Driver](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [DSN](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Encrypt](#encrypt) | [SQL_COPT_SS_ENCRYPT](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssencrypt) | LMW |
| [Failover_Partner](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_FAILOVER_PARTNER](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssfailoverpartner) | W |
| [FailoverPartner](#connection-string-keyword-unification) (v18.7 and later versions) | [SQL_COPT_SS_FAILOVER_PARTNER](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssfailoverpartner) | W |
| [FailoverPartnerSPN](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_FAILOVER_PARTNER_SPN](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md) | W |
| [FileDSN](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [GetDataExtensions](windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md#getdataextensions) (v18.0 and later versions) | [SQL_COPT_SS_GETDATA_EXTENSIONS](windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md#getdataextensions) | LMW |
| [HostnameInCertificate](dsn-connection-string-attribute.md#hostnameincertificate) (v18.0 and later versions) | | LMW |
| [IpAddressPreference](dsn-connection-string-attribute.md#ipaddresspreference) (v18.1 and later versions) | | LMW |
| [KeepAlive](configure-connections.md#linux-and-macos-keep-alive-configuration) (v17.4 and later versions; DSN only before 17.8) | | LMW |
| [KeepAliveInterval](configure-connections.md#linux-and-macos-keep-alive-configuration) (v17.4 and later versions; DSN only before 17.8) | | LMW |
| [KeystoreAuthentication](using-always-encrypted-with-the-odbc-driver.md#connection-string-keywords) | | LMW |
| [KeystorePrincipalId](using-always-encrypted-with-the-odbc-driver.md#connection-string-keywords) | | LMW |
| [KeystoreSecret](using-always-encrypted-with-the-odbc-driver.md#connection-string-keywords) | | LMW |
| [Language](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [LongAsMax](windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md#longasmax) (v18.0 and later versions) | [SQL_COPT_SS_LONGASMAX](dsn-connection-string-attribute.md#sql_copt_ss_longasmax) | LMW |
| [MARS_Connection](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_MARS_ENABLED](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssmarsenabled) | LMW |
| [MultipleActiveResultSets](#connection-string-keyword-unification) (v18.7 and later versions) | [SQL_COPT_SS_MARS_ENABLED](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssmarsenabled) | LMW |
| [MultiSubnetFailover](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_MULTISUBNET_FAILOVER](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssmultisubnetfailover) | LMW |
| [Net](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Network](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [PacketSize](#packetsize) (Driver 18.7) | [SQL_ATTR_PACKET_SIZE](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | LMW |
| [PWD](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [QueryLog_On](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_QUERY](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfquery) | W |
| [QueryLogFile](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_QUERY_LOG](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfquerylog) | W |
| [QueryLogTime](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_QUERY_INTERVAL](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfqueryinterval) | W |
| [QuotedId](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_QUOTED_IDENT](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssquotedident) | LMW |
| [Regional](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Replication](dsn-connection-string-attribute.md#replication) | | LMW |
| [RetryExec](dsn-connection-string-attribute.md#retryexec) (18.1 and later versions) | | LMW |
| [SaveFile](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [Server](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [ServerCertificate](dsn-connection-string-attribute.md#servercertificate) (v18.1 and later versions) | | LMW |
| [ServerSPN](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_SERVER_SPN](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md) | LMW |
| [StatsLog_On](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_DATA](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfdata) | W |
| [StatsLogFile](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_PERF_DATA_LOG](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfdatalog) | W |
| [TransparentNetworkIPResolution](dsn-connection-string-attribute.md#transparentnetworkipresolution---sql_copt_ss_tnir) | [SQL_COPT_SS_TNIR](dsn-connection-string-attribute.md#transparentnetworkipresolution---sql_copt_ss_tnir) | LMW |
| [Trusted_Connection](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_INTEGRATED_SECURITY](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssintegratedsecurity) | LMW |
| [TrustServerCertificate](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | [SQL_COPT_SS_TRUST_SERVER_CERTIFICATE](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsstrustservercertificate) | LMW |
| [UID](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md) | | LMW |
| [UseFMTONLY](dsn-connection-string-attribute.md#usefmtonly) | | LMW |
| [vectorTypeSupport](vector-data-type.md#enable-vector-support) (v18.6.1.1 and later versions) | [SQL_COPT_SS_VECTOR_TYPE_SUPPORT](vector-data-type.md#enable-vector-support) | LMW |
| [WorkstationID](#connection-string-keyword-unification) (v18.7 and later versions) | | LMW |
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
| | [SQL_ATTR_DBC_INFO_TOKEN](../../odbc/reference/syntax/sqlsetconnectattr-function.md) | W |
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
| | [SQL_COPT_SS_ANSI_OEM](dsn-connection-string-attribute.md#sql_copt_ss_ansi_oem) | W |
| | [SQL_COPT_SS_AUTOBEGINTXN](dsn-connection-string-attribute.md#sql_copt_ss_autobegintxn) | LMW |
| | [SQL_COPT_SS_BCP](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssbcp) | LMW |
| | [SQL_COPT_SS_BROWSE_CACHE_DATA](../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md) | LMW |
| | [SQL_COPT_SS_BROWSE_CONNECT](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssbrowseconnect) | LMW |
| | [SQL_COPT_SS_BROWSE_SERVER](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssbrowseserver) | LMW |
| | [SQL_COPT_SS_CEKEYSTOREDATA](dsn-connection-string-attribute.md#sql_copt_ss_cekeystoredata) | LMW |
| | [SQL_COPT_SS_CEKEYSTOREPROVIDER](dsn-connection-string-attribute.md#sql_copt_ss_cekeystoreprovider) | LMW |
| | [SQL_COPT_SS_CLIENT_CONNECTION_ID](../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md) | LMW |
| | [SQL_COPT_SS_CONNECTION_DEAD](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssconnectiondead) | LMW |
| | [SQL_COPT_SS_DATACLASSIFICATION_VERSION](data-classification.md) (v17.4.2 and later versions) | LMW |
| | [SQL_COPT_SS_ENLIST_IN_DTC](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssenlistindtc) | W |
| | [SQL_COPT_SS_ENLIST_IN_XA](dsn-connection-string-attribute.md#sql_copt_ss_enlist_in_xa) | LMW |
| | [SQL_COPT_SS_FALLBACK_CONNECT](dsn-connection-string-attribute.md#sql_copt_ss_fallback_connect) | LMW |
| | [SQL_COPT_SS_INTEGRATED_AUTHENTICATION_METHOD](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md) | LMW |
| | [SQL_COPT_SS_MUTUALLY_AUTHENTICATED](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md) | LMW |
| | [SQL_COPT_SS_OLDPWD](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssoldpwd) | LMW |
| | [SQL_COPT_SS_PERF_DATA_LOG_NOW](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssperfdatalognow) | W |
| | [SQL_COPT_SS_PRESERVE_CURSORS](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsspreservecursors) | LMW |
| | [SQL_COPT_SS_SPID](dsn-connection-string-attribute.md#sql_copt_ss_spid) (v17.5 and later versions) | LMW |
| | [SQL_COPT_SS_TXN_ISOLATION](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsstxnisolation) | LMW |
| | [SQL_COPT_SS_USER_DATA](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptssuserdata) | LMW |
| | [SQL_COPT_SS_WARN_ON_CP_ERROR](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md#sqlcoptsswarnoncperror) | LMW |

`SQL_ATTR_DBC_INFO_TOKEN` is reserved for Windows Driver Manager driver-aware connection pooling. Applications don't set this attribute.

Here are some connection string keywords and connection attributes that aren't documented in [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md), [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md), and [SQLSetConnectAttr Function](../../odbc/reference/syntax/sqlsetconnectattr-function.md).

### Description

Describes the data source.

### SQL_COPT_SS_ANSI_OEM

Controls conversion between Windows and original equipment manufacturer (OEM) code pages.

| Attribute value | Description |
| --- | --- |
| `SQL_AO_OFF` (default) | Translation isn't done. |
| `SQL_AO_ON` | Translation is done. |

### SQL_COPT_SS_AUTOBEGINTXN

Starting in version 17.6, while autocommit is off, use this option to control automatic `BEGIN TRANSACTION` after `ROLLBACK` or `COMMIT`.

| Attribute value | Description |
| --- | --- |
| `SQL_AUTOBEGINTXN_ON` (default) | Automatic `BEGIN TRANSACTION` after `ROLLBACK` or `COMMIT`. |
| `SQL_AUTOBEGINTXN_OFF` | No automatic `BEGIN TRANSACTION` after `ROLLBACK` or `COMMIT`. |

### SQL_COPT_SS_FALLBACK_CONNECT

Controls the use of SQL Server fallback connections. This option is no longer supported.

| Attribute value | Description |
| --- | --- |
| `SQL_FB_OFF` (default) | Disables fallback connections. |
| `SQL_FB_ON` | Enables fallback connections. |

## New connection string keywords and connection attributes

The following sections describe keyword behavior, connection attributes, and their accepted values. Driver-specific restrictions are stated with the relevant option.

### Connection string keyword unification

Starting with version 18.7, the ODBC Driver for SQL Server supports additional connection string keywords that align with property names in the `Microsoft.Data.SqlClient` data provider. This support reduces keyword renaming when moving between Microsoft SQL data providers and drivers. Each provider still has its own supported keywords and values.

#### Keyword aliases

Each alias configures the same option as its equivalent ODBC keyword:

| Alias | Equivalent ODBC keyword | Description |
| --- | --- | --- |
| `MultipleActiveResultSets` | `MARS_Connection` | Turns Multiple Active Result Sets (MARS) on or off. Accepts `Yes` or `No`. |
| `WorkstationID` | `WSID` | Sets the workstation ID, typically the network name of the computer that runs the application. `HOST_NAME()` returns this value. |
| `FailoverPartner` | `Failover_Partner` | Specifies the failover partner server for database mirroring. Supported on Windows only. |

> [!NOTE]
> In version 18.7, `MultipleActiveResultSets` accepts `Yes` or `No`, but not `True` or `False`.

The driver applies the following rules to these aliases:

- **Case-insensitive:** The driver matches aliases regardless of case. For example, `multipleactiveresultsets` behaves the same as `MultipleActiveResultSets`.
- **First occurrence wins:** An alias and its equivalent ODBC keyword configure the same option. If a connection string contains both, the driver uses the first value and ignores subsequent values for that option.
- **Input only:** The driver accepts aliases as input. When it returns an output connection string or writes the setting to a data source name (DSN), it uses the equivalent ODBC keyword.
- **Platform support:** The driver supports `MultipleActiveResultSets` and `WorkstationID` on Windows, Linux, and macOS. The driver supports `FailoverPartner` and `Failover_Partner` on Windows only.

#### ConnectTimeout keyword

The `ConnectTimeout` keyword configures the same login timeout as the [SQL_ATTR_LOGIN_TIMEOUT](../../odbc/reference/syntax/sqlsetconnectattr-function.md) connection attribute. Applications can specify the timeout in the connection string instead of calling [SQLSetConnectAttr](../../odbc/reference/syntax/sqlsetconnectattr-function.md).

If an application explicitly sets `SQL_ATTR_LOGIN_TIMEOUT` before calling a connection function, the connection attribute takes precedence over the `ConnectTimeout` connection string value.

- **Units and default:** The value specifies the number of seconds to wait for the login to complete. The default value is `15` seconds.
- **No timeout:** A value of `0` tells the driver to wait indefinitely.
- **Maximum and clamping:** The maximum value is `65534`. When you supply a negative value, a value greater than `65534`, or a value that exceeds the supported numeric range, the driver uses `65534`. If the driver clamps the value and the connection otherwise succeeds, the driver returns `SQL_SUCCESS_WITH_INFO` with an `01S02` (Option value changed) warning.
- **Validation:** The driver rejects empty and nonnumeric values.
- **DSN support:** On Windows, applications can store and retrieve `ConnectTimeout` in a data source name (DSN).
- **Platform support:** The driver supports `ConnectTimeout` on Windows, Linux, and macOS.

#### Example

The following connection string uses the unified keyword spellings. Replace `<server>`, `<database>`, `<user_id>`, and `<password>` with your values. For Azure SQL Database, use `<server>.database.windows.net` as the hostname:

```ini
Driver={ODBC Driver 18 for SQL Server};Server=tcp:<server>;Database=<database>;UID=<user_id>;PWD=<password>;Encrypt=yes;TrustServerCertificate=no;MultipleActiveResultSets=Yes;WorkstationID=app-node-01;ConnectTimeout=30;
```

The aliases in the preceding connection string configure the same options as the original ODBC keywords in the following connection string:

```ini
Driver={ODBC Driver 18 for SQL Server};Server=tcp:<server>;Database=<database>;UID=<user_id>;PWD=<password>;Encrypt=yes;TrustServerCertificate=no;MARS_Connection=Yes;WSID=app-node-01;
```

Both strings enable MARS and set the workstation ID to `app-node-01`. The first string also waits up to 30 seconds for the login to complete. `ConnectTimeout` has no equivalent keyword in Driver 17 or earlier Driver 18 releases. To set their login timeout, call [SQLSetConnectAttr](../../odbc/reference/syntax/sqlsetconnectattr-function.md) with `SQL_ATTR_LOGIN_TIMEOUT`.

### Authentication - SQL_COPT_SS_AUTHENTICATION

Sets the authentication mode for the connection. For more information, see [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md).

| Keyword value | Attribute value | Description |
| --- | --- | --- |
| | `SQL_AU_NONE` (default) | Without an explicitly set attribute, other connection settings determine authentication. Explicitly setting this attribute overrides the DSN or connection string's `Authentication` selection. |
| `SqlPassword` | `SQL_AU_PASSWORD` | SQL authentication with a user name and password. |
| `ActiveDirectoryIntegrated` | `SQL_AU_AD_INTEGRATED` | Microsoft Entra integrated authentication. |
| `ActiveDirectoryInteractive` | `SQL_AU_AD_INTERACTIVE` | Microsoft Entra interactive authentication. |
| `ActiveDirectoryMsi` | `SQL_AU_AD_MSI` | Microsoft Entra managed identity authentication. For a user-assigned identity, set `UID` to its client ID on Azure App Service or Azure Container Instances; use its object ID on other supported hosts. Omit `UID` for a system-assigned identity. |
| `ActiveDirectoryServicePrincipal` | `SQL_AU_AD_SPA` | Microsoft Entra service principal authentication. `UID` is set to the client ID of the service principal. `PWD` is set to the client secret. |
| `ActiveDirectoryPassword` | `SQL_AU_AD_PASSWORD` | Deprecated Microsoft Entra user name and password authentication. For more information, see [ActiveDirectoryPassword deprecation](using-azure-active-directory.md#activedirectorypassword-is-deprecated). |
| | `SQL_AU_RESET` | Clears an explicitly set authentication attribute so the DSN or connection string's `Authentication` setting can apply again. |

> [!NOTE]  
> When using the `Authentication` keyword or attribute, explicitly set `Encrypt` in the connection string, DSN, or connection attribute. For behavior and defaults, see [Encrypt](#encrypt).

### ColumnEncryption - SQL_COPT_SS_COLUMN_ENCRYPTION

Controls transparent column encryption (Always Encrypted). For more information, see [Using Always Encrypted with the ODBC Driver for SQL Server](using-always-encrypted-with-the-odbc-driver.md).

| Keyword value | Attribute value | Description |
| --- | --- | --- |
| `Enabled` | `SQL_CE_ENABLED` | Enables Always Encrypted. |
| `Disabled` (default) | `SQL_CE_DISABLED` | Disables Always Encrypted. |
| | `SQL_CE_RESULTSETONLY` | Enables decryption only (results and return values). |

### ConcatNullYieldsNull - SQL_COPT_SS_CONCAT_NULL

Controls International Organization for Standardization (ISO) handling of `NULL` when concatenating strings. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).

| Keyword value | Attribute value | Description |
| --- | --- | --- |
| `Yes` (default) | `SQL_CN_ON` | `NULL` concatenation yields `NULL`. |
| `No` | `SQL_CN_OFF` | `NULL` concatenation yields a string. |

### Encrypt

Specifies whether connections use Transport Layer Security (TLS) encryption over the network. Driver 17 accepts `yes` and `no`. Driver 18 also accepts `mandatory` as a synonym for `yes`, `optional` as a synonym for `no`, and `strict`, which selects Tabular Data Stream (TDS) 8.0 encryption and has no Driver 17 equivalent.

Driver 18 defaults to `yes`. Driver 17 ordinarily defaults to `no`, but defaults to `yes` when a nonempty `Authentication` setting or an access token is supplied. Set `Encrypt` explicitly rather than relying on these authentication-dependent defaults.

Regardless of the setting for `Encrypt`, the server login credentials (user name and password) are always encrypted.

The `Encrypt`, `TrustServerCertificate`, and server-side `Force Encryption` settings determine whether connections are encrypted over the network. The following tables show the effect of these settings.

<a id="odbc-driver-18-and-newer"></a>

#### ODBC Driver 18 and later versions

| Encrypt setting | Trust server certificate | Server force encryption | Result |
| --- | --- | --- | --- |
| No | No | No | Server certificate isn't checked.<br />Data sent between client and server isn't encrypted. |
| No | Yes | No | Server certificate isn't checked.<br />Data sent between client and server isn't encrypted. |
| Yes | No | No | Server certificate is checked.<br />Data sent between client and server is encrypted. |
| Yes | Yes | No | Server certificate isn't checked.<br />Data sent between client and server is encrypted. |
| No | No | Yes | Server certificate is checked.<br />Data sent between client and server is encrypted. |
| No | Yes | Yes | Server certificate isn't checked.<br />Data sent between client and server is encrypted. |
| Yes | No | Yes | Server certificate is checked.<br />Data sent between client and server is encrypted. |
| Yes | Yes | Yes | Server certificate isn't checked.<br />Data sent between client and server is encrypted. |
| Strict | - | - | `TrustServerCertificate` is ignored. Server certificate is checked.<br />Data sent between client and server is encrypted. |

> [!NOTE]  
> The `Strict` value is only available against servers that support TDS 8.0 connections.

<a id="odbc-driver-17-and-older"></a>

#### ODBC Driver 17 and earlier versions

| Encrypt setting | Trust server certificate | Server force encryption | Result |
| --- | --- | --- | --- |
| No | No | No | Server certificate isn't checked.<br />Data sent between client and server isn't encrypted. |
| No | Yes | No | Server certificate isn't checked.<br />Data sent between client and server isn't encrypted. |
| Yes | No | No | Server certificate is checked.<br />Data sent between client and server is encrypted. |
| Yes | Yes | No | Server certificate isn't checked.<br />Data sent between client and server is encrypted. |
| No | No | Yes | Server certificate isn't checked.<br />Data sent between client and server is encrypted. |
| No | Yes | Yes | Server certificate isn't checked.<br />Data sent between client and server is encrypted. |
| Yes | No | Yes | Server certificate is checked.<br />Data sent between client and server is encrypted. |
| Yes | Yes | Yes | Server certificate isn't checked.<br />Data sent between client and server is encrypted. |

### TransparentNetworkIPResolution - SQL_COPT_SS_TNIR

Controls the Transparent Network IP Resolution feature, a legacy multi-IP fallback for the ODBC driver. This setting doesn't affect the connection sequence when `MultiSubnetFailover=Yes`, which is the recommended setting for Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, availability group listeners, and failover cluster instances. For more information, see [Use transparent network ip resolution with the ODBC driver](using-transparent-network-ip-resolution.md) or [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md).

| Keyword value | Attribute value | Description |
| --- | --- | --- |
| `Enabled` (default) | `SQL_IS_ON` | Enables Transparent Network IP Resolution. |
| `Disabled` | `SQL_IS_OFF` | Disables Transparent Network IP Resolution. |

### UseFMTONLY

Controls the use of `SET FMTONLY` for metadata when connecting to SQL Server 2012 and later versions.

| Keyword value | Description |
| --- | --- |
| `No` (default) | Use `sp_describe_first_result_set` for metadata if available. |
| `Yes` | Use `SET FMTONLY` for metadata. |

### Replication

Specifies the use of a replication login on ODBC Driver version 17.8 and later versions.

| Keyword value | Description |
| --- | --- |
| `No` (default) | Replication login isn't used. |
| `Yes` | Triggers with the `NOT FOR REPLICATION` option don't fire on the connection. |

### PacketSize

Driver 18.7 supports `PacketSize` to request the network packet size in bytes. The negotiated size can differ from the requested value. Use `SQL_ATTR_PACKET_SIZE` to set the corresponding connection attribute or retrieve the negotiated size. For attribute limits and behavior, see [SQLSetConnectAttr](../../odbc/reference/syntax/sqlsetconnectattr-function.md). Driver 17 applications use the attribute rather than the `PacketSize` keyword.

### RetryExec

Configurable retry logic is available in Driver 18.1 and later versions. Driver 17 doesn't support `RetryExec`. Enable it with the `RetryExec` connection string keyword and a list of rules that control re-execution of `SQLExecDirect` and `SQLExecute`.

After an execution error, the driver selects the first rule whose error list contains that error code. It then checks that rule's query prefix against the incoming command text for `SQLExecDirect` or the prepared text for `SQLExecute`. If the prefix doesn't match, the driver doesn't retry the command or continue to later rules. A later match-all rule isn't a fallback for an error code already listed in an earlier rule.

Retry only operations that are safe to repeat. A retry can execute a command more than once, and a connection failure doesn't establish that a previous write failed to complete.

The value of the `RetryExec` keyword is a list of semicolon-separated retry rules.  
`RetryExec={rule1;rule2}`

A retry rule is as follows: `<errormatch>:<retrypolicy>:<querymatch>`

- **Error match:** A comma-separated list of error codes, such as `1000,2000`.

- **Retry policy:** The retry count followed by the initial delay in seconds. For example, `3,10+7` allows three retries after the initial execution, with delays of 10, 17, and 24 seconds: four executions in total if every attempt fails. Without `+`, the delay doubles for each retry. A trailing `+` without an increment uses the initial delay as the increment.

- **Query match:** The command-text prefix. An empty prefix applies to all commands. For example, `SELECT` matches commands that start with `SELECT`.

Combining all three components together for use in a connection string:

`RetryExec={1000,2000:3,10+7:SELECT}`

For errors `1000` and `2000` on a command that starts with `SELECT`, this rule allows three retries, after 10, 17, and 24 seconds.

**Examples**

`40501,40540:4,5`

For errors `40501` and `40540`, retry up to four times, with an initial delay of 5 seconds, and exponential doubling between each retry. This rule applies to all queries.

`49919:2,10+:CREATE`

For error `49919` on a query that starts with `CREATE`, retry at most twice, initially after 10 seconds, and then 20 seconds.

`49918,40501,10928:5,10+5:SELECT c1`

For errors `49918`, `40501`, and `10928` on queries starting with `SELECT c1`, retry up to five times, waiting 10 seconds on the first retry and increasing the wait by 5 seconds thereafter.

Specify the preceding three rules together in the connection string as follows:

`RetryExec={49918,40501,10928:5,10+5:SELECT c1;49919:2,10+:CREATE;40501,40540:4,5}`

In this combination, error `40501` selects the first rule and retries only commands starting with `SELECT c1`. The final rule doesn't provide a fallback for other commands that fail with `40501`. To apply the final policy to all commands with that error, remove `40501` from the first rule's error list. Prefer nonoverlapping error lists when each code needs one policy.

### ClientCertificate

Specifies the certificate for authentication with loopback connections. This option is available only in SQL Server on Linux. The options are:

| Option value | Description |
| --- | --- |
| `sha1:<hash_value>` | The ODBC driver uses the Secure Hash Algorithm 1 (SHA-1) hash to locate a certificate in the Windows Certificate Store. |
| `subject:<subject>` | The ODBC driver uses the subject to locate a certificate in the Windows Certificate Store. |
| `file:<file_location>[,password:<password>]` | The ODBC driver uses a certificate file. |

If the certificate is in Personal Information Exchange (PFX) format and its private key is password protected, include the `password` keyword. For certificates in Privacy Enhanced Mail (PEM) and Distinguished Encoding Rules (DER) formats, include the `ClientKey` keyword.

### ClientKey

Specifies the file location of the private key for `PEM` or `DER` certificates specified by the `ClientCertificate` attribute. Format:

| Option value | Description |
| --- | --- |
| `file:<file_location>[,password:<password>]` | Specifies the location of the private key file. |

If the private key file is password protected, include the `password` keyword. If the password contains any `,` characters, add an extra `,` character immediately after each one. For example, if the password is `a,b,c`, the escaped password in the connection string is `a,,b,,c`.

### HostnameInCertificate

Specifies the hostname to expect in the server's certificate when [encryption](../../database-engine/configure-windows/configure-sql-server-encryption.md) is negotiated, if it's different from the default value derived from `Addr`, `Address`, or `Server`. The `HostnameInCertificate` option is ignored when you use the `ServerCertificate` option.

### IpAddressPreference

Starting with version 18.1, use this option to specify the type of IP address to prioritize for connections.

The possible options are `IPv4First`, `IPv6First`, and `UsePlatformDefault`. `UsePlatformDefault` connects to addresses in the order they're provided by the system call to resolve the server name. The default value is `IPv4First`, which corresponds to the behavior in previous versions.

### ServerCertificate

Starting with version 18.1, use this option only with strict encryption mode. Use the `ServerCertificate` keyword to specify the path to a certificate file to match against the server's TLS certificate. File matching replaces normal hostname and trust-chain validation, but the driver still checks the server certificate's validity dates. Use a PEM- or DER-encoded certificate file. Files with a `.cer` extension are accepted when they use one of these encodings. The server certificate must exactly match the certificate in the specified file.

### SQL_COPT_SS_ACCESS_TOKEN

Use a Microsoft Entra ID access token for authentication. For more information, see [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md).

| Attribute value | Description |
| --- | --- |
| `NULL` (default) | No access token is supplied. |
| `ACCESSTOKEN*` | Pointer to an access token. |

### SQL_COPT_SS_CEKEYSTOREDATA

Communicates with a loaded keystore provider library. Controls transparent column encryption (Always Encrypted). This attribute has no default value. For more information, see [Custom Keystore Providers](custom-keystore-providers.md).

| Attribute value | Description |
| --- | --- |
| `CEKEYSTOREDATA *` | Communication data structure for a keystore provider library. |

### SQL_COPT_SS_CEKEYSTOREPROVIDER

Loads a keystore provider library for Always Encrypted, or retrieves the names of loaded keystore provider libraries. For more information, see [Custom Keystore Providers](custom-keystore-providers.md). This attribute has no default value.

| Attribute value | Description |
| --- | --- |
| `char *` | Path to a keystore provider library. |

### SQL_COPT_SS_ENLIST_IN_XA

To enlist a connection in [XA distributed transactions](use-xa-with-dtc.md), call `SQLSetConnectAttr` with `SQL_COPT_SS_ENLIST_IN_XA` and a pointer to an `XACALLPARAM` object. This option is supported on Windows (17.3 and later versions), Linux, and macOS.

```cpp
SQLSetConnectAttr(hdbc, SQL_COPT_SS_ENLIST_IN_XA, param, SQL_IS_POINTER);  // XACALLPARAM *param
```

To associate an XA transaction with an ODBC connection only, provide `TRUE` or `FALSE` with `SQL_COPT_SS_ENLIST_IN_XA` instead of the pointer when calling `SQLSetConnectAttr`. This setting is only valid on Windows and can't be used to specify XA operations through a client application.

```cpp
SQLSetConnectAttr(hdbc, SQL_COPT_SS_ENLIST_IN_XA, (SQLPOINTER)TRUE, 0);
```

| Value | Description | Platforms |
| --- | --- | --- |
| `XACALLPARAM` object* | The pointer to `XACALLPARAM` object. | Windows, Linux, and macOS |
| `TRUE` | Associates the XA transaction with the ODBC connection. All related database activities are performed under the protection of the XA transaction. | Windows |
| `FALSE` | Disassociates the transaction with the ODBC connection. | Windows |

For more information about XA transactions, see [Using XA Transactions](use-xa-with-dtc.md).

### SQL_COPT_SS_LONGASMAX

Controls whether the driver sends long character and binary parameters as **varchar(max)**, **nvarchar(max)**, and **varbinary(max)**. Driver 18 supports this option; Driver 17 doesn't.

| Attribute value | Description |
| --- | --- |
| `SQL_XL_OFF` (`0`, default) | Don't convert long types to max types when sending. |
| `SQL_XL_ON` (`1`) | Convert long types to max types when sending. |

These are numeric values for `SQLSetConnectAttr`. The corresponding `LongAsMax` connection string keyword uses `No` and `Yes`.

### SQL_COPT_SS_SPID

Retrieves the session ID of the connection. This property is equivalent to the Transact-SQL (T-SQL) [@@SPID](../../t-sql/functions/spid-transact-sql.md) variable, except that it doesn't incur an extra round trip to the server.

| Attribute value | Description |
| --- | --- |
| `DWORD` | Session ID. |
