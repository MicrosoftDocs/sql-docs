---
title: SQL Server on Azure Arc-enabled servers
titleSuffix:
description: Manage instances of SQL Server on Azure Arc-enabled servers
author: anosov1960
ms.author: sashan 
ms.reviewer: mikeray
ms.date: 07/30/2021
ms.topic: conceptual
ms.prod: sql
ms.custom: references_regions
---

# SQL Server on Azure Arc-enabled servers

You can manage your instances of SQL Server from Azure with SQL Server on Azure Arc-enabled servers.

You can enable SQL Server on [Azure Arc-enabled servers](/azure/azure-arc/servers/overview). It extends Azure services to SQL Server instances hosted outside of Azure; in your datacenter, on the edge, or in a multi-cloud environment.

To enable Azure services, register a running SQL Server instance with Azure Arc using the Azure portal and a registration script. The registration will install a SQL Arc extension to the [Connected Machine agent](/azure/azure-arc/servers/agent-overview), which in turn will a  __SQL Server – Azure Arc__ resource representing each SQL Server instance installed on that machine. The properties of this resource reflect a subset of the SQL Server configuration settings.

## Architecture

The SQL Server instance can be installed in a virtual or physical machine running Windows or Linux that is connected to Azure Arc via the [Connected Machine agent](/azure/azure-arc/servers/agent-overview). When you register the SQL Server instance, the agent is installed and the machine is registered automatically.

The Connected Machine agent communicates outbound securely to Azure Arc over TCP port 443. If the machine connects through a firewall or an HTTP proxy server to communicate over the Internet, review the [network configuration requirements for the Connected Machine agent](/azure/azure-arc/servers/agent-overview#prerequisites).

SQL Server on Azure Arc-enabled servers supports a set of solutions that require the Microsoft Monitoring Agent (MMA) server extension to be installed and connected to an Azure Log analytics workspace for data collection and reporting. These solutions include Advanced data security using Azure Security Center and Azure Sentinel, and SQL Environment health checks using On-demand SQL Assessment feature.

The following diagram illustrates the architecture of SQL Server on Azure Arc enable servers.

![Customer infrastructure hosts virtualization and persistent storage. Use the Azure portal or the appropriate CLI to manage the SQL Server instance.](media/overview/architecture.png)

To learn more about these capabilities, you can also refer to this Data Exposed episode.
> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Understanding-Azure-Arc-Enabled-SQL-Server/player?format=ny]

## Prerequisites

### Supported SQL versions and operating systems

SQL Server on Azure Arc-enabled servers supports SQL Server 2012 or higher running on one of the following versions of the Windows or Linux operating system:

- Windows Server 2012 R2 and higher
- Ubuntu 16.04 and 18.04 (x64)
- Red Hat Enterprise Linux (RHEL) 7 (x64) 
- SUSE Linux Enterprise Server (SLES) 15 (x64)

> [!NOTE]
> SQL Server on Azure Arc-enabled servers does not support container images with SQL Server.  

### Required permissions

To connect the SQL Server instances and the hosting machine to Azure Arc, you must have an account with privileges to perform the following actions:

- Microsoft.HybridCompute/machines/extensions/write
- Microsoft.HybridCompute/machines/extensions/delete
- Microsoft.HybridCompute/machines/read
- Microsoft.HybridCompute/machines/write
- Microsoft.GuestConfiguration/guestConfigurationAssignments/read
- Microsoft.Authorization/roleAssignments/write
- Microsoft.Authorization/roleAssignments/read

For optimal security, create a custom role in Azure that has the minimal permissions listed. For information on how to create a custom role in Azure with these permissions, see [Custom roles overview](/azure/active-directory/users-groups-roles/roles-custom-overview). To add role assignment, see [Add or remove role assignments using Azure portal](/azure/role-based-access-control/role-assignments-portal) or [Add or remove role assignments using Azure RBAC and Azure CLI](/azure/role-based-access-control/role-assignments-cli).

### Azure subscription and service limits

Before configuring your SQL server instances and machines with Azure Arc, review the Azure Resource Manager [subscription limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#subscription-limits) and [resource group limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#resource-group-limits) to plan for the number of machines to be connected.

### Networking configuration and resource providers

Review [networking configuration, transport layer security, and resource providers](/azure/azure-arc/servers/agent-overview#prerequisites) required for Connected Machine agent.

The resource provider `Microsoft.AzureArcData` is required to connect the SQL Server instances to Azure Arc. To register the resource provider, follow the instructions in the [Prerequisites](connect.md#prerequisites) section.

If you connected an instance of SQL Server ot Azure Arc prior to December 2020, you need to follow the [prerequisite steps](connect.md#prerequisites) to migrate the existing **SQL Server - Azure Arc** resources to the new namespace.

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
