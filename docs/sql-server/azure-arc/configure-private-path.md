---
title: Connect with Private Path
description: This article describes how to connect SQL Server enabled by Azure Arc to Azure with a private path.
author: MashaMSFT
ms.author: mathoma
ms.date: 10/06/2026
ms.topic: how-to #Don't change
# customer intent: As a data engineer, I want to use a private path to connect to Azure Arc so I can avoid internet traffic.
---

# Connect to Azure with a private path for SQL Server enabled by Azure Arc

This article describes how to configure communication for a SQL Server enabled by Azure Arc instance so that it connects to Azure without going over internet paths.

This design deploys forward proxy servers in Azure to allow SQL Server to communicate over a site-to-site VPN or ExpressRouteConnection with private IP addresses. The proxies communicate with Arc URLs over the Azure backbone network.

The following diagram represents this pattern.

:::image type="content" source="media/configure-private-path/architecture.png" alt-text="Diagram representing private path deployment." lightbox="media/configure-private-path/architecture.png":::

For the forward proxy, choose either:

- [Azure Firewall explicit proxy](/azure/firewall/explicit-proxy) feature, which is a platform as a service (PaaS) network security service.

  Or

- A third-party proxy Network Virtual Appliance (NVA).

  The diagram shows Azure Firewall explicit proxy.

## Use case

Use this architecture for SQL Server enabled by Azure Arc when:

- The SQL Server instance is isolated and protected.

- Azure Connected Machine agent can only communicate with a forward proxy over a private IP hosted within your Virtual Network in Azure. The URLs and public IPs that the forward proxy communicates with, for accessing Arc-related URLs, are within the Microsoft backbone, not over the internet. Therefore, traffic doesn't go over the public internet.

- Traffic between the SQL Server instance and the forward proxy has to be secure. In the previous diagram, ExpressRoute is depicted for connectivity between the SQL server and the forward proxy, but it can also be achieved using a site-to-site VPN.

- The traffic traverses over ExpressRoute's private peering rather than public peering.

- The proxy configuration is performed within the Arc Connected Machine agent and not at the OS level. This configuration might not affect your security-related policies.

- Arc communication is encrypted over port 443, and employing ExpressRoute or site-to-site VPN adds an additional layer of security.

## Prerequisite - An Azure virtual network with two subnets

This architecture requires a virtual network in Azure with two subnets. The following steps prepare for site-to-site VPN instead of ExpressRoute.

1. Create a [virtual network](/azure/virtual-network/manage-virtual-network) and subnet for the Azure VPN gateway.
1. Create a separate subnet for Azure Firewall.

A later step deploys Azure Firewall as a forward proxy.

## Create a VPN between the SQL Server and Azure

Create a site-to-site VPN from your SQL Server location to Azure.

1. Follow the steps at [Tutorial: Create and manage a VPN gateway using the Azure portal](/azure/vpn-gateway/tutorial-create-gateway-portal) to create VPN Gateway.

1. Create a local network gateway before you create the site-to-site VPN. Follow the steps in [Tutorial: Create a site-to-site VPN connection in the Azure portal](/azure/vpn-gateway/tutorial-site-to-site-portal).

## Create firewall and configure the proxy

1. Create an Azure Firewall along with Firewall Manager.
1. Configure the Azure Firewall explicit proxy setting to act as a forward proxy.
1. Create a rule to allow the SQL Server IP (`10.2.1.4`).

The Azure Connected Machine agent uses that rule to access `https` outbound through the firewall.

## Connect SQL server using Azure Arc

From Azure portal, generate an onboarding script. As explained here, [Connect your SQL Server to Azure Arc](connect.md).

Run the script to install the Azure Connected Machine agent with the correct configurations. You can configure the proxy settings when you generate your script.

In this article, you update the private path proxy settings after you install the Azure Connected Machine agent.

## Configure Azure Connected Machine agent

To configure Azure Connected Machine agent, use [azcmagent CLI](/azure/azure-arc/servers/azcmagent):

1. Set the proxy URL

   ```console
   azcmagent config set proxy.url "http://<ip address>:8443"
   ```

1. Verify proxy URL

   ```console
   azcmagent config get proxy.url
   ```

   The console returns the current proxy URL.

1. Verify agent is connected.

   ```console
   azcmagent show | find "Agent Status"
   ```

   The console returns the agent status. If the agent is configured the console returns:

   ```output
   Agent Status: Connected
   ```

## Bypass URLs

You might need to configure the agent to bypass certain URLs from going through the proxy, and instead allow direct access. Proxy URL bypass if you need to support private endpoints. For more information, review [Proxy bypass for private endpoints](/azure/azure-arc/servers/manage-agent?tabs=windows#proxy-bypass-for-private-endpoints).

## Configure firewall for log analytics

You can also configure the firewall to send its logs to Azure Log Analytics. For details, review [Monitor Azure Firewall](/azure/firewall/monitor-firewall#azure-monitor-resource-logs).

## Clean up resources

If you're not going to continue to use this application, delete these resources.

To delete the resources from Azure portal:

1. Enter the name of your resource group in the search box select it from the search results.
1. Select **Delete resource group**.
1. Confirm the resource group name at **Type the resource group name**, and select **Delete**.

## Related content

- [Proxy bypass for private endpoints](/azure/azure-arc/servers/manage-agent?tabs=windows#proxy-bypass-for-private-endpoints)
- [Azure Firewall explicit proxy](/azure/firewall/explicit-proxy)
- [Monitor Azure Firewall](/azure/firewall/monitor-firewall#azure-monitor-resource-logs)
- [Tutorial: Create and manage a VPN gateway using the Azure portal](/azure/vpn-gateway/tutorial-create-gateway-portal)
- [Tutorial: Create a site-to-site VPN connection in the Azure portal](/azure/vpn-gateway/tutorial-site-to-site-portal)
