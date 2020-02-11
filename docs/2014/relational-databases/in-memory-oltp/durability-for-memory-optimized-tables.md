---
title: "Durability for Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: d304c94d-3ab4-47b0-905d-3c8c2aba9db6
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Durability for Memory-Optimized Tables
  [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] provides full durability for memory-optimized tables. When a transaction that changed a memory-optimized table commits, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (as it does for disk-based tables), guarantees that the changes are permanent (will survive a database restart), provided the underlying storage is available. There are two key components of durability: transaction logging and persisting data changes to on-disk storage.  
  
## Transaction Log  
 All changes made to disk-based tables or durable memory-optimized tables are captured in one or more transaction log records. When a transaction commits, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] writes the log records associated with the transaction to disk before communicating to the application or user session that the transaction has committed. This guarantees that changes made by the transaction are durable. The transaction log for memory-optimized tables is fully integrated with the same log stream used by disk-based tables. This integration allows existing transaction log backup, recover, and restore operations to continue to work without requiring any additional steps. However, since [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] can increase transaction throughput of your workload significantly, you need to make sure that transaction log storage is configured appropriately to handle the increased IO requirements.  
  
## Data and Delta Files  
 The data in memory-optimized tables is stored as free-form data rows that are linked through one or more in-memory indexes, in memory. There are no page structures for data rows, such as those used for disk-based tables. When the application is ready to commit the transaction, the [!INCLUDE[hek_2](../../../includes/hek-2-md.md)] generates the log records for the transaction. The persistence of memory-optimized tables is done with a set of data and delta files using a background thread. The data and delta files are located in one or more containers (using the same mechanism used for FILESTREAM data). These containers are mapped to a new type of filegroup, called a memory-optimized filegroup.  
  
 Data is written to these files in a strictly sequential fashion, which minimizes disk latency for spinning media. You can use multiple containers on different disks to distribute the I/O activity. Data and delta files in multiple containers on different disks will increase recovery performance when data is read from the data and delta files on disk, into memory.  
  
 An application does not directly access data and delta files. All data reads and writes use in-memory data.  
  
### The Data File  
 A data file contains rows from one or more memory-optimized tables that were inserted by multiple transactions as part of INSERT or UPDATE operations. For example, one row can be from memory-optimized table T1 and the next row can be from memory-optimized table T2. The rows are appended to the data file in the order of transactions in the transaction log, making data access sequential. This enables an order of magnitude better I/O throughput compared to random I/O. Each data file is sized approximately to 128MB for computers with memory greater than 16GB, and 16MB for computers with less than or equal to 16GB. Once the data file is full, the rows inserted by new transactions are stored in another data file. Over time, the rows from durable memory-optimized tables are stored in one of more data files and each data file containing rows from a disjoint but contiguous range of transactions. For example a data file with transaction commit timestamp in the range of (100, 200) has all the rows inserted by transactions that have commit timestamp greater than 100 and less than or equal to 200. The commit timestamp is a monotonically increasing number assigned to a transaction when it is ready to commit. Each transaction has a unique commit timestamp.  
  
 When a row is deleted or updated, the row is not removed or changed in-place in the data file but the deleted rows are tracked in another type of file: the delta file. Update operations are processed as a tuple of delete and insert operations for each row. This eliminates random IO on the data file.  
  
### The Delta File  
 Each data file is paired with a delta file that has the same transaction range and tracks the deleted rows inserted by transactions in the transaction range. This data and delta file is referred to as a Checkpoint File Pair (CFP) and it is the unit of allocation and deallocation as well as the unit for Merge operations. For example, a delta file corresponding to transaction range (100, 200) will store deleted rows that were inserted by transactions in the range (100, 200). Like data files, the delta file is accessed sequentially.  
  
 When a row is deleted, the row is not removed from the data file but a reference to the row is appended to the delta file associated with the transaction range where this data row was inserted. Since the row to be deleted already exists in the data file, the delta file only stores the reference information `{inserting_tx_id, row_id, deleting_tx_id }` and it follows the transactional log order of the originating delete or update operations.  
  
## Populating Data and Delta Files  
 Data and delta file are populated by a background thread called offline checkpoint. This thread reads the transaction log records generated by committed transactions on memory-optimized tables and appends information about the inserted and deleted rows into appropriate data and delta files. Unlike disk-based tables where data/index pages are flushed with random I/O when checkpoint is done, the persistence of memory-optimized table is continuous background operation. Multiple delta files are accessed because a transaction can delete or update any row that was inserted by any previous transaction. Deletion information is always appended at the end of the delta file. For example, a transaction with a commit timestamp of 600 inserts one new row and deletes rows inserted by transactions with a commit timestamp of 150, 250 and 450 as shown in the picture below. All 4 file I/O operations (three for deleted rows and 1 for the newly inserted rows), are append-only operations to the corresponding delta and data files.  
  
 ![Read log records for memory-optimized tables.](../../database-engine/media/read-logs-hekaton.gif "Read log records for memory-optimized tables.")  
  
