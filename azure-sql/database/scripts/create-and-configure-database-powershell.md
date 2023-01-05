---
title: "PowerShell: Create a single database"
description: Use an Azure PowerShell example script to create a single database in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma
ms.date: 09/29/2022
ms.service: sql-database
ms.subservice: deployment-configuration
ms.topic: sample
ms.custom:
  - "sqldbrb=1"
  - "devx-track-azurepowershell"
ms.devlang: PowerShell
---
# Use PowerShell to create a single database and configure a server-level firewall rule

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqldb.md)]

This Azure PowerShell script example creates a single database in Azure SQL Database and configures a server-level firewall rule. After the script has been successfully run, the database can be accessed from all Azure services and the allowed IP address range.

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]
[!INCLUDE [updated-for-az](../../includes/updated-for-az.md)]
[!INCLUDE [cloud-shell-try-it.md](../../includes/cloud-shell-try-it.md)]

If you choose to install and use PowerShell locally, this tutorial requires Az PowerShell 1.4.0 or later. If you need to upgrade, see [Install Azure PowerShell module](/powershell/azure/install-az-ps). If you are running PowerShell locally, you also need to run `Connect-AzAccount` to create a connection with Azure.

## Sample script

[!code-powershell-interactive[main](~/../powershell_scripts/sql-database/create-and-configure-database/create-and-configure-database.ps1?highlight=15-16 "Create SQL Database")]

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
| [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) | Creates a database in a server. |
| [Remove-AzResourceGroup](/powershell/module/az.resources/remove-azresourcegroup) | Deletes a resource group including all nested resources. |

## Configure managed identities

For more information on the benefits of using a user-assigned managed identity for the server identity in Azure SQL Database, see [User-assigned managed identity in Azure AD for Azure SQL](../authentication-azure-ad-user-assigned-managed-identity.md).

To configure the system-assigned managed identity (SMI) or user-assigned managed identity or identities (UMI) of an Azure SQL Database, see [Get or set a managed identity for a logical server or managed instance](../authentication-azure-ad-user-assigned-managed-identity.md#get-or-set-a-managed-identity-for-a-logical-server-or-managed-instance).

## Next steps

For more information on Azure PowerShell, see [Azure PowerShell documentation](/powershell/azure/).

Additional SQL Database PowerShell script samples can be found in the [Azure SQL Database PowerShell scripts](../powershell-script-content-guide.md).