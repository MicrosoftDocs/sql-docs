---
title: Connectivity Architecture
titleSuffix: Azure SQL Database and SQL database in Fabric
description: This article explains the connectivity architecture for database connections from within Azure or from outside of Azure for Azure SQL Database, SQL database in Fabric
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf
ms.date: 02/26/2026
ms.service: azure-sql-database
ms.subservice: connect
ms.topic: concept-article
ms.custom:
  - fasttrack-edit
  - sqldbrb=1
  - references_regions
monikerRange: "=azuresql || =azuresql-db || =fabricsql"
---

# Connectivity architecture

[!INCLUDE [appliesto-sqldb-fabricsqldb](../includes/appliesto-sqldb-fabricsqldb.md)]

This article explains the architecture of various components that direct network traffic to a server in Azure SQL Database and SQL database in Microsoft Fabric. It provides a broad overview of how to connect to and from Azure SQL Database and how the service establishes internal connectivity from an application endpoint to the target database server.

- For connection strings to Azure SQL Database, see [Connect and query to Azure SQL Database](connect-query-content-reference-guide.md).
- For settings that control connectivity to the [logical server](logical-servers.md) for Azure SQL Database, see [connectivity settings](connectivity-settings.md).
- This article does *not* apply to **Azure SQL Managed Instance**. For more information, see [Connectivity architecture for Azure SQL Managed Instance](../managed-instance/connectivity-architecture-overview.md).
- This article does *not* apply to dedicated SQL pools in Azure Synapse Analytics.
  - For settings that control connectivity to dedicated SQL pools in Azure Synapse Analytics, see [Azure Synapse Analytics connectivity settings](/azure/synapse-analytics/security/connectivity-settings).
  - For connection strings to Azure Synapse Analytics pools, see [Connect to Synapse SQL](/azure/synapse-analytics/sql/connect-overview).

## Overview

Azure SQL Database is more than a virtual machine running SQL Server in the cloud. It manages multiple compute units, establishes logical and security boundaries around data and processes, routes traffic, handles updates and failovers, and provides stable connectivity endpoints for your clients and applications to connect to.

The two principal components of Azure SQL Database connectivity are **redundant regional gateways** and **database cluster**. Gateway is the "front door" that accepts client connections, while database clusters host the actual SQL Server workloads.

:::image type="content" source="media/connectivity-architecture/connectivity-architecture.svg" alt-text="Diagram of the architecture overview of Azure SQL Database.":::

Every Azure SQL Database belongs to a _logical server_ with a globally unique address like `<server-name>.database.windows.net`. This address is always the same regardless of which endpoint you use to connect. The port used to connect is 1433 (or 1434 for Dedicated Admin Connection (DAC)).

Multiple logical servers within a region share the gateways and sometimes even nodes within a database cluster. When an application connects to Azure SQL Database, the connection to the correct logical server and database is made according to the logical server's globally unique address name that the application uses to connect.

> [!IMPORTANT]
> Use the exact hostname to connect to the Azure SQL Database. Changing hostnames via DNS CNAME records isn't supported. See [DNS aliases](dns-alias-overview.md) for a supported alternative.

### Connecting to Azure SQL Database

You can connect to an Azure SQL Database resource through the following endpoints:

- [Public endpoint](connectivity-settings.md#change-public-network-access) to allow inbound traffic from the internet.
- [Service endpoints](vnet-service-endpoint-rule-overview.md) to let your Azure-deployed applications connect to Azure SQL Database service without allowing them broader internet access.
- [Private endpoints](private-endpoint-overview.md) provide your Azure-deployed applications access to individual Azure SQL Database resources.

Before connecting to Azure SQL Database, review [Choosing the connection policy](connectivity-connection-policy.md) and configure your logical servers and network environment for the policy that best suits your scenarios. To change the connection policy, see [Change the connection policy](connectivity-settings.md#change-the-connection-policy).

Use various [network access controls](network-access-controls-overview.md), like built-in firewalls, to specify which IP addresses, virtual networks, or services can connect.

### Connecting Azure SQL Database to other resources and services

Azure SQL Database can connect to other Azure services and resources by initiating outbound connections through T-SQL commands and stored procedures. For supported operations, [outbound firewall rules](outbound-firewall-rule-overview.md) restrict the destinations that the logical server can access. Azure SQL Database also conforms to the [Network Security Perimeter](network-security-perimeter.md) that governs both inbound and outbound traffic between Azure services within a perimeter group.

## Connectivity architecture overview

When a client connects to the gateway, its traffic to the database can follow one of two routes:

- Gateway proxying (“Proxy”), where the gateway connects to the database node on behalf of the client and stays on the network path. Proxy has minimal networking requirements but degrades the overall connection experience.
- Client redirection (“Redirect”), where the gateway provides the client with the host and port of the database node. The client then connects directly to that node. Redirect requires extra network paths from clients to database clusters but delivers a significantly better overall connection experience than Proxy.

You select which route this traffic takes for a particular logical server by setting its connection policy to `Redirect`, `Proxy`, or `Default`. If you don't specify a policy, the logical server uses the `Default` policy.

> [!IMPORTANT]
> Policies provide different levels of performance and reliability. Configure your network environment and logical servers for the `Redirect` connection policy over either `Proxy` or `Default` whenever possible. For more information, see [Choosing the connection policy](connectivity-connection-policy.md).

:::image type="content" source="media/connectivity-architecture/connectivity-policy.svg" alt-text="Diagram of network traffic paths traversed with Proxy and Redirect policies in Azure SQL Database.":::

## Gateway IP addresses

This section lists the IP address ranges assigned to the regional gateways of SQL Database.

When the [Proxy connection policy](connectivity-connection-policy.md#proxy-connection-policy) is in effect, database clients must reach all IP addresses in all ranges for the region of the logical server. With the [Redirect connection policy](connectivity-connection-policy.md#redirect-connection-policy), clients must reach a wider set of IP addresses. To accomplish this, use the `Sql.<region>` service tags in Azure. For more information, see [Azure IP Ranges and Service Tags - Public Cloud](https://www.microsoft.com/download/details.aspx?id=56519).

Clients connecting to private endpoints don't need connectivity to any of these ranges because a private endpoint has direct connectivity to the gateways.

[!INCLUDE [gateway-ip-addresses](includes/gateway-ip-addresses.md)]

## Related content

- [Ports beyond 1433 for ADO.NET 4.5](adonet-v12-develop-direct-route-ports.md)
- [Application development overview - Azure SQL Database & Azure SQL Managed Instance](develop-overview.md)
- [Azure IP Ranges and Service Tags – Public Cloud](https://www.microsoft.com/download/details.aspx?id=56519)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)
