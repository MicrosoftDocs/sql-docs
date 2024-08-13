---
title: Configure Windows service accounts and permissions for SQL Server enabled by Azure Arc
description: Get acquainted with permissions required to start and run Azure Extension for SQL Server agent service account. See how to configure them and assign appropriate permissions.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: nikitatakru, randolphwest
ms.date: 03/28/2024
ms.topic: reference
---

# Configure Windows service accounts and permissions for Azure extension for SQL Server

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article lists the permissions Azure extension for SQL Server sets for the `NT Service\SQLServerExtension` account. This account is used when you [Operate SQL Server enabled by Azure Arc with least privilege](configure-least-privilege.md).

> [!NOTE]
> [!INCLUDE [least-privilege-default](includes/least-privilege-default.md)]

Manually setting the permissions for the agent account is not supported.

The extension sets permissions when you enable features on the Azure portal. If you don't enable a feature, the extension does not set the permissions for that feature. If you disable a feature, the extension removes the permissions.

[SQL permissions](#sql-permissions) lists the permissions tied to features that the extension grants when features are enabled.

Note: NT Authority\System must have access to modify permissions on listed directories and registry keys. This is needed so that NT Authority\System can grant required access to NT Service\SqlServerExtension account for least privileges mode.

## Directory permissions

| Directory path | Required permissions | Details | Feature |
| :--- | :--- | :--- | :--- |
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
| SQL log directory (as set in registry) <sup>1</sup>:<br />`C:\Program Files\Microsoft SQL Server\MSSQL<base_version>.<instance_name>\MSSQL\log` | Read | Required to extract SQL vCores info from SQL logs. | Default |
| SQL backup directory (as set in registry) <sup>1</sup>:<br />`C:\Program Files\Microsoft SQL Server\MSSQL<base_version>.<instance_name>\MSSQL\Backup` | ReadAndExecute/Write /Delete | Required for Backups | Backup |

<sup>1</sup> For more information, see [File Locations and Registry Mapping](../install/file-locations-for-default-and-named-instances-of-sql-server.md#file-locations-and-registry-mapping).

## Registry permissions

Base key: `HKEY_LOCAL_MACHINE`

| Registry key | Permission required | Details | Feature |
| :--- | :--- | :--- | :--- |
| `SOFTWARE\Microsoft\Microsoft SQL Server` | Read | Read SQL Server properties like `installedInstances`. | Default |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\MSSQLSERVER` | Full control | Microsoft Entra ID and Purview. | Microsoft Entra ID<br /><br />Purview |
| `SOFTWARE\Microsoft\SystemCertificates` | Full control | Required for Microsoft Entra ID. | Microsoft Entra ID |
| `SYSTEM\CurrentControlSet\Services` | Read | SQL Server account name. | Default |
| `SOFTWARE\Microsoft\AzureDefender\SQL` | Read | Azure Defender status and last update time. | Default |
| `SOFTWARE\Microsoft\SqlServerExtension` | Full control | Extension related values. | Default |
| `SOFTWARE\Policies\Microsoft\Windows` | Read and Write | Enabling automatic windows update via extension. | Automatic updates |

## Group permissions

`NT Service\SQLServerExtension` is added to Hybrid agent extension applications. Supports Azure Instance Metadata Service (IMDS) Handshake.

## SQL permissions

`NT Service\SQLServerExtension` is added:

- As a SQL login to all the instances present currently on machine
- As a user in each database

The extension also grants permissions to instance and database objects as features are enabled. The table below provides details.

| Feature | Permission | Level | Requirement |
| --- | --- | --- | --- |
| **Default** | `VIEW DATABASE STATE` | Server level | Essential |
| | `VIEW SERVER STATE` | Server level | Essential |
| | `CONNECT SQL` | Server level | Essential |
| **Database as a resource** | Default public role | Server level (This is granted by default to newly added logins) | Essential |
| **Best practices assessment** | `VIEW ANY DEFINITION` | Server level | Feature dependent |
| | `VIEW ANY DATABASE` | Server level | Feature dependent |
| | `SELECT` | `master` | Feature dependent |
| | `SELECT` | `msdb` | Feature dependent |
| | `EXECUTE ON sys.xp_enumerrorlogs` | `master` | Feature dependent |
| | `EXECUTE ON sys.xp_readerrorlog` | `master` | Feature dependent |
| **Backup** | `CREATE ANY DATABASE` | Server level | Feature dependent |
| | **db_backupoperator** role | All databases | Feature dependent |
| | **dbcreator** | Server role | Feature dependent |
| **Azure Control Plane** | `CREATE TABLE` | `msdb` | Essential |
| | `ALTER ANY SCHEMA` | `msdb` | Essential |
| | `CREATE TYPE` | `msdb` | Essential |
| | `EXECUTE` | `msdb` | Essential |
| | **db_datawriter** role | `msdb` | Feature dependent |
| | **db_datareader** role | `msdb` | Feature dependent |
| **Availability group discovery** | `VIEW ANY DEFINITION` | Server level | Essential |
| **Purview** | `SELECT` | All databases | Feature dependent |
| | `EXECUTE` | All databases | Feature dependent |
| | `CONNECT ANY DATABASE` | Server level | Feature dependent |
| | `VIEW ANY DATABASE` | Server level | Feature dependent |
| **Monitoring** | `SELECT dbo.sysjobactivity` | `msdb` | Essential |
| | `SELECT dbo.sysjobs` | `msdb` | Essential |
| | `SELECT dbo.syssessions` | `msdb` | Essential |
| | `SELECT dbo.sysjobHistory` | `msdb` | Essential |
| | `SELECT dbo.sysjobSteps` | `msdb` | Essential |
| | `SELECT dbo.syscategories` | `msdb` | Essential |
| | `SELECT dbo.sysoperators` | `msdb` | Essential |
| | `SELECT dbo.suspectpages` | `msdb` | Essential |
| | `SELECT dbo.backupset` | `msdb` | Essential |
| | `SELECT dbo.backupmediaset` | `msdb` | Essential |
| | `SELECT dbo.backupmediafamily` | `msdb` | Essential |
| | `SELECT dbo.backupfile` | `msdb` | Essential |
| | `CONNECT ANY DATABASE` | Server level | Essential |
| | `VIEW ANY DATABASE` | Server level | Essential |
| | `VIEW ANY DEFINITION` | Server level | Essential |
| **Migration Assessment** | `EXECUTE dbo.agent_datetime` | `msdb` | Essential |
| | `SELECT dbo.syscategories` | `msdb` | Essential |
| | `SELECT dbo.sysjobHistory` | `msdb` | Essential |
| | `SELECT dbo.sysjobs` | `msdb` | Essential |
| | `SELECT dbo.sysjobSteps` | `msdb` | Essential |
| | `SELECT dbo.sysmail_account` | `msdb` | Essential |
| | `SELECT dbo.sysmail_profile` | `msdb` | Essential |
| | `SELECT dbo.sysmail_profileaccount` | `msdb` | Essential |
| | `SELECT dbo.syssubsystems` | `msdb` | Essential |
| | `SELECT sys.sql_expression_dependencies` | All databases | Essential |

> [!NOTE]  
> Minimum permissions depend on enabled features. Permissions are updated when they are no longer necessary. Necessary permissions are granted when features are enabled.

## Additional permissions

- Permissions to service account to access extension service and configure autorecovery.
- Log-on-as-service rights to service account.

## Related content

- [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
