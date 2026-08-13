---
title: "PowerShell: Add a Database to a Failover Group"
description: Use an Azure PowerShell example script to create a database in Azure SQL Database, add it to a failover group, and test failover.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer:  mathoma
ms.date: 06/10/2025
ms.service: azure-sql-database
ms.subservice: high-availability
ms.topic: sample
ms.custom:
  - sqldbrb=1
  - devx-track-azurepowershell
ms.devlang: powershell
---

# Use PowerShell to add a database to a failover group

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](add-database-to-failover-group-powershell.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../../managed-instance/scripts/add-to-failover-group-powershell.md?view=azuresql-mi&preserve-view=true)

This PowerShell script example creates a single database in Azure SQL Database, creates a failover group, adds the database to it, and tests failover.

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]
[!INCLUDE [updated-for-az](../../includes/updated-for-az.md)]
[!INCLUDE [cloud-shell-try-it.md](../../includes/cloud-shell-try-it.md)]

If you choose to install and use PowerShell locally, this tutorial requires Az PowerShell 1.4.0 or later. If you need to upgrade, see [Install Azure PowerShell module](/powershell/azure/install-az-ps). If you're running PowerShell locally, you also need to run `Connect-AzAccount` to create a connection with Azure.

## Sample scripts

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-single-db-to-failover-group-az-ps.ps1" id="FullScript":::

## Clean up deployment

Use the following command to remove  the resource group and all resources associated with it.

```powershell
Remove-AzResourceGroup -ResourceGroupName $resourceGroupName
```

## Script explanation

This script uses the following commands. Each command in the table links to command specific documentation.

| Command | Notes |
|---|---|
| [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup) | Creates a resource group in which all resources are stored. |
| [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) | Creates a server. |
| [New-AzSqlServerFirewallRule](/powershell/module/az.sql/new-azsqlserverfirewallrule) | Creates a server-level firewall rule for a server. |
| [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) | Creates a new database. |
| [New-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/new-azsqldatabasefailovergroup) | Creates a new failover group. |
| [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) | Gets one or more databases. |
| [Add-AzSqlDatabaseToFailoverGroup](/powershell/module/az.sql/add-azsqldatabasetofailovergroup) | Adds one or more databases to a failover group. |
| [Get-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/get-azsqldatabasefailovergroup) | Gets or lists failover groups. |
| [Switch-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/switch-azsqldatabasefailovergroup)| Executes a failover of a failover group. |
| [Remove-AzResourceGroup](/powershell/module/az.resources/remove-azresourcegroup) | Removes a resource group |

## Related content

- [Azure PowerShell documentation](/powershell/azure/)

- [Azure SQL Database PowerShell scripts](../powershell-script-content-guide.md)