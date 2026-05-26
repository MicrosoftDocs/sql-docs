---
title: Convert a Database to Hyperscale
description: How to convert an Azure SQL Database to the Hyperscale tier.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 05/18/2026
ms.service: azure-sql-database
ms.topic: how-to
ms.custom:
  - devx-track-azurepowershell
  - devx-track-azurecli
monikerRange: "=azuresql || =azuresql-db"
---

# Convert an existing database to Hyperscale

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

You can convert an existing database in Azure SQL Database to Hyperscale by using the Azure portal, the Azure CLI, PowerShell, or Transact-SQL.

## Prerequisites

- To convert a database that uses [geo-replication](active-geo-replication-overview.md) or is part of a [failover group](failover-group-sql-db.md) to Hyperscale, start by converting the primary replica. The geo-secondary replica is converted automatically. You can [convert a geo-replicated non-Hyperscale database to Hyperscale](#convert-database-with-geo-replicas) by using T-SQL, REST API, PowerShell, or Azure CLI.

- Direct conversion from the Basic service tier to Hyperscale isn't supported. To perform this conversion, first change the database to any service tier other than Basic (for example, General Purpose), and then proceed with the conversion to Hyperscale.

- Direct movement of a database from a zone-redundant elastic pool to a non–zone-redundant Hyperscale pool is not supported. To complete this transition, first remove the database from the elastic pool and convert it to a standalone database. Next, disable zone redundancy for the database. Once these steps are complete, the database can be added to a new or existing non–zone-redundant Hyperscale pool.

- You can monitor the progress of the conversion with T-SQL. To run T-SQL commands on your Azure SQL Database, use [SQL Server Management Studio (SSMS)](https://aka.ms/ssms), the [MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code), [sqlcmd](/sql/tools/sqlcmd/sqlcmd-utility), or your favorite T-SQL querying tool.

### Convert database with geo-replicas

When you convert a database in a [geo-replication](active-geo-replication-overview.md) relationship, the conversion process preserves the geo-replication link. Both the primary and the geo-secondary databases are converted to Hyperscale together.

- You must start conversion to Hyperscale by converting the primary geo-replica. Attempting to convert a geo-secondary replica results in an error: *A geo-secondary replica 'database-name-placeholder' cannot be converted to Hyperscale. To convert both the primary and geo-secondary replicas to Hyperscale, retry the operation on the primary replica.*
- Reduce the number of geo-secondary replicas to one to initiate the conversion process. 
- Creating geo-replica of a geo-replica (also known as "geo-replica chaining") isn't supported in Hyperscale. If a chained geo-replication configuration exists, remove it before starting the conversion to Hyperscale.
- A planned failover isn't possible while the conversion of the geo-primary database to Hyperscale is in progress. A forced failover to a geo-secondary replica is possible. However, depending on the state of the conversion when the forced failover occurs, the new geo-primary after failover might use either the Hyperscale service tier, or its original service tier.
- If a geo-primary database is in an elastic pool, you can move it to an existing Hyperscale elastic pool as part of the conversion or make it a standalone Hyperscale database. However, if a geo-secondary database is in an elastic pool, conversion to Hyperscale always moves it out of the pool. You can move the geo-secondary database to a Hyperscale elastic pool in a separate step once conversion completes.

## Cutover

The conversion process has two stages: **conversion** while the database is online, and then **cutover** to the new Hyperscale database.   

- The time required to move an existing database to Hyperscale consists of the time to copy data and the time to replay the changes made in the source database while copying data. While the data copy time scales roughly with the size of the database, actual copy speed can vary due to factors such as network throughput, I/O bandwidth, storage latency, and transient service load. We recommend converting to Hyperscale during a lower write activity period so that the time to replay accumulated changes is shorter. It is recommended to use manual cutover to control the next stage.

- You have the ability to choose when the cutover occurs - as soon as the database is ready, or manually at a time of your choosing. By default, the process to convert to Hyperscale will cut over automatically.
   - If you choose to manually cutover at a time of your choosing, you have 24 hours to initiate a manual cutover after the point when the database is ready for cutover. You can initiate a manual cutover via the Azure portal, Azure CLI, PowerShell, or T-SQL.
- While the database is in a ready-for-cutover state, the system continues to copy and replay the changes made in the source database, but switches from asynchronous to synchronous copy. This synchronous copy can increase write latency under write-intensive workloads until cutover occurs.
- During the final cutover to Hyperscale, your applications only experience a short period of downtime, usually less than a minute.

You can monitor multiple phases in the conversion process in the Azure portal (on the progress reporting page), via Azure CLI ([az sql db op list](/cli/azure/sql/db/op#az-sql-db-op-list)), PowerShell ([Get-AzSqlDatabaseActivity](/powershell/module/az.sql/get-azsqldatabaseactivity)), or using T-SQL ([sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database)).

When you convert a database from the Premium or Business Critical service tiers to Hyperscale, the process disconnects existing client connections during phase 1. This disconnection is similar to the disconnect that occurs when scaling the database between service tiers. Applications should be designed to gracefully handle transient connectivity interruptions by implementing retry logic as described in [Retry logic for transient errors](/azure/azure-sql/database/troubleshoot-common-connectivity-issues#retry-logic-for-transient-errors).

## Convert a database to Hyperscale

To convert an existing Azure SQL Database to Hyperscale, first identify your target service objective. 

Review [resource limits for single databases](resource-limits-vcore-single-databases.md) if you're not sure which service objective is right for your database. In many cases, you can choose a service objective with the same number of vCores and the same hardware generation as the original database. If needed, you can [change the service objective](scale-resources.md) later with minimal downtime. Billing for Hyperscale begins only after cutover.

Select the tab for your preferred method to convert your database:

# [Portal](#tab/azure-portal)

The Azure portal enables you to convert to Hyperscale by modifying the service tier for your database.

:::image type="content" source="media/convert-to-hyperscale/service-tier-dropdown-azure-sql-database-azure-portal.png" alt-text="Screenshot of the compute + storage panel of a database in Azure SQL Database. The service tier dropdown is expanded, displaying the option for the Hyperscale service tier." lightbox="media/convert-to-hyperscale/service-tier-dropdown-azure-sql-database-azure-portal.png":::

1. Go to the database you want to convert in the Azure portal.
1. In the left navigation bar, select **Compute + storage**.
1. Select the **Service tier** dropdown list to expand the options for service tiers.
    1. If you were using the [Azure SQL Database free offer](free-offer.md), select the button to remove the **Free database offer**. Then you see the **Service tier** dropdown list.
1. Select **Hyperscale** from the dropdown list.
1. Review the **Compute tier** and choose **Provisioned** or **Serverless**. 
1. Review the **Cutover mode**, a choice specific to conversion to Hyperscale.
    - The cutover occurs after the database is prepared for conversion to Hyperscale. **Cutover mode** determines when connectivity to the existing Azure SQL Database is momentarily disrupted for the conversion to Hyperscale:
        - **Automatic cutover** performs the cutover as soon as the Hyperscale database is ready. 
        - **Manual cutover** prompts you to initiate the cutover at a time of your choice in the Azure portal. This option is most useful to time the cutover for minimal business disruption.
1. Review the **Hardware Configuration** listed. If desired, select **Change configuration** to select the appropriate hardware configuration for your workload.
1. Select the **vCores** slider if you want to change the number of vCores available for your database under the Hyperscale service tier.
1. Select the **High-Availability Secondary Replicas** slider if you want to change the number of replicas under the Hyperscale service tier. 
1. Select **Apply**.
1. Monitor the conversion in the Azure portal. 
    1. Go to the database in the Azure portal.
    1. In the left navigation bar, select **Overview**.
    1. Review the **Notifications** section at the bottom of the right pane. If operations are ongoing, a notification box appears.
    1. Select the notification box to view details.
    1. The **Ongoing operations** pane opens. Review the details of the ongoing operations.

If you selected **Manual cutover**, the Azure portal presents you a **Cutover** button when ready.

:::image type="content" source="media/convert-to-hyperscale/cutover.png" alt-text="Screenshot from the Azure portal showing the Cutover button in a Hyperscale conversion.":::

# [Azure CLI](#tab/azure-cli)

This code sample calls [az sql db update](/cli/azure/sql/db#az-sql-db-update) to convert an existing Azure SQL Database to Hyperscale. You must specify both the edition and service objective. Replace `resourceGroupName`, `serverName`, `databaseName`, and `serviceObjective` with the appropriate values before running the following code sample:

```azurecli-interactive
resourceGroupName="myResourceGroup"
serverName="server01"
databaseName="mySampleDatabase"
serviceObjective="HS_Gen5_2"

az sql db update -g $resourceGroupName -s $serverName -n $databaseName \
    --edition Hyperscale --service-objective $serviceObjective
```

Use `--manual-cutover` to choose to manually initiate the cutover to Hyperscale at a time of your choice. This option is most useful to time the cutover for minimal business disruption. For example:

```azurecli-interactive
resourceGroupName="myResourceGroup"
serverName="server01"
databaseName="mySampleDatabase"
serviceObjective="HS_Gen5_2"

az sql db update -g $resourceGroupName -s $serverName -n $databaseName --edition Hyperscale --service-objective $serviceObjective --manual-cutover
```

Monitor the ongoing operation with [az sql db op list](/cli/azure/sql/db/op#az-sql-db-op-list), to return recent or ongoing operations for a database in Azure SQL Database.

```azurecli-interactive
resourceGroupName="myResourceGroup"
serverName="server01"
databaseName="mySampleDatabase"

az sql db op list -g $resourceGroupName -s $serverName -n $databaseName
```

Use `--perform-cutover` to initiate the cutover (within 24 hours) once the Hyperscale database is ready:

```azurecli-interactive
az sql db update -g $resourceGroupName -s $serverName -n $databaseName --perform-cutover
```

# [PowerShell](#tab/azure-powershell)

The following example uses the [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) cmdlet to convert an existing Azure SQL Database to Hyperscale. You must specify both the edition and service objective. Replace `$resourceGroupName`, `$serverName`, `$databaseName`, and `$serviceObjective` with the appropriate values before running this code sample:

```powershell-interactive
$resourceGroupName = "myResourceGroup"
$serverName = "server01"
$databaseName = "mySampleDatabase"
$serviceObjective = "HS_Gen5_2"

Set-AzSqlDatabase -ResourceGroupName $resourceGroupName `
    -ServerName $serverName `
    -DatabaseName $databaseName `
    -Edition "Hyperscale" `
    -RequestedServiceObjectiveName $serviceObjective
```

Use `-ManualCutover` to manually initiate the cutover at a time of your choice. This option is most useful to time the cutover for minimal business disruption. For example:

```powershell-interactive
$resourceGroupName = "myResourceGroup"
$serverName = "server01"
$databaseName = "mySampleDatabase"
$serviceObjective = "HS_Gen5_2"

Set-AzSqlDatabase -ResourceGroupName $resourceGroupName `
    -ServerName $serverName `
    -DatabaseName $databaseName `
    -Edition "Hyperscale" `
    -RequestedServiceObjectiveName $serviceObjective `
    -ManualCutover
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

Use `-PerformCutover` to initiate the cutover once the Hyperscale database is ready:

```powershell-interactive
$resourceGroupName = "myResourceGroup"
$serverName = "server01"
$databaseName = "mySampleDatabase"
$serviceObjective = "HS_Gen5_2"

Set-AzSqlDatabase -ResourceGroupName $resourceGroupName `
    -ServerName $serverName `
    -DatabaseName $databaseName `
    -Edition "Hyperscale" `
    -RequestedServiceObjectiveName $serviceObjective `
    -PerformCutover
```

# [Transact-SQL](#tab/t-sql)

To convert an existing Azure SQL Database to Hyperscale with Transact-SQL, first connect to the `master` database on your [logical SQL server](logical-servers.md) by using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) or [the mssql extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code).

You must specify both the edition and service objective in the [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql?view=azuresqldb-current&preserve-view=true) statement.

This example statement converts a database named `mySampleDatabase` to Hyperscale with the `HS_Gen5_2` service objective. Replace the database name with the appropriate value before executing the statement.

```sql
ALTER DATABASE [mySampleDatabase]
   MODIFY (EDITION = 'Hyperscale', SERVICE_OBJECTIVE = 'HS_Gen5_2');
GO
```

By default, the database performs a cutover to the Hyperscale database to finish the conversion as soon as the Hyperscale database is available. Optionally, use the `MANUAL_CUTOVER` argument to instead start a conversion that ends with a manually initiated cutover, at a time of your choice. This option is most useful to time the cutover for minimal business disruption. For example:

```sql
ALTER DATABASE [mySampleDatabase]
   MODIFY (EDITION = 'Hyperscale', SERVICE_OBJECTIVE = 'HS_Gen5_2')
   WITH MANUAL_CUTOVER;
```

To monitor all operations for a Hyperscale database, including the conversion to Hyperscale, connect to the `master` database of your geo-primary [logical server](logical-servers.md) and execute T-SQL queries on the [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database?view=azuresqldb-current&preserve-view=true) dynamic management view. The `sys.dm_operation_status` reports the progress of database operations including the conversion to Hyperscale. If you chose `MANUAL_CUTOVER`, the `sys.dm_operation_status` view includes additional information.

For example,

```sql
SELECT *
FROM sys.dm_operation_status
WHERE major_resource_id = 'mySampleDatabase'
ORDER BY start_time DESC;
```

When ready for manual cutover, the `phase_desc` is `WaitingForCutover`. Use the `PERFORM_CUTOVER` argument to initiate the cutover:

```sql
ALTER DATABASE [mySampleDatabase] PERFORM_CUTOVER;
```

---

## Related content

- [How to manage a Hyperscale database](manage-hyperscale-database.md)
- [Reverse migrate a database from Hyperscale](reverse-migrate-from-hyperscale.md)
