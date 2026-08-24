---
title: TDS 8.0
description: This article discusses TDS 8.0, the application layer protocol used by clients to connect to SQL Server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest, jaferebe, jopilov
ms.date: 07/14/2026
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# TDS 8.0

[!INCLUDE [SQL Server 2022, Azure SQL Database, Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sqlserver2022-asdb-asmi-fabricsqldb.md)]

[!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)], [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../../includes/ssazuremi-md.md)] support Tabular Data Stream (TDS) 8.0.

The [Tabular Data Stream (TDS)](/openspecs/windows_protocols/ms-tds/b46a581a-39de-4745-b076-ec4dbb7d13ec) protocol is an application layer protocol used by clients to connect to SQL Server. SQL Server uses Transport Layer Security (TLS) to encrypt data that is transmitted across a network between an instance of SQL Server and a client application.

TDS is a secure protocol, but in previous versions of SQL Server, encryption could be turned off or not enabled. To meet the standards of mandatory encryption while using SQL Server, an iteration of the TDS protocol was introduced: TDS 8.0.

The TLS handshake now precedes any TDS messages, wrapping the TDS session in TLS to enforce encryption, aligning TDS 8.0 with HTTPS and other web protocols. This enhancement significantly contributes to TDS traffic manageability, as standard network appliances are now able to filter and securely passthrough SQL queries.

A benefit to TDS 8.0 compared to previous TDS versions is its compatibility with TLS 1.3, and TLS standards to come. TDS 8.0 is also fully compatible with TLS 1.2 and previous TLS versions.

## How TDS works

The Tabular Data Stream (TDS) protocol is an application-level protocol used for the transfer of requests and responses between clients and database server systems. The client system typically establishes a long-lived connection with the server. Once the connection is established using a transport-level protocol, TDS messages are exchanged to communicate between the client and SQL Server.

During the TDS session lifespan, there are three phases:

- Initialization
- Authentication
- Data exchange

Encryption is negotiated during the initial phase, but TDS negotiation happens over an unencrypted connection. The SQL Server connection looks like this for prior versions to TDS 8.0:

TCP handshake :arrow_right: TDS prelogin (cleartext) and response (cleartext) :arrow_right: TLS handshake :arrow_right: authentication (encrypted) :arrow_right: data exchange (could be encrypted or unencrypted)

With the introduction of TDS 8.0, the SQL Server connections are as follows:

TCP handshake :arrow_right: TLS handshake :arrow_right: TDS prelogin (encrypted) and response (encrypted) :arrow_right: authentication (encrypted) :arrow_right: data exchange (encrypted)

## Compatibility matrix for TDS, TLS, OS and encryption options

You can enable both TLS 1.2 and TLS 1.3 versions at the OS level, which allows client connections to SQL Server to use multiple TDS protocol versions (TDS 7.x and 8.0). Depending on the OS version, TLS 1.2 and TLS 1.3 might be enabled by default. 

Only TDS 7.x supports non-encrypted (optional) communication, TDS 8.0 doesn't support this. TDS 7.x supports encryption using TLS up to version 1.2. TDS 8.0 requires encryption – everything is always encrypted with TDS 8.0 (Encrypt=Strict). TDS 8.0 has no minimum TLS version requirement and supports TLS 1.3.
TLS 1.3 support is dependent on the operating system version. The following table summarizes various scenarios with the encryption options and the corresponding TLS and TDS versions.


