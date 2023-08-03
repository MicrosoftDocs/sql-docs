---
title: Configure persistent memory (PMEM) - Linux
description: Learn how to configure persistent memory (PMEM) for SQL Server on Linux, and how to create namespaces for PMEM devices
author: briancarrig
ms.author: brcarrig
ms.reviewer: randolphwest
ms.date: 02/21/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
monikerRange: ">= sql-server-linux-ver15  || >= sql-server-ver15"
---

# Configure persistent memory (PMEM) for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes how to configure the persistent memory (PMEM) for [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] and later versions on Linux.

## Overview

[!INCLUDE [sssql19-md](../includes/sssql19-md.md)] introduced many in-memory features that use persistent memory. This article covers the steps required to configure persistent memory for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux.

> [!NOTE]  
> The term _enlightenment_ was introduced to convey the concept of working with a persistent memory aware file system. Direct access to the file system from user-space applications is facilitated using memory mapping (`mmap()`). When a memory mapping for a file is created the application can issue load/store instructions bypassing the I/O stack completely. This is considered an "enlightened" file access method from the perspective of the host extension application (which is the code that allows SQLPAL interact with the Windows or Linux OS).

## Create namespaces for PMEM devices

### Configure the devices

In Linux, use the `ndctl` utility.

- Install `ndctl` to configure PMEM device. You can find it [here](https://docs.pmem.io/persistent-memory/getting-started-guide/installing-ndctl).
- Use `ndctl` to create a namespace. Namespaces are interleaved across PMEM NVDIMMs and can provide different types of user-space access to memory regions on the device. `fsdax` is default and desired mode for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

```bash
ndctl create-namespace -f -e namespace0.0 --mode=fsdax --map=dev
```

We have chosen `fsdax` mode and are using system memory to store per-page metadata. We recommend using `--map=dev`. This option stores the meta data on the namespace directly. Storing meta data in memory using `--map=mem` is experimental at this time.

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

For example, with XFS

```bash
mkfs.xfs -f /dev/pmem0
mount -o dax,noatime /dev/pmem0 /mnt/dax
xfs_io -c "extsize 2m" /mnt/dax
```

For example, with EXT4

```bash
mkfs.ext4 -b 4096 -E stride=512 -F /dev/pmem0
mount -o dax,noatime /dev/pmem0 /mnt/dax
```

## Technical considerations

- Block allocation of 2 MB for either XFS/EXT4, as described previously
- Misalignment between block allocation and `mmap` results in silent fallback to 4 KB
- File sizes should be a multiple of 2 MB (modulo 2 MB)
- Don't disable transparent huge pages (THP) (enabled by default on most distributions)

Once the device is configured with `ndctl`, created, and mounted, you can place database files in it or create a new database.

You can store the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data files (MDFS, NDFS) and `tempdb` files on a PMEM device when configured with the mode `fsdax` using the following command. Don't use this to store the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] log (LDFS) files, as transaction log needs to be on storage that provides sector atomic guarantees:

```bash
ndctl create-namespace -f -e namespace0.0 --mode=fsdax --map=dev
```

Before you set the map option in the preceding command, keep the following points in mind:

- For best performance at accessing and updating these NVDIMM page entries for this device, it's preferable to use `-map=mem`
- If the capacity of the NVDIMM is too large (greater than 512 GB), set the `–map=dev`, which would impact the IO throughput and stymie the performance

For [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] log files on PMEM devices, con the PMEM device(s) to use sector/Block Translation Table (BTT). This provides the needed sector atomicity for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] logs files for this technology of storage devices. We also recommend that you perform workload performance validations. You can compare the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] log performance for your workload between this solution and best-in-class NVMe SSDs, and then select the solution that best meets your needs and provides better performance.

```bash
ndctl create-namespace -f -e namespace0.0 --mode= sector
```

### Disable forced flush behavior

Because PMEM devices are `O_DIRECT` (direct I/O) safe, you can [disable the forced flush behavior](#sql-server-and-forced-unit-access-fua-io-subsystem-capability).

> [!NOTE]  
> A storage system can make sure that any cached or staged writes are considered safe and durable, by guaranteeing that writes issued to the device are kept on a medium that will persist across system crashes, interface resets and power failures, and the medium itself is hardware redundant.

- Database (`.mdf` and `.ndf`) and transaction log (`.ldf`) files don't use `writethrough` and `alternatewritethrough` by default in [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 6 and later versions, because they use the forced flush behavior. Trace Flag 3979 disables the use of the forced flush behavior for database and transaction log files, and uses the `writethrough` and `alternatewritethrough` logic.

- Other files that are opened by using `FILE_FLAG_WRITE_THROUGH` in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], such as database snapshots, internal snapshots for database consistency checks (`DBCC CHECKDB`), profiler trace files, and extended event trace files, use the `writethrough` and `alternatewritethrough` optimizations.

For more information about the changes introduced in [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 6, see [KB 4131496](https://support.microsoft.com/help/4131496). For more information about forced unit access (FUA) internals, see [FUA internals](/archive/blogs/bobsql/sql-server-on-linux-forced-unit-access-fua-internals).

#### SQL Server and Forced Unit Access (FUA) I/O subsystem capability

[!INCLUDE [linux-forced-unit-access](includes/linux-forced-unit-access.md)]

## Next steps

- [SQL Server on Linux](sql-server-linux-overview.md)
- [Performance Best Practices](sql-server-linux-performance-best-practices.md)
