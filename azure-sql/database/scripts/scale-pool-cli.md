---
title: "Azure CLI Example: Scale an Elastic Pool"
description: Use an Azure CLI example script to scale an elastic pool in Azure SQL Database.
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

# Scale an elastic pool in Azure SQL Database using the Azure CLI

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This Azure CLI script example creates elastic pools in Azure SQL Database, moves pooled databases, and changes elastic pool compute sizes.

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../reusable-content/azure-cli/azure-cli-prepare-your-environment.md)]

## Sample script

[!INCLUDE [cli-launch-cloud-shell-sign-in.md](../../includes/cli-launch-cloud-shell-sign-in.md)]

### Run the script

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/scale-pool/scale-pool.sh" id="FullScript"

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
| [az sql db](/cli/azure/sql/db) | Database commands. |
| [az sql elastic-pools](/cli/azure/sql/elastic-pool) | Elastic pool commands. |

## Related content

- [Azure CLI documentation](/cli/azure)
- [Azure CLI samples for Azure SQL Database](../az-cli-script-samples-content-guide.md)
