---
title: "Azure CLI Example: Copy Database to New Logical Server"
description: Use this Azure CLI example script to copy a database in Azure SQL Database to a new logical server
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 01/23/2026
ms.service: azure-sql-database
ms.subservice: data-movement
ms.topic: sample
ms.custom:
  - devx-track-azurecli
ms.devlang: azurecli
---

# Copy a database in Azure SQL Database to a new logical server using the Azure CLI

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This Azure CLI script example creates a copy of an existing database in a new Azure SQL Database logical server.

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../reusable-content/azure-cli/azure-cli-prepare-your-environment.md)]

## Sample script

[!INCLUDE [cli-launch-cloud-shell-sign-in.md](../../includes/cli-launch-cloud-shell-sign-in.md)]

### Run the script

[!INCLUDE [server-admin-login-security-note](../../includes/server-admin-login-security-note.md)]

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/copy-database-to-new-server/copy-database-to-new-server.sh" id="FullScript":::

## Clean up resources

[!INCLUDE [cli-clean-up-resources.md](../../includes/cli-clean-up-resources.md)]

```azurecli
az group delete --name $targetResourceGroup
az group delete --name $resourceGroup
```

## Sample reference

This script uses the following commands. Each command in the table links to command specific documentation.

| Command | Description |
|---|---|
| [az sql db copy](/cli/azure/sql/db#az-sql-db-copy) | Creates a copy of a database that uses the snapshot at the current time. |

## Related content

- [Azure CLI documentation](/cli/azure)
- [Azure CLI samples for Azure SQL Database](../az-cli-script-samples-content-guide.md)