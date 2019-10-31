---
title: How to configure persistent memory (PMEM) for SQL Server on Linux
description: This article provides a walk-through for configuring PMEM on Linux.
author: briancarrig 
ms.author: brcarrig
ms.date: 10/31/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
monikerRange: ">= sql-server-linux-ver15  || >= sql-server-ver15 || = sqlallproducts-allversions"
---
# How to configure persistent memory (PMEM) for SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article describes how to configure persistent memory (PMEM) for SQL Server on Linux.

## Overview

[!INCLUDE[sqlv15](../includes/sssqlv15-md.md)] has a number of in-memory features that use persistent memory. This document covers the steps required to configure persistent memory for SQL Server on Linux.

> [!NOTE]
> The term "enlightenment" was introduced to convey the concept of working with a persistent memory aware file system which provides direct access to applications in user-space. When a file is memory mapped from this type of file system (XFS/EXT4) load/store, non-paged access to the file is provided. The application can use memory regions on this file system in a manner similar to memory regions in volatile memory.

## Create namespaces for PMEM devices

1. Configure the devices.

  In Linux, use the `ndctl` utility.

  - Install `ndctl` to configure PMEM device. You can find it [here](https://docs.pmem.io/getting-started-guide/installing-ndctl).
  - Use `ndctl` to create a namespace. Namespaces are interleaved across PMEM NVDIMMs and can provide different types of user-space access to memory regions on the device. `fsdax` is default and desired mode for SQL Server.

  ```bash 
  ndctl create-namespace -f -e namespace0.0 --mode=fsdax* --map=mem
  ```
  Note that we have chosen `fsdax` mode and are using system memory to store per-page metadata. With very large persistent memory devices and considerably smaller amounts of system memory, it might be advisable to consider `--map=dev`. This will store the meta data on the namespace directly.

  Use `ndctl` to verify the namespace. 
  
  Sample output follows:

```bash
ndctl list
[
  {
    "dev":"namespace0.0",
    "mode":"memory",
    "size":1099511627776,
    "blockdev":"pmem0",
    "numa_node":0
  }
]
```

  - Create and mount PMEM device

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

    Important to note the following

    - Block allocation of 2MB for either XFS/EXT4 as described above.
    - Misalignment between block allocation and mmap will result in silent fallback to 4KB.
    - Transparent Huge Pages should be enabled.
    - Huge pagefaults should be set to 2MB.

  Once the device has been configured with ndctl, formatted and mounted, you can place database files in it. You can also create a new database.

1. Since PMEM devices are O_DIRECT safe, enable trace flag 3979 to disable the forced flush mechanism. This trace flag is a startup trace flag, and as such needs to be enabled using the mssql-conf utility. Please note that this is a server-wide configuration change, and you should not use this trace flag if you have any O_DIRECT non-compliant devices that need the forced flush mechanism to ensure data integrity. For more information see https://support.microsoft.com/en-us/help/4131496/enable-forced-flush-mechanism-in-sql-server-2017-on-linux

1. Restart SQL Server.

## Next steps

For more information about SQL Server on Linux, see [SQL Server on Linux](sql-server-linux-overview.md).
