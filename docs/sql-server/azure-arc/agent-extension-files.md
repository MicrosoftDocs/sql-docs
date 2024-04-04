---
title: Azure extension for SQL Server files and registry reference
description: Lists files and registry keys deployed and managed by the Azure extension for SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: nikitatakru
ms.date: 04/02/2024
ms.topic: reference
---

# Azure extension for SQL Server files & registry reference

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article lists files and registry keys installed or set when you enable servers or SQL Server instances for Azure Arc. 

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

## Registry keys

Base key: `HKEY_LOCAL_MACHINE`

| Key | Description & notes |
| :----- | :----- |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\MSSQLSERVER` | Microsoft Entra ID registry key |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\PurviewConfig` | Purview registry key |
| `SOFTWARE\Microsoft\SystemCertificates` | Windows certificate registry key |

## Related content

- [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
