---
title: Hardware for SQL In-Memory OLTP
description: Learn about hardware considerations for In-Memory OLTP performance in SQL Server. In-Memory OLTP uses memory and disk in different ways than disk-based tables.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/27/2023
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-2016 || >=sql-server-linux-2017"
---
# Hardware considerations for In-Memory OLTP in SQL Server

In-Memory OLTP uses memory and disk in different ways than traditional disk-based tables. The performance improvement you see with In-Memory OLTP depends the hardware you use. In this article, we discuss several general hardware considerations, and provide generic guidelines for hardware to use with In-Memory OLTP.

> [!NOTE]  
> This article was a blog published on August 1, 2013, by the Microsoft SQL Server 2014 team. The blog webpage is being retired.
>  
> [SQL Server In-Memory-OLTP](./overview-and-usage-scenarios.md)

## CPU

In-Memory OLTP doesn't require a high-end server to support a high-throughput OLTP workload. We recommend using a mid-range server with two CPU sockets. Due to the increased throughput enabled by In-Memory OLTP, two sockets are likely going to be enough for your business needs.

We recommend turning on simultaneous multithreading (SMT) with In-Memory OLTP. With some OLTP workloads, we have seen performance gains of up to 40% when using SMT.

## Memory

All memory-optimized tables reside fully in memory. Therefore, you must have enough physical memory for the tables themselves and to sustain the workload running against the database – how much memory you actually need really depends on the workload, but as a starting point you need enough available memory for about twice the data size. You also need enough memory for the buffer pool in case the workload also operates on traditional disk-based tables.

To determine how much memory a given memory-optimized table uses, run the following query:

```sql
select object_name(object_id), * from sys.dm_db_xtp_table_memory_stats;
```

The results show the memory used for memory-optimized tables and their indexes. The table data includes the user data, and all the older row versions that are still required by running transactions, or haven't yet been cleaned up by the system. The memory used by hash indexes is constant, and doesn't depend on the number of rows in the table.

It's important to keep in mind when you use In-Memory OLTP that your whole database doesn't need to fit in memory. You can have a multi-Terabyte database and still benefit from In-Memory OLTP, as long as the size of your hot data (that is, the memory-optimized tables) doesn't exceed 256 GB. The maximum number of checkpoint data files that SQL Server can manage for a single database is 4,000, with each file being 128 MB. Although this would give a theoretical maximum of 512 GB, in order to guarantee that SQL Server can keep up with merging checkpoint files and not hit the limit of 4,000 files, we support up to 256 GB. This limit applies only the memory-optimized tables; there's no such size limitation on the traditional disk-based tables in the same SQL Server database.

Non-durable memory-optimized tables (NDTs), that is, memory-optimized tables with DURABILITY=SCHEMA_ONLY aren't persisted on disk. Although NDTs aren't limited by the number of checkpoint files, only 256 GB is supported. The considerations for log and data drives in the remainder of this post don't apply to non-durable tables, as the data exists only in memory.

## Log drive

Log records pertaining to memory-optimized tables are written to the database transaction log, along with the other SQL Server log records.

It's always important to put the log file on a drive that has low latency, such that transactions don't need to wait too long, and to prevent contention on log I/O. Your system runs as fast as your slowest component (Amdahl's law). You need to ensure that, when running In-Memory OLTP, your log I/O device doesn't become a bottleneck. We recommend using a storage device with low latency, at least SSD.

Memory-optimized tables use less log bandwidth than disk-based tables, as they don't log index operations and don't log UNDO records. This can help to relieve log I/O contention.

## Data drive

Persistence of memory-optimized tables using checkpoint files uses streaming I/O. Therefore, these files don't need a drive with low latency or fast random I/O. Instead, the main factor for these drives is the speed of sequential I/O and bandwidth of the host bus adapter (HBA). Thus, you don't need SSDs for checkpoint files; you can place them on high performance spindles (for example, SAS), as long as their sequential I/O speed meets your requirements.

The biggest factor in determining the speed requirement is your RTO [Recovery Time Objective] on server restart. During database recovery, all data in the memory-optimized tables needs to be read from disk, into memory. Database recovery happens at the sequential read speed of your I/O subsystem; disk is the bottleneck.

To meet strict RTO requirements, we recommend spreading the checkpoint files over multiple disks, by adding multiple containers to the MEMORY_OPTIMIZED_DATA filegroup. SQL Server supports parallel load of checkpoint files from multiple drives – recovery happens at the aggregate speed of the drives.

In terms of disk capacity, we recommend having 2 - 3 times the size of the memory-optimized tables available.

## Related content

- [Sample Database for In-Memory OLTP](sample-database-for-in-memory-oltp.md)
