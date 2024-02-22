---
title: Virtual cluster architecture
titleSuffix: Azure SQL Managed Instance
description: Learn about the architecture of the virtual cluster that hosts Azure SQL Managed Instance, which is based on an isolated set of virtual machines that form the cluster.
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma, zoranrilak
ms.date: 11/14/2023
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.custom: ignite-2023
ms.topic: conceptual
---

# Virtual cluster architecture - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes the architecture and operation management of the virtual cluster that hosts Azure SQL Managed Instance.

## Overview

Azure SQL Managed Instance is a single-tenant, platform-as-a-service (PaaS) made up of service components that are hosted on a _dedicated set of isolated virtual machines_ and joined to a virtual cluster. These dedicated sets of virtual machines are placed into virtual machine groups based on similar instance configuration attributes, such as hardware generation and maintenance windows. One or more instances can be in a virtual machine group, and one or more virtual machine groups form a virtual cluster. A virtual cluster automatically expands or contracts as needed to accommodate new and removed instances. 

Each virtual cluster is associated with one subnet and _automatically deployed_ when the first SQL managed instance in a subnet is created. Likewise, a virtual cluster is _automatically removed_ when the last instance in a subnet is deleted, leaving the subnet empty and ready to be removed. The virtual cluster connects the subnet to the managed instances deployed inside that subnet. A [service association link (SAL)](/rest/api/virtualnetwork/service-association-links) is used to establish the association between a subnet and the cluster. 

The following diagram shows the conceptual layout of the virtual cluster:

:::image type="content" source="media/virtual-cluster-architecture/sql-managed-instance-virtual-cluster-architecture.png" border="false" alt-text="Diagram that shows the virtual cluster architecture for Azure SQL Managed Instance.":::


## Role in management operations

The role of the virtual cluster in [management operations](management-operations-overview.md) is to find appropriate compute resources for the operation, as well as manage the resources within the cluster, such as the virtual machines that create the instance, and the virtual machine groups.  Management operations include creating new instances, as well as deleting, or modifying the configuration of, existing instances. The virtual cluster expands, shrinks, or deletes existing virtual machine groups, or creates new virtual machine groups, depending on the operation.

Since virtual machine groups are defined by similar instance configuration attributes (such as hardware generation and maintenance windows), making changes to any of these attributes for an instance triggers the virtual cluster to perform an action to the virtual machine groups that form it. Actions triggered by management operations include creating new or deleting existing virtual machines, and virtual machine groups, as well as expanding existing groups and moving instances between groups. If all virtual machines are removed out of a group, the virtual cluster also deletes the virtual machine group. 

For example, if you change the hardware generation of an instance, the virtual cluster creates a new virtual machine group for the hardware generation if one doesn't already exist, and moves the instance to that group. 

The duration of virtual group change operations depends on the operation type. For more information, see [SQL Managed Instance management operations](management-operations-overview.md#duration).

## Number of virtual machine groups

The number of virtual machine groups in a virtual cluster depends on the following:
- The number of different [hardware generation configurations](service-tiers-managed-instance-vcore.md#hardware-configurations)
- The number of different [maintenance window configurations](maintenance-window.md)
- Limits of the virtual machine group size (which are defined at the compute layer and are subject to change)


You can determine the number of virtual machine groups in a virtual cluster by multiplying the number of different hardware generation configurations by the number of different maintenance window configurations in your subnet. For example, if you have two hardware generation configurations (such as one Standard-series and one Premium-series instance) and two different maintenance window configurations, the virtual cluster has four virtual machine groups. 

SQL Managed Instance supports three different [hardware generation configurations](service-tiers-managed-instance-vcore.md#hardware-configurations) and three different [maintenance window configurations](maintenance-window.md). Therefore, the minimum number of virtual machine groups in a virtual cluster is 1 (one hardware generation configuration, one maintenance window configuration), and the maximum is 9 (three different hardware generation configurations, three different maintenance window configurations).

> [!IMPORTANT]
> Since there is a limit to the number of virtual machines that can join a group, a lack of space in an existing group can result in creating a virtual machine group with identical specifications. It's possible for a subnet with a large number of instances to have multiple machine groups with the same configuration, and exceed 9 virtual machine groups.



## Role in IP address usage

The built-in high availability of Azure SQL Managed Instance is implemented with [Azure Service Fabric](/azure/service-fabric/service-fabric-overview). A Service Fabric cluster is a network-connected set of virtual or physical machines. Each machine or VM that is part of a Service Fabric cluster is called a cluster node, and each node reserves one IP address. As such, _each virtual machine_ in the dedicated set of VMs that create a SQL managed instance is considered a node in the Service Fabric cluster.  The virtual cluster that hosts one or more SQL managed instances assigns IP addresses to each VM to form a Service Fabric cluster for high availability. 

Since the virtual cluster is responsible for assigning IP addresses to the virtual machines inside it, and each virtual cluster is associated with a single subnet, you have to carefully consider the number of instances that you expect to deploy into the subnet when determining an appropriate size for the subnet. 

When determining an appropriate size for the subnet where you'll deploy your managed instances, take into account: 
- The number of instances that you expect to deploy into the subnet
- The number of different virtual machine groups that you expect in the subnet

To learn more, see [determine required subnet size and range for Azure SQL Managed Instance](vnet-subnet-determine-size.md).


## DNS synchronization

The virtual cluster synchronizes DNS server settings changes in a virtual network that hosts existing SQL managed instances. The virtual cluster triggers the synchronization and propagates it to the instances inside the cluster. For more information, see [resolve private domain names in Azure SQL Managed Instance](resolve-private-domain-names.md).


## Delete a subnet after deleting an Azure SQL Managed Instance

Before deleting a subnet used for SQL managed instances, the subnet needs to be empty. Since virtual clusters are automatically created when the first instance in the subnet is created, and automatically deleted when the last instance in the subnet is deleted, you first need to delete all instances in the subnet before you can delete the subnet. 

> [!IMPORTANT]
> - Creating and deleting the virtual cluster is automatic and requires no manual action past creating the first instance or deleting the last instance in a subnet. 
> - Deleting a virtual cluster is a [long-running operation that can last up to 1.5 hours](management-operations-overview.md). The virtual cluster will still be visible in the portal until deleting the virtual cluster completes.

In rare circumstances, creating an instance fails and results in an empty virtual cluster. Additionally, since you can cancel [creating an instance](management-operations-cancel.md), it's possible for a virtual cluster to be deployed with instances in a failed to deploy state. Empty virtual clusters, or clusters with instances that have failed to deploy, are automatically removed in the background, and there are no charges associated with these clusters. 




## Next steps

- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md).
- Learn how to [set up a new Azure virtual network](virtual-network-subnet-create-arm-template.md) or an [existing Azure virtual network](vnet-existing-add-subnet.md) where you can deploy SQL Managed Instance.
- [Calculate the size of the subnet](vnet-subnet-determine-size.md) where you want to deploy SQL Managed Instance.
- Learn how to create a managed instance:
  - From the [Azure portal](instance-create-quickstart.md).
  - By using [PowerShell](scripts/create-configure-managed-instance-powershell.md).
  - By using [an Azure Resource Manager template](https://azure.microsoft.com/resources/templates/sqlmi-new-vnet/).
  - By using [an Azure Resource Manager template with a jumpbox and SQL Server Management Studio](https://azure.microsoft.com/resources/templates/sqlmi-new-vnet-w-jumpbox/).
