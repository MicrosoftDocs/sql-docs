---
title: Deployment options for Azure Arc-enabled SQL Server
description: Explains different ways to deploy Azure Arc-enabled SQL Server.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 10/12/2022
ms.service: sql
ms.topic: conceptual
---

# Deployment options for Azure Arc-enabled SQL Server

Connecting machines in your hybrid environment directly with Azure can be accomplished using different methods, depending on your requirements and the tools you prefer to use.

An Arc-enabled server will automatically connect SQL Server instances as Azure Arc-enabled SQL Server. For instructions, see [Quickstart: Connect SQL Server machines to Azure Arc](quick-enabled-sql-server.md).

> [!TIP]
> Beginning with SQL Server 2022, you can connect a new SQL Server instance to Azure Arc when you're installing it on Windows Operating System.  [Install SQL Server 2022](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#install-sql-server-2022).

## Onboarding methods

The following table highlights each method so that you can determine which works best for your deployment. For detailed information, follow the links to view the steps for each topic.

| Method | Description |
|--------|-------------|
| Automatic | After a server is Arc-enabled, if the server has one or more instances of SQL Server, the agent will Arc-enable those instances. See [Quickstart: Connect SQL Server machines to Azure Arc](quick-enabled-sql-server.md).| 
| Interactively | Manually connect the SQL Server instance on a single physical or virtual machine that is not currently connected to Azure Arc. [Connect when machine not connected to Azure](connect.md)|
| Interactively | [Connect your SQL Server to Azure Arc with installer (.msi)](connect-with-installer.md) |
| Interactively | Manually connect the SQL Server on a single physical or virtual machine that is already connected to Azure Arc. [Connect your SQL Server to Azure Arc on a server already connected to Azure Arc](connect-already-enabled.md)|
|At scale|[Automatically connect Azure Arc-enabled SQL Servers](connect-at-scale-autodeploy.md)|
| At scale | [Connect SQL Servers at scale using Azure policy](connect-at-scale-policy.md)|
| At scale | [Connect SQL Server at scale using script](connect-at-scale-script.md)|
| At scale | [Connect SQL Server machines at scale with a Configuration Manager custom task sequence](onboard-configuration-manager-custom-task.MD)|

Be sure to review the basic [prerequisites](prerequisites.md) before you deploy the agent, as well as any specific requirements listed in the steps for the onboarding method you choose.

## Next steps

* Learn about the Azure Connected Machine agent and network requirements in [prerequisites](prerequisites.md).