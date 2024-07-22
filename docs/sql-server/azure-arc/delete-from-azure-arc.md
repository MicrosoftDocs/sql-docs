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

Your Azure account is in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) for the instance subscription and resource group.

> [!IMPORTANT]
> You don't need access to the hosting machine to disconnect from Azure Arc.

## Opt out of automatic installation

Before you uninstall the Azure extension for SQL Server, opt out of the automatic installation of Azure extension for SQL Server, add the following tag and value to the Arc Server resource.

| Tag | Value |
| --- | ----- |
| `ArcSQLServerExtensionDeployment` | `Disabled` |

Alternatively, you can limit which extensions can be installed on your server. You can configure lists of the extensions you wish to allow and block on the server. To learn more, see [Extension allowlists and blocklists](/azure/azure-arc/servers/security-overview#extension-allowlists-and-blocklists).

## Uninstall Azure extension for SQL Server

### [Azure portal](#tab/azure)

To uninstall Azure extension for SQL Server:

1. Go to **Azure Arc** portal
1. Under **Machines** select the specific server that hosts the SQL Server instance
1. Under **Extensions**, select the extension you want to uninstall (*`WindowsAgent.SqlServer`* if it's a Windows machine, or *`LinuxAgent.SqlServer`* if it's a Linux  machine)
1. Click on the **Uninstall** tab
1. Confirm that you want to uninstall the extension when prompted

To remove the SQL Server - Azure Arc resources:

1. Go to **Azure Arc** portal
1. Under **SQL Servers instances** select the specific SQL Server instances you wish to remove
1. Click on the **Delete** tab
1. Confirm that you want to delete the resource when prompted

### [PowerShell](#tab/powershell)

To uninstall the Azure extension for SQL Server, run:

```powershell
Remove-AzConnectedMachineExtension -MachineName "{your machine name}" -ResourceGroup "{your resource group name}" -Name "{extension name}" -NoWait 
```

For Windows machines, the extension name is `WindowsAgent.SqlServer`. For Linux machines, the extension name is `LinuxAgent.SqlServer`.

To remove the SQL Server - Azure Arc resource, run:

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

To remove the SQL Server - Azure Arc resource, run:

```azurecli
az resource delete --resource-group "{your resource group name}" --resource-type Microsoft.AzureArcData/SqlServerInstances --name "{full SQL instance name}"
```

 If your instance (SQL Server - Azure Arc resource) has dependent Azure resources, such as databases (SQL Server database - Azure Arc resource), this command may take a long time to complete. You can add `--No-Wait` parameter to return immediately and execute the command as a background job.

---

To disconnect all the Arc-enabled SQL Server instances in a larger scope, such as a resource group, subscription, or multiple subscriptions, with a single command, use the [script to uninstall Azure Extension for SQL Server](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/uninstall-azure-extension-for-sql-server). The script is as an open source SQL Server sample and includes the step-by-step instructions.

## Residual files and accounts

This section explains files and database objects left after you uninstall.

### Files

Uninstalling deletes the binary files.

Extension logs and other data may not be deleted.

Disabling doesn't delete any binary files or folders.

### Tables

Tables created by the agent stay after the extension is uninstalled.

### Accounts

If you didn't install the extension in least privilege mode, the agent uses **NTAUTHORITY\SYSTEM** account.

Disabling or deleting the extension doesn't remove **NTAUTHORITY\SYSTEM** login from any databases because other applications may require this login. You have to manually remove the role from each user database.

**NTAUTHORITY\SYSTEM** account doesn't apply to installations with least privilege. For details about least privilege mode, see [Operate SQL Server enabled by Azure Arc with least privilege](configure-least-privilege.md).
