---
title: "ODBC Driver Major Version Differences"
description: Learn about breaking changes between Microsoft ODBC Driver 18 for SQL Server and version 17, including the encryption default change, new Encrypt values, and new connection string keywords.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, sunilbs, mcimfl
ms.date: 08/26/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ai-usage: ai-assisted
helpviewer_keywords:
  - "ODBC Driver for SQL Server, major version differences"
  - "ODBC Driver 18 for SQL Server, breaking changes"
  - "ODBC Driver 17 for SQL Server, upgrading"
---
# Major version differences in the Microsoft ODBC Driver for SQL Server

This article describes breaking changes between Microsoft ODBC Driver 18 for SQL Server and version 17.

Most applications that upgrade from version 17 to version 18 are affected by a single change: connections are encrypted by default. If your application stops connecting after you upgrade, read [Default encryption behavior](#default-encryption-behavior) first.

## Summary of changes

| Area | Version 17 | Version 18 and later versions |
| --- | --- | --- |
| Default `Encrypt` setting | `no` | `yes` |
| `Encrypt` accepted values | `yes`, `no` | `yes`, `no`, `Mandatory`, `Optional`, `Strict` |
| Encryption modes | Off or on | Off, on, or strict |
| Server certificate validation | Doesn't occur unless you request encryption | Occurs by default, because encryption is on by default |
| Connection string keywords | Base set | Base set plus new keywords |

## Encryption changes

### Default encryption behavior

In version 17, connections aren't encrypted unless you ask for encryption or the server requires it. In version 18 and later versions, connections are encrypted by default.

Because the driver validates the server certificate whenever it encrypts a connection, an application that connects to a server with a self-signed or otherwise untrusted certificate connects successfully with version 17 and fails with version 18. The failure is a certificate validation error, not an authentication error.

You have three ways to resolve it, listed from most to least secure:

- Install a certificate on the server that the client trusts. This option is the recommended one, and it's the only one that keeps both encryption and validation.
- Keep encryption on and tell the driver which name to validate, by setting `HostNameInCertificate` or `ServerCertificate`.
- Turn encryption off to restore the version 17 default, by adding `Encrypt=no` to the connection string.

> [!CAUTION]  
> Setting `TrustServerCertificate=yes` keeps the connection encrypted but disables certificate validation, which leaves the connection open to adversary-in-the-middle attacks. Prefer a trusted certificate.

### Encrypt values

Version 17 accepts only `yes` and `no`. Version 18 and later versions accept those values and add three more:

| Value | Equivalent to | Behavior |
| --- | --- | --- |
| `Mandatory` | `yes` | The connection is encrypted. |
| `Optional` | `no` | The connection isn't encrypted unless the server requires it. |
| `Strict` | None | The connection uses TDS 8.0 encryption. |

`Strict` has no version 17 equivalent. It's the only value that selects TDS 8.0, where encryption is negotiated before the login exchange rather than during it.

### Restore version 17 encryption behavior

To restore the version 17 default behavior, set `Encrypt` explicitly:

```
Driver={ODBC Driver 18 for SQL Server};Server=<server>;Database=<database>;UID=<user_id>;PWD=<password>;Encrypt=no;
```

Setting the value explicitly is beneficial even on version 17, because it makes the connection string behave the same way on both versions.

## New connection string keywords

Version 18 adds the following connection string keywords, which version 17 doesn't recognize:

| Keyword | Purpose |
| --- | --- |
| `ConcatNullYieldsNull` | Controls whether concatenating a null value yields null. |
| `GetDataExtensions` | Controls which `SQLGetData` extensions the driver enables. |
| `HostNameInCertificate` | Specifies the host name to validate in the server's TLS certificate. |
| `IpAddressPreference` | Specifies the IP address family the driver tries first. |
| `LongAsMax` | Maps long data types to their `max` equivalents. |
| `RetryExec` | Configures retry rules for failed queries. |
| `ServerCertificate` | Specifies the path to a certificate file to validate the server against. |

Individual keywords were introduced in different version 18 releases. For the release that introduced each keyword, and for the full keyword reference, see [ODBC DSN and connection string keywords](dsn-connection-string-attribute.md).

## Related content

- [ODBC DSN and connection string keywords](dsn-connection-string-attribute.md)
- [Microsoft ODBC Driver for SQL Server](microsoft-odbc-driver-for-sql-server.md)
- [Release notes for ODBC Driver for SQL Server on Windows](windows/release-notes-odbc-sql-server-windows.md)
- [Release notes for ODBC Driver for SQL Server on Linux and macOS](linux-mac/release-notes-odbc-sql-server-linux-mac.md)
