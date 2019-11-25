---
title: "Write-Enabled Partitions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "storage [Analysis Services], partitions"
  - "write-enabled partitions [Analysis Services]"
  - "partitions [Analysis Services], write-enabled"
  - "partitions [Analysis Services], storage"
  - "writeback [Analysis Services], partitions"
  - "storing data [Analysis Services], partitions"
ms.assetid: 46e7683f-03ce-4af2-bd99-a5203733d723
author: minewiskan
ms.author: owend
manager: craigg
---
# Write-Enabled Partitions
  The data in a cube is generally read-only. However, for certain scenarios, you may want to write-enable a partition. Write-enabled partitions are used to enable business users to explore scenarios by changing cell values and analyzing the effects of the changes on cube data. When you write-enable a partition, client applications can record changes to the data in the partition. These changes, known as writeback data, are stored in a separate table and do not overwrite any existing data in a measure group. However, they are incorporated into query results as if they are part of the cube data.  
  
 You can write-enable an entire cube or only certain partitions in the cube. Write-enabled dimensions are different but complementary. A write-enabled partition lets users update partition cells, whereas a write-enabled dimension lets users update dimension members. You can also use these two features in combination. For example, a write-enabled cube or a write-enabled partition does not have to include any write-enabled dimensions. **Related topic:**[Write-Enabled Dimensions](../multidimensional-models-olap-logical-dimension-objects/write-enabled-dimensions.md).  
  
> [!NOTE]  
>  If you want to write-enable a cube that has a Microsoft Access database as a data source, do not use Microsoft OLE DB Provider for ODBC Drivers in the data source definitions for the cube, its partitions, or its dimensions. Instead, you can use Microsoft Jet 4.0 OLE DB Provider, or any version of the Jet Service Pack that includes Jet 4.0 OLE. For more information, see the Microsoft Knowledge Base article [How to obtain the latest service pack for the Microsoft Jet 4.0 Database Engine](https://support.microsoft.com/?kbid=239114).  
  
 A cube can be write-enabled only if all its measures use the `Sum` aggregate function. Linked measure groups and local cubes cannot be write-enabled.  
  
## Writeback Storage  
 Any change made by the business user is stored in the writeback table as a difference from the currently displayed value. For example, if an end user changes a cell value from 90 to 100, the value `+10` is stored in the writeback table, together with the time of the change and information about the business user who made it. The net effect of accumulated changes is displayed to client applications. The original value in the cube is preserved, and an audit trail of changes is recorded in the writeback table.  
  
 Changes to leaf and nonleaf cells are handled differently. A leaf cell represents an intersection of a measure and a leaf member from every dimension referenced by the measure group. The value of a leaf cell is taken directly from the fact table, and cannot be divided further by drilling down. If a cube or any partition is write-enabled, changes can be made to a leaf cell. Changes can be made to a nonleaf cell only if the client application provides a way of distributing the changes among the leaf cells that make up the nonleaf cell. This process, called allocation, is managed through the UPDATE CUBE statement in Multidimensional Expressions (MDX). Business intelligence developers can use the UPDATE CUBE statement to include allocation functionality. For more information, see [UPDATE CUBE Statement &#40;MDX&#41;](/sql/mdx/mdx-data-manipulation-update-cube).  
  
> [!IMPORTANT]  
>  When updated cells do not overlap, the `Update Isolation Level` connection string property can be used to enhance performance for UPDATE CUBE. For more information, see <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A>.  
  
 Regardless of whether a client application distributes changes that were made to nonleaf cells, whenever queries are evaluated, changes in the writeback table are applied to both leaf and nonleaf cells so that business users can view the effects of the changes throughout the cube.  
  
 Changes that were made by the business user are kept in a separate writeback table that you can work with as follows:  
  
-   Convert to a partition to permanently incorporate changes into the cube. This action makes the measure group read-only. You can specify a filter expression to select the changes you want to convert.  
  
-   Discard to return the partition to its original state. This action makes the partition read-only.  
  
## Security  
 A business user is permitted to record changes in a cube's writeback table only if the business user belongs to a role that has read/write permission to the cube's cells. For each role, you can control which cube cells can and cannot be updated. For more information, see [Grant cube or model permissions &#40;Analysis Services&#41;](../multidimensional-models/grant-cube-or-model-permissions-analysis-services.md).  
  
## See Also  
 [Write-Enabled Dimensions](../multidimensional-models-olap-logical-dimension-objects/write-enabled-dimensions.md)   
 [Aggregations and Aggregation Designs](../multidimensional-models-olap-logical-cube-objects/aggregations-and-aggregation-designs.md)   
 [Partitions &#40;Analysis Services - Multidimensional Data&#41;](../multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md)   
 [Write-Enabled Dimensions](../multidimensional-models-olap-logical-dimension-objects/write-enabled-dimensions.md)  
  
  
