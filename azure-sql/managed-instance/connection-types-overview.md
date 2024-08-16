---
title: Connection types
titleSuffix: Azure SQL Managed Instance
description: Learn about Azure SQL Managed Instance connection types
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: vanto, mathoma
ms.date: 12/01/2021
ms.service: azure-sql-managed-instance
ms.subservice: connect
ms.topic: conceptual
ms.custom: devx-track-azurepowershell
---

# Azure SQL Managed Instance connection types
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article explains how clients connect to Azure SQL Managed Instance depending on the connection type. Script samples to change connection types are provided below, along with considerations related to changing the default connectivity settings.

## Connection types

Azure SQL Managed Instance's VNet-local endpoint supports the following two connection types:

- **Redirect (recommended):** This is the preferred way for SQL clients to connect to managed instances. With redirect, clients establish connections directly to the node hosting the database. To enable redirect, you need to configure firewalls and Network Security Group (NSG) rules to allow inbound access on ports 1433 and port range 11000-11999. Redirect exhibits superior latency and throughput performance compared to proxy. Redirect also minimizes the impact of planned maintenance events of the gateway component, since redirect connections, once established, have no dependency on the gateway. Redirection capability depends on SQL drivers to understand TDS (Tabular Data Stream) 7.4 or newer. TDS 7.4 was first published with Microsoft SQL Server 2012, so any client newer than that will work.
- **Proxy (default):** This is the legacy connectivity mechanism meant to support SQL drivers that implement TDS versions older than 7.4. In this mode, all connections are proxied through the internal gateway and only the port 1433 is required to be open. Depending on the nature of the workload, proxy mode can severely degrade the latency and lower the throughput compared to redirect. It is also more susceptible to the loss of live connections due to planned maintenance events of the gateway component. For this reason, we highly recommend you configure all your managed instances to use the redirect connection policy unless your SQL clients do not support TDS redirects.

Note that redirect option only has effect on the VNet-local endpoint. Public endpoints and private endpoints to Azure SQL Managed Instance always operate in proxy mode.

## Redirect connection type

In the redirect connection type, after the TCP session is established to the SQL engine, the client session obtains the destination virtual IP of the virtual cluster node from the load balancer. Subsequent packets flow directly to the virtual cluster node, bypassing the gateway. The following diagram illustrates this traffic flow.

![Diagram shows an on-premises network with redirect-find-db connected to a gateway in an Azure virtual network and a redirect-query connected to a database primary node in the virtual network.](./media/connection-types-overview/redirect.png)

> [!IMPORTANT]
> The redirect connection type only afects the VNet-local endpoint. Connections coming through public and private endpoints are always handled using the proxy connection type regardless of the connection type setting.

## Proxy connection type

> [!WARNING]
> Proxy connection type is only recommended for old clients and applications that do not support Tabular Data Stream (TDS) standard 7.4 newer (available since SQL Server 2012). Managed instances should be configured to use the redirect connection type whenever possible.

In the proxy connection type, the TCP session is established using the gateway and all subsequent packets flow through it. The following diagram illustrates this traffic flow.

![Diagram shows an on-premises network with a proxy connected to a gateway in an Azure virtual network, connect next to a database primary node in the virtual network.](./media/connection-types-overview/proxy.png)

## Changing Connection Type

- **Using the Portal:**
To change the Connection Type using the Azure portal, open the Virtual Network page and use the **Connection type** setting to change the connection type and save the changes.

- **Script to change connection type settings using PowerShell:**

[!INCLUDE [updated-for-az](../includes/updated-for-az.md)]

The following PowerShell script shows how to change the connection type for a managed instance to `Redirect`.

```powershell
Install-Module -Name Az
Import-Module Az.Accounts
Import-Module Az.Sql

Connect-AzAccount
# Get your SubscriptionId from the Get-AzSubscription command
Get-AzSubscription
# Use your SubscriptionId in place of {subscription-id} below
Select-AzSubscription -SubscriptionId {subscription-id}
# Replace {rg-name} with the resource group for your managed instance, and replace {mi-name} with the name of your managed instance
$mi = Get-AzSqlInstance -ResourceGroupName {rg-name} -Name {mi-name}
$mi = $mi | Set-AzSqlInstance -ProxyOverride "Redirect" -force
```

## Next steps

- [Restore a database to SQL Managed Instance](restore-sample-database-quickstart.md)
- Learn how to [configure a public endpoint on SQL Managed Instance](public-endpoint-configure.md)
- Learn about [SQL Managed Instance connectivity architecture](connectivity-architecture-overview.md)
