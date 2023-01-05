---
title: Driver-Aware Connection Pooling in the ODBC Driver
description: Learn about enhancements made to driver-aware connection pooling in the Microsoft ODBC Driver for SQL Server on Windows.
author: David-Engel
ms.author: v-davidengel
ms.date: 02/15/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Driver-Aware Connection Pooling in the ODBC Driver for SQL Server

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md). This article describes the enhancements made to driver-aware connection pooling in the Microsoft ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows:

- Whatever the connection properties, connections that use `SQLDriverConnect` go into a separate pool from connections that use `SQLConnect`.
- When using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication and driver-aware connection pooling, the driver doesn't use the Windows user's security context for the current thread to separate connections in the pool. That is, if connections are equivalent in their parameters for Windows impersonation scenarios with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication, and they're using the same [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication credentials to connect to the backend, different Windows users can potentially use the same pool of connections. When using Windows Authentication and driver-aware connection pooling, the driver uses the current Windows user's security context to separate connections in the pool. That is, for Windows impersonation scenarios, different Windows users don't share connections even if the connections use the same parameters.
- When using Azure Active Directory and driver-aware connection pooling, the driver also uses the Authentication value to determine the membership in the connection pool.
- Driver-aware connection pooling prevents a bad connection from being returned from the pool.
- Driver-aware connection pooling recognizes driver-specific connection attributes. So, if a connection uses `SQL_COPT_SS_APPLICATION_INTENT` set to read only, that connection gets its own connection pool.
- Setting the `SQL_COPT_SS_ACCESS_TOKEN` attribute causes a connection to be pooled separately

If one of the following connection-attribute IDs or connection string keywords is different between your connection string and the pooled connection string, the driver uses a pooled connection. However, performance is better if all connection attribute IDs or connection string keywords match. (To match a connection in the pool, the driver resets the attribute.) Performance degrades because resetting the following parameters requires an extra network call.

- If two or more of the following connection attributes or connection keywords differ, a pooled connection isn't used.

  - `Language`
  - `QuoteId`
  - `SQL_ATTR_TXN_ISOLATION`
  - `SQL_COPT_SS_QUOTED_IDENT`

- If there's a difference in any of the following connection keywords between your connection string and a pooled connection string, a pooled connection isn't used.

    |Keyword|ODBC Driver 13+|ODBC Driver 11|
    |-|-|-|
    |`Address`|Yes|Yes|
    |`AnsiNPW`|Yes|Yes|
    |`App`|Yes|Yes|
    |`ApplicationIntent`|Yes|Yes|
    |`Authentication`|Yes|No|
    |`ColumnEncryption`|Yes|No|
    |`Database`|Yes|Yes|
    |`Encrypt`|Yes|Yes|
    |`Failover_Partner`|Yes|Yes|
    |`FailoverPartnerSPN`|Yes|Yes|
    |`MARS_Connection`|Yes|Yes|
    |`Network`|Yes|Yes|
    |`PWD`|Yes|Yes|
    |`Server`|Yes|Yes|
    |`ServerSPN`|Yes|Yes|
    |`TransparentNetworkIPResolution`|Yes|Yes|
    |`Trusted_Connection`|Yes|Yes|
    |`TrustServerCertificate`|Yes|Yes|
    |`UID`|Yes|Yes|
    |`WSID`|Yes|Yes|

