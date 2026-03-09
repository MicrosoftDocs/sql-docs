---
title: Connectivity Architecture
titleSuffix: Azure SQL Database and SQL database in Fabric
description: This article explains the connectivity architecture for database connections from within Azure or from outside of Azure for Azure SQL Database, SQL database in Fabric
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, mathoma, randolphwest
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

This article explains the architecture of various components that direct network traffic to a server in Azure SQL Database and SQL database in Microsoft Fabric. It covers different connection policies and how they affect clients connecting from within Azure and clients connecting from outside of Azure.

- For connection strings to Azure SQL Database, see [Connect and query to Azure SQL Database](connect-query-content-reference-guide.md).

- For settings that control connectivity to the [logical server](logical-servers.md) for Azure SQL Database, see [connectivity settings](connectivity-settings.md).

- This article does *not* apply to **Azure SQL Managed Instance**. For more information, see [Connectivity architecture for Azure SQL Managed Instance](../managed-instance/connectivity-architecture-overview.md).

- This article does *not* apply to dedicated SQL pools in Azure Synapse Analytics.

  - For settings that control connectivity to dedicated SQL pools in Azure Synapse Analytics, see [Azure Synapse Analytics connectivity settings](/azure/synapse-analytics/security/connectivity-settings).

  - For connection strings to Azure Synapse Analytics pools, see [Connect to Synapse SQL](/azure/synapse-analytics/sql/connect-overview).

## Connectivity architecture overview

The following diagram provides a high-level overview of the connectivity architecture.

:::image type="content" source="media/connectivity-architecture/connectivity-overview.svg" alt-text="Diagram that shows a high-level overview of the connectivity architecture.":::

The following steps describe how to establish a connection:

1. Clients connect to the gateway that has a public IP address and listens on port 1433.

1. Depending on the effective connection policy, the gateway redirects or proxies the traffic to the correct database cluster.

1. Inside the database cluster, the gateway forwards traffic to the appropriate database.

## Connection policy

Logical SQL servers support the following three options for the server's connection policy setting:

- **Redirect (recommended)**: Clients establish connections directly to the node hosting the database, which reduces latency and improves throughput. To use this mode for connections, clients need to:

  - Allow outbound communication from the client to all Azure SQL IP addresses in the region on ports in the range of 11000 to 11999. Use the Service Tags for SQL to make this easier to manage. If you're using Private Link, see [Use Redirect connection policy with private endpoints](private-endpoint-overview.md#use-redirect-connection-policy-with-private-endpoints) for the port ranges to allow.

  - Allow outbound communication from the client to Azure SQL Database gateway IP addresses on port 1433.

  - When you use the Redirect connection policy, see the [Azure IP Ranges and Service Tags - Public Cloud](https://www.microsoft.com/download/details.aspx?id=56519) for a list of your region's IP addresses to allow.

- **Proxy**: In this mode, all connections go through the Azure SQL Database gateways, which increases latency and reduces throughput. To use this mode for connections, clients need to allow outbound communication from the client to Azure SQL Database gateway IP addresses on port 1433.

  - When you use the Proxy connection policy, see the [Gateway IP addresses](#gateway-ip-addresses) list later in this article for your region's IP addresses to allow.

- **Default**: This connection policy is in effect on all servers after creation unless you explicitly alter the connection policy to either `Proxy` or `Redirect`. The default policy is:

  - `Redirect` for all client connections originating inside of Azure (for example, from an Azure Virtual Machine).

  - `Proxy` for all client connections originating outside (for example, connections from your local workstation).

  - Currently, the connection policy for [SQL database in Microsoft Fabric](/fabric/database/sql/limitations#connection-policy) is **default** and can't be changed.

For the lowest latency and highest throughput, we highly recommend the `Redirect` connection policy instead of the `Proxy` connection policy. However, you need to meet the extra requirements for allowing network traffic for outbound communication:

- If the client is an Azure Virtual Machine, you can accomplish this requirement by using Network Security Groups (NSG) with [service tags](/azure/virtual-network/network-security-groups-overview#service-tags).

- If the client connects from a workstation on-premises, you might need to work with your network admin to allow network traffic through your corporate firewall.

To change the connection policy, see [Change the connection policy](connectivity-settings.md#change-the-connection-policy).

## Connectivity from within Azure

If you connect from within Azure, your connections use a connection policy of `Redirect` by default. A `Redirect` policy means that after the TCP session is established, the client session redirects to the right database cluster. The destination virtual IP changes from the Azure SQL Database gateway to the cluster. All subsequent packets flow directly to the cluster, bypassing the gateway. The following diagram illustrates this traffic flow.

:::image type="content" source="media/connectivity-architecture/connectivity-azure.svg" alt-text="Diagram of the architecture overview of Azure SQL connectivity via redirection within Azure.":::

## Connectivity from outside of Azure

If you connect from outside Azure, your connections use a connection policy of `Proxy` by default. A policy of `Proxy` means that the TCP session is established via the Azure SQL Database gateway and all subsequent packets flow via the gateway. The following diagram illustrates this traffic flow.

:::image type="content" source="media/connectivity-architecture/connectivity-outside-azure.svg" alt-text="Diagram that shows how the TCP session is established via the Azure SQL Database gateway and all subsequent packets flow via the gateway.":::

> [!IMPORTANT]  
> Open TCP ports 1434 and 14000-14999 to enable [Connecting with DAC](/sql/database-engine/configure-windows/diagnostic-connection-for-database-administrators#connecting-with-dac).

## Gateway IP addresses

This section lists the IP address ranges assigned to the regional gateways of SQL Database.

When the proxy [connection policy](#connection-policy) is in effect, database clients must be able to reach all given IP addresses in all ranges for the region of the logical server. With the redirect connection type, clients must be able to reach a wider set of IP addresses. To accomplish this, use the `Sql.<region>` service tags in Azure. For more information, see [Azure IP Ranges and Service Tags - Public Cloud](https://www.microsoft.com/download/details.aspx?id=56519).

Clients connecting to private endpoints don't need connectivity to any of these ranges because a private endpoint has direct connectivity to the gateways.

[!INCLUDE [gateway-ip-addresses](includes/gateway-ip-addresses.md)]

## Related content

- [Ports beyond 1433 for ADO.NET 4.5](adonet-v12-develop-direct-route-ports.md)
- [Application development overview - Azure SQL Database & Azure SQL Managed Instance](develop-overview.md)
- [Azure IP Ranges and Service Tags – Public Cloud](https://www.microsoft.com/download/details.aspx?id=56519)