| Encrypt<br />option | TLS version enabled | OS version | Expected<br />connection<br />outcome | Notes |
| --- | --- | --- | --- | --- |
| Strict | TLS 1.3 only (or later) | Windows 11<br /><br />Windows Server 2022 and later | Success | TLS 1.3 negotiated; TDS 8.0 triggered (Encrypt=Strict) |
| Strict | TLS 1.2 and TLS 1.3 | Windows 11<br /><br />Windows Server 2022 and later | Success | TLS 1.3 negotiated; TDS 8.0 triggered (Encrypt=Strict) |
| Strict | TLS 1.2 only (or earlier) | Windows 11<br /><br />Windows Server 2022 and later | Success | TLS 1.2 negotiated; TDS 8.0 triggered (Encrypt=Strict) |
| Strict | TLS 1.2 only (or earlier) | Windows 10<br /><br />Windows Server 2019 / 2016 | Success | TLS 1.2 negotiated; TDS 8.0 triggered (TLS 1.3 not available) |
| Mandatory | TLS 1.3 only (or later) | Windows 11<br /><br />Windows Server 2022 and later | Failure | Encrypt=Mandatory is incompatible with TLS 1.3 for TDS 8.0 |
| Mandatory | TLS 1.2 and TLS 1.3 | Windows 11<br /><br />Windows Server 2022 and later | Success | TLS 1.2 negotiated; TDS 8.0 not triggered (Encrypt=Mandatory) |
| Mandatory | TLS 1.2 only (or earlier) | Windows 11<br /><br />Windows Server 2022 and later | Success | TLS 1.2 negotiated; TDS 8.0 not triggered (Encrypt=Mandatory) |
| Mandatory | TLS 1.2 only (or earlier) | Windows 10<br /><br />Windows Server 2019 / 2016 | Success | TLS 1.2 negotiated; TDS 8.0 not supported on this OS (uses TDS 7.x) |
| Optional | TLS 1.3 only (or later) | Windows 11<br /><br />Windows Server 2022 and later | Failure | Encrypt=Optional (false) is TDS 7.x, which is incompatible with TLS 1.3. |
| Optional | TLS 1.2 and TLS 1.3 | Windows 11<br /><br />Windows Server 2022 and later | Success | TLS 1.2 negotiated; TDS 8.0 not triggered (Encrypt=Optional) |
| Optional | TLS 1.2 only (or earlier) | Windows 11<br /><br />Windows Server 2022 and later | Success | TLS 1.2 negotiated; TDS 8.0 not triggered (Encrypt=Optional) |
| Optional | TLS 1.2 only (or earlier) | Windows 10<br /><br />Windows Server 2019 / 2016 | Success | TLS 1.2 negotiated; encryption optional; connection can <br />succeed without encryption |
| Any | TLS 1.3 only (or later) | Windows 10<br /><br />Windows Server 2019 / 2016 | Failure | TLS 1.3 not supported on this OS |

