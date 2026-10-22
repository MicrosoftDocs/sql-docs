---
title: Azure Extension for SQL Server System Objects
description: Lists system objects - files, registry keys, and tables deployed and managed by the Azure extension for SQL Server.
author: adbadram
ms.author: adbadram
ms.reviewer: nikitatakru
ms.date: 12/08/2025
ms.topic: reference
---

# Azure extension for SQL Server system objects

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article lists system objects that Azure extension for SQL Server deploys and manages. They include:

- Files
- Registry keys
- Windows services
- Tables

## Windows server files

| Path | Description |
| --- | --- |
| `%ProgramFiles%\AzureConnectedMachineAgent\*` | `azcmagent` CLI and instance metadata service executables |
| `%ProgramFiles%\AzureConnectedMachineAgent\GCArcService\GC\*` | Extension service executables |
| `%ProgramData%\AzureConnectedMachineAgent\*` | Configuration, log, and identity token files for `azcmagent` CLI and instance metadata service |
| `%ProgramData%\Application Data\Microsoft\Crypto\RSA\MachineKeys` | Windows certificate private keys |

## SQL Server files

| Path | Description & notes |
| --- | --- |
| `%ProgramFiles%\SQL Server Extension\*` | Extension program files |
| `%SYSTEMDRIVE%\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SQLServer\<extension_version>\*` | Extension executables |
| `%SYSTEMDRIVE%\Windows\system32\extensionUpload\*` | Usage files |
| `C:\Windows\System32\Tasks\Microsoft\SqlServerExtension` | XML for scheduled task for providing privileges |
| `C:\Windows\ServiceProfiles\SqlServerExtension\AppData\Local\Microsoft SQL Server Extension Agent\*` | When configured for [least privilege](configure-least-privilege.md)<br /><br />Feature application |
| `C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent\*` | When not configured for [least privilege](configure-least-privilege.md)<br /><br />Feature application |

> [!NOTE]  
> [!INCLUDE [least-privilege-default](includes/least-privilege-default.md)]

## Windows Services

| Service name | Display name | Process name | Description |
| --- | --- | --- | --- |
| `SqlServerExtension` | Microsoft SQL Server Extension Service | SqlServerExtension.exe | Connects your SQL Server instance to Azure. |
| `himds` | Azure Hybrid Instance Metadata Service | `himds.exe` | Synchronizes metadata with Azure and hosts a local REST API for extensions and applications to access the metadata and request Microsoft Entra managed identity tokens |
| `GCArcService` | Machine configuration Arc Service | `gc_arc_service.exe` (gc_service.exe earlier than version 1.36) | Audits and enforces Azure machine configuration policies on the machine. |
| `ExtensionService` | Machine configuration Extension Service | `gc_extension_service.exe` (gc_service.exe earlier than version 1.36) | Installs, updates, and manages extensions on the machine. |

## Virtual service accounts

| Virtual Account | Description |
| --- | --- |
| `NT SERVICE\himds` | Unprivileged account used to run the Hybrid Instance Metadata Service. |
| `NT Service\SQLServerExtension` | Unprivileged account used to run the SQL Server Extension Service in least privilege mode. |

## Registry keys

Base key: `HKEY_LOCAL_MACHINE`

| Key | Description & notes |
| --- | --- |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\MSSQLSERVER` | Microsoft Entra ID registry key |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\PurviewConfig` | Purview registry key |
| `SOFTWARE\Microsoft\SystemCertificates` | Windows certificate registry key |

## Tables

In each instance of SQL Server enabled by Azure Arc, the extension creates the following tables in `msdb`:

| Table Names| Purpose |
| --- | --- |
|`dq.arcJobTriggers00`, `dt.arcJobDefinitions`| These tables store background job definition and execution history. Background jobs perform scheduled and user-initiated actions. These tables allow long-running jobs to automatically resume if the Azure Extension for SQL Server is restarted.|
| `dbo.SQLServerAzureArcProperties` | This table contains the resource identity for the SQL Server instance in Azure Resource Manager. This table can be used to detect if the SQL Server instance is Arc-enabled, and if so, what the identity of the resource is in Azure. |

## Frequently asked questions

### Where are these background jobs?

The background jobs are used to perform long-running tasks that can persist state if the computer restarts. The logic of the jobs is stored in the extension, while state is stored in `msdb`. For example, a migration assessment job can take a long time to execute, so the state is stored in `msdb`.

### What security context do the jobs run under?

For least privilege mode, the jobs run under the `C:\Program Files\SQL Server Extension\SqlServerExtension.Service.exe` service context. The service connects to the `msdb` database on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] with the `NT Service\SQLServerExtension` account. This service has the minimum permissions required to operate on `msdb`.

If the agent extension is not in least privilege mode, the connection is made using the `LocalSystem` account.

### How long are the rows on this table retained for? What is the purge policy?

The maximum job lifetime is 15 days. This setting is currently not user configurable via the Azure Resource Manager API. After 15 days, the engine automatically purges old jobs that are finished executing.

A given job has a maximum lifetime of one day before it fails. This period limits the lifetime a job can remain on the system.

### How large are these tables expected to grow?

The tables are expected to be small. The retention period is finite, and there's only a few jobs.

### What indexes do I need on these tables?

Indexes don't help performance. The tables should be trivially sized as they're tied to the number of features/jobs running at a given point in time.

### Which features use these tables and jobs?

Various features of the Arc SQL Extension features use background jobs to store state.

Including:

- Best Practice Assessment
- Migration Assessment
- Database Backup/Restore

The tables allow the extension to continue expensive operations (such as discovery) that can take long periods of time, without starting from scratch each time.

### What happens if these tables are inadvertently dropped or if corruption occurs in these tables?

If the tables are dropped, state is lost, and the extension deployer recreates the table. If the tables are corrupted by mutating state the jobs are reading from, the extension might fail.

### How can these tables be manually recreated if they're missing?

Any Arc action that invokes the extension deployer - such as an ARM settings change, or an upgrade - recreates the tables.

## Related content

- [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
