---
title: Connect your application to SQL Managed Instance
titleSuffix: Azure SQL Managed Instance
description: This article discusses how to connect your application to Azure SQL Managed Instance.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma, bonova, vanto, randolphwest
ms.date: 05/28/2024
ms.service: sql-managed-instance
ms.subservice: connect
ms.topic: conceptual
ms.custom:
  - sqldbrb=1
---

# Connect your application to Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes how to connect your application to Azure SQL Managed Instance in a number of different application scenarios inside or between Azure virtual networks.

Today you have multiple choices when deciding how and where you host your application. You might choose to host an application in the cloud by using Azure App Service or some of Azure's virtual network integrated options, like Azure App Service Environment, Azure Virtual Machines, and Virtual Machine Scale Sets. You can also take the hybrid ("mixed") cloud approach and keep your applications on-premises. Whatever choice you make, your application can connect to Azure SQL Managed Instance in a number of different application scenarios inside or between Azure virtual networks.

You can also enable data access to your managed instance from outside a virtual network – for example, from multi-tenant Azure services like Power BI and Azure App Service, or from an on-premises network not connected to your virtual networks via VPN. To accomplish these and similar scenarios, refer to [Configure public endpoint in Azure SQL Managed Instance](./public-endpoint-configure.md).

:::image type="content" source="media/connect-application-instance/application-deployment-topologies.png" alt-text="Diagram demonstrating High availability." lightbox="media/connect-application-instance/application-deployment-topologies.png":::

## Connect from inside the same VNet

Connecting an application inside the same virtual network as SQL Managed Instance is the simplest scenario. Virtual machines inside the virtual network can connect to each other directly even if they're inside different subnets. This means that to connect an application inside App Service Environment or a virtual machine deployed in the same virtual network as SQL Managed Instance is to configure the connection string to target its [VNet-local endpoint](connectivity-architecture-overview.md#vnet-local-endpoint).

## Connect from inside a different VNet

Connecting an application when it resides in a virtual network different than that of SQL Managed Instance requires that the application first gains access either to the virtual network where SQL Managed Instance is deployed, or to SQL Managed Instance itself. The two virtual networks don't have to be in the same subscription.

There are three options to connect to a SQL Managed Instance in a different virtual network:

- [Private endpoints](private-endpoint-overview.md)
- [Azure VNet peering](/azure/virtual-network/virtual-network-peering-overview)
- VNet-to-VNet VPN gateway ([Azure portal](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-resource-manager-portal), [PowerShell](/azure/vpn-gateway/vpn-gateway-vnet-vnet-rm-ps), [Azure CLI](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-cli))

Of the three, private endpoints are the most secure and resource-economical option because they:
- only expose the SQL Managed Instance from its virtual network.
- only allow one-way connectivity.
- require just one IP address in the application's virtual network.

