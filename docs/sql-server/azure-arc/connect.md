---
title: Connect your SQL Server to Azure Arc
description: Connect an instance of SQL Server to Azure Arc. Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, maghan
ms.date: 01/12/2023
ms.service: sql
ms.topic: conceptual
ms.custom: event-tier1-build-2022
---

# Connect your SQL Server to Azure Arc

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] you connect a new SQL Server instance to Azure Arc when you're installing it on Windows Operating System. See [Install SQL Server 2022](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#install-sql-server-2022).

This article explains how to connect your SQL Server instance to Azure Arc. Before you proceed, complete the [Prerequisites](prerequisites.md#prerequisites).

## Onboard the server to Azure Arc

If the server that runs your SQL Server instance isn't yet connected to Azure, you can initiate the connection from the target machine using the onboarding script. This script connects the server to Azure and installs the Azure extension for SQL Server.

> [!NOTE]
> If your server is already connected to Azure, proceed to [When the machine is already connected to an Arc-enabled Server](connect-already-enabled.md).

### Generate an onboarding script for SQL Server

1. Go to **Azure Arc > SQL Server** and select **+ Add**

   :::image type="content" source="media/join/start-creation-of-sql-server-azure-arc-resource.png" alt-text="Screenshot of the start creation.":::

1. Under **Connect SQL Server to Azure Arc**, select **Connect Servers**

1. Review the prerequisites and select **Next: Server details**

1. Specify:

    - **Subscription**
    - **Resource group**
    - **Region**
    - **Operating system**

    If necessary, specify the proxy your network uses to connect to the Internet.

   :::image type="content" source="media/join/server-details-sql-server-azure-arc.png" alt-text="Screenshot of server details for Azure Arc.":::

1. Select the SQL Server edition and license type you are using on this machine. [Learn more:](billing.md).

1. Specify the SQL Server instance(s) you want to exclude from registering (if you have multiple instances installed on the server).  Separate each excluded instance by a space.

   > [!IMPORTANT]  
   > If the machine hosting the SQL Server instance is already [connected to Azure Arc](/azure/azure-arc/servers/onboard-portal), make sure to select the same resource group that contains the corresponding **Server - Azure Arc** resource.

   :::image type="content" source="media/join/server-details-sql-server-management-azure-arc.png" alt-text="Screenshot of server management details.":::

1. Select **Next: Tags** to optionally add tags to the resource for your SQL Server instance.

1. Select **Run script** to generate the onboarding script.
Screenshot of

   :::image type="content" source="media/join/download-script-sql-server-azure-arc.png" alt-text="Screenshot of a download script.":::

1. Select **Download** to download the script to your machine.

### Connect SQL Server instances to Azure Arc

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