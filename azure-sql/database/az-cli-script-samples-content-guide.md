---
title: Azure CLI samples
description: Find Azure CLI script samples to create and manage Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 04/30/2024
ms.service: azure-sql-database
ms.subservice: deployment-configuration
ms.topic: sample
ms.custom:
  - overview-samples
  - mvc
  - azure-sql-split
  - devx-track-azurecli
  - seo-azure-cli
keywords:
  - "sql database"
  - "managed instance"
  - "azure cli samples"
  - "azure cli examples"
  - "azure cli code samples"
  - "azure cli script examples"
ms.devlang: azurecli
monikerRange: "= azuresql || = azuresql-db"
---

# Azure CLI samples for Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](az-cli-script-samples-content-guide.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/az-cli-script-samples-content-guide.md?view=azuresql&preserve-view=true)

You can configure Azure SQL Database by using the <a href="/cli/azure">Azure CLI</a>.

[!INCLUDE [quickstarts-free-trial-note](../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../azure-sql/reusable-content/azure-cli/azure-cli-prepare-your-environment.md)]

## Samples


The following table includes links to Azure CLI script examples to manage single and pooled databases in Azure SQL Database.

|Area|Description|
|---|---|
|**Create databases**||
| [Create a single database](scripts/create-and-configure-database-cli.md) | Creates a SQL Database and configures a server-level firewall rule. |
| [Create pooled databases](scripts/move-database-between-elastic-pools-cli.md) | Creates elastic pools, moves pooled databases, and changes compute sizes. |
|**Scale databases**||
| [Scale a single database](scripts/monitor-and-scale-database-cli.md) | Scales single database. |
| [Scale pooled database](scripts/scale-pool-cli.md) | Scales a SQL elastic pool to a different compute size. |
|**Configure geo-replication**||
| [Single database](scripts/setup-geodr-failover-database-cli.md)| Configures active geo-replication for a database in Azure SQL Database and fails it over to the secondary replica. |
| [Pooled database](scripts/setup-geodr-failover-pool-cli.md)| Configures active geo-replication for a database in an elastic pool, then fails it over to the secondary replica. |
|**Configure failover group**||
| [Configure failover group](scripts/setup-geodr-failover-group-cli.md) | Configures a failover group for a group of databases and failover over databases to the secondary server. |
| [Single database](scripts/add-database-to-failover-group-cli.md)| Creates a database and a failover group, adds the database to the failover group, then tests failover to the secondary server. |
| [Pooled database](scripts/add-elastic-pool-to-failover-group-cli.md) | Creates a database, adds it to an elastic pool, adds the elastic pool to the failover group, then tests failover to the secondary server. |
| **Back up, restore, copy, and import a database**||
| [Back up a database](scripts/backup-database-cli.md)| Backs up a database in SQL Database to an Azure storage backup. |
| [Restore a database](scripts/restore-database-cli.md)| Restores a database in SQL Database to a specific point in time. |
| [Copy a database to a new server](scripts/copy-database-to-new-server-cli.md) | Creates a copy of an existing database in SQL Database in a new server. |
| [Import a database from a BACPAC file](scripts/import-from-bacpac-cli.md)| Imports a database to SQL Database from a BACPAC file. |


Learn more about the [single-database Azure CLI API](single-database-manage.md#azure-cli).

## Related content 

- [Az sql cmdlets](/cli/azure/sql)
- [PowerShell scripts](powershell-script-content-guide.md)