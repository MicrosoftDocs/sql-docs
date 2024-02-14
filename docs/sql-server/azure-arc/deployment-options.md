---
title: Alternate deployment options
description: Explains different ways to deploy SQL Server enabled by Azure Arc.
author: anosov1960
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 07/18/2023
ms.topic: conceptual
---

# Alternate deployment options for SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

> [!IMPORTANT]  
> Azure Arc automatically installs the Azure extension for SQL Server when a server connected to Azure Arc has SQL Server installed. All the SQL Server instance resources are automatically created in Azure, providing a centralized management platform for all your SQL Server instances.
> To automatically connect your SQL Server instances, see [Automatically Connect your SQL Server to Azure Arc](automatically-connect.md).
> Use the methods below, if your server is already connected to Azure, but Azure extension for SQL Server is not deployed automatically using above methods.

Connecting machines in your hybrid environment directly with Azure can be accomplished using different methods, depending on your requirements and the tools you prefer to use.

> [!TIP]
> Beginning with SQL Server 2022, you can connect a new SQL Server instance to Azure Arc when you're installing it on Windows Operating System.  [Install SQL Server 2022](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#install-sql-server-2022).

## Onboarding methods

The following table highlights each method so that you can determine which works best for your deployment. For detailed information, follow the links to view the steps for each topic.

| Method | Description |
| ------ | ----- |
| Interactively | Manually connect the SQL Server instance on a single physical or virtual machine. [Connect your SQL Server to Azure Arc](automatically-connect.md)|
| Interactively | [Connect your SQL Server to Azure Arc with installer (.msi)](connect-with-installer.md) |
| Interactively | Manually connect the SQL Server on a single physical or virtual machine that is already connected to Azure Arc. [Connect your SQL Server to Azure Arc on a server already connected to Azure Arc](connect-already-enabled.md)|
|At scale| [Automatically enable SQL Server for Azure Arc](manage-autodeploy.md)|
| At scale | Connect SQL Server at scale using Azure policy (deprecated - use [Automatically connect your SQL Server to Azure Arc](automatically-connect.md))|
| At scale | [Connect SQL Server at scale using script](connect-at-scale-script.md)|
| At scale | [Connect SQL Server machines at scale with a Configuration Manager custom task sequence](onboard-configuration-manager-custom-task.MD)|

Be sure to review the basic [prerequisites](prerequisites.md) before you deploy the agent, as well as any specific requirements listed in the steps for the onboarding method you choose.

## Next steps

* Learn about the Azure Connected Machine agent and network requirements in [prerequisites](prerequisites.md)
- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)