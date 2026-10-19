---
title: Disconnect SQL Server Instances from Azure Arc
description: Get steps to disconnect and unregister your SQL Server instances from Azure Arc.
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 05/08/2026
ms.topic: how-to
ms.custom:
  - template-how-to-pattern
---

# Disconnect SQL Server instances from Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article describes how to disconnect SQL Server instances from Azure Arc by using the Azure portal or a command shell. It applies to SQL Server instances enabled by Azure Arc. To stop managing a SQL Server instance with Azure Arc, remove the SQL Server extension. After you complete these steps, SQL Server - Azure Arc resources and associated components are fully removed from your system and Azure.

## Prerequisites

Your Azure account must have a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) for the instance subscription and resource group.

> [!NOTE]  
> You don't need access to the hosting machine to disconnect from Azure Arc.

## Opt out of automatic installation

Before you uninstall Azure Extension for SQL Server, opt out of automatic installation of the extension by adding the following tag and value to the Azure Arc-enabled SQL Server resource:

| Tag | Value |
| --- | --- |
| `ArcSQLServerExtensionDeployment` | `Disabled` |

Alternatively, you can limit which extensions can be installed on your server. You can configure lists of the extensions that you want to allow and block on the server. To learn more, see [Allow lists and block lists](/azure/azure-arc/servers/security-extensions#allowlists-and-blocklists).

## Uninstall Azure Extension for SQL Server

### [Azure portal](#tab/azure)

To uninstall Azure Extension for SQL Server:

1. In the Azure portal, go to **Azure Arc**.
1. Under **Machines**, select the specific server that hosts the SQL Server instance.
1. Under **Extensions**, select the extension that you want to uninstall (`WindowsAgent.SqlServer` if it's a Windows machine, or `LinuxAgent.SqlServer` if it's a Linux machine).
1. Select **Uninstall**.
1. When prompted, confirm that you want to uninstall the extension.

To remove the *SQL Server - Azure Arc* resource:

1. In the Azure portal, go to **Azure Arc**.
1. Under **SQL Server instances**, select the specific SQL Server instance that you want to remove.
1. Select **Delete**.
1. When prompted, confirm that you want to delete the resource.

To remove dependent resources from a resource group:

1. In the Azure portal, go to the resource group where the SQL Server resource was.
1. Under **Overview**, select the specific SQL Server resource and resources that you want to remove.
1. Select **Delete**.
1. When prompted, confirm that you want to delete the resources.

### [PowerShell](#tab/powershell)

To uninstall Azure Extension for SQL Server, run:

```powershell
Remove-AzConnectedMachineExtension -MachineName "{your machine name}" -ResourceGroup "{your resource group name}" -Name "{extension name}" -NoWait
```

For Windows machines, use the extension name `WindowsAgent.SqlServer`. For Linux machines, use the extension name `LinuxAgent.SqlServer`.

To remove the *SQL Server - Azure Arc* resource, run:

```powershell
remove-azresource -ResourceGroup "{your resource group name}" -ResourceType Microsoft.AzureArcData/SqlServerInstances -Name "{full SQL instance name}" -Force
```

If your instance (*SQL Server - Azure Arc* resource) has dependent Azure resources such as databases (*SQL Server database - Azure Arc* resource), this command might take a long time to finish. You can add an `-AsJob` parameter to return immediately and run the command as a background job.

> [!TIP]  
> Run the script from Azure Cloud Shell. It has the required Azure PowerShell modules preinstalled, and you're authenticated automatically. For details, see [Running the script using Cloud Shell](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/uninstall-azure-extension-for-sql-server#running-the-script-using-cloud-shell).

### [Azure CLI](#tab/az)

To uninstall Azure Extension for SQL Server, run:

```azurecli
az connectedmachine extension delete --machine-name "{your machine name}" --resource-group "{your resource group name}" --name "{OS}Agent.SqlServer" --publisher "Microsoft.AzureData"
```

To remove the *SQL Server - Azure Arc* resource, run:

```azurecli
az resource delete --resource-group "{your resource group name}" --resource-type Microsoft.AzureArcData/SqlServerInstances --name "{full SQL instance name}"
```

If your instance (*SQL Server - Azure Arc* resource) has dependent Azure resources, such as SQL Server databases, this command might take a long time to finish. You can add the `--No-Wait` parameter to return immediately and run the command as a background job.

---

To disconnect all the Azure Arc-enabled SQL Server instances in a larger scope (such as a resource group, a subscription, or multiple subscriptions) with a single command, use the [script to uninstall Azure Extension for SQL Server](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/uninstall-azure-extension-for-sql-server). The script is an open-source SQL Server sample and includes step-by-step instructions.

[!INCLUDE [resource-caching-after-deletion](includes/resource-caching-after-deletion.md)]

## Residual files and accounts

After you uninstall Azure Extension for SQL Server, some files and database objects remain.

### Files

Uninstalling the extension deletes the binary files, but extension logs and other data might not be deleted.

Disabling the extension doesn't delete any binary files or folders.

### Tables

Tables created by the agent remain after you uninstall the extension.

### Accounts

If you don't install the extension in least privilege mode, the agent uses the `NT AUTHORITY\SYSTEM` account.

Disabling or deleting the extension doesn't remove the `NT AUTHORITY\SYSTEM` login from any databases because other applications might require this login. You must manually remove the login from each user database.

An `NT AUTHORITY\SYSTEM` account doesn't apply to installations that use least privilege.

For more information about least privilege mode, see [Operate SQL Server enabled by Azure Arc with least privilege](configure-least-privilege.md).