- If there's a difference in any of the following connection attributes between your connection string and a pooled connection string, a pooled connection isn't used.

    |Attribute|ODBC Driver 13+|ODBC Driver 11|
    |-|-|-|
    |`SQL_ATTR_CURRENT_CATALOG`|Yes|Yes|
    |`SQL_ATTR_PACKET_SIZE`|Yes|Yes|
    |`SQL_COPT_SS_ANSI_NPW`|Yes|Yes|
    |`SQL_COPT_SS_ACCESS_TOKEN`|Yes|No|
    |`SQL_COPT_SS_AUTHENTICATION`|Yes|No|
    |`SQL_COPT_SS_ATTACHDBFILENAME`|Yes|Yes|
    |`SQL_COPT_SS_BCP`|Yes|Yes|
    |`SQL_COPT_SS_COLUMN_ENCRYPTION`|Yes|No|
    |`SQL_COPT_SS_CONCAT_NULL`|Yes|Yes|
    |`SQL_COPT_SS_ENCRYPT`|Yes|Yes|
    |`SQL_COPT_SS_FAILOVER_PARTNER`|Yes|Yes|
    |`SQL_COPT_SS_FAILOVER_PARTNER_SPN`|Yes|Yes|
    |`SQL_COPT_SS_INTEGRATED_SECURITY`|Yes|Yes|
    |`SQL_COPT_SS_MARS_ENABLED`|Yes|Yes|
    |`SQL_COPT_SS_OLDPWD`|Yes|Yes|
    |`SQL_COPT_SS_SERVER_SPN`|Yes|Yes|
    |`SQL_COPT_SS_TRUST_SERVER_CERTIFICATE`|Yes|Yes|
    |`SSPROP_AUTH_REPL_SERVER_NAME`|Yes|Yes|
    |`SQL_COPT_SS_TNIR`|Yes|No|

- The driver can reset and adjust the following connection keywords and attributes without making an extra network call. The driver resets these parameters to ensure that the connection doesn't contain incorrect information.

    These connection keywords aren't considered when the Driver Manager tries to match your connection with a connection in the pool. (Even if you change one of these parameters, an existing connection can be reused. The driver will reset the options, as needed.) These attributes can be reset in the client side without making an extra network call.

    |Keyword|ODBC Driver 13+|ODBC Driver 11|
    |-|-|-|
    |`AutoTranslate`|Yes|Yes|
    |`Description`|Yes|Yes|
    |`MultisubnetFailover`|Yes|Yes|
    |`QueryLog_On`|Yes|Yes|
    |`QueryLogFile`|Yes|Yes|
    |`QueryLogTime`|Yes|Yes|
    |`Regional`|Yes|Yes|
    |`StatsLog_On`|Yes|Yes|
    |`StatsLogFile`|Yes|Yes|

    If you change one of the following connection attributes, an existing connection can be reused.  The driver will reset the value, as needed. The driver can reset these attributes in the client without making an extra network call.

    |Attribute|ODBC Driver 13+|ODBC Driver 11|
    |-|-|-|
    |All statement attributes|Yes|Yes|
    |`SQL_ATTR_AUTOCOMMIT`|Yes|Yes|
    |`SQL_ATTR_CONNECTION_TIMEOUT`|Yes|Yes|
    |`SQL_ATTR_DISCONNECT_BEHAVIOR SQL_ATTR_CONNECTION_TIMEOUT`|Yes|Yes|
    |`SQL_ATTR_LOGIN_TIMEOUT`|Yes|Yes|
    |`SQL_ATTR_ODBC_CURSORS`|Yes|Yes|
    |`SQL_COPT_SS_PERF_DATA`|Yes|Yes|
    |`SQL_COPT_SS_PERF_DATA_LOG`|Yes|Yes|
    |`SQL_COPT_SS_PERF_DATA_LOG_NOW`|Yes|Yes|
    |`SQL_COPT_SS_PERF_QUERY`|Yes|Yes|
    |`SQL_COPT_SS_PERF_QUERY_INTERVAL`|Yes|Yes|
    |`SQL_COPT_SS_PERF_QUERY_LOG`|Yes|Yes|
    |`SQL_COPT_SS_PRESERVE_CURSORS`|Yes|Yes|
    |`SQL_COPT_SS_TRANSLATE`|Yes|Yes|
    |`SQL_COPT_SS_USER_DATA`|Yes|Yes|
    |`SQL_COPT_SS_WARN_ON_CP_ERROR`|Yes|Yes|

## See Also

[Microsoft ODBC Driver for SQL Server on Windows](microsoft-odbc-driver-for-sql-server-on-windows.md)
