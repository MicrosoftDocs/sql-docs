---
title: Failover cluster instances
description: "Learn about failover cluster instances (FCIs) with SQL Server on Azure Virtual Machines."
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: randolphwest, mathoma
ms.date: 06/18/2024
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: overview
editor: monicar
tags: azure-service-management
---

# Failover cluster instances with SQL Server on Azure Virtual Machines

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article introduces feature differences when you're working with failover cluster instances (FCI) for SQL Server on Azure Virtual Machines (VMs).

To get started, [prepare your vm](failover-cluster-instance-prepare-vm.md).

## Overview

SQL Server on Azure VMs uses [Windows Server Failover Clustering (WSFC)](hadr-windows-server-failover-cluster-overview.md) functionality to provide local high availability through redundancy at the server-instance level: a failover cluster instance. An FCI is a single instance of SQL Server that's installed across WSFC (or simply the cluster) nodes and, possibly, across multiple subnets. On the network, an FCI appears to be a single instance of SQL Server running on a single computer. But the FCI provides failover from one WSFC node to another if the current node becomes unavailable.

The rest of the article focuses on the differences for failover cluster instances when they're used with SQL Server on Azure VMs. To learn more about the failover clustering technology, see:

- [Windows cluster technologies](/windows-server/failover-clustering/failover-clustering-overview)
- [SQL Server failover cluster instances](/sql/sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server)

> [!NOTE]  
> It's now possible to lift and shift your failover cluster instance solution to SQL Server on Azure VMs using Azure Migrate. See [Migrate failover cluster instance](../../migration-guides/virtual-machines/sql-server-failover-cluster-instance-to-sql-on-azure-vm.md) to learn more.

## Quorum

Failover cluster instances with SQL Server on Azure Virtual Machines support using a disk witness, a cloud witness, or a file share witness for cluster quorum.

