---
title: Configure storage
description: This article describes how you can configure storage for both new and existing SQL Server on Azure VMs. 
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma
ms.date: 03/01/2024
ms.service: virtual-machines-sql
ms.subservice: management
ms.topic: how-to
ms.custom:
tags: azure-resource-manager
---
# Configure storage for SQL Server on Azure VMs
[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article teaches you how to configure your storage for your SQL Server on Azure Virtual Machines (VMs) deployed through the Azure Marketplace using Premium SSD.

SQL Server VMs deployed through marketplace images automatically follow default [storage best practices](performance-guidelines-best-practices-storage.md) which can be modified during deployment. Some of these configuration settings can be changed after deployment.

> [!NOTE]
> This article is only applicable to SQL Server on Azure VMs using Premium Storage, not [Premium SSD v2 storage](storage-configuration-premium-ssd-v2.md). 

## Prerequisites

To use the automated storage configuration settings, your virtual machine requires the following characteristics:

- Provisioned with a [SQL Server gallery image](sql-server-on-azure-vm-iaas-what-is-overview.md#licensing).
- Uses the [Resource Manager deployment model](/azure/azure-resource-manager/management/deployment-models).
- Uses [premium SSDs](/azure/virtual-machines/disks-types).

## New VMs

The following sections describe how to configure storage for new SQL Server virtual machines.

### Azure portal

When provisioning an Azure VM using a SQL Server gallery image, select **Change configuration** under **Storage** on the **SQL Server Settings** tab to open the **Configure storage** page. You can either leave the values at default, or modify the type of disk configuration that best suits your needs based on your workload.

:::image type="content" source="./media/storage-configuration/sql-vm-storage-configuration-provisioning.png" alt-text="Screenshot that highlights the SQL Server settings tab and the Change configuration option.":::

> [!NOTE]
> If you selected a supported VM size, you can use [Premium SSD v2](storage-configuration-premium-ssd-v2.md), which gives you granular control over disk size, IOPS, and throughput. 

Choose the drive location for your data files and log files, specifying the disk type, and number of disks. Use the IOPS values to determine the best storage configuration to meet your business needs. Choosing premium storage sets the [caching](/azure/virtual-machines/disks-performance#virtual-machine-uncached-vs-cached-limits) to *ReadOnly* for the data drive, and *None* for the log drive as per [SQL Server VM performance best practices](./performance-guidelines-best-practices-checklist.md).

:::image type="content" source="./media/storage-configuration/sql-vm-storage-configuration.png" alt-text="Screenshot from the Azure portal of the SQL Server VM Storage Configuration page during provisioning.":::

The disk configuration is completely customizable so that you can configure the storage topology, disk type and IOPS you need for your SQL Server VM workload. You also have the ability to use Ultradisk as an option for the **Disk type** if your SQL Server VM is in one of the supported regions and you've enabled [ultra disks for your subscription](/azure/virtual-machines/disks-enable-ultra-ssd).

Configure your `tempdb` database settings under **TempDb storage**, such as the location of the database files, as well as the number of files, initial size, and autogrowth size in MB.

- Currently, during deployment, the max number of `tempdb` files is 8, but more files can be added after the SQL Server VM is deployed.
- If you configure the SQL Server instance `tempdb` on the D: local SSD volume as recommended, the SQL IaaS Agent extension manages the folders and permissions needed upon reprovisioning. This doesn't require that you created the SQL virtual machine with an image from the Azure Marketplace.

:::image type="content" source="./media/create-sql-vm-portal/storage-configuration-tempdb-storage.png" alt-text="Screenshot that shows where you can configure the tempdb storage for your SQL VM.":::

Additionally, you have the ability to set the caching for the disks. Azure VMs have a multi-tier caching technology called [Blob Cache](/azure/virtual-machines/premium-storage-performance#disk-caching) when used with [Premium Disks](/azure/virtual-machines/disks-types#premium-ssds). Blob Cache uses a combination of the Virtual Machine RAM and local SSD for caching.

Disk caching for Premium SSD can be *ReadOnly*, *ReadWrite, or *None*.

- *ReadOnly* caching is highly beneficial for SQL Server data files that are stored on Premium Storage. *ReadOnly* caching brings low read latency, high read IOPS, and throughput as, reads are performed from cache, which is within the VM memory and local SSD. These reads are much faster than reads from data disk, which is from Azure Blob storage. Premium storage doesn't count the reads served from cache toward the disk IOPS and throughput. Therefore, your applicable is able to achieve higher total IOPS and throughput.
- *None* cache configuration should be used for the disks hosting SQL Server Log file as the log file is written sequentially and doesn't benefit from *ReadOnly* caching.
- *ReadWrite* caching shouldn't be used to host SQL Server files as SQL Server doesn't support data consistency with the *ReadWrite* cache. Writes waste capacity of the *ReadOnly* blob cache and latencies slightly increase if writes go through *ReadOnly* blob cache layers.


   > [!TIP]
   > Be sure that your storage configuration matches the limitations imposed by the the selected VM size. Choosing storage parameters that exceed the performance cap of the VM size will result in warning: `The desired performance might not be reached due to the maximum virtual machine disk performance cap`. Either decrease the IOPs by changing the disk type, or increase the performance cap limitation by increasing the VM size. This will not stop provisioning.


Based on your choices, Azure performs the following storage configuration tasks after creating the VM:

- Creates and attaches Premium SSDs to the virtual machine.
- Configures the data disks to be accessible to SQL Server.
- Configures the data disks into a storage pool based on the specified size and performance (IOPS and throughput) requirements.
- Associates the storage pool with a new drive on the virtual machine.


For a full walkthrough of how to create a SQL Server VM in the Azure portal, see [the provisioning tutorial](./create-sql-vm-portal.md).

### Resource Manager templates

If you use the following Resource Manager templates, two premium data disks are attached by default, with no storage pool configuration. However, you can customize these templates to change the number of premium data disks that are attached to the virtual machine.

- [Create VM with Automated Backup](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.compute/vm-sql-full-autobackup)
- [Create VM with Automated Patching](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.compute/vm-sql-full-autopatching)
- [Create VM with AKV Integration](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.compute/vm-sql-full-keyvault)

### Quickstart template

You can use the following quickstart template to deploy a SQL Server VM using storage optimization.

- [Create VM with storage optimization](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sqlvirtualmachine/sql-vm-new-storage/)
- [Create VM using Ultradisk](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sqlvirtualmachine/sql-vm-new-storage-ultrassd)

> [!NOTE]
> Some VM sizes might not have [temporary or local storage](/azure/virtual-machines/azure-vms-no-temp-disk). If you deploy a SQL Server on Azure VM without temporary storage, `tempdb` data and log files are placed in the data folder. 

## Existing VMs

> [!NOTE]
> Storage is only configurable for SQL Server VMs that were deployed from a SQL Server image in Azure Marketplace, and not currently supported for [Premium SSD v2](storage-configuration-premium-ssd-v2.md) disks. To modify disk configurations on an Azure virtual machine with self-installed SQL Server, use the [Disks pane](/azure/virtual-machines/windows/expand-os-disk#expand-the-volume-in-the-operating-system). 

### Modify existing drives

For existing SQL Server VMs that have been deployed through Azure Marketplace, you can modify some storage settings in the Azure portal through the SQL virtual machines resource, or on the [Disks pane](/azure/virtual-machines/windows/expand-os-disk#expand-the-volume-in-the-operating-system). 

To modify the storage settings, open your [SQL virtual machines resource](manage-sql-vm-portal.md#access-the-resource), and select **Storage configuration** under **Settings**, where you can:

- Add additional disks
- Configure or expand existing disks

:::image type="content" source="./media/storage-configuration/sql-vm-storage-configuration-existing.png" alt-text="Screenshot that highlights the Configure option and the Storage Usage section.":::

Selecting **Configure** opens the **Extend Data drive** page, allowing you to change the disk type, as well as add additional disks. You can also add disks through the [Disks pane](/azure/virtual-machines/windows/attach-managed-disk-portal). 

:::image type="content" source="./media/storage-configuration/sql-vm-storage-extend-drive.png" alt-text="A screenshot from the Azure portal showing the Extend Data drive page, used to configure storage for an existing SQL Server VM.":::

If you've already reached the maximum disks supported for a particular VM size, you may need to [Resize the VM](/azure/virtual-machines/sizes/resize-vm). 

### Modifying tempdb

It's also possible to modify your `tempdb` settings using the **Storage configuration** page, such as the number of `tempdb` files, as well as the initial size, and the autogrowth ratio. Select **Configure** next to **tempdb** to open the **tempdb Configuration** page.

Choose **Yes** next to **Configure tempdb data files** to modify your settings, and then choose **Yes** next to **Manage tempdb database folders on restart** to allow Azure to manage your `tempdb` configuration, folder, and permissions the next time your SQL Server service starts. This doesn't require that you created the SQL virtual machine with an image from the Azure Marketplace.  

:::image type="content" source="media/manage-sql-vm-portal/tempdb-configuration.png" alt-text="Screenshot of the tempdb configuration page of the Azure portal from the SQL virtual machines resource page.":::

Restart your SQL Server service to apply your changes.

### Increasing temporary disk size

To increase the temporary disk size, resize the VM to a SKU that supports a higher disk size for temporary storage. 

## Automated changes

This section provides a reference for the storage configuration changes that Azure automatically performs during SQL Server VM provisioning or configuration in the Azure portal.

- Azure configures a storage pool from storage selected from your VM. The next section of this article provides details about storage pool configuration.
- Automatic storage configuration always uses [premium SSDs](/azure/virtual-machines/disks-types) P30 data disks. So, there's a 1:1 mapping between your selected number of Terabytes and the number of data disks attached to your VM.

For pricing information, see the [Storage pricing](https://azure.microsoft.com/pricing/details/storage) page on the **Disk Storage** tab.

### Creation of the storage pool

Azure uses the following settings to create the storage pool on SQL Server VMs.

| Setting | Value |
| --- | --- |
| Stripe size |64 KB  |
| Disk sizes |1 TB each |
| Cache |Read |
| Allocation size |64-KB NTFS allocation unit size |
| Recovery | Simple recovery (no resiliency) |
| Number of columns |Number of data disks up to 8<sup>1</sup> |


<sup>1</sup> After the storage pool is created, you can't alter the number of columns in the storage pool.

## Enable caching

For Premium SSD, you can change the caching policy at the disk level. You can do so using the Azure portal, [PowerShell](/powershell/module/az.compute/set-azvmdatadisk), or the [Azure CLI](/cli/azure/vm/disk).

To change your caching policy in the Azure portal, follow these steps:

1. Stop your SQL Server service.
1. Sign into the [Azure portal](https://portal.azure.com).
1. Navigate to your virtual machine, select **Disks** under **Settings**.

   :::image type="content" source="./media/storage-configuration/disk-in-portal.png" alt-text="Screenshot showing the VM disk configuration pane in the Azure portal.":::

1. Choose the appropriate caching policy for your disk from the dropdown list - either Read-only, or None.

   :::image type="content" source="./media/storage-configuration/azure-disk-config.png" alt-text="Screenshot showing the disk caching policy configuration in the Azure portal.":::

1. After the change takes effect, restart the SQL Server VM and start the SQL Server service.


## Enable Write Accelerator

[Write Accelerator](/azure/virtual-machines/how-to-enable-write-accelerator) is a disk feature that is only available for the M-Series Virtual Machines (VMs). The purpose of write acceleration is to improve the I/O latency of writes against Azure Premium Storage when you need single digit I/O latency due to high volume mission critical OLTP workloads or data warehouse environments. 

Before enabling Write Accelerator, review some of the [restrictions](/azure/virtual-machines/how-to-enable-write-accelerator#restrictions-when-using-write-accelerator) to confirm they're acceptable for your business. 

Stop all SQL Server activity and shut down the SQL Server service before making changes to your write acceleration policy.

If your disks are striped, enable Write Acceleration for each disk individually, and your Azure VM should be shut down before making any changes.

To enable Write Acceleration using the Azure portal, follow these steps:

1. Stop your SQL Server service. If your disks are striped, shut down the virtual machine.
1. Sign into the [Azure portal](https://portal.azure.com).
1. Navigate to your virtual machine, select **Disks** under **Settings**.

   :::image type="content" source="./media/storage-configuration/disk-in-portal.png" alt-text="Screenshot showing the VM disk configuration pane in the Azure portal.":::

1. Choose the cache option with **Write Accelerator** for your disk from the dropdown list.

   :::image type="content" source="./media/storage-configuration/write-accelerator.png" alt-text="Screenshot showing the write accelerator cache policy.":::

1. After the change takes effect, start the virtual machine and SQL Server service.

## Disk striping

For more throughput, you can add additional data disks and use disk striping. To determine the number of data disks, analyze the throughput and bandwidth required for your SQL Server data files, including the log and `tempdb`. Throughput and bandwidth limits vary by VM size. To learn more, see [VM Size](/azure/virtual-machines/sizes).


- For Windows 8/Windows Server 2012 or later, use [Storage Spaces](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/hh831739(v=ws.11)) with the following guidelines:

  1. Set the interleave (stripe size) to 64 KB (65,536 bytes) to avoid performance impact due to partition misalignment. This must be set with PowerShell.

  1. Set column count = number of physical disks. Use PowerShell when configuring more than 8 disks (not Server Manager UI).

For example, the following PowerShell creates a new storage pool with the interleave size to 64 KB and the number of columns equal to the amount of physical disk in the storage pool:

# [Windows Server 2016 +](#tab/windows2016)

  ```powershell
  $PhysicalDisks = Get-PhysicalDisk | Where-Object {$_.FriendlyName -like "*2" -or $_.FriendlyName -like "*3"}
  
  New-StoragePool -FriendlyName "DataFiles" -StorageSubsystemFriendlyName "Windows Storage on <VM Name>" `
      -PhysicalDisks $PhysicalDisks | New-VirtualDisk -FriendlyName "DataFiles" `
      -Interleave 65536 -NumberOfColumns $PhysicalDisks.Count -ResiliencySettingName simple `
      -UseMaximumSize |Initialize-Disk -PartitionStyle GPT -PassThru |New-Partition -AssignDriveLetter `
      -UseMaximumSize |Format-Volume -FileSystem NTFS -NewFileSystemLabel "DataDisks" `
      -AllocationUnitSize 65536 -Confirm:$false
  ```

In Windows Server 2016 and later, the default value for `-StorageSubsystemFriendlyName` is `Windows Storage on <VM Name>`



# [Windows Server 2008 - 2012 R2](#tab/windows2012)



  ```powershell
  $PhysicalDisks = Get-PhysicalDisk | Where-Object {$_.FriendlyName -like "*2" -or $_.FriendlyName -like "*3"}
  
  New-StoragePool -FriendlyName "DataFiles" -StorageSubsystemFriendlyName "Storage Spaces on <VMName>" `
      -PhysicalDisks $PhysicalDisks | New-VirtualDisk -FriendlyName "DataFiles" `
      -Interleave 65536 -NumberOfColumns $PhysicalDisks.Count -ResiliencySettingName simple `
      -UseMaximumSize |Initialize-Disk -PartitionStyle GPT -PassThru |New-Partition -AssignDriveLetter `
      -UseMaximumSize |Format-Volume -FileSystem NTFS -NewFileSystemLabel "DataDisks" `
      -AllocationUnitSize 65536 -Confirm:$false 
  ```

In Windows Server 2008 to 2012 R2, the default value for `-StorageSubsystemFriendlyName` is `Storage Spaces on <VMName>`. 

---


  * For Windows 2008 R2 or earlier, you can use dynamic disks (OS striped volumes) and the stripe size is always 64 KB. This option is deprecated as of Windows 8/Windows Server 2012. For information, see the support statement at [Virtual Disk Service is transitioning to Windows Storage Management API](/windows/win32/w8cookbook/vds-is-transitioning-to-wmiv2-based-windows-storage-management-api).

  * If you're using [Storage Spaces Direct (S2D)](/windows-server/storage/storage-spaces/storage-spaces-direct-in-vm) with [SQL Server Failover Cluster Instances](./failover-cluster-instance-storage-spaces-direct-manually-configure.md), you must configure a single pool. Although different volumes can be created on that single pool, they'll all share the same characteristics, such as the same caching policy.

  * Determine the number of disks associated with your storage pool based on your load expectations. Keep in mind that different VM sizes allow different numbers of attached data disks. For more information, see [Sizes for virtual machines](/azure/virtual-machines/sizes?toc=/azure/virtual-machines/windows/toc.json).

## Known issues

### Configure Disk option or Storage Configuration pane on SQL virtual machine resource is grayed out

The **Storage Configuration** pane can be grayed out in the Azure portal if your SQL IaaS Agent extension is in a failed state. [Repair the SQL IaaS Agent extension](sql-agent-extension-troubleshoot-known-issues.md#repair-extension). 

**Configure** on the Storage Configuration pane can be grayed out if you've customized your storage pool, or if you're using a non-Marketplace image. 

### I have a disk with 1 TB of unallocated space that I can't remove from storage pool

There's no option to remove the unallocated space from a disk that belongs to a storage pool.

### My transaction log is full 

Review [Troubleshoot a full transaction log](/sql/relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002) if your log becomes full. 

### The Storage pane for the SQL virtual machines resource is unavailable in the Azure Portal

Storage configuration for the SQL virtual machines resource in the Azure portal isn't available in the following scenarios: 
- Virtual machines with self-installed SQL Server instances. Currently, only SQL Server VM images from Azure marketplace are supported.  
- SQL Server VMs using Premium SSDv2. Currently, only SQL Server VMs with Premium SSD are supported. 
- When TCP/IP is disabled in SQL Server Configuration Manager. 

## Related content

- [What is SQL Server on Azure Windows Virtual Machines?](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [Storage: Performance best practices for SQL Server on Azure VMs](performance-guidelines-best-practices-storage.md)
