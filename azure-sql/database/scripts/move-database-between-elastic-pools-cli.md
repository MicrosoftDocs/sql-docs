---
title: "Azure CLI Example: Move a Database Between Elastic Pools"
description: Use this Azure CLI example script to create two elastic pools and move a database in SQL Database from one elastic pool to another.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 06/10/2025
ms.service: azure-sql-database
ms.subservice: elastic-pools
ms.topic: sample
ms.custom:
  - sqldbrb=1
  - devx-track-azurecli
ms.devlang: azurecli
---

# Move a database in SQL Database in a SQL elastic pool using the Azure CLI

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This Azure CLI script example creates two elastic pools, moves a pooled database in SQL Database from one SQL elastic pool into another SQL elastic pool, and then moves the pooled database out of the SQL elastic pool to be a single database in SQL Database.

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../azure-sql/reusable-content/azure-cli/azure-cli-prepare-your-environment.md)]

## Sample script

[!INCLUDE [cli-launch-cloud-shell-sign-in.md](../../includes/cli-launch-cloud-shell-sign-in.md)]

### Run the script

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/move-database-between-pools/move-database-between-pools.sh" id="FullScript":::

## Clean up resources

[!INCLUDE [cli-clean-up-resources.md](../../includes/cli-clean-up-resources.md)]

```azurecli
az group delete --name $resourceGroup
```

## Sample reference

This script uses the following commands. Each command in the table links to command-specific documentation.

| Command | Description |
|---|---|
| [az sql server](/cli/azure/sql/server) | Server commands. |
| [az sql elastic-pools](/cli/azure/sql/elastic-pool) | Elastic pool commands. |
| [az sql db](/cli/azure/sql/db) | Database commands. |

## Related content

- [Azure CLI documentation](/cli/azure)
- [Azure CLI samples for Azure SQL Database](../az-cli-script-samples-content-guide.md)
