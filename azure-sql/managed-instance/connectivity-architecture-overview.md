---
title: Connectivity architecture
titleSuffix: Azure SQL Managed Instance
description: Learn about Azure SQL Managed Instance communication and connectivity architecture and how the components direct traffic for a managed instance.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma, bonova
ms.date: 07/10/2024
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: conceptual
ms.custom:
  - fasttrack-edit
  - build-2023
  - build-2023-dataai
  - ignite-2023
---

# Connectivity architecture for Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes the connectivity architecture in Azure SQL Managed Instance and how components direct communication traffic for a managed instance.  

## Overview

In SQL Managed Instance, an instance is placed inside the Azure virtual network and inside the subnet that's dedicated to managed instances. The deployment provides:

- A secure virtual network-local (VNet-local) IP address.
- The ability to connect an on-premises network to SQL Managed Instance.
- The ability to connect SQL Managed Instance to a linked server or to another on-premises data store.
- The ability to connect SQL Managed Instance to Azure resources.

## High-level connectivity architecture

SQL Managed Instance is made of up service components hosted on a dedicated set of isolated virtual machines that are grouped together by similar configuration attributes and joined to a [virtual cluster](virtual-cluster-architecture.md). Some service components are deployed inside the customer's virtual network subnet while other services operate within a secure network environment that Microsoft manages.

:::image type="content" source="media/connectivity-architecture-overview/2-connectivity-architecture-diagram-sql-managed-instance.png" alt-text="Diagram that shows the high-level connectivity architecture for Azure SQL Managed Instance after November 2022." lightbox="media/connectivity-architecture-overview/2-connectivity-architecture-diagram-sql-managed-instance.png":::

Customer applications can connect to SQL Managed Instance and can query and update databases inside the virtual network, peered virtual network, or network connected by VPN or Azure ExpressRoute.

The following diagram shows entities that connect to SQL Managed Instance. It also shows the resources that need to communicate with a managed instance. The communication process at the bottom of the diagram represents customer applications and tools that connect to SQL Managed Instance as data sources.  

:::image type="content" source="media/connectivity-architecture-overview/1-connectivity-architecture-diagram-entities.png" alt-text="Diagram that shows entities in the connectivity architecture for Azure SQL Managed Instance after November 2022." lightbox="media/connectivity-architecture-overview/1-connectivity-architecture-diagram-entities.png":::

SQL Managed Instance is a single-tenant, platform as a service offering that operates in two planes: a data plane and a control plane.

The *data plane* is deployed inside the customer's subnet for compatibility, connectivity, and network isolation. Data plane depends on Azure services like Azure Storage, Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) for authentication, and telemetry collection services. You'll see traffic that originates in subnets that contain SQL Managed Instance going to those services.

The *control plane* carries the deployment, management, and core service maintenance functions via automated agents. These agents have exclusive access to the compute resources that operate the service. You can't use `ssh` or Remote Desktop Protocol to access those hosts. All control plane communications are encrypted and signed by using certificates. To check the trustworthiness of communicating parties, SQL Managed Instance constantly verifies these certificates by using certificate revocation lists.


## Communication overview

Applications can connect to SQL Managed Instance via three types of endpoints. These endpoints serve different scenarios and exhibit distinct network properties and behaviors.

