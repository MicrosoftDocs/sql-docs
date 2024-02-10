---
title: Configure Windows service accounts and permissions for SQL Server enabled by Azure Arc
description: Get acquainted with permissions required to start and run Azure Extension for SQL Server agent service account. See how to configure them and assign appropriate permissions.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: nikitatakru
ms.date: 01/17/2024
ms.topic: reference
---

# Configure Windows service accounts and permissions for Azure Extension for SQL Server

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article lists the permissions Azure Extension for SQL Server requires for SQL Server enabled by Azure Arc.

These permissions are set automatically, when you configure least privilege. To configure least privilege, follow the steps in [Operate SQL Server enabled by Azure Arc with least privilege (preview)](configure-least-privilege.md).

It is not recommended to manually set the permissions described in this article.

## Directory permissions

| Directory path | Required permissions | Details | Feature |
| :----- | :----- | :----- | :----- |
| `<SystemDrive>\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SQLServer` | Full control | Extension related dlls and exe files. | Default |
| `C:\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SqlServer\<extension_version>\RuntimeSettings` | Full control | Extension settings file. | Default |
| `C:\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SqlServer\<extension_version>\status` | Full control | Extension status file. | Default |
| `C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer` | Full control | Extension log files. | Default |
| `C:\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SqlServer\<extension_version>\status\HeartBeat.Json` | Full control | Extension heartbeat file. | Default |
| `%ProgramFiles%\Sql Server Extension` | Full control | Extension service files. | Default |
| `<SystemDrive>\Windows\system32\extensionUpload` | Full control | Required to write usage file needed for billing. | Default |
| `<SystemDrive>\Windows\system32\ExtensionHandler.log` | Full control | Pre-log folder created by extension. | Default |
| `<ProgramData>\AzureConnectedMachineAgent\Config` | Read | Arc config files directory. | Default |
| `C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent` | Full control | Required to write assessment reports and status. | Default |
| SQL log directory (as set in registry<sup>1</sup>).: <br/> `C:\Program Files\Microsoft SQL Server\MSSQL<base_version>.<instance_name>\MSSQL\log` | Read | Required to extract SQL vCores info from SQL logs. | Default |
| SQL backup directory (as set in registry<sup>1</sup>).: <br/> `C:\Program Files\Microsoft SQL Server\MSSQL<base_version>.<instance_name>\MSSQL\Backup` | ReadAndExecute/Write /Delete | Required for Backups | Backup |

<sup>1</sup> For more information, see [File Locations and Registry Mapping](../install/file-locations-for-default-and-named-instances-of-sql-server.md#file-locations-and-registry-mapping).

## Account

SQL Extension for Azure Arc creates the following user in each database: `NT Service\SqlServerExtension`.

## Registry permissions

Base key – `HKEY_LOCAL_MACHINE`.

| Registry key | Permission required | Details | Feature |
| :----- | :----- | :----- | :----- |
| `SOFTWARE\Microsoft\Microsoft SQL Server` | Read | Read SQL Server properties like `installedInstances`. | Default |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\MSSQLSERVER` | Full control | Microsoft Entra ID and Purview. | Microsoft Entra ID </br></br>Purview |
| `SOFTWARE\Microsoft\SystemCertificates` | Full control | Required for Microsoft Entra ID. | Microsoft Entra ID |
| `SYSTEM\CurrentControlSet\Services` | Read | SQL Server account name. | Default |
| `SOFTWARE\Microsoft\AzureDefender\SQL` | Read | Azure Defender status and last update time. | Default |
| `SOFTWARE\Microsoft\SqlServerExtension` | Full control | Extension related values. | Default |
| `SOFTWARE\Policies\Microsoft\Windows` | Read and Write | Enabling automatic windows update via extension. | Patching |

## Group permissions

`NT Service\SQLServerExtension` is added to Hybrid agent extension applications. Supports Azure Instance Metadata Service (IMDS) Handshake.

## SQL Permissions

`NT Service\SQLServerExtension` is added as SQL Login to all the instances present currently on machine.

| Feature | Permission | Level |
| :---- | :---- | :---- |
| Default | VIEW DATABASE STATE | Server Level |
| | VIEW SERVER STATE | Server Level |
| | CONNECT SQL | Server Level |
| Database as a Resource | Default public role | Server level (This is granted by default to newly added logins) |
| Best practices assessment | VIEW ANY DEFINITION | Server Level |
| | VIEW ANY DATABASE | Server Level |
| | SELECT | master |
| | SELECT | msdb |
| | EXECUTE ON sys.xp_enumerrorlogs| master |
| | EXECUTE ON sys.xp_readerrorlog | master |
| Backup | CREATE ANY DATABASE | Server level |
| | db_backupoperator role | All databases |
| | dbcreator | Server role |
| Azure Control Plane | CREATE TABLE | msdb |
| | ALTER ANY SCHEMA | msdb |
| | CREATE TYPE | msdb |
| | EXECUTE | msdb |
| | db_datawriter role | msdb |
| | db_datareader role | msdb |
| Availability Group Discovery | VIEW ANY DEFINITION | Server Level |
| Purview | SELECT | All databases |
| | EXECUTE | All databases |
| | CONNECT ANY DATABASE | Server Level |
| | VIEW ANY DATABASE | Server Level |
| Monitoring | SELECT dbo.sysjobactivity | msdb |
| | SELECT dbo.sysjobs | msdb |
| | SELECT dbo.syssessions | msdb |
| | SELECT dbo.sysjobHistory | msdb |
| | SELECT dbo.sysjobSteps | msdb |
| | SELECT dbo.syscategories | msdb |
| | SELECT dbo.sysoperators | msdb |
| | SELECT dbo.suspectpages | msdb |
| | SELECT dbo.backupset | msdb |
| | SELECT dbo.backupmediaset | msdb |
| | SELECT dbo.backupmediafamily | msdb |
| | SELECT dbo.backupfile | msdb |
| | CONNECT ANY DATABASE | Server Level |
| | VIEW ANY DATABASE | Server Level |
| | VIEW ANY DEFINITION | Server Level |
| Migration Assessment | EXECUTE dbo.agent_datetime | msdb |
| | SELECT dbo.syscategories | msdb |
| | SELECT dbo.sysjobHistory | msdb |
| | SELECT dbo.sysjobs | msdb |
| | SELECT dbo.sysjobSteps | msdb |
| | SELECT dbo.sysmail_account | msdb |
| | SELECT dbo.sysmail_profile | msdb |
| | SELECT dbo.sysmail_profileaccount | msdb |
| | SELECT dbo.syssubsystems | msdb |
| | SELECT sys.sql_expression_dependencies | All databases |

## Additional permissions

* Permissions to service account to access extension service and configure auto-recovery.
* Log-on-as-service rights to service account.

## Related content

[Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
