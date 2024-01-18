---
title: Scale single database resources
description: This article describes how to scale the compute and storage resources available for a single database in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma
ms.date: 01/18/2024
ms.service: sql-database
ms.subservice: performance
ms.topic: conceptual
ms.custom: sqldbrb=1, references_regions
---
# Scale single database resources in Azure SQL Database

This article describes how to scale the compute and storage resources available for an Azure SQL Database in the provisioned compute tier. Alternatively, the [serverless compute tier](serverless-tier-overview.md) provides compute autoscaling and bills per second for compute used.

After initially picking the number of vCores or DTUs, you can scale a single database up or down dynamically based on actual experience using:

* [Transact-SQL](/sql/t-sql/statements/alter-database-transact-sql#overview-sql-database)
* [Azure portal](single-database-manage.md#the-azure-portal)
* [PowerShell](/powershell/module/az.sql/set-azsqldatabase)
* [Azure CLI](/cli/azure/sql/db#az-sql-db-update)
* [REST API](/rest/api/sql/databases/update)

> [!IMPORTANT]
> Under some circumstances, you may need to shrink a database to reclaim unused space. For more information, see [Manage file space in Azure SQL Database](file-space-manage.md).

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Impact


Changing the service tier or compute size of mainly involves the service performing the following steps:

1. Create a new compute instance for the database. 

    A new compute instance is created with the requested service tier and compute size. For some combinations of service tier and compute size changes, a replica of the database must be created in the new compute instance, which involves copying data and can strongly influence the overall latency. Regardless, the database remains online during this step, and connections continue to be directed to the database in the original compute instance.

2. Switch routing of connections to a new compute instance.

    Existing connections to the database in the original compute instance are dropped. Any new connections are established to the database in the new compute instance. For some combinations of service tier and compute size changes, database files are detached and reattached during the switch.  Regardless, the switch can result in a brief service interruption when the database is unavailable generally for less than 30 seconds and often for only a few seconds. If there are long-running transactions running when connections are dropped, the duration of this step may take longer in order to recover aborted transactions. [Accelerated Database Recovery](../accelerated-database-recovery.md) can reduce the impact from aborting long running transactions.

> [!IMPORTANT]
> No data is lost during any step in the workflow. Make sure that you have implemented some [retry logic](troubleshoot-common-connectivity-issues.md) in the applications and components that are using Azure SQL Database while the service tier is changed.

## Latency

The estimated latency to change the service tier, scale the compute size of a single database or elastic pool, move a database in/out of an elastic pool, or move a database between elastic pools is parameterized as follows:

| Database scaling latency | To Basic single database,</br>Standard single database (S0-S1) | To Standard single database (S2-S12),</br>General Purpose single database,</br>Basic elastic pooled database,</br>Standard elastic pooled database,</br>General Purpose pooled database | To Premium single database or pooled database,</br>Business Critical single database or pooled database | To Hyperscale single database or pooled database |
|:-|:-|:-|:-|:-|
| **From Basic single database,</br>Standard single database (S0-S1)** | &bull; &nbsp;Constant time latency independent of space used.</br>&bull; &nbsp;Typically, less than 5 minutes. | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. |
| **From Basic pooled database,</br>Standard single database (S2-S12),</br>Standard pooled database,</br>General Purpose single database or pooled database** | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. | &bull; &nbsp;For single databases, constant time latency independent of space used.</br>&bull; &nbsp;Typically, less than 5 minutes for single databases.</br>&bull; &nbsp;For elastic pools, proportional to the number of databases. | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. |
| **From Premium single database or pooled database,</br>Business Critical single database or pooled database** | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. | &bull; &nbsp;Latency proportional to database space used due to data copying.</br>&bull; &nbsp;Typically, less than 1 minute per GB of space used. |
| **From Hyperscale single database or pooled database** | N/A | See [Reverse migrate from Hyperscale](manage-hyperscale-database.md#reverse-migrate-from-hyperscale) for supported scenarios and limitations. | N/A | &bull; &nbsp;Constant time latency independent of space used.</br>&bull; &nbsp;Typically, less than 2 minutes. |

> [!NOTE]
> - Additionally, for Standard (S2-S12) and General Purpose databases, latency for moving a database in/out of an elastic pool or between elastic pools will be proportional to database size if the database is using Premium File Share ([PFS](/azure/storage/files/storage-files-introduction)) storage.
> - In the case of moving a database to/from an elastic pool, only the space used by the database impacts the latency, not the space used by the elastic pool.
> - To determine if a database is using PFS storage, execute the following query in the context of the database. If the value in the AccountType column is `PremiumFileStorage` or `PremiumFileStorage-ZRS`, the database is using PFS storage.

```sql
SELECT s.file_id,
       s.type_desc,
       s.name,
       FILEPROPERTYEX(s.name, 'AccountType') AS AccountType
FROM sys.database_files AS s
WHERE s.type_desc IN ('ROWS', 'LOG');
```

> [!NOTE]
> - The zone redundant property will remain the same by default when scaling a single database from the Business Critical to the General Purpose tier. 
> - Latency for the scaling operation when zone redundancy is changed for a General Purpose single database is proportional to database size.

> [!TIP]
> To monitor in-progress operations, see: [Manage operations using the SQL REST API](/rest/api/sql/operations/list), [Manage operations using CLI](/cli/azure/sql/db/op), [Monitor operations using T-SQL](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) and these two PowerShell commands: [Get-AzSqlDatabaseActivity](/powershell/module/az.sql/get-azsqldatabaseactivity) and [Stop-AzSqlDatabaseActivity](/powershell/module/az.sql/stop-azsqldatabaseactivity).

## Monitor or cancel scaling changes

A service tier change or compute rescaling operation can be monitored and canceled.

### [Azure portal](#tab/azure-portal)

In the SQL database **Overview** page, look for the banner indicating a scaling operation is ongoing, and select the **See more** link for the deployment in progress.

:::image type="content" source="media/single-database-scale/scaling-operation-in-progress-see-more.png" alt-text="Screenshot from the Azure portal showing a scaling operation in progress.":::

On the resulting **Ongoing operations** page, select **Cancel this operation**.

:::image type="content" source="media/single-database-scale/ongoing-operations-cancel-this-operation.png" alt-text="Screenshot from the Azure portal showing the Ongoing operations page and the cancel this operation button.":::

### [PowerShell](#tab/azure-powershell)

In order to invoke the PowerShell commands on a computer, [Az package version 9.7.0](https://www.powershellgallery.com/packages/Az/9.7.0) or a newer version must be installed locally. Or, consider using the [Azure Cloud Shell](/azure/cloud-shell/overview) to run Azure PowerShell at [shell.azure.com](https://shell.azure.com/).

First, log in to Azure and set the proper context for your subscription:

```powershell
Login-AzAccount
$SubscriptionID = "<YourSubscriptionIdHere>"
Select-AzSubscription -SubscriptionName $SubscriptionID
```

To monitor operations on a database, including scaling operations, use [Get-AzSqlDatabaseActivity](/powershell/module/az.sql/get-azsqldatabaseactivity). The following sample returns an `OperationId` for each operation currently executing.

```powershell
Get-AzSqlDatabaseActivity -ResourceGroupName "ResourceGroup01" -ServerName "Server01" -DatabaseName "Database01"
```

To cancel an asynchronous operation like a database scale, identify the operation then use [Stop-AzSqlDatabaseActivity](/powershell/module/az.sql/stop-azsqldatabaseactivity) with a specific `OperationId`, as in the following sample.

```powershell
Stop-AzSqlDatabaseActivity -ResourceGroupName "ResourceGroup01" -ServerName "Server01" -DatabaseName "Database01" -OperationId af97005d-9243-4f8a-844e-402d1cc855f5
```

### [Azure CLI](#tab/azure-cli)

From a Cloud shell terminal, use the following sample command to identify operations currently executing. From a Cloud shell terminal, set the `$resourceGroupName`, `$serverName`, and `$databaseName` variables, and then run the following command:

```azurecli
$resourceGroupName = "<resource group name>"
$serverName = "<server name>"
$databaseName = "<sql database name>"
az sql db op list --resource-group $resourceGroupName --server $serverName --database $databaseName --query "[?state=='InProgress'].name" --out tsv
```

To stop an asynchronous operation like a database scale, from a Cloud shell terminal, set the `$resourceGroupName`, `$serverName`, and `$databaseName` variables, and then run the following command:

```azurecli
$resourceGroupName = "<resource group name>"
$serverName = "<server name>"
$databaseName = "<sql database name>"
$operationName = (az sql db op list --resource-group $resourceGroupName --server $serverName --database $databaseName --query "[?state=='InProgress'].name" --out tsv)
if (-not [string]::IsNullOrEmpty($operationName)) {
    (az sql db op cancel --resource-group $resourceGroupName --server $serverName --database $databaseName --name $operationName)
        "Operation " + $operationName + " has been canceled"
}
else {
    "No service tier change or compute rescaling operation found"
}
```

---

## Permissions

To scale databases via Transact-SQL, `ALTER DATABASE` is used. To scale a database a login must be either the server admin login (created when the Azure SQL Database logical server was provisioned), the Microsoft Entra admin of the server, a member of the dbmanager database role in `master`, a member of the db_owner database role in the current database, or `dbo` of the database. For more information, see [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql?view=azuresqldb-current&preserve-view=true#permissions-1).

To scale databases via the Azure portal, PowerShell, Azure CLI, or REST API, Azure RBAC permissions are needed, specifically the Contributor, SQL DB Contributor role, or SQL Server Contributor Azure RBAC roles. For more information, visit [Azure RBAC built-in roles](/azure/role-based-access-control/built-in-roles).


## Additional considerations

- If you're upgrading to a higher service tier or compute size, the database max size doesn't increase unless you explicitly specify a larger size (maxsize).
- To downgrade a database, the database used space must be smaller than the maximum allowed size of the target service tier and compute size.
- When downgrading from **Premium** to the **Standard** tier, an extra storage cost applies if both (1) the max size of the database is supported in the target compute size, and (2) the max size exceeds the included storage amount of the target compute size. For example, if a P1 database with a max size of 500 GB is downsized to S3, then an extra storage cost applies since S3 supports a max size of 1 TB and its included storage amount is only 250 GB. So, the extra storage amount is 500 GB – 250 GB = 250 GB. For pricing of extra storage, see [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/). If the actual amount of space used is less than the included storage amount, then this extra cost can be avoided by reducing the database max size to the included amount.
- When upgrading a database with [geo-replication](active-geo-replication-configure-portal.md) enabled, upgrade its secondary databases to the desired service tier and compute size before upgrading the primary database (general guidance for best performance). When upgrading to a different edition, it's a requirement that the secondary database is upgraded first.
- When downgrading a database with [geo-replication](active-geo-replication-configure-portal.md) enabled, downgrade its primary databases to the desired service tier and compute size before downgrading the secondary database (general guidance for best performance). When downgrading to a different edition, it's a requirement that the primary database is downgraded first.
- The restore service offerings are different for the various service tiers. If you're downgrading to the **Basic** tier, there's a lower backup retention period. See [Azure SQL Database Backups](automated-backups-overview.md).
- The new properties for the database aren't applied until the changes are complete.
- When data copying is required to scale a database (see [Latency](#latency)) when changing the service tier, high resource utilization concurrent to the scaling operation may cause longer scaling times. With [Accelerated Database Recovery (ADR)](/sql/relational-databases/accelerated-database-recovery-concepts), rollback of long running transactions is not a significant source of delay, but high concurrent resource usage may leave less compute, storage, and network bandwidth resources for scaling, particularly for smaller compute sizes.

## Billing

You're billed for each hour a database exists using the highest service tier + compute size that applied during that hour, regardless of usage or whether the database was active for less than an hour. For example, if you create a single database and delete it five minutes later your bill reflects a charge for one database hour.

## Change storage size

### vCore-based purchasing model

- Storage can be provisioned up to the data storage max size limit using 1-GB increments. The minimum configurable data storage is 1 GB. For data storage max size limits in each service objective, see resource limit documentation pages for [Resource limits for single databases using the vCore purchasing model](resource-limits-vcore-single-databases.md) and [Resource limits for single databases using the DTU purchasing model](resource-limits-dtu-single-databases.md).
- Data storage for a single database can be provisioned by increasing or decreasing its max size using the [Azure portal](https://portal.azure.com), [Transact-SQL](/sql/t-sql/statements/alter-database-transact-sql#examples-1), [PowerShell](/powershell/module/az.sql/set-azsqldatabase), [Azure CLI](/cli/azure/sql/db#az-sql-db-update), or [REST API](/rest/api/sql/databases/update). If the max size value is specified in bytes, it must be a multiple of 1 GB (1073741824 bytes).
- The amount of data that can be stored in the data files of a database is limited by the configured data storage max size. In addition to that storage, Azure SQL Database automatically adds 30% more storage to be used for the transaction log. The price of storage for a single database or an elastic pool is the sum of data storage and transaction log storage amounts multiplied by the storage unit price of the service tier. For example, if data storage is set to _10 GB_, the additional transaction log storage is _10 GB * 30% = 3 GB_, and the total amount of billable storage is _10 GB + 3 GB = 13 GB_.

  > [!NOTE]
  > The maximum size of the transaction log file is managed automatically, and in some cases can be greater than 30% of the data storage maximum size. This does not increase the price of storage for the database.

- Azure SQL Database automatically allocates 32 GB per vCore for the `tempdb` database. `tempdb` is located on the local SSD storage in all service tiers. The cost of `tempdb` is included in the price of a single database or an elastic pool.
- For details on storage price, see [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/).

> [!IMPORTANT]
> Under some circumstances, you may need to shrink a database to reclaim unused space. For more information, see [Manage file space in Azure SQL Database](file-space-manage.md).

### DTU-based purchasing model

- The DTU price for a single database includes a certain amount of storage at no additional cost. Extra storage beyond the included amount can be provisioned for an additional cost up to the max size limit in increments of 250 GB up to 1 TB, and then in increments of 256 GB beyond 1 TB. For included storage amounts and max size limits, see [Single database: Storage sizes and compute sizes](resource-limits-dtu-single-databases.md#single-database-storage-sizes-and-compute-sizes).
- Extra storage for a single database can be provisioned by increasing its max size using the Azure portal, [Transact-SQL](/sql/t-sql/statements/alter-database-transact-sql#examples-1), [PowerShell](/powershell/module/az.sql/set-azsqldatabase), the [Azure CLI](/cli/azure/sql/db#az-sql-db-update), or the [REST API](/rest/api/sql/databases/update).
- The price of extra storage for a single database is the extra storage amount multiplied by the extra storage unit price of the service tier. For details on the price of extra storage, see [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/).

> [!IMPORTANT]
> Under some circumstances, you may need to shrink a database to reclaim unused space. For more information, see [Manage file space in Azure SQL Database](file-space-manage.md).

### Geo-replicated database

To change the database size of a replicated secondary database, change the size of the primary database. This change will then be replicated and implemented on the secondary database as well.

## P11 and P15 constraints when max size greater than 1 TB

More than 1 TB of storage in the Premium tier is currently available in all regions except: China East, China North, Germany Central, and Germany Northeast. In these regions, the storage max in the Premium tier is limited to 1 TB. The following considerations and limitations apply to P11 and P15 databases with a maximum size greater than 1 TB:

- If the max size for a P11 or P15 database was ever set to a value greater than 1 TB, then can it only be restored or copied to a P11 or P15 database.  Subsequently, the database can be rescaled to a different compute size provided the amount of space allocated at the time of the rescaling operation doesn't exceed max size limits of the new compute size.
- For active geo-replication scenarios:
  - Setting up a geo-replication relationship: If the primary database is P11 or P15, the secondary(ies) must also be P11 or P15. Lower compute sizes are rejected as secondaries since they aren't capable of supporting more than 1 TB.
  - Upgrading the primary database in a geo-replication relationship: Changing the maximum size to more than 1 TB on a primary database triggers the same change on the secondary database. Both upgrades must be successful for the change on the primary to take effect. Region limitations for the more than 1-TB option apply. If the secondary is in a region that doesn't support more than 1 TB, the primary isn't upgraded.
- Using the Import/Export service for loading P11/P15 databases with more than 1 TB isn't supported. Use SqlPackage to [import](database-import.md) and [export](database-export.md) data.

## Related content

For overall resource limits, see [Azure SQL Database vCore-based resource limits - single databases](resource-limits-vcore-single-databases.md) and [Azure SQL Database DTU-based resource limits - single databases](resource-limits-dtu-single-databases.md).
