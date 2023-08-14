---
title: Hyperscale elastic pools overview
description: Manage and scale multiple Hyperscale databases in Azure SQL Database by using Hyperscale elastic pools. For one price, you can distribute resources where they're needed.
author: arvindshmicrosoft
ms.author: arvindsh
ms.reviewer: wiassaf, mathoma
ms.date: 05/21/2023
ms.service: sql-database
ms.subservice: elastic-pools
ms.topic: conceptual
ms.custom: 
---
# Hyperscale elastic pools overview in Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides an overview of Hyperscale elastic pools in Azure SQL Database. 

An Azure SQL Database [elastic pool](./elastic-pool-overview.md) enables software as a service (SaaS) developers to optimize the price performance ratio for a group of databases within a prescribed budget, while delivering performance elasticity for each database. Azure SQL Database Hyperscale elastic pools introduces a shared resource model for Hyperscale databases. 

For examples to create, scale, or move databases into a Hyperscale elastic pool by using the Azure CLI or PowerShell, review [Working with Hyperscale elastic pools using command-line tools](./hyperscale-elastic-pool-command-line.md)

> [!NOTE]
> [Elastic pools for Hyperscale](./hyperscale-elastic-pool-overview.md) are currently in preview.

## Overview

Deploy your Hyperscale database to an [elastic pool](elastic-pool-overview.md) to share resources between databases within the pool and optimize the cost of having multiple databases with different usage patterns. 

Scenarios to use an elastic pool with your Hyperscale databases: 

- When you need to scale the compute resources allocated to the elastic pool up or down in a predictable amount of time, independent of the amount of allocated storage. 
- When you want to scale out the compute resources allocated to the elastic pool by adding one or more read-scale replicas. 
- If you want to use high transaction log throughput for write-intensive workloads, even with lower compute resources. 

Migrating non-Hyperscale databases to a Hyperscale elastic pool upgrades the databases to the Hyperscale service tier. 

## Architecture 

