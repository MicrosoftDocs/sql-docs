---
title: "Comparing Disk-Based Table Storage to Memory-Optimized Table Storage | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: eacf443c-001a-445f-ad1c-5f5a45eca6f4
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Comparing Disk-Based Table Storage to Memory-Optimized Table Storage
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  
  
|Categories|Disk-based Table|Durable Memory-Optimized Table|  
|----------------|-----------------------|-------------------------------------|  
|DDL|Metadata information is stored in system tables in the primary filegroup of the database and is accessible through catalog views.|Metadata information is stored in system tables in the primary filegroup of the database and is accessible through catalog views.|  
|Structure|Rows are stored in 8K pages. A page stores only rows from the same table.|Rows are stored as individual rows. There is no page structure. Two consecutive rows in a data file can belong to different memory-optimized tables.|  
|Indexes|Indexes are stored in a page structure similar to data rows.|Only the index definition is persisted (not index rows). Indexes are maintained in-memory and are regenerated when the memory-optimized table is loaded into memory as part of restarting a database. Since index rows are not persisted, no logging is done for index changes.|  
|DML operation|The first step is to find the page and then load it into buffer-pool.<br /><br /> Insert<br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] inserts the row on the page accounting for row ordering in case of clustered index.<br /><br /> Delete<br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] locates the row to be deleted on the page and marks it deleted.<br /><br /> Update<br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] locates the row on the page. The update is done in-place for non-key columns. Key-column update is done by a delete and insert operation.<br /><br /> After the DML operation completes, the affected pages are flushed to disk as part of buffer pool policy, checkpoint or transaction commit for minimally-logged operations. Both read/write operations on pages leads to unnecessary I/O.|For memory-optimized tables, since the data resides in memory, the DML operations are done directly in memory. There is a background thread that reads the log records for memory-optimized tables and persist them into data and delta files. An update generates a new row version. But an update is logged as a delete followed by an insert.|  
|Data Fragmentation|Data manipulation fragments data leading to partially filled pages and logically consecutive pages that are not contiguous on disk. This degrades data access performance and requires you to defragment data.|Memory-optimized data is not stored in pages so there is no data fragmentation. However, as rows are updated and deleted, the data and delta files need to be compacted. This is done by a background MERGE thread based on a merge policy.|  
  
## See Also  
 [Creating and Managing Storage for Memory-Optimized Objects](../../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md)  
  
  
