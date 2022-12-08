---
title: "Index Disk Space Example"
description: Index Disk Space Example
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "online index disk space"
  - "disk space [SQL Server], indexes"
  - "index disk space [SQL Server]"
  - "space [SQL Server], indexes"
  - "indexes [SQL Server], disk space requirements"
  - "offline index disk space [SQL Server]"
ms.assetid: e5c71f55-0be3-4c93-97e9-7b3455c8f581
---
# Index Disk Space Example
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Whenever an index is created, rebuilt, or dropped, disk space for both the old (source) and new (target) structures is required in their appropriate files and filegroups. The old structure is not deallocated until the index creation transaction commits. Additional temporary disk space for sorting operations may also be needed. For more information, see [Disk Space Requirements for Index DDL Operations](../../relational-databases/indexes/disk-space-requirements-for-index-ddl-operations.md).  
  
 In this example, disk space requirements to create a clustered index are determined.  
  
 Assume the following conditions are true before creating the clustered index:  
  
-   The existing table (heap) contains 1 million rows. Each row is 200 bytes long.  
  
-   Nonclustered index A contains 1 million rows. Each row is 50 bytes long.  
  
-   Nonclustered index B contains 1 million rows. Each row is 80 bytes long.  
  
-   The index create memory option is set to 2 MB.  
  
-   A fill factor value of 80 is used for all existing and new indexes. This means the pages are 80 percent full.  
  
    > [!NOTE]  
    >  As a result of creating a clustered index, the two nonclustered indexes must be rebuilt to replace the row indicator with the new clustered index key.  
  
## Disk Space Calculations for an Offline Index Operation  
 In the following steps, both temporary disk space to be used during the index operation and permanent disk space to store the new indexes are calculated. The calculations shown are approximate; results are rounded up and consider only the size of index leaf level. The tilde (~) is used to indicate approximate calculations.  
  
1.  Determine the size of the source structures.  
  
     Heap: 1 million * 200 bytes ~ 200 MB  
  
     Nonclustered index A: 1 million * 50 bytes / 80% ~ 63 MB  
  
     Nonclustered index B: 1 million * 80 bytes / 80% ~ 100 MB  
  
     Total size of existing structures: 363 MB  
  
2.  Determine the size of the target index structures. Assume that the new clustered key is 24 bytes long including a uniqueifier. The row indicator (8 bytes long) in both nonclustered indexes will be replaced by this clustered key.  
  
     Clustered index: 1 million * 200 bytes / 80% ~ 250 MB  
  
     Nonclustered index A: 1 million * (50 - 8 + 24) bytes / 80% ~ 83 MB  
  
     Nonclustered index B: 1 million * (80 - 8 + 24) bytes / 80% ~ 120 MB  
  
     Total size of new structures: 453 MB  
  
     Total disk space required to support both the source and target structures for the duration of the index operation is 816 MB (363 + 453). The space currently allocated to the source structures will be deallocated after the index operation is committed.  
  
3.  Determine additional temporary disk space for sorting.  

     Space requirements are shown for sorting in **tempdb** (with SORT_IN_TEMPDB set to ON) and sorting in the target location (with SORT_IN_TEMPDB set to OFF).  
  
    1.  When SORT_IN_TEMPDB is set to ON, **tempdb** must have sufficient disk space to hold the largest index (1 million * 200 bytes ~ 200 MB). Fill factor is not considered in the sorting operation.  
  
         Additional disk space (in the **tempdb** location) equal to the [Configure the index create memory Server Configuration Option](../../database-engine/configure-windows/configure-the-index-create-memory-server-configuration-option.md) value = 2 MB.  
  
         Total size of temporary disk space with SORT_IN_TEMPDB set to ON ~ 202 MB.  
  
    2.  When SORT_IN_TEMPDB is set to OFF (default), the 250 MB of disk space already considered for the new index in step 2 is used for sorting.  
  
         Additional disk space (in the target location) equal to the [Configure the index create memory Server Configuration Option](../../database-engine/configure-windows/configure-the-index-create-memory-server-configuration-option.md) value = 2 MB.  
  
         Total size of temporary disk space with SORT_IN_TEMPDB set to OFF = 2 MB.  
  
 Using **tempdb**, a total of 1018 MB (816 + 202) would be needed to create the clustered and nonclustered indexes. Although using **tempdb** increases the amount of temporary disk space used to create an index, it may reduce the time that is required to create an index when **tempdb** is on a different set of disks than the user database. For more information about using **tempdb**, see [SORT_IN_TEMPDB Option For Indexes](../../relational-databases/indexes/sort-in-tempdb-option-for-indexes.md).  
  
 Without using **tempdb**, a total of 818 MB (816+ 2) would be needed to create the clustered and nonclustered indexes.  
  
