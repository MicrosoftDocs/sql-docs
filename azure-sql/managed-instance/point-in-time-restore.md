---
title: Point-in-time restore
titleSuffix: Azure SQL Managed Instance
description: Learn how to restore a database to an earlier point in time for Azure SQL Managed Instance.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma, nvraparl
ms.date: 03/25/2023
ms.service: azure-sql-managed-instance
ms.subservice: backup-restore
ms.topic: how-to
ms.custom: devx-track-azurepowershell, devx-track-azurecli
---
# Restore a database in Azure SQL Managed Instance to a previous point in time

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

You can use point-in-time restore to create a database that's a copy of a database at a specific, earlier point in time. This article describes how to do a point-in-time restore of a database in Azure SQL Managed Instance.

> [!NOTE]
> The [Create or Update v02.01.2022](/rest/api/sql/2022-02-01-preview/managed-databases/create-or-update) has been deprecated. Starting in January 2023, use the replacement [Create or Update v5.0.2022](/rest/api/sql/2022-05-01-preview/managed-databases/create-or-update) API call for all database restore operations. 

## Overview

Point-in-time restore is useful in recovery scenarios, like for an incident that's caused by error or failure, when data doesn't load correctly, or if crucial data is deleted. You can also use it simply to test and audit your database deployment. Azure backup files are kept for 7 to 35 days depending on your database settings.

You can use point-in-time restore to restore a database in these scenarios:

- From an existing database
- From a deleted database
- To the same managed instance or to a different managed instance
- To a managed instance in the same subscription or to a managed instance in a different subscription

The following table shows point-in-time restore scenarios for SQL Managed Instance:

| Scenario | Azure portal | Azure CLI | PowerShell |
|:----------|:----------|:----------|:----------|
| Restore an existing database to the same managed instance | Yes | Yes | Yes|
| Restore an existing database to a different managed instance | Yes |  Yes | Yes |
| Restore a deleted database to the same managed instance | Yes | Yes | Yes |
| Restore a deleted database to a different managed instance | Yes | Yes | Yes |
| Restore an existing database to a managed instance in another subscription | Yes | Yes | Yes |
| Restore a deleted database to a managed instance in another subscription | Yes | Yes | Yes |

## Permissions 

To recover a database, you must be either:

- A member of the SQL Server Contributor role or [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) role (depending on the recovery destination) in the subscription
- The subscription owner 

To restore database to a different target subscription, if you're not in the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) role you should also have the following permissions:

- **Microsoft.Sql/managedInstances/databases/readBackups/action** on the source SQL managed instance. 
- **Microsoft.Sql/managedInstances/crossSubscriptionPITR/action** on the target SQL managed instance.

For more information, see [Azure RBAC: Built-in roles](/azure/role-based-access-control/built-in-roles). 

## Limitations

These limitations apply to point-in-time restore in SQL Managed Instance:

- You can't use point-in-time restore to recover an entire SQL Managed Instance deployment. Use point-in-time restore only to make a copy of a database that's hosted on SQL Managed Instance.

- Limitations in point-in-time restore depend on whether you're restoring your database to a managed instance in the same subscription or to a managed instance in a different subscription.

- When [service endpoint policies](service-endpoint-policies-configure.md) are enabled on Azure SQL Managed Instance, placing a service endpoint policy on a subnet prevents point-in-time restores from instances in different subnets.

> [!WARNING]
> Be aware of the storage size of your managed instance. Depending on the size of the data to be restored, you might run out of storage for your managed instance. If you don't have enough storage space in your managed instance for the restored data, use a different approach.

### Restore to the same subscription

If you restore from one managed instance to another managed instance in the same Azure subscription, both managed instances must be in the same region. Currently, cross-region restore isn't supported.

### Restore to a different subscription

Restoring a point-in-time restore backup across subscriptions has the following limitations:

