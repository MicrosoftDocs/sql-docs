---
title: What's New in Microsoft.Data.SqlClient
description: Find Microsoft.Data.SqlClient release notes, current stable and preview releases, major upgrade changes, and optional package releases.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, paulmedynski, cmalhotra
ms.date: 09/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: whats-new
ai-usage: ai-assisted
---

# What's new in Microsoft.Data.SqlClient

Microsoft.Data.SqlClient ships independently of .NET. This article summarizes the significant features, behavior changes, and compatibility changes in each release line. The release tables include every stable package publication, including each hotfix and unlisted package. For every fix and dependency update, see the [complete release notes in the SqlClient repository](https://github.com/dotnet/SqlClient/tree/main/release-notes).

## Current releases

| Release line | Latest version | Release date | Support |
| --- | --- | --- | --- |
| 7.0 | [7.0.3](https://www.nuget.org/packages/Microsoft.Data.SqlClient/7.0.3) | September 2026 | Standard Term Support (STS) |
| 6.1 | [6.1.7](https://www.nuget.org/packages/Microsoft.Data.SqlClient/6.1.7) | September 2026 | Long Term Support (LTS) |
| 7.1 preview | [7.1.0-preview3.26238.4](https://www.nuget.org/packages/Microsoft.Data.SqlClient/7.1.0-preview3.26238.4) | August 2026 | Preview |

Don't use preview releases in production. Preview APIs and behavior can change before General Availability (GA).

Release tables and general availability summaries use the NuGet publication month. The lifecycle article lists official release dates, which can differ. For support windows, see [SqlClient driver support lifecycle](sqlclient-driver-support-lifecycle.md).

## 7.1 preview

The 7.1 preview line extends batching, schema discovery, connection pooling, and Always Encrypted APIs. Changes through 7.1.0-preview3 include:

- `SqlBatch` support on .NET Framework.
- Asynchronous `SqlConnection.GetSchemaAsync` overloads.
- Connection string aliases such as `ColumnEncryption`, `ConnectTimeout`, `FailoverPartner`, `PacketSize`, and `WorkstationId`.
- SQL Graph pseudo-column mappings for `$node_id`, `$edge_id`, `$from_id`, and `$to_id` in `SqlBulkCopy`.
- Opt-in connection idle timeout enforcement.
- An opt-in connection timeout mode that includes time spent waiting for a pooled connection.
- Further development of the experimental channel-based connection pool.
- Asynchronous methods on Always Encrypted key store providers. The driver doesn't yet call these methods during command execution, so they don't make existing Always Encrypted operations asynchronous.

`SqlBatchCommand.CommandBehavior` is honored starting in this line. Earlier versions ignored the property. The obsolete `Type System Version=SQL Server 2000` connection option now throws an `ArgumentException`.

Preview3 also changes certificate and enclave validation. `ServerCertificate` always compares the configured certificate file with the certificate presented by the server. An invalid or unreadable file now fails the connection instead of falling back to host-name validation. Virtual Secure Mode (VSM) and Host Guardian Service (HGS) enclave attestation now verifies that the enclave public key is bound to the signed attestation report. Retest these paths when you move from an earlier preview.

Read the release notes for [preview1](https://github.com/dotnet/SqlClient/blob/main/release-notes/7.1/7.1.0-preview1.md), [preview2](https://github.com/dotnet/SqlClient/blob/main/release-notes/7.1/7.1.0-preview2.md), and [preview3](https://github.com/dotnet/SqlClient/blob/main/release-notes/7.1/7.1.0-preview3.md) before testing this line.

## 7.0 STS

Version 7.0 reached general availability in March 2026. It's the current STS line.

### Features and changes

- Driver-provided Microsoft Entra authentication moved from the core package to `Microsoft.Data.SqlClient.Extensions.Azure`. Add the extension package when you use an `Active Directory` authentication mode.
- The core package no longer depends on `Azure.Core`, `Azure.Identity`, or Microsoft Authentication Library (MSAL).
- `SqlConnection.SspiContextProvider` supports custom Kerberos and NTLM token negotiation. The provider identity is part of the connection pool key.
- Enhanced routing for Azure SQL Hyperscale and named read replicas is negotiated automatically.
- Strongly typed command, connection, and transaction diagnostic events are available on .NET Framework and current .NET.
- `SqlConfigurableRetryFactory.BaselineTransientErrors` exposes the driver's default transient error list.
- `Active Directory Password` authentication is obsolete. Use interactive authentication for user applications, service principal authentication for services, or managed identity for Azure-hosted workloads.
- Target support starts at .NET Framework 4.6.2 and .NET 8. The driver also supports building and running on .NET 10.

Packet multiplexing for asynchronous reads remains preview functionality and is opt-in in 7.0. Test compatibility switches with production workloads and keep a rollback path.

### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [7.0.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/7.0/7.0.0.md) | March 2026 | General availability release. |
| [7.0.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/7.0/7.0.1.md) | April 2026 | Fixed `SqlBulkCopy` metadata queries for SQL Server 2016 and Azure Synapse Analytics dedicated SQL pools, corrected vector field metadata, added the missing .NET Framework `System.Data.Common` dependency, and added type forwarding for authentication abstractions. |
| [7.0.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/7.0/7.0.2.md) | June 2026 | Aligned the core driver and companion packages, hardened TDS token parsing, corrected Always Encrypted signature-verification cache handling, and fixed errors in cancellation and null-buffer `GetBytes` and `GetChars` calls. |
| [7.0.3](https://github.com/dotnet/SqlClient/blob/main/release-notes/7.0/7.0.3.md) | September 2026 | Updated SNI to 6.0.3. Fixed a `SqlBulkCopy` regression for logins that can't read `sys.all_columns`, a memory allocation regression when tracing is disabled, `ServerCertificate` validation on managed SNI, Always Encrypted enclave attestation key verification, and configurable retry registering a process-wide assembly resolution handler. |

> [!IMPORTANT]
> Starting with 7.0.2, align direct references to `Microsoft.Data.SqlClient`, `Microsoft.Data.SqlClient.Extensions.Azure`, `Microsoft.Data.SqlClient.Extensions.Abstractions`, and `Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider`. Let `Microsoft.Data.SqlClient.Internal.Logging` resolve transitively. On .NET Framework, the assembly versions of `Extensions.Azure`, `Extensions.Abstractions`, and `Internal.Logging` changed from `1.0.0.0` to `7.0.0.0`. Rebuild the application or add binding redirects when you update these packages.

## 6.1 LTS

Version 6.1.0 was released in July 2025, but was later unlisted because of critical regressions. Version 6.1.1, released in August 2025, established the supported 6.1 LTS line.

### Features and changes

- Native SQL Server 2025 vector transport through `SqlVector<T>`, initially for single-precision floating-point vectors.
- A .NET Standard 2.0 reference asset for library compatibility. This asset isn't a runtime implementation. Executable applications must use a supported .NET or .NET Framework runtime asset.
- Opt-in packet multiplexing work for large asynchronous reads and endpoint handling for SQL database in Microsoft Fabric.
- Improved native Ahead-of-Time (AOT) compilation support.
- Reduced allocations in connection and TDS processing paths.
- Internal groundwork for the public Security Support Provider Interface (SSPI) extensibility added in 7.0.

Don't deploy version 6.1.0. Version 6.1.1 reverted the affected packet detection and replay changes.

### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [6.1.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.1/6.1.0.md) | July 2025 | General availability release. This package was unlisted because of critical regressions. Don't use it. |
| [6.1.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.1/6.1.1.md) | August 2025 | Reverted partial-packet detection, fixup, and replay changes from 6.1.0. Corrected vector reference assemblies and `SqlVector<T>.Null`. |
| [6.1.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.1/6.1.2.md) | October 2025 | Fixed early performance-counter initialization, replacement of custom authentication providers, and pool concurrency that prevented use of the configured maximum pool size. |
| [6.1.3](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.1/6.1.3.md) | November 2025 | Added `IgnoreServerProvidedFailoverPartner` for Basic Availability Groups and custom ports. Fixed early EventSource metrics initialization. |
| [6.1.4](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.1/6.1.4.md) | January 2026 | Added `EnableMultiSubnetFailoverByDefault`. Fixed `SqlDataAdapter` batching failures and negative active-connection counts in pool metrics. |
| [6.1.5](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.1/6.1.5.md) | April 2026 | Removed unnecessary Service Principal Name (SPN) and Domain Name System (DNS) work for non-integrated authentication, made `ExecuteScalar` propagate server errors returned after data, and corrected vector metadata types. |
| [6.1.6](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.1/6.1.6.md) | June 2026 | Added opt-in Windows Web Account Manager (WAM) broker support, hardened TDS token parsing, corrected cached column master key signature failures, and fixed `GetChars` argument validation. |
| [6.1.7](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.1/6.1.7.md) | September 2026 | Updated SNI to 6.0.3. Fixed `ServerCertificate` validation on managed SNI, Always Encrypted enclave attestation key verification, `AccessTokenCallback` connection retry behavior, and configurable retry registering a process-wide assembly resolution handler. |

## Previous release lines

The following release lines are no longer supported. Use their summaries when you plan an upgrade across multiple driver versions.

### 6.0

Version 6.0 reached general availability in January 2025. The final patch was 6.0.5, released in January 2026.

- Added native SQL Server JSON support through `Microsoft.Data.SqlTypes.SqlJson`, including read, write, streaming, and bulk-copy paths.
- Added public, strongly typed diagnostic payloads for command, connection, and transaction events.
- Added `OpenAsync(SqlConnectionOverrides, CancellationToken)`, including `OpenWithoutRetry`.
- Added `DateOnly` and `TimeOnly` support in `DataTable` and `SqlDataRecord` structured parameters.
- Added .NET 9 support.
- Dropped .NET Standard and .NET 6 runtime assets. Supported targets became .NET Framework 4.6.2 and later versions and .NET 8 and later versions.
- Restricted `EnableOptimizedParameterBinding` to text commands.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [6.0.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.0/6.0.0.md) | January 2025 | General availability release. This package is unlisted on NuGet, and the upstream notes don't document a reason. |
| [6.0.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.0/6.0.1.md) | January 2025 | Corrected `SqlClientDiagnostic` reference APIs, down-level TLS warnings, and dependencies. |
| [6.0.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.0/6.0.2.md) | April 2025 | Fixed possible socket-receive null failures, `SqlJson` reference APIs, JSON output parameters, and dependencies. |
| [6.0.3](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.0/6.0.3.md) | October 2025 | Prevented replacement of custom authentication providers and fixed pool concurrency that underused `Max Pool Size`. |
| [6.0.4](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.0/6.0.4.md) | November 2025 | Added `Switch.Microsoft.Data.SqlClient.IgnoreServerProvidedFailoverPartner` for Basic Availability Groups that use custom TCP ports. |
| [6.0.5](https://github.com/dotnet/SqlClient/blob/main/release-notes/6.0/6.0.5.md) | January 2026 | Fixed `SqlDataAdapter` batch RPC null handling, added `Switch.Microsoft.Data.SqlClient.EnableMultiSubnetFailoverByDefault`, and updated dependencies. |

### 5.2

Version 5.2 reached general availability in February 2024. The final patch was 5.2.3, released in April 2025.

- Added `SqlConnection.AccessTokenCallback` for asynchronous, pool-compatible token acquisition.
- Added `SqlBatch` and `SqlBatchCommand` on .NET 6 and later versions.
- Added `SqlDiagnosticListener` support on .NET Standard.
- Added 64-bit `SqlBulkCopy.RowsCopied64`. The older `RowsCopied` property remains a 32-bit integer.
- Added `Active Directory Workload Identity` authentication.
- Added .NET 8 and big-endian platform support.
- Changed `UseOneSecFloorInTimeoutCalculationDuringLogin` to default to `true`.
- Used `NegotiateAuthentication` for managed SSPI on .NET 7 and later versions.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [5.2.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.2/5.2.0.md) | February 2024 | General availability release. |
| [5.2.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.2/5.2.1.md) | May 2024 | Fixed Linux named-instance connections with ports, `FireInfoMessageEventOnUserErrors`, **datetimeoffset(n)** table-valued parameters, delayed `OpenAsync`, and `AccessTokenCallback` preservation by `SqlConnection.Clone()`. |
| [5.2.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.2/5.2.2.md) | August 2024 | Fixed token timeouts, managed SNI socket connections, assembly targeting, SSPI retries, user-defined type reads, and encrypted-reader data. Updated identity packages for CVE-2024-35255. |
| [5.2.3](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.2/5.2.3.md) | April 2025 | Fixed possible socket-receive null failures and reference-assembly inconsistencies, allowed negative transient error numbers, and updated dependencies. |

### 5.1

Version 5.1 reached general availability in January 2023. The final patch was 5.1.9, released in January 2026.

- Added .NET 6 support and dropped .NET Core 3.1.
- Added `DateOnly` and `TimeOnly` support for parameter values and `GetFieldValue`.
- Added TLS 1.3 in .NET and native SQL Server Network Interface (SNI) paths.
- Added `ServerCertificate` for exact certificate matching with `Encrypt=Mandatory` and `Encrypt=Strict`.
- Added Windows ARM64 support for .NET Framework.
- Made the `SqlConnectionEncryptOption` string parser public.
- Version 5.1.3 fixed the CVE-2024-0056 encryption downgrade path and certificate-chain validation.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [5.1.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.0.md) | January 2023 | General availability release. |
| [5.1.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.1.md) | March 2023 | Fixed Always Encrypted error reporting, redirect-mode transactions, token throttling, large-query TDS RPC failures, and `GetBytesAsync` null handling. |
| [5.1.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.2.md) | October 2023 | Fixed SQL Express user instances, enclave retries, LocalDB, connection-string builder values, `SqlConnectionEncryptOption` configuration conversion, and `OpenAsync` transient faults. |
| [5.1.3](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.3.md) | January 2024 | Fixed CVE-2024-0056 and certificate-chain validation. |
| [5.1.4](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.4.md) | January 2024 | Fixed a .NET distributed-transaction deadlock and upgraded `Azure.Identity` for CVE-2023-36414. |
| [5.1.5](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.5.md) | January 2024 | Fixed transaction cleanup before pooling and Always Encrypted date and time conversion failures. Updated IdentityModel dependencies for CVE-2024-21319. |
| [5.1.6](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.6.md) | August 2024 | Fixed `OpenAsync`, authentication timeout, and encrypted-reader pending-data failures. Updated identity dependencies for CVE-2024-35255 and cached `TokenCredential` instances. |
| [5.1.7](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.7.md) | April 2025 | Fixed a possible socket-receive null failure, aligned source and reference assemblies, and updated dependencies. |
| [5.1.8](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.8.md) | November 2025 | Added `Switch.Microsoft.Data.SqlClient.IgnoreServerProvidedFailoverPartner`, fixed UTF-8 byte order mark handling in bulk copy, modernized MSAL creation, and updated dependencies. |
| [5.1.9](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.1/5.1.9.md) | January 2026 | Moved `SqlStatistics` timing to `Environment.TickCount` and updated framework-specific dependencies. |

### 5.0

Version 5.0 reached general availability in August 2022. The final patch was 5.0.2, released in March 2023.

- Added TDS 8.0 through `Encrypt=Strict`. Strict mode starts TLS before the TDS login exchange and always validates the server certificate.
- Changed `SqlConnectionStringBuilder.Encrypt` from `bool` to `SqlConnectionEncryptOption`.
- Moved SQL Server common language runtime (CLR) types to the separately versioned `Microsoft.SqlServer.Server` package.
- Added `Server SPN` and `Failover Server SPN` connection options.
- Added Windows SQL Server alias support on current .NET.
- Added `SqlDataSourceEnumerator` on Windows.
- Dropped .NET Framework 4.6.1.

> [!IMPORTANT]
> The `SqlConnectionStringBuilder.Encrypt` type change is binary breaking. Types removed from `Microsoft.Data.SqlClient.Server` also require source changes to use `Microsoft.SqlServer.Server`. Recompile applications and libraries, update namespaces, and check for namespace conflicts when `System.Data.SqlClient` remains referenced.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [5.0.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.0/5.0.0.md) | August 2022 | General availability release. |
| [5.0.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.0/5.0.1.md) | October 2022 | Restored `HostNameInCertificate` on .NET Framework and fixed attention deadlocks, `SqlConnectionStringBuilder.Encrypt`, cancellation registration, managed SNI hangs, UTF-8 collation, and an SNI application-domain crash. |
| [5.0.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/5.0/5.0.2.md) | March 2023 | Fixed an asynchronous-read memory leak, redirect-mode `TransactionScope`, Always Encrypted errors, large-query TDS RPC failures, and distributed-transaction deadlocks. |

### 4.1

Version 4.1 reached general availability in January 2022. The final patch was 4.1.1, released in September 2022.

- Added `Attestation Protocol=None` for Virtualization-based Security (VBS) enclaves.
- Parallelized SQL Server Resolution Protocol requests on Linux and macOS when `MultiSubnetFailover` is enabled.
- Fixed certificate revocation list checks during authentication.
- Fixed Microsoft Entra authentication, null `SqlBinary` and **rowversion** values, and UTF-8 collation behavior.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [4.1.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.1/4.1.0.md) | January 2022 | General availability release. |
| [4.1.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.1/4.1.1.md) | September 2022 | Fixed certificate revocation list failures, parallelized SQL Server Resolution Protocol requests for `MultiSubnetFailover`, and corrected Microsoft Entra authentication, **rowversion**, UTF-8, and SNI application-domain failures. |

### 4.0

Version 4.0 reached general availability in November 2021. The final patch was 4.0.6, released in August 2024.

- Changed the default value of `Encrypt` from `false` to `true`.
- Added `SqlCommand.EnableOptimizedParameterBinding` for commands with many parameters.
- Added `SqlFileStream` for Windows through the .NET Standard implementation.
- Added shared LocalDB instance support in managed SNI.
- Added `GetFieldValue<T>` and `GetFieldValueAsync<T>` for `XmlReader`, `TextReader`, and `Stream`.
- Removed the safety switch from configurable retry logic. Retry remained disabled until an application configured a provider.
- Changed Microsoft Entra authentication failures to throw `SqlException` instead of `AggregateException`.
- Dropped .NET Core 2.1.
- Version 4.0.5 fixed the CVE-2024-0056 encryption downgrade path and certificate-chain validation.

> [!IMPORTANT]
> Because encryption defaults to `true` in 4.0 and later versions, an upgrade can expose certificate trust or server-name mismatches. Don't disable encryption to work around these errors. Install a trusted server certificate or configure certificate validation for the deployment.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [4.0.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.0/4.0.0.md) | November 2021 | General availability release. |
| [4.0.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.0/4.0.1.md) | January 2022 | Added `SuppressInsecureTLSWarning` and fixed .NET 6 Kerberos, LocalDB pipe names, and concurrent enclave queries. |
| [4.0.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.0/4.0.2.md) | September 2022 | Fixed certificate revocation list failures, parallelized SQL Server Resolution Protocol requests for `MultiSubnetFailover`, and corrected Microsoft Entra authentication, **rowversion**, UTF-8, and SNI application-domain failures. |
| [4.0.3](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.0/4.0.3.md) | April 2023 | Reduced cached-account token throttling and fixed large-query TDS RPC failures. |
| [4.0.4](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.0/4.0.4.md) | October 2023 | Fixed asynchronous enclave retries, LocalDB and managed SNI fallback, file versions, and connection activity correlation. |
| [4.0.5](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.0/4.0.5.md) | January 2024 | Fixed CVE-2024-0056 and certificate-chain validation. |
| [4.0.6](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.0/4.0.6.md) | August 2024 | Fixed transaction-event cleanup before pooling and `ActiveDirectoryAuthenticationProvider.AcquireTokenAsync` timeout handling. |

### 3.1

Version 3.1 reached general availability in March 2022. The final patch was 3.1.7, released in August 2024.

- Added `Attestation Protocol=None` for VBS enclaves.
- Added SQL errors 42108 and 42109 to the default transient error list.
- Added Windows ARM64 support for .NET Framework in 3.1.2.
- Reduced repeated Microsoft Entra token requests through silent token acquisition in 3.1.3.
- Fixed the CVE-2024-0056 encryption downgrade path in 3.1.5.
- Fixed transaction cleanup before pooling and Microsoft Entra token timeout handling in 3.1.7.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [3.1.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/3.1/3.1.0.md) | March 2022 | General availability release. |
| [3.1.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/3.1/3.1.1.md) | August 2022 | Fixed null `SqlBinary` **rowversion** values, .NET 6 Kerberos failures, and Microsoft Entra authentication null-reference failures. |
| [3.1.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/3.1/3.1.2.md) | February 2023 | Added .NET Framework Windows ARM64 support and fixed retry-list thread safety, distributed-transaction deadlocks, UTF-8 collation, and stored-procedure validation. |
| [3.1.3](https://github.com/dotnet/SqlClient/blob/main/release-notes/3.1/3.1.3.md) | March 2023 | Reduced Microsoft Entra token throttling with `AcquireTokenSilent` and fixed large-query TDS RPC failures. |
| [3.1.4](https://github.com/dotnet/SqlClient/blob/main/release-notes/3.1/3.1.4.md) | October 2023 | Fixed asynchronous enclave retries, LocalDB and managed SNI fallback, file versions, connection activity correlation, and EventSource formatting. |
| [3.1.5](https://github.com/dotnet/SqlClient/blob/main/release-notes/3.1/3.1.5.md) | January 2024 | Fixed CVE-2024-0056 and certificate-chain validation. |
| [3.1.6](https://www.nuget.org/packages/Microsoft.Data.SqlClient/3.1.6) | August 2024 | This package wasn't properly signed and was unlisted. No upstream release-note file exists. Use 3.1.7 instead. |
| [3.1.7](https://github.com/dotnet/SqlClient/blob/main/release-notes/3.1/3.1.7.md) | August 2024 | Fixed transaction-event cleanup before pooling, authentication timeouts, assembly signing, and an SNI application-domain crash. |

### 3.0

Version 3.0 reached general availability in June 2021. The final patch was 3.0.1, released in September 2021.

- Added configurable retry logic for connection and command operations. In 3.0, it remained opt-in behind `Switch.Microsoft.Data.SqlClient.EnableRetryLogic`.
- Added EventCounters for connection and pool activity.
- Added `Active Directory Default` authentication through `Azure.Identity`.
- Added connection-scoped and command-scoped Always Encrypted key store provider registration.
- Added TCP IP address preference.
- Changed user-assigned managed identity authentication to use the client ID instead of the object ID in `User Id`.
- Changed SQL **rowversion** null values to return `DBNull` instead of an empty byte array. An AppContext switch preserved the older behavior.
- Raised the minimum .NET Framework target from 4.6 to 4.6.1.

System key store providers take precedence over providers registered on a connection or command starting in 3.0. Rename a custom provider if its name conflicts with a built-in provider.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [3.0.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/3.0/3.0.0.md) | June 2021 | General availability release. |
| [3.0.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/3.0/3.0.1.md) | September 2021 | Fixed blocking Microsoft Entra connection opens, transaction promotion state, required encryption, retry recursion and deadlocks, and unusable connections. |

### 2.1

Version 2.1 reached general availability in November 2020. The final patch was 2.1.7, released in January 2024.

- Extended Always Encrypted to supported .NET Standard 2.0 platforms.
- Added secure enclave support on Unix for .NET Core and across .NET Standard 2.1 platforms.
- Added `Active Directory Device Code Flow` and managed identity authentication.
- Added customization hooks for interactive and device code authentication.
- Added sensitivity classification rank metadata.
- Added `SqlConnection.ServerProcessId`.
- Added the `Command Timeout` connection option as the default for commands created by a connection.
- Version 2.1.7 fixed the CVE-2024-0056 encryption downgrade path.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [2.1.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.1/2.1.0.md) | November 2020 | General availability release. |
| [2.1.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.1/2.1.1.md) | December 2020 | Fixed system-assigned managed identity in Azure Functions, Unix Kerberos, and TCP keep-alive. |
| [2.1.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.1/2.1.2.md) | March 2021 | Fixed Unix named instances, timeout wrong results, unsafe DTD processing, Kerberos SPNs, missing caching dependencies, tracing, and MARS headers. |
| [2.1.3](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.1/2.1.3.md) | May 2021 | Prevented transaction data corruption with open result sets and fixed a `SinglePhaseCommit` and `TransactionEnded` race. |
| [2.1.4](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.1/2.1.4.md) | September 2021 | Ensured connections fail when encryption is required and fixed connections entering an unusable state. |
| [2.1.5](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.1/2.1.5.md) | August 2022 | Added stored-procedure `CommandText` length validation and fixed .NET 6 Kerberos authentication and `SqlTypeWorkarounds`. |
| [2.1.6](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.1/2.1.6.md) | April 2023 | Fixed large-query TDS RPC failures in `SqlCommand.ExecuteReaderAsync`, default UTF-8 collation conflicts, and attention-send deadlocks. |
| [2.1.7](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.1/2.1.7.md) | January 2024 | Fixed CVE-2024-0056 and certificate-chain validation. |

### 2.0

Version 2.0 reached general availability in June 2020. The final patch was 2.0.1, released in August 2020.

- Added `Active Directory Integrated` and `Active Directory Interactive` authentication on .NET Core and .NET Standard.
- Added `Active Directory Service Principal` authentication.
- Added `Microsoft.Data.SqlClient.EventSource` tracing.
- Added cross-platform TCP keep-alive and an opt-in managed networking implementation on Windows for testing and debugging. The managed path didn't support non-domain Windows authentication.
- Added `SqlBulkCopy.RowsCopied` and ordered bulk-copy hints.
- Added `SqlConnection.Open(SqlConnectionOverrides.OpenWithoutRetry)`.
- Added Windows ARM support through the `Microsoft.Data.SqlClient.SNI.runtime` dependency.
- Began validating the server certificate when a server forces encryption, including when the connection string sets `Encrypt=false`. Connections can fail after this upgrade if the certificate isn't trusted or its name doesn't match the server.
- Changed decimal parameter scale handling to use SQL Server-compatible rounding.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [2.0.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.0/2.0.0.md) | June 2020 | General availability release. |
| [2.0.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/2.0/2.0.1.md) | August 2020 | Added coexistence-friendly authentication configuration and fixed pooled token expiry, transient-fault handling, enclave caching, and native SNI prelogin errors. |

### 1.1

Version 1.1 reached general availability in November 2019. The final patch was 1.1.4, released in March 2021.

- Added Always Encrypted with secure enclaves on supported .NET Framework and .NET Core targets.
- Added `|DataDirectory|` expansion in `AttachDBFilename` on .NET Core.
- Improved asynchronous reads with multipacket target-buffer caching.
- Reduced snapshot overhead in `TdsParserStateObject` and `SqlDataReader`.
- Fixed timeout-state and Multiple Active Result Sets (MARS) header errors in later patches.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [1.1.0](https://github.com/dotnet/SqlClient/blob/main/release-notes/1.1/1.1.0.md) | November 2019 | General availability release. |
| [1.1.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/1.1/1.1.1.md) | February 2020 | Reverted asynchronous `SNIPacket` changes that caused deadlocks and corrected the SNI dependency range. |
| [1.1.2](https://github.com/dotnet/SqlClient/blob/main/release-notes/1.1/1.1.2.md) | April 2020 | Added usernames for Active Directory Interactive authentication and fixed password persistence and MARS TDS headers. |
| [1.1.3](https://github.com/dotnet/SqlClient/blob/main/release-notes/1.1/1.1.3.md) | May 2020 | Prevented aborted transactions from enlisting pooled connections and reverted the regressive MARS header fix. |
| [1.1.4](https://github.com/dotnet/SqlClient/blob/main/release-notes/1.1/1.1.4.md) | March 2021 | Corrected timeout-state wrong results and MARS header errors on .NET Framework 4.8 and later versions. |

### 1.0

Microsoft.Data.SqlClient 1.0 reached general availability in August 2019. The final patch was 1.0.19269.1, released in September 2019.

- Introduced the separately shipped `Microsoft.Data.SqlClient` provider.
- Supported .NET Framework 4.6 and later versions, .NET Core 2.1 and later versions, and .NET Standard 2.0.
- Added `Active Directory Interactive` authentication on .NET Framework.
- Added `Active Directory Password` authentication on .NET Core.
- Added data sensitivity classification metadata on `SqlDataReader`.
- Added SQL Server 2019 UTF-8 collation support.
- Added Always Encrypted support on .NET Core.

#### Releases

| Version | Released | Customer-relevant changes |
| --- | --- | --- |
| [1.0.19239.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/1.0/1.0.19239.1.md) | August 2019 | General availability release. Fixed connection reuse after transaction failures, SNI publishing, globalization-invariant handling, and bulk-copy null widening. |
| [1.0.19249.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/1.0/1.0.19249.1.md) | September 2019 | Fixed large multipacket data reads on Unix. |
| [1.0.19269.1](https://github.com/dotnet/SqlClient/blob/main/release-notes/1.0/1.0.19269.1.md) | September 2019 | Restored `SqlCommand.StatementCompleted`, added the missing `SqlConnectionStringBuilder.Authentication`, and reverted an incompatible `SqlAuthenticationParameters.Resource` API change. |

For an application moving from the older provider, use [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](migrate-system-data-sql-client-to-microsoft-data-sql-client.md) instead of reading release notes alone.

## Optional package release notes

Optional packages have their own release notes. Starting with version 7.0.2, they ship version-aligned with the core driver.

| Package | Release notes |
| --- | --- |
| `Microsoft.Data.SqlClient.Extensions.Azure` | [Azure extension release notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/Extensions/Azure) |
| `Microsoft.Data.SqlClient.Extensions.Abstractions` | [Extension abstractions release notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/Extensions/Abstractions) |
| `Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider` | [Azure Key Vault Provider release notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/add-ons/AzureKeyVaultProvider) |

Don't reference `Microsoft.Data.SqlClient.Internal.Logging` directly. It's an internal dependency shared by driver packages.

## Evaluate an update

1. Identify every driver and optional package version used directly or transitively.
1. Read the release notes for every minor and major version crossed by the update.
1. Check [SqlClient driver support lifecycle](sqlclient-driver-support-lifecycle.md) for target framework and database compatibility.
1. Update all related packages to compatible versions.
1. Build and test the application's authentication, encryption, connection pooling, parameters, transactions, retries, diagnostics, and deployment output.

For installation and deployment commands, see [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md).

## Related content

- [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md)
- [SqlClient driver support lifecycle](sqlclient-driver-support-lifecycle.md)
- [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](migrate-system-data-sql-client-to-microsoft-data-sql-client.md)
- [Microsoft.Data.SqlClient release notes](https://github.com/dotnet/SqlClient/tree/main/release-notes)
