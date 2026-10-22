---
title: "PowerShell: Import a BACPAC File to a New Database"
description: Azure PowerShell example script to import a BACPAC file into a database in SQL Database
author: dnethi
ms.author: dinethi
ms.reviewer: wiassaf
ms.date: 06/10/2025
ms.service: azure-sql-database
ms.subservice: backup-restore
ms.topic: sample
ms.custom:
  - "load & move data"
  - "sqldbrb=1"
  - "devx-track-azurepowershell"
ms.devlang: powershell
---
# Use PowerShell to import a BACPAC file into a database in SQL Database

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This Azure PowerShell script example imports a database from a BACPAC file into a new database in SQL Database.  

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]
[!INCLUDE [updated-for-az](../../includes/updated-for-az.md)]
[!INCLUDE [cloud-shell-try-it.md](../../includes/cloud-shell-try-it.md)]

If you choose to install and use PowerShell locally, this tutorial requires Az PowerShell 1.4.0 or later. If you need to upgrade, see [Install Azure PowerShell module](/powershell/azure/install-az-ps). If you are running PowerShell locally, you also need to run `Connect-AzAccount` to create a connection with Azure.

## Sample script

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/import-from-bacpac/import-from-bacpac-az-ps.ps1" id="FullScript":::

## Clean up deployment

Use the following command to remove  the resource group and all resources associated with it.

```powershell
Remove-AzResourceGroup -ResourceGroupName $resourcegroupname
```

## Script explanation

This script uses the following commands. Each command in the table links to command-specific documentation.

| Command | Notes |
|---|---|
| [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup) | Creates a resource group in which all resources are stored. |
| [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) | Creates a server that hosts databases and elastic pools. |
| [New-AzSqlServerFirewallRule](/powershell/module/az.sql/new-azsqlserverfirewallrule) | Creates a server-level firewall rule for a server. |
| [New-AzSqlDatabaseImport](/powershell/module/az.sql/new-azsqldatabaseimport) | Imports a BACPAC file and create a new database on the server. |
| [Remove-AzResourceGroup](/powershell/module/az.resources/remove-azresourcegroup) | Deletes a resource group including all nested resources. |

## Related content

- [Azure PowerShell documentation](/powershell/azure/)
- [Azure PowerShell samples for Azure SQL Database](../powershell-script-content-guide.md)