If private endpoints can't fully meet the requirements of your scenario, consider virtual network peering instead. Peering uses the backbone Azure network, so there's no noticeable latency penalty for communication across virtual network boundaries. Virtual network peering is supported between networks across all regions (global virtual network peering), while [instances hosted in subnets created before September 22, 2020](frequently-asked-questions-faq.yml#does-sql-managed-instance-support-global-vnet-peering) only support peering within their region.

## Connect from on-premises

You can connect your on-premises application to the [VNet-local endpoint](connectivity-architecture-overview.md#vnet-local-endpoint) of your SQL Managed Instance. In order to access it from on-premises, you need to make a site-to-site connection between the application and the SQL Managed Instance virtual network. If data-only access to your managed instance is sufficient, you can connect to it from outside a virtual network via a public endpoint - review [Configure public endpoint in Azure SQL Managed Instance](./public-endpoint-configure.md) to learn more.

There are two options to connect an on-premises application to an Azure virtual network:

- Site-to-site VPN connection ([Azure portal](/azure/vpn-gateway/tutorial-site-to-site-portal), [PowerShell](/azure/vpn-gateway/vpn-gateway-create-site-to-site-rm-powershell), [Azure CLI](/azure/vpn-gateway/vpn-gateway-howto-site-to-site-resource-manager-cli))
- [Azure ExpressRoute](/azure/expressroute/expressroute-introduction) connection

If you've established an on-premises connection to Azure and you can't establish a connection to SQL Managed Instance, check if your firewall has an open outbound connection on SQL port 1433, as well as the 11000-11999 range of ports for redirection.

## Connect a developer box

It's also possible to connect your developer box to SQL Managed Instance. In order to access it from your developer box via the virtual network, you first need to make a connection between your developer box and the SQL Managed Instance virtual network. To do so, configure a point-to-site connection to a virtual network using native Azure certificate authentication. For more information, see [Configure a point-to-site connection to connect to Azure SQL Managed Instance from an on-premises computer](point-to-site-p2s-configure.md).

For data access to your managed instance from outside a virtual network see [Configure public endpoint in Azure SQL Managed Instance](./public-endpoint-configure.md).

## Connect to a spoke network

Another common scenario is where a VPN gateway is installed in a separate virtual network (and perhaps subscription) - _spoke network_ - from the one hosting SQL Managed Instance (_hub network_). Connectivity to SQL Managed Instance from the spoke network is configured via one of the options listed in [Connect from inside a different VNet](#connect-from-inside-a-different-vnet): private endpoints, VNet peering, or a VNet-to-VNet gateway.

The following sample architecture diagram shows VNet peering:

:::image type="content" source="media/connect-application-instance/vnet-peering.png" alt-text="Diagram showing Virtual network peering.":::

If you're peering hub and spoke networks, ensure the VPN gateway sees the IP addresses from the hub network. To do so, make the following changes under **Peering settings**:

1. In the virtual network that hosts the VPN gateway (spoke network), go to **Peerings**, go to the peered virtual network connection for SQL Managed Instance, and select **Allow Gateway Transit**.
1. In the virtual network that hosts SQL Managed Instance (hub network), go to **Peerings**, go to the peered virtual network connection for the VPN gateway, and select **Use remote gateways**.

## Connect Azure App Service

You can also connect an application hosted by Azure App Service when it's [integrated with your virtual network](/azure/app-service/overview-vnet-integration). To do so, select one of the mechanisms listed in [Connect from inside a different VNet](#connect-from-inside-a-different-vnet). For data access to your managed instance from outside a virtual network, see [Configure public endpoint in Azure SQL Managed Instance](./public-endpoint-configure.md).

A special case for connecting Azure App Service to SQL Managed Instance is when you integrate Azure App Service to a network peered to a SQL Managed Instance virtual network. That case requires the following configuration to be set up:

- SQL Managed Instance virtual network must NOT have a gateway
- SQL Managed Instance virtual network must have the `Use remote gateways` option set
- Peered virtual network must have the `Allow gateway transit` option set

This scenario is illustrated in the following diagram:

:::image type="content" source="media/connect-application-instance/integrated-app-peering.png" alt-text="Diagram for integrated app peering." lightbox="media/connect-application-instance/integrated-app-peering.png":::

> [!NOTE]  
> The virtual network integration feature does not integrate an app with a virtual network that has an ExpressRoute gateway. Even if the ExpressRoute gateway is configured in coexistence mode, virtual network integration does not work. If you need to access resources through an ExpressRoute connection, then you can use App Service Environment, which runs in your virtual network.

To troubleshoot Azure App Service access via virtual network, review [Troubleshooting virtual networks and applications](/azure/app-service/overview-vnet-integration#troubleshooting).

## Troubleshoot connectivity issues

To troubleshoot connectivity issues, review the following:

- If you're unable to connect to SQL Managed Instance from an Azure virtual machine within the same virtual network but a different subnet, check if you have a Network Security Group set up on VM subnet that might be blocking access. Additionally, open outbound connection on SQL port 1433 as well as ports in the range 11000-11999, since those are needed to connect via redirection inside the Azure boundary.
- Ensure that propagation of gateway routes is disabled for the route table associated with the virtual network.
- If using point-to-site VPN, check the configuration in the Azure portal to see if you see **Ingress/Egress** numbers. Nonzero numbers indicate that Azure is routing traffic to/from on-premises.

   :::image type="content" source="media/connect-application-instance/ingress-egress-numbers.png" alt-text="Screenshot showing ingress/egress numbers in the Azure portal." lightbox="media/connect-application-instance/ingress-egress-numbers.png":::

- Check that the client machine (that is running the VPN client) has route entries for all the virtual networks that you need to access. The routes are stored in
`%AppData%\Roaming\Microsoft\Network\Connections\Cm\<GUID>\routes.txt`.

   :::image type="content" source="media/connect-application-instance/route-txt.png" alt-text="Screenshot showing the route.txt." lightbox="media/connect-application-instance/route-txt.png":::

   As shown in this image, there are two entries for each virtual network involved and a third entry for the VPN endpoint that is configured in the portal.

   Another way to check the routes is via the following command. The output shows the routes to the various subnets:

   ```cmd
   C:\ >route print -4
   ===========================================================================
   Interface List
   14...54 ee 75 67 6b 39 ......Intel(R) Ethernet Connection (3) I218-LM
   57...........................rndatavnet
   18...94 65 9c 7d e5 ce ......Intel(R) Dual Band Wireless-AC 7265
   1...........................Software Loopback Interface 1
   Adapter===========================================================================

   IPv4 Route Table
   ===========================================================================
   Active Routes:
   Network Destination        Netmask          Gateway       Interface  Metric
          0.0.0.0          0.0.0.0       10.83.72.1     10.83.74.112     35
         10.0.0.0    255.255.255.0         On-link       172.26.34.2     43
         10.4.0.0    255.255.255.0         On-link       172.26.34.2     43
   ===========================================================================
   Persistent Routes:
   None
   ```

- If you're using virtual network peering, ensure that you have followed the instructions for setting [Allow Gateway Transit and Use Remote Gateways](#connect-from-on-premises).

- If you're using virtual network peering to connect an Azure App Service hosted application, and the SQL Managed Instance virtual network has a public IP address range, make sure that your hosted application settings allow your outbound traffic to be routed to public IP networks. Follow the instructions in [Regional virtual network integration](/azure/app-service/overview-vnet-integration#regional-virtual-network-integration).

## Recommended versions of drivers and tools

Although older versions might work, the following table lists the recommended minimum versions of the tools and drivers to connect to SQL Managed Instance:

| Driver/tool | Version |
| --- | --- |
| .NET Framework | 4.6.1 (or .NET Core) |
| ODBC driver | v17 |
| PHP driver | 5.2.0 |
| JDBC driver | 6.4.0 |
| Node.js driver | 2.1.1 |
| OLEDB driver | 18.0.2.0 |
| SSMS | 18.0 or [higher](/sql/ssms/download-sql-server-management-studio-ssms) |
| [SMO](/sql/relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide) | [150](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) or higher |

## Related content

- [What is SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- [Create a managed instance](instance-create-quickstart.md)
