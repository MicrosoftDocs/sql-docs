---
title: Modify SQL Server license type 
description: Customers have ability to change the SQL Server license option through Arc enabled SQL Server.
author: anosov1960
ms.author: rajpo
ms.reviewer: mikeray, maghan
ms.date: 02/08/2023
ms.service: sql
ms.topic: conceptual
ms.custom: event-tier1-build-2022
---

# Modify license type

Host license type is a property of Azure extension for SQL Server resource. It applies to all instances installed on the server where the extension is running. For your convenience it is also included in the overview blade of Arc-enabled SQL Server as "Host License Type".

The following table provide features enabled depending on license type:

   :::image type="content" source="media/join/LT_features.png" alt-text="Screenshot for license type and features.":::

Learn more [SQL Server licensing and billing options](billing.md)


## Modify license type

You can change the license type of an installed Azure extension for SQL Server by using one of the following methods.

## [Azure portal](#tab/azure)

To install the Azure extension for SQL Server, use the following steps:

1. Open the **SQL Server - Azure Arc** resource
1. Search the connected SQL Server instance resource.
1. On overview page, navigate to **Host License Type** properties and click on license type value.
1. On **Sql Server management** page, select a specific licnese type applicable to SQL Server.  Please note that some Arc-enabled SQL Server features are only available for SQL Servers with Software Assurance (Paid) or with Azure pay-as-you-go. The above table provides a list of features available for different license types. [Learn more:](billing.md).
1. Click **Save** to save the new license type selected.

## [PowerShell](#tab/powershell)

The following command will set the license type to "PAYG":

```powershell
//Updated settings object
$Settings = @{ SqlManagement = @{ IsEnabled = $true }; ExcludedSqlInstances = @( "Foo","Bar">); LicenseType="PAYG"}

// Command stays the same as before, only settings is changed above:
New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName {your resource group name} -MachineName {your machine name} -Location {azure region} -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"

```

## [Azure CLI](#tab/az)

The following command will set the license type to "PAYG":

```azurecli
az connectedmachine extension update --machine-name "simple-vm" -g "arceeBilling" --name "WindowsAgent.SqlServer" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{"LicenseType":"PAYG", "SqlManagement": {"IsEnabled":true},\"ExcludedSqlInstances\":[]}'    
```

---

## Bulk modify license type

TBD -The process to execute bulk script prepared by Sasha goes here.

> [!IMPORTANT]  
>
> - The update command overwrites all settings. If your extension settings have a list of excluded SQL Server instances, make sure to specify the full exclusion list with the update command.
> - If you already have an older version of the Azure extension installed, make sure to upgrade it first, and then use one the modify methods to set the correct license type. For details, see [How to upgrade a mchine extenstion](/azure/azure-arc/servers/manage-automatic-vm-extension-upgrade) for details. 

## Next steps

- [Review SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Install SQL Server 2022 using the pay-as-you-go activation option](../../database-engine/install-windows/install-sql-server.md)
- [Frequently asked questions](faq.yml#billing)
