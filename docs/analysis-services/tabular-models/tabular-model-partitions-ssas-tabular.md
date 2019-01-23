---
title: "Analysis Services tabular model partitions | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Tabular Model Partitions 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Partitions divide a table into logical parts. Each partition can then be processed (Refreshed) independent of other partitions. Partitions defined for a model during model authoring are duplicated in a deployed model. Once deployed, you can manage those partitions and create new partitions by using the **Partitions** dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by using a script. Information provided in this topic describes partitions in a deployed tabular model database. For more information about creating and managing partitions during model authoring, see [Partitions](../../analysis-services/tabular-models/partitions-ssas-tabular.md).  
  
 Sections in this topic:  
  
-   [Benefits](#bkmk_benefits)  
  
-   [Permissions](#bkmk_permissions)  
  
-   [Process Partitions](#bkmk_process_partitions)  
  
-   [Parallel Processing](#bkmk_parallelProc)  
  
-   [Related Tasks](#bkmk_related_tasks)  
  
##  <a name="bkmk_benefits"></a> Benefits  
 Effective model design utilizes partitions to eliminate unnecessary processing and subsequent processor load on Analysis Services servers, while at the same time, making certain that data is processed and refreshed often enough to reflect the most recent data from data sources.  
  
 For example, a tabular model can have a Sales table which includes sales data for the current 2011 fiscal year and each of the previous fiscal years. The model's Sales table has the following three partitions:  
  
|Partition|Data from|  
|---------------|---------------|  
|Sales2011|Current fiscal year|  
|Sales2010-2001|Fiscal years 2001, 2002, 2003, 2004, 2005, 2006. 2007, 2008, 2009, 2010|  
|SalesOld|All fiscal years prior to the last ten years.|  
  
 As new sales data is added for the current 2011 fiscal year; that data must be processed daily to accurately be reflected in current fiscal year sales data analysis, thus the Sales2011 partition is processed nightly.  
  
 There is no need to process data in the Sales2010-2001 partition nightly; however, because sales data for the previous ten fiscal years can still occasionally change because of product returns and other adjustments, it must still be processed regularly, thus data in the Sales2010-2001 partition is processed monthly. Data in the SalesOld partition never changes therefore only processed annually.  
  
 When entering the 2012 fiscal year, a new Sales2012 partition is added to the mode's Sales table. The Sales2011 partition can then be merged with the Sales2010-2001 partition and renamed to Sales2011-2002. Data from the 2001 fiscal year is eliminated from the new Sales2011-2002 partition and moved into the SalesOld partition. All partitions are then processed to reflect changes.  
  
 How you implement a partition strategy for your organization's tabular models will largely be dependent on your particular model data processing needs and available resources.  
  
##  <a name="bkmk_permissions"></a> Permissions  
 In order to create, manage, and process partitions in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you must have the appropriate Analysis Services permissions defined in a security role. Each security role has one of the following permissions:  
  
|Permission|Actions|  
|----------------|-------------|  
|Administrator|Read, process, create, copy, merge, delete|  
|Process|Read, process|  
|Read Only|Read|  
  
 To learn more about creating roles during model authoring by using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], see [Roles](../../analysis-services/tabular-models/roles-ssas-tabular.md). To learn more about managing role members for deployed tabular model roles by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], see [Tabular Model Roles](../../analysis-services/tabular-models/tabular-model-roles-ssas-tabular.md).  
  
##  <a name="bkmk_parallelProc"></a> Parallel Processing  
Analysis Services includes parallel processing for tables with two or more partitions, increasing processing performance. There are no configuration settings for parallel processing (see notes). Parallel processing occurs by default when you Process Table or you select multiple partitions for the same table and Process. You can still choose to process a tables partitions independently.  
  
> [!NOTE]  
>  To specify whether refresh operations run sequentially or in parallel, you can use the **maxParallism** property option with the [Sequence command (TMSL)](https://docs.microsoft.com/bi-reference/tmsl/sequence-command-tmsl).

> [!NOTE]  
>  If re-encoding is detected, parallel processing can cause increased use of system resources. This is because multiple partition operations need to be interrupted and re-started with the new encoding in-parallel.  
  
##  <a name="bkmk_process_partitions"></a> Process Partitions  
 Partitions can be processed (refreshed) independent of other partitions by using the **Partitions** dialog box in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or by using a script. Processing has the following options:  
  
|Mode|Description|  
|----------|-----------------|  
|Process Default|Detects the process state of a partition object, and performs processing necessary to deliver unprocessed or partially processed partition objects to a fully processed state. Data for empty tables and partitions is loaded; hierarchies, calculated columns, and relationships are built or rebuilt.|  
|Process Full|Processes a partition object and all the objects that it contains. When Process Full is run for an object that has already been processed, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] drops all data in the object, and then processes the object. This kind of processing is required when a structural change has been made to an object.|  
|Process Data|Load data into a partition or a table without rebuilding hierarchies or relationships or recalculating calculated columns and measures.|  
|Process Clear|Removes all data from a partition.|  
|Process Add|Incrementally update partition with new data.|  
  
##  <a name="bkmk_related_tasks"></a> Related Tasks  
  
|Task|Description|  
|----------|-----------------|  
|[Create and Manage Tabular Model Partitions](../../analysis-services/tabular-models/create-and-manage-tabular-model-partitions-ssas-tabular.md)|Describes how to create and manage partitions in a deployed tabular model by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].|  
|[Process Tabular Model Partitions](../../analysis-services/tabular-models/process-tabular-model-partitions-ssas-tabular.md)|Describes how to process partitions in a deployed tabular model by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].|  
  
  
