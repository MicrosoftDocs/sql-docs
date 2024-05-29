---
title: High availability, disaster recovery, business continuity
description: Learn about the high availability, disaster recovery (HADR), and business continuity options available for SQL Server on Azure VMs, such as Always On availability groups, failover cluster instance, database mirroring, log shipping, and back up and restore to Azure Storage.
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: mathoma, randolphwest
ms.date: 06/11/2024
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: conceptual
tags: azure-service-management
---
# Business continuity and HADR for SQL Server on Azure VMs

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article compares and contrasts the Azure-only and hybrid business continuity solutions you can use for high availability and disaster recovery (HADR) with your SQL Server on Azure Virtual Machines (VMs)

Business continuity means continuing your business in the event of a disaster, planning for recovery, and ensuring that your data is highly available. SQL Server on Azure Virtual Machines can help lower the cost of a high-availability and disaster recovery (HADR) database solution.

> [!NOTE]  
> It's possible to lift and shift both your [failover cluster instance](../../migration-guides/virtual-machines/sql-server-failover-cluster-instance-to-sql-on-azure-vm.md) and [availability group](../../migration-guides/virtual-machines/sql-server-availability-group-to-sql-on-azure-vm.md) solution to SQL Server on Azure VMs using Azure Migrate.

## Overview

SQL Server on Azure VMs support the following type of solutions: 

- **Azure-only**: the entire HADR system runs in Azure.
- **Hybrid**: part of the solution runs in Azure and the other part runs on-premises in your organization.

The flexibility of the Azure environment enables you to move partially or completely to Azure to satisfy the budget and HADR requirements of your SQL Server database systems. It's up to you to ensure that your database systems have HADR capabilities that meet your business requirements for recovery time objective (RTO), recovery point objective (RPO), and service-level agreement (SLA). 

The built-in high-availability mechanisms provided by Azure, such as service healing for cloud services and failure recovery detection for virtual machines, don't guarantee that you can meet the SLA, RTO, or RPO. Although these mechanisms help protect the high availability of the virtual machine, they don't protect the availability of SQL Server running inside the VM. It's possible for the SQL Server instance to fail while the VM is online and healthy. Even the high-availability mechanisms provided by Azure allow for downtime of the VMs due to events like recovery from software or hardware failures and operating system upgrades.

## Business continuity features

The following table lists both Azure-only and hybrid SQL Server features you can use for high availability (HA), disaster recovery (DR), or both (HA/DR): 

These SQL Server features are supported for business continuity in both an Azure-only or hybrid configuration. Some of the options are ideal for both high availability and disaster recovery (HA/DR), high-availability (HA), while others would be used for disaster recovery (DR). 

| SQL Server features | HA/DR option | Details |
| --- | --- | --- |
| [Always On availability groups](/sql/database-engine/availability-groups/windows/always-on-availability-groups-sql-server) | High availability and disaster recovery | Provides database level protection, increase high availability and disaster recovery by adding replicas in different availability zones and/or regions. |
| [Always On failover cluster instances (FCIs)](/sql/sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server) | High availability | Uses shared storage to provide protection at the instance level. Increase protection to both the database and instance level by combining with availability groups. |
| [Log shipping](/sql/database-engine/log-shipping/about-log-shipping-sql-server) | Disaster recovery | Database level protection for disaster recovery involves sending transaction log backups from a primary server and restoring them to a secondary server. An Azure file share is needed. |
| [SQL Server backup and restore with Azure Blob storage](/sql/relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service) | Disaster recovery | Production database backups stored in Azure Blob storage for disaster recovery protection. |
|[Azure Site Recovery](/azure/site-recovery/site-recovery-sql) | Disaster recovery | A disaster recovery solution that replicates VMs from a primary site to a secondary site. |

You can combine technologies to implement a SQL Server solution that has both high-availability and disaster recovery capabilities. Depending on the technology that you use, a hybrid deployment might require a VPN tunnel with the Azure virtual network. While the technologies are the same, there might be some differences in how they're set up in Azure or in a hybrid design. 


## Availability Groups (HADR)

Protecting your SQL Server on Azure VMs at the database level can be done using [**availability groups**](availability-group-overview.md) as a high-availability and disaster recovery (HADR) solution. Replicas running in Azure VMs in the same region provide high availability. A domain controller VM is needed since Windows failover clustering requires an Active Directory domain. 

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/azure-only-ha-always-on.png" alt-text="Diagram that shows the Domain Controller above the WSFC Cluster made of the Primary Replica, Secondary Replica, and File Share Witness.":::

To get started, review the [availability group tutorial](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md).

For higher redundancy, availability, and disaster recovery protection, the Azure VMs can be deployed to different [availability zones](/azure/availability-zones/az-overview) as documented in the [availability group overview](availability-group-overview.md). Expanding availability replicas to run across multiple datacenters in Azure VMs adds further disaster recovery coverage. A cross-region solution helps protect against a complete site outage.

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/azure-only-dr-alwayson.png" alt-text="Diagram that shows two regions with a Primary Replica and Secondary Replica connected by an Asynchronous Commit.":::

