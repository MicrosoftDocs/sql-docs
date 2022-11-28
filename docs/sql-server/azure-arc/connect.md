---
title: Connect to Azure Arc
description: Connect an instance of SQL Server to Azure Arc. Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray
ms.date: 09/30/2021
ms.topic: conceptual
ms.custom:
- event-tier1-build-2022
ms.service: sql
---
# Connect your SQL Server to Azure Arc

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] you connect a new SQL Server instance to Azure Arc when you are installing it on Windows Operating System. See [Install SQL Server 2022](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#install-sql-server-2022).

You can connect your existing SQL Server instance to Azure Arc by following these steps.

## Prerequisites

* Your machine has at least one instance of SQL Server installed
* The **Microsoft.AzureArcData** and **Microsoft.HybridCompute** resource providers have been registered.
* The user onboarding Arc-enabled SQL Server resources must have the following permissions:

   Microsoft.AzureArcData/sqlServerInstances/read

   Microsoft.AzureArcData/sqlServerInstances/write

> [!NOTE]
> SQL Server on Azure Arc-enabled servers does not support SQL Server Failover Cluster Instances. 

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

## When machine already connected to Arc-enabled Server

If the machine with SQL Server is already connected to Azure Arc, you can connect the SQL Server instances on that machine by installing *Azure extension for SQL Server*. The SQL Server extension for Azure Arc Server can be found in the extension manager as **SQL Server Extension - Azure Arc**.  Once installed, Azure extension for SQL Server will recognize all the installed SQL Server instances and connect them with Azure Arc. The extension will run continuously to detect changes of the SQL Server configuration. For example, if a new SQL Server instance is installed on the machine, it will be automatically registered with Azure Arc. See [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions) for instructions on how to install and uninstall extensions to [Azure connected machine agent](/azure/azure-arc/servers/agent-overview) using the Azure portal, Azure PowerShell or Azure CLI.

> [!IMPORTANT]
>
> - The Managed System Identity used by the Azure connected machine agent must have the *Azure Connected SQL Server Onboarding* role at resource group level.
> - The Azure resource with type `SQL Server - Azure Arc` representing the SQL Server instance installed on the machine machine will use the same region and resource group as the Azure resources for Arc-enabled servers.

# [Azure portal](#tab/azure)

To install the Azure extension for SQL Server, use the following steps:

1. Open the __Azure Arc > Servers__ resource. 
1. Search for the connected server with the SQL Server instance that you want to connect to Azure
1. Under __Extensions__, click __+ Add__ 
1. Select `Azure extension for SQL Server` and click __Next__.
1. Specify the SQL Server instance(s) you want to exclude from registering (if you have multiple instances installed on the server) and click __Review + Create__
1.  Click __Create__

# [PowerShell](#tab/powershell)

To install *Azure extension for SQL Server*, run:

```powershell
$Settings = @{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}
New-AzConnectedMachineExtension -Name "WindowsAgent.SqlServer" -ResourceGroupName {your resource group name} -MachineName {your machine name} -Location {azure region} -Publisher "Microsoft.AzureData" -Settings $Settings -ExtensionType "WindowsAgent.SqlServer"
```

# [Azure CLI](#tab/az)

To install *Azure extension for SQL Server* for Windows Operating System, run:

```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}'
```

---

To install *Azure extension for SQL Server* for Linux operating system, run:

```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "LinuxAgent.SqlServer" --resource-group "{your resource group name}" --type "LinuxAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}'
```

*Azure extension for SQL Server* for Linux is available for preview.

---

> [!NOTE]
> The specified resource group must match the resource group where the corresponding connected server is registered. Otherwise, the command will fail.

## When machine not connected to Arc-enabled Server

If the server that runs your SQL Server instance is not yet connected to Azure, you can initiate the connection from the target machine using the onboarding script. This script will connect the server to Azure and will install Azure extension for SQL Server.

### Generate an onboarding script for SQL Server

1. Go to __Azure Arc > SQL Server__ and click __+ Add__
   ![Start creation](media/join/start-creation-of-sql-server-azure-arc-resource.png)

1. Select __Connect SQL Server to Azure Arc__  

1. Review the prerequisites and click __Next: Server details__

1. Select the subscription, resource group, Azure region, and the host operating system. If required, also specify the proxy that your network uses to connect to Internet.

   > [!IMPORTANT]
   > If the machine hosting the SQL Server instance is already [connected to Azure Arc](/azure/azure-arc/servers/onboard-portal), make sure to select the same resource group that contains the corresponding __Server - Azure Arc__ resource.

   ![Server details](media/join/server-details-sql-server-azure-arc.png)

1. Click __Tags__ to optionally add tags to the resource for your SQL Server instance.

1. Click __Run script__ to generate the onboarding script.

   ![Download script](media/join/download-script-sql-server-azure-arc.png)

1. Click __Download__ to download the script to your machine. 

### Connect SQL Server instances to Azure Arc

In this step, you will take the script you downloaded from Azure portal and execute it on the target machine. The script installs Azure extension for SQL Server. If the machine itself does not have the Azure connected machine agent installed, the script will install it first, then install Azure extension for SQL Server. Azure connected machine agent will register the connected server as an Azure resource of type `Server - Azure Arc`, and Azure extension for SQL Server will connect the SQL Server instances as Azure resources of type `SQL Server - Azure Arc`.

> [!IMPORTANT]
> Make sure to execute the script using an account that meets the minimum permission requirements described in [prerequisites](overview.md#prerequisites).

# [Windows](#tab/windows)

1. Launch an admin instance of __powershell.exe__ and sign in your PowerShell module with your Azure credentials. Follow the [sign in instructions](/powershell/azure/install-az-ps#sign-in).

2. Execute the downloaded script

   ```powershell
   & '.\RegisterSqlServerArc.ps1'
   ```

   > [!NOTE]
   > If you haven't previously installed the [Az PowerShell module](/powershell/azure/new-azureps-module-az) and see issues the first time you run it, follow the instructions in the script and run it again.

# [Linux](#tab/linux)

1. Use Azure CLI to sign in with your Azure credentials. Follow the [sign in instructions](/cli/azure/authenticate-azure-cli)

2. Grant the execution permission to the downloaded script and execute it.

   ```console
   sudo chmod +x ./RegisterSqlServerArc.sh
   ./RegisterSqlServerArc.sh
   ```

---

## Deploy SQL Server extenstion from AzureExtensionForSQLServer.msi

Alternatively you can also onboard your SQL Servers to Azure Arc by directly using  AzureExtensionForSQLServer.msi.  This method will help integrating onboarding SQL Servers to Arc with any existing deployment automation tools and services.
1. Download AzureExtensionForSQLServer.msi from the [link](https://aka.ms/AzureExtensionForSQLServer).
2. Double click on AzureExtensionForSQLServer.msi.  This will install the necessary packages for onboarding SQL Servers to Azure Arc.
3. Open powershell console in admin mode and execute the following commands.
   
   If you use Azure Active Directory service principal to authenticate execute the command below on the target SQL Server.
   ```powershell
   '& "$env:ProgramW6432\AzureExtensionForSQLServer\AzureExtensionForSQLServer.exe" --subId <subscriptionid> --resourceGroup <resourceGroupName> --location <AzureLocation> --tenantid <TenantId> --service-principal-app-id <servicePrincipalAppId> --service-principal-secret <servicePrincipalSecret> --proxy <proxy> --tags ""'
   ```
   Otherwise execute the command below on the target SQL Server.
   ```powershell
   '& "$env:ProgramW6432\AzureExtensionForSQLServer\AzureExtensionForSQLServer.exe" --subId  <subscriptionid>--resourceGroup <resourceGroupName> --location <AzureLocation> --tenantid <TenantId>  --proxy  <proxy> --tags ""'
   ```
## Validate your Arc-enabled SQL Server resources

Go __Azure Arc > SQL Server__ and open the newly registered Arc-enabled SQL Server resource  to validate.

![Validate connected SQL server ](media/join/validate-sql-server-azure-arc.png)

## Delete your Arc-enabled SQL Server resource

To delete your Arc-enabled SQL Server resource, go to __Azure Arc > SQL Server__, open the Arc-enabled SQL Server resource for that instance, and select the **Delete** button. 

> [!IMPORTANT]
> Because there could be multiple SQL Server instances installed on the same machine, the *Delete* button will not uninstall Azure extension for SQL Server on that machine.  To uninstall it, follow the [uninstall extension](/azure/azure-arc/servers/manage-vm-extensions-portal#uninstall-extension) steps.

## Restore a deleted Arc-enabled SQL Server resource

If you deleted your Arc-enabled SQL Server resource by mistake, you can restore it with the following steps.

1. If you also uninstalled the SQL Server extension by mistake, reinstall it. Select the correct version for your OS.

```azurecli
   az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "{OS}Agent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}'
```
 > [!IMPORTANT]
   > The location property must match the location of the Arc-enabled SQL Server resource for the server specified by the *--machine-name* parameter.

2. Check to make sure your instance is in the exclusion list (see the value of the _excludedSqlInstances_ property).

```azurecli
    az connectedmachine extension show --machine-name "{your machine name}" --resource-group "{your resource group name}" -n WindowsAgent.SqlServer
```

3. Make sure to remove your instance from the exclusion list and update the extension settings.

```azurecli
    az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[\"{named instance 1}\",\"{named instance 3}}\"]}'
```
The instance will be restored after the next sync with the agent. For information on how to manage vm extensions using Portal or PowerShell, see [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions).

## Next steps

* [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)

* [Configure on-demand SQL assessment for your SQL Server instance](assess.md)
