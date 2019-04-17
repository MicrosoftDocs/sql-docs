---
title: "Set Partition Writeback | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Set Partition Writeback
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  If you write-enable a measure group, end users can change cube data while they browse it, where changes are saved in a separate table called a writeback table, not in the cube data or source data. End users who browse a write-enabled partition see the net effect of all changes in the writeback table for the partition.  
  
 You can browse or delete writeback data. You can also convert writeback data to a partition. On a write-enabled partition, you can use cube roles to grant read/write access to users and groups of users, and to limit access to specific cells or groups of cells in the partition.  
  
 For a short video introduction to writeback, see [Excel 2010 Writeback to Analysis Services](http://go.microsoft.com/fwlink/p/?LinkId=394951). A more detailed exploration of this feature is available through this blog post series, [Building a Writeback Application with Analysis Services (blog)](http://go.microsoft.com/fwlink/?LinkId=394977).  
  
> [!NOTE]  
>  Writeback is supported for SQL Server relational databases and data marts only, and only for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] multidimensional models.  
  
## How to write-enable a partition  
 You can write-enable a partition's measure groups by write-enabling the partition itself in Cube Designer in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
-   In Cube Designer, in the Partitions tab, right-click a partition and choose **Writeback Settings**.  
  
-   In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], expand the database | cube | measure group, and then right-click **Writeback** and choose **Enable Writeback**.  
  
 Writeback is only supported for measures that use the SUM aggregation. In the AdventureWorks sample database, you can use the Sales Targets measure group to test writeback behaviors.  
  
 When you write-enable a partition, you specify a table name and a data source for storing the write-back table. Any subsequent changes to the measure group are recorded in this table.  
  
## Browse writeback data in a partition  
 You can browse the contents of a cube's writeback table in the **Browse Data** dialog box, which you can access by right-clicking a write-enabled partition on the **Partitions** tab in Cube Designer.  
  
## Delete writeback data or disable writeback  
 Deleting writeback data clears the writeback cache; as soon as that data is deleted, additional writeback work is performed on a clean slate. Disabling writeback for a cube partition simply turns off writeback for that partition.  
  
## Convert writeback data to a partition  
 You can convert the data in a partition's writeback table to a partition. This procedure causes the writeback table to become the new partition's fact table.  
  
> [!CAUTION]  
>  Incorrect use of partitions can result in inaccurate cube data. For more information, see [Create and Manage a Local Partition &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/create-and-manage-a-local-partition-analysis-services.md).  
  
 Converting the writeback data table to a partition also write-disables the partition. All unrestricted read/write policies and read/write permissions for the partition's cells are disabled, and end users will not be able to change displayed cube data. (End users with disabled unrestricted read/write policies or disabled read/write permissions will still be able to browse the cube.) Read and read-contingent permissions are not affected.  
  
 To convert writeback data to a partition, use the **Convert to Partition** dialog box, which is accessed by right-clicking the writeback table for a write-enabled partition in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You specify a name for the partition and whether to design the aggregation for the partition later or at the same time that you create it. To create the aggregation at the same time you choose the partition, you must choose to copy the aggregation design from an existing partition. This is normally, but not necessarily, the current writeback partition. You can also choose to process the partition at the same time that you create it.  
  
## See Also  
 [Write-Enabled Partitions](../../analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-write-enabled-partitions.md)   
 [Enabling Write-back to an OLAP Cube at Cell Level in Excel 2010](http://go.microsoft.com/fwlink/p/?LinkId=394952)   
 [Enabling and Securing Data Entry with Analysis Services Writeback](http://go.microsoft.com/fwlink/p/?LinkId=394953)  
  
  