Within a region, all replicas should be within the same cloud service and the same virtual network. Because each region has a separate virtual network, these solutions require network-to-network connectivity. For more information, see [Configure a network-to-network connection by using the Azure portal](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-resource-manager-portal). For detailed instructions, see [Configure a SQL Server Always On availability group across different Azure regions](availability-group-manually-configure-multiple-regions.md).

In a hybrid configuration, some availability replicas run in Azure VMs and other replicas are on-premises for cross-site disaster recovery. The production site can be either on-premises or in an Azure datacenter.

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/hybrid-dr-alwayson.png" alt-text="Diagram of Availability groups configured from on-premises to Azure."::: 

Because all availability replicas must be in the same failover cluster, the cluster must span both networks (a multi-subnet failover cluster). This configuration requires a VPN connection between Azure and the on-premises network.

For successful disaster recovery of your databases, you should also install a replica domain controller at the disaster recovery site. To get started, review the [availability group tutorial](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md). 


## Failover cluster instances (HA)

SQL Server on Azure VMs support [**failover cluster instancesN (FCI)**](failover-cluster-instance-overview.md) and this solution provides high-availability at the instance level. For additional protection, you can create redundancy at both the database and instance level by creating availability groups on top of failover cluster instances. The FCI feature requires shared storage, and there are five solutions that work with SQL Server on Azure VMs:

- Using [Azure shared disks](failover-cluster-instance-azure-shared-disks-manually-configure.md) for Windows Server 2019. Shared managed disks are an Azure product that allow attaching a managed disk to multiple virtual machines simultaneously. VMs in the cluster can read or write to your attached disk based on the reservation chosen by the clustered application through SCSI Persistent Reservations (SCSI PR). SCSI PR is an industry-standard storage solution that's used by applications running on a storage area network (SAN) on-premises. Enabling SCSI PR on a managed disk allows you to migrate these applications to Azure as is.

- Using [Storage Spaces Direct \(S2D\)](failover-cluster-instance-storage-spaces-direct-manually-configure.md) to provide a software-based virtual SAN for Windows Server 2016 and later.

- Using a [Premium file share](failover-cluster-instance-premium-file-share-manually-configure.md) for Windows Server 2012 and later. Premium file shares are SSD backed, have consistently low latency, and are fully supported for use with FCI.

