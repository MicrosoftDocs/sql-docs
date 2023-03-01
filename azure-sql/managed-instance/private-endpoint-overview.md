---
title: Azure Private Link and private endpoints for Azure SQL Managed Instance
titleSuffix: Azure SQL Managed Instance
description: Connect your Azure SQL Managed Instance to virtual networks and Azure services via private endpoints.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma, srbozovi
ms.date: 11/30/2022
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: how-to
---
# Azure Private Link for Azure SQL Managed Instance (Preview)
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

[Private Link](../../private-link/private-link-overview) is an Azure technology that makes Azure SQL Managed Instance available in a virtual network of your choice. A network administrator can deploy [private endpoints](../../private-link/private-endpoint-overview) to Azure SQL Managed Instance in another virtual network, while an administrator of that network will have a chance to accept or reject it before the endpoint goes live. Private endpoints establish secure, isolated connectivity between a service and multiple virtual networks without exposing your service's entire network infrastructure.

### How does a private endpoint differ from VNet-local endpoint?
The default, VNet-local endpoint deployed with each Azure SQL Managed Instance behaves as if a computer running the service were physically attached to your virtual network. It allows near-complete traffic control via route tables, network security groups, DNS resolution, firewalls, and similar mechanisms. You can also use this endpoint to involve your instance in scenarios requiring connectivity on ports other than 1433, such as linked server, failover groups, and MI Link. However, great flexibility that this endpoint provides comes with complexity in configuring it for particular scenarios, especially those involving multiple virtual networks or tenants.

By contrast, setting up a private endpoint is like extending a physical network cable from a computer running Azure SQL Managed Instance to another virtual network. This connectivity path is established virtually via Azure Private Link technology. It only allows connections in one direction: from the private endpoint to Azure SQL Managed Instance; and it only carries traffic on port 1433 (the standard TDS traffic port). In this way, your Azure SQL Managed Instance becomes available in a different virtual network without having to set up network peering, turning on the instance's public endpoint, and securing the link.