## Disk Space Calculations for an Online Clustered Index Operation  
 When you create, drop, or rebuild a clustered index online, additional disk space is required to build and maintain a temporary mapping index. This temporary mapping index contains one record for each row in the table, and its contents are the union of the old and new bookmark columns.  
  
 To calculate the disk space needed for an online clustered index operation, follow the steps shown for an offline index operation and add those results to the results of the following step.  
  
-   Determine space for the temporary mapping index.  
  
     In this example, the old bookmark is the row ID (RID) of the heap (8 bytes) and the new bookmark is the clustering key (24 bytes including a **uniqueifier**). There are no overlapping columns between the old and new bookmarks.  
  
     Temporary mapping index size = 1 million * (8 bytes + 24 bytes) / 80% ~ 40 MB.  
  
     This disk space must be added to the required disk space in the target location if SORT_IN_TEMPDB is set to OFF, or to **tempdb** if SORT_IN_TEMPDB is set to ON.  
  
 For more information about the temporary mapping index, see [Disk Space Requirements for Index DDL Operations](../../relational-databases/indexes/disk-space-requirements-for-index-ddl-operations.md).  
  
## Disk Space Summary  
 The following table summarizes the results of the disk space calculations.  
  
|Index operation|Disk space requirements for the locations of the following structures|  
|---------------------|---------------------------------------------------------------------------|  
|Offline index operation with SORT_IN_TEMPDB = ON|Total space during the operation: 1018 MB<br /><br /> -Existing table and indexes: 363 MB\*<br /><br /> -<br />                    **tempdb**: 202 MB*<br /><br /> -New indexes: 453 MB<br /><br /> Total space required after the operation: 453 MB|  
|Offline index operation with SORT_IN_TEMPDB = OFF|Total space during the operation: 816 MB<br /><br /> -Existing table and indexes: 363 MB*<br /><br /> -New indexes: 453 MB<br /><br /> Total space required after the operation: 453 MB|  
|Online index operation with SORT_IN_TEMPDB = ON|Total space during the operation: 1058 MB<br /><br /> -Existing table and indexes: 363 MB\*<br /><br /> -<br />                    **tempdb** (includes mapping index): 242 MB*<br /><br /> -New indexes: 453 MB<br /><br /> Total space required after the operation: 453 MB|  
|Online index operation with SORT_IN_TEMPDB = OFF|Total space during the operation: 856 MB<br /><br /> -Existing table and indexes: 363 MB*<br /><br /> -Temporary mapping index: 40 MB\*<br /><br /> -New indexes: 453 MB<br /><br /> Total space required after the operation: 453 MB|  
  
 *This space is deallocated after the index operation is committed.  
  
 This example does not consider any additional temporary disk space required in **tempdb** for version records created by concurrent user update and delete operations.  
  
## Related Content  
 [Disk Space Requirements for Index DDL Operations](../../relational-databases/indexes/disk-space-requirements-for-index-ddl-operations.md)  
  
 [Transaction Log Disk Space for Index Operations](../../relational-databases/indexes/transaction-log-disk-space-for-index-operations.md)  
  
  
