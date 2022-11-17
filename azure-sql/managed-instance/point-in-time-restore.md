---
title: Point-in-time restore
titleSuffix: Azure SQL Managed Instance
description: Restore a database in your Azure SQL Managed Instance deployment to a previous point in time.
author: MilanMSFT
ms.author: mlazic
ms.reviewer: mathoma, nvraparl
ms.date: 08/25/2019
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: how-to
ms.custom: devx-track-azurepowershell
---
# Restore a database in Azure SQL Managed Instance to a previous point in time

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

You can use point-in-time restore to create a database that's a copy of a different database at a specific, earlier point in time. This article describes how to do a point-in-time restore of a database in your Azure SQL Managed Instance deployment.

> [!NOTE]
> The [Create or Update v02.01.2022](/rest/api/sql/2022-02-01-preview/managed-databases/create-or-update) API is deprecated. Beginning in January 2022, use the [Create or Update v5.0.2022](/rest/api/sql/2022-05-01-preview/managed-databases/create-or-update) replacement API call for all database restore operations.

## Scenarios

Point-in-time restore is useful in recovery scenarios, like in an incident that's caused by errors, data that doesn't load correctly, or if crucial data is deleted. You can also use it simply to test and audit your database deployment. The Azure backup files are kept for 7 to 35 days depending on your database settings.

Point-in-time restore can restore a database in these scenarios:

- From an existing database
- From a deleted database
- To the same managed instance or to a different managed instance
- To a managed instance in the same subscription or to a managed instance in a different subscription

The following table shows point-in-time restore scenarios for SQL Managed Instance:

| Scenario | Azure portal | Azure CLI | PowerShell |
|:----------|:----------|:----------|:----------|
| Restore an existing database to the same managed instance | Yes | Yes | Yes|
| Restore an existing database to a different managed instance | Yes |  Yes | Yes |
| Restore a dropped database to the same managed instance | Yes | No| Yes |
| Restore a dropped database to a different managed instance | Yes | No | Yes |
| Restore an existing database to a managed instance in another subscription | Yes | No | No |
| Restore a dropped database to a managed instance in another subscription | Yes | No | No |

## Limitations

These limitations apply to point-in-time restore in SQL Managed Instance:

- You can't use point-in-time restore to recover an entire SQL Managed Instance deployment. Use point-in-time restore only to make a copy of a database that's hosted on SQL Managed Instance.

- Limitations in point-in-time restore depend on whether you're restoring your database to a managed instance in the same subscription or to a managed instance in a different subscription.

- If you enable [service endpoint policies](/service-endpoint-policies-configure.md) for your managed instance, placing a service endpoint policy on a subnet prevents point-in-time restore from a managed instance that's in a different subnet.

> [!WARNING]
> Be aware of the storage size of your managed instance. Depending on size of the data to be restored, you might run out of storage for your managed instance. If you don't have enough storage space in your managed instance for the restored data, use a different approach.

### Restore to same subscription

If you restore from one managed instance to another managed instance in the same Azure subscription, both managed instances must be in the same region. Currently, cross-region restore isn't supported.

### Restore to a different subscription

Restoring a point-in-time restore backup across subscriptions has the following limitations:

- Both subscriptions must be in the same region.
- Both subscriptions must be in the same tenant.
- The subscription type must be either EA, CSP, MCA, or PayGo.
- You can do a restore operation only only on the primary instance.
- Geo-replicated backups aren't currently supported for cross-subscription point-in-time restore.
- The user performing the restore must either be part of the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles.md#sql-managed-instance-contributor) role or have the following explicit permissions: **crossSubscription/action**, **readBackups/action**.
- If you're bringing your own key (BYOK), then the key must be present in both subscriptions.

## Restore an existing database

Restore an existing database in the same subscription by using the Azure portal, PowerShell, or the Azure CLI. If you're restoring to a different instance in the same subscription and using PowerShell or the Azure CLI, be sure to specify the properties for the target SQL Managed Instance or the database will be restored to the same instance by default.

Restoring your database to an instance in a different subscription is currently only possible by using the Azure portal. If you're restoring to a different subscription, the [Create or Update v5.0.2022](/rest/api/sql/2022-05-01-preview/managed-databases/create-or-update) API call underlying the restore action must contain `restorePointInTime`, `crossSubscriptionTargetManagedInstanceId`, and `crossSubscriptionSourceDatabaseId` *or* `crossSubscriptionRestorableDroppedDatabaseId`.

# [Portal](#tab/azure-portal)

