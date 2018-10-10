---
title: "Monitoring and Troubleshooting Merge for Data and Delta File Pairs | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: a8b0bacc-4d2c-42e4-84bf-1a97e0bd385b
author: stevestein
ms.author: sstein
manager: craigg
---
# Monitoring and Troubleshooting Merge for Data and Delta File Pairs
  In-Memory OLTP uses a merge policy to merge adjacent data and delta file pairs automatically. You cannot disable merge activity.  
  
 You can monitor data and delta file pairs, as follows:  
  
-   Compare the size of in-memory storage to overall size of storage. If the storage is dis-proportionately large, then it is likely that merge is not getting triggered. For information  
  
-   Look at the used space in data and delta files using [sys.dm_db_xtp_checkpoint_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-files-transact-sql) to see if merge is not getting triggered when it should.  
  
## Performing a Manual Merge  
 You can use [sys.sp_xtp_merge_checkpoint_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-xtp-merge-checkpoint-files-transact-sql) to perform a manual merge.  
  
 Use the following query to retrieve information about the data and delta files,  
  
```tsql  
select checkpoint_file_id, file_type_desc, internal_storage_slot, file_size_in_bytes, file_size_used_in_bytes,   
inserted_row_count, deleted_row_count, lower_bound_tsn, upper_bound_tsn   
from sys.dm_db_xtp_checkpoint_files  
where state = 1  
order by file_type_desc, upper_bound_tsn  
```  
  
 Assume that you found three data files that have not been merged. Using the `lower_bound_tsn` value of the first data file and the `upper_bound_tsn` of the last data file, you can issue the following command:  
  
```tsql  
exec sys.sp_xtp_merge_checkpoint_files 'H_DB',  12345, 67890  
```  
  
 Assume that the three data and delta file pairs each had 15,836 rows and 5,279 deleted rows. After the merge, the new data file has 31,872 rows and 0 deleted rows. The size of the new data file can be much larger than the initially allocated size of 128MB. This is because manual merge overrides the merge policy and forces the merge of the requested files.  
  
 The blog [State Transition of Checkpoint Files in Databases with Memory-Optimized Tables](http://blogs.technet.com/b/dataplatforminsider/archive/2014/01/23/state-transition-of-checkpoint-files-in-databases-with-memory-optimized-tables.aspx) describes state transition of data and delta file pairs from inception to garbage collection.  
  
## See Also  
 [Creating and Managing Storage for Memory-Optimized Objects](../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md)  
  
  
