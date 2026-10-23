---
title: "PowerShell: Copy a Database to New Logical Server"
description: Azure PowerShell example script to copy a database to a new logical server
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf
ms.date: 06/10/2025
ms.service: azure-sql-database
ms.subservice: data-movement
ms.topic: sample
ms.custom:
  - sqldbrb=1
  - devx-track-azurepowershell
ms.devlang: powershell
---
# Use PowerShell to copy a database to a new logical server

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This Azure PowerShell script example creates a copy of an existing database in Azure SQL Database in a new logical server.

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]
[!INCLUDE [updated-for-az](../../includes/updated-for-az.md)]
[!INCLUDE [cloud-shell-try-it.md](../../includes/cloud-shell-try-it.md)]

If you choose to install and use PowerShell locally, this tutorial requires Az PowerShell 1.4.0 or later. If you need to upgrade, see [Install Azure PowerShell module](/powershell/azure/install-az-ps). If you are running PowerShell locally, you also need to run `Connect-AzAccount` to create a connection with Azure.

## Copy a database to a new logical server

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/copy-database-to-new-server/copy-database-to-new-server-az-ps.ps1" id="FullScript":::

## Clean up deployment

Use the following command to remove  the resource group and all resources associated with it.

```powershell
Remove-AzResourceGroup -ResourceGroupName $sourceresourcegroupname
Remove-AzResourceGroup -ResourceGroupName $targetresourcegroupname
```

## Script explanation

This script uses the following commands. Each command in the table links to command specific documentation.

| Command | Notes |
|---|---|
| [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup) | Creates a resource group in which all resources are stored. |
| [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) | Creates a new logical server that hosts databases and elastic pools. |
| [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) | Creates a database or elastic pool. |
| [New-AzSqlDatabaseCopy](/powershell/module/az.sql/new-azsqldatabasecopy) | Creates a copy of a database that uses the snapshot at the current time. |
| [Remove-AzResourceGroup](/powershell/module/az.resources/remove-azresourcegroup) | Deletes a resource group including all nested resources. |

## Related content

- [Azure PowerShell documentation](/powershell/azure/)
- [Azure PowerShell samples for Azure SQL Database](../powershell-script-content-guide.md)
