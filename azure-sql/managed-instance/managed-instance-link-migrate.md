---
title: Migrate with the Link
titleSuffix: Azure SQL Managed Instance
description: Learn how to use the Managed Instance link to migrate your SQL Server data to Azure SQL Managed Instance.
author: danimir
ms.author: danil
ms.reviewer: vanto
ms.date: 11/18/2026
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
ai-usage: ai-assisted
ms.custom:
  - ignite-2023
---

# Migrate with the link - Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you to migrate your SQL Server database to Azure SQL Managed Instance by using the [Managed Instance link](managed-instance-link-feature-overview.md) in single-database link mode.

To migrate multiple databases together through one link, you can extend an existing Always On availability group or create a new one. For this workflow, see [Extend an Always On availability group to Azure SQL Managed Instance](managed-instance-link-extend-availability-group.md).

For a detailed migration guide, review [Migrate to Azure SQL Managed Instance](/data-migration/sql-server/managed-instance/guide). To compare migration tools, review [Compare LRS with Managed Instance link](log-replay-service-compare-mi-link.md).

> [!NOTE]  
> You can now migrate your SQL Server instance enabled by Azure Arc to Azure SQL Managed Instance directly through the Azure portal. For more information, see [Migrate to Azure SQL Managed Instance](/sql/sql-server/azure-arc/migrate-to-azure-sql-managed-instance).

## Overview

The Managed Instance link enables migration from SQL Server hosted anywhere, to Azure SQL Managed Instance. The link uses Always On availability group technology to replicate changes nearly in real time from the primary SQL Server instance to the secondary SQL Managed Instance. The link provides the only truly online migration option between SQL Server and Azure SQL Managed Instance, since the only downtime is cutting over to the target SQL managed instance.

Migrating with the link gives you:

- The ability to test your workloads with a read-only database replica on SQL Managed Instance before you finalize the migration to Azure.
- The ability to keep the link and migration running for as long as you need, weeks and even months at a time.
- Near real-time replication of data that provides the fastest available data replication to Azure.
- The most minimum downtime migration compared to all other solutions available today.
- Instantaneous cutover to the target SQL Managed Instance.
- The ability to migrate anytime you're ready.
- You can migrate multiple databases by using a separate link for each database in single-database link mode, or one link for an availability group in multiple-database link mode.
- The only true online migration to the Business Critical service tier.

> [!NOTE]  
> In single-database link mode, you can migrate one database per link and establish multiple links from the same SQL Server instance to SQL Managed Instance. To replicate an existing Always On availability group with multiple databases through one link, use [multiple-database link mode](managed-instance-link-extend-availability-group.md). Review the AG extension requirements before configuring the link.

## Cutover behavior

Cutover behavior depends on the version of SQL Server you're migrating from and the update policy of your target SQL Managed Instance:
- For **SQL Server 2016** to **SQL Server 2019**, cutover to SQL Managed Instance drops the link. 
- For **SQL Server 2022** or later versions cutover to SQL Managed Instance can either drop the link or keep the link based on your choice during failover. If you choose to keep the link, and your SQL managed instance is configured with a matching [update policy](update-policy.md), you can reverse migrate back to SQL Server if needed. 

