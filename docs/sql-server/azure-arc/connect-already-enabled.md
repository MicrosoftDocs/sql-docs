---
title: Connect SQL Server on a server already connected to Azure Arc
description: Connect an instance of SQL Server to Azure Arc on a server that is already connected to Arc. Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, maghan
ms.date: 01/12/2023
ms.service: sql
ms.topic: conceptual
ms.custom: event-tier1-build-2022
---

# Connect your SQL Server to Azure Arc on a server already connected to Azure Arc

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] you connect a new SQL Server instance to Azure Arc when you're installing it on Windows Operating System. See [Install SQL Server 2022](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#install-sql-server-2022).

This article explains how to connect your SQL Server instance to Azure Arc on an Arc-enabled server. If the physical or virtual server is not connected to Azure yet, follow the steps in [Connect your SQL Server to Azure Arc](connect.md).

Before you proceed, complete the [Prerequisites](prerequisites.md#prerequisites).

If the machine with SQL Server is already connected to Azure Arc, to connect the SQL Server instances, install *Azure extension for SQL Server*. The extension is in the extension manager as **SQL Server Extension - Azure Arc**. 

Once installed, the Azure extension for SQL Server recognizes all the installed SQL Server instances and connects them with Azure Arc. 

The extension will runs continuously to detect changes in the SQL Server configuration. For example, if a new SQL Server instance is installed on the machine, the extension automatically detects and registers it with Azure Arc. See [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions) for instructions on how to install and uninstall extensions to [Azure connected machine agent](/azure/azure-arc/servers/agent-overview) using the Azure portal, Azure PowerShell or Azure CLI.

> [!IMPORTANT]  
>
> - The Azure resource with type `SQL Server - Azure Arc` representing the SQL Server instance installed on the machine uses the same region and resource group as the Azure resources for Arc-enabled servers.

## [Azure portal](#tab/azure)

To install the Azure extension for SQL Server, use the following steps:

1. Open the **Azure Arc > Servers** resource.
1. Search for the connected server with the SQL Server instance that you want to connect to Azure.
1. Under **Extensions**, select **+ Add**.
1. Select `Azure extension for SQL Server` and select **Next**.
1. Specify the SQL Server edition and license type you are using on this machine.
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

> [!NOTE]  
> The specified resource group must match the resource group where the corresponding connected server is registered. Otherwise, the command will fail.
 an

## Connect SQL Server instances to Azure Arc

In this step, you'll take the script you downloaded from the Azure portal and execute it on the target machine. The script installs Azure extension for SQL Server. If the machine itself doesn't have the Azure connected machine agent installed, the script will install it first, then install the Azure extension for SQL Server. Azure connected machine agent will register the connected server as an Azure resource of type `Server - Azure Arc`, and Azure extension for SQL Server will connect the SQL Server instances as Azure resources of type `SQL Server - Azure Arc`.

> [!IMPORTANT]  
> Make sure to execute the script using an account that meets the minimum permission requirements described in [prerequisites](prerequisites.md).

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