For more information on how clients use different TDS versions, see the keywords usage in [Changes to connection string encryption properties](#additional-changes-to-connection-string-encryption-properties) section.

## SQL Server 2025 support

[!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] introduces TDS 8.0 support for the following command-line tools and SQL Server features:

- [SQL Server Agent](/ssms/agent/sql-server-agent#tds-80-and-strict-encryption-support)
- [sqlcmd utility](../../../tools/sqlcmd/sqlcmd-utility.md#tds-80-support)
- [bcp utility](../../../tools/bcp-utility.md#tds-80-support)
- [SQL VSS Writer](../../../database-engine/configure-windows/sql-writer-service.md)
- [SQL CEIP service](../../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md)
- [Database Mail](../../database-mail/database-mail.md)
- [Polybase](../../polybase/overview.md#sql-server-2025-polybase-enhancements)
- [Always On availability groups](connect-with-strict-encryption.md#connect-to-an-always-on-availability-group)
- [Always On failover cluster instance (FCI)](connect-with-strict-encryption.md#connect-to-a-failover-cluster-instance)
- [Linked servers](../../linked-servers/linked-servers-database-engine.md#sql-server-2025-and-msoledbsql-version-19)<sup>1</sup>
- [Transactional replication](../../replication/transactional/transactional-replication.md#configure-tls-13-encryption)<sup>1</sup>
- [Merge replication](../../replication/merge/merge-replication.md#configure-tls-13-encryption)<sup>1</sup>
- [Snapshot replication](../../replication/snapshot-replication.md#configure-tls-13-encryption)<sup>1</sup>
- [Log shipping](../../../database-engine/log-shipping/about-log-shipping-sql-server.md#enforce-tls-13-encryption)<sup>1</sup>

<sup>1</sup>TDS 8.0 support introduces [breaking changes](../../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md) to these features. 

## Strict connection encryption

To use TDS 8.0, [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] added `strict` as an additional connection encryption option to SQL Server drivers (`Encrypt=strict`). To use the `strict` connection encryption type, download the latest version of the .NET, ODBC, OLE DB, JDBC, PHP, and Python drivers:

- [Microsoft ADO.NET for SQL Server and Azure SQL Database](../../../connect/ado-net/microsoft-ado-net-sql-server.md) version 5.1 or higher
- [ODBC Driver for SQL Server](../../../connect/odbc/download-odbc-driver-for-sql-server.md) version 18.1.2.1 or higher
- [Microsoft OLE DB Driver for SQL Server](../../../connect/oledb/download-oledb-driver-for-sql-server.md) version 19.2.0 or higher
- [Microsoft JDBC Driver for SQL Server](../../../connect/jdbc/microsoft-jdbc-driver-for-sql-server.md) version 11.2.0 or higher
- [Microsoft Drivers for PHP for SQL Server](../../../connect/php/microsoft-php-driver-for-sql-server.md) version 5.10 or higher
- [Python SQL Driver - mssql-python](../../../connect/python/mssql-python/python-sql-driver-mssql-python.md)

In order to prevent a man-in-the-middle attack with `strict` connection encryption, users can't set the `TrustServerCertificate` option to `true` and allow any certificate the server provided. Instead, users would use the `HostNameInCertificate` option to specify the certificate `ServerName` that should be trusted. The certificate supplied by the server would need to pass the certificate validation. For more information on certificate validation, see [Certificate requirements for SQL Server](../../../database-engine/configure-windows/certificate-requirements.md)

## Additional changes to connection string encryption properties

The following options are added to connection strings to encrypt communication:

| Keyword | Default | Description |
| --- | --- | --- |
| `Encrypt` | *false* | **Previous connection string options**<br /><br />Valid options are <br />- `true`, or `yes` <br /> - `false`, or `no`. <br /> For more information, see [Connection String Syntax](/dotnet/framework/data/adonet/connection-string-syntax#enable-encryption). When `true`, SQL Server uses TLS 1.2 encryption for all data exchanged between the client and server if the server has a certificate installed.<br /><br />**Latest connection string options**<br /><br />Valid options are  <br/>- `strict`  <br />- `mandatory`, or `true`, or `yes`  <br />- `optional`, or `false`, or `no`. <br /><br />When set to `strict`, SQL Server uses TDS 8.0 for all data exchanged between the client and server.<br /><br />When set to `mandatory`, `true`, or `yes`, SQL Server uses TDS 7.x with TLS/SSL encryption for all data sent between the client and server if the server has a certificate installed.<br /><br />When set to `optional`, `false`, or `no`, the connection uses TDS 7.x and would be encrypted only if required by the SQL Server. |
| `TrustServerCertificate` | *false* | **Previous connection string option**<br /><br />When set to `true` (not recommended), the driver doesn't validate the server TLS/SSL certificate. If `true`, the server TLS/SSL certificate is automatically trusted (bypassing validation) when the communication layer is encrypted using TLS.<br /><br />If `false`, the driver validates the server TLS/SSL certificate. If the server certificate validation fails, the driver raises an error and closes the connection. The default value is `false`. Make sure the value passed to `serverName` exactly matches the `Common Name (CN)` or DNS name in the `Subject Alternate Name` in the server certificate for a TLS/SSL connection to succeed.<br /><br />**Change of behavior for Microsoft SQL Server ODBC Driver 18 and later**<br /><br />If `Encrypt` is set to `strict`, this setting specifies the location of the certificate to be used for server certificate validation (exact match). The driver supports PEM, DER, and CER file extensions.<br /><br />If `Encrypt` is set to `true` or `false`, and the `TrustServerCertificate` property is unspecified or set to `null`, `true`, or `false`, the driver uses the `ServerName` property value on the connection URL as the host name to validate the SQL Server TLS/SSL certificate. |
| `HostNameInCertificate` | `null` | The host name to be used in validating the SQL Server TLS/SSL certificate. If the `HostNameInCertificate` property is unspecified or set to `null`, the driver uses the `ServerName` property value as the host name to validate the SQL Server TLS/SSL certificate. |

## Related content

- [Connect to SQL Server with strict encryption](connect-with-strict-encryption.md)