- Both subscriptions must be in the same region.
- Both subscriptions must be in the same tenant.
- The subscription type must be either Enterprise Agreement, Cloud Solution Provider, Microsoft Certified Partner, or pay-as-you-go.
- You can use the restore action only on the primary instance.
- You can only restore a backup from the primary region. Restoring a database from the geo-replicated secondary region is not supported for cross-subscription point-in-time restore.
- The user who takes the restore action must either have the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) role assignment or have these explicit permissions:
    - **Microsoft.Sql/managedInstances/databases/readBackups/action** on the source SQL managed instance. 
    - **Microsoft.Sql/managedInstances/crossSubscriptionPITR/action** on the target SQL managed instance.
- If you bring your own key (BYOK), the key must be present in both subscriptions.

## Restore an existing database

You can restore an existing database in the same subscription by using the Azure portal, PowerShell, or the Azure CLI. If you restore to a different instance in the same subscription by using PowerShell or the Azure CLI, be sure to specify the properties for the target SQL Managed Instance resource. The database is restored to the same instance by default.

If you restore to a different subscription, the [Create or Update v5.0.2022](/rest/api/sql/2022-05-01-preview/managed-databases/create-or-update) API call that underlies the restore action must contain `restorePointInTime`, `crossSubscriptionTargetManagedInstanceId`, and either `crossSubscriptionSourceDatabaseId` or `crossSubscriptionRestorableDroppedDatabaseId`. 

# [Portal](#tab/azure-portal)

To restore an existing database, you can do so by going to the database page in the Azure portal, and selecting **Restore**. 

Alternatively to restore your database, you can follow these steps: 

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Go to the target SQL Managed Instance where you plan to restore your database to. 
1. On the **Overview** page, choose **+ New database** to open the **Create Azure SQL Managed Database** page. 

   :::image type="content" source="media/point-in-time-restore/choose-database-to-restore.png" alt-text="Screenshot that shows the SQL Managed Instance overview pane in the Azure portal, with adding a new database selected. ":::

1. On the **Basics** tab of the **Create Azure SQL Managed Database page**, provide subscription and resource group details under **Project details**. Then, under **Database details** provide the new name of the database you plan to restore. Confirm the correct managed instance is listed in the drop down. Then select **Next: Data source >**

    :::image type="content" source="./media/point-in-time-restore/create-database-page.png" alt-text="Screenshot of the Azure portal that shows the Basics tab of the Create Azure SQL Managed Database page.":::

1. On the **Data source** tab, choose **Point-in-time restore** under **Use existing data**. Provide the subscription, resource group and managed instance that contains the source database. From the **Managed database** drop-down, choose the database you want to restore, and then choose the point in time you want to restore the database from. The source and target instance can be the same, or two different instances. Select **Next : Additional settings >**

    :::image type="content" source="./media/point-in-time-restore/database-data-source.png" alt-text="Screenshot of the Azure portal that shows the data source tab of the Create Azure SQL Managed Database page, with point-in-time restore selected.":::

1. On the **Additional settings** tab, you can check the box to inherit the retention policy from the source database, or, alternatively, you can select **Configure retention** to open the **Configure policies** page, and set your desired retention policies for your restored database.  When finished, select **Review + create**. 

    :::image type="content" source="./media/point-in-time-restore/additional-settings-page.png" alt-text="Screenshot of the Azure portal that shows the additional settings tab of the Create Azure SQL Managed Database page.":::


1. On **Review + create**, when validation is successful, select **Create** to restore your database.