- [VNet-local endpoint](#vnet-local-endpoint)
- [Public endpoint](#public-endpoint)
- [Private endpoints](#private-endpoints)

:::image type="content" source="media/connectivity-architecture-overview/4-connectivity-architecture-endpoints.png" alt-text="Diagram that shows the scope of visibility for VNet-local, public, and private endpoints to an Azure SQL Managed Instance." lightbox="media/connectivity-architecture-overview/4-connectivity-architecture-endpoints.png":::

### VNet-local endpoint

The VNet-local endpoint is the default means to connect to SQL Managed Instance. The VNet-local endpoint is a domain name in the form of `<mi_name>.<dns_zone>.database.windows.net` that resolves to an IP address from the subnet's address pool; hence **VNet-local**, or an endpoint that is local to the virtual network. The VNet-local endpoint can be used to connect to a SQL Managed Instance in all standard connectivity scenarios.

VNet-local endpoints support both [proxy and redirect connection types](connection-types-overview.md).

When connecting to the VNet-local endpoint, always use its domain name as the underlying IP address can occasionally change.

### Public endpoint

The public endpoint is an optional domain name in the form of `<mi_name>.public.<dns_zone>.database.windows.net` that resolves to a public IP address reachable from the Internet. Public endpoint allows TDS traffic only to reach SQL Managed Instance on port 3342 and can't be used for integration scenarios, such as failover groups, Managed Instance link, and similar technologies.

When connecting to the public endpoint, always use its domain name as the underlying IP address can occasionally change.

Public endpoint always uses the [proxy connection type](connection-types-overview.md) regardless of the connection type setting.

Learn how to set up a public endpoint in [Configure public endpoint for Azure SQL Managed Instance](public-endpoint-configure.md).

### Private endpoints

A private endpoint is an optional fixed IP address in another virtual network that conducts traffic to your SQL managed instance. One Azure SQL Managed Instance can have multiple private endpoints in multiple virtual networks. Private endpoints allow TDS traffic only to reach SQL Managed Instance on port 1433 and can't be used for integration scenarios, such as failover groups, Managed Instance link, and other similar technologies.

When connecting to a private endpoint, always use the domain name since connecting to Azure SQL Managed Instance via its IP address isn't supported yet.

Private endpoints always uses the [proxy connection type](connection-types-overview.md) regardless of the connection type setting.

Learn more about private endpoints and how to configure them in [Azure Private Link for Azure SQL Managed Instance](private-endpoint-overview.md).

## Virtual cluster connectivity architecture

The following diagram shows the conceptual layout of the [virtual cluster architecture](virtual-cluster-architecture.md):

:::image type="content" source="media/connectivity-architecture-overview/3-connectivity-architecture-diagram-virtual-cluster.png" alt-text="Diagram that shows the virtual cluster connectivity architecture for Azure SQL Managed Instance." lightbox="media/connectivity-architecture-overview/3-connectivity-architecture-diagram-virtual-cluster.png":::

The domain name of the VNet-local endpoint resolves to the private IP address of an internal load balancer. Although this domain name is registered in a public Domain Name System (DNS) zone and is publicly resolvable, its IP address belongs to the subnet's address range and can only be reached from inside its virtual network by default.

The load balancer directs traffic to a SQL Managed Instance gateway. Because multiple managed instances can run inside the same cluster, the gateway uses the SQL Managed Instance host name as seen in the connection string to redirect traffic to the correct SQL engine service.

The value for `dns-zone` is automatically generated when you create the cluster. If a newly created cluster hosts a secondary managed instance, it shares its zone ID with the primary cluster.

## Network requirements

Azure SQL Managed Instance requires aspects of the delegated subnet to be configured in specific ways, which you can achieve by using the [service-aided subnet configuration](#service-aided-subnet-configuration). Beyond what the service requires, users have full control over their subnet network configuration, such as: 

- Allowing or blocking traffic on some or all the ports 
- Adding entries to the route table to route traffic through virtual network appliances or a gateway
- Configuring custom DNS resolution, or
- Setting up peering or a VPN

The subnet in which SQL Managed Instance is deployed must meet the following requirements:

- **Dedicated subnet**: The subnet SQL Managed Instance uses can be delegated only to the SQL Managed Instance service. The subnet can't be a gateway subnet, and you can deploy only SQL Managed Instance resources in the subnet.
- **Subnet delegation**: The SQL Managed Instance subnet must be delegated to the `Microsoft.Sql/managedInstances` resource provider.
- **Network security group**: A network security group must be associated with the SQL Managed Instance subnet. You can use a network security group to control access to the SQL Managed Instance data endpoint by filtering traffic on port 1433 and ports 11000-11999 when SQL Managed Instance is configured for redirect connections. The service automatically provisions [rules](subnet-service-aided-configuration-enable.md#mandatory-security-rules-and-routes) and keeps them current as required to allow uninterrupted flow of management traffic.
- **Route table**: A route table must be associated with the SQL Managed Instance subnet. You can add entries to this route table, for example to route traffic to premises through a virtual network gateway, or to add the [default 0.0.0.0/0 route](/azure/virtual-network/virtual-networks-udr-overview#default-route) directing all traffic through a virtual network appliance such as a firewall. Azure SQL Managed Instance automatically provisions and manages [its required entries](subnet-service-aided-configuration-enable.md#mandatory-security-rules-and-routes) in the route table.
- **Sufficient IP addresses**: The SQL Managed Instance subnet must have at least 32 IP addresses. For more information, see [Determine the size of the subnet for SQL Managed Instance](vnet-subnet-determine-size.md). You can deploy managed instances in the [existing network](vnet-existing-add-subnet.md) after you configure it to satisfy the [networking requirements for SQL Managed Instance](#network-requirements). Otherwise, create a [new network and subnet](virtual-network-subnet-create-arm-template.md).
- **Allowed by Azure policies**: If you use [Azure Policy](/azure/governance/policy/overview) to prevent resource creation or modification in a scope that includes a SQL Managed Instance subnet or virtual network, your policies must not prevent SQL Managed Instance from managing its internal resources. The following resources need to be excluded from policy deny effects for normal operation:
  - Resources of type `Microsoft.Network/serviceEndpointPolicies`, when resource name begins with `\_e41f87a2\_`
  - All resources of type `Microsoft.Network/networkIntentPolicies`
  - All resources of type `Microsoft.Network/virtualNetworks/subnets/contextualServiceEndpointPolicies`
- **Locks on virtual network**: [Locks](/azure/azure-resource-manager/management/lock-resources) on the dedicated subnet's virtual network, its parent resource group, or subscription, might occasionally interfere with SQL Managed Instance management and maintenance operations. Take special care when you use resource locks.
- **Replication traffic**: Replication traffic for failover groups between two managed instances should be direct and not routed through a hub network.
- **Custom DNS server:** If the virtual network is configured to use a custom DNS server, the DNS server must be able to resolve public DNS records. Using features like Microsoft Entra authentication might require resolving more fully qualified domain names (FQDNs). For more information, see [Resolving private DNS names in Azure SQL Managed Instance](resolve-private-domain-names.md).

## Service-aided subnet configuration

To improve service security, manageability, and availability, SQL Managed Instance uses service-aided subnet configuration and network intent policy on the Azure virtual network infrastructure to configure the network, associated components, and route table to ensure that minimum requirements for SQL Managed Instance are met.

Automatically configured network security and route table rules are visible to the customer and annotated with one of these prefixes:

- `Microsoft.Sql-managedInstances_UseOnly_mi-` for mandatory rules and routes
- `Microsoft.Sql-managedInstances_UseOnly_mi-optional-` for optional rules and routes

For additional details, review [service-aided subnet configuration](subnet-service-aided-configuration-enable.md).

For more information about the connectivity architecture and management traffic, see [High-level connectivity architecture](#high-level-connectivity-architecture).

## Networking constraints

- **TLS 1.2 is enforced on outbound connections**: Beginning in January 2020, Microsoft enforces TLS 1.2 for intra-service traffic in all Azure services. For SQL Managed Instance, this resulted in TLS 1.2 being enforced on outbound connections that are used for replication and on linked server connections to SQL Server. If you use a version of SQL Server that's earlier than 2016 with SQL Managed Instance, make sure that you apply [TLS 1.2-specific updates](https://support.microsoft.com/help/3135244/tls-1-2-support-for-microsoft-sql-server).

The following virtual network features are currently *not supported* with SQL Managed Instance:

- **Private subnets**: Deploying managed instances in private subnets (where [default outbound access](/azure/virtual-network/ip-services/default-outbound-access) is disabled) is currently not supported.
- **Database mail to external SMTP relays on port 25**: Sending [database mail](/sql/relational-databases/database-mail/configure-database-mail) via port 25 to external email services is only available to certain subscription types in Microsoft Azure. Instances on other subscription types should use a different port (for example, 587) to contact external SMTP relays. Otherwise, instances might fail to deliver database mail. For more information, see [Troubleshoot outbound SMTP connectivity problems in Azure](/azure/virtual-network/troubleshoot-outbound-smtp-connectivity).
- **Microsoft peering**: Enabling [Microsoft peering](/azure/expressroute/expressroute-faqs#microsoft-peering) on ExpressRoute circuits that are peered directly or transitively with a virtual network in which SQL Managed Instance resides affects traffic flow between SQL Managed Instance components inside the virtual network and services it depends on. Availability issues result. SQL Managed Instance deployments to a virtual network that already has Microsoft peering enabled are expected to fail.
- **Virtual network peering – global**: [Virtual network peering](/azure/virtual-network/virtual-network-peering-overview) connectivity across Azure regions doesn't work for instances of SQL Managed Instance that are placed in subnets that were created before September 9, 2020.
- **Virtual network peering – configuration**: When establishing virtual network peering between virtual networks that contain subnets with SQL Managed Instances, such subnets must use different route tables and network security groups (NSG). Reusing the route table and NSG in two or more subnets participating in virtual network peering will cause connectivity issues in all subnets using those route tables or NSG, and cause SQL Managed Instance's management operations to fail.
- **AzurePlatformDNS tag**: Using the AzurePlatformDNS [service tag](/azure/virtual-network/service-tags-overview) to block platform DNS resolution might render SQL Managed Instance unavailable. Although SQL Managed Instance supports customer-defined DNS for DNS resolution inside the engine, there's a dependency on platform DNS for platform operations.
- **NAT gateway**: Using [Azure Virtual Network NAT](/azure/virtual-network/nat-gateway/nat-overview) to control outbound connectivity with a specific public IP address renders SQL Managed Instance unavailable. The SQL Managed Instance service is currently limited to use the basic load balancer, which doesn't provide coexistence of inbound and outbound flows with Azure Virtual Network NAT.
- **IPv6 for Azure Virtual Network**: Deploying SQL Managed Instance to [dual stack IPv4/IPv6 virtual networks](/azure/virtual-network/ip-services/ipv6-overview) is expected to fail. Associating a network security group or a route table with user-defined routes (UDRs) that contains IPv6 address prefixes to a SQL Managed Instance subnet renders SQL Managed Instance unavailable. Also, adding IPv6 address prefixes to a network security group or UDR that's already associated with a managed instance subnet renders SQL Managed Instance unavailable. SQL Managed Instance deployments to a subnet with a network security group and UDR that already have IPv6 prefixes are expected to fail.
- **DNS records for reserved Microsoft services**: The following domain names are reserved and their resolution as defined in Azure DNS must not be overridden in a virtual network hosting managed instances: `windows.net`, `database.windows.net`, `core.windows.net`, `blob.core.windows.net`, `table.core.windows.net`, `management.core.windows.net`, `monitoring.core.windows.net`, `queue.core.windows.net`, `graph.windows.net`, `login.microsoftonline.com`, `login.windows.net`, `servicebus.windows.net`, and `vault.azure.net`. Deploying SQL Managed Instance to a virtual network in which one or more such domain names are overridden, either via [Azure DNS private zones](/azure/dns/private-dns-privatednszone) or by a custom DNS server, will fail. Overriding the resolution of these domains in a virtual network that contains a managed instance renders that managed instance unavailable. For information about how to configure Private Link DNS records inside a virtual network that contains managed instances, please see [Azure Private Endpoint DNS configuration](/azure/private-link/private-endpoint-dns).

## Related content

- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md).
- To learn more, see
    - [Virtual cluster architecture](virtual-cluster-architecture.md)
    - [Service-aided subnet configuration](subnet-service-aided-configuration-enable.md)
    - [Set up a new Azure virtual network](virtual-network-subnet-create-arm-template.md) or an [existing Azure virtual network](vnet-existing-add-subnet.md) where you can deploy SQL Managed Instance.
    - [Calculate the size of the subnet](vnet-subnet-determine-size.md) where you want to deploy SQL Managed Instance.
- Learn how to create a managed instance:
  - From the [Azure portal](instance-create-quickstart.md).
  - By using [PowerShell](scripts/create-configure-managed-instance-powershell.md).
  - By using [an Azure Resource Manager template](https://azure.microsoft.com/resources/templates/sqlmi-new-vnet/).
  - By using [an Azure Resource Manager template with a jumpbox and SQL Server Management Studio](https://azure.microsoft.com/resources/templates/sqlmi-new-vnet-w-jumpbox/).
