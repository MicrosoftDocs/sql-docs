---
title: Use Azure CLI to enable transparent data encryption
description: Enable transparent data encryption in Azure SQL Managed Instance using CLI and your own key.
author: MladjoA
ms.author: mlandzic
ms.reviewer: vanto
ms.date: 05/18/2022
ms.service: sql-managed-instance
ms.subservice: security
ms.topic: conceptual
ms.custom: kr2b-contr-experiment
ms.devlang: azurecli
---

# Azure CLI script to enable transparent data encryption using your own key

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqlmi.md)]

This Azure CLI script example configures transparent data encryption (TDE) in Azure SQL Managed Instance, using a customer-managed key from Azure Key Vault. This is often referred to as a bring-your-own-key (BYOK) scenario for TDE. To learn more about TDE with customer-managed key, see [TDE Bring Your Own Key to Azure SQL](../../database/transparent-data-encryption-byok-overview.md).

This sample requires an existing managed instance, see [Use Azure CLI to create an Azure SQL Managed Instance](create-configure-managed-instance-cli.md).

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [azure-cli-prepare-your-environment.md](../../includes/azure-cli-prepare-your-environment.md)]

## Sample script

[!INCLUDE [cli-run-local-sign-in.md](../../includes/cli-run-local-sign-in.md)]

### Run the script

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/transparent-data-encryption/setup-tde-byok-sqlmi.sh" id="FullScript":::

## Clean up resources

[!INCLUDE [cli-clean-up-resources.md](../../includes/cli-clean-up-resources.md)]

```azurecli
az group delete --name $resourceGroup
```

## Sample reference

This script uses the following commands. Each command in the table links to command specific documentation.

| Command | Description |
|---|---|
| [az sql db](/cli/azure/sql/db) | Database commands. |
| [az sql failover-group](/cli/azure/sql/failover-group) | Failover group commands. |

## Next steps

For more information on Azure CLI, see [Azure CLI documentation](/cli/azure).

Additional SQL Database CLI script samples can be found in the [Azure SQL Database documentation](../../database/az-cli-script-samples-content-guide.md).