---
title: TLS 1.3 support
description: This article discusses TLS 1.3 support with SQL Server 2022 and Azure SQL Database.
author: srdan-bozovic-msft
ms.author: srbozovi
ms.date: 05/15/2026
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - ignite-2025
monikerRange: ">=sql-server-ver16 || =azuresqldb-mi-current || =azuresqldb-current || >=sql-server-linux-ver16 || =fabric-sqldb"
---
# TLS 1.3 support

 [!INCLUDE [sqlserver2022-asdb-asdbmi-fabricsqldb](../../../includes/applies-to-version/sqlserver2022-asdb-asmi-fabricsqldb.md)]

[!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] (Beginning with [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]), Azure SQL Database, and Azure SQL Managed Instance support Transport Layer Security (TLS) 1.3 when Tabular Data Stream (TDS) 8.0 is used.

> [!IMPORTANT]
> Even with TLS 1.3 support for TDS connections, TLS 1.2 is still required for starting up [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] satellite services. Don't disable TLS 1.2 on the machine.

[!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and earlier versions don't support TLS 1.3.

## Differences between TLS 1.2 and TLS 1.3

TLS 1.3 reduces the number of round trips from two to one during the handshake phase, making it faster and more secure than TLS 1.2. The server hello packet containing server certificate is encrypted and one Round Trip Time (1-RTT) resumption is discontinued, and replaced with 0-RTT resumption based on client key share. Added security of TLS 1.3 comes from discontinuing certain cyphers and algorithms.

Here's a list of algorithms and ciphers removed in TLS 1.3:

- RC4 stream cipher
- RSA key exchange
- SHA-1 hash function
- CBC (block) mode ciphers
- MD5 algorithm
- Various nonephemeral Diffie-Hellman groups
- EXPORT-strength ciphers
- DES
- 3DES

## Driver support

Review the [Driver feature support matrix](../../../connect/driver-feature-matrix.md) to determine what drivers currently support TLS 1.3.

## Operating system support

Currently, the following operating systems support TLS 1.3:

- [Windows 11](/windows/win32/secauthn/tls-cipher-suites-in-windows-11)
- [Windows Server 2022](/windows/win32/secauthn/tls-cipher-suites-in-windows-server-2022)

## SQL Server 2025 support

[!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] introduces TLS 1.3 support for the following features: 

- [SQL Server Agent](/ssms/agent/sql-server-agent#tds-80-and-strict-encryption-support)
- [Always On availability groups](connect-with-strict-encryption.md#connect-to-an-always-on-availability-group)
- [Always On failover cluster instances (FCI)](connect-with-strict-encryption.md#connect-to-a-failover-cluster-instance)
- [Linked servers](../../linked-servers/linked-servers-database-engine.md#sql-server-2025-and-msoledbsql-version-19)
- [Transactional replication](../../replication/transactional/transactional-replication.md#configure-tls-13-encryption)
- [Merge replication](../../replication/merge/merge-replication.md#configure-tls-13-encryption)
- [Snapshot replication](../../replication/snapshot-replication.md#configure-tls-13-encryption)
- [Log shipping](../../../database-engine/log-shipping/about-log-shipping-sql-server.md#enforce-tls-13-encryption)
- [Database Mail](../../database-mail/database-mail.md)

### Setup limitations

SQL Server 2025 setup fails when TLS 1.3 is the only TLS version enabled on the operating system. The setup process requires TLS 1.2 to be available during installation. After setup completes, TLS 1.2 can be disabled if desired.

The error message during setup is:
`A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - No process is on the other end of the pipe.)`

### Certificate requirements

When using TDS 8.0 with SQL Server 2025, specific certificate requirements must be met:

- **Trusted certificates**: Certificates must be issued by a trusted Certificate Authority (CA). Self-signed certificates are no longer accepted by default with Microsoft OLE DB Driver for SQL Server version 19.
- **Certificate validation**: `TrustServerCertificate` must be set to `False` or `No`. The Microsoft OLE DB Driver for SQL Server version 19 validates the certificate's trust chain and certificate validation can't be bypassed.
- **Subject Alternative Name (SAN) requirements**: Certificates must include both the fully qualified domain name (FQDN) and the Netbios name in the SAN list. SQL Server Management Studio (SSMS) often uses Netbios names when connecting, and missing entries will cause validation errors.
- **Planning SAN entries**: Include all possible client connection names (FQDN, Netbios names, service aliases) during certificate issuance. Adding names later requires creating a new certificate and restarting the SQL Server instance.

For more information on certificate validation, see [Encryption and certificate validation - OLE DB Driver for SQL Server](../../../connect/oledb/features/encryption-and-certificate-validation.md).

### Secure-by-default configurations in SQL Server 2025

SQL Server 2025 introduces secure-by-default configurations for several features that now use TDS 8.0 with encryption enabled by default:

- **SQL Server Agent**: Uses Microsoft OLE DB Driver for SQL Server version 19 with `Encrypt=Mandatory` and requires valid server certificates with `TrustServerCertificate` set to `No`. When the only TLS version enabled is TLS 1.3, you must configure `Encrypt=Strict` (Force Strict Encryption).

- **Always On availability groups and FCIs**: Uses ODBC Driver for SQL Server version 18 with `Encrypt=Mandatory` by default. Unlike other features, Always On availability groups and FCIs allow `TrustServerCertificate=Yes` for self-signed scenarios.

- **Linked servers**: Uses Microsoft OLE DB Driver for SQL Server version 19 with `Encrypt=Mandatory` by default. The encryption parameter must be specified in the connection string when targeting another SQL Server instance.

- **Log shipping**: Uses Microsoft OLE DB Driver for SQL Server version 19 with `Encrypt=Mandatory` and requires valid server certificates. When performing an in-place upgrade from a lower version, which doesn't support the latest security configurations, if encryption settings aren't explicitly overridden with a more secure option, log shipping will use `TrustServerCertificate=Yes` to allow for backwards compatibility. To enforce TLS 1.3 and `Encrypt=Strict` with TDS 8.0 after upgrading, drop and recreate the topology with the updated parameters in the log shipping stored procedures.

- **Replication**: (Transactional, Snapshot, Merge) Uses Microsoft OLE DB Driver for SQL Server version 19 with `Encrypt=Mandatory` and requires valid certificates with `TrustServerCertificate=False`.

- **Database Mail**: The default settings are `Encrypt=Optional` and `TrustServerCertificate=Yes`. When TLS 1.3 is enforced, these values change to `Encrypt=Strict` and `TrustServerCertificate=False`. By default, Azure SQL Managed Instance uses the TLS 1.3 protocol.

- **PolyBase**: Uses ODBC Driver for SQL Server version 18 with `Encrypt=Yes` (`Mandatory`). PolyBase allows `TrustServerCertificate=Yes` for self-signed scenarios.

- **SQL VSS Writer**: When connecting to a SQL Server 2025 instance with `Encryption=Strict`, SQL VSS Writer will use TLS 1.3 and TDS 8.0 for the non-Virtual Device Interface (VDI) part of that connection.

### Component-specific requirements

- **SQL Server Agent with TLS 1.3**: You must use Force Strict Encryption (TDS 8.0) when TLS 1.3 is the only version enabled. Lower encryption settings (`Mandatory` or `Optional`) result in connection failures.

- **SQL Server Agent T-SQL jobs**: SQL Server Agent T-SQL jobs connecting to the local instance inherit the SQL Server Agent encryption settings.

- **PowerShell modules**: SQLPS.exe and the SQLPS PowerShell module are currently unsupported for TDS 8.0.

- **Always On availability groups and FCIs**: To configure strict encryption with TDS 8.0, use the `CLUSTER_CONNECTION_OPTIONS` clause with `Encrypt=Strict` and failover to apply settings.

## Related content

- [TDS 8.0](tds-8.md)
- [Connect to SQL Server with strict encryption](connect-with-strict-encryption.md)
- [Configure TLS 1.3](connect-with-tls-1-3.md)
