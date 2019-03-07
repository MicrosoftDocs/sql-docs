---
title: "Partitions in Multidimensional Models | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 26e01dc7-fa49-4b1f-99eb-7799d1b4dcd2
author: minewiskan
ms.author: owend
manager: craigg
---
# Partitions in Multidimensional Models
  In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a *partition* provides the physical storage of fact data loaded into a measure group. A single partition is created for each measure group automatically, but it is common to create additional partitions that further segment the data, resulting in more efficient processing and faster query performance.  
  
 Processing is more efficient because partitions can be processed independently and in parallel, on one or more servers. Queries run faster because each partition can be configured to have storage modes and aggregation optimizations that result in shorter response times. For example, choosing MOLAP storage for partitions containing newer data is typically faster than ROLAP. Likewise, if you partition by date, partitions containing newer data can have more optimizations than partitions containing older data that is accessed less frequently. Note that varying storage and aggregation design by partition will have a negative impact on future merge operations. Be sure to consider whether merging is an essential component of your partition management strategy before optimizing individual partitions.  
  
> [!NOTE]  
>  Support for multiple partitions is available in the business intelligence edition and enterprise edition. The standard edition does not support multiple partitions. For more information, see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## Important considerations when designing a partitioning strategy  
 The integrity of a cube's data relies on the data being distributed among the partitions of the cube such that no data is duplicated among the partitions. When data is summarized from the partitions, any data elements that are present in more than one partition will be summarized as if they were different data elements. This can result in incorrect summaries and erroneous data provided to the end user. For example, if a sales transaction for Product X is duplicated in the fact tables for two partitions, summaries of Product X sales can include a double accounting of the duplicated transaction.  
  
 Partitions can be merged; you can use this feature in your overall storage and data update strategy. Partitions can be merged only if they have the same storage mode and aggregation design. To create partitions that are candidates for later merging, you can copy the aggregation design of another partition when you create partitions. You can also edit a partition after it has been created to copy the aggregation design of another partition. Merging partitions must also be performed carefully to avoid duplication of data in the resulting partition, which can cause cube data to be inaccurate.  
  
## Local Partitions  
 Local partitions are partitions that are defined, processed, and stored on one server. If you have large measure groups in a cube, you might want to partition them out so that processing occurs in parallel across the partitions. The advantage is that parallel processing provides faster execution. Because one partition processing job does not have to finish before another starts, they can run in parallel. For more information, see [Create and Manage a Local Partition &#40;Analysis Services&#41;](create-and-manage-a-local-partition-analysis-services.md).  
  
## Remote Partitions  
 Remote partitions are partitions that are defined on one server, but are processed and stored on another. If you want to distribute storage of your data and metadata across multiple servers, use remote partitions. Ordinarily, when you transition from development to production, the size of data under analysis grows several times over. With such large chunks of data, one possible alternative is to distribute that data over multiple computers. This is not just because one computer cannot hold all the data, but because you will want more than one computer processing the data in parallel. For more information, see [Create and Manage a Remote Partition &#40;Analysis Services&#41;](create-and-manage-a-remote-partition-analysis-services.md).  
  
## Aggregations  
 Aggregations are precalculated summaries of cube data that help enable [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to provide rapid query responses. You can control the number of aggregations created for a measure group by setting limits on storage, performance gains, or arbitrarily stopping the aggregation build process after it has been running for a while. More aggregations are not necessarily better. Every new aggregation comes at a cost, both in terms of disk space and processing time. We recommend creating aggregations for a thirty percent performance gain, and then raising the number only if testing or experience warrants it.For more information, see [Designing Aggregations &#40;Analysis Services - Multidimensional&#41;](designing-aggregations-analysis-services-multidimensional.md).  
  
## Partition Merging and Editing  
 If two partitions use the same aggregation design, you can merge those two partitions into one. For example, if you have an inventory dimension that is partitioned by month, then at the end of each calendar month, you can merge that month partition with the existing year-to-date partition. This way, the current month partition can be processed and analyzed quickly, while the rest of the year in months only has to be reprocessed when merged. That reprocess requires longer processing time, and can be run less frequently. For more information about managing the partition merging process, see [Merge Partitions in Analysis Services &#40;SSAS - Multidimensional&#41;](merge-partitions-in-analysis-services-ssas-multidimensional.md). To edit cube partitions by using the **Partitions** tab in Cube Designer, see [Edit or Delete Partitions &#40;Analysis Services - Multidimensional&#41;](edit-or-delete-partitions-analyisis-services-multidimensional.md).  
  
## Related Topics  
  
|Topic|Description|  
|-----------|-----------------|  
|[Create and Manage a Local Partition &#40;Analysis Services&#41;](create-and-manage-a-local-partition-analysis-services.md)|Contains information about how to partition data using filters or different fact tables without duplicating data.|  
|[Set Partition Storage &#40;Analysis Services - Multidimensional&#41;](set-partition-storage-analysis-services-multidimensional.md)|Describes how to configure storage for partitions.|  
|[Edit or Delete Partitions &#40;Analysis Services - Multidimensional&#41;](edit-or-delete-partitions-analyisis-services-multidimensional.md)|Describes how to view and edit partitions.|  
|[Merge Partitions in Analysis Services &#40;SSAS - Multidimensional&#41;](merge-partitions-in-analysis-services-ssas-multidimensional.md)|Contains information about how to merge partitions that have different fact tables or different data slices without duplicating data.|  
|[Set Partition Writeback](set-partition-writeback.md)|Provides instructions on write-enabling a partition.|  
|[Create and Manage a Remote Partition &#40;Analysis Services&#41;](create-and-manage-a-remote-partition-analysis-services.md)|Describes how to create and manage a remote partition.|  
  
  
