---
title: "SORT_IN_TEMPDB Option For Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "SORT_IN_TEMPDB option"
  - "disk space [SQL Server], indexes"
  - "space [SQL Server], indexes"
  - "tempdb database [SQL Server], indexes"
  - "indexes [SQL Server], tempdb database"
  - "index creation [SQL Server], tempdb database"
ms.assetid: 754a003f-fe51-4d10-975a-f6b8c04ebd35
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SORT_IN_TEMPDB Option For Indexes
  When you create or rebuild an index, by setting the SORT_IN_TEMPDB option to ON you can direct the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to use **tempdb** to store the intermediate sort results that are used to build the index. Although this option increases the amount of temporary disk space that is used to create an index, the option could reduce the time that is required to create or rebuild an index when **tempdb** is on a set of disks different from that of the user database. For more information about **tempdb**, see [Configure the index create memory Server Configuration Option](../../database-engine/configure-windows/configure-the-index-create-memory-server-configuration-option.md).  
  
## Phases of Index Building  
 As the [!INCLUDE[ssDE](../../includes/ssde-md.md)] builds an index, it goes through the following phases:  
  
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)] first scans the data pages of the base table to retrieve key values and builds an index leaf row for each data row. When the internal sort buffers have been filled with leaf index entries, the entries are sorted and written to disk as an intermediate sort run. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] then resumes the data page scan until the sort buffers are again filled. This pattern of scanning multiple data pages followed by sorting and writing a sort run continues until all the rows of the base table have been processed.  
  
     In a clustered index, the leaf rows of the index are the data rows of the table; therefore, the intermediate sort runs contain all the data rows. In a nonclustered index, the leaf rows may contain nonkey columns, but are generally smaller than a clustered index. If the index keys are large, or there are several nonkey columns included in the index, a nonclustered sort run can be large. For more information about including nonkey columns, see [Create Indexes with Included Columns](create-indexes-with-included-columns.md).  
  
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)] merges the sorted runs of index leaf rows into a single, sorted stream. The sort merge component of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] starts with the first page of each sort run, finds the lowest key in all the pages, and passes that leaf row to the index create component. The next lowest key is processed, and then the next, and so on. When the last leaf index row is extracted from a sort run page, the process shifts to the next page from that sort run. When all the pages in a sort run extent have been processed, the extent is freed. As each leaf index row is passed to the index create component, it is included in a leaf index page in the buffer. Each leaf page is written as it is filled. As leaf pages are written, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] also builds the upper levels of the index. Each upper level index page is written when it is filled.  
  
## SORT_IN_TEMPDB Option  
 When SORT_IN_TEMPDB is set to OFF, the default, the sort runs are stored in the destination filegroup. During the first phase of creating the index, the alternating reads of the base table pages and writes of the sort runs move the disk read/write heads from one area of the disk to another. The heads are in the data page area as the data pages are scanned. They move to an area of free space when the sort buffers fill and the current sort run has to be written to disk, and then move back to the data page area as the table page scan is resumed. The read/write head movement is greater in the second phase. At that time the sort process is typically alternating reads from each sort run area. Both the sort runs and the new index pages are built in the destination filegroup. This means that at the same time the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is spreading reads across the sort runs, it has to periodically jump to the index extents to write new index pages as they are filled.  
  
 If the SORT_IN_TEMPDB option is set to ON and **tempdb** is on a separate set of disks from the destination filegroup, during the first phase, the reads of the data pages occur on a different disk from the writes to the sort work area in **tempdb**. This means the disk reads of the data keys generally continue more serially across the disk, and the writes to the **tempdb** disk also are generally serial, as do the writes to build the final index. Even if other users are using the database and accessing separate disk addresses, the overall pattern of reads and writes are more efficient when SORT_IN_TEMPDB is specified than when it is not.  
  
 The SORT_IN_TEMPDB option may improve the contiguity of index extents, especially if the CREATE INDEX operation is not being processed in parallel. The sort work area extents are freed on a somewhat random basis with regard to their location in the database. If the sort work areas are contained in the destination filegroup, as the sort work extents are freed, they can be acquired by the requests for extents to hold the index structure as it is built. This can randomize the locations of the index extents to a degree. If the sort extents are held separately in **tempdb**, the sequence in which they are freed has no effect on the location of the index extents. Also, when the intermediate sort runs are stored in **tempdb** instead of the destination filegroup, there is more space available in the destination filegroup. This increases the chances that index extents will be contiguous.  
  
 The SORT_IN_TEMPDB option affects only the current statement. No metadata records that the index was or was not sorted in **tempdb**. For example, if you create a nonclustered index using the SORT_IN_TEMPDB option, and at a later time create a clustered index without specifying the option, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not use the option when it re-creates the nonclustered index.  
  
