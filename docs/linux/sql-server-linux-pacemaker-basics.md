---
title: Pacemaker for availability groups and failover cluster instances on Linux
description: Learn about using Pacemaker for high availability options for SQL Server on Linux.
author: VanMSFT
ms.author: randolphwest
ms.reviewer: vanto, amitkh-msft
ms.date: 10/20/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom: seo-lt-2019
---
# Pacemaker for availability groups and failover cluster instances on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

Starting with [!INCLUDE[sssql17-md](../includes/sssql17-md.md)], [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] is supported on both Linux and Windows. Like Windows-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] databases and instances need to be highly available under Linux. This article covers the basic information to understand Pacemaker with Corosync, and how to plan and deploy it for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] configurations.

## HA add-on/extension basics

All of the currently supported distributions ship a high availability add-on/extension, which is based on the Pacemaker clustering stack. This stack incorporates two key components: Pacemaker and Corosync. All the components of the stack are:

- **Pacemaker.** The core clustering component that coordinates things across the clustered machines.
- **Corosync.** A framework and set of APIs that provides things like quorum, the ability to restart failed processes, and so on.
- **libQB.** Provides things like logging.
- **Resource agent.** Specific functionality provided so that an application can integrate with Pacemaker.
- **Fence agent.** Scripts/functionality that help with isolating nodes and deal with them if they are having issues.

> [!NOTE]  
> The cluster stack is commonly referred to as Pacemaker in the Linux world.

This solution is in some ways similar to, but in many ways different from deploying clustered configurations using Windows. In Windows, the availability form of clustering, called a Windows Server failover cluster (WSFC), is built into the operating system, and the feature that enables the creation of a WSFC, failover clustering, is disabled by default. In Windows, AGs and FCIs are built on top of a WSFC, and share tight integration because of the specific resource DLL that is provided by [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]. This tightly coupled solution is possible by and large because it's all from one vendor.

:::image type="content" source="./media/sql-server-linux-pacemaker-basics/ha-basics.png" alt-text="Diagram of high availability basics.":::

On Linux, while each supported distribution has Pacemaker available, each distribution can customize and have slightly different implementations and versions. Some of the differences will be reflected in the instructions in this article. The clustering layer is open source, so even though it ships with the distributions, it isn't tightly integrated in the same way a WSFC is under Windows. This is why Microsoft provides *mssql-server-ha*, so that [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] and the Pacemaker stack can provide close to, but not exactly the same, experience for AGs and FCIs as under Windows.

For full documentation on Pacemaker, including a more in-depth explanation of what everything is with full reference information, for RHEL and SLES:

