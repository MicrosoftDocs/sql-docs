---
title: "Checkpoint Operation for Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 47975bd5-373f-43cd-946a-da8e8088b610
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Checkpoint Operation for Memory-Optimized Tables
  A checkpoint needs to occur periodically for memory-optimized data in data and delta files to advance the active part of transaction log. The checkpoint allows memory-optimized tables to restore or recover to the last successful checkpoint and then the active portion of transaction log is applied to update the memory-optimized tables to complete the recovery. The checkpoint operation for disk-based tables and memory-optimized tables are distinct operations. The following describes different scenarios and the checkpoint behavior for disk-based and memory-optimized tables:  
  
## Manual Checkpoint  
 When you issue a manual checkpoint, it closes the checkpoint for both disk-based and memory-optimized tables. The active data file is closed even though it may be partially filled.  
  
## Automatic Checkpoint  
 Automatic checkpoint is implemented differently for disk-based and memory-optimized tables because of the different ways the data is persisted.  
  
 For disk-based tables, an automatic checkpoint is taken based on the recovery interval configuration option (for more information, see [Change the Target Recovery Time of a Database &#40;SQL Server&#41;](../logs/change-the-target-recovery-time-of-a-database-sql-server.md)).  
  
 For memory-optimized tables, an automatic checkpoint is taken when transaction log file becomes bigger than 512 MB since the last checkpoint. 512 MB includes transaction log records for both disk-based and memory-optimized tables.  
  
## See Also  
 [Creating and Managing Storage for Memory-Optimized Objects](creating-and-managing-storage-for-memory-optimized-objects.md)  
  
  
