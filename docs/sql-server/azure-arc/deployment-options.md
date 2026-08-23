---
title: Deployment Options
description: Explains different ways to deploy SQL Server enabled by Azure Arc.
author: pochiraju
ms.author: rajpo
ms.reviewer: randolphwest
ms.date: 07/03/2025
ms.topic: install-set-up-deploy
---

# Deployment options for SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Azure Arc automatically installs the Azure extension for SQL Server when a server connected to Azure Arc has SQL Server installed. All the SQL Server instance resources are automatically created in Azure, providing a centralized management platform for all your SQL Server instances.

To automatically connect your server, see [Azure Connected Machine agent deployment options](/azure/azure-arc/servers/deployment-options).

If your server is already connected to Azure, but Azure extension for SQL Server didn't deploy automatically.

> [!TIP]  
> Beginning with SQL Server 2022, you can connect a new SQL Server instance to Azure Arc when you're installing it on Windows Operating System. [Install SQL Server 2022](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#install-sql-server-2022).

<a id="onboarding-methods"></a>

## Onboard methods

The following table highlights each method so that you can determine which works best for your deployment. For detailed information, follow the links to view the steps for each topic.

| Method | Description |
| --- | --- |
| Interactively | Manually connect the SQL Server on a single physical or virtual machine that is already connected to Azure Arc. [Connect your SQL Server to Azure Arc on a server already enabled by Azure Arc](connect-already-enabled.md) |
| At scale | [Manage automatic connection for SQL Server enabled by Azure Arc](manage-autodeploy.md) |
| At scale | [Connect machines at scale by running PowerShell scripts with Configuration Manager](/azure/azure-arc/servers/onboard-configuration-manager-powershell) |
| At scale | [Connect machines at scale with a Configuration Manager custom task sequence](/azure/azure-arc/servers/onboard-configuration-manager-custom-task) |

Be sure to review the basic [prerequisites](prerequisites.md) before you deploy the agent, as well as any specific requirements listed in the steps for the onboarding method you choose.

## Upgrade the extension

[!INCLUDE [manage-extension](includes/manage-extension.md)]

## Related content

- [Prerequisites - SQL Server enabled by Azure Arc](prerequisites.md)