1. Sign in to the [Azure portal](https://portal.azure.com).
1. In SQL Managed Instance, go to your source managed instance.
1. In **Managed Instance databases**, select the database you want to restore.

   :::image type="content" source="media/point-in-time-restore/choose-database-to-restore.png" alt-text="Screenshot of the Azure portal, SQL Managed Instance overview blade, with a database selected. ":::

1. Select **Restore** on the database page to open the **Create Azure SQL Managed Database** page:

    :::image type="content" source="./media/point-in-time-restore/restore-database-to-mi.png" alt-text="Screenshot of the Azure portal, databases page, with Restore a database selected.":::

1. On the **Basics** tab under **Project details**, select the target destination subscription and resource group that contains the managed instance you want to restore the database to.
1. Under **Database details**, provide the new name your restored database will use, and the target SQL Managed Instance you want to restore the database to.
1. On the **Data source** tab, select **Continuous backup**, and then choose the subscription and resource group from the source database you want to restore.
1. Once you provide the source subscription and resource group, choose the database you want to restore from the **Managed Database** dropdown and then select the point in time restore point you want to restore the database from.
1. On the **Additional settings** tab, you can check the box if you want your restored database to inherit the retention settings of the source database, or you can uncheck the box and specify a new retention policy by selecting **Configure retention**.
1. Select **Review + Create**  to validate your database restore settings and then select **Create** to restore your database. This action starts the restore process, which creates a new database and populates it with data from the original database at the specified point in time. For more information about the recovery process, see [Recovery time](../database/recovery-using-backups.md#recovery-time).

# [PowerShell](#tab/azure-powershell)

If you don't already have Azure PowerShell installed, see [Install the Azure PowerShell module](/powershell/azure/install-az-ps).

To restore the database by using PowerShell, specify your values for the parameters in the following command. Then, run the command:

```powershell-interactive
$subscriptionId = "<Subscription ID>"
$resourceGroupName = "<Resource group name>"
$managedInstanceName = "<SQL Managed Instance name>"
$databaseName = "<Source-database>"
$pointInTime = "2018-06-27T08:51:39.3882806Z"
$targetDatabase = "<Name of new database to be created>"

Get-AzSubscription -SubscriptionId $subscriptionId
Select-AzSubscription -SubscriptionId $subscriptionId

Restore-AzSqlInstanceDatabase -FromPointInTimeBackup `
                              -ResourceGroupName $resourceGroupName `
                              -InstanceName $managedInstanceName `
                              -Name $databaseName `
                              -PointInTime $pointInTime `
                              -TargetInstanceDatabaseName $targetDatabase `
```

To restore the database to another SQL Managed Instance, also specify the names of the target resource group and target SQL Managed Instance:  

```powershell-interactive
$targetResourceGroupName = "<Resource group of target SQL Managed Instance>"
$targetInstanceName = "<Target SQL Managed Instance name>"

Restore-AzSqlInstanceDatabase -FromPointInTimeBackup `
                              -ResourceGroupName $resourceGroupName `
                              -InstanceName $managedInstanceName `
                              -Name $databaseName `
                              -PointInTime $pointInTime `
                              -TargetInstanceDatabaseName $targetDatabase `
                              -TargetResourceGroupName $targetResourceGroupName `
                              -TargetInstanceName $targetInstanceName 
```

For details, see [Restore-AzSqlInstanceDatabase](/powershell/module/az.sql/restore-azsqlinstancedatabase).

# [Azure CLI](#tab/azure-cli)

If you don't already have the Azure CLI installed, see [Install the Azure CLI](/cli/azure/install-azure-cli).

To restore the database by using the Azure CLI, specify your values for the parameters in the following command. Then, run the command:

```azurecli-interactive
az sql midb restore -g mygroupname --mi myinstancename |
-n mymanageddbname --dest-name targetmidbname --time "2018-05-20T05:34:22"
```

To restore the database to another SQL Managed Instance, also specify the names of the target resource group and SQL Managed Instance:  

```azurecli-interactive
az sql midb restore -g mygroupname --mi myinstancename -n mymanageddbname |
       --dest-name targetmidbname --time "2018-05-20T05:34:22" |
       --dest-resource-group mytargetinstancegroupname |
       --dest-mi mytargetinstancename
```

For a detailed explanation of the available parameters, see the [CLI documentation for restoring a database in a SQL Managed Instance](/cli/azure/sql/midb#az-sql-midb-restore).

---

## Restore a deleted database

You can restore a deleted database by using the Azure portal or PowerShell.

### Portal

To recover a deleted managed database by using the Azure portal:

1. In the Azure portal, go to your source managed instance.
1. In the left menu under **Data management**, select **Backups**.
1. Under **Show databases**, select **Deleted**.
1. Select **Restore** for the deleted database you want to restore.

  :::image type="content" source="./media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png" alt-text="Screenshot of Azure portal, databases page, with restore deleted Azure SQL instance database highlighted." lightbox="./media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png":::

1. In **Create Azure SQL Managed database** under **Basics**, enter or select details for the target managed instance you want to restore your database to.
1. In **Data source**, enter or select details for your source database.
1. In **Additional settings**, configure retention settings.
1. In **Review + Create**, select **Create** to restore your deleted database.

### PowerShell

To restore a deleted database to the same managed instance, update the parameter values and then run the following PowerShell command:

```powershell
$subscriptionId = "<subscription ID>"
Get-AzSubscription -SubscriptionId $subscriptionId
Select-AzSubscription -SubscriptionId $subscriptionId

$resourceGroupName = "<resource group name>"
$managedInstanceName = "<managed instance name>"
$deletedDatabaseName = "<source database name>"
$targetDatabaseName = "<target database name>"

$deletedDatabase = Get-AzSqlDeletedInstanceDatabaseBackup -ResourceGroupName $resourceGroupName `
-InstanceName $managedInstanceName -DatabaseName $deletedDatabaseName

Restore-AzSqlinstanceDatabase -FromPointInTimeBackup -Name $deletedDatabase.Name `
   -InstanceName $deletedDatabase.ManagedInstanceName `
   -ResourceGroupName $deletedDatabase.ResourceGroupName `
   -DeletionDate $deletedDatabase.DeletionDate `
   -PointInTime UTCDateTime `
   -TargetInstanceDatabaseName $targetDatabaseName
```

To restore the database to a different managed instance, also specify the names of the target resource group and the target managed instance:

```powershell
$targetResourceGroupName = "<resource group of target managed instance>"
$targetInstanceName = "<target managed instance name>"

Restore-AzSqlinstanceDatabase -FromPointInTimeBackup -Name $deletedDatabase.Name `
   -InstanceName $deletedDatabase.ManagedInstanceName `
   -ResourceGroupName $deletedDatabase.ResourceGroupName `
   -DeletionDate $deletedDatabase.DeletionDate `
   -PointInTime UTCDateTime `
   -TargetInstanceDatabaseName $targetDatabaseName `
   -TargetResourceGroupName $targetResourceGroupName `
   -TargetInstanceName $targetInstanceName 
```

## Overwrite an existing database

To overwrite an existing database:

1. Delete the existing database that you want to overwrite.
2. Rename the point-in-time restore database to the name of the database you deleted.

### Delete the original database

You can delete the database by using the Azure portal, PowerShell, or the Azure CLI.

Another option to delete the database is to connect to your managed instance directly, open SQL Server Management Studio, and then use the `DROP` Transact-SQL (T-SQL) command:

```sql
DROP DATABASE WorldWideImporters;
```

Use one of the following methods to connect to the database in your managed instance:

- [SQL Server Management Studio and Azure Data Studio via an Azure virtual machine](./connect-vm-instance-configure.md)
- [Point-to-site](./point-to-site-p2s-configure.md)
- [Public endpoint](./public-endpoint-configure.md)

# [Portal](#tab/azure-portal)

In the Azure portal, select the database in your your managed instance. In the menu bar, select **Delete**.

:::image type="content" source="./media/point-in-time-restore/delete-database-from-mi.png" alt-text="Screenshot that shows how to delete a database by using the Azure portal.":::

# [PowerShell](#tab/azure-powershell)

Use the following PowerShell command to delete an existing database from your managed instance:

```powershell
$resourceGroupName = "<resource group name>"
$managedInstanceName = "<managed instance name>"
$databaseName = "<source database name>"

Remove-AzSqlInstanceDatabase -Name $databaseName -InstanceName $managedInstanceName -ResourceGroupName $resourceGroupName
```

# [Azure CLI](#tab/azure-cli)

Use the following Azure CLI command to delete an existing database from your managed instance:

```azurecli-interactive
az sql midb delete -g mygroupname --mi myinstancename -n mymanageddbname
```

---

### Change the new database name to match the original database name

Connect directly to your managed instance and start SQL Server Management Studio. Then, run the following T-SQL query. The query changes the name of the restored database to the name of the deleted database you intend to overwrite.

```sql
ALTER DATABASE WorldWideImportersPITR MODIFY NAME = WorldWideImporters;
```

Use one of the following methods to connect to the database in your managed instance:

- [Azure virtual machine](./connect-vm-instance-configure.md)
- [Point-to-site](./point-to-site-p2s-configure.md)
- [Public endpoint](./public-endpoint-configure.md)

## Next steps

Learn about [automated backups](../database/automated-backups-overview.md).
