---
title: Create a zone-redundant Hyperscale database
description: Learn how to create a zone-redundant Hyperscale database. 
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf, mathoma
ms.date: 10/12/2023
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: conceptual
---

# Create a zone-redundant Hyperscale database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article teaches you to create a zone-redundant Hyperscale database by creating a new database, creating a geo-replica, or using database copy. 

## Create a zone-redundant database

Use [Azure PowerShell](/powershell/azure/install-az-ps) or the [Azure CLI](/cli/azure/update-azure-cli) to create a zone redundant Hyperscale database. Confirm you have the latest version of the API to ensure support for any recent changes.

# [Azure PowerShell](#tab/azure-powershell)

Specify the `-ZoneRedundant` parameter to enable zone redundancy for your Hyperscale database by using Azure PowerShell. The database must have at least 1 high availability replica and zone-redundant backup storage must be specified.

To enable zone redundancy using Azure PowerShell, use the following example command:

```powershell
New-AzSqlDatabase -ResourceGroupName "ResourceGroup01" -ServerName "Server01" -DatabaseName "Database01" `
    -Edition "Hyperscale" -HighAvailabilityReplicaCount 1 -ZoneRedundant -BackupStorageRedundancy Zone -RequestedServiceObjectiveName HS_Gen5_2'
```

# [Azure CLI](#tab/azure-cli)

Specify the `-zone-redundant parameter` to enable zone redundancy for your Hyperscale database by using the Azure CLI. The database copy must have at least 1 high availability replica and zone-redundant backup storage.

To enable zone redundancy using the Azure CLI, use the following example command:

```azurecli
az sql db create -g mygroup -s myserver -n mydb -e Hyperscale -f Gen5 –-ha-replicas 1 –-zone-redundant -–backup-storage-redundancy Zone --capacity 2
```

---

## Create a database by creating a geo-replica

To make an existing Hyperscale database zone redundant, use Azure PowerShell or the Azure CLI to create a zone redundant Hyperscale database using active geo-replication.  The geo-replica can be in the same or different region as the existing Hyperscale database.

# [Azure PowerShell](#tab/azure-powershell)

Specify the `-ZoneRedundant` parameter to enable zone redundancy for your Hyperscale database secondary. The secondary database must have at least 1 high availability replica and zone-redundant backup storage must be specified.

To create your zone redundant database using Azure PowerShell, use the following example command:

```powershell
New-AzSqlDatabaseSecondary -ResourceGroupName "myResourceGroup" -ServerName $sourceserver -DatabaseName "databaseName" -PartnerResourceGroupName "myPartnerResourceGroup" -PartnerServerName $targetserver -PartnerDatabaseName "zoneRedundantCopyOfMySampleDatabase" -ZoneRedundant -BackupStorageRedundancy Zone -HighAvailabilityReplicaCount 1
```

# [Azure CLI](#tab/azure-cli)

Specify the `-zone-redundant parameter` to enable zone redundancy for your Hyperscale database secondary. The secondary database must have at least 1 high availability replica and zone-redundant backup storage.

To enable zone redundancy using the Azure CLI, use the following example command:

```azurecli
az sql db replica create -g mygroup -s myserver -n originalDb --partner-server newDb -–ha-replicas 1 -–zone-redundant -–backup-storage-redundancy Zone
```

---

## Create a database by using database copy

To make an existing Hyperscale database zone redundant, use Azure PowerShell or the Azure CLI to create a zone redundant Hyperscale database using database copy.  The database copy can be in the same or different region as the existing Hyperscale database.

# [Azure PowerShell](#tab/azure-powershell)

Specify the `-ZoneRedundant` parameter to enable zone redundancy for your Hyperscale database copy. The database copy must have at least 1 high availability replica and zone-redundant backup storage must be specified.

To create your zone redundant database using Azure PowerShell, use the following example command:

```powershell
New-AzSqlDatabaseCopy -ResourceGroupName "myResourceGroup" -ServerName $sourceserver -DatabaseName "databaseName" -CopyResourceGroupName "myCopyResourceGroup" -CopyServerName $copyserver -CopyDatabaseName "zoneRedundantCopyOfMySampleDatabase" -ZoneRedundant -BackupStorageRedundancy Zone
```

# [Azure CLI](#tab/azure-cli)

Specify the `-zone-redundant parameter` to enable zone redundancy for your Hyperscale database copy. The database copy must have at least 1 high availability replica and zone-redundant backup storage.

To enable zone redundancy using the Azure CLI, use the following example command:

```azurecli
az sql db copy --dest-name "CopyOfMySampleDatabase" --dest-resource-group "myResourceGroup" --dest-server $targetserver --name "<databaseName>" --resource-group "<resourceGroup>" --server $sourceserver -–ha-replicas 1 -–zone-redundant -–backup-storage-redundancy Zone
```

---

## Next steps

Learn more about Hyperscale databases in the following articles:

- [Quickstart: Create a Hyperscale database in Azure SQL Database](hyperscale-database-create-quickstart.md)
- [Hyperscale service tier](service-tier-hyperscale.md)
- [Azure SQL Database Hyperscale FAQ](service-tier-hyperscale-frequently-asked-questions-faq.yml)
- [Hyperscale secondary replicas](service-tier-hyperscale-replicas.md)
- [Azure SQL Database Hyperscale named replicas FAQ](service-tier-hyperscale-frequently-asked-questions-faq.yml#read-scale-out-questions)
