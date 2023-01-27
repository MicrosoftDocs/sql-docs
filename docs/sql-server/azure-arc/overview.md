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

Azure Arc-enabled SQL Server extends Azure services to SQL Server instances hosted outside of Azure; in your datacenter, on the edge, or in a multicloud environment.

To enable Azure services, you must onboard a running SQL Server instance to Azure Arc. The onboarding will install an *Azure  extension for SQL Server* to the [Connected Machine agent](/azure/azure-arc/servers/agent-overview), which in turn will create an Azure resource for each SQL Server instance.  You can see all the Arc-enabled SQL Server resources in the Azure portal under __Azure Arc > SQL Server__. The properties of this resource reflect a subset of the SQL Server configuration settings.

Azure Arc-enabled SQL Server doesn't store any customer data.

## Architecture

The SQL Server instance can be installed in a virtual or physical machine running Windows or Linux that is connected to Azure Arc via the [Connected Machine agent](/azure/azure-arc/servers/agent-overview). When you register the SQL Server instance, the agent is installed, and the machine is registered automatically.

The Connected Machine agent communicates outbound securely to Azure Arc over TCP port 443. If the machine connects through a firewall or an HTTP proxy server to communicate over the Internet, review the [network configuration requirements for the Connected Machine agent](/azure/azure-arc/servers/agent-overview#prerequisites).

Azure Arc-enabled SQL Server supports a set of solutions that require Microsoft Monitoring Agent (MMA) to be installed and connected to an Azure Log analytics workspace for data collection and reporting. These solutions include Microsoft Defender for Cloud and On-demand SQL Assessment feature.

The following diagram illustrates the architecture of SQL Server on Azure Arc enable servers.

:::image type="content" source="media/overview/architecture.png" alt-text="Diagram showing customer infrastructure hosts virtualization and persistent storage. Use the Azure portal or the appropriate CLI to manage the SQL Server instance.":::

To learn more about these capabilities, you can also refer to this Data Exposed episode.
> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Understanding-Azure-Arc-Enabled-SQL-Server/player?format=ny]

## Next steps

- [Prerequisites](prerequisites.md)
- [Connect your SQL Server to Azure Arc](connect.md)
- [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)
- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
