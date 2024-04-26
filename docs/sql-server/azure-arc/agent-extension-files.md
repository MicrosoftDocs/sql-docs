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

This article lists system objects - files, registry keys, and tables deployed and managed by the Azure extension for SQL Server.

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

## Related content

- [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
