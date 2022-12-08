---
title: Configure persistent memory (PMEM) - Linux
description: Learn how to configure persistent memory (PMEM) for SQL Server on Linux, and how to create namespaces for PMEM devices
ms.custom: seo-lt-2019
author: briancarrig 
ms.author: brcarrig
ms.date: 09/21/2021
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
monikerRange: ">= sql-server-linux-ver15  || >= sql-server-ver15"
---
# Configure persistent memory (PMEM) for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes how to configure the persistent memory (PMEM) for [!INCLUDE[sqlv15](../includes/sssql19-md.md)] on Linux.

## Overview

[!INCLUDE[sqlv15](../includes/sssql19-md.md)] has many in-memory features that use persistent memory. This document covers the steps required to configure persistent memory for SQL Server on Linux.

> [!NOTE]
> The term _enlightenment_ was introduced to convey the concept of working with a persistent memory aware file system. Direct access to the file system from user-space applications is facilitated using memory mapping (`mmap()`). When a memory mapping for a file is created the application can issue load/store instructions bypassing the I/O stack completely. This is considered an "enlightened" file access method from the perspective of the host extension application (which is the black box code that allows SQLPAL interact with the Windows or Linux OS).

## Create namespaces for PMEM devices

### Configure the devices

In Linux, use the `ndctl` utility.

- Install `ndctl` to configure PMEM device. You can find it [here](https://docs.pmem.io/getting-started-guide/installing-ndctl).
- Use `ndctl` to create a namespace. Namespaces are interleaved across PMEM NVDIMMs and can provide different types of user-space access to memory regions on the device. `fsdax` is default and desired mode for SQL Server.

```bash 
ndctl create-namespace -f -e namespace0.0 --mode=fsdax* --map=dev
```

We have chosen `fsdax` mode and are using system memory to store per-page metadata. We recommend using `--map=dev`. This stores the meta data on the namespace directly. Storing meta data in memory using `--map=mem` is considered experimental at this time.

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

- Block allocation of 2 MB for either XFS/EXT4, as described above
- Misalignment between block allocation and `mmap` results in silent fallback to 4 KB
- File sizes should be a multiple of 2 MB (modulo 2 MB)
- Do not disable transparent huge pages (THP) (enabled by default on most distros)

Once the device has been configured with `ndctl`, created, and mounted, you can place database files in it or create a new database.

You can store the SQL Server data files (MDFS, NDFS) and tempdb files on a PMEM device when configured with the mode `fsdax` using the command below. This shouldn't be used to store the SQL Server log (LDFS) files, as transaction log needs to be on storage that provides sector atomic guarantees:

```bash
ndctl create-namespace -f -e namespace0.0 --mode=fsdax --map=dev
```

Before you set the map option in the preceding command, keep the following points in mind:

- For best performance at accessing and updating these NVDIMM page entries for this device, it's preferable to use `-map=mem `
- If the capacity of the NVDIMM is too large (greater than 512 GB), set the `–map=dev`, which would impact the IO throughput and stymie the performance

For SQL Server log files on PMEM devices, provision the PMEM device(s) to use sector/Block Translation Table (BTT). This provides the needed sector atomicity for SQL Server logs files for this technology of storage devices. We also recommend that you perform workload performance validations and compare the SQL Server log performance for your workload between this solution and best-in-class NVMe SSDs, and then select the solution that best meets your needs and provides better performance.

```bash
ndctl create-namespace -f -e namespace0.0 --mode= sector
```

Because PMEM devices are O_DIRECT (direct I/O) safe, consider enabling trace flag 3979 to disable the forced flush mechanism. For more information, see [FUA support](https://support.microsoft.com/help/4131496/enable-forced-flush-mechanism-in-sql-server-2017-on-linux). Forced unit access internals are covered here [FUA internals](/archive/blogs/bobsql/sql-server-on-linux-forced-unit-access-fua-internals).

## Next steps

For more information about SQL Server on Linux, see [SQL Server on Linux](sql-server-linux-overview.md).
For performance best practices for SQL Server on Linux, see [Performance Best Practices](sql-server-linux-performance-best-practices.md).
