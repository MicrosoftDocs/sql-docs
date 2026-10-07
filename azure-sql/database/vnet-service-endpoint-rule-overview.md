---
title: Virtual Network Endpoints and Rules for Azure SQL Database
description: Learn how to mark a subnet as a virtual network service endpoint and add the endpoint as a virtual network rule for Azure SQL Database.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf
ms.date: 10/06/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: how-to
ms.custom:
  - sqldbrb=1
  - subject-rbac-steps
  - sfi-image-nochange
---
# Use virtual network service endpoints and rules for servers in Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

*Virtual network rules* are a firewall security feature that controls whether the server for your databases and elastic pools in [Azure SQL Database](sql-database-paas-overview.md) accepts communications that are sent from particular subnets in virtual networks. This article explains why virtual network rules are sometimes your best option for securely allowing communication to your database in SQL Database.

> [!NOTE]  
> In this article, *server* refers to the [logical server](logical-servers.md) that hosts databases in Azure SQL Database.

To create a virtual network rule, there must first be a [virtual network service endpoint](/azure/virtual-network/virtual-network-service-endpoints-overview) for the rule to reference.

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Create a virtual network rule

If you want to only create a virtual network rule, you can skip ahead to the steps and explanation [later in this article](#anchor-how-to-by-using-firewall-portal-59j).

## Details about virtual network rules

This section describes several details about virtual network rules.

### Only one geographic region

Each virtual network service endpoint applies to only one Azure region. The endpoint doesn't enable other regions to accept communication from the subnet.

Any virtual network rule is limited to the region that its underlying endpoint applies to.

### Server level, not database level

Each virtual network rule applies to your whole server, not just to one particular database on the server. In other words, virtual network rules apply at the server level, not at the database level.

In contrast, IP rules can apply at either level.

### Security administration roles

There's a separation of security roles in the administration of virtual network service endpoints. Action is required from each of the following roles:

- **Network Admin ([Network Contributor](/azure/role-based-access-control/built-in-roles#network-contributor) role):** &nbsp;Turn on the endpoint.
- **Database Admin ([SQL Server Contributor](/azure/role-based-access-control/built-in-roles#sql-server-contributor) role):** &nbsp;Update the access control list (ACL) to add the given subnet to the server.

#### Azure RBAC alternative

The roles of Network Admin and Database Admin have more capabilities than are needed to manage virtual network rules. You only need a subset of their capabilities.

You can use [role-based access control (RBAC)](/azure/role-based-access-control/overview) in Azure to create a single custom role that has only the necessary subset of capabilities. You can use the custom role instead of involving either the Network Admin or the Database Admin. The surface area of your security exposure is lower if you add a user to a custom role than if you add the user to the other two major administrator roles.

> [!NOTE]  
> In some cases, the database in SQL Database and the virtual network subnet are in different subscriptions. In these cases, you must ensure the following configurations:
>
> - The user has the required permissions to initiate operations, such as enabling service endpoints and adding a virtual network subnet to the given server.
> - Both subscriptions have the `Microsoft.Sql` provider registered.

## Limitations

For SQL Database, the virtual network rules feature has the following limitations:

- In the firewall for your database in SQL Database, each virtual network rule references a subnet. You must host all these referenced subnets in the same geographic region that hosts the database.
- Each server can have up to 128 ACL entries for any virtual network.
- Virtual network rules apply only to Azure Resource Manager virtual networks and not to [classic deployment model](/azure/azure-resource-manager/management/deployment-models) networks.
- On the firewall, IP address ranges apply to the following networking items, but virtual network rules don't:
  - [Site-to-site (S2S) virtual private network (VPN)](/azure/vpn-gateway/index)
  - On-premises via [Azure ExpressRoute](/azure/expressroute/index)
- Both subscriptions must be in the same Microsoft Entra tenant.

### Considerations when you use service endpoints

When you use service endpoints for SQL Database, review the following considerations:

- **Outbound to Azure SQL Database public IPs is required.** You must open network security groups (NSGs) to SQL Database IPs to allow connectivity. You can do this by using NSG [service tags](/azure/virtual-network/network-security-groups-overview#service-tags) for SQL Database.

### ExpressRoute

If you use [ExpressRoute](/azure/expressroute/expressroute-introduction?toc=%2fazure%2fvirtual-network%2ftoc.json) from your premises, for public peering or Microsoft peering, you need to identify the NAT IP addresses that are used. For public peering, each ExpressRoute circuit by default uses two NAT IP addresses applied to Azure service traffic when the traffic enters the Microsoft Azure network backbone. For Microsoft peering, either the customer or the service provider provides the NAT IP addresses that are used. To allow access to your service resources, you must allow these public IP addresses in the resource IP firewall setting. To find your public peering ExpressRoute circuit IP addresses, [open a support ticket with ExpressRoute](https://portal.azure.com/#blade/Microsoft_Azure_Support/HelpAndSupportBlade/overview) via the Azure portal. To learn more about NAT for ExpressRoute public and Microsoft peering, see [NAT requirements for Azure public peering](/azure/expressroute/expressroute-nat?toc=%2fazure%2fvirtual-network%2ftoc.json#nat-requirements-for-azure-public-peering).

To allow communication from your circuit to SQL Database, you must create IP network rules for the public IP addresses of your NAT.

<!--
FYI: Re ARM, 'Azure Service Management (ASM)' was the old name of 'classic deployment model'.
When searching for blogs about ASM, you probably need to use this old and now-forbidden name.
-->

## Impact of using virtual network service endpoints with Azure Storage

Azure Storage supports virtual network service endpoints that limit connectivity to a storage account. If SQL Database uses an account with this configuration, review the impact on blob auditing.

<a id="sql-database-blob-auditing"></a>

### Azure SQL Database auditing to blob storage

Azure SQL auditing can write SQL audit logs to your own storage account. If this storage account uses the virtual network service endpoints feature, see how to [write audit to a storage account behind VNet and firewall](audit-write-storage-account-behind-vnet-firewall.md).

## Add a virtual network firewall rule to your server

Before this feature was enhanced, you had to turn on virtual network service endpoints before you could implement a live virtual network rule in the firewall. The endpoints related a given virtual network subnet to a database in SQL Database. As of January 2018, you can circumvent this requirement by setting the `IgnoreMissingVNetServiceEndpoint` flag. Now, you can add a virtual network firewall rule to your server without turning on virtual network service endpoints.

Merely setting a firewall rule doesn't help secure the server. You must also turn on virtual network service endpoints for the security to take effect. When you turn on service endpoints, your virtual network subnet experiences downtime until it completes the transition from turned off to on. This period of downtime is especially true in the context of large virtual networks. You can use the `IgnoreMissingVNetServiceEndpoint` flag to reduce or eliminate the downtime during transition.

You can set the `IgnoreMissingVNetServiceEndpoint` flag by using PowerShell. For more information, see [PowerShell to create a virtual network service endpoint and rule for SQL Database](scripts/vnet-service-endpoint-rule-powershell-create.md).

<a id="anchor-how-to-by-using-firewall-portal-59j"></a>

## Use Azure portal to create a virtual network rule

This section illustrates how you can use the [Azure portal](https://portal.azure.com/) to create a *virtual network rule* in your database in SQL Database. The rule tells your database to accept communication from a particular subnet that you tagged as a *virtual network service endpoint*.

> [!NOTE]  
> To add a service endpoint to the virtual network firewall rules of your server, first ensure that service endpoints are turned on for the subnet.
>
> If service endpoints aren't turned on for the subnet, the portal asks you to enable them. Select the **Enable** button on the same pane on which you add the rule.

### Prerequisites

You must already have a subnet that's tagged with the particular virtual network service endpoint *type name* relevant to SQL Database.

- The relevant endpoint type name is **Microsoft.Sql**.
- If your subnet might not be tagged with the type name, see [Verify your subnet is an endpoint](scripts/vnet-service-endpoint-rule-powershell-create.md#a-verify-subnet-is-endpoint-ps-100).

<a id="a-portal-steps-for-vnet-rule-200"></a>

### Azure portal steps

1. Sign in to the [Azure portal](https://portal.azure.com/).

1. Search for and select **SQL servers**, and then select your server. Under **Security**, select **Networking**.
1. Under the **Public access** tab, ensure **Public network access** is set to **Select networks**, otherwise the **Virtual networks** settings are hidden. Select **+ Add existing virtual network** in the **Virtual networks** section.

    :::image type="content" source="media/vnet-service-endpoint-rule-overview/portal-firewall-vnet-firewalls-and-virtual-networks.png" alt-text="Screenshot that shows logical server properties for Networking." lightbox="media/vnet-service-endpoint-rule-overview/portal-firewall-vnet-firewalls-and-virtual-networks.png":::

1. In the new **Create/Update** pane, fill in the boxes with the names of your Azure resources.

    > [!TIP]  
    > You must include the correct address prefix for your subnet. You can find the **Address prefix** value in the portal. Go to **All resources** &gt; **All types** &gt; **Virtual networks**. The filter displays your virtual networks. Select your virtual network, and then select **Subnets**. The **ADDRESS RANGE** column has the address prefix you need.

    :::image type="content" source="media/vnet-service-endpoint-rule-overview/portal-firewall-create-update-vnet-rule-20.png" alt-text="Screenshot that shows filling in boxes for the new rule." lightbox="media/vnet-service-endpoint-rule-overview/portal-firewall-create-update-vnet-rule-20.png":::

1. See the resulting virtual network rule on the **Firewall** pane.

    :::image type="content" source="media/vnet-service-endpoint-rule-overview/portal-firewall-vnet-result-rule-30.png" alt-text="Screenshot that shows the new rule on the Firewall pane." lightbox="media/vnet-service-endpoint-rule-overview/portal-firewall-vnet-result-rule-30.png":::

1. Set **Allow Azure services and resources to access this server** to **No**.

    > [!IMPORTANT]  
    > If you leave **Allow Azure services and resources to access this server** checked, your server accepts communication from any subnet inside the Azure boundary. That communication originates from one of the IP addresses that's recognized as those within ranges defined for Azure datacenters. Leaving the control enabled might be excessive access from a security point of view. The Microsoft Azure Virtual Network service endpoint feature in coordination with the virtual network rules feature of SQL Database together can reduce your security surface area.

1. Select the **OK** button near the bottom of the pane.

> [!NOTE]  
> The following statuses or states apply to the rules:
>
> - **Ready**: Indicates that the operation you initiated succeeded.
> - **Failed**: Indicates that the operation you initiated failed.
> - **Deleted**: Only applies to the `Delete` operation and indicates that the rule is deleted and no longer applies.
> - **InProgress**: Indicates that the operation is in progress. The old rule applies while the operation is in this state.

## Use PowerShell to create a virtual network rule

You can use a script to create virtual network rules by using the PowerShell cmdlet `New-AzSqlServerVirtualNetworkRule` or [az network vnet create](/cli/azure/network/vnet#az-network-vnet-create). For more information, see [PowerShell to create a virtual network service endpoint and rule for SQL Database](scripts/vnet-service-endpoint-rule-powershell-create.md).

## Use REST API to create a virtual network rule

Internally, the PowerShell cmdlets for SQL virtual network actions call REST APIs. You can call the REST APIs directly. For more information, see [Virtual network rules: Operations](/rest/api/sql/virtual-network-rules).

<a id="errors-40914-and-40615"></a>

## Troubleshoot errors 40914 and 40615

Connection error 40914 relates to *virtual network rules*, as specified on the **Firewall** pane in the Azure portal.  
Error 40615 is similar, but it relates to *IP address rules* on the firewall.

### Error 40914

**Message text:** "Cannot open server '*[server-name]*' requested by the login. Client is not allowed to access the server."

**Error description:** The client is in a subnet that has virtual network server endpoints. But the server has no virtual network rule that grants the subnet the right to communicate with the database.

**Error resolution:** On the **Firewall** pane of the Azure portal, use the virtual network rules control to [add a virtual network rule](#anchor-how-to-by-using-firewall-portal-59j) for the subnet.

### Error 40615

**Message text:** "Cannot open server '{0}' requested by the login. Client with IP address '{1}' is not allowed to access the server."

**Error description:** The client is trying to connect from an IP address that isn't authorized to connect to the server. The server firewall has no IP address rule that allows a client to communicate from the given IP address to the database.

**Error resolution:** Enter the client's IP address as an IP rule. Use the **Firewall** pane in the Azure portal to complete this step.

## Virtual network service endpoints for Azure Synapse Analytics

For more information, see [Virtual network service endpoints and rules for Azure Synapse Analytics](/azure/synapse-analytics/sql/vnet-service-endpoint-rule-overview).

<a id="anchor-how-to-links-60h"></a>

## Related content

- [Azure virtual network service endpoints](/azure/virtual-network/virtual-network-service-endpoints-overview)
- [Server-level and database-level firewall rules](firewall-configure.md)
- [Use PowerShell to create a virtual network service endpoint and then a virtual network rule for SQL Database](scripts/vnet-service-endpoint-rule-powershell-create.md)
- [Virtual network rules: Operations](/rest/api/sql/virtual-network-rules)