Traditionally, the [architecture of a standalone Hyperscale database](service-tier-hyperscale.md#distributed-functions-architecture) consists of three main independent components: Compute, Storage ("Page Servers"), and the log ("Log Service"). When you create an elastic pool for your Hyperscale databases, the databases within the pool share compute and log resources. Additionally, if you choose to configure high availability, then each high availability pool is created with an equivalent and independent set of compute and log resources.

The following describes the architecture of an elastic pool for Hyperscale databases: 

- A Hyperscale elastic pool consists of a primary pool that hosts primary Hyperscale databases, and, if configured, up to four additional high availability pools. 
- Primary Hyperscale databases hosted in the primary elastic pool share the SQL Server database engine (sqlservr.exe) compute process, vCores, memory, and SSD cache. 
- Configuring high availability for the primary pool creates additional high availability pools that contain read-only database replicas for the databases in the primary pool. Each primary pool can have a maximum of four high availability replica pools. Each high availability pool shares compute, SSD cache, and memory resources for all the secondary read-only databases in the pool. 
- Hyperscale databases in the primary elastic pool all share the same log service. Since databases in the high availability pools don't have write workload, they don't utilize the log service. 
- Each Hyperscale database has its own set of page servers, and these page servers are shared between the primary database in the primary pool, and all secondary replica databases in the high availability pool. 
- Geo-replicated secondary Hyperscale databases can be placed inside another elastic pool. 
- Specifying `ApplicationIntent=ReadOnly` in your database connection string routes you to a read-only replica database in one of the high availability pools. 

The following diagram shows the architecture of an elastic pool for Hyperscale databases: 

:::image type="content" source="media/hyperscale-elastic-pool-overview/elastic-pool-hyperscale-architecture.png" alt-text="Diagram showing the Hyperscale elastic pool architecture.":::

## Manage Hyperscale elastic pool databases

You can use the [same commands](elastic-pool-manage.md) to manage your pooled Hyperscale databases as pooled databases in the other service tiers. Just be sure to specify `Hyperscale` for the edition when creating your Hyperscale elastic pool. 

The only difference is the ability to modify the number of high availability (H/A) replicas for an existing Hyperscale elastic pool. To do so:

- Use the `HighAvailabilityReplicaCount` parameter of the Azure PowerShell [Set-AzSqlElasticPool](/powershell/module/az.sql/set-azsqlelasticpool) command. 
- Use the `--ha-replicas` parameter of the Azure CLI [az sql elastic-pool update](/cli/azure/sql/elastic-pool) command.

You can use the following client tools to manage your Hyperscale databases in an elastic pool: 

- Azure PowerShell: [Az.Sql.3.11.0 or higher](https://www.powershellgallery.com/packages/Az.Sql/3.11.0). PowerShell AzureRM.Sql isn't supported. 
- The Azure CLI: [Az version 2.40.0 or higher](/cli/azure/install-azure-cli).
- Transact-SQL (T-SQL) starting with: [SQL Server Management Studio (SSMS) v18.12.1](/sql/ssms/download-sql-server-management-studio-ssms) or [Azure Data Studio v1.39.1](/sql/azure-data-studio/download-azure-data-studio).

## Resource limits

The following lists the supported limits for working with Hyperscale databases within elastic pools: 

- Supported hardware generation: Standard-series (Gen5) only. 
- vCore maximum per pool: 80 vCores. 
- Maximum supported data size per database: 100 TB.
- Maximum supported total data size across DBs in the pool: 100 TB.
- Maximum supported transaction log throughput per database: 100 MB.
- Maximum supported total transaction log throughput across databases in the pool: 130 MB/second.
- Each Hyperscale elastic pool can have up to 25 databases. 

For greater detail, see the [Hyperscale elastic pool resource limits](resource-limits-vcore-elastic-pools.md#hyperscale---provisioned-compute---standard-series-gen5). 

> [!NOTE]
> Performance profiles, supported capabilities, and published limits are subject to change while the feature is in preview. As such, it's best to validate your use case with regular functional, performance, and scale testing of workloads. 

## Limitations

During preview, consider the following limitations: 

- Changing an existing non-Hyperscale elastic pool to the Hyperscale edition isn't supported. Individual databases need to be moved out of their respective existing pool before they can be added to the Hyperscale elastic pool.
- Changing the edition of a Hyperscale elastic pool to a non-Hyperscale edition isn't supported.
- In order to [reverse migrate](./manage-hyperscale-database.md#reverse-migrate-from-hyperscale) an eligible database, which is in a  Hyperscale elastic pool, it must first be removed from the Hyperscale elastic pool. The standalone Hyperscale database can then be reverse migrated to a General Purpose standalone database.
- Maintenance of databases in a pool is performed, and maintenance windows are configured, at the pool level. It isn't currently possible to configure a maintenance window for Hyperscale elastic pools. 
- Zone redundancy isn't currently available for Hyperscale elastic pools. Attempting to add a zone-redundant Hyperscale database to a Hyperscale elastic pool results in an error. 
- Adding a [named replica](./service-tier-hyperscale-replicas.md#named-replica) inside a Hyperscale elastic pool isn't supported. Attempting to add a named replica of a Hyperscale database to a Hyperscale elastic pool results in a `UnsupportedReplicationOperation` error. Instead, create the named replica as a single Hyperscale database.

## Known issues

| Issue | Recommendation |
|--|--|
| In rare cases, you may get the error `45122 - This Hyperscale database cannot be added into an elastic pool at this time. In case of any questions, please contact Microsoft support`, when trying to move / restore / copy a Hyperscale database into an elastic pool. | This limitation is due to implementation-specific details. If this error is blocking you, raise a support incident and request help. |
| If you use Azure PowerShell or the Azure CLI to create a [geo-replica with Geo-Zone backup storage redundancy](./hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) inside a Hyperscale pool, the request fails with the error `(UnsupportedBackupStorageRedundancyForEdition) The requested backup storage redundancy of GeoZone is not supported for edition.` | To work around this issue, use the T-SQL [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql?view=azuresqldb-current&preserve-view=true#add-secondary-on-server-partner_server_name) command to add the geo-secondary database. |
| If you try to create a new Hyperscale elastic pool from PowerShell with the `-ZoneRedundant` parameter specified, you get a vague `One or more errors occurred`. If you run the PowerShell command with the respective `-Verbose` and `-Debug` parameters specified, you get the actual error: `Provisioning of zone redundant database/pool is not supported for your current request`. | At this time, creating Hyperscale elastic pools with zone redundancy specified is unsupported. |
| If you try to use the Azure portal to add an existing Hyperscale database, which has Geo Zone specified as the backup storage redundancy level, into a Hyperscale elastic pool, you get an error `Changing the backup storage account type value for Hyperscale is not supported`. | To work around this issue, use the [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql?view=azuresqldb-current&preserve-view=true#add-secondary-on-server-partner_server_name) command, or the equivalent [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase), or [az sql db update](/cli/azure/sql/db#az-sql-db-update) commands to add the database to the elastic pool. |

## Next steps

- For examples to create, scale, and move databases into a Hyperscale elastic pool by using the Azure CLI or PowerShell, review [Working with Hyperscale elastic pools using command-line tools](./hyperscale-elastic-pool-command-line.md).
- For pricing information, see [Elastic pool pricing](https://azure.microsoft.com/pricing/details/sql-database/elastic).
- To scale elastic pools, see [Scale elastic pools](elastic-pool-scale.md) and [Scale an elastic pool - sample code](scripts/monitor-and-scale-pool-powershell.md).
- To learn more about design patterns for SaaS applications by using elastic pools, see [Design patterns for multitenant SaaS applications with SQL Database](saas-tenancy-app-design-patterns.md).
- For a SaaS tutorial by using elastic pools, see [Introduction to the Wingtip SaaS application](saas-dbpertenant-wingtip-app-overview.md).
- To learn about resource management in elastic pools with many databases, see [Resource management in dense elastic pools](elastic-pool-resource-management.md).
