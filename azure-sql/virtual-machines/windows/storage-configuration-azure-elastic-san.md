---
title: Configure Azure Elastic SAN
description: This article describes how you can configure an Azure Elastic SAN for both new and existing SQL Server on Azure VMs. 
author: dplessMSFT
ms.author: dpless
ms.reviewer: mathoma
ms.date: 03/17/2025
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
ms.custom:
tags: azure-resource-manager
---
# Configure Azure Elastic SAN for SQL Server on Azure VMs
[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article teaches you to configure an [Azure Elastic SAN](/azure/storage/elastic-san/elastic-san-introduction) for your [SQL Server on Azure Virtual Machines (VMs)](sql-server-on-azure-vm-iaas-what-is-overview.md). 

## Overview

[Azure Elastic SAN](/azure/storage/elastic-san/elastic-san-introduction) is a network-attached storage offering that provides customers a flexible and scalable solution with the potential to reduce cost through storage consolidation. Azure Elastic SAN delivers a cost-effective, performant, and reliable block storage solution that connects to a variety of Azure compute services over iSCSI protocol. Elastic SAN enables a seamless transition from an existing SAN storage estate to the cloud without having to refactor customer application architecture. 

This solution can scale up to millions of IOPS, double-digit GB/s of throughput, and low single-digit millisecond latencies, with built-in resiliency to minimize downtime. Use Azure Elastic SAN if you need to consolidate storage, work with multiple compute services, or have workloads that require high throughput levels when driving storage over network bandwidth. However, since achieving desired IOPS/throughput for SQL Server workloads often requires overprovisioning capacity, _it's not typically appropriate for **single** SQL Server workloads_. To attain the most cost-effective solution with Elastic SAN, consider using it as storage for multiple consolidated SQL server workloads, or a combination of SQL Server and other low-performance workloads.

When considering VM sizing and performance for Azure Elastic SAN, it's important to understand that storage communication occurs over the network. For example, the VM size E4d_v5 doesn't support Azure Premium Storage but works well with Azure Elastic SAN as it supports up to 12,500-Mbps network throughput. When using Azure Elastic SAN with this VM size, you must ensure the network and storage throughput requirements for your workload fall under the 12,500-Mbps network throughput limit. 

Determine your network and storage requirements before deploying your SQL Server VM with an Azure Elastic SAN, and then carefully monitor network and storage utilization to confirm the chosen VM can accommodate the workload. To learn more, review [VM performance with Elastic SAN volumes](/azure/storage/elastic-san/elastic-san-performance) and [Elastic SAN metrics](/azure/storage/elastic-san/elastic-san-metrics). 

> [!CAUTION]
> VM sizing with Elastic SAN must accommodate production (VM to VM) network throughput requirements in addition to storage throughput. When using Elastic SAN, VM sizes that optimize for IO throughput might not be as cost-effective as VM sizes that optimize for network bandwidth. 

Consider placing SQL Server workloads on Elastic SAN for better cost efficiency because: 

- **Storage consolidation and dynamic performance sharing**: Normally for SQL Server on Azure VM workloads, disk storage is provisioned on a per VM basis based on your capacity and peak performance requirements for that VM. This overprovisioned performance is available when needed but the unused performance can't be shared with workloads on other VMs. Elastic SAN, similar to on-premises SAN, allows consolidating storage needs of multiple SQL and non-SQL workloads to achieve better cost efficiency, with the ability to dynamically share provisioned performance across the volumes provisioned to these different workloads based on IO demands. For example, in the East US region, say you have 10 workloads that require 2-TiB capacity and 10K IOPS each, but collectively they don't need more than 60,000 IOPS at any point in time. You can configure an Elastic SAN with 12 base units (1 base unit = $0.08 per GiB/month) that will give you 12 TiB capacity and the needed 60K IOPS, and 8 capacity-only units (1 capacity-only unit = $0.06 per GiB/month), which gives you the remaining 8-TiB capacity at a lower cost. This optimal storage configuration provides better cost efficiency while providing the necessary performance (10K IOPS) to each of these workloads. For more information on Elastic SAN base and capacity-only provisioning units, see [Planning for an Azure Elastic SAN](/azure/storage/elastic-san/elastic-san-planning#storage-and-performance) and for pricing, visit [Azure Elastic SAN - Pricing](https://azure.microsoft.com/pricing/details/elastic-san/).
- **To drive higher storage throughput**: SQL Server on Azure VM deployments occasionally require overprovisioning a VM due to disk throughput limits for that VM. You can avoid this with Elastic SAN, since you drive higher storage throughput over compute network bandwidth with the iSCSI protocol. For example, a Standard_E32ds_v5 VM is capped at 51,200 IOPS and 865 MBps for disk/storage throughput, but it can achieve up to a maximum of 2,000-MBps network throughput. If the storage throughput requirement for your workload is greater than 865 MBps, you won't have to upgrade the VM to a larger SKU since it can now support up to 2,000 MBps by using Elastic SAN.

## SAN concepts 

To better understand how a SAN can work for your SQL Server workload, it's important to understand the components of a SAN.

Azure Elastic SAN is designed to simplify the deployment, scaling, and management of storage at scale. It consists of three main components: the Elastic SAN itself, volume groups, and volumes.

- **Volume groups**: management constructs that allow you to manage volumes at scale. Any settings or configurations applied to a volume group, such as virtual network rules, are inherited by all volumes associated with that volume group. 
This is similar to how resource groups work in Azure, providing a way to apply configurations and policies across multiple volumes.
- **Volumes**: individual storage units where data is stored. Each volume can be mounted to clients using the iSCSI protocol, and they support mounting on Windows, Linux, Azure VMware Solution (AVS) and Azure Kubernetes Service (AKS).

For a SQL DBA concerned about separating SQL Server data files on to separate logical drives: 
- **Create a volume group**: This volume group acts as a container for your volumes and inherits any configurations you apply to it.
- **Create separate volumes**: Within this volume group, create separate volumes for disk to host data, log, and tempdb data files. Each volume can be configured with its own performance and capacity settings, and are mounted to the virtual machine as separate logical drives.

This setup allows you to manage and configure your storage resources efficiently while ensuring that SQL Server data files (data, log, tempdb) are stored on separate logical drives, as dictated by best practices.

## Prerequisites

To configure an Azure Elastic SAN for your SQL Server VM, you need the following prerequisites:

- An Azure subscription. If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn) before you begin.
- An [Azure virtual machine](/azure/virtual-machines/windows/quick-create-portal) appropriately [sized for your SQL Server workload](performance-guidelines-best-practices-vm-size.md). 
- Access to [SQL Server installation media](https://www.microsoft.com/evalcenter/evaluate-sql-server-2022) and a [product key](https://www.microsoft.com/en-us/sql-server/sql-server-2022-pricing?msockid=3c7688a9da0a67f905079ca8db5d66c6) for the desired version and edition of SQL Server. 

## Create and configure Azure Elastic SAN

Follow the instructions to [Create an Azure Elastic SAN](/azure/storage/elastic-san/elastic-san-create). 

Your Elastic SAN should be in the same resource group, virtual network, and availability zone as your Azure VM. 

After the Elastic SAN is created, follow these steps to configure your SAN: 

1. Go to your new [Elastic SAN](https://portal.azure.com/#browse/Microsoft.ElasticSan%2Felasticsans) in the Azure portal. 
1. Under **SAN Management**, select **Volume groups** to open the **Volume groups** pane. 
1. If a volume group doesn't already exist, use **+ Create**  on the **Volume groups** pane to create a new volume group. If a volume already exists, then modify it, if necessary, because the volume group should: 
    1. Have **CRC Protection** enabled if you're using a Windows virtual machine. 
    1. Be in the same virtual network and subnet as the Azure VM you intend to use for SQL Server. Use the **Networking** tab when you create or edit a volume group, and then use the dropdown list to select **Add an existing virtual network** to open the **Add network** pane. Select the appropriate subscription, virtual network, and subnet that your Azure VM uses from the dropdown lists, and then select **Add**.

1. Go to the **Volumes** page under **SAN Management** in the Azure portal to create new volumes in the volume group. To follow best practices: 
    1. Create three separate volumes, one for SQL Server data, one for the log, and one for tempdb. 
    1. Make each volume a different size so they're easy to differentiate in Disk Management. 

## Connect volumes to the VM

After you've created volumes in your volume group, you can [connect](/azure/storage/elastic-san/elastic-san-connect-windows) them to the virtual machine. 

To connect the volumes to the virtual machine, follow these steps: 

1. Go to the **Volumes** pages for your Azure Elastic SAN in the Azure portal.
1. Check the boxes next to all the volumes you want to connect to your SQL Server VM, and then select **Connect** to open the **Connect volumes** page:  

   :::image type="content" source="media/storage-configuration-azure-elastic-san/connect-to-san-volumes.png" alt-text="Screenshot of the volumes page for the elastic SAN in the Azure portal with connect selected.":::

1. If you haven't already done so, configure network access so that the volume group is in the same virtual network and subnet as the Azure VM you intend to use for SQL Server. If this is already done, skip to the next step. Otherwise, use the **Networking** dropdown list to select **Add an existing virtual network** to open the **Add network** pane. Select the appropriate subscription, virtual network, and subnet that your Azure VM uses from the dropdown lists, and then select **Add**.
1. Choose the appropriate operating system and then copy the PowerShell script provided under the **Connect to target(s)** step. Save this script for a later step. 
1. Connect to the Azure virtual using your preferred method, such as [Bastion](/azure/bastion/connect-vm-native-client-windows). 

After you're connected to the virtual machine, open an administrative PowerShell session. 

First, enable the iSCSI Initiator service with the following PowerShell commands: 

```powershell
# Confirm iSCSI is running
Get-Service -Name MSiSCSI

# If it's not running, start it
Start-Service -Name MSiSCSI

# Set it to start automatically
Set-Service -Name MSiSCSI -StartupType Automatic
```

Next, install Multipath I/O (MPIO) with the following PowerShell commands: 

```powershell
# Install Multipath-IO
Add-WindowsFeature -Name 'Multipath-IO'

# Verify if the installation was successful
Get-WindowsFeature -Name 'Multipath-IO'

# Enable multipath support for iSCSI devices
Enable-MSDSMAutomaticClaim -BusType iSCSI

# Set the default load balancing policy based on your requirements. In this example, we set it to round robin
# which should be optimal for most workloads.
mpclaim -L -M 2
```

> [!TIP]
> Before connecting the VM to the SAN, consider optimizing your SAN configuration by following [Elastic SAN best practices](/azure/storage/elastic-san/elastic-san-best-practices).


Finally, run the PowerShell script you saved earlier from the **Connect to targets** step in the Azure portal to connect the volumes to the virtual machine.

After the script completes, you can use [Disk Management](/windows-server/storage/disk-management/overview-of-disk-management) (for Windows) to format the volumes and bring them online. 

## Install SQL Server

Install SQL Server on the virtual machine as you normally would, using the [setup media](https://www.microsoft.com/evalcenter/evaluate-sql-server-2022) for your desired version of SQL Server. 

When you get to the **Database Engine Configuration** page, select the **Data Directories** tab and then choose the Elastic SAN volumes as the locations for your data files: 

:::image type="content" source="media/storage-configuration-azure-elastic-san/sql-data-directories.png" alt-text="Screenshot of the database engine configuration page of a SQL Server installation, with the data directories tab selected.":::

Then use the **TempDB** tab to modify the location of your tempdb so it's also using the designated Azure Elastic SAN volume. 

Once your data directories are configured for your SQL Server data files, proceed with the SQL Server installation. 

## Register with SQL IaaS Agent extension

To manage your SQL Server VM from the Azure portal and unlock a number [feature benefits](sql-server-iaas-agent-extension-automate-management.md#feature-benefits), [register](sql-agent-extension-manually-register-single-vm.md) it with the SQL IaaS Agent extension by using the [New-AzSQLVM](/powershell/module/az.sqlvirtualmachine/new-azsqlvm) command.

The `-LicenseType` parameter can be:
- `PAYG` for pay as you go
- `AHUB` for the [Azure Hybrid Benefit](licensing-model-azure-hybrid-benefit-ahb-change.md#azure-hybrid-benefit)
- `DR` to activate the [free DR replica license](business-continuity-high-availability-disaster-recovery-hadr-overview.md#free-dr-replica-in-azure)

Register a SQL Server VM with PowerShell by using the following sample command:

```powershell-interactive
# Get the existing compute VM
$vm = Get-AzVM -Name <vm_name> -ResourceGroupName <resource_group_name>

# Register SQL VM with SQL IaaS Agent extension
New-AzSqlVM -Name $vm.Name -ResourceGroupName $vm.ResourceGroupName -Location $vm.Location `
   -LicenseType <license_type>
```

> [!NOTE]
> Managing the storage through the [SQL virtual machines](manage-sql-vm-portal.md) resource isn't supported for self-installed SQL Server instances using Azure Elastic SAN. Selecting **Configure storage** on the **Management** tab of the **Storage** page for the SQL virtual machines resource in the Azure portal lets you see the volumes, but you can't modify them from this page. Use the Azure Elastic SAN resource to manage volumes.

## Modify volumes

You can modify the size of existing volumes, or add more volumes, from the **Volumes** page of your Azure Elastic SAN in the Azure portal.

To modify an existing volume, select the volume name to open the **Edit volume** page. 

To add more volumes, use **+ Create volume** on the **Volumes** page to open the **Create volume** page. You need to connect this volume to the virtual machine as you did with [the other volumes](#connect-volumes-to-the-vm).

## Increase size and IOPS

If you need to increase the IOPS of your SAN volumes, increase the size of the volume from the **Volumes** page of your Azure Elastic SAN in the Azure portal to raise the IOPS. You can cumulatively increase the size of each volume up to the maximum capacity of the SAN, or 64 TiB, whichever is smaller.

If you need to increase your IOPS beyond that, then increase the maximum capacity of the SAN itself from the **Configuration** page, under **Settings** of the Azure Elastic SAN in the Azure portal. Change the **Base** size to increase the maximum capacity of the SAN, thereby increasing the IOPS available to your volumes.

## Monitor performance 

You can use the [Metrics](/azure/storage/elastic-san/elastic-san-metrics) page for your Azure Elastic SAN to monitor performance. You can also use [I/O Analysis](storage-performance-analysis.md) tab of the **Storage page** for your [SQL virtual machines](manage-sql-vm-portal.md) in the Azure portal to identify VM-level throttling. 

## Best practices

Consider the following best practices when using an Azure Elastic SAN with SQL Server on Azure VMs:

- Use separate volumes to separate SQL Server data, log, and tempdb data files. 
- Enable CRC protection for Windows virtual machines. 
- Use Zone-redundant storage (ZRS) for high availability.
- Use a private endpoint so your data doesn't go over the public internet. 
- If you're going to use the [Volume Snapshot](/azure/storage/elastic-san/elastic-san-snapshots) feature to back up your Azure Elastic SAN data, be sure to schedule the snapshots during off-peak hours to avoid performance issues. 

## Limitations

Consider the following limitations when using an Azure Elastic SAN with SQL Server on Azure VMs:
- Changing the storage configuration via the [SQL virtual machines](manage-sql-vm-portal.md) resource in the Azure portal isn't supported. 

## Related content

- [What is SQL Server on Azure Windows Virtual Machines?](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [Storage: Performance best practices for SQL Server on Azure VMs](performance-guidelines-best-practices-storage.md)
- [Azure Elastic SAN](/azure/storage/elastic-san/elastic-san-introduction)
