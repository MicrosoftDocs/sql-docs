---
title: "Azure CLI Example: Configure a Failover Group for a Group of Databases in Azure SQL Database"
description: Use this Azure CLI example script to set up a failover group for a set of databases in Azure SQL Database and fail it over.
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

# Configure a failover group for a group of databases in Azure SQL Database using the Azure CLI

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../reusable-content/azure-cli/azure-cli-prepare-your-environment.md)]

## Sample script

[!INCLUDE [cli-launch-cloud-shell-sign-in.md](../../includes/cli-launch-cloud-shell-sign-in.md)]

### Run the script

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/setup-geodr-and-failover/setup-geodr-and-failover-database-failover-group.sh" id="FullScript":::

## Clean up resources

[!INCLUDE [cli-clean-up-resources.md](../../includes/cli-clean-up-resources.md)]

```azurecli
az group delete --name $failoverResourceGroup -y
az group delete --name $resourceGroup
```

## Sample reference

This script uses the following commands. Each command in the table links to command specific documentation.

| Command | Description |
|---|---|
| [az sql failover-group create](/cli/azure/sql/failover-group#az-sql-failover-group-create) | Creates a failover group. |
| [az sql failover-group set-primary](/cli/azure/sql/failover-group#az-sql-failover-group-set-primary) | Set the primary of the failover group by failing over all databases from the current primary server |
| [az sql failover-group show](/cli/azure/sql/failover-group) | Gets a failover group |
| [az sql failover-group delete](/cli/azure/sql/failover-group) | Deletes a failover group |

## Related content

- [Azure CLI documentation](/cli/azure)
- [Azure CLI samples for Azure SQL Database](../az-cli-script-samples-content-guide.md)
