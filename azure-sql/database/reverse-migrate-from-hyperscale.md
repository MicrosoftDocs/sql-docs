---
title: How to Reverse Migrate a Database from Hyperscale
description: How to reverse migrate an Azure SQL Database from the Hyperscale tier to the General Purpose tier.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 02/10/2025
ms.service: azure-sql-database
ms.topic: how-to
ms.custom:
  - devx-track-azurepowershell
  - devx-track-azurecli
monikerRange: "=azuresql || =azuresql-db"
---

# Reverse migrate a database from Hyperscale

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

You can migrate an existing Hyperscale database in Azure SQL Database to the General Purpose service tier using the Azure portal, the Azure CLI, PowerShell, or Transact-SQL.

Reverse migration to the General Purpose service tier allows customers who have recently converted an existing database in Azure SQL Database to Hyperscale to move back in an emergency, should Hyperscale not meet their needs. While reverse migration is initiated by a service tier change, it's essentially a size-of-data move between different architectures.

## Limitations for reverse migration

Reverse migration is available under the following conditions:

- Reverse migration is only available within 45 days of the original migration to Hyperscale.
- Databases originally created in the Hyperscale service tier aren't eligible for reverse migration.
- You might reverse migrate to the [General Purpose](service-tiers-sql-database-vcore.md#general-purpose) service tier only. Your migration from Hyperscale to General Purpose can target either the serverless or provisioned compute tiers. If you wish to migrate the database to another service tier, such as [Business Critical](service-tiers-sql-database-vcore.md#business-critical) or a [DTU based service tier](service-tiers-dtu.md), first reverse migrate to the General Purpose service tier, then change the service tier.
- Reverse migration to databases with less than 2 vCores is not supported. You can scale the database down to fewer than 2 vCores once the migration is complete.
- Direct reverse migration from, or to, an elastic pool isn't supported. You can reverse migrate only a Hyperscale single database to a General Purpose single database.
    - If the Hyperscale database is part of a [Hyperscale elastic pool](./hyperscale-elastic-pool-overview.md), you have to first remove it from the Hyperscale elastic pool prior to reverse migration.
    - After reverse migration is complete, you can then optionally add the General Purpose single database to a General Purpose elastic pool if needed.
- For databases that don't qualify for reverse migration, the only way to migrate from Hyperscale to a non-Hyperscale service tier is to export/import using a bacpac file or other data movement technologies (Bulk Copy, Azure Data Factory, Azure Databricks, SSIS, etc.) Bacpac export/import from Azure portal, from PowerShell using New-AzSqlDatabaseExport or New-AzSqlDatabaseImport, from Azure CLI using az sql db export and az sql db import, and from REST API isn't supported. Bacpac import/export for smaller Hyperscale databases (up to 150 GB) is supported using SSMS and SqlPackage version 18.4 and later. For larger databases, bacpac export/import might take a long time, and can fail for various reasons.

## Duration and downtime

Unlike regular service level objective change operations in Hyperscale, migrating to Hyperscale and reverse migration to General Purpose are size-of-data operations.

The duration of a reverse migration operation depends mainly on the size of the database and concurrent write activities happening during the migration. The number of vCores you assign to the target General Purpose database also impacts the duration of the reverse migration. We recommend that you provision the target General Purpose database with a number of vCores greater than or equal to the number of vCores assigned to the source Hyperscale database to sustain similar workloads.

During reverse migration, the source Hyperscale database can experience performance degradation if under substantial load. Specifically, transaction log rate might be reduced (throttled) to ensure that reverse migration is making progress.

You'll experience a short period of downtime, generally a few minutes, during the final cutover to the new target General Purpose database.

## Prerequisites

Before you initiate a reverse migration from Hyperscale to the General Purpose service tier, you must ensure that your database meets the [limitations for reverse migration](#limitations-for-reverse-migration) and:

- Your database doesn't have Geo Replication enabled.
- Your database doesn't have named replicas.
- Your database (allocated size) is small enough to fit into the target service tier.
- If you specify max database size for the target General Purpose database, ensure the allocated size of the database is small enough to fit into that maximum size.

Prerequisite checks occur before a reverse migration operation starts. If prerequisites aren't met, the reverse migration operation fails immediately.

## Backup policies

You are [billed using the regular pricing](automated-backups-overview.md?tabs=single-database#backup-storage-costs) for all existing database backups within the [configured retention period](automated-backups-overview.md#backup-retention). You are billed for the Hyperscale backup storage snapshots and for size-of-data storage blobs that must be retained to be able to restore the backup.

You can convert a database to Hyperscale and reverse migrate back to General Purpose multiple times. Only backups from the current and once-previous tier of your database are available for restore. If you have moved from the General Purpose service tier to Hyperscale and back to General Purpose, the only backups available are the ones from the current General Purpose database and the immediately previous Hyperscale database. These retained backups are billed as per Azure SQL Database billing. Any previous tiers tried won't have backups available and will not be billed.

For example, you could migrate between Hyperscale and non-Hyperscale service tiers:

1. General Purpose
1. Convert to Hyperscale
1. Reverse migrate to General Purpose
1. Service tier change to Business Critical
1. Convert to Hyperscale
1. Reverse migrate to General Purpose

In this case, the only backups available would be from steps 5 and 6 of the timeline, if they are still within the [configured retention period](automated-backups-overview.md#backup-retention). Any backups from previous steps would be unavailable. Carefully consider the availability of backups when attempting repeated migrations of the same database between the Hyperscale and the General Purpose service tiers. Backups of databases older than the immediately previous database become unavailable as soon as a reverse migration is started and remain unavailable even if the migration is canceled. 

## How to reverse migrate a Hyperscale database to the General Purpose service tier

To reverse migrate an existing Hyperscale database in Azure SQL Database to the General Purpose service tier, first identify your target service objective in the General Purpose service tier and whether you wish to migrate to the provisioned or serverless compute tiers. Review [resource limits for single databases](resource-limits-vcore-single-databases.md) if you aren't sure which service objective is right for your database.

If you wish to perform an additional service tier change after reverse migrating to General Purpose, identify your eventual target service objective. Ensure that your database's allocated size is small enough to fit in that service objective.

Select the tab for your preferred method to reverse migrate your database:

# [Portal](#tab/azure-portal)

The Azure portal enables you to reverse migrate to the General Purpose service tier by modifying the pricing tier for your database.

:::image type="content" source="media/reverse-migrate-from-hyperscale/reverse-migrate-hyperscale-service-compute-tier-pane.png" alt-text="Screenshot of the compute + storage panel of a Hyperscale database in Azure SQL Database." lightbox="media/reverse-migrate-from-hyperscale/reverse-migrate-hyperscale-service-compute-tier-pane.png":::

1. Navigate to the database in the Azure portal.
1. In the left navigation bar, select **Compute + storage**.
1. Select the **Service tier** dropdown list to expand the options for service tiers.
1. Select **General Purpose (Scalable compute and storage options)** from the dropdown list menu.
1. Review the **Hardware Configuration** listed. If desired, select **Change configuration** to select the appropriate hardware configuration for your workload.
1. Select the **vCores** slider if you wish to change the number of vCores available for your database under the General Purpose service tier.
1. Select **Apply**.
1. Monitor the conversion in the Azure portal. 
    1. Navigate to the database in the Azure portal.
    1. In the left navigation bar, select **Overview**.
    1. Review the **Notifications** section at the bottom of the right pane. If operations are ongoing, a notification box appears.
    1. To view details, select the notification box.
    1. The **Ongoing operations** pane opens. Review the details of the ongoing operations.

# [Azure CLI](#tab/azure-cli)

This code sample calls [az sql db update](/cli/azure/sql/db#az-sql-db-update) to reverse migrate an existing Hyperscale database to the General Purpose service tier. You must specify both the edition and service objective. You might select either `Provisioned` or `Serverless` for the target compute model.

Replace `resourceGroupName`, `serverName`, `databaseName`, and `serviceObjective` with the appropriate values before running the following code sample:

```azurecli-interactive
resourceGroupName="myResourceGroup"
serverName="server01"
databaseName="mySampleDatabase"
serviceObjective="GP_Gen5_2"
computeModel="Provisioned"

az sql db update -g $resourceGroupName -s $serverName -n $databaseName \
    --edition GeneralPurpose --service-objective $serviceObjective \
    --compute-model $computeModel
```

You can optionally include the `maxsize` argument. If the `maxsize` value exceeds the valid maximum size for the target service objective, an error is returned. If the `maxsize` argument isn't specified, the operation defaults to the maximum size available for the given service objective. The following example specifies `maxsize`:

```azurecli-interactive
resourceGroupName="myResourceGroup"
serverName="server01"
databaseName="mySampleDatabase"
serviceObjective="GP_Gen5_2"
computeModel="Provisioned"
maxsize="200GB"

az sql db update -g $resourceGroupName -s $serverName -n $databaseName \
    --edition GeneralPurpose --service-objective $serviceObjective \
    --compute-model $computeModel --max-size $maxsize
```

Monitor the ongoing operation with [az sql db op list](/cli/azure/sql/db/op#az-sql-db-op-list). To return recent or ongoing operations for a database in Azure SQL Database:

```azurecli-interactive
resourceGroupName="myResourceGroup"
serverName="server01"
databaseName="mySampleDatabase"

az sql db op list -g $resourceGroupName -s $serverName -n $databaseName
```

# [PowerShell](#tab/azure-powershell)

This code sample uses the [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) cmdlet to reverse migrate an existing database from Hyperscale to the General Purpose service tier. You must specify both the edition and service objective. You might select either `Provisioned` or `Serverless` for the target compute tier.

Replace `$resourceGroupName`, `$serverName`, `$databaseName`, `$serviceObjective`, and `$computeModel` with the appropriate values before running this code sample:

```powershell-interactive
$resourceGroupName = "myResourceGroup"
$serverName = "server01"
$databaseName = "mySampleDatabase"
$serviceObjective = "GP_Gen5_2"
$computeModel = "Provisioned"

Set-AzSqlDatabase -ResourceGroupName $resourceGroupName -ServerName $serverName `
    -DatabaseName $databaseName -Edition "GeneralPurpose" -computemodel $computeModel `
    -RequestedServiceObjectiveName $serviceObjective
```

You can optionally include the `maxsize` argument. If the `maxsize` value exceeds the valid maximum size for the target service objective, an error is returned. If the `maxsize` argument isn't specified, the operation defaults to the maximum size available for the given service objective. The following example specifies `maxsize`:

```powershell-interactive
$resourceGroupName = "myResourceGroup"
$serverName = "server01"
$databaseName = "mySampleDatabase"
$serviceObjective = "GP_Gen5_2"
$computeModel = "Provisioned"
$maxSizeBytes = "268435456000"

Set-AzSqlDatabase -ResourceGroupName $resourceGroupName -ServerName $serverName `
    -DatabaseName $databaseName -Edition "GeneralPurpose" -computemodel $computeModel `
    -RequestedServiceObjectiveName $serviceObjective -MaxSizeBytes $maxSizeBytes
```

Monitor the operation in progress with the [Get-AzSqlDatabaseActivity](/powershell/module/az.sql/get-azsqldatabaseactivity) cmdlet, which returns recent or ongoing operations for a database in Azure SQL Database. Set the `$resourceGroupName`, `$serverName`, and `$databaseName` parameters to the appropriate values for your database before running the sample code:

```powershell-interactive
$resourceGroupName = "myResourceGroup"
$serverName = "server01"
$databaseName = "mySampleDatabase"

Get-AzSqlDatabaseActivity -ResourceGroupName $resourceGroupName `
    -ServerName $serverName `
    -DatabaseName $databaseName
```

# [Transact-SQL](#tab/t-sql)

To reverse migrate a Hyperscale database to the General Purpose service tier with Transact-SQL, first connect to the `master` database on your [logical SQL server](logical-servers.md) using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms).

You must specify both the edition and service objective in the [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql?preserve-view=true&view=azuresqldb-current) statement.

This example statement migrates a database named `mySampleDatabase` to the General Purpose service tier with the `GP_Gen5_4` service objective. Replace the database name and service objective with the appropriate values before executing the statement.

```sql
ALTER DATABASE [mySampleDatabase]
    MODIFY (EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_Gen5_2');
GO
```

You can optionally include the `maxsize` argument. If the `maxsize` value exceeds the valid maximum size for the target service objective, an error is returned. If the `maxsize` argument isn't specified, the operation defaults to the maximum size available for the given service objective. The following example specifies `maxsize`:

```sql
ALTER DATABASE [mySampleDatabase]
    MODIFY (EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_Gen5_2', MAXSIZE = 200 GB);
GO
```

To monitor operations for a Hyperscale database, connect to the `master` database of your [logical server](logical-servers.md) using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms), the [MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code), or the client of your choice to run Transact-SQL commands.

Query the [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database?view=azuresqldb-current&preserve-view=true) Dynamic Management View to review information about recent operations performed on databases on your logical server.

The `sys.dm_operation_status` dynamic management view returns operations sorted by most recent. Replace the database name with the appropriate value before running the code sample.

```sql
SELECT *
FROM sys.dm_operation_status
WHERE major_resource_id = 'mySampleDatabase'
ORDER BY start_time DESC;
GO
```

---

## Related content

- [How to manage a Hyperscale database](manage-hyperscale-database.md)
- [Convert an existing database to Hyperscale](convert-to-hyperscale.md)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)
