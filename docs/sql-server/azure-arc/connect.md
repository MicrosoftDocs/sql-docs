---
title: Connect Your SQL Server to Azure Arc
description: Connect an instance of SQL Server to Azure Arc. Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: pochiraju
ms.author: rajpo
ms.reviewer: vanto
ms.date: 07/08/2025
ms.topic: how-to
ms.custom:
  - references_regions
---

# Connect your SQL Server to Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [automatic](includes/if-manual.md)]

This article explains how to connect your SQL Server instance to Azure Arc. Before you proceed, complete the [Prerequisites - SQL Server enabled by Azure Arc](prerequisites.md).

## Onboard the server to Azure Arc

If the server that runs your SQL Server instance isn't yet connected to Azure, you can initiate the connection from the target machine using the onboarding script. This script connects the server to Azure and installs the Azure extension for SQL Server.

> [!NOTE]  
> If your server is already connected to Azure, proceed to [Connect your SQL Server to Azure Arc on a server already enabled by Azure Arc](connect-already-enabled.md).

### Generate an onboarding script for SQL Server

1. Go to [Azure Arc](https://portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/getStarted) in the Azure portal.

1. Under **Data services**, select **SQL servers**. Under **SQL Server instances**, select **+ Add**.

   The **Add existing SQL Server instances** page opens.

1. Select **Connect SQL Server instances** for a new registration or **Register SQL Server instances** for a disconnected instance.

1. Review the prerequisites and select **Next: Server details**

1. Specify:

    - **Subscription**
    - **Resource group**
    - **Region**
    - **Operating system**

    If necessary, specify the proxy your network uses to connect to the Internet.

    To use a specific name for Azure Arc enabled Server instead of default host name, users can add the name for Azure Arc enabled Server in **Server Name**.

   :::image type="content" source="media/join/server-details-sql-server-azure-arc.png" alt-text="Screenshot of server details for Azure Arc." lightbox="media/join/server-details-sql-server-azure-arc.png":::

1. Select the SQL Server edition and license type you're using on this machine. Some Arc-enabled SQL Server features are only available for SQL Server instances with Software Assurance (Paid) or with Azure pay-as-you-go. For more information, review [Configure SQL Server enabled by Azure Arc](manage-configuration.md).

1. Specify the SQL Server instance(s) you want to exclude from registering (if you have multiple instances installed on the server). Separate each excluded instance by a space.

   > [!IMPORTANT]  
   > If the machine that hosts the SQL Server instance is already [connected to Azure Arc](/azure/azure-arc/servers/onboard-portal), make sure to select the same resource group that contains the corresponding **Server - Azure Arc** resource.

   :::image type="content" source="media/join/server-details-sql-server-management-azure-arc.png" alt-text="Screenshot of server management details." lightbox="media/join/server-details-sql-server-management-azure-arc.png":::

1. Select **Next: Tags** to optionally add tags to the resource for your SQL Server instance.

1. Select **Run script** to generate the onboarding script.

   :::image type="content" source="media/join/download-script-sql-server-azure-arc.png" alt-text="Screenshot of a download script." lightbox="media/join/download-script-sql-server-azure-arc.png":::

1. Select **Download** to download the script to your machine.

### Connect SQL Server instances to Azure Arc

In this step, execute the script you downloaded from the Azure portal, on the target machine. The script installs Azure extension for SQL Server. If the machine itself doesn't have the Azure connected machine agent installed, the script installs it first, then installs the Azure extension for SQL Server. Azure connected machine agent registers the connected server as an Azure resource of type `Server - Azure Arc`, and the Azure extension for SQL Server connects the SQL Server instances as an Azure resource of type `SQL Server - Azure Arc`.

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

   :::image type="content" source="media/join/validate-sql-server-azure-arc.png" alt-text="Screenshot of validating a connected SQL Server." lightbox="media/join/validate-sql-server-azure-arc.png":::

## Upgrade the extension

[!INCLUDE [manage-extension](includes/manage-extension.md)]

## Related content

- [Protect SQL Server with Microsoft Defender for Cloud](configure-advanced-data-security.md)
- [Configure best practices assessment for SQL Server enabled by Azure Arc](assess.md)
- [Known issues: SQL Server enabled by Azure Arc](known-issues.md)
