---
title: Configure Persistent Memory (PMEM) on Linux
description: Learn how to configure persistent memory (PMEM) for SQL Server on Linux, and how to create namespaces for PMEM devices
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: derekw, amitkh, atsingh
ms.date: 08/11/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
  - build-2025
monikerRange: ">=sql-server-linux-ver15 || >=sql-server-ver15"
---

# Configure persistent memory (PMEM) for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

This article describes how to configure persistent memory (PMEM) for [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions on Linux.

## Overview

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] adds persistent memory support to accelerate several storage-intensive operations.

With a PMEM-aware file system, memory mapping (`mmap()`) gives user-space applications direct access to file data. When a memory map is created for a file, the application can issue load/store instructions that bypass the storage layer.

> [!NOTE]  
> This direct access is called an *enlightened* file access method from the perspective of the host extension application, which is how SQL Server interacts with the host operating system, using the SQL Platform Abstraction Layer (SQLPAL).

This article shows you how to configure persistent memory for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux.

## Create namespaces for PMEM devices

### Configure the devices

In Linux, use the `ndctl` utility.

- Install `ndctl` to configure PMEM device from [Installing NDCTL](https://docs.pmem.io/persistent-memory/getting-started-guide/installing-ndctl).
- Use `ndctl` to create a namespace. Namespaces are interleaved across PMEM NVDIMMs and can provide different types of user-space access to memory regions on the device. `fsdax` is default and desired mode for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

```bash
ndctl create-namespace -f -e namespace0.0 --mode=fsdax --map=dev
```

The `fsdax` mode stores per-page metadata in system memory. The `--map=dev` option is recommended because it stores the metadata on the namespace directly. Storing metadata in memory with `--map=mem` is experimental.

Use `ndctl` to verify the namespace.

Sample output follows:

```bash
# ndctl list -N
{
  "dev":"namespace0.0",
  "mode":"fsdax",
  "map":"dev",
  "size":4294967296,
  "sector_size":512,
  "blockdev":"pmem0",
  "numa_node":0
}
```

### Create and mount PMEM device

For example, with **XFS**:

```bash
mkfs.xfs -f /dev/pmem0
mount -o dax,noatime /dev/pmem0 /mnt/dax
xfs_io -c "extsize 2m" /mnt/dax
```

For example, with **ext4**:

```bash
mkfs.ext4 -b 4096 -E stride=512 -F /dev/pmem0
mount -o dax,noatime /dev/pmem0 /mnt/dax
```

## Technical considerations

- Block allocation of 2 MB for either XFS or ext4, as described previously
- Misalignment between block allocation and `mmap` results in silent fallback to 4 KB
- File sizes should be a multiple of 2 MB (modulo 2 MB)
- Don't disable transparent huge pages (THP) (enabled by default on most distributions)

After you use `ndctl` to configure, create, and mount the device, you can place database files in it or create a new database.

You can store the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] data files (`.mdf`, `.ndf`) and `tempdb` files on a PMEM device in `fsdax` mode with the following command. Don't use this mode to store the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] log (`.ldf`) files, because the transaction log requires storage that provides sector atomic guarantees:

```bash
ndctl create-namespace -f -e namespace0.0 --mode=fsdax --map=dev
```

Before you set the map option in the preceding command, keep the following points in mind:

- For best performance when accessing and updating these NVDIMM page entries for this device, use `-map=mem`
- If the capacity of the NVDIMM is too large (greater than 512 GB), set `-map=dev`, which affects I/O throughput and reduces performance

For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] log files on PMEM devices, configure the PMEM devices to use sector/Block Translation Table (BTT). This configuration provides the sector atomicity that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] log files require for this storage technology. Perform workload performance validations. Compare the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] log performance for your workload between this solution and best-in-class NVMe SSDs, and then select the one that best meets your needs.

```bash
ndctl create-namespace -f -e namespace0.0 --mode= sector
```

### Disable forced flush behavior

Because PMEM devices are `O_DIRECT` (direct I/O) safe, you can [disable the forced flush behavior](#sql-server-and-forced-unit-access-fua-io-subsystem-capability).

> [!NOTE]  
> A storage system can ensure that any cached or staged writes are safe and durable by guaranteeing that writes to the device reside on a medium that persists across system crashes, interface resets, and power failures, and that the medium itself is hardware redundant.

- Database (`.mdf` and `.ndf`) and transaction log (`.ldf`) files don't use `writethrough` and `alternatewritethrough` by default in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 6 and later versions, because they use the forced flush behavior. Trace flag 3979 disables the forced flush behavior for database and transaction log files, and uses the `writethrough` and `alternatewritethrough` logic.

- Other files that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] opens with `FILE_FLAG_WRITE_THROUGH`, such as database snapshots, internal snapshots for database consistency checks (`DBCC CHECKDB`), profiler trace files, and extended event trace files, use the `writethrough` and `alternatewritethrough` optimizations.

For more information about the changes introduced in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 6, see [KB 4131496](https://support.microsoft.com/help/4131496). For more information about forced unit access (FUA) internals, see [FUA internals](/archive/blogs/bobsql/sql-server-on-linux-forced-unit-access-fua-internals).

#### SQL Server and Forced Unit Access (FUA) I/O subsystem capability

[!INCLUDE [linux-forced-unit-access](../includes/linux-forced-unit-access.md)]

## Related content

- [What is SQL Server on Linux?](../sql-server-linux-overview.md)
- [Performance best practices: Storage, kernel, CPU, and network for SQL Server on Linux](performance-best-practices-operating-system.md)
- [Performance best practices: SQL Server memory on Linux](performance-best-practices-sql-server-memory.md)
