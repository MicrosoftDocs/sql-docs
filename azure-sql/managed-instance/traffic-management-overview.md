---
title: Traffic Management
titleSuffix: Azure SQL Managed Instance
description: Learn how Azure SQL Managed Instance separates traffic into user-managed and service-managed planes.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma, bonova
ms.date: 12/03/2025
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: concept-article
ms.custom:
  - fasttrack-edit
  - sfi-image-nochange
---

# Traffic management in Azure SQL Managed Instance

This article describes how [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) manages the difference in traffic between user-managed and service-managed traffic. 

## Overview

As a Platform as a Service (PaaS), SQL Managed Instance manages its network traffic. In a traditional on-premises SQL Server environment, regular traffic updates, maintenance, and monitoring usually operate over the same network as client data. In SQL Managed Instance, these flows route over internal Azure networking to ensure automated, seamless, secure, and monitored service management.

Internal traffic, termed _service-managed traffic_, is distinguished from user traffic (known as _user-managed traffic_) to ensure that service operations don't interfere with user workloads and vice versa. Traffic separates into these two categories, as the following diagram illustrates:

:::image type="content" source="media/traffic-management-overview/traffic-management-overview.png" alt-text="Diagram showing the separation of user-managed and service-managed traffic in Azure SQL Managed Instance." lightbox="media/traffic-management-overview/traffic-management-overview.png":::

## User-managed traffic

User-managed traffic is established on demand between SQL clients or applications and Azure SQL Managed Instance. It comprises:

- All inbound traffic directed to SQL managed instance endpoints within your virtual network
- All outbound traffic that originates from SQL managed instances within your virtual network

User-managed traffic originates from, and terminates in, virtual networks owned by the user of the SQL Managed Instance, which makes it subject to the network configuration of that virtual network. This means that standard network control and configuration mechanisms also apply to the SQL managed instance's traffic, including firewalls, network security group rules, route tables, domain name resolution, and more. Therefore, it's the responsibility of SQL Managed Instance users to ensure that user-managed traffic can actually reach its intended destination. This responsibility is especially important for traffic to the [VNet-local endpoint](connectivity-architecture-overview.md#vnet-local-endpoint). Outbound traffic that originates from SQL managed instances is similarly controlled.

### How is user-managed traffic secured?

Because user-managed traffic flows through the virtual network you own and manage, it is subject to the array of security, auditing, monitoring, and observability tools available in Azure. For more information on best practices and controls available, see:

* [Secure your Azure SQL Managed Instance](secure-managed-instance.md)
* [An overview of Azure SQL Managed Instance security capabilities](../database/security-overview.md)

## Service-managed traffic

SQL Managed Instance uses service-managed traffic to perform housekeeping activities, such as taking regular backups, emitting service telemetry, and receiving software updates.

Service-managed traffic includes two planes:
- Control plane
- Internal data plane

### Control plane

The control plane consists of components that govern the configuration and behavior of the PaaS service, such as retrieving and storing service health information, monitoring, scaling, and deployment. For example, when you deploy a new instance, components in the control plane orchestrate the provisioning of resources and their configuration. Control plane traffic is responsible for the data component of these behaviors.

### Internal data plane

Internal data plane components orchestrate the operations on user data to ensure durability, high availability, and business continuity of your data assets. In contrast to the control plane, which doesn't carry user data, internal data plane does transfer data stored in your databases. Similar to the control plane, endpoints and services within the internal data plane are managed and secured by Microsoft.

The internal data plane facilitates regular backups, replication traffic between availability replicas, data seeding, geo-replication in failover groups, and other data flows that involve actual user data. Examples of internal data plane traffic include:

- Traffic from the SQL managed instance to storage accounts where automated backups are kept.
- Traffic to telemetry endpoints.
-	Authentication and secret exchange with Microsoft Entra ID and Azure Key Vault.
- Traffic to virtual machine image download endpoints and the Windows Update service.
- Data transfer operations initiated and governed by the service, such as data backups, and some data replication between replicas.
- Fabric mirroring traffic.

### How is service-managed traffic secured?

Service-managed traffic is integral to the continuous operation of the PaaS and to achieve service level objectives (SLO). For this reason, Microsoft ensures that this traffic is uninterrupted, continuously available, monitored, and secured.

Network traffic between components in the control plane and in the internal data plane is encrypted. All connections are authenticated, and operations are authorized following the principle of least privilege.

L3/L4 firewalls protect internal network environments where control and internal data components reside. These firewalls allow inbound and outbound traffic only to and from known sources and destinations.

When other Azure services participate in service-managed traffic (such as Azure Storage or Azure Key Vault), SQL managed instance accesses them through network-local access mechanisms, such as Azure Private Link and service endpoints, to minimize the scope of network exposure. Authentication and authorization are performed through Microsoft-owned principals unique to each customer. These principals are assigned strictly scoped permission sets in observance of the principle of least privilege.

To learn more about the security capabilities of Azure SQL Managed Instance, see:

* [Security Overview](../database/security-overview.md)
* [An overview of Azure SQL Managed Instance security capabilities](../database/security-overview.md)

## Related content

- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- To learn more, see:
  - [Virtual cluster architecture](virtual-cluster-architecture.md).
  - [Connectivity architecture](connectivity-architecture-overview.md).
  - [Service-aided subnet configuration](subnet-service-aided-configuration-enable.md).
- Learn how to create a SQL managed instance:
  - From the [Azure portal](instance-create-quickstart.md).
  - By using [PowerShell](scripts/create-configure-managed-instance-powershell.md).
  - By using [an Azure Resource Manager template](/samples/azure/azure-quickstart-templates/sqlmi-new-vnet/).
  - By using [an Azure Resource Manager template with a jumpbox and SQL Server Management Studio](/samples/azure/azure-quickstart-templates/sqlmi-new-vnet-w-jumpbox/).
