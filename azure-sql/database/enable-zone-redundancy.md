---
title: Enable zone redundancy for Azure SQL Database
description: Learn how to update your Azure SQL Database to enable availability zone support.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: anaharris, mathoma
ms.date: 07/25/2025
ms.service: azure-sql-database
ms.subservice: high-availability
ms.topic: upgrade-and-migration-article
ms.custom:
  - subject-reliability
monikerRange: "=azuresql || =azuresql-db"
---
# Enable zone redundancy for Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This guide describes how to update your Azure SQL Database for zone redundancy with [availability zones](/azure/reliability/availability-zones-overview).

Enabling zone redundancy for Azure SQL Database guarantees high availability, making your databases and elastic pools resilient to a larger set of failures, such as catastrophic datacenter outages, without any changes of the application logic.  When zone redundancy is enabled, your database utilizes availability zones to replicate data across multiple physical locations within a single Azure region.  


## Prerequisites

Before you enable availability zone support, ensure that your Azure SQL Database is in a supported service tier and deployment model. Make sure that your tier and model is offered in a [region that supports availability zones](/azure/reliability/regions-list).

| Service tier | Deployment model | Zone redundancy availability |
|-----|------|------|
| Premium  | Single database or Elastic Pool | [All regions that support availability zones](/azure/reliability/regions-list)|
| Business Critical | Single database or Elastic Pool | [All regions that support availability zones](/azure/reliability/regions-list) |
| General Purpose  | Single database or Elastic Pool | [Selected regions that support availability zones](high-availability-sla.md?view=azuresql-db&preserve-view=true#general-purpose-service-tier-zone-redundant-availability)|
| Hyperscale | Single database | [All regions that support availability zones](/azure/reliability/regions-list) |

## Downtime requirements

Enabling zone redunancy for Premium, Business Critical, and General Purpose service tier is an online operation with a brief disconnect. If you have implemented [retry logic for standard transient errors](troubleshoot-common-connectivity-issues.md?view=azuresql-db&preserve-view=true#retry-logic-for-transient-errors), you won't notice the failover. 

For Hyperscale service tier, zone redundancy support can only be specified during database creation and can't be modified once the resource is provisioned. If you wish to move to availability zone support, you'll need to transfer the data with database copy, point-in-time restore, or geo-replica. If the target database is in a different region than the source or if the database backup storage redundancy for the target differs from the source database, then downtime is proportional to the size of the data operation.  

<a id="migration"></a>

## Enable zone redundancy (Premium, Business Critical, and General Purpose)

For the Premium, Business Critical, and General Purpose service tiers, enabling zone redundancy is possible. 

Follow these steps to enable zone redundancy for a single database or an elastic pool.

<a id="migrate-a-single-database"></a>

### Enable zone redundancy for a single database

# [Azure portal](#tab/portal)

1. Go to the [Azure portal](https://portal.azure.com) to find your database. Search for and select **SQL databases**.

1. Select the desired database.

1. Under **Settings** select **Compute + Storage**. 

1. Select **Yes** for **Would you like to make this database zone redundant?**

1. Select **Apply**.

1. Wait to receive an operation completion notice in **Notifications** in the top menu of the Azure portal. 

1. To verify that zone redundancy is enabled, select **Overview** and then select **Properties**.

1. Under the **Availability** section, confirm that zone redundancy is set to **Enabled**.   

# [PowerShell](#tab/powershell)

Open PowerShell as Administrator and run the following command (replace the placeholders in `<>` with your resource names). The `<server_name>` should not include `.database.windows.net`.

```powershell
Connect-AzAccount
$subscriptionid = <'your subscription id here'>
Set-AzContext -SubscriptionId $subscriptionid

$parameters = @{
    ResourceGroupName = '<Resource_Group_Name>'
    ServerName = '<Server_Name>'
    DatabaseName = '<Database_Name>'
}
Set-AzSqlDatabase @parameters -ZoneRedundant 
```

# [Azure CLI](#tab/cli)

Use Azure CLI to run the following command (replace the placeholders in `<>` with your resource names):

```azurecli
    az sql db update --resource-group "<Resource_Group_Name>" --server "<Server_Name>" --name "<Database_Name>" --zone-redundant 
```

# [REST API](#tab/API)

To enable zone redundancy, see [Databases - Create Or Update API](/rest/api/sql/2022-05-01-preview/databases/create-or-update?tabs=HTTP) and use the `properties.zoneRedundant` property.

---

<a id="migrate-an-elastic-pool"></a>

### Enable zone redundancy for an elastic pool

> [!IMPORTANT]
> Enabling zone redundancy support for elastic pools makes all databases within the pool zone redundant.

# [Azure portal](#tab/portal)

1. Go to the [Azure portal](https://portal.azure.com) to find and select the desired elastic pool.

1. Under **Settings**, select **Compute + Storage**.

1. Select **Yes** for **Would you like to make this elastic pool zone redundant?**.

1. Select **Save**.

1. Wait to receive an operation completion notice in **Notifications** in the top menu of the Azure portal. 

1. To verify that zone redundancy is enabled, select **Configure** and then select **Pool settings**.

1. The zone redundant option should be set to **Yes**.   

# [PowerShell](#tab/powershell)

Open PowerShell as Administrator and run the following command (replace the placeholders in `<>` with your resource names). The `<server_name>` should not include `.database.windows.net`.

```powershell
Connect-AzAccount
$subscriptionid = <'your subscription id here'>
Set-AzContext -SubscriptionId $subscriptionid

$parameters = @{
    ResourceGroupName = '<Resource_Group_Name>'
    ServerName = '<Server_Name>'
    ElasticPoolName = '<Elastic_Pool_Name>'
}

Set-AzSqlElasticPool  @parameters -ZoneRedundant 
```

# [Azure CLI](#tab/cli)

Use Azure CLI to run the following command (replace the placeholders in `<>` with your resource names):

```azurecli
    az sql elastic-pool update --resource-group "<Resource_Group_Name>" --server "<Server_Name>" --name "<Elastic_Pool_Name>" --zone-redundant 
```

# [REST API](#tab/API)

To enable zone redundancy, see [Elastic Pools - Create Or Update API](/rest/api/sql/2022-05-01-preview/elastic-pools/create-or-update?tabs=HTTP). 

---

## Redeployment (Hyperscale)

For the Hyperscale service tier, zone redundancy support can only be specified during database creation and can't be modified once the database is provisioned. If you wish to gain zone redundancy support, you need to perform a data transfer from your existing Hyperscale service tier single database. To perform the transfer and enable the zone redundancy option, a clone must be created using database copy, point-in-time restore, or geo-replica.  

### Redeployment considerations

- There are two modes of redeployment (online and offline): 

    - The **Database copy and point-in-time restore methods (offline mode)** create a transactionally consistent database at a certain point in time. As a result, any data changes performed after the copy or restore operation have been initiated won't be available on the copied or restored database.

    - **Geo-replica method (online mode)** is a redeployment wherein any data changes from source are synchronized to target. 

- Connection string for the application must be updated to point to the zone redundant database. 

### Redeploy a single database

#### Database copy

To create a database copy and enable zone redundancy with Azure portal, PowerShell, or Azure CLI, follow the instructions in [copy a transactionally consistent copy of a database in Azure SQL Database](database-copy.md?view=azuresql-db&preserve-view=true#copy-a-database).

#### Point-in-time restore

To create a point-in-time database restore and enable zone redundancy with Azure portal, PowerShell, or Azure CLI, follow the instructions in [Point-in-time restore](recovery-using-backups.md?view=azuresql-db&preserve-view=true#point-in-time-restore).

##### Geo-replica

To create a geo-replica of the database:

1. Follow the instructions with Azure portal, PowerShell, or Azure CLI in [Configure active geo-replication and failover (Azure SQL Database)](active-geo-replication-configure-portal.md?view=azuresql-db&preserve-view=true) and enable zone redundancy under **Compute + Storage**

1. The replica is seeded, and the time taken for seeding the data depends upon size of source database. You can monitor the status of seeding in the Azure portal or by running the following T-SQL queries on the replica database:

    ```sql
        SELECT * FROM sys.dm_geo_replication_link_status;
        SELECT * FROM sys.dm_operation_status;
    ```

1. Once the database seeding is finished, perform a planned (no data loss) failover to make the zone redundant target database as primary. 
   - Use the [sys.dm_geo_replication_link_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database?view=azuresqldb-current&preserve-view=true) to view the status of the geo-replication state. The `replication_state_desc` is `CATCH_UP` when the secondary database is in a transactionally consistent state. 
   - In the [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database?view=azuresqldb-current&preserve-view=true) dynamic management view, look for `state_desc` to be `COMPLETED` when the seeding operation has completed.

1. Update the server name in the connection strings for the application to reflect the new zone redundant database.

1. To clean up, consider removing the original non-zone redundant database from the geo replica relationship. You can choose to delete it.  

## Validate zone redundancy

You can use Azure PowerShell or the Azure CLI or the [REST API](/rest/api/sql/databases/get) to check the `ZoneRedundant` property for a database.

# [Portal](#tab/portal)

1. In the Azure portal, navigate to your Azure SQL Database.
1. Under **Settings**, select **Compute + Storage**.
1. Check the value of the **Would you like to make this database zone redundant?** setting.

# [Azure PowerShell](#tab/powershell)

Use the following example command to check the value of `ZoneRedundant` property for a database, for example the `master` database.

```powershell
Get-AzSqlDatabase -ResourceGroupName "myResourceGroup" -ServerName "myServerName" -DatabaseName "master"
```

# [Azure CLI](#tab/cli)

Use the following example command to check the value of `ZoneRedundant` property for a database, for example the `master` database.

```azurecli
az sql db show --resource-group "myResourceGroup" --server "myServerName" --name master
```

# [REST API](#tab/API)

See [Databases - Get API](/rest/api/sql/2022-05-01-preview/databases/get?tabs=HTTP) and view the `properties.zoneRedundant` property.

---

## Disable zone-redundancy

To disable zone-redundancy for a single database or an elastic pool, you can use the Azure portal, ARM API, PowerShell, or CLI. 

### Disable zone-redundancy for a single database

# [Portal](#tab/portal)

1. Go to the  [Azure portal](https://portal.azure.com) to find and select the database that you no longer want to be zone-redundant.

1. Under **Settings**, select **Compute + Storage**.

1. Select **No** for **Would you like to make this database zone redundant?**

1. Select **Save**.

# [PowerShell](#tab/powershell)

```powershell
set-azsqlDatabase -ResourceGroupName "<Resource-Group-Name>" -DatabaseName "<Server-Name>" -ServerName "<Server-Name>" -ZoneRedundant:$false 
```

# [CLI](#tab/cli)

```azurecli
az sql db update --resource-group "<Resource-Group-Name>" --server "<Server-Name>" --name "<Server-Name>" --zone-redundant false
```

# [REST API](#tab/API)

See [Databases - Create Or Update API](/rest/api/sql/2022-05-01-preview/databases/create-or-update?tabs=HTTP) and use the `properties.zoneRedundant` property.

---

### Disable zone-redundancy for an elastic pool

# [Portal](#tab/portal)

1. Go to the  [Azure portal](https://portal.azure.com) to find and select the elastic pool that you no longer want to be zone-redundant.

1. Under **Settings**, select **Compute + Storage**.

1. Select **No** for **Would you like to make this elastic pool zone redundant?**.

1. Select **Save**.

# [PowerShell](#tab/powershell)

```powershell
Set-AzSqlElasticpool -ResourceGroupName "<Resource-Group-Name>" -ServerName "<Server-Name>" -ElasticPoolName "<Elastic-Pool-Name>" -ZoneRedundant:$false
```

# [CLI](#tab/cli)

```azurecli
az sql elastic-pool update --resource-group "<Resource-Group-Name>" --server "<Server-Name>" --name "<Server-Name>" --zone-redundant false
```

# [REST API](#tab/API)

See [Databases - Create Or Update API](/rest/api/sql/elastic-pools/create-or-update?tabs=HTTP) and use the `properties.zoneRedundant` property.

---

To disable zone-redundancy for Hyperscale service tier, you can reverse the steps documented in [Redeployment (Hyperscale)](#redeployment-hyperscale).

## Related content

- [Availability through redundancy -  Azure SQL Database](high-availability-sla-local-zone-redundancy.md)
- [Azure services that support availability zones](/azure/reliability/availability-zones-service-support)
- [Azure regions that support availability zones](/azure/reliability/regions-list)
