---
title: "Azure CLI Example: Failover Group - Azure SQL Database Elastic Pool"
description: Use this Azure CLI example script to create an Azure SQL Database elastic pool, add it to a failover group, and test failover.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 06/10/2025
ms.service: azure-sql-database
ms.subservice: high-availability
ms.topic: sample
ms.custom:
  - devx-track-azurecli
ms.devlang: azurecli
---

# Add an Azure SQL Database elastic pool to a failover group using the Azure CLI

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This Azure CLI script example creates a single database, adds it to an elastic pool, creates a failover group, and tests failover.

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../reusable-content/azure-cli/azure-cli-prepare-your-environment.md)]

## Sample script

[!INCLUDE [cli-launch-cloud-shell-sign-in.md](../../includes/cli-launch-cloud-shell-sign-in.md)]

### Run the script

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-elastic-pool-to-failover-group-az-cli.sh" id="FullScript":::

## Clean up resources

[!INCLUDE [cli-clean-up-resources.md](../../includes/cli-clean-up-resources.md)]

```azurecli
az group delete --name $resourceGroup
```

## Sample reference

This script uses the following commands. Each command in the table links to command specific documentation.

| Command | Description |
|---|---|
| [az sql elastic-pool](/cli/azure/sql/elastic-pool) | Elastic pool commands. |
| [az sql failover-group](/cli/azure/sql/failover-group) | Failover group commands. |

## Related content

- [Azure CLI documentation](/cli/azure/overview)
- [Azure CLI samples for Azure SQL Database](../az-cli-script-samples-content-guide.md)