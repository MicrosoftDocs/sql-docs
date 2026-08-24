---
title: "go-mssqldb Encryption and Certificates"
description: "Configure TLS encryption, certificate validation, and TDS 8.0 strict mode in the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 06/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# go-mssqldb encryption and certificates

The `go-mssqldb` driver supports multiple encryption modes and certificate validation strategies for connections to SQL Server and Azure SQL. This article explains each encryption mode, how to configure certificate validation, and when to use TDS 8.0 strict mode.

## Choose a TLS configuration

Use this table to choose the safest option that fits your environment before you tune individual parameters.

| Scenario | Recommended settings | Why |
| --- | --- | --- |
| Azure SQL or SQL Server 2022 and later versions, and you want the strongest default protection | `encrypt=strict` | TDS 8.0 performs the TLS handshake before TDS negotiation and always validates the certificate. |
| The server certificate chains to a trusted public or corporate CA | `encrypt=true` | Standard certificate validation is usually enough when the host name and trust chain are already correct. |
| The server uses a private CA that isn't in the system trust store | `encrypt=true&certificate=/path/to/ca-cert.pem` | Adds the CA certificate to a custom trust pool and keeps normal chain validation. |
| The server uses a self-signed certificate and you want certificate pinning | `encrypt=true&serverCertificate=/path/to/server.pem` | Compares the exact server certificate byte-for-byte instead of relying on a CA chain. |
| Local development or temporary test environments only | `encrypt=true&TrustServerCertificate=true` | Keeps encryption on, but skips server identity validation. Don't use this setting in production. |

## Encryption modes

Set the `encrypt` connection parameter to one of the following values:

| Value | Behavior |
| --- | --- |
| `strict` | TDS 8.0 encryption. The TLS handshake happens before the TDS connection negotiation. Requires SQL Server 2022 or Azure SQL. |
| `true` or `mandatory` | The connection is encrypted after the TDS prelogin handshake. |
| `false` or `optional` | Encryption is used only if the server requires it. This is the default when `encrypt` isn't specified. Azure SQL always requires encryption server-side, so connections to Azure SQL are encrypted regardless of this setting. |
| `disable` | No encryption. Not recommended for production. |

### Example

Enable encryption in the connection string by using the `encrypt` parameter:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true
```

## Certificate validation

When you enable encryption (`encrypt=true` or `encrypt=strict`), the driver validates the server certificate unless you set `TrustServerCertificate=true`. Two independent approaches exist for this validation.

### Chain validation by using the `certificate` parameter

Provide a PEM or DER certificate file. The driver adds the certificate to a custom trust pool and performs standard X.509 chain validation against that pool:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true&certificate=/path/to/ca-cert.pem
```

### Byte-level comparison by using the `serverCertificate` parameter

> [!NOTE]
> The `serverCertificate` parameter was introduced in driver v1.9.6.

Provide a PEM or DER file containing the exact server certificate. The driver compares the server's certificate byte-for-byte against the provided file. The driver doesn't perform chain validation. This approach is useful for self-signed certificates:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true&serverCertificate=/path/to/server.pem
```

### Host name override

By default, the driver validates that the certificate's Common Name (CN) or Subject Alternative Name (SAN) matches the server host name. Use `hostnameincertificate` to override the expected host name:

```text
sqlserver://<user>:<password>@10.0.0.5:1433?database=AdventureWorks2025&encrypt=true&hostnameincertificate=<server>.domain.com
```

### Skip certificate validation

Set `TrustServerCertificate=true` to skip all certificate validation. The connection is still encrypted, but the server identity isn't verified:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true&TrustServerCertificate=true
```

> [!CAUTION]
> Setting `TrustServerCertificate=true` exposes the connection to adversary-in-the-middle attacks. Use this option only for development and testing.

## TDS 8.0 strict mode

TDS 8.0 (strict encryption) performs the TLS handshake before any TDS protocol negotiation. This approach prevents an attacker from downgrading the entire connection. TDS 8.0 requires SQL Server 2022 or Azure SQL:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=strict
```

In strict mode, the `TrustServerCertificate` parameter is ignored. Certificate validation always occurs, using the system trust store by default. You can supply `certificate` or `serverCertificate` for custom validation.

## Minimum TLS version

Use the `tlsmin` parameter to enforce a minimum TLS version:

```text
sqlserver://<user>:<password>@<server>:1433?database=AdventureWorks2025&encrypt=true&tlsmin=1.2
```

Valid values: `1.0`, `1.1`, `1.2`, `1.3`.

## Encryption parameter summary

| Parameter | Default | Description |
| --- | --- | --- |
| `encrypt` | `false` | Encryption mode. Azure SQL always requires encryption server-side regardless of this setting. |
| `TrustServerCertificate` | Depends on `encrypt` | Skip certificate validation when `true`. The default is `false` when `encrypt` is specified and `true` when `encrypt` is omitted. |
| `certificate` | - | PEM/DER file path for chain validation. |
| `serverCertificate` | - | PEM/DER file path for byte-level comparison. |
| `hostnameincertificate` | - | Override expected host name in certificate. |
| `tlsmin` | - | Minimum TLS version. |

## Related content

- [go-mssqldb connection strings](connection-strings.md)
- [go-mssqldb connection options](connection-options.md)
- [SQL Server and Windows authentication with go-mssqldb](authentication.md)
- [go-mssqldb protocols](protocols.md)
