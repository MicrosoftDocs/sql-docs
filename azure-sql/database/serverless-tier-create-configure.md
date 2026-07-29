---
title: Create and configure a serverless database
description: Learn how to create a new serverless database, move an existing database to the serverless compute tier, and modify serverless configuration in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: kendalv, moslake, mathoma, dfurman
ms.date: 07/28/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: how-to
ms.custom:
  - "devx-track-azurecli"
  - "devx-track-azurepowershell"
monikerRange: "=azuresql||=azuresql-db"
ai-usage: ai-assisted
---
# Create and configure a serverless database in Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article explains how to create a new [serverless](serverless-tier-overview.md) database, move an existing database to the serverless compute tier, and modify serverless configuration in Azure SQL Database.

<a id="create-serverless-db"></a>

## Create a new serverless database

Creating a new database or moving an existing database into a serverless compute tier follows the same pattern as creating a new database in provisioned compute tier. The process involves the following two steps:

1. Specify the service objective. The service objective prescribes the service tier, hardware configuration, and maximum vCores. For service objective options, see [serverless resource limits](resource-limits-vcore-single-databases.md#general-purpose---serverless-compute---gen5).

1. Optionally, specify the minimum vCores and auto-pause delay to change their default values. For more information, see [Auto-pause and auto-resume](serverless-tier-overview.md#default-settings).

The following examples create a new database in the serverless compute tier.

### Use Azure portal

See [Quickstart: Create a single database in Azure SQL Database using the Azure portal](single-database-create-quickstart.md).

### Use PowerShell

# [General Purpose](#tab/general-purpose)

Create a new serverless General Purpose database with the following PowerShell example: 

```powershell
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<new database name>"

$params = @{
    ResourceGroupName = $resourceGroupName
    ServerName = $serverName
    DatabaseName = $databaseName
    Edition = 'GeneralPurpose'
    ComputeModel = 'Serverless'
    ComputeGeneration = 'Gen5'
    MinVcore = 0.5
    MaxVcore = 2
    AutoPauseDelayInMinutes = 15
}
New-AzSqlDatabase @params
```

# [Hyperscale](#tab/hyperscale)

Create a new serverless Hyperscale database by using the following PowerShell example: 

```powershell
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<new database name>"

$params = @{
    ResourceGroupName = $resourceGroupName
    ServerName = $serverName
    DatabaseName = $databaseName
    Edition = 'Hyperscale'
    ComputeModel = 'Serverless'
    ComputeGeneration = 'Gen5'
    MinVcore = 0.5
    MaxVcore = 2
}
New-AzSqlDatabase @params
```

Create a new serverless Hyperscale database with one high availability replica and zone redundancy by using the following PowerShell example:

```powershell
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<new database name>"

$params = @{
    ResourceGroupName = $resourceGroupName
    ServerName = $serverName
    DatabaseName = $databaseName
    Edition = 'Hyperscale'
    ComputeModel = 'Serverless'
    ComputeGeneration = 'Gen5'
    MinVcore = 0.5
    MaxVcore = 2
    HighAvailabilityReplicaCount = 1
    BackupStorageRedundancy = 'Zone'
    ZoneRedundant = $true
}
New-AzSqlDatabase @params
```

---

### Use Azure CLI

# [General Purpose](#tab/general-purpose)

Create a new serverless General Purpose database by using the following Azure CLI example: 

```azurecli
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<database name>"

az sql db create -g $resourceGroupName `
-s $serverName `
-n $databaseName `
-e GeneralPurpose `
--compute-model Serverless `
-f Gen5 `
--min-capacity 0.5 `
-c 2 `
--auto-pause-delay 15

```

# [Hyperscale](#tab/hyperscale)

Create a new serverless Hyperscale database by using the following Azure CLI example: 

```azurecli
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<database name>"

az sql db create -g $resourceGroupName `
-s $serverName `
-n $databaseName `
-e Hyperscale `
--compute-model Serverless `
-f Gen5 `
--min-capacity 0.5 `
-c 2 
```

Create a new serverless Hyperscale database with one high availability replica and zone redundancy by using the following Azure CLI example:

```azurecli
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<database name>"

az sql db create -g $resourceGroupName `
-s $serverName `
-n $databaseName `
-e Hyperscale `
--compute-model Serverless `
-f Gen5 `
--min-capacity 0.5 `
-c 2 `
--ha-replicas 1 `
--backup-storage-redundancy Zone `
--zone-redundant

```

---


### Use Transact-SQL (T-SQL)

When you use T-SQL to create a new serverless database, the system applies default values for the minimum vCores and auto-pause delay. You can later change these values from the Azure portal or via API, including PowerShell, Azure CLI, and REST.

For details, see [CREATE DATABASE](/sql/t-sql/statements/create-database-transact-sql?view=azuresqldb-current&preserve-view=true). 

# [General Purpose](#tab/general-purpose)

Create a new General Purpose serverless database with the following T-SQL example: 

```sql
CREATE DATABASE testdb
( EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_S_Gen5_1' ) ;
```

# [Hyperscale](#tab/hyperscale)

Create a new Hyperscale serverless database with the following T-SQL example: 

```sql
CREATE DATABASE testdb
( EDITION = 'Hyperscale', SERVICE_OBJECTIVE = 'HS_S_Gen5_2') ; 
```

---

## Move a database between compute tiers or service tiers

You can move a database between the provisioned compute tier and serverless compute tier.

You can also move a serverless database from the General Purpose service tier to the Hyperscale service tier. For more information, see [Convert an existing database to Hyperscale](convert-to-hyperscale.md).

When you move a database between compute tiers, specify the **compute model** parameter as either `Serverless` or `Provisioned` when using PowerShell or Azure CLI. When using T-SQL, specify the `SERVICE_OBJECTIVE`. Review [resource limits](resource-limits-vcore-single-databases.md) to identify the appropriate service objective.  

The following examples move an existing database from provisioned compute to serverless. 

### Use PowerShell

# [General Purpose](#tab/general-purpose)

Move a provisioned compute General Purpose database to the serverless compute tier by using the following PowerShell example: 

```powershell
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<new database name>"

$params = @{
    ResourceGroupName = $resourceGroupName
    ServerName = $serverName
    DatabaseName = $databaseName
    Edition = 'GeneralPurpose'
    ComputeModel = 'Serverless'
    ComputeGeneration = 'Gen5'
    MinVcore = 1
    MaxVcore = 4
    AutoPauseDelayInMinutes = 1440
}
Set-AzSqlDatabase @params
```

# [Hyperscale](#tab/hyperscale)

Move a provisioned compute Hyperscale database to the serverless compute tier by using the following PowerShell example: 

```powershell
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<new database name>"

$params = @{
    ResourceGroupName = $resourceGroupName
    ServerName = $serverName
    DatabaseName = $databaseName
    Edition = 'Hyperscale'
    ComputeModel = 'Serverless'
    ComputeGeneration = 'Gen5'
    MinVcore = 1
    MaxVcore = 4
}
Set-AzSqlDatabase @params
```

---

### Use Azure CLI

# [General Purpose](#tab/general-purpose)

To move a provisioned compute General Purpose database to the serverless compute tier, use the following Azure CLI example: 

```azurecli
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<database name>"

az sql db update -g $resourceGroupName `
-s $serverName `
-n $databaseName `
--edition GeneralPurpose `
--compute-model Serverless `
--family Gen5 `
--min-capacity 1 `
--capacity 4 `
--auto-pause-delay 1440

```

# [Hyperscale](#tab/hyperscale)

To move a provisioned compute Hyperscale database to the serverless compute tier, use the following Azure CLI example: 

```azurecli
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<database name>"

az sql db update -g $resourceGroupName `
-s $serverName `
-n $databaseName `
--edition Hyperscale `
--compute-model Serverless `
--family Gen5 `
--min-capacity 1 `
--capacity 4

```

---

### Use Transact-SQL (T-SQL)

When you use T-SQL to move a database between compute tiers, the operation applies default values for the minimum vCores and auto-pause delay. You can change these values later from the Azure portal or via API, including PowerShell, Azure CLI, and REST. For more information, see [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql?view=azuresqldb-current&preserve-view=true).

# [General Purpose](#tab/general-purpose)

To move a provisioned compute General Purpose database to the serverless compute tier, use the following T-SQL example: 

```sql
ALTER DATABASE testdb 
MODIFY ( SERVICE_OBJECTIVE = 'GP_S_Gen5_1') ;
```

# [Hyperscale](#tab/hyperscale)

To move a provisioned compute Hyperscale database to the serverless compute tier, use the following T-SQL example: 

```sql
ALTER DATABASE testdb  
MODIFY ( SERVICE_OBJECTIVE = 'HS_S_Gen5_2') ; 
```

---

## Modify serverless configuration

### Use PowerShell

Use [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) to change the maximum or minimum vCores, and the auto-pause delay. Use the `MaxVcore`, `MinVcore`, and `AutoPauseDelayInMinutes` parameters. The Hyperscale tier doesn't currently support serverless auto-pausing, so the auto-pause delay parameter only applies to the General Purpose tier.

For example, to modify the `MaxVcore`, `MinVcore`, or `AutoPauseDelayInMinutes`

```powershell
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<new database name>"

$params = @{
    ResourceGroupName = $resourceGroupName
    ServerName = $serverName
    DatabaseName = $databaseName
    MinVcore = 1
    MaxVcore = 4
    AutoPauseDelayInMinutes = 1440
}
Set-AzSqlDatabase @params
```

### Use Azure CLI

Use [az sql db update](/cli/azure/sql/db#az-sql-db-update) to change the maximum or minimum vCores, and the auto-pause delay. Use the `capacity`, `min-capacity`, and `auto-pause-delay` parameters. The Hyperscale tier doesn't currently support serverless auto-pausing, so the auto-pause delay parameter only applies to the General Purpose tier. 

For example, to modify the database to use a different minimum or maximum number of vCores, or to change the autopause delay:

```azurecli
$resourceGroupName = "<resource group name>"
$serverName = "<logical SQL server name>"
$databaseName = "<database name>"

az sql db update -g $resourceGroupName `
-s $serverName `
-n $databaseName `
--min-capacity 1 `
--capacity 4 `
--auto-pause-delay 1440

```

## Related content

- [Serverless compute tier for Azure SQL Database](serverless-tier-overview.md)
- [Serverless compute tier billing](serverless-tier-billing.md)
- [Auto-pause and auto-resume in serverless](serverless-tier-auto-pause-resume.md)
- [Resource limits for single databases using the vCore purchasing model](resource-limits-vcore-single-databases.md)
