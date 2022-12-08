---
title: Secure Azure SQL Managed Instance public endpoints
description: "Securely use public endpoints in Azure SQL Managed Instance"
author: srdan-bozovic-msft
ms.author: srbozovi
ms.reviewer: vanto
ms.date: 05/08/2019
ms.service: sql-managed-instance
ms.subservice: security
ms.topic: conceptual
ms.custom: sqldbrb=1
---
# Use Azure SQL Managed Instance securely with public endpoints
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

Azure SQL Managed Instance can provide user connectivity over [public endpoints](public-endpoint-configure.md). This article explains how to make this configuration more secure.

## Scenarios

Azure SQL Managed Instance provides a VNet-local endpoint](connectivity-architecture-overview.md#vnet-local-endpoint) to allow connectivity from inside its virtual network. The default option is to provide maximum isolation. However, there are scenarios where you need to provide a public endpoint connection:

- The managed instance must integrate with multi-tenant-only platform-as-a-service (PaaS) offerings.
- You need higher throughput of data exchange than is possible when you're using a VPN.
- Company policies prohibit PaaS inside corporate networks.

## Deploy a managed instance for public endpoint access

Although not mandatory, the common deployment model for a managed instance with public endpoint access is to create the instance in a dedicated isolated virtual network. In this configuration, the virtual network is used only for virtual cluster isolation. It doesn't matter if the managed instance's IP address space overlaps with a corporate network's IP address space.

## Secure data in motion

SQL Managed Instance data traffic is always encrypted if the client driver supports encryption. Data sent between the managed instance and other Azure virtual machines or Azure services never leaves Azure's backbone. If there's a connection between the managed instance and an on-premises network, we recommend you use Azure ExpressRoute. ExpressRoute helps you avoid moving data over the public internet. For managed instance local connectivity, only private peering can be used.

## Lock down inbound and outbound connectivity

The following diagram shows the recommended security configurations:

![Security configurations for locking down inbound and outbound connectivity](./media/public-endpoint-overview/managed-instance-vnet.png)

A managed instance has a public endpoint address that is dedicated to a customer. This endpoint shares the IP address with the [management endpoint](management-endpoint-find-ip-address.md) but uses a different port. Similar to a VNet-local endpoint, the public endpoint may change after certain management operations. Always determine the public endpoint address by resolving the endpoint FQDN record, such as, for example, when configuring application-level firewall rules. 

To ensure traffic to the managed instance is coming from trusted sources, we recommend connecting from sources with well-known IP addresses. Use a network security group to limit access to the managed instance public endpoint on port 3342.

When clients need to initiate a connection from an on-premises network, make sure the originating address is translated to a well-known set of IP addresses. If you can't do so (for example, a mobile workforce being a typical scenario), we recommend you use [point-to-site VPN connections and a VNet-local endpoint](point-to-site-p2s-configure.md).

If connections are started from Azure, we recommend that traffic come from a well-known assigned [virtual IP address](/previous-versions/azure/virtual-network/virtual-networks-reserved-public-ip) (for example, a virtual machine). To make managing virtual IP (VIP) addresses easier, you might want to use [public IP address prefixes](/azure/virtual-network/ip-services/public-ip-address-prefix).

## Next steps

- Learn how to configure public endpoint for manage instances: [Configure public endpoint](public-endpoint-configure.md)
