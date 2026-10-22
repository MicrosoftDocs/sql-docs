---
title: "ODBC Driver Major Version Differences"
description: Learn about breaking changes between Microsoft ODBC Driver 18 for SQL Server and version 17, including the encryption default change, new Encrypt values, and new connection string keywords.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, sunilbs, mcimfl
ms.date: 10/10/2026
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

This article describes breaking changes between Microsoft ODBC Driver 18 for SQL Server and version 17. Use it when you choose a driver version, upgrade an application from version 17 to version 18, or need to make a connection string behave consistently on both versions.

Most applications that upgrade from version 17 to version 18 are affected by a single change: connections are encrypted by default. If your application stops connecting after you upgrade, read [Default encryption behavior](#default-encryption-behavior) first.

## Choose version 17 or version 18

Driver 18 is the current major version. Driver 17 remains supported, but applications that use it still need the latest minor version in the Driver 17 line to receive fixes. For support dates and servicing rules, see [Support lifecycle](support-lifecycle.md).

| Scenario | Recommended path |
| --- | --- |
| New application | Start with Driver 18. Configure encryption and certificate validation before production. |
| Existing Driver 17 application | Install Driver 18 alongside Driver 17, change the driver name in the connection string or DSN, and set `Encrypt` explicitly. If certificate validation fails, fix the server certificate or the client's trust store. |
| Application that needs `Encrypt=Strict`, `HostnameInCertificate`, `RetryExec`, or another Driver 18 keyword | Use a Driver 18 release that supports the setting. Driver 17 doesn't accept the `Strict` value for `Encrypt` and doesn't recognize the new keywords. |
| Application that needs certificate-file pinning with `ServerCertificate` | Use Driver 18.1 or a later version with `Encrypt=Strict`. Driver 17 doesn't support this setting. |

Changing major versions usually means changing the driver name or package you select:

| Platform | Driver 17 | Driver 18 |
| --- | --- | --- |
| Connection string driver name | `ODBC Driver 17 for SQL Server` | `ODBC Driver 18 for SQL Server` |
| Linux package name | `msodbcsql17` | `msodbcsql18` |

## Summary of changes

The default and validation rows describe ordinary remote connections without a DSN `Encrypt` setting, the `SQL_COPT_SS_ENCRYPT` attribute, or the Windows **Force Protocol Encryption** override. LocalDB isn't a remote TLS target.

| Area | Version 17 | Version 18 and later versions |
| --- | --- | --- |
| Default `Encrypt` setting | `no`, unless `Authentication` is set to a value other than `none` or the application sets `SQL_COPT_SS_ACCESS_TOKEN`; then `yes` | `yes` (18.0.1 and later) |
| `Encrypt` accepted values | `yes`, `no` | `yes`, `no`, `Mandatory`, `Optional`, `Strict` |
| Encryption modes | Off or on | Off, on, or strict |
| Server certificate validation | With `Encrypt=yes;TrustServerCertificate=no`, validates the certificate. Server-required encryption alone doesn't select validation. | Validates by default. In non-Strict modes, `TrustServerCertificate=yes` skips normal validation, including when the server requires encryption. |
| Connection string keywords | Base set | Base set plus new keywords |

## Encryption changes

### Default encryption behavior

Driver 17 defaults to `Encrypt=no` unless the `Authentication` keyword is set to a value other than `none` or the application sets the `SQL_COPT_SS_ACCESS_TOKEN` attribute. Either one changes the default to `yes`. An explicit `Encrypt=no` still turns encryption off. Driver 18.0.1 and later default to `Encrypt=yes`. Set `Encrypt` explicitly when you compare versions.

Full-connection encryption protects query text and results as well as the login. With `Encrypt=no`, the server can still require full-connection encryption. When both client and server select encryption off, the non-Strict negotiation uses login-only encryption; this doesn't protect subsequent application data.

With `Encrypt=yes;TrustServerCertificate=no`, both drivers select normal server certificate validation. If only the server requires full-connection encryption and `TrustServerCertificate=no` (the default), Driver 18 validates the certificate. With `TrustServerCertificate=yes` outside Strict mode, Driver 18 skips that validation. Driver 17 doesn't validate solely because of that server requirement. Windows **Force Protocol Encryption** and internal certificate-validation callback paths aren't covered by this comparison.

With the default encryption settings, upgrading from version 17 to version 18 can expose an untrusted server certificate. If version 18 rejects the certificate, diagnose the certificate chain and name before changing authentication settings.

Keep encryption and server identity validation enabled:

- Configure a valid server certificate and the client's trust store so that the client trusts its issuing certificate authority. Connect using a name in the certificate.
- If you connect through an alias, set `HostnameInCertificate` to a valid name in the server certificate. This setting doesn't repair an untrusted certificate chain.
- For `Encrypt=Strict`, Driver 18.1 and later versions also support `ServerCertificate` to match the server certificate against a certificate file. This setting pins a certificate; it doesn't specify a host name. Review its restrictions in [ODBC DSN and connection string keywords](dsn-connection-string-attribute.md) before using it.

> [!CAUTION]  
> For an isolated test environment, `Encrypt=yes;TrustServerCertificate=yes` requests encryption but skips server identity validation, leaving the connection open to adversary-in-the-middle attacks. Don't use it as a production trust repair. In Strict mode, `TrustServerCertificate` is ignored.

If an existing application requires the version 17 default during a controlled compatibility test, see [Restore version 17 encryption behavior](#restore-version-17-encryption-behavior). `Encrypt=no` and `Encrypt=optional` don't repair certificate trust and can leave application data unencrypted.

### Encrypt values

Version 17 accepts only `yes` and `no`. Version 18 and later versions accept those values and add three more:

| Value | Equivalent to | Behavior |
| --- | --- | --- |
| `Mandatory` | `yes` | The connection is encrypted. |
| `Optional` | `no` | Doesn't request full-connection encryption. The server can still require it; login-only encryption isn't full-session protection. |
| `Strict` | None | The connection uses TDS 8.0 encryption. The server must support TDS 8.0, such as SQL Server 2022 and later versions. |

`Strict` has no version 17 equivalent. It's the only value that selects TDS 8.0, where encryption is negotiated before the login exchange rather than during it.

### Restore version 17 encryption behavior

After reviewing the trust fixes, use the following setting only for a controlled compatibility test that permits unencrypted application data:

```
Encrypt=no
```

Driver 18 also accepts `Encrypt=optional` for this setting. Neither value prevents the server from requiring encryption. Restore the intended encrypted configuration after the test; don't use disabled encryption as a certificate-trust remedy.

## Upgrade checklist

1. Inventory connection strings, DSNs, package references, and deployment scripts that select Driver 17.
1. Install Driver 18 in a test environment that matches production.
1. Change the `Driver` keyword in each connection string to `ODBC Driver 18 for SQL Server`, and update or re-create each DSN to use Driver 18. Installing Driver 18 doesn't switch existing connections, because connections that name Driver 17 keep using Driver 17.
1. Set `Encrypt` explicitly. Use `Encrypt=Mandatory` with normal certificate validation. Use `Encrypt=Strict` only when the server supports TDS 8.0, such as SQL Server 2022 and later versions. For servers that don't support TDS 8.0, use `Encrypt=Mandatory`.
1. For normal validation, connect with `TrustServerCertificate=no` and verify the trusted chain and accepted server name. For Driver 18.1 and later versions with Strict file pinning, verify the exact certificate match and validity dates instead; `TrustServerCertificate` is ignored in Strict mode.
1. Run the application's authentication, query, transaction, bulk copy, failover, and retry paths.
1. Review diagnostics for certificate validation errors before treating failures as authentication or network errors.
1. Keep Driver 17 installed and keep the inventory from step 1 until validation is complete, so you can [roll back to version 17](#roll-back-to-version-17).

## Roll back to version 17

Driver 17 and Driver 18 install side by side, so you can roll back without uninstalling Driver 18. For download and installation details, see [Download ODBC Driver for SQL Server](download-odbc-driver-for-sql-server.md).

1. Confirm that Driver 17 is still installed on each client.
1. Using the inventory from the upgrade checklist, change the `Driver` keyword in each connection string back to `ODBC Driver 17 for SQL Server`.
1. Update or re-create each DSN to use Driver 17.
1. Revert package references and deployment scripts to the Driver 17 package, such as `msodbcsql17` on Linux.
1. Restore the `Encrypt` and `TrustServerCertificate` values the application used with Driver 17. Driver 17 accepts only `yes` and `no` for `Encrypt` and doesn't recognize Driver 18 keywords such as `HostnameInCertificate` or `ServerCertificate`, so remove values and keywords added for Driver 18. Change alias keywords back to their Driver 17 names, and replace `ConnectTimeout` with a `SQLSetConnectAttr` call that sets `SQL_ATTR_LOGIN_TIMEOUT`. For the mapping, see [New connection string keywords](#new-connection-string-keywords).

Validate the rollback before treating it as complete:

- Each connection reports Driver 17. Call [SQLGetInfo](../../odbc/reference/syntax/sqlgetinfo-function.md) with `SQL_DRIVER_VER`; the major version is `17`.
- The application's authentication, query, transaction, bulk copy, failover, and retry paths succeed.
- Connection strings and DSNs contain no `Encrypt` values or keywords that were added for Driver 18.

## New connection string keywords

Version 18 adds the following connection string keywords, which version 17 doesn't recognize. Some of them are new names for settings that version 17 supports under a different keyword or attribute, listed in the **Version 17 equivalent** column.

| Keyword | Minimum version | Purpose | Version 17 equivalent |
| --- | --- | --- | --- |
| `ConcatNullYieldsNull` | 18.6 | Controls whether concatenating a null value yields null. | None |
| `ConnectTimeout` | 18.7 | Sets the login timeout in seconds. If the application also sets `SQL_ATTR_LOGIN_TIMEOUT`, the attribute takes precedence. | `SQL_ATTR_LOGIN_TIMEOUT` attribute, set with `SQLSetConnectAttr` |
| `FailoverPartner` | 18.7 | Specifies the failover partner for database mirroring. Windows only. | `Failover_Partner` keyword |
| `GetDataExtensions` | 18.0 | Controls which `SQLGetData` extensions the driver enables. | None |
| `HostnameInCertificate` | 18.0 | Specifies the host name to validate in the server's TLS certificate. | None |
| `IpAddressPreference` | 18.1 | Specifies the IP address family the driver tries first. | None |
| `LongAsMax` | 18.0 | Maps long data types to their `max` equivalents. | None |
| `MultipleActiveResultSets` | 18.7 | Enables Multiple Active Result Sets (MARS). Accepts `Yes` or `No`. | `MARS_Connection` keyword |
| `RetryExec` | 18.1 | Configures retry rules for failed queries. | None |
| `ServerCertificate` | 18.1 | Specifies the certificate file to match against the server certificate when using `Encrypt=Strict`. | None |
| `WorkstationID` | 18.7 | Specifies the workstation name sent to the server. | `WSID` keyword |

`MultipleActiveResultSets`, `WorkstationID`, and `FailoverPartner` are aliases that align ODBC keyword names with `Microsoft.Data.SqlClient` property names. Version 18 also accepts the older names. For details, see [Connection string keyword unification](dsn-connection-string-attribute.md#connection-string-keyword-unification) and [ConnectTimeout keyword](dsn-connection-string-attribute.md#connecttimeout-keyword).

For the full keyword reference, see [ODBC DSN and connection string keywords](dsn-connection-string-attribute.md).

## Related content

- [ODBC DSN and connection string keywords](dsn-connection-string-attribute.md)
- [Microsoft ODBC Driver for SQL Server](microsoft-odbc-driver-for-sql-server.md)
- [Support lifecycle](support-lifecycle.md)
- [Release notes for ODBC Driver for SQL Server on Windows](windows/release-notes-odbc-sql-server-windows.md)
- [Release notes for ODBC Driver for SQL Server on Linux and macOS](linux-mac/release-notes-odbc-sql-server-linux-mac.md)
