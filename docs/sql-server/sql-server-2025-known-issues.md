---
title: SQL Server 2025 Known Issues
description: Known issues, causes, and workarounds for SQL Server 2025 (17.x), covering upgrades, replication, PolyBase, session behavior, platform compatibility (Windows and Linux), backup compression, and other platform-specific limitations.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 03/04/2026
ms.service: sql
ms.subservice: release-landing
ms.topic: troubleshooting-known-issue
ms.custom:
  - ignite-2025
monikerRange: ">=sql-server-2016"
---

# SQL Server 2025 known issues

[!INCLUDE [sqlserver2025](../includes/applies-to-version/sqlserver2025.md)]

This article describes known issues for [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

The following issues are currently identified:

- [Installation fails when TLS 1.2 is disabled](#sql-server-2025-installation-fails-when-tls-12-is-disabled)
- [Windows Arm64 not supported](#windows-arm64-not-supported)
- [In-place upgrade fails due to Microsoft Visual C++ Redistributable](#in-place-upgrade-fails-due-to-microsoft-visual-c-redistributable)
- [SQL Server on Windows fails to start on machines with more than 64 logical cores per NUMA node](#sql-server-on-windows-fails-to-start-on-machines-with-more-than-64-logical-cores-per-numa-node)
- [Database mail on Linux](#database-mail-on-linux)
- [SQLPS](#sqlps)
- [Incorrect behavior of SESSION_CONTEXT in parallel plans](#incorrect-behavior-of-session_context-in-parallel-plans)
- [Issue when setting the backup compression algorithm to ZSTD](#issue-when-setting-the-backup-compression-algorithm-to-zstd)
- [Local ONNX models not supported on Linux operating systems](#local-onnx-models-not-supported-on-linux-operating-systems)
- [PBKDF2 hashing algorithm can affect login performance](#pbkdf2-hashing-algorithm-can-affect-login-performance)
- [Vector index](#vector-index)
- [SQL Server audit events don't write to the Security log](#sql-server-audit-events-dont-write-to-the-security-log)
- [Upgrade fails if Data Quality Services is installed](#upgrade-fails-if-data-quality-services-is-installed)
- [Full-Text Search](#full-text-search)
- [Incorrect license agreement for LocalDB installer](#incorrect-license-agreement-for-localdb-installer)
- [SQL Server might become slow or unresponsive after creating or bringing online a large number of databases](#sql-server-might-become-slow-or-unresponsive-after-creating-or-bringing-online-a-large-number-of-databases)

## SQL Server 2025 installation fails when TLS 1.2 is disabled

**Issue**: [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] installation fails if TLS 1.2 is disabled on the machine, including failover cluster instances.

**Workaround**: Enable TLS 1.2 on the machine before attempting to install [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

To enable TLS 1.2, set the following registry entry for TLS 1.2 to `true`:

```text
HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols
```

[Configure Windows to use TLS](../relational-databases/security/networking/connect-with-tls-1-3.md) provides a PowerShell script to enable TLS 1.2 programmatically.

## Windows Arm64 not supported

[!INCLUDE [sssql25-md](../includes/sssql25-md.md)] isn't supported on Windows Arm64. Only Intel and AMD x86-64 CPUs with [up to 64 cores per NUMA node](compute-capacity-limits-by-edition-of-sql-server.md#numa-64) are currently supported.

## In-place upgrade fails due to Microsoft Visual C++ Redistributable

An upgrade from the following versions might fail:

- [!INCLUDE [sssql16-md](../includes/sssql16-md.md)]
- [!INCLUDE [sssql17-md](../includes/sssql17-md.md)]

This can happen when the existing operating system environment is missing the Microsoft Visual C++ Redistributable for Visual Studio 2022, or an older version of this component is installed.

When this happens, the installation log includes an entry like the following example:

```output
This application requires Microsoft Visual C++ Redistributable for
Visual Studio 2022 (x64/x86, version 14.34 at minimum).
Please install the Redistributable, then run this installer again.
For more information, see: https://go.microsoft.com/fwlink/?linkid=2219560.
```

To complete the upgrade, add or repair the redistributable component, and run the installation again.

To get the redistributable file, review [Microsoft Visual C++ Redistributable latest supported downloads](/cpp/windows/latest-supported-vc-redist).

## SQL Server on Windows fails to start on machines with more than 64 logical cores per NUMA node

**Issue**: SQL Server instances on Windows might fail to start after the installation if the machine has more than 64 logical cores per NUMA node.

For more information, see [Limit number of logical cores per NUMA node to 64](compute-capacity-limits-by-edition-of-sql-server.md#limit-number-of-logical-cores-per-numa-node-to-64).

## Database mail on Linux

**Issue**: Database mail on Linux doesn't work when SQL Server is configured to enforce strict encryption.

Currently, the only workaround is to not enforce strict encryption.

## SQLPS

**Issue**: SQLPS.exe, the SQL Agent PowerShell subsystem, and the SQLPS PowerShell module don't work when SQL is configured to enforce strict encryption.

Currently, the only workaround is to not enforce strict encryption.

The SQL Server Agent job `syspolicy_purge_history` reports a failure on step 3. This job runs daily by default. An instance that doesn't enforce strict encryption doesn't reproduce this problem; another option is to disable the job.

## Incorrect behavior of SESSION_CONTEXT in parallel plans

Queries that use the built-in `SESSION_CONTEXT` function might return incorrect results or trigger access violation (AV) dumps when executed in parallel query plans. This issue stems from the way the function interacts with parallel execution threads, particularly when the session is reset for reuse.

For more information, see the [Known issues](../t-sql/functions/session-context-transact-sql.md#known-issues) section in `SESSION_CONTEXT`.

## Issue when setting the backup compression algorithm to ZSTD

There's a known issue when attempting to set the [backup compression algorithm](../database-engine/configure-windows/view-or-configure-the-backup-compression-algorithm-server-configuration-option.md) to ZSTD.

When specifying the ZSTD algorithm (`backup compression algorithm = 3`), the following error message returns:

```output
Msg 15129, Level 16, State 1
Procedure sp_configure '3' is not a valid value for configuration option 'backup compression algorithm'.
```

Use the new compression algorithm directly in the [BACKUP](../t-sql/statements/backup-transact-sql.md#compression) Transact-SQL command instead of setting the server configuration option.

## Local ONNX models not supported on Linux operating systems

[CREATE EXTERNAL MODEL](../t-sql/statements/create-external-model-transact-sql.md) local ONNX models hosted directly on the SQL Server aren't currently available for Linux on [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

## PBKDF2 hashing algorithm can affect login performance

In [!INCLUDE [sssql25-md](../includes/sssql25-md.md)], password-based authentication uses PBKDF2 (RFC2898) as the default hashing algorithm. This enhancement improves password security by applying 100,000 iterations of SHA-512 hashing. The increased computational cost of PBKDF2 means slightly longer SQL Authentication login time. This effect is especially noticeable in environments without connection pooling, or where login latency is closely monitored. In pooled environments, the effect is typically minimal.

For more information, see [CREATE LOGIN](../t-sql/statements/create-login-transact-sql.md) and [Support for Iterated and Salted Hash Password Verifiers in SQL Server 2022 CU12](https://techcommunity.microsoft.com/blog/azuresqlblog/support-for-iterated-and-salted-hash-password-verifiers-in-sql-server-2022-cu12/4087155).

## SQL Server audit events don't write to the Security log

Assume that you configured multiple [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] audit events to write to the [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Security log. In this scenario, you notice that all server audits, except the first server audit, don't write. Additionally, when you add the second server audit, you might receive an error that resembles the following message in the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] error log:

```output
Error: 33204, Severity: 17, State: 1.
SQL Server Audit could not write to the security log.
```

We have identified a fix for a future release of [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

**Workaround**: Use one of the following methods:

- Write audit events to a file instead of the SQL Server Security log.

- To let multiple server audits write to the Security log, change this registry subkey value from `0` to `1`:

  ```text
  HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\EventLog\Security\MSSQL$<InstanceName>$Audit\EventSourceFlags
  ```

  Server audits must be restarted for the new registry setting to take effect:

  ```sql
  ALTER SERVER AUDIT [AuditName] WITH (STATE = OFF);
  GO
  ALTER SERVER AUDIT [AuditName] WITH (STATE = ON);
  GO
  ```

## Vector index

When you build a vector index using the `CREATE VECTOR INDEX` statement, or using the vector index via `VECTOR_SEARCH`, you get the following warning message:

```output
Warning: The join order has been enforced because a local join hint is used.
```

The warning can be safely ignored, as it doesn't affect the correctness of the results.

When you use `MAXDOP` with `CREATE VECTOR INDEX` or `VECTOR_SEARCH`, the value set for `MAXDOP` is ignored. To set the desired value for `MAXDOP`, set the server-level `max degree of parallelism` configuration option instead. For more information, see [Server configuration: max degree of parallelism](../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) or the database-level `MAXDOP` option in [ALTER DATABASE SCOPED CONFIGURATION](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

## Upgrade fails if Data Quality Services is installed

If Data Quality Services is installed and you upgrade your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance to [!INCLUDE [sssql25-md](../includes/sssql25-md.md)], the upgrade fails during the Feature Rules step of the SQL Server Upgrade wizard.

:::image type="content" source="media/sql-server-2025-known-issues/upgrade-error-data-quality-services.png" alt-text="Screenshot of SQL Server Upgrade Feature Rules screen, with the Data Quality Services highlighted in red.":::

**Workaround**: Use the `/IACCEPTDQUNINSTALL` parameter from the command line. For more information, see [Upgrade parameters](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#upgrade-parameters) in the article [Install, configure, or uninstall SQL Server on Windows from the command prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).

You can also run a full unattended upgrade from the command line, as long as you include the `/IACCEPTDQUNINSTALL` parameter.

## Full-Text Search

**Issue**: Full-Text Search fails to index plaintext documents larger than 25 MB
If you try to index a plaintext document larger than 25 MB, you see the symbolic error `FILTER_E_PARTIALLY_FILTERED` in the crawl log:

```output
Error '0x8004173e: The document was too large to filter in its entirety. Portions of the document were not emitted.' occurred during full-text index population for table or indexed view ...
```

> [!NOTE]  
> Plaintext documents include documents with a `class_id` of `{C1243CA0-BF96-11CD-B579-08002B30BFEB}`, as reported by [sys.fulltext_document_types](../relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql.md).

**Workaround**: Configure the maximum file size in the Windows registry:

> [!WARNING]  
> [!INCLUDE [ssnoteregistry-md](../includes/ssnoteregistry-md.md)]

Edit the DWORD value `MaxTextFilterBytes`, which is located in `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ContentIndex`. For example, to remove the size limit entirely from the command line with [`reg add`](/windows-server/administration/windows-commands/reg-add), run the following command:

```console
reg add "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ContentIndex" /v MaxTextFilterBytes /t REG_DWORD /d ffffffff
```

After updating the registry value, reissue the Full-Text crawl.

**Issue**: Full-Text Queries using inflectional forms fail for certain languages when Index Version2 is enabled

Full-Text uses stemmers for Freetext, Freetexttable or `FORMSOF(INFLECTIONAL)` argument in Contains and Containstable. For languages where a stemmer isn't registered or available, queries that reference inflectional forms can fail with following error.

```output
Msg 30010, Level 16, State 2, Line 119
An error has occurred during the full-text query. Common causes include: word-breaking errors or timeout, FDHOST permissions/ACL issues, service account missing privileges, malfunctioning IFilters, communication channel issues with FDHost and sqlservr.exe, etc. If recently performed in-place upgrade to SQL2025, For help please see https://aka.ms/sqlfulltext.
```

**Workaround**: Avoid using inflectional-form queries for languages that don't have registered stemmers. For more information, see [Version 2 word breakers](../relational-databases/search/full-text-index-binaries.md#word-breakers). If the application has a strong dependency on inflectional or linguistic search behavior for such languages, configure the database to use Full-Text Index Version 1 instead.

## Incorrect license agreement for LocalDB installer

**Issue**: The LocalDB installer points to a preview version of the end-user license agreement (EULA).

To work around this issue, you must download the Express edition installer instead, and choose the **LocalDB** option from the package selection screen.

We have identified a fix for a future release of [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

## SQL Server might become slow or unresponsive after creating or bringing online a large number of databases

**Issue**: This behavior is caused by a per database background worker thread created as part of the [Persisted statistics for readable secondary replicas](../relational-databases/performance/persisted-stats-secondary-replicas.md) feature. This feature is enabled by default in [!INCLUDE [sssql25-md](../includes/sssql25-md.md)]. The background thread is created when databases come online and can cause worker thread pressure and reduced instance responsiveness, even when no secondary replicas are configured.

**Workaround**: Enable startup [trace flag 15608](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf15608) and restart [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. You must enable the trace flag at startup. Enabling it after startup doesn't stop background threads that are already created for databases that were brought online. In scenarios with no secondary replicas, this trace flag is still required as a temporary mitigation to prevent the per database background thread from being created during database startup.

A fix is planned for a future update of [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

## Related content

- [What's new in SQL Server 2025](what-s-new-in-sql-server-2025.md)
- [SQL Server 2025 release notes](sql-server-2025-release-notes.md)
- [Breaking changes to Database Engine features in SQL Server 2025](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md)
