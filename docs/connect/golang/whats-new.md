---
title: "What's new in go-mssqldb"
description: "Version history and release highlights for the go-mssqldb driver across the Microsoft SQL platform."
author: dlevy-msft
ms.author: dlevy
ms.date: 06/23/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# What's new in go-mssqldb

This article lists significant changes in each release of the `go-mssqldb` driver. The `microsoft/go-mssqldb` repository is a fork of `denisenkom/go-mssqldb`. This article lists only releases published under the Microsoft fork, starting with v0.16.0. For a full changelog, see the [GitHub releases](https://github.com/microsoft/go-mssqldb/releases) page.

> [!NOTE]
> Some version numbers aren't sequential. For example, versions v1.2.0 through v1.4.0 were never published. The version jumps reflect the actual release history of the Microsoft fork.

## v1.10.0 (April 2026)

- Added `FailoverPartnerSPN` connection string parameter for database mirroring failover.
- Added `NewConnectorWithProcessQueryText` function for compatibility with the `mssql` driver name.
- Added nullable `civil` types (`NullDate`, `NullTime`, `NullDateTime`) for date and time parameters.
- Implemented the `driver.DriverContext` interface for `database/sql` compatibility.
- Detected server-aborted transactions to prevent silent auto-commit when `XACT_ABORT` is enabled.
- Surfaced server errors from `Rows.Close()` during token drain.
- Handled `COLINFO` and `TABNAME` TDS tokens returned by tables with triggers.
- Returned `interface{}` scan type for `sql_variant` columns instead of `nil`.
- Made `readCancelConfirmation` respect context cancellation.
- Exposed `TrustServerCertificate` in `msdsn.Config` and URL round-trip.
- Sanitized credentials from connection string parsing errors.
- Allowed named pipe protocol support for ARM64 Windows.

## v1.9.8 (March 2026)

- Updated dependencies to resolve `govulncheck` findings.

## v1.9.7 (February 2026)

- Added Extended Protection for Authentication (EPA) support.
- Improved performance by using pointer receivers for `columnStruct`, reducing memory copies.
- Fixed bulk copy column escaping.
- Fixed `stateSep` handling in the batch package.

## v1.9.6 (January 2026)

- Added `serverCertificate` connection parameter for byte-comparison TLS certificate validation.
- Updated `golang.org/x/crypto` dependency.

## v1.9.4 (November 2025)

- Fixed attention packet handling during transaction rollback.
- Restricted named pipe and shared memory protocols to supported platforms.

## v1.9.3 (September 2025)

- Fixed bulk copy `FMTONLY` reset on failure.
- Fixed TLS handshake packet flush during connection setup.
- Added support for double-quoted values in ADO-style connection strings.

## v1.8.2 (May 2025)

- Recognized `Pwd` as a password field alias in connection strings.
- Added `COMMIT` and `ROLLBACK` as recognized builtin commands.

## v1.8.0 (December 2024)

- Added user-defined type (UDT) support for `hierarchyid`, `geometry`, and `geography`.
- Added tracing data to prelogin and login7 packets.
- Fixed connection not closed when database name is incorrect.
- Bumped `azidentity` to v1.6.0.

## v1.7.2 (May 2024)

- Fixed nullable type support for Always Encrypted in bulk copy.
- Fixed Entra authentication scope handling.

## v1.7.1 (April 2024)

- Implemented Always Encrypted with the `columnencryption` connection parameter.
- Added Azure Key Vault (`akv`) column encryption key provider.
- Added `ActiveDirectoryDeviceCode` and `ActiveDirectoryAzCli` fedauth types.
- Accepted additional values for the `encrypt` parameter in TDS 8.0 mode.
- Added DER certificate support.
- Added `multisubnetfailover` connection parameter.
- Allowed Kerberos (krb5) configuration through environment variables.
- Implemented change password during login.
- Fixed LCID-to-code-page mappings.
- Fixed string output parameter truncation.
- Fixed `UniqueIdentifier` NULL value handling in `Scan()`.
- Fixed named pipe path handling.

## v1.7.0 (February 2024)

- Added `NullUniqueIdentifier` type with JSON and text marshaling support.

## v1.6.0 (August 2023)

- Added TDS 8.0 support with `encrypt=strict`.

## v1.5.0 (August 2023)

- Vendored the `npipe` package for named pipe connections.
- Added DAC (Dedicated Administrator Connection) port browser support.
- Added `UniqueIdentifier` `MarshalText` and `UnmarshalJSON` methods.
- Enabled local DNS resolution with custom dialers.
- Fixed `mips` and `mipsel` builds.

## v1.1.0 (June 2023)

- Added DAC port browser service.

## v0.16.0 (August 2022) - First Microsoft fork release

- Migrated to `azidentity` v1.0 for Microsoft Entra authentication.
- Added `tlsmin` connection parameter to enforce a minimum TLS version.

## Related content

- [Installation and system requirements](installation.md)
- [Support and lifecycle](support-lifecycle.md)
- [go-mssqldb releases on GitHub](https://github.com/microsoft/go-mssqldb/releases)