> [!NOTE]  
>  If a sort operation is not required or if the sort can be performed in memory, the SORT_IN_TEMPDB option is ignored.  
  
## Disk Space Requirements  
 When you set the SORT_IN_TEMPDB option to ON, you must have sufficient free disk space available in **tempdb** to hold the intermediate sort runs, and enough free disk space in the destination filegroup to hold the new index. The CREATE INDEX statement fails if there is insufficient free space and there is some reason the databases cannot autogrow to acquire more space, such as no space on the disk or autogrow is set to off.  
  
 If SORT_IN_TEMPDB is set to OFF, the available free disk space in the destination filegroup must be roughly the size of the final index. During the first phase, the sort runs are built and require about the same amount of space as the final index. During the second phase, each sort run extent is freed after it has been processed. This means that sort run extents are freed at about the same rate at which extents are acquired to hold the final index pages; therefore, the overall space requirements do not greatly exceed the size of the final index. One side effect of this is that if the amount of free space is very close to the size of the final index, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will generally reuse the sort run extents very quickly after they are freed. Because the sort run extents are freed in a somewhat random manner, this reduces the continuity of the index extents in this scenario. If SORT_IN_TEMPDB is set to OFF, the continuity of the index extents is improved if there is sufficient free space available in the destination filegroup that the index extents can be allocated from a contiguous pool instead of from the freshly deallocated sort run extents.  
  
 When you create a nonclustered index, you must have available as free space:  
  
-   If SORT_IN_TEMPDB is set to ON, there must be sufficient free space in **tempdb** to store the sort runs, and sufficient free space in the destination filegroup to store the final index structure. The sort runs contain the leaf rows of the index.  
  
-   If SORT_IN_TEMPDB is set to OFF, the free space in the destination filegroup must be large enough to store the final index structure. The continuity of the index extends may be improved if more free space is available.  
  
 When you create a clustered index on a table that does not have nonclustered indexes, you must have available as free space:  
  
-   If SORT_IN_TEMPDB is set to ON, there must be sufficient free space in **tempdb** to store the sort runs. These include the data rows of the table. There must be sufficient free space in the destination filegroup to store the final index structure. This includes the data rows of the table and the index B-tree. You may have to adjust the estimate for factors such as having a large key size or a fill factor with a low value.  
  
-   If SORT_IN_TEMPDB is set to OFF, the free space in the destination filegroup must be large enough to store the final table. This includes the index structure. The continuity of the table and index extents may be improved if more free space is available.  
  
 When you create a clustered index on a table that has nonclustered indexes, you must have available as free space:  
  
-   If SORT_IN_TEMPDB is set to ON, there must be sufficient free space in **tempdb** to store the collection of sort runs for the largest index, typically the clustered index, and sufficient free space in the destination filegroup to store the final structures of all the indexes. This includes the clustered index that contains the data rows of the table.  
  
-   If SORT_IN_TEMPDB is set to OFF, the free space in the destination filegroup must be large enough to store the final table. This includes the structures of all the indexes. The continuity of the table and index extents may be improved if more free space is available.  
  
## Related Tasks  
 [CREATE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-index-transact-sql)  
  
 [Reorganize and Rebuild Indexes](indexes.md)  
  
## Related Content  
 [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql)  
  
 [Configure the index create memory Server Configuration Option](../../database-engine/configure-windows/configure-the-index-create-memory-server-configuration-option.md)  
  
 [Disk Space Requirements for Index DDL Operations](disk-space-requirements-for-index-ddl-operations.md)  
  
  
