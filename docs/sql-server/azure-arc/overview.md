---
title: Azure Arc enabled SQL Server
titleSuffix:
description: Manage instances of SQL Server with Azure Arc enabled SQL Server
author: anosov1960
ms.author: sashan 
ms.reviewer: mikeray
ms.date: 09/06/2020
ms.topic: conceptual
ms.prod: sql
---

# Azure Arc enabled SQL Server (preview)

Azure Arc enabled SQL Server is part of the Azure Arc for servers. It extends Azure services to SQL Server instances hosted outside of Azure in the customer’s datacenter, on the edge or in a multi-cloud environment.

To enable Azure services, a running SQL Server instance must be registered with Azure Arc using Azure Portal and a registration script. After registration the instance will be represented on Azure as a __SQL Server – Azure Arc__ resource   . The properties of this resource reflect a subset of the SQL Server confutation settings.

The SQL Server can be installed in a virtual or physical machine running Windows or Linux that is connected to Azure Arc via the Connected Machine agent. The agent is installed and machine is and registered automatically as part of the SQL Server instance registration. The Connected Machine agent communicates outbound securely to Azure Arc over TCP port 443. If the machine connects through a firewall or a HTTP proxy server to communicate over the Internet, review the [network configuration requirements for the Connected Machine agent](https://docs.microsoft.com/en-us/azure/azure-arc/servers/agent-overview#prerequisites).

The public preview of Azure Arc enabled SQL Server supports a set of solutions that require the Microsoft Monitoring Agent (MMA) server extension to be installed and connected to a Azure Log analytics workspace for data collection and reporting. These solutions include Advanced data security using Azure Security Center and Azure Sentinel, and SQL Environment health checks using On-demand SQL Assessment feature.

The following diagram illustrates the architecture of Azure Arc enable SQL Server using the virtualized environment.

![Public preview architecture](media/overview/pubic-preview-architecture.png)

## Prerequisites

### Supported SQL versions and operating systems

Azure Arc enabled SQL Server supports SQL Server 2012 or higher running on one of the following versions of the Windows and Linux operating system:
- Windows Server 2012 R2 and higher
- Ubuntu 16.04 and 18.04 (x64)
- CentOS Linux 7 (x64)
- SUSE Linux Enterprise Server (SLES) 15 (x64)
- Red Hat Enterprise Linux (RHEL) 7 (x64)
- Amazon Linux 2 (x64)

### Required permissions

To connect the SQL Server instances and the hosting to Azure Arc, you must be a member of the __Azure Connected Machine Resource Administrator role__.

### Azure subscription and service limits

Before configuring your SQL server instances and machines with Azure Arc, review the Azure Resource Manager [subscription limits](https://docs.microsoft.com/en-us/azure/azure-resource-manager/management/azure-subscription-service-limits#subscription-limits) and [resource group limits](https://docs.microsoft.com/en-us/azure/azure-resource-manager/management/azure-subscription-service-limits#resource-group-limits) to plan for the number of machines to be connected.

### Networking configuration and resource providers

Review [networking configuration, transport layer security and resource providers](https://docs.microsoft.com/en-us/azure/azure-arc/servers/agent-overview#prerequisites) required for Connected machine agent.

### Supported Azure regions

The public preview is available in the following regions:
- East US
- West US 2
- Southeast Asia
- West Europe

## Next steps

- [Connect your SQL Server to Azure Arc](connect.md)
- [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)
- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