To learn more, see [Quorum best practices with SQL Server VMs in Azure](hadr-cluster-best-practices.md#quorum).

## Storage

In traditional on-premises clustered environments, a Windows failover cluster uses a storage area network (SAN) that's accessible by both nodes as the shared storage. SQL Server files are hosted on the shared storage, and only the active node can access the files at one time.

SQL Server on Azure VMs offers various options as a shared storage solution for a deployment of SQL Server failover cluster instances:

| | [Azure shared disks](/azure/virtual-machines/disks-shared) | [Premium file shares](/azure/storage/files/storage-how-to-create-file-share) | [Storage Spaces Direct (S2D)](/windows-server/storage/storage-spaces/storage-spaces-direct-overview) |
| --- | --- | --- | --- |
| **Minimum OS version** | All | Windows Server 2012 | Windows Server 2016 |
| **Minimum SQL Server version** | All | SQL Server 2012 | SQL Server 2016 |
| **Supported VM availability** | [Premium SSD LRS](/azure/virtual-machines/disks-redundancy#locally-redundant-storage-for-managed-disks): Availability Sets with or without [proximity placement group](/azure/virtual-machines/windows/proximity-placement-groups-portal)<br />[Premium SSD ZRS](/azure/virtual-machines/disks-redundancy#zone-redundant-storage-for-managed-disks): Availability Zones<br />[Ultra disks](/azure/virtual-machines/disks-enable-ultra-ssd): Same availability zone | Availability sets and availability zones | Availability sets |
| **Supports FileStream** | Yes | No | Yes |
| **Azure blob cache** | No | No | Yes |

The rest of this section lists the benefits and limitations of each storage option available for SQL Server on Azure VMs.

### Azure shared disks

[Azure shared disks](/azure/virtual-machines/disks-shared) are a feature of [Azure managed disks](/azure/virtual-machines/managed-disks-overview). Windows Server Failover Clustering supports using Azure shared disks with a failover cluster instance.

**Supported OS**: All  
**Supported SQL version**: All

**Benefits**:

- Useful for applications looking to migrate to Azure while keeping their high-availability and disaster recovery (HADR) architecture as is.
- Can migrate clustered applications to Azure as is because of SCSI Persistent Reservations (SCSI PR) support.
- Supports shared Azure Premium SSD and Azure Ultra Disk storage.
- Can use a single shared disk or stripe multiple shared disks to create a shared storage pool.
- Supports FILESTREAM.
- Premium SSDs support availability sets.
- Premium SSDs Zone Redundant Storage (ZRS) supports Availability Zones. VMs part of FCI can be placed in different availability zones.

> [!NOTE]  
> While Azure shared disks also support [Standard SSD sizes](/azure/virtual-machines/disks-shared#disk-sizes), we do not recommend using Standard SSDs for SQL Server workloads due to the performance limitations.

**Limitations**:

- Premium SSD disk caching isn't supported.
- Ultra disks don't support availability sets or Zone Redundant Storage (ZRS). 
- Availability zones are supported for Ultra Disks, but the VMs must be in the same availability zone, which reduces the availability of the virtual machine to 99.9%.

To get started, see [SQL Server failover cluster instance with Azure shared disks](failover-cluster-instance-azure-shared-disks-manually-configure.md).

### Storage Spaces Direct

[Storage Spaces Direct](/windows-server/storage/storage-spaces/storage-spaces-direct-overview) is a Windows Server feature that is supported with failover clustering on Azure Virtual Machines. It provides a software-based virtual SAN.

**Supported OS**: Windows Server 2016 and later  
**Supported SQL version**: SQL Server 2016 and later

**Benefits:**

- Sufficient network bandwidth enables a robust and highly performant shared storage solution.
- Supports Azure blob cache, so reads can be served locally from the cache. (Updates are replicated simultaneously to both nodes.)
- Supports FileStream.

**Limitations:**

- Available only for Windows Server 2016 and later.
- Availability zones aren't supported.
- Requires the same disk capacity attached to both virtual machines.
- High network bandwidth is required to achieve high performance because of ongoing disk replication.
- Requires a larger VM size and double pay for storage, because storage is attached to each VM.

To get started, see [SQL Server failover cluster instance with Storage Spaces Direct](failover-cluster-instance-storage-spaces-direct-manually-configure.md).

### Premium file share

[Premium file shares](/azure/storage/files/storage-how-to-create-file-share) are a feature of [Azure Files](/azure/storage/files/index). Premium file shares are SSD backed and have consistently low latency. They're fully supported for use with failover cluster instances for SQL Server 2012 or later on Windows Server 2012 or later. Premium file shares give you greater flexibility, because you can resize and scale a file share without any downtime.

**Supported OS**: Windows Server 2012 and later  
**Supported SQL version**: SQL Server 2012 and later

**Benefits:**

- Shared storage solution for virtual machines spread over multiple availability zones.
- Fully managed file system with single-digit latencies and burstable I/O performance.
- Not all SQL Server features are supported - such as database snapshots, filestream, and CHECKDB without TABLOCK. Review [Limitations](failover-cluster-instance-premium-file-share-manually-configure.md#limitations) for details. 

**Limitations:**

- Available only for Windows Server 2012 and later.
- FileStream isn't supported.

To get started, see [SQL Server failover cluster instance with Premium file share](failover-cluster-instance-premium-file-share-manually-configure.md).

### Partner

There are partner clustering solutions with supported storage.

**Supported OS**: All  
**Supported SQL version**: All

One example uses SIOS DataKeeper as the storage. For more information, see the blog entry [Failover clustering and SIOS DataKeeper](https://azure.microsoft.com/blog/high-availability-for-a-file-share-using-wsfc-ilb-and-3rd-party-software-sios-datakeeper/).

### iSCSI and ExpressRoute

You can also expose an iSCSI target shared block storage via Azure ExpressRoute.

**Supported OS**: All  
**Supported SQL version**: All

For example, NetApp Private Storage (NPS) exposes an iSCSI target via ExpressRoute with Equinix to Azure VMs.

For shared storage and data replication solutions from Microsoft partners, contact the vendor for any issues related to accessing data on failover.

## Connectivity

To match the on-premises experience for connecting to your failover cluster instance, deploy your SQL Server VMs to [multiple subnets](failover-cluster-instance-prepare-vm.md#subnets) within the same virtual network. Having multiple subnets negates the need for the extra dependency on an Azure Load Balancer, or a distributed network name (DNN) to route your traffic to your FCI.

If you deploy your SQL Server VMs to a single subnet, you can configure a virtual network name (VNN) and an Azure Load Balancer, or a distributed network name (DNN) to route traffic to your failover cluster instance. [Review the differences between the two](hadr-windows-server-failover-cluster-overview.md#virtual-network-name-vnn) and then deploy either a [distributed network name](failover-cluster-instance-distributed-network-name-dnn-configure.md) or a [virtual network name](failover-cluster-instance-vnn-azure-load-balancer-configure.md) for your failover cluster instance.

The distributed network name is recommended, if possible, as failover is faster, and the overhead and cost of managing the load balancer is eliminated.

Most SQL Server features work transparently with FCIs when using the DNN, but there are certain features that may require special consideration. For more information, see [FCI and DNN interoperability](failover-cluster-instance-dnn-interoperability.md).

> [!NOTE]
> If you have multiple AGs or FCIs on the same cluster and you use either a DNN or VNN listener, then each AG or FCI needs its own independent connection point.

## Limitations

Consider the following limitations for failover cluster instances with SQL Server on Azure Virtual Machines.

### Limited extension support

At this time, SQL Server failover cluster instances on Azure virtual machines, registered with the SQL IaaS Agent extension, only support a limited number of features. See the [table of benefits](sql-server-iaas-agent-extension-automate-management.md#feature-benefits).

If your SQL Server VM has already been registered with the SQL IaaS Agent extension and you've enabled any features that require the agent, you need to [unregister](sql-agent-extension-manually-register-single-vm.md#unregister-from-extension) from the extension by deleting the **SQL virtual machine** resource for the corresponding VMs and then register it with the SQL IaaS Agent extension again. When you're deleting the **SQL virtual machine** resource by using the Azure portal, clear the check box next to the correct virtual machine to avoid deleting the virtual machine.

SQL Server FCIs registered with the SQL IaaS Agent extension don't support features that require the agent, such as automated backup, patching, Microsoft Entra authentication and advanced portal management. See the [table of benefits](sql-server-iaas-agent-extension-automate-management.md#feature-benefits) for more information.

### MSDTC

Azure Virtual Machines support Microsoft Distributed Transaction Coordinator (MSDTC) on Windows Server 2019 with storage on Clustered Shared Volumes (CSV) and [Azure Standard Load Balancer](/azure/load-balancer/load-balancer-overview) or on SQL Server VMs that are using Azure shared disks.

On Azure Virtual Machines, MSDTC isn't supported for Windows Server 2016 or earlier with Clustered Shared Volumes because:

- The clustered MSDTC resource can't be configured to use shared storage. On Windows Server 2016, if you create an MSDTC resource, it doesn't show any shared storage available for use, even if storage is available. This issue has been fixed in Windows Server 2019.
- The basic load balancer doesn't handle RPC ports.

### Azure Elastic SAN

[Azure Elastic SAN](performance-guidelines-best-practices-storage.md#azure-elastic-san) is not currently supported on Windows Server Failover Cluster so SQL Server failover cluster instances (FCIs) are unsupported. 

## Related content

- [HADR configuration best practices (SQL Server on Azure VMs)](hadr-cluster-best-practices.md)
- [Prepare virtual machines for an FCI (SQL Server on Azure VMs)](failover-cluster-instance-prepare-vm.md)
- [Windows Server Failover Cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Failover cluster instance overview](/sql/sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server)
