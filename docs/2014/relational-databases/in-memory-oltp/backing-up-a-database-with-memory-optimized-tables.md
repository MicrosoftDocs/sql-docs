---
title: "Backing Up a Database with Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 83d47694-e56d-4dae-b54e-14945bf8ba31
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Backing Up a Database with Memory-Optimized Tables
  Memory-optimized tables are backed up as part of regular database backups. As for disk-based tables, the CHECKSUM of data and delta file pairs is validated as part of the database backup to detect storage corruption.  
  
> [!NOTE]  
>  During a backup, if you detect a CHECKSUM error in one or more files in a memory-optimized filegroup, you will not be able to restore and restart the database. In that situation, you must, restore your database with the last known good backup. If you don't have a backup, you can export the data from memory-optimized tables and disk-based tables and reload after you drop and recreate the database.  
  
 A full backup of a database with one or more memory-optimized tables consists of the allocated storage for disk-based tables (if any), the active transaction log, and the data and delta file pairs (also known as checkpoint file pairs) for memory-optimized tables. However, as described in [Durability for Memory-Optimized Tables](memory-optimized-tables.md), the storage used by memory-optimized tables can be much larger than its size in memory, and it affects the size of the database backup.  
  
## Full Database Backup  
 This discussion will focus on database backups for databases with just durable memory-optimized tables, because the backup for disk-based tables is the same. The checkpoint file pairs in the memory-optimized filegroup could be in various states. The table below describes what part of the files is backed.  
  
|Checkpoint File Pair State|Backup|  
|--------------------------------|------------|  
|PRECREATED|File metadata only|  
|UNDER CONSTRUCTION|File metadata only|  
|Active|File metadata plus used bytes|  
|Merge source|File metadata plus used bytes|  
|Merge target|File metadata only|  
|REQUIRED FOR BACKUP/HA|File metadata plus used bytes|  
|IN TRANSITION TO TOMBSTONE|File metadata only|  
|TOMBSTONE|File metadata only|  
  
 The size of database backups with one or more memory-optimized tables is typically larger than its size in memory but smaller than the on-disk storage. The extra size will depend upon the number of deleted rows and the number of checkpoint file pairs in the states Merge source and REQUIRED FOR BACKUP/HA which indirectly depends upon the workload. For descriptions of the states for checkpoint file pairs, see [sys.dm_db_xtp_checkpoint_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-files-transact-sql).  
  
### Estimating Size of Full Database Backup  
  
> [!IMPORTANT]  
>  It's recommended that you not use the BackupSizeInBytes value to estimate the backup size for In-Memory OLTP.  
  
 The first workload scenario is for (mostly) insert. In this scenario, most data files will be in the Active state, fully loaded, and with very few deleted rows. The size of database backup up will be close to the size of data in memory.  
  
 The second workload scenario is for frequent insert, delete, and update operations: In the worst case, each of the checkpoint file pairs are 50% loaded, after accounting for the deleted rows. So the size of the database backup will at least be 2 times the size of data in memory. Additionally, there will few checkpoint file pairs in states Merge source and Required for backup/high availability that will add to the size of database backup.  
  
## Differential Backups of Databases with Memory-Optimized Tables  
 The storage for memory-optimized tables consists of data and delta files as described in [Durability for Memory-Optimized Tables](memory-optimized-tables.md). The differential backup of a database with memory-optimized tables contains the following data:  
  
-   Differential backup for filegroups storing disk-based tables is not affected by the presence of memory-optimized tables.  
  
-   The active transaction log is the same as in a full database backup.  
  
-   For a memory-optimized data filegroup, the differential backup uses the same algorithm as full database backup to identify the data and delta files for backup but it then filters out the subset of files as follows:  
  
    -   A data file contains newly inserted rows and when it is full, it is closed and marked read-only. A data file is backed up only if it was closed after the last full database backup. The differential backup only backups up data files containing the inserted rows since the last full database backup with the exception of an update and delete scenario where it is possible that some of the inserted rows may have already been either marked for garbage collection or already garbage collected.  
  
    -   A delta file stores references to the deleted data rows. Since any future transaction can delete a row, a delta file can be modified anytime in its life time, it is never closed. A delta file is always backed up. Delta files typically use less than 10% of the storage, so delta files have a minimal impact on the size of differential backup.  
  
 If memory-optimized tables are a significant portion of your database size, the differential backup can significantly reduce the size of your database backup.  
  
 For typical OLTP workloads, the differential backups will be considerably smaller than the full database backups.  
  
## See Also  
 [Backup, Restore, and Recovery of Memory-Optimized Tables](restore-and-recovery-of-memory-optimized-tables.md)  
  
  
