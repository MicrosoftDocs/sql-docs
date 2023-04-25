---
title: "Migrate SQL Server availability group from a single subnet to a multi-subnet"
description: "Learn how to migrate a SQL Server Always On Availability Group from a single subnet to a multi-subnet."
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: mathoma, randolphwest
ms.date: 04/20/2023
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: how-to
editor: monicar
tags: azure-service-management
---

# Migrate SQL Server availability group from a single subnet to a multi-subnet

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!TIP]
> There are many [methods to deploy an availability group](availability-group-overview.md#deployment-options). Simplify your deployment and eliminate the need for an Azure load balancer or distributed network name (DNN) for your Always On availability group by creating your SQL Server virtual machines (VMs) in [multiple subnets](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md) within the same Azure virtual network.

Customers who are running SQL Server on Azure virtual machines can implement an Always On availability group in either a single or multi-subnet. The multi-subnet configuration simplifies the experience by removing the need for an Azure Load Balancer or a DNN for the listener. While using a multi-subnet approach is recommended, it requires the connection strings for an application to use `MultiSubnetFailover = true`, which might not be immediately possible due to application level changes.

If you originally created an availability group in a single-subnet and are using an Azure Load Balancer or DNN for the listener and now want to reduce complexity by moving to a multi-subnet configuration, you can do so with some manual steps.

Prior to starting a migration of an existing environment, the risks of changing an environment in use should be weighed. There are two ways to move forward with a migration of this type.

1. Create a new environment to perform side-by-side testing
1. Manually move an existing availability group

## New environment with side-by-side testing

The first method to move to a multi-subnet availability group is to set up a new environment. If this route is chosen, then new virtual machines are created. Additionally, a new availability group in a multi-subnet configuration is set up, and finally a backup of your current database is restored to this environment.

Initially in this environment, the listener would have a different name from the existing environment. A new availability group allows for side-by-side testing of the application (testing with both the multi-subnet and the current load balanced AG in place).

Once the multi-subnet environment is thoroughly validated, then a cut over to the new infrastructure could take place. Depending on the environment (production, test) a maintenance window is taken to complete the change. During the maintenance window, restore the database to the new primary replica, drop the listener on both AGs (multi-subnet and load balancer), and lastly recreate the listener with the name for the connection string in the multi-subnet AG.

Setting up a new environment in a [multi-subnet configuration is now easier with the deployment experience](availability-group-azure-portal-configure.md) from the Azure portal.

## Manually move an existing availability group

The other option is to manually move from the load balancer to a multi-subnet. In order to go this route, there are a few prerequisites needed to migrate:

- An IP address for each machine in a new subnet
- Connection strings using `MultiSubnetFailover = true` in place

The steps to perform this process are outlined below:

1. Create a new subnet for each secondary, as all virtual machines are currently in the same subnet.

1. Determine the Cluster IP and Listener IP for all servers in the AG. For example, if you have an availability group with two nodes, you have the following:

    | VM Name | Subnet | Cluster IP | Listener IP |
    |----- |----- |----- |----- |
    | VM1 (primary) | 10.1.1.0/24 (existing subnet) | 10.1.1.15 | 10.1.1.16 |
    | VM2 (secondary) | 10.1.2.0/24 (new subnet) | 10.1.2.15 | 10.1.2.16 |

1. Add the Cluster IP and the Listener IP to the [primary server](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md?#add-secondary-ips-to-sql-server-vms). Adding these IP addresses is an online operation.

1. In the portal, move the secondary server to the new subnet, by going to the virtual machine > **Networking > Network Interface > IP Configurations**. Moving the server to a new subnet results in a reboot of the secondary.

1. Add the Cluster IP and the Listener IP to the [secondary server](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md?#add-secondary-ips-to-sql-server-vms). Adding these IP addresses is an online operation.

1. At this point, you have the IPs and the subnets in place, so you can delete the load balancer.

1. Drop the listener

1. If you're using Windows Server 2019 and later versions, skip this step. If you're using Windows Server 2016, manually add the [cluster IPs to the FCI](availability-group-manually-configure-tutorial-multi-subnet.md?#set-the-failover-cluster-ip-address).

1. Recreate the listener with the new listener IPs.

1. Flush DNS on all servers using ipconfig `/flushdns`.

Performing any migration involves some risk, so as always test thoroughly in a non-production environment before moving to a production environment.

## Next steps

- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Overview of Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md)
