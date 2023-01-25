---
title: "Checkpoint Operation for Memory-Optimized Tables"
description: Learn about checkpoints for memory-optimized tables in SQL Server. The memory-optimized table checkpoint operation is distinct from that of disk-based tables.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: 47975bd5-373f-43cd-946a-da8e8088b610
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Checkpoint Operation for Memory-Optimized Tables
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  A checkpoint needs to occur periodically for memory-optimized data in data and delta files to advance the active part of transaction log. The checkpoint allows memory-optimized tables to restore or recover to the last successful checkpoint and then the active portion of transaction log is applied to update the memory-optimized tables to complete the recovery. The checkpoint operation for disk-based tables and memory-optimized tables are distinct operations. The following describes different scenarios and the checkpoint behavior for disk-based and memory-optimized tables:  
  
## Manual Checkpoint  
 When you issue a manual checkpoint, it closes the checkpoint for both disk-based and memory-optimized tables. The active data file is closed even though it may be partially filled.  
  
## Automatic Checkpoint  
 Automatic checkpoint is implemented differently for disk-based and memory-optimized tables because of the different ways the data is persisted.  
  
 For disk-based tables, an automatic checkpoint is taken based on the recovery interval configuration option (for more information, see [Change the Target Recovery Time of a Database &#40;SQL Server&#41;](../../relational-databases/logs/change-the-target-recovery-time-of-a-database-sql-server.md)).  
  
 For memory-optimized tables, an automatic checkpoint is taken when transaction log file becomes bigger than 1.5 GB since the last checkpoint. This 1.5 GB size  includes transaction log records for both disk-based and memory-optimized tables.  
  
## See Also  
 [Creating and Managing Storage for Memory-Optimized Objects](../../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md)  
  
  
