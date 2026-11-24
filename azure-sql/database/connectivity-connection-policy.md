---
title: Choosing the best connection policy
titleSuffix: Azure SQL Database and SQL database in Fabric
description: This article explains the connection policies available to Azure SQL Database and SQL database in Fabric, their requirements on networking and effects on connection performance.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: wiassaf
ms.date: 10/23/2026
ms.service: azure-sql-database
ms.subservice: connect
ms.topic: concept-article
ms.custom:
  - fasttrack-edit
  - sqldbrb=1
  - references_regions
monikerRange: "=azuresql || =azuresql-db || =fabricsql"
---

# Choosing the connection policy

[!INCLUDE [appliesto-sqldb-fabricsqldb](../includes/appliesto-sqldb-fabricsqldb.md)]

This article explains the connection policies available to Azure SQL Database. It covers different connection policies and how they affect clients connecting from within Azure and clients connecting from outside of Azure.

## Supported connection policies

Azure SQL Database supports three connection policies:
* [Proxy connection policy](#proxy-connection-policy)
* [Redirect connection policy](#redirect-connection-policy)
* [Default connection policy](#default-connection-policy)

For a side-by-side comparison of policies, see [Which connection policy to choose](#which-connection-policy-should-you-choose).

> [!IMPORTANT]
> [SQL database in Microsoft Fabric](/fabric/database/sql/limitations#connection-policy) doesn't support changing the connection policy to a setting other than `Default`.

### Proxy connection policy
The Proxy policy means that the client’s TCP session is established via the Azure SQL Database gateway and all subsequent packets flow via the gateway. The following diagram illustrates this traffic flow.

:::image type="content" source="media/connectivity-architecture/connectivity-outside-azure.svg" alt-text="Diagram that shows how the TCP session is established via the Azure SQL Database gateway and all subsequent packets flow via the gateway.":::

In Proxy mode, all connections go through the Azure SQL Database gateways. This mechanism ensures full backward compatibility with nonstandard TDS drivers that incorrectly report the support for redirection. However, it significantly increases latency, reduces throughput, and causes disconnects due to gateway failovers. To use the Proxy mode for connections, clients need to:

* For public endpoint access: allow outbound communication from the client to Azure SQL Database gateway IP addresses on port 1433. Use the **Gateway IP addresses** list later in this article for your region's IP addresses to allow.
* For private endpoint access: if you’re using Private Link, allow outbound communication from the client to the private endpoint’s IP address on port 1433.

### Redirect connection policy
Redirect policy means that the client's TCP session connects directly with the host running the database. The following diagram shows this traffic flow.

:::image type="content" source="media/connectivity-architecture/connectivity-azure.svg" alt-text="Diagram of the architecture overview of Azure SQL connectivity via redirection within Azure.":::

In Redirect mode, clients first contact the gateway briefly to obtain the address of the node node hosting the database, then disconnect and connect directly to the node. Compared to Proxy, this mechanism reduces latency, improves throughput, and increases the overall resiliency of database connections. To use Redirect mode for connections, clients need to:

* For public endpoint access: allow outbound communication from the client to all Azure SQL IP addresses in the region on ports 1433 and 11000 through 11999 (ports 1434 and 14000 through 14999 for DAC). Use the `Sql.<region>` service tags in Azure to make this easier to manage. See [Azure IP Ranges and Service Tags - Public Cloud](https://www.microsoft.com/download/details.aspx?id=56519).
* For private endpoint access: If you use Private Link, allow client-to-private-endpoint traffic on ports 1433 through 65535. DAC is available on port 1434. For more information, see [Use Redirect connection policy with private endpoints](private-endpoint-overview.md#use-redirect-connection-policy-with-private-endpoints).

### Default connection policy

For public and service endpoints, the Default connection policy automatically chooses between Redirect and Proxy. Connections originating from Azure use Redirect when the client supports TDS 7.4 or later. Connections originating outside Azure, and connections from clients that don't support Redirect, use Proxy.

 For public and service endpoints, clients in Azure using the Default policy must have outbound connectivity to Azure SQL Database on the ports required for Redirect.

 Existing private endpoints using Default use Proxy on port 1433. To use Redirect, meet the [private endpoint prerequisites](private-endpoint-overview.md#use-redirect-connection-policy-with-private-endpoints) and explicitly select the Redirect connection policy.

## Which connection policy should you choose?

**For production workloads, use explicit Redirect**. Redirect provides direct connectivity to the database host, so it avoids the extra latency, throughput limitations, and gateway dependency that Proxy adds.
When you use Redirect, configure client outbound connectivity explicitly to meet the network requirements described in the following table.

> [!NOTE]
>
> Only use Proxy when client or network constraints prevent the use of Redirect. Proxy negatively affects connection performance and reliability.
> For Azure SQL Database, avoid the use of Default in production environments, as its variable application of Proxy and Redirect complicates connectivity setups and makes connectivity problems harder to trace. This recommendation doesn't apply to SQL database in Fabric, where Default is the only supported policy.

| | Redirect (recommended) | Proxy |
| - | ---------------- | ------------------- |
| **Connection path** | **Direct**. The gateway redirects the connection to the database host and is then removed from the data path. | **Indirect**. All traffic continues through the gateway for the lifetime of the connection. |
| **Latency** | **Lower**. After the initial connection through the gateway, traffic flows directly between the client and database host. | Higher. The gateway remains in the data path, which can particularly affect chatty applications making many small, sequential database calls. |
| **Throughput** | **Higher**. Traffic flows directly between the client and database host. | Lower. All database traffic flows through the gateway, which can affect bulk transfers and large result sets. |
| **Performance consistency** | **Better**. The gateway is removed from the data path after redirection. | Adds a dependency on shared regional gateway resources. |
| **Resilience** | **Better isolation from gateway events**. Established redirected connections don't depend on the gateway remaining available. | Established connections depend on the gateway and can be affected by gateway failover or maintenance. |
| **Network requirements, public and service endpoints** | Requires access to the `Sql.<region>` [service tag](https://www.microsoft.com/download/details.aspx?id=56519) on port 1433 and ports 11000–11999. | Requires access to the [regional gateway IP addresses](#gateway-ip-addresses) for the region on port 1433. |
| **Network requirements, private endpoint** | Requires access to the private endpoint IP address on ports 1433–65535. | Requires access to the private endpoint IP address on port 1433. |
| **Driver requirements** | Compatible with all supported TDS drivers. TDS 7.4 or later is required to negotiate redirection; older drivers fall back to Proxy. | Any supported TDS driver. |
| **When to choose** | **Preferred for production workloads**. Design network topology and firewall rules to support Redirect. Use the latest TDS drivers for best security and performance: [SQL Connectivity and Drivers](https://learn.microsoft.com/sql/connect/). | Use only when client or network constraints prevent Redirect from being used. |

**For new deployments, design your network to support Redirect**. Use Proxy only when specific client or network constraints make Redirect impractical.

## Changing connection policy

To change the connection policy after configuring your network flows for the required traffic flows, see [Change the connection policy](connectivity-settings.md#change-the-connection-policy).

## Gateway IP addresses

This section lists the IP address ranges assigned to the regional gateways of SQL Database.

When the [Proxy connection policy](#proxy-connection-policy) is in effect, database clients must reach all IP addresses in all ranges for the region of the logical server. With the [Redirect connection policy](#redirect-connection-policy), clients must reach a wider set of IP addresses. To accomplish this, use the `Sql.<region>` service tags in Azure. For more information, see [Azure IP Ranges and Service Tags - Public Cloud](https://www.microsoft.com/download/details.aspx?id=56519).

Clients connecting to private endpoints don't need connectivity to any of these ranges because a private endpoint has direct connectivity to the gateways.

[!INCLUDE [gateway-ip-addresses](includes/gateway-ip-addresses.md)]
