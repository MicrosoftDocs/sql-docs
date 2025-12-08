---
title: Azure extension for SQL Server system objects
description: Lists system objects - files, registry keys, and tables deployed and managed by the Azure extension for SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: nikitatakru
ms.date: 04/26/2024
ms.topic: reference
---

# Azure extension for SQL Server system objects

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article lists system objects - files, registry keys, Windows services and tables deployed and managed by the Azure extension for SQL Server.

## Windows server files

| Path | Description |
| :----- | :----- |
| `%ProgramFiles%\AzureConnectedMachineAgent\*` | `azcmagent` CLI and instance metadata service executables |
| `%ProgramFiles%\AzureConnectedMachineAgent\GCArcService\GC\*` | Extension service executables |
| `%ProgramData%\AzureConnectedMachineAgent\*` | Configuration, log, and identity token files for `azcmagent` CLI and instance metadata service |
| `%ProgramData%\Application Data\Microsoft\Crypto\RSA\MachineKeys` | Windows certificate private keys | 

## SQL Server files

| Path | Description & notes |
| :----- | :----- |
| `%ProgramFiles%\Sql Server Extension\*` | Extension program files |
| `%SYSTEMDRIVE%\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SQLServer\<extension_version>\*` | Extension executables |
| `%SYSTEMDRIVE%\Windows\system32\extensionUpload\*` | Usage files |
| `C:\Windows\System32\Tasks\Microsoft\SqlServerExtension` | XML for scheduled task for providing privileges |
| `C:\Windows\ServiceProfiles\SqlServerExtension\AppData\Local\Microsoft SQL Server Extension Agent\*` | When configured for [least privilege](configure-least-privilege.md) <br/><br/> Feature application |
| `C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent\*`| When not configured for [least privilege](configure-least-privilege.md) <br/></br> Feature application |

> [!NOTE]
> [!INCLUDE [least-privilege-default](includes/least-privilege-default.md)]

## Windows Services

| Service name | Display name | Process name | Description |
| :----- | :----- | :----- | :----- |
| SqlServerExtension |Microsoft Sql Server Extension Service | SqlServerExtension.exe | Connects your SQL Server instance to Azure.  | 
| himds | Azure Hybrid Instance Metadata Service | `himds.exe` | Synchronizes metadata with Azure and hosts a local REST API for extensions and applications to access the metadata and request Microsoft Entra managed identity tokens |
| GCArcService | Machine configuration Arc Service | `gc_arc_service.exe` (gc_service.exe earlier than version 1.36) | Audits and enforces Azure machine configuration policies on the machine. |
| ExtensionService | Machine configuration Extension Service | `gc_extension_service.exe` (gc_service.exe earlier than version 1.36) | Installs, updates, and manages extensions on the machine. |

## Virtual service accounts

| Virtual Account  | Description |
|------------------|-------------|
| `NT SERVICE\himds` | Unprivileged account used to run the Hybrid Instance Metadata Service. |
| `NT Service\SQLServerExtension` | Unprivileged account used to run the SQL Server Extension Service in least privilege mode. |

## Registry keys

Base key: `HKEY_LOCAL_MACHINE`

| Key | Description & notes |
| :----- | :----- |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\MSSQLSERVER` | Microsoft Entra ID registry key |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\PurviewConfig` | Purview registry key |
| `SOFTWARE\Microsoft\SystemCertificates` | Windows certificate registry key |

## Tables

In each instance of SQL Server enabled by Azure Arc, the extension creates the following tables in `msdb`:

- `dq.arcJobTriggers00`
- `dt.arcJobDefinitions`

These tables store background job definition and execution history. Background jobs perform scheduled and user-initiated actions. These tables allow long-running jobs to automatically resume in the event that the Azure Extension for SQL Server is restarted.

Additionally, the table `dbo.SQLServerAzureArcProperties` contains the resource identity for the SQL Server instance in Azure Resource Manager.  This table can be used to detect if the SQL Server instance is Arc-enabled, and if so, what the identity of the resource is in Azure.

### FAQs

1. Where are these background jobs?
   
   The background jobs are used to perform long-running tasks that can persist state in case of computer restarts. The logic of the jobs are in the extension, while state state is stored in `msdb`. For example, a Migration Assessment Job can take a long time to execute, the state is stored in `msdb`.
   
2. What security context do they use to execute?

   They are run in the Service `C:\Program Files\Sql Server Extension\SqlServerExtension.Service.exe`. The service connects to SQL Server `msdb` as the low privelge `NT Service\SQLServerExtension` user. 
   
   This service has the minimum amount of permissions required to operate on `msdb`.

3. How long are the rows on this table retained for? What is the purge policy?

   The maximum job lifetime is 15 days, it is currently not user configurable via the ARM API. After 15 days, the engine automatically purges old jobs that have finished executing.

   A given Job has a maximum lifetime of 1 day, before it is failed. This limits the lifetime a job can remain on the system.

3. How large are these tables expected to grow?

   Very minimal, as the retention is finite, and there are a handful of jobs.

4. Any indexes needed on these tables?

   No, indexes will not help performance, as the tables are expected to be trivially sized as they are tied to the number of features/jobs running at a given point in time.


## Related content

- [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
