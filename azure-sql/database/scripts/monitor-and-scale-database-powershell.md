---
title: Use PowerShell to Monitor and Scale a Single Database
description: Use an Azure PowerShell example script to monitor and scale a single database in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf
ms.date: 06/10/2025
ms.service: azure-sql-database
ms.subservice: performance
ms.topic: sample
ms.custom:
  - sqldbrb=1
  - devx-track-azurepowershell
ms.devlang: powershell
---

# Use PowerShell to monitor and scale a single database in Azure SQL Database

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This PowerShell script example monitors the performance metrics of a single database, scales it to a higher compute size, and creates an alert rule on one of the performance metrics.

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]
[!INCLUDE [updated-for-az](../../includes/updated-for-az.md)]
[!INCLUDE [cloud-shell-try-it.md](../../includes/cloud-shell-try-it.md)]

If you choose to install and use PowerShell locally, this tutorial requires Az PowerShell 1.4.0 or later. If you need to upgrade, see [Install Azure PowerShell module](/powershell/azure/install-az-ps). If you are running PowerShell locally, you also need to run `Connect-AzAccount` to create a connection with Azure.

## Sample script

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/monitor-and-scale-database/monitor-and-scale-database-az-ps.ps1" id="FullScript":::

> [!NOTE]
> For a full list of metrics, see [metrics supported](/azure/azure-monitor/essentials/metrics-supported#microsoftsqlserversdatabases).
> [!TIP]
> Use [Get-AzSqlDatabaseActivity](/powershell/module/az.sql/get-azsqldatabaseactivity) to get the status of database operations and use [Stop-AzSqlDatabaseActivity](/powershell/module/az.sql/stop-azsqldatabaseactivity) to cancel a database update operation.

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
| [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) | Creates a server that hosts a single database or elastic pool. |
| [Get-AzMetric](/powershell/module/az.monitor/get-azmetric) | Shows the size usage information for the database.|
| [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) | Updates database properties or moves the database into, out of, or between elastic pools. |
| [Add-AzMetricAlertRule](/powershell/module/az.monitor/add-azmetricalertrule) | (*Deprecated*) Adds or updates an alert rule to automatically monitor metrics in the future. Applies only to classic metric-based alert rules. |
| [Add-AzMetricAlertRuleV2](/powershell/module/az.monitor/add-azmetricalertruleV2) | Adds or updates an alert rule to automatically monitor metrics in the future. Applies only to non-classic metric-based alert rules. |
| [Remove-AzResourceGroup](/powershell/module/az.resources/remove-azresourcegroup) | Deletes a resource group including all nested resources. |

## Related content

- [Azure PowerShell documentation](/powershell/azure/)
- [Azure PowerShell samples for Azure SQL Database](../powershell-script-content-guide.md)
