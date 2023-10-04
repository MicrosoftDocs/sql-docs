---
title: "Disconnect your SQL Server from Azure Arc"
description: Steps to disconnect and unregister your SQL Server instances from Azure Arc.
author: MikeRayMSFT
ms.author: mikeray
ms.date: 04/09/2023
ms.topic: how-to
ms.custom: template-how-to-pattern
---

# Disconnect your SQL Server instances from Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article describes how you can disconnect the Arc-enabled SQL Server instances from Azure Arc using Azure portal or in the command shell.

## Prerequisites

* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) for the subscriptions and resource groups in which the Arc-enabled SQL Server instances have been created. 

> [!IMPORTANT]
   > You don't need access to the hosting machine to disconnect from Azure Arc.

## Disconnect and unregister individual SQL Server instances from Azure Arc

### [Azure portal](#tab/azure)

To uninstall Azure extension for SQL Server:

1. Go to **Azure Arc** portal
1. Under **Servers** select the specific machine hosting SQL Server instance(s) you wish to disconnect from Azure Arc
1. Under **Extensions**, select the extension you want to uninstall (*`WindowsAgent.SqlServer`* if it's a Windows machine, or *`LinuxAgent.SqlServer`* if it's a Linux  machine)
1. Click on the **Uninstall** tab
1. Confirm that you want to uninstall the extension when prompted

To remove the SQL Server - Azure Arc resource(s):

1. Go to **Azure Arc** portal
1. Under **SQL Servers** select the specific SQL Server instance(s) you wish to remove
1. Click on the **Delete** tab
1. Confirm that you want to delete the resource(s) when prompted

### [PowerShell](#tab/powershell)

To uninstall Azure extension for SQL Server, run:

```powershell
Remove-AzConnectedMachineExtension -MachineName "{your machine name}" -ResourceGroup "{your resource group name}" -Name "{extension name}" -NoWait 
```

For Windows machines, the extension name is `WindowsAgent.SqlServer`. For Linux machines, the extension name is `LinuxAgent.SqlServer`.

To remove the SQL Server - Azure Arc resource(s), run:

```powershell
remove-azresource -ResourceGroup "{your resource group name}" -ResourceType Microsoft.AzureArcData/SqlServerInstances -Name "{full SQL instance name}" -Force 
```
If your instance has dependent Azure resources, such as databases, this command may take a long time to complete. You can add `-AsJob` parameter to return immediately and execute the command as a background job. 

> [!TIP]  
> Run the script from Azure Cloud shell as it has the required Azure PowerShell modules pre-installed and you will be automatically authenticated. For details, see [Running the script using Cloud Shell](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/uninstall-azure-extension-for-sql-server#running-the-script-using-cloud-shell).

### [Azure CLI](#tab/az)

To uninstall Azure extension for SQL Server, run:

```azurecli
az connectedmachine extension delete --machine-name "{your machine name}" --resource-group "{your resource group name}" --name "{OS}Agent.SqlServer" --publisher "Microsoft.AzureData" 
```

To remove the SQL Server - Azure Arc resource(s), run:

```azurecli
az resource delete --resource-group "{your resource group name}" --resource-type Microsoft.AzureArcData/SqlServerInstances --name "{full SQL instance name}"
```

 If your instance (SQL Server - Azure Arc resource) has dependent Azure resources, such as databases (SQL Server database - Azure Arc resource), this command may take a long time to complete. You can add `--No-Wait` parameter to return immediately and execute the command as a background job.

---

To disconnect all the Arc-enabled SQL Server instances in a larger scope, such as a resource group, subscription, or multiple subscriptions, with a single command, use the [Uninstall Azure Extension for SQL Server](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/uninstall-azure-extension-for-sql-server) script. It's published as an open source SQL Server sample and includes the step-by-step instructions.
