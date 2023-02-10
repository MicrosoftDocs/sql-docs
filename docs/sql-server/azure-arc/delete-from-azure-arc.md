---
title: Delete instance from or restore instances to Azure Arc #Required; page title is displayed in search results. Include the brand.
description: Delete Azure Arc-enabled SQL Server instance from Azure Arc, or restore or re-enable an instance for Azure Arc.
author: MikeRayMSFT #Required; your GitHub user alias, with correct capitalization.
ms.author: mikeray #Required; microsoft alias of author; optional team alias.
ms.service: sql #Required; service per approved list. slug assigned by ACOM.
ms.topic: how-to #Required; leave this attribute/value as-is.
ms.date: 02/01/2022 #Required; mm/dd/yyyy format.
ms.custom: template-how-to-pattern #Required; leave this attribute/value as-is.
---

# Delete your Arc-enabled SQL Server resource

This article describes how you can do one of the following tasks:

* Delete an Azure Arc-enabled SQL Server instance from Azure.
* Restore (or re-enable) an Azure Arc-enabled SQL Server instance after it has been deleted.

## Delete resources from the portal

To delete your Arc-enabled SQL Server resource, go to **Azure Arc** > **SQL Server**, open the Arc-enabled SQL Server resource for that instance, and select the **Delete** button.

> [!IMPORTANT]  
> Because multiple SQL Server instances could be installed on the same machine, the *Delete* button doesn't uninstall the Azure extension for SQL Server on that machine. To uninstall it, follow the [uninstall extension](/azure/azure-arc/servers/manage-vm-extensions-portal#uninstall-extension) steps.

## Restore a deleted Arc-enabled SQL Server resource

If you accidentally deleted your Arc-enabled SQL Server resource, you can restore it with the following steps.

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