---
title: "Configure Persistent Memory (PMEM) - Windows"
description: Learn how to configure persistent memory (PMEM) for SQL Server on Windows, and how to create namespaces for PMEM devices.
author: adbadram
ms.author: adbadram
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Configure persistent memory (PMEM) for SQL Server on Windows

This article describes how to configure the persistent memory (PMEM) for [!INCLUDE [sqlv14](../../includes/sssql16-md.md)] and above on Windows.

## Overview

[!INCLUDE [sqlv15](../../includes/sssql19-md.md)] has several in-memory database features that rely on persistent memory. This document covers the steps required to configure persistent memory for SQL Server on Windows.

> [!NOTE]  
> The term _enlightenment_ was introduced to convey the concept of working with a persistent memory aware file system. Direct access (DAX) extensions to the NTFS file system provide the ability to memory map files from kernel space to user space. When a file is memory mapped into user space the application can issue load/store instructions directly to the memory mapped file, bypassing the kernel I/O stack completely. This is considered an "enlightened" file access method. As of Windows Server 2022, this _enlightenment_ functionality is available on both Windows and Linux platforms.

## Configure the devices

### Create namespaces for PMEM devices

In Windows, use the `ipmctl` utility to configure the PMEM disks (referred to as namespaces in Linux). For more information, see [How To Provision Intel&reg; Optane&trade; DC Persistent Memory for KVM/QEMU Guests](https://www.intel.com/content/www/us/en/developer/articles/training/provision-intel-optane-dc-persistent-memory-for-kvm-qemu-guests.html). Details on supported PMEM hardware on different Windows versions are at [Understand and deploy persistent memory](/azure-stack/hci/concepts/deploy-persistent-memory#supported-hardware). PMEM disks should be interleaved across PMEM NVDIMMs and can provide different types of user-space access to memory regions on the device. For more information on interleaved sets in Windows, see [Understand and deploy persistent memory](/azure-stack/hci/concepts/deploy-persistent-memory#understand-interleaved-sets).

## PMEM disks

### Use PowerShell to examine PMEM disks

```powershell
#Get information about all physical disks
Get-PhysicalDisk

#Review logical configuration of PMEM disks
Get-PmemDisk

#Get information about PMEM devices
Get-PmemPhysicalDevice

#Get information about unused PMEM regions
Get-PmemUnusedRegion
```

### BTT and DAX

By default, `New-PmemDisk` will use the desired `FSDax` mode. Atomicity is set to the default of `None` rather than `BlockTranslationTable`. From a support perspective, BTT must be enabled for the transaction log, to mimic required sector mode semantics. Although use of [BTT](/azure-stack/hci/concepts/deploy-persistent-memory#block-translation-table) with NTFS is generally recommended, BTT isn't recommended when using large pages, such as required for [DAX](/windows-server/storage/storage-spaces/persistent-memory-direct-access#dax-and-block-translation-table-btt).

```powershell
Get-PmemUnusedRegion | New-PmemDisk -Atomicity None
```

### Format the NTFS volumes

```powershell
#Initialize PMEM disks
Get-PmemDisk | Initialize-Disk -PartitionStyle GPT

#Create new partitions and format the volumes with DAX Mode
$partition = @{ UseMaximumSize = $true; AssignDriveLetter = $true; Offset = 2097152; Alignment = 2097152 }
$volume = @{ FileSystem = 'NTFS'; IsDAX = $true; AllocationUnitSize = 2097152 }
Get-PmemDisk[0] | New-Partition @partition | Format-Volume @volume
```

## File alignment and offset

### Check partition offsets

```powershell
Get-Partition | Select-Object DiskNumber, DriveLetter, IsDAX, Offset, Size, PartitionNumber | Format-List
```

Check the file alignment of a particular file using `fsutil`. Our file size must be a modulo of 2 MB.

```bash
fsutil dax queryFileAlignment A:\AdventureWorks2022_A.mdf
```

## Replace PMEM

### Reprovision PMEM disks

Whenever a PMEM module is replaced, it needs to be reprovisioned.

> [!NOTE]  
> Removing a PMEM disk will result in the loss of data on that disk.

```powershell
# Remove all PMEM disks
Get-PmemDisk | Remove-PmemDisk -Confirm:$false
```

### Erase PMEM modules

To permanently erase data from PMEM modules, use the `Initialize-PmemPhysicalDevice` PowerShell cmdlet.

```powershell
# Reinitialize all PMEM disks
Get-PmemPhysicalDevice | Initialize-PmemPhysicalDevice -Confirm:$false
```

## Related content

- [PersistentMemory](/powershell/module/persistentmemory/)
