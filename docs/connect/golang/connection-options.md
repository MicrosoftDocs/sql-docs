---
title: "go-mssqldb Connection Options"
description: "Reference for all connection options in the go-mssqldb driver, including timeouts, failover, packet size, and session initialization."
author: dlevy-msft
ms.author: dlevy
ms.date: 08/12/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---
# go-mssqldb connection options

This article provides a complete reference for all connection parameters accepted by the `go-mssqldb` driver. For connection string format syntax, see [Connection strings](connection-strings.md).

The driver resolves the aliases in the following tables only in [ADO format](connection-strings.md#ado-format) connection strings. URL and ODBC format connection strings match parameter names exactly, apart from casing.

## Common parameters

| Parameter | Aliases | Default | Description |
| --- | --- | --- | --- |
| `user id` | `user` | - | SQL Server login name. |
| `password` | - | - | Password for the SQL Server login. |
| `database` | - | - | Target database. |
| `connection timeout` | - | `0` | Timeout in seconds for the login exchange. `0` applies no timeout to the login exchange, but the initial network connection is bounded separately by `dial timeout`. Prefer Go contexts for connection and query timeouts. For [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, the first connection to a paused database fails with error 40613 while the database resumes. Add retry logic rather than a longer timeout. Databases generally resume in less than one minute. For more information, see [Auto-pause and auto-resume](/azure/azure-sql/database/serverless-tier-auto-pause-resume). |
| `dial timeout` | - | `15 x protocol count` | Network dial timeout in seconds. `0` applies the default of 15 seconds per registered protocol rather than waiting indefinitely. |
| `encrypt` | - | `false` | Encryption mode. Azure SQL always requires encryption server-side. See [Encryption and certificates](encryption-certificates.md). |
| `app name` | - | - | Application name sent in the login record. |
| `authenticator` | - | - | Custom authenticator registered by external packages such as `azuread`. |

## Server and port

In ADO and ODBC formats, the server and port are separate parameters:

| Parameter | Default | Description |
| --- | --- | --- |
| `server` | `localhost` | SQL Server host name, optionally including `\instance`. |
| `port` | `1433` | TCP port number. |

In URL format, specify the host and port directly in the URL: `sqlserver://host:port`.

## Failover

| Parameter | Default | Description |
| --- | --- | --- |
| `failoverpartner` | - | Failover partner server host name. |
| `failoverpartnerspn` | - | Service Principal Name (SPN) for the failover partner. |
| `failoverport` | `1433` | TCP port for the failover partner. |

When the primary server is unreachable, the driver attempts to connect to the failover partner automatically.

## Network and performance

| Parameter | Aliases | Default | Description |
| --- | --- | --- | --- |
| `packet size` | - | `4096` | TDS packet size in bytes. Valid range: 512-32767. |
| `keepAlive` | - | `30` | Keep-alive interval in seconds. A value of `0` applies the 30-second default rather than deferring to the operating system. |
| `TrustServerCertificate` | - | Depends on `encrypt` | When `true`, skip server certificate validation. The default is `false` when `encrypt` is specified and `true` when `encrypt` is omitted. Ignored when `encrypt=strict`. Not recommended for production. |
| `multisubnetfailover` | `multi subnet failover` | `true` | Dial behavior when DNS resolves the host to multiple IP addresses. When `true` and DNS returns multiple addresses, the driver dials all resolved endpoints in parallel and uses the first successful connection. When DNS returns a single address, the driver dials it sequentially, so `true` is safe on single-IP targets. Keep the default for availability group listeners, failover cluster instances, and Azure SQL failover group listeners. This driver defaults to `true` for backward compatibility; other Microsoft SQL drivers default to `false`. |

## Application intent

| Parameter | Values | Description |
| --- | --- | --- |
| `ApplicationIntent` | `ReadOnly`, `ReadWrite` | Declares whether the application connects for read-only or read-write operations. Used with AlwaysOn read-only routing. |

## Workstation and SPN

| Parameter | Default | Description |
| --- | --- | --- |
| `Workstation ID` | - | Workstation name sent in the login record. |
| `ServerSPN` | - | Service Principal Name for the SQL Server. Required only in non-default SPN configurations. |

## Protocol

| Parameter | Values | Description |
| --- | --- | --- |
| `protocol` | `tcp`, `np`, `lpc`, `admin` | Transport protocol. See [Protocols](protocols.md). |
| `pipe` | - | Named pipe path. Only applies when `protocol=np`. |

## Always Encrypted

| Parameter | Default | Description |
| --- | --- | --- |
| `columnencryption` | None | Set to `true` to enable Always Encrypted. See [Always Encrypted](always-encrypted.md). |

## Encryption and TLS

| Parameter | Default | Description |
| --- | --- | --- |
| `encrypt` | See [Common parameters](#common-parameters) | `strict` (TDS 8.0), `true`/`mandatory`, `false`/`optional`, `disable`. |
| `TrustServerCertificate` | Depends on `encrypt` | Skip certificate validation when `true`. The default is `false` when `encrypt` is specified and `true` when `encrypt` is omitted. Ignored when `encrypt=strict`. |
| `certificate` | None | Path to a PEM or DER certificate file for certificate chain validation. |
| `serverCertificate` | None | Path to a PEM or DER certificate file for byte-level comparison (v1.5.0 and later). |
| `hostnameincertificate` | None | Override the host name used during TLS certificate validation. |
| `tlsmin` | None | Minimum TLS version: `1.0`, `1.1`, `1.2`, `1.3`. |

For detailed encryption guidance, see [Encryption and certificates](encryption-certificates.md).

## Log flags

Use the `log` parameter to control diagnostic output. Values are bitmask flags, and you can combine them by adding the integer values:

| Value | Description |
| --- | --- |
| `1` | Log errors |
| `2` | Log messages |
| `4` | Log rows |
| `8` | Log SQL statements |
| `16` | Log parameters |
| `32` | Log transactions |
| `64` | Log debug information |
| `128` | Log retries |

Example: to log errors and SQL statements, set `log=9` (1 + 8).

For more information, see [Logging and diagnostics](logging-diagnostics.md).

## uniqueidentifier columns

The driver reads `uniqueidentifier` columns as raw byte arrays by default. To get a standard GUID-formatted string, scan into `mssql.UniqueIdentifier` instead of `string` or `[]byte`. The driver doesn't support automatic GUID conversion through connection parameters.

## SessionInitSQL

`SessionInitSQL` isn't a connection string parameter. Set it through the `NewConnectorConfig` API:

```go
import (
    "database/sql"
    "github.com/microsoft/go-mssqldb"
    "github.com/microsoft/go-mssqldb/msdsn"
)

config := msdsn.Config{
    Host:     "<server>",
    Port:     1433,
    Database: "AdventureWorks2025",
}

connector := mssql.NewConnectorConfig(config)
connector.SessionInitSQL = "SET ANSI_NULLS ON"
db := sql.OpenDB(connector)
```

The SQL statement runs immediately after each new connection is established, before the connection is returned to the caller.

## Related content

- [go-mssqldb connection strings](connection-strings.md)
- [go-mssqldb encryption and certificates](encryption-certificates.md)
- [go-mssqldb protocols](protocols.md)
- [Logging and diagnostics with go-mssqldb](logging-diagnostics.md)