## Accessing Data and Delta Files  
 Data and delta file pairs are accessed when the following occurs.  
  
 Offline checkpoint thread  
 This thread appends inserts and deletes to memory-optimized data rows, to the corresponding data and delta file pairs.  
  
 Merge operation  
 The operation merges one or more data and delta file pairs and creates a new data and delta file pair.  
  
 During crash recovery  
 When [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is restarted or the database is brought back online, the memory-optimized data is populated using the data and delta file pairs. The delta file acts as a filter for the deleted rows when reading the rows from the corresponding data file. Because each data and delta file pair is independent, these files are loaded in parallel to reduce the time taken to populate data into memory. Once the data has been loaded into memory, the In-Memory OLTP engine applies the active transaction log records not yet covered by the checkpoint files so that the memory-optimized data is complete.  
  
 During restore operation  
 The In-Memory OLTP checkpoint files are created from the database backup, and then one or more transaction log backups are applied. As with crash recovery, the In-Memory OLTP engine loads data into memory in parallel, to minimize the impact on recovery time.  
  
## Merging Data and Delta Files  
 The data for memory optimized tables is stored in one or more data and delta file pairs (also called a checkpoint file pair, or CFP). Data files store inserted rows and delta files reference deleted rows. During the execution of an OLTP workload, as the DML operations update, insert, and delete rows, new CFPs are created to persist the new rows, and the reference to the deleted rows is appended to delta files.  
  
 The metadata of all previously-closed and currently active CFPs is stored in an internal array structure referred to as the storage array. It is a finitely sized (8,192 entries) array of CFPs. The entries in the storage array are ordered by transaction range. The CFPs in the storage array (along with the tail of the log) represent all the on-disk state required to recover a database with memory-optimized tables.  
  
 Over time, with DML operations, the number of CFPs grow causing the storage array to reach capacity, which introduces the following challenges:  
  
-   Deleted rows.  Deleted rows remain in the data file but are marked as deleted in the corresponding delta file. These rows are no longer needed and will be removed from the storage. If deleted rows were not removed from CFPs, they would use space unnecessarily and make recovery time slower.  
  
-   Storage array full. When there 8,000 entries in the storage array are allocated (192 entries in the array are reserved for existing merges to compete or to allow you to do manual merges), no new DML transactions can be executed on durable memory-optimized tables. Only checkpoint and merge operations are allowed to consume the remaining entries. This ensures that DML transactions do not fill the array and that some entries in the array are reserved to merge existing files and to reclaim space in the array.  
  
-   Storage array manipulation overhead. Internal processes search the storage array for operations such as finding the delta file to append information about a deleted row. The cost of these operations increases with the number of entries.  
  
 To help prevent these inefficiencies, the older closed CFPs are merged, based on a merge policy described below, so the storage array is compacted to represent the same set of data, with a reduced number of CFPs.  
  
 The total in-memory size of all durable tables in a database should not exceed 250 GB. Durable tables that use up to 250 GB of memory will, assuming insert, delete, and update operations, require on average 500 GB of storage space. 4,000 data and delta file pairs in the memory-optimized file group are required to support the 500 GB of storage space.  
  
 Short-term surges in database activity may cause checkpoint and merge operations lag, which will increase the number of required data and delta file pairs. To accommodate short-term surges spikes in database activity, the storage system can allocate up to 8,000 data and delta file pairs up to a total of 1TB of storage. When that limit is reached, there will be no new transactions allowed on the database until checkpoint operations catch up. If the size of durable tables in memory exceeds 250GB for long periods of time, there is a chance of reaching the 8,000 file pair limit.  
  
 The merge operation takes as input one or more adjacent closed CFPs (called merge source) based on an internally defined merge policy, and produces one resultant CFP, called the merge target. The entries in each delta file of the source CFPs are used to filter rows from the corresponding data file to remove the data rows that are not needed. The remaining rows in the source CFPs are consolidated into one target CFP. After the merge is complete, the resultant merge-target CFP replaces the source CFPs (merge sources). The merge-source CFPs go through a transition phase before they are removed from storage.  
  
 In the example below, the memory-optimized table file group has four data and delta file pairs at timestamp 500 containing data from previous transactions. For example, the rows in the first data file correspond to transactions with timestamp greater than 100 and less than or equal to 200; alternatively represented as (100, 200]. The second and third data files are shown to be less than 50 percent full after accounting for the rows marked as deleted. The merge operation combines these two CFPs and creates a new CFP containing transactions with timestamp greater than 200 and less than or equal to 400, which is the combined range of these two CFPs. You see another CFP with range (500, 600] and non-empty delta file for transaction range (200, 400] shows that merge operation can be done concurrently with transactional activity including deleting more rows from the source CFPs.  
  
 ![Diagram shows memory optimized table file group](../../database-engine/media/storagediagram-hekaton.png "Diagram shows memory optimized table file group")  
  
 A background thread evaluates all closed CFPs using a merge policy and then initiates one or more merge requests for the qualifying CFPs. These merge requests are processed by the offline checkpoint thread. The evaluation of merge policy is done periodically and also when a checkpoint is closed.  
  
### [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] Merge Policy  
 [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] implements the following merge policy:  
  
-   A merge is scheduled if 2 or more consecutive CFPs can be consolidated, after accounting for deleted rows, such that the resultant rows can fit into 1 CFP of ideal size. The ideal size of CFP is determined as follows:  
  
    -   If a computer has less than or equal to 16GB of memory, the data file is 16MB and delta file is 1MB.  
  
    -   If a computer has greater than 16GB of memory, the data file is 128MB and delta file is 16MB.  
  
-   A single CFP can be self-merged if the data file exceeds 256 MB and more than half of the rows are deleted. A data file can grow larger than 128MB if, for example, a single transaction or multiple concurrent transactions inserts or updates large amount of data, forcing the data file to grow beyond its ideal size because a transaction cannot span multiple CFPs.  
  
 Here are some examples that show the CFPs that will be merged under the merge policy:  
  
|Adjacent CFPs Source Files (% full)|Merge Selection|  
|-------------------------------------------|---------------------|  
|CFP0 (30%), CFP1 (50%), CFP2 (50%), CFP3 (90%)|(CFP0, CFP1)<br /><br /> CFP2 is not chosen as it will make resultant data file greater than 100% of the ideal size.|  
|CFP0 (30%), CFP1 (20%), CFP2 (50%), CFP3 (10%)|(CFP0, CFP1, CFP2). Files are chosen starting from left.<br /><br /> CTP3 is not chosen as it will make resultant data file greater than 100% of the ideal size.|  
|CFP0 (80%), CFP1 (30%), CFP2 (10%), CFP3 (40%)|(CFP1, CFP2, CFP3). Files are chosen starting from left.<br /><br /> CFP0 is skipped because if combined with CFP1, the resultant data file will be greater than 100% of the ideal size.|  
  
 Not all CFPs with available space qualify for merge. For example, if two adjacent CFPs are 60% full, they will not qualify for merge and each of these CFPs will have 40% storage unused. In the worst case, all CFPs will be 50% full, a storage utilization of only 50%. While the deleted rows may exist in storage because the CFPs don't qualify for merge, the deleted rows may have already been removed from memory by in-memory garbage collection. The management of storage and the memory is independent from garbage collection. Storage taken by active CFPs (not all CFPs are being updated) can be up to 2 times larger than the size of durable tables in memory.  
  
 If needed, a manual merge can be explicitly performed by calling [sys.sp_xtp_merge_checkpoint_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-merge-checkpoint-files-transact-sql).  
  
### Life Cycle of a CFP  
 CPFs transition through several states before they can be deallocated. At any given time, the CFPs are in one of the following phases: PRECREATED, UNDER CONSTRUCTION, ACTIVE, MERGE TARGET, MERGED SOURCE, REQUIRED FOR BACKUP/HA, IN TRANSITION TO TOMBSTONE, and TOMBSTONE. For a description of these phases, see [sys.dm_db_xtp_checkpoint_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-files-transact-sql).  
  
 After accounting for the storage taken by CFPs in various states, the overall storage taken by durable memory-optimized tables can be much larger than 2 times the size of the tables in memory. The DMV [sys.dm_db_xtp_checkpoint_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-files-transact-sql) can be queried to list all the CFPs in the memory-optimized filegroup, including their phase. Transitioning CFPs from MERGE SOURCE state to TOMBSTONE and ultimately garbage collection can take up five checkpoints, with each checkpoint followed by a transaction log backup, if the database is configured for full or bulk-logged recovery model.  
  
 You can manually force the checkpoint followed by log backup to expedite the garbage collection but then this will add 5 empty CFPs (5 data/delta file pairs with data file of size 128MB each). In production scenarios, the automatic checkpoints and log backups taken as part of backup strategy will seamlessly transition CFPs through these phases without requiring any manual intervention. The impact of the garbage collection process is that databases with memory-optimized tables may have a larger storage size compared to its size in memory. It is not uncommon for CFPs to be up to four times the size of the durable memory-optimized tables in memory.  
  
## See Also  
 [Creating and Managing Storage for Memory-Optimized Objects](creating-and-managing-storage-for-memory-optimized-objects.md)  
  
  
