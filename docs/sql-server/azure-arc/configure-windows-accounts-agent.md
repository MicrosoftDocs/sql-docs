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

## Directory permissions

| Directory Path | Required Permissions | Details | Feature |
| :----- | :----- | :----- | :----- |
| `<SystemDrive>\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SQLServer` | Full Control | This directory stores all the extension related dlls and exe files. | Basic |
| Config folder from handler environment | Full Control | This folder contains extension settings file. | Basic |
| Status folder from handler environment | Full Control | This folder contains extension status file. | Basic |
| Log folder from handler environment | Full Control | This folder contains extension log files. | Basic |
| Heartbeat folder from handler environment | Full Control | This folder contains extension heartbeat file. | Basic |
| `%ProgramFiles%\Sql Server Extension` | Full Control | This folder contains extension service files. |  |
| `<SystemDrive>\Windows\system32\extensionUpload` | Full Control | Required to write usage file needed for billing. | Basic |
| `<SystemDrive>\Windows\system32\ExtensionHandler.log` | Full Control | This is a pre-log folder created by extension. | Basic |
| `<ProgramData>\AzureConnectedMachineAgent\Config` | Read | This is Arc config file directory which Arc proxy details. | Basic |
| `C:\Program Files\Microsoft SQL Server\MSSQL<base_version>.<instance_name>\MSSQL\log` | Read (will need to iterate over all the instances) | Required to extract SQL vcores info from SQL logs. | Basic  (Fetched from registry)| 
| `C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent` | Full Control (It needs to be created in deployer as well) | Required to write assessment reports and status. | Basic |
| `C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019NEW\MSSQL\Backup` | ReadAndExecute/Write /Delete | This is required for Backups | Backup  (Fetched from registry)| 

## Registry permissions

Base Key – `HKEY_LOCAL_MACHINE`

| Registry Key | Permission Required | Details | Feature |
| :----- | :----- | :----- | :----- |
| `SOFTWARE\Microsoft\Microsoft SQL Server` | Read | Read on SQL Server registry is needed to read SQL Server properties like installedInstances etc.  | Basic |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\MSSQLSERVER` | Full Control | Required for Microsoft Entra ID and Purview.</br></br>Full control is required for Microsoft Entra ID, because we are modifying ACLs for the Entra ID registry key on Entra ID enablement. | Microsoft Entra ID </br></br>Purview |
| `SOFTWARE\Microsoft\SystemCertificates` | Full Control | Full control is required for Microsoft Entra ID because we are granting permissions to SQL service account on Microsoft Entra ID certs. | Microsoft Entra ID |
| `SYSTEM\CurrentControlSet\Services` | Read | Required for fetching SQL Server account name. | Basic |
| `SOFTWARE\Microsoft\AzureDefender\SQL` | Read | Required for fetching Azure Defender status and last update time. | Basic |
|` SOFTWARE\Microsoft\SqlServerExtension` | Full Control | This is registry key for our extension and extension related values are stored in subkeys. | Basic |
| `SOFTWARE\Policies\Microsoft\Windows` | Read and Write | Required for enabling automatic windows update via extension. | Patching |

## Group permissions

`NT Service\SQLServerExtension` is added to Hybrid agent extension applications. Supports Azure Instance Metadata Service (IMDS) Handshake.

## SQL Permissions

`NT Service\SQLServerExtension` is added as SQL Login to all the instances present currently on machine.

| Feature | Permission | Level |
| :---- | :---- | :---- |
| Basic | VIEW DATABASE STATE | Server Level |
|  | VIEW SERVER STATE | Server Level |
|  | CONNECT SQL | Server Level |
| Database as a Resource | Default public role | Server level (This is granted by default to newly added logins) |
| Best practices assessment | VIEW ANY DEFINITION | Server Level |
|  | VIEW ANY DATABASE | Server Level |
|  | SELECT | master db and msdb |
|  | EXECUTE | On stored procedure (master.sys.xp_enumerrorlogs) and |
|  |  | (master.sys.xp_readerrorlog) |
| Backup | CREATE ANY DATABASE | Server level |
|  | db_backupoperator role | On each database level |
|  | dbcreator | Server role |
| Azure Control Plane | CREATE TABLE | msdb |
|  | ALTER ANY SCHEMA | msdb |
|  | CREATE TYPE | msdb |
|  | EXECUTE | msdb |
|  | db_datawriter role | msdb |
|  | db_datareader role | msdb |
| Availability Group Discovery | VIEW ANY DEFINITION | Server Level |
| Purview | SELECT | All databases |
|  | EXECUTE | All databases |
|  | CONNECT ANY DATABASE | Server Level |
|  | VIEW ANY DATABASE | Server Level |
| Monitoring | SELECT | msdb.dbo.sysjobactivity |
|  | SELECT | msdb.dbo.sysjobs |
|  | SELECT | msdb.dbo.syssessions |
|  | SELECT | msdb.dbo.sysjobHistory |
|  | SELECT | msdb.dbo.sysjobSteps |
|  | SELECT | msdb.dbo.syscategories |
|  | SELECT | msdb.dbo.sysoperators |
|  | SELECT | msdb.dbo.suspectpages |
|  | SELECT | msdb.dbo.backupset |
|  | SELECT | msdb.dbo.backupmediaset |
|  | SELECT | msdb.dbo.backupmediafamily |
|  | SELECT | msdb.dbo.backupfile |
|  | CONNECT ANY DATABASE | Server Level |
|  | VIEW ANY DATABASE | Server Level |
|  | VIEW ANY DEFINITION | Server Level |
| Migration Assessment | EXECUTE | msdb.dbo.agent_datetime |
|  | SELECT | msdb.dbo.syscategories |
|  | SELECT | msdb.dbo.sysjobHistory |
|  | SELECT | msdb.dbo.sysjobs |
|  | SELECT | msdb.dbo.sysjobSteps |
|  | SELECT | msdb-dbo.sysmail_account |
|  | SELECT | msdb-dbo.sysmail_profile |
|  | SELECT | msdb-dbo.sysmail_profileaccount |
|  | SELECT | msdb.dbo.syssubsystems |
|  | SELECT | all.sys_sql_expression_dependencies |

## Additional permissions

* Permissions to service account to access extension service and configure auto-recovery.
* Log-on-as-service rights to service account.

<!---------------------------------------------------------------------------------------------------------->

## Related content

[Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