For migration, removing the link is the recommended option. If you choose to keep the link but later decide to remove it, wait for Azure to complete the first full backup of the database after failover before removing the link. This step ensures the database is healthy and fully functional on SQL Managed Instance. It also helps avoid a rare known issue where the database can become temporarily unavailable after a server restart if the link is removed too early. For more information, see [Database becomes unavailable after server restart following MI link failover](managed-instance-link-troubleshoot-how-to.md#database-unavailable-after-server-restart).

## Reverse a migration

Reverse migration back to SQL Server from Azure SQL Managed Instance might be supported depending on the [update policy](update-policy.md) of your SQL managed instance. For example:

- [SQL Server 2022 update policy](/azure/azure-sql/managed-instance/update-policy#sql-server-2022-update-policy): Databases from instances configured with the **SQL Server 2022** update policy can be restored back to SQL Server 2022 instances.
- [SQL Server 2025 update policy](/azure/azure-sql/managed-instance/update-policy#sql-server-2025-update-policy): Databases from instances configured with the **SQL Server 2025** update policy can be restored back to SQL Server 2025 instances.
- [Always-up-to-date update policy](/azure/azure-sql/managed-instance/update-policy#always-up-to-date-update-policy): Databases from instances configured with the **Always-up-to-date** update policy can't be restored back to SQL Server.

If your source SQL Server version is earlier than SQL Server 2022, reverse migration isn't possible. When you migrate your database to SQL Managed Instance, it undergoes an internal upgrade to a newer database version that isn't compatible with earlier SQL Server versions. Reverse migration database compatibility is only available when SQL Managed Instance is configured with the corresponding update policy.

You can't migrate SQL Managed Instance databases encrypted with service-managed TDE keys to SQL Server. To migrate an encrypted database to SQL Server, the database must use a customer-managed key, and the destination SQL Server instance must have access to the same key. For more information, see [Set up SQL Server TDE with Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault).

## Prerequisites

To use the link with Azure SQL Managed Instance for migration, you need the following prerequisites:

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#prerequisites) with the required service update installed.

## Assess and discover

Assess your SQL Server database for migration readiness before you migrate. You can use the **Migrate SQL Server** feature in SQL Server Management Studio (SSMS) to [assess readiness for migration](/ssms/migrate/migrate-sql-server-azure-sql?tabs=sql-standard#assess-readiness-for-migration).

## Create target instance

After you've assessed your existing environment, and determined the appropriate service tier and hardware configuration for your target SQL managed instance, deploy your target instance by using the [Azure portal](instance-create-quickstart.md), [PowerShell](scripts/create-configure-managed-instance-powershell.md) or the [Azure CLI](scripts/create-configure-managed-instance-cli.md).

If you want to retain reverse failover capabilities, your SQL Server version must match the update policy on your managed instance. For example, provision your managed instance with the **SQL Server 2022** update policy for SQL Server 2022, or the **SQL Server 2025** update policy for SQL Server 2025. The **Always-up-to-date** update policy on your managed instance doesn't support reverse failover to SQL Server. For more information, see [Update policy](update-policy.md) and [Reverse a migration](#reverse-a-migration).

## Configure link

After you create your target SQL managed instance, configure a link between SQL Server and Azure SQL Managed Instance. Use single-database link mode to replicate one database per link, or multiple-database link mode to replicate all databases in a SQL Server Always On availability group through a single Managed Instance link.

First, [prepare your environment](managed-instance-link-preparation.md), including [preparing your databases](managed-instance-link-preparation.md#prepare-databases). For single-database link mode, configure a link by using [SQL Server Management Studio (SSMS)](managed-instance-link-configure-how-to-ssms.md) or [scripts](managed-instance-link-configure-how-to-scripts.md). Multiple-database link mode isn't enabled by default; for requirements and steps to enable and configure it, see [Extend an Always On availability group to Azure SQL Managed Instance](managed-instance-link-extend-availability-group.md).

## Check replication lag

It's important that the secondary replica catches up to the primary replica before performing a planned migration failover. Planned failover can time out and fail if the secondary replica lags far behind the primary replica.

Use the following T-SQL query on both SQL Server and SQL Managed Instance to monitor replication lag between the replicas:

```sql
-- Execute on SQL Server and SQL Managed Instance 
USE master
-- Replace <DAGname> with the distributed availability group name, which is typically the link name.
DECLARE @link_name varchar(max) = '<DAGname>'
SELECT
   ag.name [Link name], 
   ars1.role_desc [Link role],
   ars2.connected_state_desc [Link connected state],
   ars2.synchronization_health_desc [Link sync health],
   drs.secondary_lag_seconds [Link replication latency (seconds)]
FROM
   sys.availability_groups ag 
   JOIN sys.dm_hadr_availability_replica_states ars1
   ON ag.group_id = ars1.group_id
   JOIN sys.dm_hadr_availability_replica_states ars2
   ON ag.group_id = ars2.group_id
   JOIN sys.dm_hadr_database_replica_states drs
   ON ars2.replica_id = drs.replica_id
WHERE 
   ag.is_distributed = 1 AND ag.name = @link_name AND ars1.is_local = 1 AND ars2.is_local = 0
GO
```

If replication lag is high, wait for the secondary replica to catch up with the primary replica. You might need to perform additional troubleshooting steps if the lag persists, such as pausing workloads on the primary replica, improving link network throughput between the two instances, or increasing resource capacity on the secondary replica. The easiest way to stop workloads on a SQL Server primary replica is to cut application connections to the instance.

## Migrate multiple databases

You can migrate multiple databases by using either single-database or multiple-database link mode. Single-database link mode replicates one database per link, so each database requires a separate link. Multiple-database link mode replicates all databases in a SQL Server Always On availability group through a single Managed Instance link on [supported SQL Server versions and cumulative updates](managed-instance-link-extend-availability-group.md#supportability).

In single-database link mode, if you plan to migrate multiple databases from instances on the same server, for optimal performance and predictability, migrate 8 databases per instance at a time. For example, if you have 10 instances with 32 linked databases each, migrate 8 databases at a time from each instance by using planned failovers, and repeat the process until all databases are migrated.

In multiple-database link mode, the seeding and failover process is managed automatically, so you don't need to manually migrate a limited number of databases at a time as you do in single-database link mode. For requirements and configuration steps, see [Extend an Always On availability group to Azure SQL Managed Instance](managed-instance-link-extend-availability-group.md).

## Data sync and cutover

After your link is established, and you're ready to migrate, follow these steps (typically during a maintenance window):

1. Stop the workload on the primary SQL Server database so the secondary database on SQL Managed Instance catches up. The easiest way to stop workloads on a SQL Server primary replica is to cut application connections to the instance.
1. Validate all data has made it over to the secondary database on SQL Managed Instance. Check [replication lag](#check-replication-lag) to ensure the secondary replica is caught up with the primary replica.
1. [Fail over the link](managed-instance-link-failover-how-to.md) to the secondary SQL managed instance by choosing **Planned failover**.
1. (Optionally) Check the box to **Remove link after successful failover** to ensure that failover is one way, and the link is removed. 
1. (Optionally) If you're on a supported SQL Server version with a matching SQL Managed Instance [update policy](update-policy.md), you can keep the link after failover to reverse a migration if needed. Check the [reverse a migration section](#reverse-a-migration) for specific version details.
1. Cut over the application to connect to the SQL managed instance endpoint.
1. (Optionally) If you didn't choose to remove the link during failover, you can remove the link after cutover once you no longer need it.

## Validate migration

After you've cut over to the SQL managed instance target, monitor your application, test performance and remediate any issues.

For details, review [post-migration](/data-migration/sql-server/managed-instance/guide#post-migration).

## Known issues with migration

Review the following known issues related to migration with the link. 

### Database becomes unavailable after server restart following MI link failover

In rare circumstances, your database might become temporarily unavailable on SQL Managed Instance after a server restart following the failover of the link. This known issue occurs when you drop a link before Azure completes a full backup of the database after initial failover to SQL Managed Instance.

The database automatically recovers after Microsoft's intervention, but this recovery can take some time.

To avoid this problem, don't drop the link until the first full backup finishes after failover from SQL Server to SQL Managed Instance. For more information, see [Database unavailable after server restart](managed-instance-link-troubleshoot-how-to.md#database-unavailable-after-server-restart).

[!INCLUDE [known-issues-after-migration](../includes/sql-managed-instance/known-issues-after-migration.md)]


## Post-migration steps

After migrating your databases:

- Update your application's connection string to point to the destination instance.
- Migrate the logins your applications require.
- If you have SQL Server Agent jobs, manually copy them to SQL Managed Instance.
- Monitor application performance and validate the migration.

For instructions, see [Post-migration steps](/data-migration/sql-server/managed-instance/guide#post-migration).

## Related content

To use the link:

- [Prepare your environment for a link](managed-instance-link-preparation.md)
- [Configure link with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Configure link with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Fail over link](managed-instance-link-failover-how-to.md)
- [Managed Instance link best practices](managed-instance-link-best-practices.md)

To learn more about the link:

- [Overview of the Managed Instance link](managed-instance-link-feature-overview.md)
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md)

For other replication and migration scenarios, consider:

- [Transactional replication with Azure SQL Managed Instance](replication-transactional-overview.md)
- [Overview of Log Replay Service with Azure SQL Managed Instance](log-replay-service-overview.md)
- [Compare LRS with Managed Instance link](log-replay-service-compare-mi-link.md)
