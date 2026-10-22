---
title: "PowerShell: Restore an Automatic Backup of a Database"
description: Use an Azure PowerShell example script to restore a database in SQL Database to an earlier point in time from automatic backups.
author: dnethi
ms.author: dinethi
ms.reviewer: wiassaf
ms.date: 06/10/2025
ms.service: azure-sql-database
ms.subservice: backup-restore
ms.topic: sample
ms.custom:
  - devx-track-azurepowershell
ms.devlang: powershell
---

# Use PowerShell to restore a database to an earlier point in time

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This PowerShell script example restores a database in SQL Database to a specific point in time.  

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]
[!INCLUDE [updated-for-az](../../includes/updated-for-az.md)]
[!INCLUDE [cloud-shell-try-it.md](../../includes/cloud-shell-try-it.md)]

If you choose to install and use PowerShell locally, this tutorial requires Az PowerShell 1.4.0 or later. If you need to upgrade, see [Install Azure PowerShell module](/powershell/azure/install-az-ps). If you are running PowerShell locally, you also need to run `Connect-AzAccount` to create a connection with Azure.

## Sample script

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/restore-database/restore-database-az-ps.ps1" id="FullScript":::

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
| [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) | Creates a database in a server. |
| [Get-AzSqlDatabaseGeoBackup](/powershell/module/az.sql/get-azsqldatabasegeobackup) | Gets a geo-redundant backup of a standalone or pooled database. |
| [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) | Restores a database. |
| [Remove-AzSqlDatabase](/powershell/module/az.sql/remove-azsqldatabase) | Removes a database. |
| [Get-AzSqlDeletedDatabaseBackup](/powershell/module/az.sql/get-azsqldeleteddatabasebackup) | Gets a deleted database that you can restore. |
| [Remove-AzResourceGroup](/powershell/module/az.resources/remove-azresourcegroup) | Deletes a resource group including all nested resources. |

## Related content

- [Azure PowerShell documentation](/powershell/azure/)
- [Azure PowerShell samples for Azure SQL Database](../powershell-script-content-guide.md)