- [RHEL](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/high_availability_add-on_reference/ch-overview-haar)
- [SLES](https://www.suse.com/documentation/sle_ha/book_sleha/data/book_sleha.html)

Ubuntu doesn't have a guide for availability.

For more information about the whole stack, also see the official [Pacemaker documentation page](https://clusterlabs.org/doc/) on the ClusterLabs site.

## Pacemaker concepts and terminology

This section documents the common concepts and terminology for a Pacemaker implementation.

### Node

A node is a server participating in the cluster. A Pacemaker cluster natively supports up to 16 nodes. This number can be exceeded if Corosync isn't running on additional nodes, but Corosync is required for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]. Therefore, the maximum number of nodes a cluster can have for any [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]-based configuration is 16; this is the Pacemaker limit, and has nothing to do with maximum limitations for AGs or FCIs imposed by [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)].

### Resource

Both a WSFC and a Pacemaker cluster have the concept of a resource. A resource is specific functionality that runs in context of the cluster, such as a disk or an IP address. For example, under Pacemaker both FCI and AG resources can get created. This isn't dissimilar to what is done in a WSFC, where you see a [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] resource for either an FCI or an AG resource when configuring an AG, but isn't exactly the same due to the underlying  differences in how [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] integrates with Pacemaker.

Pacemaker has standard and clone resources. Clone resources are ones that run simultaneously on all nodes. An example would be an IP address that runs on multiple nodes for load balancing purposes. Any resource that gets created for FCIs uses a standard resource, since only one node can host an FCI at any given time.

[!INCLUDE [bias-sensitive-term-t](../includes/bias-sensitive-term-t.md)]

When an AG is created, it requires a specialized form of a clone resource called a multi-state resource. While an AG only has one primary replica, the AG itself is running across all nodes that it's configured to work on, and can potentially allow things such as read-only access. Because this is a "live" use of the node, the resources have the concept of two states: *Promoted* (previously *Master*) and *Unpromoted* (previously *Slave*). For more information, see [Multi-state resources: Resources that have multiple modes](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/high_availability_add-on_reference/s1-multistateresource-haar).

### Resource groups/sets

Similar to roles in a WSFC, a Pacemaker cluster has the concept of a resource group. A resource group (called a *set* in SLES) is a collection of resources that function together and can fail over from one node to another as a single unit. Resource groups can't contain resources that are configured as *Promoted* or *Unpromoted*; thus, they can't be used for AGs. While a resource group can be used for FCIs, it isn't generally a recommended configuration.

### Constraints

WSFCs have various parameters for resources and things like dependencies, which tell the WSFC of a parent/child relationship between two different resources. A dependency is just a rule telling the WSFC which resource needs to be online first.

A Pacemaker cluster doesn't have the concept of dependencies, but there are constraints. There are three kinds of constraints: colocation, location, and ordering.

- A colocation constraint enforces whether or not two resources should be running on the same node.
- A location constraint tells the Pacemaker cluster where a resource can (or can't) run.
- An ordering constraint tells the Pacemaker cluster the order in which the resources should start.

> [!NOTE]  
> Colocation constraints are not required for resources in a resource group, since all of those are seen as a single unit.

### Quorum, fence agents, and STONITH

Quorum under Pacemaker is similar to a WSFC in concept. The whole purpose of a cluster's quorum mechanism is to ensure that the cluster stays up and running. Both a WSFC and the HA add-ons for the Linux distributions have the concept of voting, where each node counts towards quorum. You want a majority of the votes up, otherwise, in a worst case scenario, the cluster will be shut down.

Unlike a WSFC, there's no witness resource to work with quorum. Like a WSFC, the goal is to keep the number of voters odd. Quorum configuration has different considerations for AGs than FCIs.

WSFCs monitor the status of the nodes participating and handle them when a problem occurs. Later versions of WSFCs offer such features as quarantining a node that is misbehaving or unavailable (node isn't on, network communication is down, etc.). On the Linux side, this type of functionality is provided by a fence agent. The concept is sometimes referred to as fencing. However, these fence agents are specific to the deployment, and often provided by hardware vendors and some software vendors, such as those who provide hypervisors. For example, VMware provides a fence agent that can be used for Linux VMs virtualized using vSphere.

Quorum and fencing ties into another concept called STONITH, or Shoot the Other Node in the Head. STONITH is required to have a supported Pacemaker cluster on all Linux distributions. For more information, see [Fencing in a Red Hat High Availability Cluster](https://access.redhat.com/solutions/15575) (RHEL).

### corosync.conf

The `corosync.conf` file contains the configuration of the cluster. It is located in `/etc/corosync`. In the course of normal day-to-day operations, this file should never have to be edited if the cluster is set up properly.

### Cluster log location

Log locations for Pacemaker clusters differ depending on the distribution.

- RHEL and SLES: `/var/log/cluster/corosync.log`
- Ubuntu: `/var/log/corosync/corosync.log`

To change the default logging location, modify `corosync.conf`.

## Plan Pacemaker clusters for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]

This section discusses the important planning points for a Pacemaker cluster.

### Virtualize Linux-based Pacemaker clusters for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]

Using virtual machines to deploy Linux-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments for AGs and FCIs is covered by the same rules as for their Windows-based counterparts. There is a base set of rules for supportability of virtualized [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments provided by Microsoft in [Microsoft Support KB 956893](https://support.microsoft.com/help/956893/support-policy-for-microsoft-sql-server-products-that-are-running-in-a-hardware-virtualization-environment). Different hypervisors such as Microsoft's Hyper-V and VMware's ESXi may have different variances on top of that, due to differences in the platforms themselves.

When it comes to AGs and FCIs under virtualization, ensure that anti-affinity is set for the nodes of a given Pacemaker cluster. When configured for high availability in an AG or FCI configuration, the VMs hosting [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] should never be running on the same hypervisor host. For example, if a two-node FCI is deployed, there would need to be *at least* three hypervisor hosts so that there's somewhere for one of the VMs hosting a node to go in the event of a host failure, especially if using features like Live Migration or vMotion.

For more specifics, consult:

- Hyper-V Documentation - [Using Guest Clustering for High Availability](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/dn440540(v=ws.11))
- Whitepaper (written for Windows-based deployments, but most of the concepts still apply) - [Planning Highly Available, Mission Critical SQL Server Deployments with VMware vSphere](https://www.vmware.com/content/dam/digitalmarketing/vmware/en/pdf/solutions/vmware-vsphere-highly-available-mission-critical-sql-server-deployments.pdf)

### Network

Unlike a WSFC, Pacemaker doesn't require a dedicated name or at least one dedicated IP address for the Pacemaker cluster itself. AGs and FCIs will require IP addresses (see the documentation for each for more information), but not names, since there's no network name resource. SLES does allow the configuration of an IP address for administration purposes, but it isn't required, as can be seen in [Create the Pacemaker cluster](sql-server-linux-deploy-pacemaker-cluster.md#create).

Like a WSFC, Pacemaker would prefer redundant networking, meaning distinct network cards (NICs or pNICs for physical) having individual IP addresses. In terms of the cluster configuration, each IP address would have what is known as its own ring. However, as with WSFCs today, many implementations are virtualized or in the public cloud where there's only a single virtualized NIC (vNIC) presented to the server. If all pNICs and vNICs are connected to the same physical or virtual switch, there's no true redundancy at the network layer, so configuring multiple NICs is a bit of an illusion to the virtual machine. Network redundancy is usually built into the hypervisor for virtualized deployments, and is definitely built into the public cloud.

One difference with multiple NICs and Pacemaker versus a WSFC is that Pacemaker allows multiple IP addresses on the same subnet, whereas a WSFC doesn't. For more information on multiple subnets and Linux clusters, see the article [Configure multiple subnets](sql-server-linux-configure-multiple-subnet.md).

### Quorum and STONITH

Quorum configuration and requirements are related to AG or FCI-specific deployments of [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)].

STONITH is required for a supported Pacemaker cluster. Use the documentation from the distribution to configure STONITH. An example is at [Storage-based Fencing](https://www.suse.com/documentation/sle_ha/book_sleha/data/sec_ha_storage_protect_fencing.html) for SLES. There is also a STONITH agent for VMware vCenter for ESXI-based solutions. For more information, see [Stonith Plugin Agent for VMware VM VCenter SOAP Fencing (Unofficial)](https://github.com/olafrv/fence_vmware_soap).

### Interoperability

This section documents how a Linux-based cluster can interact with a WSFC or with other distributions of Linux.

#### WSFC

Currently, there's no direct way for a WSFC and a Pacemaker cluster to work together. This means that there's no way to create an AG or FCI that works across a WSFC and Pacemaker. However, there are two interoperability solutions, both of which are designed for AGs. The only way an FCI can participate in a cross-platform configuration is if it's participating as an instance in one of these two scenarios:

- An AG with a cluster type of None. For more information, see the Windows [availability groups documentation](../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).
- A distributed AG, which is a special type of availability group that allows two different AGs to be configured as their own availability group. For more information on distributed AGs, see the documentation [Distributed Availability Groups](../database-engine/availability-groups/windows/distributed-availability-groups.md).

#### Other Linux distributions

On Linux, all nodes of a Pacemaker cluster must be on the same distribution. For example, this means that a RHEL node can't be part of a Pacemaker cluster that has a SLES node. The main reason for this was previously stated: the distributions may have different versions and functionality, so things couldn't work properly. Mixing distributions has the same story as mixing WSFCs and Linux: use None or distributed AGs.

## Next steps

- [Deploy a Pacemaker cluster for SQL Server on Linux](sql-server-linux-deploy-pacemaker-cluster.md)
- [Configure a Pacemaker cluster for SQL Server availability groups](sql-server-linux-availability-group-cluster-pacemaker.md)
