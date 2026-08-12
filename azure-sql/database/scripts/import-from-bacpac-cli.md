---
title: "Azure CLI Example: Import BACPAC File to Database in Azure SQL Database"
description: Use this Azure CLI example script to import a BACPAC file into a database in Azure SQL Database
author: dinethi
ms.author: dinethi
ms.reviewer: mathoma
ms.date: 06/10/2025
ms.service: azure-sql-database
ms.subservice: backup-restore
ms.topic: sample
ms.custom:
  - load & move data
  - devx-track-azurecli
ms.devlang: azurecli
---

# Import a BACPAC file into a database in SQL Database using the Azure CLI

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This Azure CLI script example imports a database from a *.bacpac* file into a database in SQL Database.  

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../reusable-content/azure-cli/azure-cli-prepare-your-environment.md)]

## Sample script

[!INCLUDE [cli-run-local-sign-in.md](../../includes/cli-run-local-sign-in.md)]

### Run the script

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/import-from-bacpac/import-from-bacpac.sh" id="FullScript":::

## Clean up resources

[!INCLUDE [cli-clean-up-resources.md](../../includes/cli-clean-up-resources.md)]

```azurecli
az group delete --name $resourceGroup
```

## Sample reference

This script uses the following commands. Each command in the table links to command specific documentation.

| Command | Description |
|---|---|
| [az sql server](/cli/azure/sql/server) | Server commands. |
| [az sql db import](/cli/azure/sql/db#az-sql-db-import) | Database import command. |

## Related content

- [Azure CLI documentation](/cli/azure)
- [Azure CLI samples for Azure SQL Database](../az-cli-script-samples-content-guide.md)
