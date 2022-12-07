---
title: Connect to Azure Arc
description: Connect an instance of SQL Server to Azure Arc. Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, maghan
ms.date: 12/06/2022
ms.service: sql
ms.topic: conceptual
ms.custom: event-tier1-build-2022
---

# Connect your SQL Server to Azure Arc

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] you connect a new SQL Server instance to Azure Arc when you're installing it on Windows Operating System. See [Install SQL Server 2022](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#install-sql-server-2022).

You can connect your existing SQL Server instance to Azure Arc by following these steps.

## Prerequisites

- You need an Azure account with an active subscription. [Create one for free](https://azure.microsoft.com/free/).
- The **Microsoft.AzureArcData** and **Microsoft.HybridCompute** resource providers have been registered.
- The user onboarding Arc-enabled SQL Server resources must have the following permissions:

   Microsoft.AzureArcData/sqlServerInstances/read

   Microsoft.AzureArcData/sqlServerInstances/write

> [!NOTE]  
> SQL Server on Azure Arc-enabled servers does not support SQL Server Failover Cluster Instances.

To register the resource providers, use one of the methods below:

## [Azure portal](#tab/azure)

1. Select **Subscriptions**.
1. Choose your subscription.
1. Under **Settings**, select **Resource providers**.
1. Search for `Microsoft.AzureArcData` and `Microsoft.HybridCompute` and select **Register**.

## [PowerShell](#tab/powershell)

Run:

```powershell
Register-AzResourceProvider -ProviderNamespace Microsoft.AzureArcData
```

## [Azure CLI](#tab/az)

Run:

```azurecli
az provider register --namespace 'Microsoft.AzureArcData'
```

---

## When the machine is already connected to an Arc-enabled Server

If the machine with SQL Server is already connected to Azure Arc, you can connect the SQL Server instances by installing *Azure extension for SQL Server*. The SQL Server extension for Azure Arc Server can be found in the extension manager as **SQL Server Extension - Azure Arc**. Once installed, the Azure extension for SQL Server will recognize all the installed SQL Server instances and connect them with Azure Arc. The extension will run continuously to detect changes in the SQL Server configuration. For example, if a new SQL Server instance is installed on the machine, it will be automatically registered with Azure Arc. See [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions) for instructions on how to install and uninstall extensions to [Azure connected machine agent](/azure/azure-arc/servers/agent-overview) using the Azure portal, Azure PowerShell or Azure CLI.

> [!IMPORTANT]  
>
> - The Managed System Identity used by the Azure connected machine agent must have the *Azure Connected SQL Server Onboarding* role at the resource group level.
> - The Azure resource with type `SQL Server - Azure Arc` representing the SQL Server instance installed on the machine uses the same region and resource group as the Azure resources for Arc-enabled servers.

## [Azure portal](#tab/azure)

To install the Azure extension for SQL Server, use the following steps:

1. Open the **Azure Arc > Servers** resource.
1. Search for the connected server with the SQL Server instance that you want to connect to Azure.
1. Under **Extensions**, select **+ Add**.
1. Select `Azure extension for SQL Server` and select **Next**.
1. Specify the SQL Server instance(s) you want to exclude from registering (if you have multiple instances installed on the server) and select **Review + Create**.
1. Select **Create**.

## [PowerShell](#tab/powershell)

To install *Azure extension for SQL Server*, run:

```powershell
$Settings = @{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}
New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName {your resource group name} -MachineName {your machine name} -Location {azure region} -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"
```

## [Azure CLI](#tab/az)

To install *Azure extension for SQL Server* for Windows Operating System, run:

```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resomsiurce group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}'
```

To install *Azure extension for SQL Server* for Linux operating system, run:

```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "LinuxAgent.SqlServer" --resource-group "{your resource group name}" --type "LinuxAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}'
```

*Azure extension for SQL Server* for Linux is available for preview.

---

> [!NOTE]  
> The specified resource group must match the resource group where the corresponding connected server is registered. Otherwise, the command will fail.
 an

## When the machine isn't connected to an Arc-enabled Server

If the server that runs your SQL Server instance isn't yet connected to Azure, you can initiate the connection from the target machine using the onboarding script. This script will connect the server to Azure and install the Azure extension for SQL Server.

### Generate an onboarding script for SQL Server

1. Go to **Azure Arc > SQL Server** and select **+ Add**

   :::image type="content" source="media/join/start-creation-of-sql-server-azure-arc-resource.png" alt-text="Screenshot of the start creation.":::

1. Select **Connect SQL Server to Azure Arc**

1. Review the prerequisites and select **Next: Server details**

1. Select the subscription, resource group, Azure region, and host operating system. If necessary, specify the proxy your network uses to connect to the Internet.

   > [!IMPORTANT]  
   > If the machine hosting the SQL Server instance is already [connected to Azure Arc](/azure/azure-arc/servers/onboard-portal), make sure to select the same resource group that contains the corresponding **Server - Azure Arc** resource.

   :::image type="content" source="media/join/server-details-sql-server-azure-arc.png" alt-text="Screenshot of server details.":::

1. Select **Tags** to optionally add tags to the resource for your SQL Server instance.

1. Select **Run script** to generate the onboarding script.
Screenshot of

   :::image type="content" source="media/join/download-script-sql-server-azure-arc.png" alt-text="Screenshot of a download script.":::

1. Select **Download** to download the script to your machine.

### Connect SQL Server instances to Azure Arc

In this step, you'll take the script you downloaded from the Azure portal and execute it on the target machine. The script installs Azure extension for SQL Server. If the machine itself doesn't have the Azure connected machine agent installed, the script will install it first, then install the Azure extension for SQL Server. Azure connected machine agent will register the connected server as an Azure resource of type `Server - Azure Arc`, and Azure extension for SQL Server will connect the SQL Server instances as Azure resources of type `SQL Server - Azure Arc`.

> [!IMPORTANT]  
> Make sure to execute the script using an account that meets the minimum permission requirements described in [prerequisites](overview.md#prerequisites).

## [Windows](#tab/windows)

1. Launch an admin instance of **powershell.exe** and sign in to your PowerShell module with your Azure credentials. Follow the [sign-in instructions](/powershell/azure/install-az-ps#sign-in).

1. Execute the downloaded script.

   ```powershell
   & '.\RegisterSqlServerArc.ps1'
   ```

   > [!NOTE]  
   > If you have yet to previously install the [Az PowerShell module](/powershell/azure/new-azureps-module-az) and see issues the first time you run it, follow the instructions in the script and rerun it.

## [Linux](#tab/linux)

1. Use Azure CLI to sign in with your Azure credentials. Follow the [sign in instructions](/cli/azure/authenticate-azure-cli)

1. Grant the execution permission to the downloaded script and execute it.

   ```console
   sudo chmod +x ./RegisterSqlServerArc.sh
   ./RegisterSqlServerArc.sh
   ```

---

## Deploy SQL Server extension from AzureExtensionForSQLServer.msi

Alternatively, you can also onboard your SQL Servers to Azure Arc by directly using  AzureExtensionForSQLServer.msi. This method helps you integrate onboarding SQL Servers to Arc with any existing deployment automation tools and services.

1. Download AzureExtensionForSQLServer.msi from the [link](https://aka.ms/AzureExtensionForSQLServer).
1. Double-select on AzureExtensionForSQLServer.msi.  This installs the necessary packages for onboarding SQL Servers to Azure Arc.
1. Open PowerShell console in admin mode and execute the following commands.

   If you use Azure Active Directory service principal to authenticate, execute the command below on the target SQL Server.

   ```powershell
   '& "$env:ProgramW6432\AzureExtensionForSQLServer\AzureExtensionForSQLServer.exe" --subId <subscriptionid> --resourceGroup <resourceGroupName> --location <AzureLocation> --tenantid <TenantId> --service-principal-app-id <servicePrincipalAppId> --service-principal-secret <servicePrincipalSecret> --proxy <proxy> --tags ""'
   ```

   Otherwise, execute the command below on the target SQL Server.

   ```powershell
   '& "$env:ProgramW6432\AzureExtensionForSQLServer\AzureExtensionForSQLServer.exe" --subId  <subscriptionid>--resourceGroup <resourceGroupName> --location <AzureLocation> --tenantid <TenantId>  --proxy  <proxy> --tags ""'
   ```

 > [!IMPORTANT]  
 > Microsoft Azure Arc-enabled SQL Server is licensed to you as part of your or your company's subscription license for Microsoft Azure Services. You may only use the software with Microsoft Azure Services and are subject to the terms and conditions of the agreement under which you obtained Microsoft Azure Services. You may not use the software if you do not have an active subscription license for Microsoft Azure Services.  
 > Microsoft Azure Legal Information: [Microsoft Azure Legal Information](https://azure.microsoft.com/support/legal/) and [Microsoft Privacy Statement](https://azure.microsoft.com/support/legal/)

## Validate your Arc-enabled SQL Server resources

Go to **Azure Arc > SQL Server** and open the newly registered Arc-enabled SQL Server resource to validate.

   :::image type="content" source="media/join/validate-sql-server-azure-arc.png" alt-text="Screenshot of validating a connected SQL server.":::

## Delete your Arc-enabled SQL Server resource

To delete your Arc-enabled SQL Server resource, go to **Azure Arc > SQL Server**, open the Arc-enabled SQL Server resource for that instance, and select the **Delete** button.

> [!IMPORTANT]  
> Because multiple SQL Server instances could be installed on the same machine, the *Delete* button doesn't uninstall the Azure extension for SQL Server on that machine. To uninstall it, follow the [uninstall extension](/azure/azure-arc/servers/manage-vm-extensions-portal#uninstall-extension) steps.

## Restore a deleted Arc-enabled SQL Server resource

If you accidentally deleted your Arc-enabled SQL Server resource, you can restore it with the following steps.

1. If you also uninstalled the SQL Server extension by mistake, reinstall it. Select the correct version for your OS.

    ```azurecli
       az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "{OS}Agent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}'
   ```

   > [!IMPORTANT]  
   > The location property must match the location of the Arc-enabled SQL Server resource for the server specified by the `--machine-name` parameter.

1. Check to make sure your instance is in the exclusion list (see the value of the *excludedSqlInstances* property).

    ```azurecli
        az connectedmachine extension show --machine-name "{your machine name}" --resource-group "{your resource group name}" -n WindowsAgent.SqlServer
    ```

1. Make sure to remove your instance from the exclusion list and update the extension settings.

    ```azurecli
        az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[\"{named instance 1}\",\"{named instance 3}}\"]}'
    ```

The instance is restored after the next sync with the agent. For information on managing vm extensions using Portal or PowerShell, see [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions).

## Next steps

- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure on-demand SQL assessment for your SQL Server instance](assess.md)