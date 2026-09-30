---
title: Troubleshoot the Microsoft OLE DB Driver for SQL Server
description: Diagnose provider registration, authentication, certificate, network, parameter, conversion, and timeout failures in the Microsoft OLE DB Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: 09/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: troubleshooting
ai-usage: ai-generated
---

# Troubleshoot the Microsoft OLE DB Driver for SQL Server

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

Use this article to identify the failing stage of an OLE DB operation, choose the next check, and find detailed troubleshooting instructions. The guidance uses the current provider, `MSOLEDBSQL19`. For release-specific defects and upgrade changes, see [Known issues](oledb-driver-for-sql-server-known-issues.md) and [Major version differences](major-version-differences.md).

## Identify the symptom

Capture the complete error description and all available error records before changing settings. A top-level `HRESULT`, such as `DB_E_ERRORSOCCURRED`, doesn't identify the cause by itself. Record whether the failure occurs when loading the provider, opening a connection, executing a command, fetching data, or committing a transaction.

| Symptom | Start here |
| --- | --- |
| Provider can't be found, or class isn't registered. | [Provider registration and architecture](#provider-registration-and-architecture) |
| Login fails, access is denied, or integrated authentication fails. | [Login and authentication failures](#login-and-authentication-failures) |
| Certificate chain isn't trusted, or the certificate name doesn't match. | [TLS certificate failures](#tls-certificate-failures) |
| Server or instance can't be found, or the connection is refused. | [Network and instance discovery failures](#network-and-instance-discovery-failures) |
| Parameters fail, values are truncated, or data can't be converted. | [Parameter and data-conversion errors](#parameter-and-data-conversion-errors) |
| Connection drops, recovery fails, or a timeout expires. | [Connection loss and timeouts](#connection-loss-and-timeouts) |
| Error details are missing, or you need a trace for support. | [Diagnostics and tracing](#diagnostics-and-tracing) |

For connection failures, compare the application with a [Universal Data Link (UDL) connection test](/troubleshoot/sql/database-engine/connect/test-oledb-connectivity-use-udl-file). Use the same computer, provider, process architecture, authentication identity, server, database, and encryption settings. A successful test with a different provider or identity doesn't establish that the application's configuration works.

## Provider registration and architecture

Errors such as *Provider cannot be found* or `REGDB_E_CLASSNOTREG` (`0x80040154`, *Class not registered*) indicate provider loading before SQL Server authentication.

1. Check the provider that the application requests. `MSOLEDBSQL19` and `MSOLEDBSQL` identify different major versions. Installing the current driver doesn't change an application's provider selection. Follow the [migration steps](major-version-differences.md#migration-steps) if the application still requests another provider.
1. Check the architecture of the process that hosts the application. A 32-bit application needs the 32-bit provider, even on 64-bit Windows. For a service or scheduled job, check the executable and account used by that host, not just your development environment.
1. Install or repair the driver with the supported installer on the computer that runs the application. The x64 installer includes both 64-bit and 32-bit driver binaries. Check the required dependencies in [Install the OLE DB Driver](applications/installing-oledb-driver-for-sql-server.md) and [System requirements](system-requirements-for-oledb-driver-for-sql-server.md). Don't copy driver libraries from another computer as a substitute for installation.
1. Repeat the UDL test with the matching architecture and provider. If it works but the application still can't load the provider, compare the application's effective provider selection and host architecture with the test.

If the error specifically names `adal.dll`, check the [known authentication library issue](oledb-driver-for-sql-server-known-issues.md#known-issues) rather than treating it as a missing SQL Server provider.

## Login and authentication failures

Distinguish a server login rejection from a failure to obtain credentials or establish an encrypted connection. Read the full error text, including any nested provider error.

1. For SQL Server error 18456, ask the database administrator to inspect the corresponding server error log entry and state. Check the authentication mode, login status, requested database, and database access using [MSSQLSERVER_18456](../../relational-databases/errors-events/mssqlserver-18456-database-engine-error.md). Don't assume every login rejection means an incorrect password.
1. For integrated authentication, confirm the identity under which the application runs. A service account or scheduled-task account can differ from the user who successfully tested the connection. If the message includes *Cannot generate SSPI context*, follow [Security Support Provider Interface (SSPI) troubleshooting](/troubleshoot/sql/database-engine/connect/cannot-generate-sspi-context-error) and [Service Principal Name (SPN) support](features/service-principal-name-spn-support-in-client-connections.md).
1. For Microsoft Entra ID, check that the selected authentication method fits the application's execution environment and that its identity has access to the target database. Review the method-specific settings and access-token restrictions in [Use Microsoft Entra ID](features/using-azure-active-directory.md). Don't combine an access token with conflicting authentication or credential properties.
1. Compare the effective settings with the correct [connection string keyword table](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md). `IDBInitialize::Initialize`, `IDataInitialize::GetDataSource`, and ActiveX Data Objects (ADO) use different keyword tables. Check the table for the interface your application uses.

The text *The target principal name is incorrect* can appear in different contexts. If it accompanies *Cannot generate SSPI context*, investigate Windows authentication and SPNs. If the error identifies the certificate or encryption handshake, use the next section.

## TLS certificate failures

Transport Layer Security (TLS) errors can occur before a login reaches SQL Server. The current driver enables mandatory encryption by default, so an upgrade can expose a certificate trust or name problem that an older connection configuration didn't detect.

1. For *The certificate chain was issued by an authority that is not trusted*, check the certificate that SQL Server presents and the issuing certificate chain that the client computer trusts. Configure a valid server certificate and install the required trusted root and intermediate certificates through your organization's certificate management process.
1. For a certificate name mismatch, compare the server or listener name that the application uses with the names in the certificate. Use a certificate that covers the intended connection name. If the application intentionally uses a different connection name, review the documented [HostNameInCertificate property](major-version-differences.md#hostnameincertificate-v1900-and-later-versions) before configuring the expected certificate name.
1. Check the effective encryption and validation settings, including [registry settings](features/registry-settings.md#encryption-and-certificate-validation). Review the [encryption and certificate validation tables](features/encryption-and-certificate-validation.md) for precedence and `Strict` behavior. In `Strict` mode, the driver validates the certificate regardless of the trust-server-certificate setting.
1. If the failure began during migration, check [major-version troubleshooting](major-version-differences.md#troubleshooting), including the encryption property's value type and the restriction on using `ServerCertificate` outside `Strict` mode.

Use [Certificate requirements for SQL Server](../../database-engine/configure-windows/certificate-requirements.md) and [Certificate chain not trusted troubleshooting](/troubleshoot/sql/database-engine/connect/certificate-chain-not-trusted) for detailed checks. Keep encryption and certificate validation enabled in production. Disabling either doesn't repair a certificate deployment problem.

## Network and instance discovery failures

For *server not found*, *error locating server/instance specified*, or connection-refused errors, identify the endpoint that the application is trying to reach.

1. Verify the server name, instance name, and configured listening port with the database administrator. Confirm that the database service is running and that the intended protocol and listener are enabled. Don't assume every instance listens on port 1433.
1. For a remote Transmission Control Protocol (TCP) connection, test the known endpoint by using the driver's `tcp:<server>,<port>` server-name format. Keep the same authentication, database, and encryption settings. See [Connection string keywords](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md) for the server keyword that applies to your interface.
1. If the explicit host and port work but the named instance doesn't, investigate SQL Server Browser and instance discovery. Check the Browser service and the User Datagram Protocol (UDP) port 1434 path where Browser discovery is used.
1. If the explicit endpoint also fails, check Domain Name System (DNS) resolution, routing, and firewall access to the actual listening port from the application host. Follow [Network-related or instance-specific connection errors](/troubleshoot/sql/database-engine/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection) rather than changing several connection settings at once.

For an availability group listener, also review [High availability and disaster recovery support](features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md). For LocalDB, use [LocalDB support](features/oledb-driver-for-sql-server-support-for-localdb.md) to check the local instance and user context instead of applying remote TCP discovery steps.

## Parameter and data-conversion errors

If the connection opens but command execution or data retrieval fails, reduce the reproduction to the failing command and value. Preserve the original data type, length, null status, and character encoding when replacing sensitive data.

1. Compare each `?` parameter marker with its binding ordinal, direction, and metadata. When you use `ICommandWithParameters::SetParameterInfo`, match the SQL source type to the command or stored procedure. Don't assume parameter metadata is always derived automatically. Review [Command parameters](ole-db-commands/command-parameters.md) for derivation restrictions and output-parameter behavior.
1. Inspect accessor binding statuses and each returned value's status and length, not just the overall `HRESULT`. For property-setting failures, inspect each property's `dwStatus`. A partial-success return such as `DB_S_ERRORSOCCURRED` can require status-array inspection even when no error object is available. See [Return codes](ole-db-errors/return-codes.md).
1. For conversion or truncation, compare the consumer buffer type and size with the actual column or parameter metadata. Check precision and scale for numeric values, valid ranges and fractional seconds for date/time values, and byte lengths for character buffers. Investigate `DBSTATUS_E_CANTCONVERTVALUE`, and don't treat `DBSTATUS_S_TRUNCATED` as a complete value. Use [Data type mapping](ole-db-data-types/data-type-mapping-in-rowsets-and-parameters.md), [Fetching rows](ole-db-rowsets/fetching-rows.md), and [Date and time conversions](ole-db-date-time/conversions-ole-db.md) for the applicable rules.
1. If bound output parameters appear missing, exhaust the returned rowsets before reading them. Follow [Use IMultipleResults to process multiple result sets](ole-db-commands/using-imultipleresults-to-process-multiple-result-sets.md). For streamed output parameters, consume or release pending streams before requesting the next result, as described in [Streaming support for output parameters](ole-db-blobs/streaming-support-for-blob-output-parameters.md).

For ADO-specific mappings, review [Use ADO with the OLE DB Driver](applications/using-ado-with-oledb-driver-for-sql-server.md) and the authentication restrictions on `DataTypeCompatibility` in [Use Microsoft Entra ID](features/using-azure-active-directory.md). Don't add a compatibility setting without checking both.

For corrupted narrow strings in a **sql_variant** column after a driver upgrade, review the existing [SSVARIANT known issue and recovery procedure](ole-db-data-types/ssvariant-structure.md#known-issues) before modifying stored data.

## Connection loss and timeouts

Record when the connection last worked, which operation failed, and how long that operation ran. Distinguish these cases before changing retry or timeout settings.

| Failing stage | Checks and detailed guidance |
| --- | --- |
| Opening a connection. | Inspect provider, network, authentication, and TLS errors first. Check the effective `DBPROP_INIT_TIMEOUT` or the corresponding connection keyword. See [Connection timeout troubleshooting](/troubleshoot/sql/database-engine/connect/timeout-expired-error). |
| Executing a command. | Check `DBPROP_COMMANDTIMEOUT` or the application's command timeout setting. Investigate blocking and query performance with [Query timeout troubleshooting](/troubleshoot/sql/database-engine/performance/troubleshoot-query-timeouts). Increasing the connection timeout doesn't change the command timeout. |
| Reusing an idle connection. | Check the recovery conditions, retry settings, and expected errors in [Idle connection resiliency](features/idle-connection-resiliency.md). Recovery can fail when the command timeout expires before reconnection completes. |
| Losing a connection during execution or commit. | Correlate client and server events to check for a network interruption, server restart, or failover. Establish the outcome of the operation before deciding whether it's safe to retry. |

Idle connection resiliency doesn't provide initial-connection retries or automatic replay of arbitrary commands and transactions. For a confirmed transient failure, use bounded application retries with a delay, and log each attempt. Don't repeatedly retry provider-loading errors, rejected credentials, or certificate validation failures without correcting the cause.

> [!CAUTION]
> If a connection drops during a write or commit, the client might not know whether SQL Server committed the transaction. Don't blindly replay the operation. Check its outcome or use an application design that prevents duplicate effects before retrying.

## Diagnostics and tracing

Collect diagnostics at the point of failure, before unrelated provider calls replace the error information.

1. Capture the failing operation, timestamp and time zone, elapsed time, and `HRESULT`. For native OLE DB consumers, retrieve all available records through `IErrorInfo` and `IErrorRecords`, not only the first description. Include `SQLSTATE` and the native SQL Server error number when available through `ISQLErrorInfo`. See [Retrieve error information](ole-db-errors/retrieving-error-information.md) and [SQL Server error detail](ole-db-errors/sql-server-error-detail.md). For ADO, capture the connection's `Errors` collection.
1. Collect per-property, per-binding, and per-value statuses for methods that report errors that way. An absent error object doesn't make a partial-success result safe to ignore.
1. Correlate the client failure with the server error log or Extended Events. When available, record `ClientConnectionID` and `ActivityID`. A failure before prelogin can occur without a client connection identifier.
1. If error records aren't sufficient, use [Access diagnostic information in the Extended Events log](features/accessing-diagnostic-information-in-the-extended-events-log.md) for driver tracing and correlation setup. Collect a bounded trace around the reproduction and stop tracing afterward.

When you escalate, include the driver version, requested provider, application and process architecture, server version, authentication method, effective connection settings, failure stage, error records, and a minimal reproduction. State whether the matching UDL test succeeds and whether the issue affects one host or multiple hosts.

Remove passwords, access tokens, and other secrets from connection settings and logs. Review traces for query text and sensitive data, store them with restricted access, and share them only through an approved support channel.

## Related content

- [Quickstart: Connect and query with the Microsoft OLE DB Driver](quickstart-cpp.md)
- [Known issues](oledb-driver-for-sql-server-known-issues.md)
- [Release notes](release-notes-for-oledb-driver-for-sql-server.md)
- [SQL Server connectivity troubleshooting overview](/troubleshoot/sql/database-engine/connect/resolve-connectivity-errors-overview)
