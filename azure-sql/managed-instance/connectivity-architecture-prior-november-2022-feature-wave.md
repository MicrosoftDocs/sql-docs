---
title: Connectivity architecture prior to November 2022 Feature Wave
titleSuffix: Azure SQL Managed Instance
description: Learn about Azure SQL Managed Instance communication and connectivity architecture prior to the November 2022 Feature Wave.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma, bonova
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: conceptual
ms.custom:
---

# Connectivity architecture for Azure SQL Managed Instance prior to November 2022
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

November 2022 introduced a number of changes to the underlying connectivity architecture for Azure SQL Managed Instance. This article provides information about the connectivity structure for instances that have not enrolled in the feature wave. 

For information about the connectivity architecture for instances that have enrolled in the feature wave, see [Connectivity architecture](connectivity-architecture-overview.md). For more information about the feature wave, review [November 2022 feature wave](doc-changes-updates-release-notes-whats-new.md#november-2022-feature-wave). 


## Communication overview

The following diagram shows entities that connect to SQL Managed Instance. It also shows the resources that need to communicate with a managed instance. The communication process at the bottom of the diagram represents customer applications and tools that connect to SQL Managed Instance as data sources.  

![Diagram showing entities in connectivity architecture for SQL Managed Instance.](./media/connectivity-architecture-prior-november-2022-feature-wave/01-connectivity-architecture-entitites.png)

SQL Managed Instance is a single-tenant Platform-as-a-Service (PaaS) offering. Its compute and networking elements are deployed inside the customer's subnet, and it is typically accessed via its VNet-local endpoint](connectivity-architecture-overview.md#vnet-local-endpoint). SQL Managed Instance depends on Azure services such as Azure Storage, Azure Active Directory (Azure AD), Azure Key Vault, Event Hubs, and telemetry collection services. Customers will observe traffic to those services originating from subnets containing SQL Managed Instance.

Deployment, management and core service maintenance operations are carried out via automated agents. These agents have exclusive access to the compute resources operating the service: it is not possible to `ssh` or RDP to those hosts. All internal communications are encrypted and signed using certificates. To check the trustworthiness of communicating parties, SQL Managed Instance constantly verifies these certificates through certificate revocation lists.

## High-level connectivity architecture

![Diagram showing the connectivity architecture of Azure SQL Managed Instance.](./media/connectivity-architecture-prior-november-2022-feature-wave/02-connectivity-architecture-sql-managed-instance.png)


## Management endpoint

To facilitate the communication between the control plane and components deployed inside the customer's subnet, Azure SQL Managed Instances not participating in the November 2022 Feature Wave employ a management endpoint. This means that elements of the virtual network's infrastructure can harm management traffic by making the instance fail and become unavailable.  Management and deployment services connect to SQL Managed Instance's management endpoint that maps to an external load balancer. Traffic is routed to the nodes only if it's received on a predefined set of ports that only the management components of SQL Managed Instance use. A built-in firewall on the nodes is set up to allow traffic only from Microsoft IP ranges. Certificates mutually authenticate all communication between management components and the management plane.

> [!NOTE]
> Traffic that goes to Azure services that are inside the SQL Managed Instance region is optimized and for that reason not NATed to the public IP address for the management endpoint. For that reason if you need to use IP-based firewall rules, most commonly for storage, the service needs to be in a different region from SQL Managed Instance.

The Azure SQL Managed Instance [mandatory inbound security rules](connectivity-architecture-overview.md#mandatory-security-rules-with-service-aided-subnet-configuration) require management ports 9000, 9003, 1438, 1440, and 1452 to be open from **Any source** on the Network Security Group (NSG) that protects SQL Managed Instance. Although these ports are open at the NSG level, they are protected at the network level by the built-in firewall.

The management endpoint is protected by a built-in firewall on the network level. On the application level, it is protected by mutual certificate verification. When connections start inside SQL Managed Instance (as with backups and audit logs), traffic appears to start from the management endpoint's public IP address. 


## Virtual cluster connectivity architecture

![Diagram showing connectivity architecture of the virtual cluster for SQL Managed Instance.](./media/connectivity-architecture-prior-november-2022-feature-wave/03-connectivity-architecture-virtual-cluster.png)

## Mandatory inbound security rules with service-aided subnet configuration

These rules are necessary to ensure inbound management traffic flow. See [paragraph above](#high-level-connectivity-architecture) for more information on connectivity architecture and management traffic.

| Name       |Port                        |Protocol|Source           |Destination|Action|
|------------|----------------------------|--------|-----------------|-----------|------|
|management  |9000, 9003, 1438, 1440, 1452|TCP     |SqlManagement    |MI SUBNET  |Allow |
|            |9000, 9003                  |TCP     |CorpnetSaw       |MI SUBNET  |Allow |
|            |9000, 9003                  |TCP     |CorpnetPublic    |MI SUBNET  |Allow |
|mi_subnet   |Any                         |Any     |MI SUBNET        |MI SUBNET  |Allow |
|health_probe|Any                         |Any     |AzureLoadBalancer|MI SUBNET  |Allow |

## Mandatory outbound security rules with service-aided subnet configuration

These rules are necessary to ensure outbound management traffic flow. See [paragraph above](#high-level-connectivity-architecture) for more information on connectivity architecture and management traffic.

| Name       |Port          |Protocol|Source           |Destination|Action|
|------------|--------------|--------|-----------------|-----------|------|
|management  |443, 12000    |TCP     |MI SUBNET        |AzureCloud |Allow |
|mi_subnet   |Any           |Any     |MI SUBNET        |MI SUBNET  |Allow |

## Mandatory user defined routes with service-aided subnet configuration

These routes are necessary to ensure that management traffic is routed directly to a destination. See [paragraph above](#high-level-connectivity-architecture) for more information on connectivity architecture and management traffic.

|Name|Address prefix|Next hop <sup>2</sup>|
|----|--------------|-------|
|subnet-to-vnetlocal|MI SUBNET <sup>1</sup>|Virtual network|
|mi-azurecloud-REGION-internet|AzureCloud.REGION|Internet|
|mi-azurecloud-REGION_PAIR-internet|AzureCloud.REGION_PAIR|Internet|
|mi-azuremonitor-internet|AzureMonitor|Internet|
|mi-corpnetpublic-internet|CorpNetPublic|Internet|
|mi-corpnetsaw-internet|CorpNetSaw|Internet|
|mi-eventhub-REGION-internet|EventHub.REGION|Internet|
|mi-eventhub-REGION_PAIR-internet|EventHub.REGION_PAIR|Internet|
|mi-sqlmanagement-internet|SqlManagement|Internet|
|mi-storage-internet|Storage|Internet|
|mi-storage-REGION-internet|Storage.REGION|Internet|
|mi-storage-REGION_PAIR-internet|Storage.REGION_PAIR|Internet|
|mi-azureactivedirectory-internet|AzureActiveDirectory|Internet|


## Next steps

For more information about SQL Managed Instance and connectivity, see [Azure SQL Managed Instance connectivity architecture](connectivity-architecture-overview.md).
