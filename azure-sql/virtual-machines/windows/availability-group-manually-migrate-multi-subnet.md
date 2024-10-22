---
title: "Migrate SQL Server availability group from a single subnet to a multi-subnet"
description: "Learn how to migrate a SQL Server Always On availability group from a single subnet to a multiple subnet (multi-subnet) environment)."
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: mathoma, randolphwest
ms.date: 06/18/2024
ms.service: azure-vm-sql-server
ms.subservice: hadr
ms.topic: how-to
editor: monicar
tags: azure-service-management
---

# Migrate SQL Server availability group to multi-subnets - SQL Server on Azure VMs

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article teaches you to migrate your Always On availability group (AG) from a single subnet to multiple subnets to simplify connecting to your listener in Azure with your SQL Server on Azure virtual machines (VMs).  

[!INCLUDE[tip-for-multi-subnet-ag](../../includes/virtual-machines-ag-listener-multi-subnet.md)]

## Overview 

Customers who are running SQL Server on Azure virtual machines can implement an Always On availability group (AG) in either a single subnet or multiple subnets (multi-subnet). A multi-subnet configuration simplifies the availability group environment by removing the need for an Azure Load Balancer or a Distributed Network Name (DNN) to route traffic to the listener on the Azure network. While using a multi-subnet approach is recommended, it requires the connection strings for an application to use `MultiSubnetFailover = true`, which might not be possible immediately due to application-level changes.

If you originally created an availability group in a single subnet and are using an Azure Load Balancer or DNN for the listener and now want to reduce complexity by moving to a multi-subnet configuration, you can do so with some manual steps.

Prior to starting a migration of an existing environment, weigh the risks of changing an in-use environment. 

Consider the following two ways to migrate your availability group to multiple subnets: 

- Create a new environment to perform side-by-side testing
- Manually move an existing availability group

> [!CAUTION]
> Performing any migration involves some risk, so as always test thoroughly in a non-production environment before moving to a production environment.


## New environment with side-by-side testing

The first method to move to a multi-subnet availability group is to set up a new environment. If this is the chosen route, then you need to: 

1. Create new virtual machines
1. Create a new availability group in a multi-subnet configuration
1. Backup your current database and restore them to the new environment

Initially in the new multi-subnet environment, create the listener with a different name than the existing single subnet environment. A newly named listener in a new availability group allows for side-by-side testing of the application (testing with both the multi-subnet and the current load balancer or DNN in place).

Once the multi-subnet environment is thoroughly validated, then you could cut over to the new infrastructure. Depending on the environment (production, test), use a maintenance window to complete the change. During the maintenance window, restore the database to the new primary replica, remove the availability group listener in both environments, and then recreate the listener in the multi-subnet environment using the same name as the previous listener, the one used in the application connection string. 

Setting up a new environment in a [multi-subnet configuration is now easier with the Azure portal deployment experience](availability-group-azure-portal-configure.md).

## Manually move an existing availability group

The other option is to manually move from the single subnet environment to a multi-subnet environment. In order to migrate using this method, you need the following prerequisites: 

- An IP address for each machine in a new subnet
- Connection strings already using `MultiSubnetFailover = true`

To migrate your availability group to a multi-subnet configuration, follow these steps: 

1. Create a new subnet for each secondary, as all virtual machines are currently in the same subnet.

1. Determine the Cluster IP and Listener IP for all servers in the AG. For example, if you have an availability group with two nodes, you have the following:

    | VM Name | Subnet | Cluster IP | Listener IP |
    |----- |----- |----- |----- |
    | VM1 (primary) | 10.1.1.0/24 (existing subnet) | 10.1.1.15 | 10.1.1.16 |
    | VM2 (secondary) | 10.1.2.0/24 (new subnet) | 10.1.2.15 | 10.1.2.16 |

1. Add the Cluster IP and Listener IP to the [primary replica server](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md?#add-secondary-ips-to-sql-server-vms). Adding these IP addresses is an online operation.

1. In the Azure portal, move the secondary server to the new subnet by going to the virtual machine > **Networking > Network Interface > IP Configurations**. Moving the server to a new subnet restarts the secondary replica server.

1. Add the Cluster IP and the Listener IP to the [secondary replica server](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md?#add-secondary-ips-to-sql-server-vms). Adding these IP addresses is an online operation.

1. At this point, since the IP addresses and subnets are in place, so you can delete the load balancer.

1. Drop the listener. 

1. If you're using Windows Server 2019 and later versions, skip this step. If you're using Windows Server 2016, manually add the [cluster IPs to the FCI](availability-group-manually-configure-tutorial-multi-subnet.md?#set-the-failover-cluster-ip-address).

1. Recreate the listener with the new listener IPs.

1. Flush DNS on all servers using ipconfig `/flushdns`.

## Next steps

- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Overview of Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md)
