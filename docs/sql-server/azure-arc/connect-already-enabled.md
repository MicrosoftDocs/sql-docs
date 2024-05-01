---
title: Connect SQL Server on a server already enabled by Azure Arc
description: Connect an instance of SQL Server to Azure Arc on a server that is already enabled by Azure Arc. Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, maghan
ms.date: 03/08/2024
ms.topic: conceptual
---

# Connect your SQL Server to Azure Arc on a server already enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [automatic](includes/if-manual.md)]

Before you proceed, complete the [Prerequisites](prerequisites.md).

This article explains how to connect your SQL Server instance to Azure Arc on an Arc-enabled server. If the physical or virtual server isn't connected to Azure yet, follow the steps in [Connect your SQL Server to Azure Arc](connect.md).

If the machine with SQL Server is already connected to Azure Arc, to connect the SQL Server instances, install *Azure extension for SQL Server*. The extension is in the extension tab of "Server -Azure Arc" resource as **Azure Extension for SQL Server**.

> [!IMPORTANT]  
> The Azure resource with type `SQL Server - Azure Arc` representing the SQL Server instance installed on the machine uses the same region and resource group as the Azure resources for Arc-enabled servers.

## [Azure portal](#tab/azure)

To install the Azure extension for SQL Server, use the following steps:

1. Open the **Azure Arc > Servers** resource.
1. Search for the connected server with the SQL Server instance that you want to connect to Azure.
1. Under **Extensions**, select **+ Add**.
1. Select `Azure extension for SQL Server` and select **Next**.
1. Specify the SQL Server edition and license type you are using on this machine. Some Arc-enabled SQL Server features are only available for SQL Server instances with Software Assurance (Paid) or with Azure pay-as-you-go. For more information, review [Manage SQL Server license type](manage-configuration.md).
1. Specify the SQL Server instance(s) you want to exclude from registering (if you have multiple instances to skip, separate them by spaces) and select **Review + Create**.
   :::image type="content" source="media/join/license-type-in-extension.png" alt-text="Screenshot for license type and exclude instances.":::
1. Select **Create**.

## [PowerShell](#tab/powershell)

To install *Azure extension for SQL Server*, run:

```powershell
$Settings = @{ SqlManagement = @{ IsEnabled = $true }; ExcludedSqlInstances = @(<Comma separated names of SQL Server instances, eg: "MSSQLSERVER01","MSSQLSERVER">); LicenseType="<License Type>"}

New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName {your resource group name} -MachineName {your machine name} -Location {azure region} -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"
```

## [Azure CLI](#tab/az)

To install *Azure extension for SQL Server* for Windows Operating System, run:

```azurecli
az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings "{\"SqlManagement\":{\"IsEnabled\":true}, \"LicenseType\":\"<License Type>\", \"ExcludedSqlInstances\":[]}"
```

To install *Azure extension for SQL Server* for Linux operating system, run:

```azurecli
settings="{\"SqlManagement\":{\"IsEnabled\":true},\"LicenseType\":\"<License Type>\"}"
az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "LinuxAgent.SqlServer" --resource-group "{your resource group name}" --type "LinuxAgent.SqlServer" --publisher "Microsoft.AzureData" --settings $settings
```

The possible licensing types that you can set are:

- `PAYG`
- `Paid`
- `LicenseOnly`

*Azure extension for SQL Server* for Linux is available for preview.

---

Once installed, the Azure extension for SQL Server recognizes all the installed SQL Server instances and connects them with Azure Arc.

The extension runs continuously to detect changes in the SQL Server configuration. For example, if a new SQL Server instance is installed on the machine, the extension automatically detects and registers it with Azure Arc. See [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions) for instructions on how to install and uninstall extensions to [Azure connected machine agent](/azure/azure-arc/servers/agent-overview) using the Azure portal, Azure PowerShell or Azure CLI.

## Validate your Arc-enabled SQL Server resources

Go to **Azure Arc > SQL Server** and open the newly registered Arc-enabled SQL Server resource to validate.

:::image type="content" source="media/join/validate-sql-server-azure-arc.png" alt-text="Screenshot of validating a connected SQL Server.":::

## Next steps

- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure best practices assessment on a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instance](assess.md)
