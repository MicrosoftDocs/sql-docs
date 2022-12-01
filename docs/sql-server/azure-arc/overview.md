---
title: Azure Arc-enabled SQL Server
description: Manage instances of Azure Arc-enabled SQL Server
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 10/12/2022
ms.service: sql
ms.topic: conceptual
ms.custom: references_regions
---

# Azure Arc-enabled SQL Server

Azure Arc-enabled SQL Server extends Azure services to SQL Server instances hosted outside of Azure; in your datacenter, on the edge, or in a multi-cloud environment.

To enable Azure services, you must onboard a running SQL Server instance to Azure Arc. The onboarding will install a *Azure  extension for SQL Server* to the [Connected Machine agent](/azure/azure-arc/servers/agent-overview), which in turn will create an Azure resource for each SQL Server instance.  You can see all the Arc-enabled SQL Server resources in the Azure portal under __Azure Arc > SQL Server__. The properties of this resource reflect a subset of the SQL Server configuration settings.

Azure Arc-enabled SQL Server doesn't store any customer data.

## Architecture

The SQL Server instance can be installed in a virtual or physical machine running Windows or Linux that is connected to Azure Arc via the [Connected Machine agent](/azure/azure-arc/servers/agent-overview). When you register the SQL Server instance, the agent is installed, and the machine is registered automatically.

The Connected Machine agent communicates outbound securely to Azure Arc over TCP port 443. If the machine connects through a firewall or an HTTP proxy server to communicate over the Internet, review the [network configuration requirements for the Connected Machine agent](/azure/azure-arc/servers/agent-overview#prerequisites).

Azure Arc-enabled SQL Server supports a set of solutions that require Microsoft Monitoring Agent (MMA) to be installed and connected to an Azure Log analytics workspace for data collection and reporting. These solutions include Microsoft Defender for Cloud and On-demand SQL Assessment feature.

The following diagram illustrates the architecture of SQL Server on Azure Arc enable servers.

:::image type="content" source="media/overview/architecture.png" alt-text="Diagram showing customer infrastructure hosts virtualization and persistent storage. Use the Azure portal or the appropriate CLI to manage the SQL Server instance.":::

To learn more about these capabilities, you can also refer to this Data Exposed episode.
> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Understanding-Azure-Arc-Enabled-SQL-Server/player?format=ny]

## Prerequisites

### Supported SQL versions and operating systems

Azure Arc-enabled SQL Server supports SQL Server 2012 or higher running on one of the following versions of the Windows or Linux operating system:

- Windows Server 2012 R2 and higher
- Ubuntu 20.04 (x64)
- Red Hat Enterprise Linux (RHEL) 8 (x64) 
- SUSE Linux Enterprise Server (SLES) 15 (x64)

> [!NOTE]
> Azure Arc-enabled SQL Server does not support the following configurations currently:
> - SQL Server running in containers.
> - SQL Server Failover Cluster Instances (FCI).
> - SQL Server roles other than the Database Engine, such as Analysis Services (SSAS), Reporting Services (SSRS), or Integration Services (SSIS).
> - SQL Server editions: Express, Web, Business Intelligence.
> - SQL Server 2008, SQL Server 2008 R2, and older.
> - Installing the Arc agent and SQL Server extension cannot be done as part of sysprep image creation.

### Required permissions

To connect the SQL Server instances and the hosting machine to Azure Arc, you must have a user account or Azure service principal with [Contributor](/azure/role-based-access-control/built-in-roles#contributor) role for the resource group in which the SQL Server will be managed.

Deploying the Connected Machine agent on a SQL Server machine requires that you have administrator permissions to install and configure the agent. On Linux this is done by using the root account, and on Windows, with an account that is a member of the Local Administrators group

### Azure subscription and service limits

Before configuring your SQL server instances and machines with Azure Arc, review the Azure Resource Manager [subscription limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#subscription-limits) and [resource group limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#resource-group-limits) to plan for the number of machines to be connected.

### Networking configuration and resource providers

Review [networking configuration, transport layer security, and resource providers](/azure/azure-arc/servers/agent-overview#prerequisites) required for Connected Machine agent.

The resource provider `Microsoft.AzureArcData` is required to connect the SQL Server instances to Azure Arc. To register the resource provider, follow the instructions in the [Prerequisites](connect.md#prerequisites) section.

If you connected an instance of SQL Server to Azure Arc prior to December 2020, you need to follow the [prerequisite steps](connect.md#prerequisites) to migrate the existing Arc-enabled SQL Server resources to the new namespace.

## Supported Azure regions

Arc-enabled SQL Server is available in the following regions:

- East US
- East US 2
- West US 2
- Central US
- South Central US
- UK South
- France Central
- West Europe
- North Europe
- Japan East
- Korea Central
- East Asia
- Southeast Asia
- Australia East

## Next steps

- [Connect your SQL Server to Azure Arc](connect.md)
- [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)
- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
