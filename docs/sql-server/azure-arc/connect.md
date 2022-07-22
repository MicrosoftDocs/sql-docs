---
title: Connect to Azure Arc
description: Connect an instance of SQL Server to Azure Arc
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 07/25/2022
ms.prod: sql
ms.topic: conceptual
ms.custom: event-tier1-build-2022
---
# Connect your SQL Server to Azure Arc

Beginning in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can deploy an instance of SQL Server from the command prompt that is connected to Azure Arc. See [Deploy - connected to Azure Arc](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#deploy---connected-to-azure-arc).

You can connect your SQL Server instance to Azure Arc by following these steps.

## Prerequisites

- Your machine has at least one instance of SQL Server installed
- The **Microsoft.AzureArcData** and **Microsoft.HybridCompute** resource providers have been registered.

To register the resource providers, use one of the methods below:

# [Azure portal](#tab/azure)

1. Select **Subscriptions**
2. Choose your subscription
3. Under **Settings**, select **Resource providers**
4. Search for `Microsoft.AzureArcData` and `Microsoft.HybridCompute` and select **Register**

# [PowerShell](#tab/powershell)

Run:

```powershell
Register-AzResourceProvider -ProviderNamespace Microsoft.AzureArcData
```

# [Azure CLI](#tab/az)

Run:

```azurecli
az provider register --namespace 'Microsoft.AzureArcData'
```

---

## Initiate the connection from Azure

If the machine with SQL Server is already connected to Azure Arc, you can register the SQL Server instances on that machine by installing the SQL Server extension (*WindowsAgent.SqlServer*). Once installed, the SQL Server extension will recognize all the installed SQL Server instances and register them with Azure Arc. The extension will run continuously to detect changes of the SQL Server configuration. For example, if a new SQL Server instance is installed on the machine, the instance will be automatically registered with Azure. See [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions) for instructions on how to install and uninstall extensions using the Azure portal, Azure PowerShell or Azure CLI.

> [!IMPORTANT]
>
> 1. The Managed System Identity for the corresponding **Server - Azure Arc** must have the *Azure Connected SQL Server Onboarding* role at resource group level.
> 2. The **SQL Server - Azure Arc** resource for each SQL Server instance installed on the machine will be created in the same region and the resource group as the corresponding **Server - Azure Arc** resource.

# [Azure portal](#tab/azure)

To assign the *Azure Connected SQL Server Onboarding* role to Arc machine managed identity, use the following steps:

1. Select the resource group that contains the **Server - Azure Arc** resource
1. Select **Access control (IAM)** on the left side of the resource group page
1. Select **+ Add** and select **Add role assignment**
   - For **Role**, select `Azure Connected SQL Server Onboarding`
   - For **Assign access to**, select `User, group or service principal`
   - For **Select**, search for your **Server - Azure Arc** name and select it.
1. Select **Save**.

To install the SQL Server extension, use the following steps:

1. Open the **Server - Azure Arc** resource.
1. Under **Extensions**, select **+ Add**
1. Select `WindowsAgent.SqlServer` from the list and select **Create**.

# [PowerShell](#tab/powershell)

To assign *Azure Connected SQL Server Onboarding* role to the machine's managed identity, run:

```powershell
$spID = (Get-AzADServicePrincipal -DisplayName $arcMachineName).Id
New-AzRoleAssignment -ObjectId $spID RoleDefinitionName "Azure Connected SQL Server Onboarding" -ResourceGroupName {resource group name}
```

To install the SQL Server extension, run:

```powershell
$Settings = @{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}
New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName {your resource group name} -MachineName {your machine name} -Location {azure region} -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"
```

# [Azure CLI](#tab/az)

To assign the *Azure Connected SQL Server Onboarding* role to Arc machine managed identity, run:

```azurecli
spID=$(az resource list -n <ArcMachineName> --query [*].identity.principalId --out tsv)
az role assignment create --assignee $spID --role 'Azure Connected SQL Server Onboarding' --scope /subscriptions/<mySubscriptionID>/resourceGroups/<myResourceGroup>
```

To install the SQL Server extension, run:

```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}'
```

---

> [!NOTE]
> The specified resource group must match the resource group of the corresponding **Server - Azure Arc** resource. Otherwise, the command will fail.

## Initiate the connection from the target machine

If you want to customize the process of connecting the SQL Server instance to Azure Arc, you can initiate the connection from the target machine using the onboarding script.

### Generate an onboarding script for SQL Server

If the machine with SQL Server is already connected to Azure Arc, you can register the SQL Server instances on that machine by installing the SQL Server extension (*WindowsAgent.SqlServer*). Once installed, the SQL Server extension will recognize all the installed SQL Server instances and register them with Azure Arc. The extension will run continuously to detect changes of the SQL Server configuration. For example, if a new SQL Server instance is installed on the machine, the extension automatically registers it with Azure. See [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions) for instructions how to install and uninstall extensions using  Azure portal, Azure PowerShell, or Azure CLI.

1. Search for **SQL Server - Azure Arc** resource type and add a new one through the creation pane.

   :::image type="content" source="media/join/start-creation-of-sql-server-azure-arc-resource.png" alt-text="Screenshot showing how to start creation.":::

2. Review the prerequisites and go to the **Server details** tab.

3. Select the subscription, resource group, Azure region, and the host operating system. If necessary, also specify the proxy that your network uses to connect to Internet.

   > [!IMPORTANT]
   > If the machine hosting the SQL Server instance is already [connected to Azure Arc](/azure/azure-arc/servers/onboard-portal), make sure to select the same resource group that contains the corresponding **Server - Azure Arc** resource.

   :::image type="content" source="media/join/server-details-sql-server-azure-arc.png" alt-text="Screenshot showing server details.":::

4. Go to the **Run script** tab and download the onboarding script. The portal generates the script for the hosting OS you specified.

   :::image type="content" source="media/join/download-script-sql-server-azure-arc.png" alt-text="Screenshot showing the download script.":::

### Connect SQL Server instances to Azure Arc

In this step, you'll take the script you downloaded from Azure portal and execute it on the target machine. The script installs the SQL Server extension. If the machine itself doesn't have the guest configuration agent installed, the script first installs it then installs the SQL Server extension. The guest agent and the SQL extension will in turn register the connected server and the SQL Server instances on it as the **Server - Azure Arc** and **SQL Server - Azure Arc** resources respectively.

> [!IMPORTANT]
> Make sure to execute the script using an account that meets the minimum permission requirements described in [prerequisites](overview.md#prerequisites).

# [Windows](#tab/windows)

1. Launch an admin instance of **powershell.exe** and sign in your PowerShell module with your Azure credentials. Follow the [sign in instructions](/powershell/azure/install-az-ps#sign-in).

2. Execute the downloaded script

   ```powershell
   & '.\RegisterSqlServerArc.ps1'
   ```

   If you haven't previously installed the [Az PowerShell module](/powershell/azure/new-azureps-module-az) and see issues the first time you run it, follow the instructions in the script and run it again.

# [Linux](#tab/linux)

1. Use Azure CLI to sign in with your Azure credentials. Follow the [sign in instructions](/cli/azure/authenticate-azure-cli)

2. Grant the execution permission to the downloaded script and execute it.

   ```console
   sudo chmod +x ./RegisterSqlServerArc.sh
   ./RegisterSqlServerArc.sh
   ```

---

## Validate the SQL Server - Azure Arc resources

Go [Azure portal](https://ms.portal.azure.com/#home) and open the newly registered **SQL Server - Azure Arc** resource to validate.

:::image type="content" source="media/join/validate-sql-server-azure-arc.png" alt-text="Screenshot showing how to validate connected SQL Server.":::

## Disconnect your SQL Server instance

To disconnect your SQL Server instance from Azure Arc, go to Azure portal, open the **SQL Server - Azure Arc** resource for that instance, and select the **Unregister** button. It will delete this resource and instruct the SQL Server extension on the machine to stop monitoring this SQL Server instance.

:::image type="content" source="media/join/unregister-sql-server-azure-arc.png" alt-text="Screenshot showing how to unregister SQL Server.":::

> [!IMPORTANT]
> Because there could be multiple SQL Server instances installed on the same machine, the *Unregister* button will not uninstall the SQL Server extension. To uninstall it, follow the [uninstall extension](/azure/azure-arc/servers/manage-vm-extensions-portal#uninstall-extension) steps.

## Restore a deleted SQL Server - Azure Arc resource

If you disconnected your SQL Server instance by mistake, you can restore its **SQL Server - Azure Arc** resource with the following steps.

1. If you also uninstalled the SQL Server extension by mistake, reinstall it.

   ```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}'
   ```

   The location property must match the location of the **Server - Azure Arc** resource for the server specified by the     `--machine-name` parameter.

2. Check to make sure your instance is in the exclusion list (see the value of the *excludedSqlInstances* property).

   ```azurecli
   az connectedmachine extension show --machine-name "{your machine name}" --resource-group "{your resource group name}" -n WindowsAgent.SqlServer
   ```

3. Make sure to remove your instance from the exclusion list and update the extension settings.

   ```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[\"{named instance 1}\",\"{named instance 3}}\"]}'
   ```

The instance will be restored after the next sync with the agent. For information on how to manage vm extensions using Portal or PowerShell, see [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions).

## Next steps

- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure on-demand SQL assessment for your SQL Server instance](assess.md)
