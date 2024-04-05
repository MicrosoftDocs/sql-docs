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

This article lists the permissions Azure extension for SQL Server requires for SQL Server enabled by Azure Arc.

Azure extension for SQL Server sets permissions when you enable features on the Azure portal. These permissions are set automatically, when you configure least privilege. If you don't enable features, the extension does not set the permissions.

[SQL permissions](#sql-permissions) lists the permissions tied to features that the extension grants when features are enabled.

To configure least privilege, follow the steps in [Operate SQL Server enabled by Azure Arc with least privilege (preview)](configure-least-privilege.md).

We don't recommend that you manually set the permissions described in this article.

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
| `SOFTWARE\Policies\Microsoft\Windows` | Read and Write | Enabling automatic windows update via extension. | Patching |

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
| **Azure Control Plane** | `CREATE TABLE` | `msdb` | Feature dependent |
| | `ALTER ANY SCHEMA` | `msdb` | Feature dependent |
| | `CREATE TYPE` | `msdb` | Feature dependent |
| | `EXECUTE` | `msdb` | Feature dependent |
| | **db_datawriter** role | `msdb` | Feature dependent |
| | **db_datareader** role | `msdb` | Feature dependent |
| **Availability group discovery** | `VIEW ANY DEFINITION` | Server level | Feature dependent |
| **Purview** | `SELECT` | All databases | Feature dependent |
| | `EXECUTE` | All databases | Feature dependent |
| | `CONNECT ANY DATABASE` | Server level | Feature dependent |
| | `VIEW ANY DATABASE` | Server level | Feature dependent |
| **Monitoring** | `SELECT dbo.sysjobactivity` | `msdb` | Feature dependent |
| | `SELECT dbo.sysjobs` | `msdb` | Feature dependent |
| | `SELECT dbo.syssessions` | `msdb` | Feature dependent |
| | `SELECT dbo.sysjobHistory` | `msdb` | Feature dependent |
| | `SELECT dbo.sysjobSteps` | `msdb` | Feature dependent |
| | `SELECT dbo.syscategories` | `msdb` | Feature dependent |
| | `SELECT dbo.sysoperators` | `msdb` | Feature dependent |
| | `SELECT dbo.suspectpages` | `msdb` | Feature dependent |
| | `SELECT dbo.backupset` | `msdb` | Feature dependent |
| | `SELECT dbo.backupmediaset` | `msdb` | Feature dependent |
| | `SELECT dbo.backupmediafamily` | `msdb` | Feature dependent |
| | `SELECT dbo.backupfile` | `msdb` | Feature dependent |
| | `CONNECT ANY DATABASE` | Server level | Feature dependent |
| | `VIEW ANY DATABASE` | Server level | Feature dependent |
| | `VIEW ANY DEFINITION` | Server level | Feature dependent |
| **Migration Assessment** | `EXECUTE dbo.agent_datetime` | `msdb` | Feature dependent |
| | `SELECT dbo.syscategories` | `msdb` | Feature dependent |
| | `SELECT dbo.sysjobHistory` | `msdb` | Feature dependent |
| | `SELECT dbo.sysjobs` | `msdb` | Feature dependent |
| | `SELECT dbo.sysjobSteps` | `msdb` | Feature dependent |
| | `SELECT dbo.sysmail_account` | `msdb` | Feature dependent |
| | `SELECT dbo.sysmail_profile` | `msdb` | Feature dependent |
| | `SELECT dbo.sysmail_profileaccount` | `msdb` | Feature dependent |
| | `SELECT dbo.syssubsystems` | `msdb` | Feature dependent |
| | `SELECT sys.sql_expression_dependencies` | All databases |

> [!NOTE]  
> Minimum permissions depend on enabled features. Permissions are updated when they are no longer necessary. Necessary permissions are granted when features are enabled.

## Additional permissions

- Permissions to service account to access extension service and configure autorecovery.
- Log-on-as-service rights to service account.

## Related content

- [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