This action starts the restore process, which creates a new database and populates it with data from the original database at the specified point in time. For more information about the recovery process, see [Recovery time](../database/recovery-using-backups.md#recovery-time).

# [PowerShell](#tab/azure-powershell)

Use Azure PowerShell to restore your database. For more information, review [Install the Azure PowerShell module](/powershell/azure/install-az-ps). For more information, see [Restore-AzSqlInstanceDatabase](/powershell/module/az.sql/restore-azsqlinstancedatabase).

Run one of the following code options with your values substituted for the parameters.

To restore the database to the same managed instance:

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

To restore the database to another subscription, set the context to the target subscription (`Set-AzContext`) and be sure to provide a value for the required parameter `-TargetSubscriptionID`: 

```powershell-interactive 
Set-AzContext -SubscriptionID "targetSubscriptionID"

Restore-AzSqlInstanceDatabase -FromPointInTimeBackup `
                              -SubscriptionId "sourceSubscriptionID" `
                              -ResourceGroupName "sourceRGName" `
                              -InstanceName "sourceManagedInstanceName" `
                              -Name "sourceDatabaseName" `
                              -PointInTime $pointInTime `
                              -TargetInstanceDatabaseName "targetDatabaseName" `
                              -TargetInstanceName "targetManagedInstanceName" `
                              -TargetResourceGroupName "targetResourceGroupName" `
                              -TargetSubscriptionId "targetSubscriptionId"
```


# [Azure CLI](#tab/azure-cli)

Use the Azure CLI to restore your database to a point in time. For more information, see [Install the Azure CLI](/cli/azure/install-azure-cli). For a detailed explanation of available parameters, see the [CLI documentation for restoring a database in SQL Managed Instance](/cli/azure/sql/midb#az-sql-midb-restore).

Run one of the following code options with your values substituted for the parameters.

To restore the database to the same managed instance:

```azurecli-interactive
az sql midb restore -g mygroupname --mi myinstancename |
-n mymanageddbname --dest-name targetmidbname --time "2018-05-20T05:34:22"
```
  
To restore the database to a different managed instance, also specify the names of the target resource group and the managed instance:  

```azurecli-interactive
az sql midb restore -g mygroupname --mi myinstancename -n mymanageddbname |
       --dest-name targetmidbname --time "2018-05-20T05:34:22" |
       --dest-resource-group mytargetinstancegroupname |
       --dest-mi mytargetinstancename
```

To restore to another subscription, be sure to set the context (`az account set`) to the target subscription: 


```azurecli-interactive
az account set -s "targetSubscriptionId" `

az sql midb restore -s sourcesubscriptionid -g sourcegroup 
--mi sourceinstance -n sourcemanageddb --dest-name targetDbName 
--dest-mi targetMI --dest-resource-group targetRG --time "2022-05-20T05:34:22"
```

---

## Restore a deleted database

You can restore a deleted database by using the Azure portal, Azure PowerShell or the Azure CLI. 

# [Portal](#tab/azure-portal)

To restore a deleted managed database by using the Azure portal:

1. In the Azure portal, go to your source managed instance.
1. In the left menu under **Data management**, select **Backups**.
1. Under **Show databases**, select **Deleted**.
1. For the database to restore, select **Restore**.

   :::image type="content" source="./media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png" alt-text="Screenshot that shows available databases in the portal, with the Restore button highlighted to restore a deleted database." lightbox="./media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png":::

1. In **Create Azure SQL Managed database**, enter or select details for the target managed instance to restore your database to. Select the **Data source** tab.
1. In **Data source**, enter or select the details for your source database. Select the **Additional settings** tab.
1. In **Additional settings**, configure retention settings. Select the **Review + create** tab.
1. In **Review + create**, select **Create** to restore your deleted database.

# [PowerShell](#tab/azure-powershell)

To restore a deleted managed database, run one of the following PowerShell code options with your values substituted for the parameters:

To restore a deleted database to the same managed instance:

```powershell-interactive
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

```powershell-interactive
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

To restore the database to another subscription, set the context to the target subscription (`Set-AzContext`) and be sure to provide values for the required parameters `-TargetSubscriptionID`, and `-DeleteDate`: 

```powershell-interactive
Set-AzContext -SubscriptionID "targetSubscriptionID"

Restore-AzSqlInstanceDatabase -FromPointInTimeBackup `
                              -SubscriptionId "sourceSubscriptionID" `
                              -ResourceGroupName "sourceRGName" `
                              -InstanceName "sourceManagedInstanceName" `
                              -Name "sourceDatabaseName" `
                              -PointInTime $pointInTime `
                              -TargetInstanceDatabaseName "targetDatabaseName" `
                              -TargetInstanceName "targetManagedInstanceName" `
                              -TargetResourceGroupName "targetResourceGroupName" `
                              -TargetSubscriptionId "targetSubscriptionId" `
                              -DeletionDate "deletion_date"
```


# [Azure CLI](#tab/azure-cli)

To restore a deleted database to the same subscription: 

```azurecli-interactive
az sql midb restore -g resourcegroup --mi instancename
-n databasename --dest-name databasename --dest-mi instancename 
--dest-resource-group resourcegroup --time "2023-02-23T11:54:00" --deleted-time "deletion_date"
```

To restore a deleted database to another subscription, be sure to set the context (`az account set`) to the target subscription and specify the -s parameter for the `az sql midb restore` command to identify the source subscription: 

```azurecli-interactive
az account set -s "targetSubscriptionId"

az sql midb restore -s sourcesubscriptionid -g sourcegroup 
--mi sourceinstance -n sourcemanageddb --dest-name targetDbName 
--dest-mi targetMI --dest-resource-group targetRG 
--time "2022-05-20T05:34:22" --deleted-time "deletion_date"
```

---


## Overwrite an existing database

To overwrite an existing database, you must do the following: 

1. Drop the original database that you want to overwrite.
2. Rename the database restored from the point-in-time to the name of the database you dropped.

### Drop the original database

You can drop the database by using the Azure portal, PowerShell, or the Azure CLI.

Another option to drop the database is to connect to your managed instance directly in SQL Server Management Studio (SSMS), and then use the `DROP` Transact-SQL (T-SQL) command:

```sql
DROP DATABASE WorldWideImporters;
```

Use one of the following methods to connect to the database in your managed instance:

- [SQL Server Management Studio and Azure Data Studio in an Azure virtual machine](./connect-vm-instance-configure.md)
- [Point-to-site](./point-to-site-p2s-configure.md)
- [Public endpoint](./public-endpoint-configure.md)

# [Portal](#tab/azure-portal)

1. In the Azure portal, select the database in your managed instance.
1. In the command bar, select **Delete**.

   :::image type="content" source="./media/point-in-time-restore/delete-database-from-mi.png" alt-text="Screenshot that shows how to delete a database by using the Azure portal.":::

# [PowerShell](#tab/azure-powershell)

To delete an existing database from your managed instance, run the following PowerShell code with your values substituted for the parameters:

```powershell
$resourceGroupName = "<resource group name>"
$managedInstanceName = "<managed instance name>"
$databaseName = "<source database name>"

Remove-AzSqlInstanceDatabase -Name $databaseName -InstanceName $managedInstanceName -ResourceGroupName $resourceGroupName
```

# [Azure CLI](#tab/azure-cli)

To delete an existing database from your managed instance, run the following Azure CLI code with your values substituted for the parameters:

```azurecli-interactive
az sql midb delete -g mygroupname --mi myinstancename -n mymanageddbname
```

---

### Change the new database name to match the original database name

Use SQL Server Management Studio (SSMS) to connect directly to your managed instance. Then run the following T-SQL query. The query changes the name of the restored database to the name of the dropped database you intend to overwrite.

```sql
ALTER DATABASE WorldWideImportersPITR MODIFY NAME = WorldWideImporters;
```

Use one of the following methods to connect to the database in your managed instance:

- [Azure virtual machine](./connect-vm-instance-configure.md)
- [Point-to-site](./point-to-site-p2s-configure.md)
- [Public endpoint](./public-endpoint-configure.md)



## Next steps

Learn about [automated backups](automated-backups-overview.md).
