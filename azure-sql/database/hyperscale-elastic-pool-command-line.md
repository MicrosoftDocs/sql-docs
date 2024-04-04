---
title: Working with Hyperscale elastic pools using command-line tools
description: Working with Hyperscale elastic pools using command-line tools such as the Azure CLI and PowerShell.
author: arvindshmicrosoft
ms.author: arvindsh
ms.reviewer: wiassaf, mathoma
ms.date: 02/26/2024
ms.service: sql-database
ms.subservice: elastic-pools
ms.topic: conceptual
ms.custom: devx-track-azurecli, devx-track-azurepowershell, ignite-2023
---
# Working with Hyperscale elastic pools using command-line tools
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

In this article, learn to create, scale, and move databases into a [Hyperscale elastic pool](hyperscale-elastic-pool-overview.md) using command line tools such as the Azure CLI and PowerShell. In addition to these methods, you can always use the Azure portal for most operations.

> [!NOTE]
> [Elastic pools for Hyperscale](./hyperscale-elastic-pool-overview.md) are currently in preview.

## Prerequisites

To work with your Hyperscale elastic pool, you should have: 

- An Azure subscription. If you don't have an Azure subscription, [create a free account](https://azure.microsoft.com/free/).
- A [logical server in Azure](logical-servers.md) deployed to a [resource group](/azure/azure-resource-manager/management/manage-resource-groups-portal). The examples in this article use the name `my-example-rg` for the resource group, and `my-example-sql-svr` for the logical server. 
- The latest version of Azure PowerShell [Az.Sql.3.11.0 or higher](https://www.powershellgallery.com/packages/Az.Sql/3.11.0) or the Azure CLI [Az version 2.40.0 or higher](/cli/azure/install-azure-cli)


## Create a new Hyperscale elastic pool

You can use the Azure CLI or Azure PowerShell to create a Hyperscale elastic pool. 

### [Azure CLI](#tab/azure-cli)

Use the [az sql elastic-pool create](/cli/azure/sql/elastic-pool#az_sql_elastic_pool_create) command to create a Hyperscale elastic pool. 

The following example creates a Hyperscale elastic pool with four vCores, and two secondary pool replicas:

```azurecli
az sql elastic-pool create --resource-group "my-example-rg" --server "my-example-sql-svr" --name "my_hs_pool" --edition "Hyperscale" --capacity 4 --family Gen5 --ha-replicas 2
```

The following example creates a zone redundant Hyperscale elastic pool with four vCores and one secondary pool replica:

```azurecli
az sql elastic-pool create --resource-group "myresourcegroup" --server "mylogicalserver" --name "zr-hs-ep" --family Gen5 --edition Hyperscale --capacity 4 --ha-replicas 1 --zone-redundant
```

### [PowerShell](#tab/azure-powershell)

Use the [New-AzSqlElasticPool](/powershell/module/az.sql/new-azsqlelasticpool) cmdlet to create a Hyperscale elastic pool. 

The following example creates a Hyperscale elastic pool configured with four vCores, and two secondary pool replicas:

```powershell
New-AzSqlElasticPool -ResourceGroupName "my-example-rg" -ServerName "my-example-sql-svr" -ElasticPoolName "my_hs_pool" -Edition "Hyperscale" -VCore 4 -ComputeGeneration Gen5 -HighAvailabilityReplicaCount 2
```

The following example creates zone redundant a Hyperscale elastic pool configured with four vCores, and one secondary pool replica:

```powershell
New-AzSqlElasticPool -ResourceGroupName "myresourcegroup" -ServerName "mylogicalserver" -ElasticPoolName "zr-hs-ep" -Edition Hyperscale -ComputeGeneration Gen5 -VCore 4 -HighAvailabilityReplicaCount 1 -ZoneRedundant
```

---

## Scale up a Hyperscale elastic pool

You can use the Azure CLI or Azure PowerShell to scale up an existing Hyperscale elastic pool. 


### [Azure CLI](#tab/azure-cli)

Use the [az sql elastic-pool update](/cli/azure/sql/elastic-pool#az-sql-elastic-pool-update) command to scale up an existing Hyperscale elastic pool. 

The following example scales up an existing Hyperscale elastic pool to 8 vCores and sets the per-DB min and max to 0 and 2, respectively:

```azurecli
az sql elastic-pool update --resource-group "my-example-rg" --server "my-example-sql-svr" --name "my_hs_pool" --capacity 8 --db-min-capacity 0 --db-max-capacity 2
```

### [PowerShell](#tab/azure-powershell)

Use the [Set-AzSqlElasticPool](/powershell/module/az.sql/set-azsqlelasticpool) cmdlet to scale up an existing Hyperscale elastic pool. 

The following example scales up an existing Hyperscale elastic pool to 8 vCores and sets the per-DB min and max to 0 and 2, respectively:


```powershell
Set-AzSqlElasticPool -ResourceGroupName "my-example-rg" -ServerName "my-example-sql-svr" -ElasticPoolName "my_hs_pool" -VCore 8 -DatabaseVCoreMin 0 -DatabaseVCoreMax 2
```

---

## Scale out (or in) a Hyperscale elastic pool

Use the Azure CLI or Azure PowerShell to add or remove secondary pool replicas for an existing Hyperscale elastic pool - also known as scaling out or scaling in. 


### [Azure CLI](#tab/azure-cli)

Use the [az sql elastic-pool update](/cli/azure/sql/elastic-pool#az-sql-elastic-pool-update) command to scale out an existing Hyperscale elastic pool by adding a secondary pool replica or scale in an elastic pool by removing secondary pool replicas. 

The following example scales out an existing Hyperscale elastic pool to use four secondary pool replicas: 

```azurecli
# use the --ha-replicas (--read-replicas can also be used) parameter to specify the new number of high-availability replicas:
az sql elastic-pool update --resource-group "my-example-rg" --server "my-example-sql-svr" --name "my_hs_pool" --ha-replicas 4
```

The following example scales in an existing Hyperscale elastic pool to use one secondary pool replica: 

```azurecli
# use the --ha-replicas (--read-replicas can also be used) parameter to specify the new number of high-availability replicas:
az sql elastic-pool update --resource-group "my-example-rg" --server "my-example-sql-svr" --name "my_hs_pool" --ha-replicas 1
```

### [PowerShell](#tab/azure-powershell)

Use the [Set-AzSqlElasticPool](/powershell/module/az.sql/set-azsqlelasticpool) command to scale out an existing Hyperscale elastic pool by adding a secondary pool replica or scale in an elastic pool by removing secondary pool replicas. 

The following example scales out an existing Hyperscale elastic pool to use four secondary pool replicas: 

```powershell
Set-AzSqlElasticPool -ResourceGroupName "my-example-rg" -ServerName "my-example-sql-svr" -ElasticPoolName "my_hs_pool"  -HighAvailabilityReplicaCount 4
```

The following example scales in an existing Hyperscale elastic pool to use one secondary pool replica: 

```powershell
Set-AzSqlElasticPool -ResourceGroupName "my-example-rg" -ServerName "my-example-sql-svr" -ElasticPoolName "my_hs_pool"  -HighAvailabilityReplicaCount 1
```

---

## Move an existing database to a Hyperscale elastic pool

You can use the Azure CLI or Azure PowerShell to move an existing database in Azure SQL Database into an existing Hyperscale elastic pool. 

### [Azure CLI](#tab/azure-cli)

Use the [az sql db update](/cli/azure/sql/db#az-sql-db-update) command to move an existing database into an existing Hyperscale elastic pool. 

The following example moves database `my_existing_db` into existing Hyperscale elastic pool `my_hs_pool`: 

```azurecli
az sql db update --resource-group "my-example-rg" --server "my-example-sql-svr" --name "my_existing_db" --elastic-pool "my_hs_pool"
```

### [PowerShell](#tab/azure-powershell)

Use the [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) cmdlet to move an existing database into an existing Hyperscale elastic pool.


The following example moves database `my_existing_db` into existing Hyperscale elastic pool `my_hs_pool`: 

```powershell
Set-AzSqlDatabase -ResourceGroupName "my-example-rg" -ServerName "my-example-sql-svr" -DatabaseName "my_existing_db" -ElasticPoolName "my_hs_pool"
```

---

## Upgrade an existing elastic pool from Gen5 to premium-series hardware

You can use the Azure CLI or Azure PowerShell to upgrade an existing elastic pool from Gen5 to premium-series hardware.

### [Azure CLI](#tab/azure-cli)

<!--
Use the [az sql elastic-pool update](/cli/azure/sql/elastic-pool#az-sql-elastic-pool-update) command to upgrade an existing Hyperscale elastic pool.

The following example upgrade an existing Hyperscale elastic pool `my_hs_pool` to premium-series hardware:

```azurecli
az sql elastic-pool update --resource-group "my-example-rg" --server "my-example-sql-svr" --name "my_hs_pool" --family "premium-series"
```
-->

Currently unavailable via Azure CLI. Use Azure PowerShell or the Azure portal instead.

### [PowerShell](#tab/azure-powershell)

Use the [Set-AzSqlElasticPool](/powershell/module/az.sql/set-azsqlelasticpool) cmdlet to scale up an existing Hyperscale elastic pool. 

The following example upgrade an existing Hyperscale elastic pool `my_hs_pool` to premium-series hardware:


```powershell
Set-AzSqlElasticPool -ResourceGroupName "my-example-rg" -ServerName "my-example-sql-svr" -ElasticPoolName "my_hs_pool" -ComputeGeneration "PRMS"
```

---


## Migrate an existing General Purpose database to a zone redundant Hyperscale elastic pool

You can use the Azure CLI or Azure PowerShell to migrate an existing General Purpose database to a zone redundant Hyperscale elastic pool.

### [Azure CLI](#tab/azure-cli)

```azurecli
az sql db update --resource-group "myresourcegroup" --server "mylogicalserver" --name "gp_zrs_standalone_db" --elastic-pool "zr-hs-ep" --backup-storage-redundancy Zone
```


### [PowerShell](#tab/azure-powershell)

```powershell
Set-AzSqlDatabase -ResourceGroupName "myResourceGroup" -ServerName "mylogicalserver" -DatabaseName gp1 -ElasticPoolName "my-ZR-Hyperscale-EP" -BackupStorageRedundancy Zone
```

---

## REST API

Use the [2021-11-01](/rest/api/sql/elastic-pools/update?tabs=HTTP#update-high-availability-replica-count-of-a-hyperscale-elastic-pool) REST API (or later) to work with secondary replicas for Hyperscale elastic pools.

The following example scales out  an existing Hyperscale elastic pool to use four secondary replicas:

```rest
PATCH https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/elasticPools/{elasticPoolName}?api-version=2021-11-01-preview

{
  "properties": {
    "highAvailabilityReplicaCount": 4
  }
}
```

## Related content

- [Azure SQL Database CLI commands](/cli/azure/sql).
- [Azure SQL Database PowerShell cmdlets](/powershell/module/az.sql/).
- [Azure SQL Database elastic pools REST API](/rest/api/sql/elastic-pools/).
- [Hyperscale elastic pools overview](./hyperscale-elastic-pool-overview.md).