- Using storage supported by a partner solution for clustering. For a specific example that uses SIOS DataKeeper, see the blog entry [Failover clustering and SIOS DataKeeper](https://azure.microsoft.com/blog/high-availability-for-a-file-share-using-wsfc-ilb-and-3rd-party-software-sios-datakeeper/).

- Using shared block storage for a remote iSCSI target via Azure ExpressRoute. For example, NetApp Private Storage (NPS) exposes an iSCSI target via ExpressRoute with Equinix to Azure VMs.

For shared storage and data replication solutions from Microsoft partners, contact the vendor for any issues related to accessing data on failover.

To get started, [prepare your VM for FCI](failover-cluster-instance-prepare-vm.md).


## Log shipping (DR)

Another disaster recovery solution in Azure is [**log shipping**](log-shipping-configure.md) which automatically sends transaction log backups from a primary database on a primary server to one or more secondary databases on a separate secondary server. The configuration of log shipping uses an [Azure File Share](/azure/storage/files/storage-files-introduction) to store the transaction log backups. 

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/azure-only-log-shipping.png" alt-text="Diagram of Log shipping in Azure."::: 

If you need to configure log shipping in a hybrid environment, then one server is located on an Azure VM and the other is on-premises for cross-site disaster recovery. Log shipping depends on Windows file sharing, so a VPN connection between the Azure virtual network and the on-premises network is required.

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/hybrid-dr-log-shipping.png" alt-text="Diagram of Log shipping."::: 

For successful disaster recovery of your databases, you should also install a replica domain controller at the disaster recovery site. 

## Back up and restore (DR)

Backing up your production databases is necessary for disaster recovery. In Azure, you can back up databases directly to Blob storage in a different datacenter for disaster recovery.

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/azure-only-dr-backup-restore.png" alt-text="Diagram that shows a Database in one region backing up to Blob Storage in another region."::: 

In a hybrid solution, on-premises production databases can be backed up directly to Azure Blob storage for disaster recovery.

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/hybrid-dr-backup-restore.png" alt-text="Diagram of Backup and restore."::: 

For more information, see [Backup and restore for SQL Server on Azure Virtual Machines](backup-restore.md).

## Replicate with Azure Site Recovery (DR)

[**Azure Site Recovery**](/azure/site-recovery/site-recovery-sql) can be used as a disaster recovery solution in both Azure and in a hybrid configuration. 

Inside of Azure, the production SQL Server instance in one Azure datacenter is replicated directly to Azure Storage in a different Azure datacenter for disaster recovery.
 
:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/azure-only-dr-standalone-sqlserver-asr.png" alt-text="Diagram that shows a Database in one Azure datacenter using Azure Site Recovery Replication for disaster recovery in another datacenter."::: 
 
For hybrid environments, an on-premises production SQL Server instance is replicated directly to Azure Storage for disaster recovery.

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/hybrid-dr-standalone-sqlserver-asr.png" alt-text="Diagram of Replicate using Azure Site Recovery."::: 

For more information, see [Protect SQL Server using SQL Server disaster recovery and Azure Site Recovery](/azure/site-recovery/site-recovery-sql). 

## Free DR replica in Azure

If you have [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default?rtc=1&activetab=software-assurance-default-pivot:primaryr3), you can implement hybrid disaster recovery (DR) plans with SQL Server without incurring additional licensing costs for the passive disaster recovery instance. You also qualify for license-free DR replicas with pay-as-you-go licensing if all replicas are hosted in Azure.

For example, you can have two free passive secondaries when all three replicas are hosted in Azure:

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/failover-with-primary-in-azure.png" alt-text="Diagram of two free passives when everything in Azure.":::

Or you can configure a hybrid failover environment, with a licensed primary on-premises, one free passive for HA, one free passive for DR on-premises, and one free passive for DR in Azure:

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/hybrid-with-primary-on-prem.png" alt-text="Diagram of three free passives when environment is hybrid with one primary on-premises replica.":::

For more information, see the [product licensing terms](https://www.microsoft.com/licensing/product-licensing/products).

To enable this benefit, go to your [SQL Server virtual machine resource](manage-sql-vm-portal.md#access-the-resource). Select **Configure** under **Settings**, and then choose the **HA/DR** option under **SQL Server License**, then select **Apply** to save your settings. When all three replicas are hosted in Azure, pay-as-you-go customers are also entitled to use the **HA/DR** license type.

:::image type="content" source="media/business-continuity-high-availability-disaster-recovery-hadr-overview/dr-replica-in-portal.png" alt-text="Diagram about configuring a disaster recovery replica in Azure.":::

## Important considerations for SQL Server HADR in Azure

Azure VMs, storage, and networking have different operational characteristics than an on-premises, nonvirtualized IT infrastructure. A successful implementation of an HADR SQL Server solution in Azure requires that you understand these differences and design your solution to accommodate them.

### High-availability nodes in an availability set

Availability sets in Azure enable you to place the high-availability nodes into separate fault domains and update domains. The Azure platform assigns an update domain and a fault domain to each virtual machine in your availability set. This configuration within a datacenter ensures that during either a planned or unplanned maintenance event, at least one virtual machine is available and meets the Azure SLA of 99.95 percent.

To configure a high-availability setup, place all participating SQL Server virtual machines in the same availability set to avoid application or data loss during a maintenance event. Only nodes in the same cloud service can participate in the same availability set. For more information, see [Manage the availability of virtual machines](/azure/virtual-machines/availability?toc=%2fazure%2fvirtual-machines%2fwindows%2ftoc.json).

### High-availability nodes in an availability zone

Availability zones are unique physical locations within an Azure region. Each zone consists of one or more datacenters equipped with independent power, cooling, and networking. The physical separation of availability zones within a region helps protect applications and data from datacenter failures by ensuring that at least one virtual machine is available and meets the Azure SLA of 99.99 percent.

To configure high availability, place participating SQL Server virtual machines spread across availability zones in the region. There will be additional charges for network-to-network transfers between availability zones. For more information, see [Availability zones](/azure/availability-zones/az-overview).

### Network latency in hybrid IT

Deploy your HADR solution with the assumption that there might be periods of high network latency between your on-premises network and Azure. When you're deploying replicas to Azure, use asynchronous commit instead of synchronous commit for the synchronization mode. When you're deploying database mirroring servers both on-premises and in Azure, use the high-performance mode instead of the high-safety mode.

See the [HADR configuration best practices](hadr-cluster-best-practices.md) for cluster and HADR settings that can help accommodate the cloud environment.

### Geo-replication support

Geo-replication in Azure disks doesn't support the data file and log file of the same database to be stored on separate disks. GRS replicates changes on each disk independently and asynchronously. This mechanism guarantees the write order within a single disk on the geo-replicated copy, but not across geo-replicated copies of multiple disks. If you configure a database to store its data file and its log file on separate disks, the recovered disks after a disaster might contain a more up-to-date copy of the data file than the log file, which breaks the write-ahead log in SQL Server and the ACID properties (atomicity, consistency, isolation, and durability) of transactions.

If you don't have the option to disable geo-replication on the storage account, keep all data and log files for a database on the same disk. If you must use more than one disk due to the size of the database, deploy one of the disaster recovery solutions listed earlier to ensure data redundancy.

## Next steps

Decide if an [availability group](availability-group-overview.md) or a [failover cluster instance](failover-cluster-instance-overview.md) is the best business continuity solution for your business. Then review the [best practices](hadr-cluster-best-practices.md) for configuring your environment for high availability and disaster recovery.
