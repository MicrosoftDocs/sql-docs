---
title: Configure Windows Service Accounts and Permissions for SQL Server Enabled by Azure Arc
description: Get acquainted with permissions required to start and run Azure Extension for SQL Server agent service account. See how to configure them and assign appropriate permissions.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: safeitle, randolphwest
ms.date: 11/03/2025
ms.topic: reference
---

# Configure Windows service accounts and permissions for Azure extension for SQL Server

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article lists the permissions the Azure extension for SQL Server grants to the `NT Service\SQLServerExtension` account when you use [least privilege](configure-least-privilege.md) for [SQL Server instances enabled by Azure Arc](overview.md). With the least privilege configuration, the extension grants only necessary permissions when you enable features in the Azure portal. 

> [!NOTE]  
> `NT Authority\System` must have access to modify permissions on listed directories and registry keys. This access is necessary so that `NT Authority\System` can grant required access to the `NT Service\SqlServerExtension` account for least privilege mode.
>
> Additionally, `NT Authority\System` must have an active SQL Server login with CONNECT SQL permission on each SQL Server instance. The Deployer connects to SQL Server as `NT Authority\System` to configure all SQL‑level permissions described in this article. If this login has been disabled, removed, or has CONNECT SQL denied, the Deployer cannot configure SQL permissions in either standard or least‑privilege mode. See Prerequisites for verification steps.

## Overview

When you connect SQL Server to Azure Arc with [least privilege](configure-least-privilege.md) enabled, the Azure Arc extension grants its service account, `NT SERVICE\SQLServerExtension`, only the permissions each feature needs when you enable that feature. The extension automatically removes those permissions if you disable the feature. If a feature is inactive, the extension doesn't grant any permissions for that feature.

Manually setting the permissions for the agent account isn't supported.

> [!NOTE]  
> [!INCLUDE [least-privilege-default](includes/least-privilege-default.md)] 

