---
title: "Optimize Parameterized Filter Performance with Precomputed Partitions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "precomputed partitions [SQL Server replication]"
  - "merge replication precomputed partitions [SQL Server replication]"
  - "merge replication precomputed partitions [SQL Server replication], about precomputed partitions"
ms.assetid: 85654bf4-e25f-4f04-8e34-bbbd738d60fa
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Parameterized Filters - Optimize for Precomputed Partitions
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Precomputed partitions is a performance optimization that can be used with filtered merge publications. Precomputed partitions is also a requirement for using logical records on filtered publications. For more information about logical records, see [Group Changes to Related Rows with Logical Records](../../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
 When a Subscriber synchronizes with a Publisher, the Publisher must evaluate the Subscriber's filters to determine which rows belong to that Subscriber's partition, or data set. This process of determining partition membership of changes at the Publisher for each Subscriber receiving a filtered dataset is referred to as *partition evaluation*. Without precomputed partitions, partition evaluation must be performed for each change made to a filtered column at the Publisher since the last time the Merge Agent ran for a specific Subscriber, and this process has to be repeated for every Subscriber that synchronizes with the Publisher.  
  
 However, if the Publisher and Subscriber are running on [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] or a later version and you use precomputed partitions, partition membership for all changes at the Publisher is precomputed and persisted at the time that the changes are made. As a result, when a Subscriber synchronizes with the Publisher, it can immediately start to download changes relevant to its partition without having to go through the partition evaluation process. This can lead to significant performance gains when a publication has a large number of changes, Subscribers, or articles in the publication.  
  
 In addition to using precomputed partitions, pre-generate snapshots and/or allow Subscribers to request snapshot generation and application the first time they synchronize. Use one or both of these options to provide snapshots for publications that use parameterized filters. If you do not specify one of these options, subscriptions are initialized using a series of SELECT and INSERT statements, rather than using the **bcp** utility; this process is much slower. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
 **To use precomputed partitions**  
  
 Precomputed partitions are enabled by default on all new and existing publications that adhere to the guidelines described above. The setting can be changed through [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or programmatically. For more information, see [Optimize Parameterized Row Filters](../../../relational-databases/replication/publish/optimize-parameterized-row-filters.md).  
  
## Requirements for Using Precomputed Partitions  
 If the following requirements are met, new merge publications are, by default, created with precomputed partitions enabled, and existing publications are automatically upgraded to use the feature. If a publication does not meet the requirements, it can be changed, and then precomputed partitions can be enabled. If some articles meet these requirements and some do not, consider creating two publications, with one enabled for precomputed partitions.  
  
### Requirements for Filter Clauses  
  
-   Any functions used in parameterized row filters, such as HOST_NAME() and SUSER_SNAME(), should appear directly in the parameterized filter clause and not be nested inside of a view or dynamic function. For more information about these functions, see [HOST_NAME &#40;Transact-SQL&#41;](../../../t-sql/functions/host-name-transact-sql.md), [SUSER_SNAME &#40;Transact-SQL&#41;](../../../t-sql/functions/suser-sname-transact-sql.md), and [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
-   The values returned for each Subscriber should not change after the partition is created. For example, if you use HOST_NAME() in a filter (and do not override the HOST_NAME() value) do not change the computer name at the Subscriber.  
  
-   Join filters should not contain dynamic functions (functions such as HOST_NAME() and SUSER_SNAME() that evaluate to a different value depending upon the Subscriber that is synchronizing). Only parameterized row filters should contain dynamic functions.  
  
-   Nondeterministic functions cannot be used in a filter clause. For more information about nondeterministic functions, see [Deterministic and Nondeterministic Functions](../../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
-   Views referenced in join filter clauses or parameterized filter clauses should not contain dynamic functions.  
  
-   There should be no circular join filter relationships in the publication.  
  
### Database Collation  
  
-   When precomputed partitions are used, the collation of the database is always used when making comparisons, rather than the collation of the table or column. Consider the following scenario:  
  
    -   A database with a case-sensitive collation contains a table with a case-insensitive collation.  
  
    -   The table contains a column **ComputerName**, which is compared against the host name of the Subscriber in a parameterized filter.  
  
    -   The table contains one row with the value "MYCOMPUTER" and one row with the value "mycomputer" in this column.  
  
     If the Subscriber synchronizes with a host name of "mycomputer", the Subscriber receives only one row because the comparison is case-sensitive (the collation of the database). If precomputed partitions are not used, the Subscriber receives both rows, because the table has a case-insensitive collation.  
  
## Performance of Precomputed Partitions  
 There is a small performance cost with precomputed partitions when changes are uploaded from the Subscriber to the Publisher, but the bulk of merge processing time is spent evaluating partitions and downloading changes from the Publisher to the Subscriber, so the net gain can still be significant. The performance benefit will vary, depending on the number of Subscribers synchronizing concurrently and the number of updates per synchronization that move rows from one partition to another.  
  
## See Also  
 [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md)  
  
  
