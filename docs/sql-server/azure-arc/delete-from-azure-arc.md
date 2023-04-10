---
title: "Delete instance from or restore instances to Azure Arc"
description: Delete Azure Arc-enabled SQL Server instance from Azure Arc, or restore or re-enable an instance for Azure Arc.
author: MikeRayMSFT
ms.author: mikeray
ms.date: 04/09/2023
ms.service: sql
ms.topic: how-to
ms.custom: template-how-to-pattern
---

# Disconnect and unregister your SQL Server instances from Azure Arc

This article describes how you can disconnect the Arc-enabled SQL Server instances from Azure Arc using Azure Portal or in the command shell. 

> [!IMPORTANT]
   > You don't need access to the hosting machine to disconnect from Azure Arc.

## Prerequisists

* You're in a [Contributor role](/azure/role-based-access-control/built-in-roles#contributor) for the subscriptions and resource groups in which the Arc-enabled SQL Server instances have been created. 

## Disconnect individual SQL Server instances from Azure Arc

### [Azure portal](#tab/azure)

To uninstall Azure extension for SQL Server:

1. Go to **Azure Arc** portal
1. Under **Servers** select the specific machine hosting SQL Server instance(s) you wish to disconnect from Azure Arc
1. Under **Extensions**, select the exension you want to uninstall (*WindowsAgent.SqlServer* if it is a Windows machine, or *LinuxAgent.SqlServer* if it is a Linux  machine)
1. Click on the **Uninstall** tab
1. Confirm that you want to install the extension when prompted

To remnove the SQL Server - Azure Arc resource(s):

1. Go to **Azure Arc** portal
1. Under **SQL Servers** select the specific SQL Server instance(s) you wish to remove
1. Click on the **Delete** tab
1. Confirm that you want to detele the resource(s) when prompted

### [PowerShell](#tab/powershell)

To uninstall Azure extension for SQL Server, run:

```powershell
Remove-AzConnectedMachineExtension -MachineName "{your machine name}" -ResourceGroup "{your resource group name}" -Name "{extension name}" -NoWait 
```

For Windows machines, the extesion name is "WindowsAgent.SqlServer". For Linux machines the extension name is "LinuxAgent.SqlServer".

To remnove the SQL Server - Azure Arc resource(s), run:

```powershell
remove-azresource -ResourceGroup "{your resource group name}" -ResourceType Microsoft.AzureArcData/SqlServerInstances -Name "{full SQL instance name}" -Force 
```
If your instance have dependent Azure resources, such as databases, this command may take a long time to complete. You can add `-AsJob` parameter to return immediately and execute the command as a background job. 

### [Azure CLI](#tab/az)

To uninstall Azure extension for SQL Server, run:

```azurecli
az connectedmachine extension delete --machine-name "{your machine name}" --resource-group "{your resource group name}" --name "{OS}Agent.SqlServer" --publisher "Microsoft.AzureData" 
```

To remnove the SQL Server - Azure Arc resource(s), run:

```azurecli
az resource delete --resource-group "{your resource group name}" --resource-type Microsoft.AzureArcData/SqlServerInstances --name "{full SQL instance name}"
```

 If your instance have dependent Azure resources, such as databases, this command may take a long time to complete. You can add `--No-Wait` parameter to return immediately and execute the command as a background job. 

---

To disconnect all the Arc-enabled SQL Server instances in a larger scope, such as a resource group, subscription, or multiple subscriptions, with a single command, use [Uninstall Azure EXtension for SQL Server](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/uninstall-azure-extension-for-sql-server). It is published as an open source SQL Server sample and includes the step-by-step instructions.

> [!TIP]  
> Run the script from Azure Cloud shell as it has the required Azure PowerShell modules pre-installed and you will be automatically authenticated. For details, see [Running the script using Cloud Shell](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/uninstall-azure-extension-for-sql-server#running-the-script-using-cloud-shell).


## Restore a deleted Arc-enabled SQL Server resource

If you accidentally deleted your Arc-enabled SQL Server resource, you can restore it with the following steps.

1. If you also uninstalled the SQL Server extension by mistake, reinstall it. Select the correct version for your OS.

   ```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "{OS}Agent.SqlServer" --publisher "Microsoft.AzureData" --settings '{"SqlManagement":{"IsEnabled":true},  "excludedSqlInstances":[],"LicenseType":"LicenseOnly"}'
   ```

   > [!IMPORTANT]  
   > The location property must match the location of the Arc-enabled SQL Server resource for the server specified by the `--machine-name` parameter.

1. Check to make sure your instance is in the exclusion list (see the value of the *excludedSqlInstances* property).

   ```azurecli
   az connectedmachine extension show --machine-name "{your machine name}" --resource-group "{your resource group name}" -n WindowsAgent.SqlServer
   ```

1. Make sure to remove your instance from the exclusion list and update the extension settings.

   ```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{"SqlManagement":{"IsEnabled":true},  "excludedSqlInstances":["{named instance 1}","{named instance 3}}"],"LicenseType":"LicenseOnly"}'
   ```

The instance is restored after the next sync with the agent. For information on managing vm extensions using Portal or PowerShell, see [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions).
