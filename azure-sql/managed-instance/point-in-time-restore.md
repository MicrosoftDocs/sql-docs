---
title: Point-in-time restore (PITR)
titleSuffix: Azure SQL Managed Instance
description: Restore a database on Azure SQL Managed Instance to a previous point in time.
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

Use point-in-time restore (PITR) to create a database as a copy of another database from some time in the past. This article describes how to do a point-in-time restore of a database in Azure SQL Managed Instance.

> [!NOTE]
> The [Create or Update v02.01.2022](/rest/api/sql/2022-02-01-preview/managed-databases/create-or-update) is being deprecated. Starting January 2022, use the [Create or Update v5.0.2022](/rest/api/sql/2022-05-01-preview/managed-databases/create-or-update) replacement API call for all database restore operations. 

## Overview

Point-in-time restore is useful in recovery scenarios, such as incidents caused by errors, incorrectly loaded data, or deletion of crucial data. You can also use it simply for testing or auditing. Backup files are kept for 7 to 35 days, depending on your database settings.

Point-in-time restore can restore a database:

- from an existing database.
- from a deleted database.
- to the same SQL Managed Instance, or to another SQL Managed Instance. 
- to an instance in the same subscription or in a different subscription from the source instance. 

The following table shows point-in-time restore scenarios for SQL Managed Instance: 

| **Scenario** | **Azure Portal** | **Azure CLI** | **PowerShell** | 
|:----------|:----------|:----------|:----------|
|Restore existing DB to the same instance of SQL Managed Instance| Yes | Yes | Yes| 
|Restore existing DB to another SQL Managed Instance| Yes |  Yes | Yes | 
|Restore dropped DB to same SQL Managed Instance| Yes | No| Yes | 
|Restore dropped DB to another SQL Managed Instance | Yes | No | Yes 
|Restore existing DB to an instance in another subscription | Yes | No | No | 
|Restore dropped DB to an instance in another subscription | Yes | No | No | 


## Limitations

Point-in-time restore of a whole SQL Managed Instance isn't possible. This article explains only what's possible: point-in-time restore of a database that's hosted on SQL Managed Instance. 

Limitations differ if you're restoring your database to an instance in the same subscription, or to a different subscription. 

When [service endpoint policies](/service-endpoint-policies-configure.md) are enabled on Azure SQL Managed Instance, placing a service endpoint policy on a subnet, prevents point-in-time restores (PITR) from instances in different subnets. 


> [!WARNING]
> Be aware of the storage size of your SQL Managed Instance. Depending on size of the data to be restored, you might run out of instance storage. If there isn't enough space for the restored data, use a different approach.

### Restore to same subscription

Point-in-time restore to SQL Managed Instance in the same subscription has the following limitation:

- When you're restoring from one instance of SQL Managed Instance to another, both instances must be in the same region. Cross-region restore aren't currently supported.

### Restore to a different subscription 

Restoring a PITR backup across subscriptions has the following limitations: 

- Both subscriptions must be in the same region. 
- Both subscriptions must be in the same tenant. 
- The subscription type has to be either EA, CSP, MCA, or PayGo. 
- The restore operation can only be performed on the primary instance. 
- Geo-replicated backups aren't currently supported for cross-subscription point-in-time restore. 
- The user performing the restore must either be part of the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) role or have the following explicit permissions: **crossSubscription/action**, **readBackups/action**. 
- If you're bringing your own key (BYOK), then the key must be present in both subscriptions. 


## Restore an existing database

Restore an existing database in the same subscription by using the Azure portal, PowerShell, or the Azure CLI. If you're restoring to a different instance in the same subscription and using PowerShell or the Azure CLI, be sure to specify the properties for the target SQL Managed Instance or the database will be restored to the same instance by default. 

Restoring your database to an instance in a different subscription is currently only possible by using the Azure portal. If you're restoring to a different subscription, the [Create or Update v5.0.2022](/rest/api/sql/2022-05-01-preview/managed-databases/create-or-update) API call underlying the restore action must contain `restorePointInTime`, `crossSubscriptionTargetManagedInstanceId`, and `crossSubscriptionSourceDatabaseId` **OR** `crossSubscriptionRestorableDroppedDatabaseId`. 


# [Portal](#tab/azure-portal)