For a more detailed discussion of the different types of endpoints supported by Azure SQL Managed Instance, see [How applications connect to Azure SQL Managed Instance](connectivity-architecture-overview.md#how-applications-connect-to-azure-sql-managed-instance).

## When to use private endpoints
Private endpoints to Azure SQL Managed Instance allow you to implement important connectivity scenarios more easily and securely than by using [VNet-local endpoint](connectivity-architecture-overview.md#vnet-local-endpoint) or [public endpoint](connectivity-architecture-overview.md#public-endpoint). These scenarios include:

- **Airlock**. Private endpoints to Azure SQL Managed Instance are deployed in a virtual network with jumpboxes and an ExpressRoute gateway, providing security and isolation between on-premises and cloud resources.
- **Hub and spoke topology**. Private endpoints in spoke virtual networks conduct traffic from SQL clients and applications to Azure SQL Managed Instances in a hub virtual network, establishing clear network isolation and separation of responsibility.
- **Publisher-consumer**. Publisher tenant (for example, an ISV) manages multiple Azure SQL Managed Instances in their virtual networks. Publisher creates private endpoints in other tenants' virtual networks to make instances available to their consumers.
- **Integration of Azure PaaS and SaaS services.** Some PaaS and SaaS services, like [Azure Data Factory](../../data-factory/introduction.md), can create and manage private endpoints to Azure SQL Managed Instance.

The benefits of using private endpoints over a VNet-local or public endpoint include:

- Granular network access: a private endpoint is only visible inside its virtual network.
- Strong network isolation: In a peering scenario, peered virtual networks establish two-way connectivity, while private endpoints are unidirectional and don't expose network resources inside their network to Azure SQL Managed Instance.
- Avoiding address overlap: peering multiple virtual networks requires careful IP space allocation and can pose a problem when address spaces overlap.
- Conserving IP address real estate: a private endpoint only consumes one IP address from its subnet's address space.
- IP address predictability: private endpoints to Azure SQL Managed Instance are assigned fixed IP addresses unique inside their subnets.

There are a couple of caveats:

- Private endpoint only allows connections to port 1433, which means that more involved connectivity, automation and integration scenarios won't work. These scenarios include linked server scenarios, MI Link, failover groups, and similar.
- In preview, private endpoints require special setup to configure the correct DNS resolution required for Azure SQL Managed Instance.

## Limitations

- Azure SQL Managed Instance requires the exact instance hostname to appear connection string; using private endpoint's IP address instead will fail. To resolve, configure your DNS server, or use a private DNS zone as described in [Set up domain name resolution for private endpoint](#set-up-domain-name-resolution-for-private-endpoint).
- Automatic registration of DNS names is disabled while in preview. Follow the steps in [Set up domain name resolution for private endpoint](#set-up-domain-name-resolution-for-private-endpoint) instead.
- Private endpoints to SQL Managed Instance can only be used to connect to port 1433, the standard TDS port for SQL traffic. More complex connectivity scenarios requiring communication on other ports must be established via instance's VNet-local endpoint.

## How to set up a private endpoint to Azure SQL Managed Instance

### Create a private endpoint in a virtual network

Private endpoints can be created using the Azure portal, PowerShell, or the Azure API:

- [The portal](../../private-link/create-private-endpoint-portal.md)
- [PowerShell](../../private-link/create-private-endpoint-powershell.md)
- [CLI](../../private-link/create-private-endpoint-cli.md)

After creating a private endpoint, you may need to approve its creation in the target virtual network; see [Review and approve a request to create a private endpoint](#review-and-approve-a-request-to-create-a-private-endpoint).

To make the private endpoint to SQL Managed Instance fully functional, follow the instructions on how to [set up domain name resolution for private endpoint](#set-up-domain-name-resolution-for-private-endpoint).

### Create a private endpoint in a PaaS or SaaS service
Some Azure PaaS and SaaS services can use private endpoints to access your data from inside their environments. For example, [Azure Data Factory](../../data-factory/introduction.md) allows you to set up a data source by linking to Azure SQL Managed Instance via a private endpoint inside an integration runtime. The procedure to set up a private endpoint in such a service (sometimes called "managed private endpoint" or "private endpoint in a managed virtual network") will vary depending on the service. An administrator will still need to review and approve the request on Azure SQL Managed Instance, as described in [Review and approve a request to create a private endpoint](#review-and-approve-a-request-to-create-a-private-endpoint).

> [!NOTE]
> Note that Azure SQL Managed Instance requires for SQL client's connection string to bear the name of the instance as the domain name's first segment (e.g. `<instance-name>.<dns-zone>.database.windows.net`). Some PaaS and SaaS services may attempt to connect to the private endpoint via its IP address, which will fail.

### Review and approve a request to create a private endpoint

Once a request to create a private endpoint is made, the SQL administrator can manage the private endpoint connection to Azure SQL Managed Instance. The first step to managing a new private endpoint connection is to review and approve it. This step is automatic if the user or service creating the private endpoint has sufficient Azure RBAC permissions on the Azure SQL Managed Instance resource. If they don't, then the review and approval must be done manually:

1. Navigate to your Azure SQL Managed Instance in Azure Portal.
2. In the sidebar, select Private endpoint connections.
![Screenshot of private endpoint connections blade showing two pending connections](media/private-endpoint/private-endpoint-connections.png)
1. Review the connections in Pending state and select one or more private endpoint connections to approve or reject.
![Screenshot of one private endpoint connection selected for approval](media/private-endpoint/pec-select.png)
1. Approve or reject selected private endpoint connection(s) with an optional text response.
![Screenshot of dialog prompting for a response message to accompany the approval of a connection](media/private-endpoint/pec-approve.png)
1. After you approve or reject connections, the list will reflect the state of selected private endpoint connection(s) along with any text response.
![Screenshot of private endpoint connections blade showing one pending and one approved connection](media/private-endpoint/pec-list-after.png)

### Set up domain name resolution for private endpoint

After you create a private endpoint to Azure SQL Managed Instance, you'll need to configure domain name resolution. Otherwise, login attempts will fail. The method below works for virtual networks that use Azure DNS resolution. If your virtual network is configured to use a custom DNS server, adjust the steps accordingly.

To set up domain name resolution for private endpoint to an instance whose FQDN is `<instance-name>.<dns-zone>.database.windows.net`, we'll consider two different virtual networks:
- Instance virtual network: hosts Azure SQL Managed Instance.
- Endpoint virtual network: hosts the private endpoint to the Azure SQL Managed Instance.

#### [Separate virtual networks](#tab/separate-vnets)

Follow the steps below if the instance and endpoint virtual networks are different and are not peered.
After you complete these steps, SQL clients connecting to `<instance-name>.<dns-zone>.database.windows.net` from inside the endpoint virtual network will be transparently routed through the private endpoint.

1. Obtain the IP address of the private endpoint either by visiting Private Link Center or by performing the following steps:
    1. Navigate to your Azure SQL Managed Instance in Azure portal.
    2. In the sidebar, select Private endpoint connections.
    3. Locate the private endpoint connection in the table and select its name.
    ![Screenshot of private endpoint connections blade with a highlight on one private endpoint name](media/private-endpoint/pec-click.png)
    4. In Overview, select the network interface.
    ![Screenshot of private endpoint connection overview with a highlight on network interface](media/private-endpoint/pec-nic-click.png)
    5. In Overview, Private IP address is shown in the Essentials section.
![Screenshot of private endpoint connection's network interface with a highlight on its private IP address](media/private-endpoint/pec-ip-display.png)
2. [Create a private Azure DNS zone](../../dns/private-dns-getstarted-portal.md#create-a-private-dns-zone) named `privatelink.<dns-zone>.database.windows.net`.
3. [Link the private DNS zone to the endpoint virtual network](../../dns/private-dns-getstarted-portal.md#link-the-virtual-network).
4. In the DNS zone, create a new record set with the following values:
   - Name: `<instance-name>`
   - Type: A
   - IP address: IP address of the private endpoint obtained in step 1.

#### [Same or peered virtual networks](#tab/same-vnet)
Follow the steps below if the instance and endpoint virtual networks are the same or are peered.

After you complete these steps, SQL clients inside the endpoint virtual network whose connection string includes `Encrypt=false` and `TrustServerCertificate=true` can connect to the private endpoint. They can still connect to the VNet-local endpoint at `<instance-name>.<dns-zone>.database.windows.net` without modifications.

> [!NOTE]
> When the instance and endpoint virtual networks are the same (or peered), you don't have the ability to establish trusted encrypted connections and to transparently re-route SQL clients via the private endpoint.

1. Obtain the IP address of the private endpoint either by visiting Private Link Center or by performing the following steps:
    1. Navigate to your Azure SQL Managed Instance in Azure portal.
    2. In the sidebar, select Private endpoint connections.
    3. Locate the private endpoint connection in the table and select its name.
    ![Screenshot of private endpoint connections blade with a highlight on one private endpoint name](media/private-endpoint/pec-click.png)
    4. In Overview, select the network interface.
    ![Screenshot of private endpoint connection overview with a highlight on network interface](media/private-endpoint/pec-nic-click.png)
    5. In Overview, Private IP address is shown in the Essentials section.
![Screenshot of private endpoint connection's network interface with a highlight on its private IP address](media/private-endpoint/pec-ip-display.png)
2. [Create a private Azure DNS zone](../../dns/private-dns-getstarted-portal.md#create-a-private-dns-zone) named _anything but_ privatelink.\<dns-zone\>.database.windows.net; for example: `privatelink.site`.
    > [!IMPORTANT]
    > Do not name the private DNS zone `privatelink.<dns-zone>.database.windows.net` because this will disrupt the instance's internal connectivity and cause management operations to fail.
3. [Link the private DNS zone to the endpoint virtual network](../../dns/private-dns-getstarted-portal.md#link-the-virtual-network).
4. In the DNS zone, create a new record set with the following values:
   - Name: `<instance-name>`
   - Type: A
   - IP address: IP address of the private endpoint obtained in step 1.

## Next steps

- Learn about the [Connectivity architecture of Azure SQL Managed Instance](connectivity-architecture-overview.md).
- Read more about [Azure Private Link](../../private-link/private-link-overview.md) and [private endpoints](../../private-link/private-endpoint-overview.md).
- See the list of Azure PaaS services compatible with Private Link at [Azure Private Link availability](../../private-link/availability)
