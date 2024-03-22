--- 
title: Files deployed by Azure extension for SQL Server
description: Lists files deployed and managed by the Azure extension for SQL Server
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: nikitatakru
ms.date: 03/22/2024
ms.topic: reference
---

# Files deployed by Azure extension for SQL Server

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article lists the files that an installation of Azure extension for SQL Server creates and maintains.

## Files

| Path | Description |
| :----- | :----- |
|` %SYSTEMDRIVE%\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SQLServer\<extension_version>\*` |  |
| `%ProgramFiles%\Sql Server Extension\*` |  |
| `C:\Windows\System32\Tasks\Microsoft\SqlServerExtension` |  |
| `%ProgramFiles%\AzureConnectedMachineAgent\*` |  |
| `%ProgramFiles%\AzureConnectedMachineAgent\ExtensionService\GC\*` |  |
| `%ProgramFiles%\AzureConnectedMachineAgent\GCArcService\GC\*` |  |
| `%ProgramData%\AzureConnectedMachineAgent\*` |  |
| `%SYSTEMDRIVE%\Windows\system32\extensionUpload\*` |  |
| `C:\Windows\ServiceProfiles\SqlServerExtension\AppData\Local\Microsoft SQL Server Extension Agent\*` | When configured for [least privilege](configure-least-privilege.md).  |
| `C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent\*`| When not configured for [least privilege](configure-least-privilege.md). |

## Related content

[Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