1. Sign in to the [Azure portal](https://portal.azure.com). 
1. Go to your source SQL Managed Instance and under **Managed Instance Databases**, choose the database that you want to restore.

   :::image type="content" source="media/point-in-time-restore/choose-database-to-restore.png" alt-text="Screenshot of the Azure portal, SQL Managed Instance overview blade, with a database selected. ":::

1. Select **Restore** on the database page to open the **Create Azure SQL Managed Database** page: 

    :::image type="content" source="./media/point-in-time-restore/restore-database-to-mi.png" alt-text="Screenshot of the Azure portal, databases page, with Restore a database selected.":::

1. On the **Basics** tab, under **Project details**, choose the target destination subscription and resource group that contains the managed instance you want to restore the database to. 
1. Under **Database details**, provide the new name your restored database will use, and the target SQL Managed Instance you want to restore the database to. 
1. On the **Data source** tab, select **Continuous backup**, and then choose the subscription and resource group from the source database you want to restore. 
1. Once you provide the source subscription and resource group, choose the database you want to restore from the **Managed Database** drop-down and then select the point in time restore point you want to restore the database from. 
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

Restoring a deleted database can be done by using the Azure portal or PowerShell. 

### Portal 

To recover a deleted managed database by using the Azure portal, follow these steps: 

1. Go to your source SQL Managed Instance. 
1. Select **Backups** under **Data management**.
1. Choose **Deleted** under **Show databases**. 
1. Select **Restore** for the deleted database you want to restore to open the **Create Azure SQL Managed Database** page. 

  :::image type="content" source="./media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png" alt-text="Screenshot of Azure portal, databases page, with restore deleted Azure SQL instance database highlighted." lightbox="./media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png":::

1. Provide details for the target managed instance you want to restore your database to on the **Basics** tab, and provide details of your source database on the **Data source** tab. Configure retention settings on the **Additional settings** tab. 
1. Once you're done, select **Review + Create** followed by **Create** to restore your deleted database. 


### PowerShell

To restore a deleted database to the same instance, update the parameter values and then run the following PowerShell command: 

```powershell-interactive
$subscriptionId = "<Subscription ID>"
Get-AzSubscription -SubscriptionId $subscriptionId
Select-AzSubscription -SubscriptionId $subscriptionId

$resourceGroupName = "<Resource group name>"
$managedInstanceName = "<SQL Managed Instance name>"
$deletedDatabaseName = "<Source database name>"
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

To restore the database to another SQL Managed Instance, also specify the names of the target resource group and target SQL Managed Instance:

```powershell-interactive
$targetResourceGroupName = "<Resource group of target SQL Managed Instance>"
$targetInstanceName = "<Target SQL Managed Instance name>"

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

To overwrite an existing database, you must:

1. Drop the existing database that you want to overwrite.
2. Rename the point-in-time-restored database to the name of the database that you dropped.

### Drop the original database

You can drop the database by using the Azure portal, PowerShell, or the Azure CLI.

You can also drop the database by connecting to the SQL Managed Instance directly, starting SQL Server Management Studio (SSMS), and then running the following Transact-SQL (T-SQL) command:

```sql
DROP DATABASE WorldWideImporters;
```

Use one of the following methods to connect to your database in the SQL Managed Instance:

- [SSMS/Azure Data Studio via an Azure virtual machine](./connect-vm-instance-configure.md)
- [Point-to-site](./point-to-site-p2s-configure.md)
- [Public endpoint](./public-endpoint-configure.md)

# [Portal](#tab/azure-portal)

In the Azure portal, select the database from the SQL Managed Instance, and then select **Delete**.

   :::image type="content" source="./media/point-in-time-restore/delete-database-from-mi.png" alt-text="Delete a database by using the Azure portal":::

# [PowerShell](#tab/azure-powershell)

Use the following PowerShell command to drop an existing database from a SQL Managed Instance:

```powershell
$resourceGroupName = "<Resource group name>"
$managedInstanceName = "<SQL Managed Instance name>"
$databaseName = "<Source database>"

Remove-AzSqlInstanceDatabase -Name $databaseName -InstanceName $managedInstanceName -ResourceGroupName $resourceGroupName
```

# [Azure CLI](#tab/azure-cli)

Use the following Azure CLI command to drop an existing database from a SQL Managed Instance:

```azurecli-interactive
az sql midb delete -g mygroupname --mi myinstancename -n mymanageddbname
```

---

### Alter the new database name to match the original database name

Connect directly to the SQL Managed Instance and start SQL Server Management Studio. Then, run the following Transact-SQL (T-SQL) query. The query will change the name of the restored database to that of the dropped database that you intend to overwrite.

```sql
ALTER DATABASE WorldWideImportersPITR MODIFY NAME = WorldWideImporters;
```

Use one of the following methods to connect to your database in SQL Managed Instance:

- [Azure virtual machine](./connect-vm-instance-configure.md)
- [Point-to-site](./point-to-site-p2s-configure.md)
- [Public endpoint](./public-endpoint-configure.md)

## Next steps

Learn about [automated backups](../database/automated-backups-overview.md).