The section [SQL privileges by feature](#sql-privileges-by-feature) explains the permissions the extension grants when you enable the following features: 

- [Default permissions for the extension](#default-extension-permissions)
- [Automated backups](#automated-backups)
- [Availability groups](#availability-groups)
- [Best practices assessment](#best-practices-assessment)
- [Migration assessment](#migration-assessment)
- [Database migration](#database-migration)
- [Point in time restore](#automated-backups)
- [Purview](#purview)


## Directory permissions

| Directory path | Required permissions | Details | Feature |
| :--- | :--- | :--- | :--- |
| `<SystemDrive>\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SQLServer` | Full control | Extension related DLLs and EXE files. | Default |
| `C:\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SqlServer\<extension_version>\RuntimeSettings` | Full control | Extension settings file. | Default |
| `C:\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SqlServer\<extension_version>\status` | Full control | Extension status file. | Default |
| `C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer` | Full control | Extension log files. | Default |
| `C:\Packages\Plugins\Microsoft.AzureData.WindowsAgent.SqlServer\<extension_version>\status\HeartBeat.Json` | Full control | Extension heartbeat file. | Default |
| `%ProgramFiles%\Sql Server Extension` | Full control | Extension service files. | Default |
| `<SystemDrive>\Windows\system32\extensionUpload` | Full control | Required to write usage file needed for billing. | Default |
| `<SystemDrive>\Windows\system32\ExtensionHandler.log` | Full control | Pre-log folder created by extension. | Default |
| `<ProgramData>\AzureConnectedMachineAgent\Config` | Read | Arc config files directory. | Default |
| `C:\Windows\System32\config\systemprofile\AppData\Local\Microsoft SQL Server Extension Agent` | Full control | Required to write assessment reports and status. | Default |
| SQL log directory (as set in registry) <sup>1</sup> :<br />`C:\Program Files\Microsoft SQL Server\MSSQL<base_version>.<instance_name>\MSSQL\log` | Read | Required to extract SQL vCores info from SQL logs. | Default |
| SQL backup directory (as set in registry) <sup>1</sup> :<br />`C:\Program Files\Microsoft SQL Server\MSSQL<base_version>.<instance_name>\MSSQL\Backup` | ReadAndExecute/Write /Delete | Required for Backups | Backup |

<sup>1</sup> For more information, see [File Locations and Registry Mapping](../install/file-locations-for-default-and-named-instances-of-sql-server.md#file-locations-and-registry-mapping).

## Registry permissions

Base key: `HKEY_LOCAL_MACHINE`

| Registry key | Required permission | Details | Feature |
| :--- | :--- | :--- | :--- |
| `SOFTWARE\Microsoft\Microsoft SQL Server` | Read | Read SQL Server properties like `installedInstances`. | Default |
| `SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceRegistryName>\MSSQLSERVER` | Full control | Microsoft Entra ID and Purview. | Microsoft Entra ID<br /><br />Purview |
| `SOFTWARE\Microsoft\SystemCertificates` | Full control | Required for Microsoft Entra ID. | Microsoft Entra ID |
| `SYSTEM\CurrentControlSet\Services` | Read | SQL Server account name. | Default |
| `SOFTWARE\Microsoft\AzureDefender\SQL` | Read | Azure Defender status and last update time. | Default |
| `SOFTWARE\Microsoft\SqlServerExtension` | Full control | Extension related values. | Default |
| `SOFTWARE\Policies\Microsoft\Windows` | Read and Write | Enabling automatic windows update via extension. | Automatic updates |

## Group permissions

`NT Service\SQLServerExtension` is added to Hybrid agent extension applications. This enables the Azure Instance Metadata Service (IMDS) handshake to retrieve the Machine resource managed identity token required to communicate to Azure data plane services such as the Data Processing Service (DPS) and the telemetry endpoint for billing usage, extension logs, and monitoring dashboard data collection.

## SQL permissions

The `NT Service\SQLServerExtension` account is added:

- As a SQL login to all the instances currently present on the machine
- As a user in each database

The extension also grants permissions to instance and database objects as features are enabled.

> [!NOTE]  
> Minimum permissions depend on enabled features. The extension updates permissions when they're no longer necessary. It grants necessary permissions when you enable features.

### `NT Service\SQLServerExtension` account permission details

| Registry Path | Permission | The associated risk on permissions if the `NT Service\SQLServerExtension` account is compromised |
| :--- | :--- | :--- |
| `SOFTWARE\Microsoft\Microsoft SQL Server` | Read | Extension can see which SQL Server versions are installed. |
| `SOFTWARE\Microsoft\Microsoft SQL Server\\MSSQLSERVER` | Full control | Only needed when Microsoft Entra authentication or Purview is enabled. Extension could modify SQL Server configuration. |
| `SOFTWARE\Microsoft\SystemCertificates` | Full control | Only needed when Microsoft Entra authentication is enabled. Extension could replace trusted root certificate authorities. |
| `SYSTEM\CurrentControlSet\Services` | Read | Extension can see service account names. |
| `SOFTWARE\Microsoft\AzureDefender\SQL` | Read | Extension can learn Microsoft Defender status and update times. |
| `SOFTWARE\Microsoft\SqlServerExtension` | Full control | Extension could change extension settings. |
| `SOFTWARE\Policies\Microsoft\Windows` | Read and Write | Only needed when [Auto update](update.md) is enabled. Extension could change Windows Update policies and disable Device Guard, which controls code integrity and virtualization-based security, extended exposure due to missed patches. |

## SQL privileges by feature

The following table lists the default behavior for the features that control permissions granted by the Azure Extension for SQL Server:

| Feature | Default behavior |
|--|--|
| [Default extension permissions](#default-extension-permissions) | Enabled by default |
| [Automated backups](#automated-backups) | Disabled by default |
| [Availability groups](#availability-groups) | Enabled by default |
| [Best practices assessment](#best-practices-assessment) | Disabled by default |
| [Migration assessment](#migration-assessment) | Enabled by default |
| [Database migration](#database-migration) | Enabled by default |
| [Point in time restore](#automated-backups) | Disabled by default |
| [Purview](#purview) | Disabled by default |

### Default extension permissions

The following default permissions are the minimum requirement for the basic level of functionality provided by the Azure Extension for SQL Server and must be applied: 

| Object type | Database or object name | Privilege |
| --- | --- | --- |
| Database | `master` | `VIEW DATABASE STATE` |
| Database | `msdb` | `ALTER ANY SCHEMA` |
| Database | `msdb` | `CREATE TABLE` |
| Database | `msdb` | `CREATE TYPE` |
| Database | `msdb` | `DB DATA READER` |
| Database | `msdb` | `DB DATA WRITER` |
| Database | `msdb` | `EXECUTE` |
| Database | `msdb` | `SELECT dbo.backupfile` |
| Database | `msdb` | `SELECT dbo.backupmediaset` |
| Database | `msdb` | `SELECT dbo.backupmediafamily` |
| Database | `msdb` | `SELECT dbo.backupset` |
| Database | `msdb` | `SELECT dbo.syscategories` |
| Database | `msdb` | `SELECT dbo.sysjobactivity` |
| Database | `msdb` | `SELECT dbo.sysjobhistory` |
| Database | `msdb` | `SELECT dbo.sysjobs` |
| Database | `msdb` | `SELECT dbo.sysjobsteps` |
| Database | `msdb` | `SELECT dbo.syssessions` |
| Database | `msdb` | `SELECT dbo.sysoperators` |
| Database | `msdb` | `SELECT dbo.suspectpages` |
| Server | | `CONNECT ANY DATABASE` |
| Server | | `CONNECT SQL` |
| Server | | `VIEW ANY DATABASE` |
| Server | | `VIEW ANY DEFINITION` |
| Server | | `VIEW SERVER STATE` |

### Automated backups

[Automated backups](backup-local.md) are disabled by default. The extension grants backup permissions to any database that has automated backups enabled. Enabling the backup feature also enables the [point-in-time restore](point-in-time-restore.md) feature, so the permission to create a database is also granted.

If the features are enabled, the extension automatically grants the following permissions: 

| Object type | Database or object name | Privilege |
| --- | --- | --- |
| Database | All databases | `DB BACKUP OPERATOR` |
| Server | | `CREATE ANY DATABASE` |
| Server | `master` | `DB CREATOR` |

### Availability groups

[Availability group](manage-availability-group.md) discovery and management features such as failing over are enabled by default, but you can disable them through the `AvailabilityGroupDiscovery` feature flag.

If the feature is enabled, the extension automatically grants the following permissions:

| Object type | Database or object name | Privilege |
| --- | --- | --- |
| Server | | `ALTER ANY AVAILABILITY GROUP` |
| Server | | `VIEW ANY DEFINITION` |

### Best practices assessment

The [best practices assessment](assess.md) is disabled by default. 

If the feature is enabled, the extension automatically grants the following permissions: 

| Object type | Database or object name | Privilege |
| --- | --- | --- |
| Database | `master` | `SELECT` |
| Database | `master` | `VIEW DATABASE STATE` |
| Database | `msdb` | `SELECT` |
| Server | | `VIEW ANY DATABASE` |
| Server | | `VIEW ANY DEFINITION` |
| Server | | `VIEW SERVER STATE` |
| StoredProcedure | EnumErrorLogsSP | `EXECUTE` |
| StoredProcedure | ReadErrorLogsSP | `EXECUTE` |

### Database migration 

The [Database migration](migrate-to-azure-sql-managed-instance.md) feature is enabled by default, and only requires the permissions listed in [default extension permissions](#default-extension-permissions), though some permissions used by the Database migration feature are granted [just-in-time permissions](#just-in-time-sql-permissions) when a specific migration action is performed.

The following actions require additional permissions that the extension grants just-in-time:
- [Create Managed Instance link migration](#create-managed-instance-link-migration)
- [Complete cutover of Managed Instance link migration](#complete-cutover-of-managed-instance-link-migration)
- [Cancel Managed Instance link migration](#cancel-managed-instance-link-migration)

> [!NOTE]
> Users with the `SqlServerAvailabilityGroups_CreateManagedInstanceLink`, `SqlServerAvailabilityGroups_failoverMiLink`, and `SqlServerAvailabilityGroups_deleteMiLink` permissions in Azure can perform actions on the [Database migration](migrate-to-azure-sql-managed-instance.md) page during the migration process that elevate the SQL Server permissions of the account used by the extension, including the `sysadmin` role. 

### Create Managed Instance link migration

On the [Migrate data](migrate-to-azure-sql-managed-instance.md#migrate-data) step, the extension grants [just-in-time permissions](#just-in-time-sql-permissions) when you select **Start data migration** on the **Review + Create** tab for a Managed Instance link migration. The service account needs elevated permissions to configure the distributed availability group. It revokes permissions once the distributed availability group is created, and the deployment visible in the Azure portal is in the completed state. If another migration is running at the same time, the extension doesn't revoke permissions until the last distributed availability group is created.

The action to create a Managed Instance link migration acquires the following permissions for the duration of the create request:

| Object type | Database or object name | Privilege                      |
| ----------- | ---------------------- | ------------------------------ |
| Server      |                          | `CREATE AVAILABILITY GROUP`    |
| Server      |                          | `ALTER ANY AVAILABILITY GROUP` |
| Server      |                          | `ALTER ANY DATABASE`           |
| Server      |                          | `CREATE ENDPOINT`              |
| Server      |                          | `ALTER ANY ENDPOINT`           |
| Server      |                          | `CREATE CERTIFICATE`           |
| Database    | `master`               | `IMPERSONATE ON USER::[dbo]`   |

### Complete cutover of Managed Instance link migration

On the [Monitor and cutover](migrate-to-azure-sql-managed-instance.md#monitor-and-cutover) step, the extension grants [just-in-time permissions](#just-in-time-sql-permissions) when you select the **Complete cutover** option for a Managed Instance link migration. The extension revokes permissions after the cutover completes.

The action to complete cutover of a Managed Instance link migration acquires the following permissions for the duration of the complete request: 

| Object type | Database or object name | Privilege |
| --- | --- | --- |
| Server | | `CREATE AVAILABILITY GROUP` |
| Server | | `ALTER ANY AVAILABILITY GROUP` |
| Server | | `ALTER ANY DATABASE` |
| Server | | `sysadmin`<sup>1</sup> |

<sup>1</sup> If least privilege is enabled, the complete cutover action also grants the `sysadmin` role to the `NT Service\SQLServerExtension` account for the duration of the cutover. This role is required to fail over the distributed availability group for cutover to the Azure SQL Managed Instance.

### Cancel Managed Instance link migration

On the [Monitor and cutover step](migrate-to-azure-sql-managed-instance.md#monitor-and-cutover), the extension grants [just-in-time permissions](#just-in-time-sql-permissions) when you select the **Cancel migration** option for a Managed Instance link migration. The extension revokes permissions after the migration is canceled.

The action to cancel a Managed Instance link migration acquires the following permissions for the duration of the cancel request: 

| Object type | Database or object name | Privilege |
| --- | --- | --- |
| Server | | `ALTER ANY AVAILABILITY GROUP` |
| Server | | `ALTER ANY DATABASE` |
| Server | | `sysadmin`<sup>1</sup> |

<sup>1</sup> If least privilege is enabled, the cancel action also grants the `sysadmin` role to the `NT Service\SQLServerExtension` account for the duration of the cancel request. This role is required when deleting a distributed availability group.

### Migration assessment

[Migration assessments](migration-assessment.md) are enabled by default. 

If the feature is disabled, the extension revokes the following permissions unless other enabled features require them:
    
| Object type | Database or object name | Privilege |
| --- | --- | --- |
| Database | All databases | `SELECT sys.sqlexpressiondependencies` |
| Database | `msdb` | `EXECUTE dbo.agentdatetime` |
| Database | `msdb` | `SELECT dbo.syscategories` |
| Database | `msdb` | `SELECT dbo.sysjobhistory` |
| Database | `msdb` | `SELECT dbo.sysjobs` |
| Database | `msdb` | `SELECT dbo.sysjobsteps` |
| Database | `msdb` | `SELECT dbo.sysmailaccount` |
| Database | `msdb` | `SELECT dbo.sysmailprofile` |
| Database | `msdb` | `SELECT dbo.sysmailprofileaccount` |
| Database | `msdb` | `SELECT dbo.syssubsystems` |



### Purview

The [Purview](/purview/register-scan-azure-arc-enabled-sql-server) features are disabled by default.

If the feature is enabled, the extension automatically grants the following permissions:

| Object type | Database or object name | Privilege |
| --- | --- | --- |
| Database | All databases | `EXECUTE` |
| Database | All databases | `SELECT` |
| Server | | `CONNECT ANY DATABASE` |
| Server | | `VIEW ANY DATABASE` |




## Just-in-time SQL permissions

Some SQL permissions are only assigned at the time they are needed to perform a specific action, and are revoked as soon as the operation that requires the permissions completes. If the revocation fails to execute, a background cleanup job that runs every 50 minutes automatically revokes permissions that have become stale.

Just-in-time permissions are assigned to the service account: 
- `NT Service\SQLServerExtension` if least privilege is enabled
- Local System account if least privilege is disabled.

Currently, the following feature uses just-in-time permissions:
- [Database migration](#database-migration) when using the Managed Instance link migration option. 

## Additional permissions

- Permissions to service account to access extension service and configure autorecovery.
- Log-on-as-service rights to service account.

## Related content

- [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)
