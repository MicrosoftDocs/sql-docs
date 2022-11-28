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
- The subscription type must be either Enterprise Agreement, Cloud Solution Provider, Microsoft Certified Partner, or pay-as-you-go.
- You can use the restore action only on the primary instance.
- Geo-replicated backups currently aren't supported for cross-subscription point-in-time restore.
- The user who takes the restore action must either have the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles.md#sql-managed-instance-contributor) role assignment or have these explicit permissions: `crossSubscription/action` and `readBackups/action`.
- If you bring your own key (BYOK), the key must be present in both subscriptions.

## Restore an existing database

Restore an existing database in the same subscription by using the Azure portal, PowerShell, or the Azure CLI. If you're restoring to a different instance in the same subscription and using PowerShell or the Azure CLI, be sure to specify the properties for the target SQL Managed Instance resource or the database is restored to the same instance by default.

Currently, to restore your database to an instance in a different subscription, you must use the Azure portal. If you restore to a different subscription, the [Create or Update v5.0.2022](/rest/api/sql/2022-05-01-preview/managed-databases/create-or-update) API call that underlies the restore action must contain `restorePointInTime`, `crossSubscriptionTargetManagedInstanceId`, and `crossSubscriptionSourceDatabaseId` *or* `crossSubscriptionRestorableDroppedDatabaseId`.

# [Portal](#tab/azure-portal)

1. Sign in to the [Azure portal](https://portal.azure.com).
1. In SQL Managed Instance, go to your source managed instance.
1. In **Managed Instance databases**, select the database you want to restore.

   :::image type="content" source="media/point-in-time-restore/choose-database-to-restore.png" alt-text="Screenshot that shows the SQL Managed Instance overview pane in the Azure portal, with a database selected. ":::

1. In the command bar, select **Restore**:

    :::image type="content" source="./media/point-in-time-restore/restore-database-to-mi.png" alt-text="Screenshot that shows a database overview pane in the Azure portal, with the Restore button highlighted.":::

1. In **Create Azure SQL Managed Database**, under **Project details**, select the target destination subscription and resource group that contains the managed instance you want to restore the database to.
1. Under **Database details**, enter a new name for your restored database and the target managed instance you want to restore the database to. Select **Next**.
1. In **Data source**, select **Continuous backup**. Then, select the subscription and resource group from the source database you want to restore.
1. In the **Managed Database** dropdown, select the database you want to restore. Then, select the point-in-time restore point you want to restore the database from. Select **Next**.
1. In **Additional settings** tab, either select or clear the option for your restored database to inherit the retention settings of the source database. If you don't use this option, select **Configure retention** to set a new retention policy. Select **Next**.
1. Select **Review + create**  to validate your database restore settings. After successful validation, select **Create** to restore your database.

   This action starts the restore process, which creates a new database and populates it with data from the original database at the specified point in time. For more information about the recovery process, see [Recovery time](../database/recovery-using-backups.md#recovery-time).

# [PowerShell](#tab/azure-powershell)

1. Open Azure PowerShell. For more information, see [Install the Azure PowerShell module](/powershell/azure/install-az-ps).

To restore the database by using PowerShell, run the following code with your values substituted for the parameters:

```powershell-interactive
$subscriptionId = "<subscription ID>"
$resourceGroupName = "<resource group name>"
$managedInstanceName = "<managed instance name>"
$databaseName = "<source database>"
$pointInTime = "2018-06-27T08:51:39.3882806Z"
$targetDatabase = "<name of the new database to create>"

Get-AzSubscription -SubscriptionId $subscriptionId
Select-AzSubscription -SubscriptionId $subscriptionId

Restore-AzSqlInstanceDatabase -FromPointInTimeBackup `
                              -ResourceGroupName $resourceGroupName `
                              -InstanceName $managedInstanceName `
                              -Name $databaseName `
                              -PointInTime $pointInTime `
                              -TargetInstanceDatabaseName $targetDatabase `
```

To restore the database to another managed instance, also specify the names of the target resource group and the target managed instance:  

```powershell-interactive
$targetResourceGroupName = "<resource group of the target managed instance>"
$targetInstanceName = "<name of the target managed instance>"

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

To restore the database by using the Azure CLI:

1. Open the Azure CLI. For more information, see [Install the Azure CLI](/cli/azure/install-azure-cli).

1. Run one of the following code options with your values substituted for the parameters:

   To restore the database in the same managed instance:

   ```azurecli-interactive
   az sql midb restore -g mygroupname --mi myinstancename |
   -n mymanageddbname --dest-name targetmidbname --time "2018-05-20T05:34:22"
   ```
  
   To restore the database to a different managed instance, you also specify the names of the target resource group and the managed instance:  

   ```azurecli-interactive
   az sql midb restore -g mygroupname --mi myinstancename -n mymanageddbname |
          --dest-name targetmidbname --time "2018-05-20T05:34:22" |
          --dest-resource-group mytargetinstancegroupname |
          --dest-mi mytargetinstancename
   ```

For a detailed explanation of available parameters, see the [CLI documentation for restoring a database in SQL Managed Instance](/cli/azure/sql/midb#az-sql-midb-restore).

---

## Restore a deleted database

You can restore a deleted database by using the Azure portal or PowerShell. Currently, you can't use the Azure CLI to restore a deleted database.

### Portal

To recover a deleted managed database by using the Azure portal:

1. In the Azure portal, go to your source managed instance.
1. In the left menu under **Data management**, select **Backups**.
1. Under **Show databases**, select **Deleted**.
1. Select **Restore** for the deleted database you want to restore.

  :::image type="content" source="./media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png" alt-text="Screenshot that shows available databases in the portal, with the Restore button highlighted to restore a deleted database." lightbox="./media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png":::

1. In **Create Azure SQL Managed database**, enter or select details for the target managed instance you want to restore your database to. Select **Next**.
1. In **Data source**, enter or select details for your source database. Select **Next***.
1. In **Additional settings**, configure retention settings. Select **Next**.
1. In **Review + create**, select **Create** to restore your deleted database.

### PowerShell

Run one of the following code options with your values substituted for the parameters:

To restore a deleted database to the same managed instance:

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

To restore the database to a different managed instance, you also specify the names of the target resource group and the target managed instance:

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

In the Azure portal, select the database in your managed instance. In the command bar, select **Delete**.

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

Connect directly to your managed instance and start SQL Server Management Studio. Then run the following T-SQL query. The query changes the name of the restored database to the name of the deleted database you intend to overwrite.

```sql
ALTER DATABASE WorldWideImportersPITR MODIFY NAME = WorldWideImporters;
```

Use one of the following methods to connect to the database in your managed instance:

- [Azure virtual machine](./connect-vm-instance-configure.md)
- [Point-to-site](./point-to-site-p2s-configure.md)
- [Public endpoint](./public-endpoint-configure.md)

## Next steps

Learn about [automated backups](../database/automated-backups-overview.md).
