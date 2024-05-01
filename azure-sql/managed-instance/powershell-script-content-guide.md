---
title: Azure PowerShell script examples
description: Use Azure PowerShell script examples to help you create and manage Azure SQL Managed Instance resources.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 04/30/2024
ms.service: sql-managed-instance
ms.subservice: development
ms.topic: sample
ms.custom: azure-sql-split, devx-track-azurepowershell
ms.devlang: powershell
monikerRange: "= azuresql || = azuresql-mi"
---

# Azure PowerShell samples for Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/powershell-script-content-guide.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](powershell-script-content-guide.md?view=azuresql&preserve-view=true)

Azure SQL Managed Instance enables you to configure your instances, and pools by using Azure PowerShell.

[!INCLUDE [quickstarts-free-trial-note](../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [cloud-shell-try-it.md](../includes/cloud-shell-try-it.md)]

If you choose to install and use the PowerShell locally, this tutorial requires AZ PowerShell 1.4.0 or later. If you need to upgrade, see [Install Azure PowerShell module](/powershell/azure/install-az-ps). If you are running PowerShell locally, you also need to run `Connect-AzAccount` to create a connection with Azure.


## Samples

The following table includes links to sample Azure PowerShell scripts for Azure SQL Managed Instance.

|Link|Description|
|---|---|
|**Create and configure managed instances**||
| [Create and manage a managed instance](../managed-instance/scripts/create-configure-managed-instance-powershell.md) | This PowerShell script shows you how to create and manage a managed instance using Azure PowerShell. |
| [Create and manage a managed instance using the Azure Resource Manager template](../managed-instance/create-template-quickstart.md) | This PowerShell script shows you how to create and manage a managed instance using Azure PowerShell and the Azure Resource Manager template.|
| [Restore database to a managed instance in another geo-region](../managed-instance/scripts/restore-geo-backup.md) | This PowerShell script takes a backup of one database and restores it to another region. This is known as a geo-restore disaster-recovery scenario. |
| **Configure transparent data encryption**||
| [Manage transparent data encryption in a managed instance using your own key from Azure Key Vault](../managed-instance/scripts/transparent-data-encryption-byok-powershell.md)| This PowerShell script configures transparent data encryption in a Bring Your Own Key scenario for Azure SQL Managed Instance, using a key from Azure Key Vault.|
|**Configure a failover group**||
| [Configure a failover group for a managed instance](../managed-instance/scripts/add-to-failover-group-powershell.md) | This PowerShell script creates two managed instances, adds them to a failover group, and then tests failover from the primary managed instance to the secondary managed instance. |

Learn more about [PowerShell cmdlets for Azure SQL Managed Instance](../managed-instance/api-references-create-manage-instance.md#powershell-create-and-configure-managed-instances).

## Related content

The examples listed on this page use [az.sql PowerShell cmdlets](/powershell/module/az.sql/) for creating and managing Azure SQL resources. Additional cmdlets for running queries and performing many database tasks are located in the [SqlServer PowerShell cmdlets](/powershell/module/sqlserver/). For more information, see [SQL Server PowerShell](/sql/powershell/sql-server-powershell/).
