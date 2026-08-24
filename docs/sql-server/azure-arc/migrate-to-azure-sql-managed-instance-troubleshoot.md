---
title: Troubleshoot Issues with Migration to Azure SQL MI
titleSuffix: SQL Server migration in Azure Arc
description: Learn how to troubleshoot common issues when migrating SQL Server databases to Azure SQL Managed Instance using SQL Server migration in Azure Arc.
author: danimir
ms.author: danil
ms.reviewer: randolphwest, mathoma
ms.date: 06/25/2026
ms.topic: how-to
---
# Troubleshoot issues when migrating to Azure SQL Managed Instance

This article helps you troubleshoot common issues you might encounter when migrating SQL Server databases to Azure SQL Managed Instance by using SQL Server migration in Azure Arc.

> [!NOTE]  
> You can provide feedback about your migration experience [directly to the product group](https://aka.ms/arc-migrations-feedback).

## Arc agent version

When you use SQL Server migration in Azure Arc, certain features require a minimum version of the Arc agent. The Arc agent is an executable that runs alongside your SQL Server instance to provide connectivity to Azure. Always keep your Arc agent version up to date to get the latest fixes and updates.

With [automatic updates](update.md) enabled, the Arc agent stays up to date automatically. However, when a new version of the Arc agent rolls out, it can take a few days for the update to reach all servers. You can speed up the process by [manually executing an on-demand Arc agent update](manage-configuration.md#upgrade-the-extension) through either the Azure portal or command line interfaces.

If you see the following error when accessing the **Database migration** pane in the Azure portal, you need to upgrade your Arc agent to a supported version:

```error-text
To enable migration and monitoring capabilities, 
please update your Azure Arc agent extension "WindowsAgentSQLServer" to the latest version.
```

## Arc agent issues

If you encounter issues with the Arc agent, such as an unhealthy extension state or a disconnected SQL Server instance, use the following extension troubleshooting guide: [Troubleshoot Azure extension for SQL Server](troubleshoot-extension.md).

## Migration readiness assessment issues

The system runs [migration readiness assessments](migration-assessment.md) every Sunday at 11 PM (23:00) local to the server. Assessments for SQL Server instances newly enabled by Azure Arc can take several days to appear in the Azure portal. Use **Run assessment** to trigger an on-demand assessment. The assessment appears after a few minutes.

If the database migration readiness assessments page is blank in the Azure portal, the scheduled assessment likely didn't run, or there was a problem running the assessment on the SQL Server instance. Disabling the Arc agent prevents assessments from running. Ensure the Arc agent is enabled. For more information, see [Change assessment settings](migration-assessment.md#change-assessment-settings).

Consider the following known issue:
- When `xp_cmdshell` is enabled and used, the assessment records a warning for SQL Managed Instance because you can still migrate the database. However, it disrupts the functionality of the object that specifically uses `xp_cmdshell`. Use the remediation guidance provided in the assessment to mitigate the issue.

Contact [Microsoft Support](/azure/azure-portal/supportability/how-to-create-azure-support-request) if you run into any of the following issues:

- The assessment reports don't appear in the portal even after the scheduled time.
- Performance data availability doesn't increase after one week of gathering data.

## View Azure activity log for migration issues

The activity log in the Azure portal, when accessed from a resource, provides insight into resource-level events that occur in Azure. This insight includes information about when you modify or delete resources, as well as details about service health and other important events.

When troubleshooting migration issues to Azure SQL Managed Instance by using SQL Server migration in Azure Arc, the Activity log is a valuable resource to identify problems and understand the sequence of events that led up to an issue.

To access the activity log in the Azure portal for your SQL Server instance enabled by Azure Arc resource, follow these steps:
1. Go to your [SQL Server instance enabled by Azure Arc resource](https://portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerInstances) in the Azure portal.
1. Select **Activity log** from the resource menu:

   :::image type="content" source="media/migrate-to-azure-sql-managed-instance-troubleshoot/activity-log.png" alt-text="Screenshot of the activity log highlighted for a SQL Server instance resource in the Azure portal.":::

You can also access the subscription-level activity log for a broader view of events across all resources in your subscription by selecting the notification bell icon of the top navigation bar and then selecting **More events in the activity log**:

:::image type="content" source="media/migrate-to-azure-sql-managed-instance-troubleshoot/notification-bell.png" alt-text="Screenshot of the notification bell icon highlighted in the Azure portal.":::

Select an event from the activity log to open a pane of event details. Use the **Summary** and **JSON** tabs to view detailed information about the event, including particular error messages. If you create a support request, communicate this information with as much detail as possible.

## New databases unavailable in the Azure portal

Recently added databases to your SQL Server instance might not be immediately visible in the Azure portal when trying to select databases for migration. This is because it takes about an hour for the Arc agent to auto-refresh the database list. 

To work around this issue, you can restart the Arc service to trigger an immediate refresh of the database list. 

On Windows, use the following command in an elevated command prompt on the server that hosts your SQL Server instance:

```cmd
Restart-Service himds
Restart-Service gcarcservice
Restart-Service extensionservice
```

Wait for the services to restart, and then use the following command to verify the service is running with the following command:

```cmd
& "$env:ProgramW6432\AzureConnectedMachineAgent\azcmagent.exe" show
```

On Linux servers, use the following command in an elevated terminal:

```bash
sudo systemctl restart himdsd
sudo systemctl restart gcad
sudo systemctl restart extd
```

Wait for the services to restart, and then use the following command to verify the service is running with the following command:

```bash
azcmagent show
```

Go to the **Databases** page in the Azure portal for your [SQL Server instance](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/SqlServerInstance), and select **Refresh** to see the newly added databases. You can now select these new databases for migration.

## Managed Instance link migration issues

This section describes some of the common issues with the Managed Instance link feature when migrating to Azure SQL Managed Instance through SQL Server migration in Azure Arc:

- [Incorrect service pack installed](#incorrect-service-pack-installed)
- [Always On availability group feature disabled](#always-on-availability-group-feature-disabled)
- [Using SQL Server 2016](#using-sql-server-2016)
- [Network connectivity issues](#network-connectivity-issues)
- [Warnings when starting Managed Instance link migration job](#warnings-when-starting-managed-instance-link-migration-job)
- [Database becomes unavailable after server restart following MI link failover](#database-becomes-unavailable-after-server-restart-following-mi-link-failover)
- [Known interoperability issue with existing links](#known-interoperability-issue-with-existing-links)
- [Detailed troubleshooting with XE Profiler](#detailed-troubleshooting-with-xe-profiler)

### Incorrect service pack installed

Make sure you have the [appropriate](migration-sql-mi-prepare-link.md#supported-sql-server-versions) SQL Server service pack (SP) or cumulative update (CU) installed.

You can check for the correct version by running the following T-SQL command on your SQL Server instance:

```sql
EXEC sp_certificate_add_issuer @CERTID, N'*.database.windows.net'
```

If you get the error that SQL Server can't find the stored procedure `sp_certificate_add_issuer`, you likely don't have the proper servicing pack installed (such as the Azure Connect Feature pack). Install the necessary servicing updates and try again.

### Always On availability group feature disabled

Ensure that the [Always On availability group feature is enabled](migration-sql-mi-prepare-link.md#enable-availability-groups) on your SQL Server instance. The Managed Instance link requires the Always On availability group feature to be enabled for proper functionality.

### Using SQL Server 2016

For [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you must complete the extra steps documented in [Prepare SQL Server 2016 prerequisites for the link](/azure/azure-sql/managed-instance/managed-instance-link-preparation-wsfc). These extra steps aren't required for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions supported by the link.

### Network connectivity issues

Successful connectivity between your SQL Server environment and Azure SQL Managed Instance is essential for the Managed Instance link feature to work. If you're having networking connectivity issues, consider the following points:

- The Managed Instance link doesn't work over a public network so the connection between your SQL Server instance and Azure SQL Managed Instance must be private, such as by using a VPN.
- You can test connectivity directly from the [Azure portal as part of the migration process](migration-sql-mi-prepare-link.md#test-network-connectivity). If the connection test succeeds in the portal but the link can't be created, check the [Activity log](#view-azure-activity-log-for-migration-issues) for details of the failure. You can also [test connectivity manually](/azure/azure-sql/managed-instance/managed-instance-link-preparation#test-network-connectivity) by using Transact-SQL and the SQL Server Agent.
- Check for any corporate firewalls on your network. Although network connectivity can appear to work, it's possible for firewalls to block specific type of packets that SQL Server uses for distributed availability groups. Verify that firewalls aren't blocking or filtering packet types.

### Warnings when starting Managed Instance link migration job

The following warnings can appear when starting the Managed Instance link migration job. You can proceed with the migration despite these warnings:
- `Warning: Database Mirroring Endpoint does not exist`
- `Warning: Database Mirroring Endpoint is not secure with a certificate`
- `Warning: Database Mirroring Endpoint is not enabled`
- `Warning: Database Mirroring Endpoint encryption algorithm is not set to AES`

These warnings are currently a known issue, and the migration process addresses them automatically so you can proceed with the migration.

Investigate other warnings. Some warnings might require resolution on your part before you can start the migration, while some can be addressed after the migration completes.

### Database becomes unavailable after server restart following MI link failover

Your database becomes unavailable on SQL Managed Instance after a server restart following the failover of the link. This known issue occurs when you drop a link before Azure completes a full backup of the database after initial failover to SQL Managed Instance. 

The following sequence of events leads to this issue:
1. You establish a link between SQL Server and SQL Managed Instance. 
1. You fail over the link to SQL Managed Instance, so it becomes the primary replica. 
1. You drop the link - without knowing that Azure didn't complete the first full backup of the database after failover, which is necessary to ensure the database is healthy and fully functional on SQL Managed Instance.
1. The database is initially accessible and available.
1. The server is restarted, such as for maintenance, a planned failover, or due to an unexpected outage.
1. The database becomes unavailable on SQL Managed Instance after the restart.

To avoid this problem, wait for Azure to complete the first full backup of the database after failover before dropping the link. You can check the status of the backup by querying the `backupset` table in the `msdb` database on SQL Managed Instance by using the following T-SQL query:

```sql
SELECT TOP (100)
    DB_NAME(DB_ID(bs.database_name)) AS [Database Name],
    CONVERT (BIGINT, bs.backup_size / 1048576) AS [Uncompressed Backup Size (MB)],
    CONVERT (BIGINT, bs.compressed_backup_size / 1048576) AS [Compressed Backup Size (MB)],
    CONVERT (NUMERIC (20, 2),
    CASE
        WHEN bs.compressed_backup_size > 0
        THEN CONVERT (FLOAT, bs.backup_size) / CONVERT (FLOAT, bs.compressed_backup_size)
        ELSE NULL
    END
    ) AS [Compression Ratio],
    bs.is_copy_only,
    -- bs.user_name, -- Applicable only for user-initiated COPY ONLY backups.
    bs.has_backup_checksums,
    DATEDIFF(SECOND, bs.backup_start_date, bs.backup_finish_date) AS [Backup Elapsed Time (sec)],
    bs.backup_finish_date AS [Backup Finish Date],
    bmf.physical_block_size
FROM msdb.dbo.backupset AS bs WITH (NOLOCK)
     INNER JOIN msdb.dbo.backupmediafamily AS bmf WITH (NOLOCK)
         ON bs.media_set_id = bmf.media_set_id
WHERE bs.[type] = 'D'
    -- AND bs.[is_copy_only] = 1  -- If you want to filter out for user initiated COPY ONLY backups.
ORDER BY bs.backup_finish_date DESC
OPTION (RECOMPILE); -- Optimize for ad hoc execution
```


### Known interoperability issue with existing links

Configuring a link through the Azure portal for migration isn't compatible with existing links that you create manually, either through SQL Server Management Studio (SSMS) or Transact-SQL (T-SQL). If a link already exists, you can't create a new link through the Azure portal.

If a link already exists on either the SQL Server source or Azure SQL Managed Instance target, you need to perform the following steps before creating a new link between that source and target through the Azure portal:
1. Drop the link manually from SQL Managed Instance by using [Remove-AzSqlInstanceLink](/powershell/module/az.sql/remove-azsqlinstancelink) or [az sql mi link delete](/cli/azure/sql/mi/link#az-sql-mi-link-delete) from Azure Cloud Shell or a machine signed in with an Azure Context.
1. Drop the link manually from SQL Server by using [DROP AVAILABILITY GROUP](../../t-sql/statements/drop-availability-group-transact-sql.md) with the name of the distributed availability group associated with the link.
1. Drop all link-related certificates from the SQL Server instance by using [DROP CERTIFICATE](../../t-sql/statements/drop-certificate-transact-sql.md). The certificates that you need to drop typically contain the following values: `DigiKey PKI`, `Microsoft PKI`, `endpoint`, and `database.windows.net`. You can use `SELECT * FROM sys.certificates` to list all certificates on SQL Server.
1. Drop all link-related certificates from SQL Managed Instance by using [Remove-AzSqlInstanceServerTrustCertificate](/powershell/module/az.sql/remove-azsqlinstanceservertrustcertificate) or [az sql mi partner-cert delete](/cli/azure/sql/mi/partner-cert#az-sql-mi-partner-cert-delete) from Azure Cloud Shell or a machine signed in with an Azure Context. You can use [Get-AzSqlInstanceServerTrustCertificate](/powershell/module/az.sql/get-azsqlinstanceservertrustcertificate) or [az sql mi partner-cert show](/cli/azure/sql/mi/partner-cert#az-sql-mi-partner-cert-show) to list the existing authentication certificates on SQL Managed Instance.
1. The previous steps clear all link-related authentication certificates generated for a link created manually. If you're not using an existing availability group locally, consider dropping the existing database mirroring endpoint by using [DROP ENDPOINT](../../t-sql/statements/drop-endpoint-transact-sql.md). You can use `SELECT * FROM sys.endpoints` to list all existing endpoints on SQL Server. You must drop the certificate associated with the endpoint before you can drop the endpoint.

### Detailed troubleshooting with XE Profiler

For detailed troubleshooting of link issues, use [XE Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md).

## Log Replay Service migration issues

This section lists common issues you might encounter when migrating by using Log Replay Service (LRS):
- [Unable to list directories in Azure Blob Storage](#unable-to-list-directories-in-azure-blob-storage)
- [No results were found in directory](#no-results-were-found-in-directory)
- [Check file restoration status](#check-file-restoration-status)
- [Error 2009 - Managed identity is not set up properly](#error-2009---managed-identity-is-not-set-up-properly)
- [Troubleshoot migration with DMS](#troubleshoot-migration-with-dms)
- [Delete migration jobs](#delete-migration-jobs)


### Unable to list directories in Azure Blob Storage

If you see the error message that you're `Unable to list directories` when selecting a directory on the **New Data Migration** page in the Azure portal, then the user currently logged into the portal doesn't have the **Storage Blob Data Reader** role assigned to the storage account. [Grant user access to the storage account](migration-sql-mi-prepare-log-replay-service.md#grant-user-access-to-the-storage-account) to resolve the issue.

### No results were found in directory

If you see the message that `No results were found in directory` when selecting a directory on the **New Data Migration** page in the Azure portal, there's no database backup available within the Azure Blob storage container. To resolve this issue, upload a full database backup to Azure Blob storage.

### Check file restoration status

To check how many files were detected, queued, skipped, or were unrestorable during the migration, use the **Monitor and cutover** page in the Azure portal. After a migration starts, go to the **Monitor and cutover** page, and then select the database you're migrating to open migration details for that database.

### Error 2009 - Managed identity is not set up properly

If your migration starts successfully but then fails with the error `2009 - managed identity is not set up properly`, the primary identity for the managed instance doesn't have the **Storage Blob data Reader** permission assigned to the Azure Blob storage account. [Grant managed identity access to the storage account](migration-sql-mi-prepare-log-replay-service.md#grant-managed-identity-access-to-the-storage-account) to resolve the issue. Verify that the appropriate identity (either the default managed identity or a user-defined custom identity) has the required permissions, or that the identity didn't change since permissions were granted originally. If the identity changed, grant appropriate permissions to the new identity to resolve the issue.

### Troubleshoot migration with DMS

Starting an LRS migration job in Azure Arc automatically creates a Database Migration Service (DMS) migration job so you can use **Azure Database Migration Service** in the Azure portal to see additional details about the migration job.

To view LRS migration job details in DMS, follow these steps:
1. Go to [Azure Database Migration Service](https://portal.azure.com/#view/Microsoft_Azure_DMS/DmsCenterMenuBlade.MenuView/~/getstarted) in the Azure portal.
1. Select **All resources** and then select the DMS migration job associated with your LRS migration to open the migration details page:

   :::image type="content" source="media/migrate-to-azure-sql-managed-instance-troubleshoot/azure-database-migration-service.png" alt-text="Screenshot of the DMS all resources page in the Azure portal.":::

1. On the migration details page, select **Monitor migrations** to see the status of the databases migrated for a particular instance using LRS. Select **Successful**, **Canceled**, or **Error** to see more details on the status.

   Another way to access this page is through the Azure Database Migration Service resource added to the resource group that contains the target SQL Managed Instance after a migration is started through Azure Arc.

### Delete migration jobs

LRS migration jobs stay on the **Monitor and cutover** page for 28 days after they finish (either successfully or failed). You can manually delete the jobs if you want to clear them from the monitoring page sooner.

To manually delete the jobs, go to the [DMS migration job associated with your LRS migration](#troubleshoot-migration-with-dms) as described in the previous section. Select the migration job you want to delete, then use the **Delete** trash can button to delete the job. Confirm by selecting "Check this box to confirm deleting". This action clears the jobs from the **Monitor and cutover** page in Azure Arc.

## Known issues after migrating to SQL Managed Instance

Consider the following known issues after migrating to Azure SQL Managed Instance:

[!INCLUDE [known-issues-after-migration](../../../azure-sql/includes/sql-managed-instance/known-issues-after-migration.md)]

## Contact Microsoft

You can contact Microsoft to open a support ticket with an issue you're having or to provide feedback directly to the product group.

### Contact support

Use <https://aka.ms/azure-support> to go to the **Help + support** page in the Azure portal, and then follow these steps to open a migration-related support ticket:

1. Select **Create a support request** to open the **Support + troubleshooting** pane.
1. Type `migration` into the text field, then select **None of the above** under **Which service are you having an issue with?**
1. From the **Select a service** dropdown list, select `SQL Server enabled by Azure Arc` and then use **Next** to proceed.
1. Select your subscription from the dropdown list.
1. Select your **SQL Server instance enabled by Azure Arc** resource from the **Resource** dropdown list and then select **Next**.
1. Select **Migration Issues** in the **Are you having one of the following issues?** tile and then select **Next**.
1. Select **Create a support request** from the top navigation bar within the **Support + troubleshooting** pane to open the support ticket form.
1. Use the following **Problem subtype** values to route your issue to the appropriate support queue:
   - **Assess**: If you're having issues with the [migration readiness assessment](migration-assessment.md). 
   - **LRS Data Migration**: If you're having issues with a [Log Replay Service (LRS) migration](migration-sql-mi-prepare-log-replay-service.md).
   - **MI Link Data Migration**: If you're having issues with a [Managed Instance link migration](migration-sql-mi-prepare-link.md).
   - **Monitoring and cutover**: If you're having issues monitoring the migration or with cutover.
   - **Target Provisioning**: If you're having issues provisioning the target Azure SQL Managed Instance.
1. Use **Next** to proceed through the remaining steps of the support request form, then select **Create** to submit your support request.

### Provide feedback to the product group

You can provide feedback to the product group to help improve the migration experience. Use the following link to submit your feedback:

- [Provide feedback to the product group](https://aka.ms/arc-migrations-feedback)

## Related content

- [SQL Server migration in Azure Arc Overview](migration-overview.md)
- [Prepare environment for a Managed Instance link migration - SQL Server migration in Azure Arc](migration-sql-mi-prepare-link.md)
- [Prepare environment for LRS migration - SQL Server migration in Azure Arc](migration-sql-mi-prepare-log-replay-service.md)
- [Migration to Azure SQL Managed Instance - SQL Server migration in Azure Arc](migrate-to-azure-sql-managed-instance.md)
