---
title: "Hardware for SQL In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "11/30/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=sqlallproducts-allversions"
---
# Hardware considerations for In-Memory OLTP in SQL Server 2014

In-Memory OLTP uses memory and disk in different ways than traditional disk-based tables. The  performance improvement you will see with In-Memory OLTP depends the hardware you use. In this blog post we discuss a number of general hardware considerations, and provide generic guidelines for hardware to use with In-Memory OLTP.

> [!NOTE]
> This article was a blog published on August 1, 2013, by the Microsoft SQL Server 2014 team. The blog webpage is being retired, and this article is a rough capture of the blog text. Documentation articles that used to link to the blog now link this this article. This article is not being maintained. This article may be excluded from the Table of Contents.
> 
> [SQL Server In-Memory-OLTP](index.md)

<!--
    Here was the link to the blog. This blog was captured into this new article on 2018/11/30, by GeneMi (MightyPen).
    https://cloudblogs.microsoft.com/sqlserver/2013/08/01/hardware-considerations-for-in-memory-oltp-in-sql-server-2014/
    At least one pre-existing article that contained the obsolete blog link was:
        relational-databases\in-memory-oltp\sample-database-for-in-memory-oltp.md
 -->

## CPU

In-Memory OLTP does not require a high-end server to support a high-throughput OLTP workload. We recommend using a mid-range server with 2 CPU sockets. Due to the increased throughput enabled by In-Memory OLTP, 2 sockets are likely going to be enough for your business needs.

We recommend to turn hyper-threading ON with in-memory OLTP. With some OLTP workloads we have seen performance gains of up to 40% when using hyper-threading.

## Memory

All memory-optimized tables reside fully in memory. Therefore, you must have enough physical memory for the tables themselves and to sustain the workload running against the database – how much memory you actually need really depends on the workload, but as a starting point you will probably want enough available memory for about 2X the data size. You will also need enough memory for the buffer pool in case the workload also operates on traditional disk-based tables.

To determine how much memory a given memory-optimized table uses, run the following query:

```sql
select object_name(object_id), * from sys.dm_db_xtp_table_memory_stats
```

The results will show the memory used for memory-optimized tables and their indexes. The table data includes the user data, as well as all the older row versions that are still required by running transactions or have not yet been cleaned up by the system. The memory used by hash indexes is constant, and does not depend on the number of rows in the table.

It is important to keep in mind when you use in-memory OLTP that your whole database does not need to fit in memory. You can have a multi-Terabyte database and still benefit from in-memory OLTP, as long as the size of your hot data (i.e., the memory-optimized tables) does not exceed 256GB. The maximum number of checkpoint data files SQL Server can manage for a single database is 4000, with each file being 128MB. Although this would give a theoretical maximum of 512GB, in order to guarantee that SQL Server can keep up with merging checkpoint files and not hit the limit of 4000 files, we support up to 256GB. Note that this limit applies only the memory-optimized tables; there is no such size limitation on the traditional disk-based tables in the same SQL Server database.

Non-durable memory-optimized tables (NDTs), i.e., memory-optimized tables with DURABILITY=SCHEMA_ONLY are not persisted on disk. Although NDTs are not limited by the number of checkpoint files, only 256GB is supported. The considerations for log and data drives in the remainder of this post do not apply to non-durable tables, as the data exists only in memory.

## Log drive

Log records pertaining to memory-optimized tables are written to the database transaction log, along with the other SQL Server log records.

It is always important to put the log file on a drive that has low latency, such that transactions do not need to wait too long, and to prevent contention on log IO. Your system will run as fast as your slowest component (Amdahl’s law). You need to ensure that, when running In-Memory OLTP, your log IO device does not become a bottleneck. We recommend using a storage device with low latency, at least SSD.

Note that memory-optimized tables use less log bandwidth than disk-based tables, as they do not log index operations and do not log UNDO records. This can help to relieve log IO contention.

## Data drive

Persistence of memory-optimized tables using checkpoint files uses streaming IO. Therefore, these files do not need a drive with low latency or fast random IO. Instead, the main factor for these drives is the speed of sequential IO and bandwidth of the host bus adapter (HBA). Thus, you don’t need SSDs for checkpoint files; you can place them on high performance spindles (e.g., SAS), as long as their sequential IO speed meets your requirements.

The biggest factor in determining the speed requirement is your RTO [Recovery Time Objective] on server restart. During database recovery, all data in the memory-optimized tables needs to be read from disk, into memory. Database recovery happens at the sequential read speed of your IO subsystem; disk is the bottleneck.

To meet strict RTO requirements we recommend to spread the checkpoint files over multiple disks, by adding multiple containers to the MEMORY_OPTIMIZED_DATA filegroup. SQL Server supports parallel load of checkpoint files from multiple drives – recovery happens at the aggregate speed of the drives.

In terms of disk capacity, we recommend to have 2-3X the size of the memory-optimized tables available.

