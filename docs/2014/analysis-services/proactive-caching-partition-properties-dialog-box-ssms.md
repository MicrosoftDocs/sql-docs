---
title: "Proactive Caching (Partition Properties Dialog Box) (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.sqlserverstudio.partitionproperties.proactivecaching.f1"
ms.assetid: ecba72a3-703f-4ede-9d85-9a3318a749e5
author: minewiskan
ms.author: owend
manager: craigg
---
# Proactive Caching (Partition Properties Dialog Box) (SSMS)
  Use the **Proactive Caching** page of the **Partition Properties** dialog box in SQL Server Management Studio to set the storage and proactive caching properties of a partition in a measure group for a cube in an [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database.  
  
## Options  
 **Standard setting**  
 Select to enable **Standard setting slider** and use predefined settings for storage mode and proactive caching features.  
  
 **Standard setting slider**  
 Set to one of the predefined settings listed in the following table.  
  
|Setting|Description|  
|-------------|-----------------|  
|**Real-time ROLAP**|Select to use the following storage and proactive caching settings:<br /><br /> ROLAP storage mode.<br /><br /> Enables proactive caching.<br /><br /> Drops outdated cache, with a latency period of 0 seconds.<br /><br /> Brings object online immediately.|  
|**Real-time HOLAP**|Select to use the following storage and proactive caching settings:<br /><br /> HOLAP storage mode.<br /><br /> Enables proactive caching.<br /><br /> Drops outdated cache, with a latency period of 0 seconds.<br /><br /> Updates cache when data changes, with a silence interval of 0 seconds and no silence override interval.<br /><br /> Brings object online immediately.|  
|**Low-latency MOLAP**|Select to use the following storage and proactive caching settings:<br /><br /> MOLAP storage mode.<br /><br /> Enables proactive caching.<br /><br /> Drops outdated cache, with a latency period of 30 minutes.<br /><br /> Updates cache when data changes, with a silence interval of 10 seconds and a silence override interval of 10 minutes.<br /><br /> Updates cache when data changes, with a silence interval of 10 seconds and a silence override interval of 10 minutes.<br /><br /> Brings object online immediately.|  
|**Medium-latency MOLAP**|Select to useBrings object online immediately.<br /><br /> the following storage and proactive caching settings:<br /><br /> MOLAP storage mode.<br /><br /> Enables proactive caching.<br /><br /> Drops outdated cache, with a latency period of 4 hours.<br /><br /> Updates cache when data changes, with a silence interval of 10 seconds and a silence override interval of 10 minutes.<br /><br /> Brings object online immediately.|  
|**Automatic MOLAP**|Select to use the following storage and proactive caching settings:<br /><br /> MOLAP storage mode.<br /><br /> Enables proactive caching.<br /><br /> Updates cache when data changes, with a silence interval of 0 seconds and no silence override interval.|  
|**Scheduled MOLAP**|Select to use the following storage and proactive caching settings:<br /><br /> MOLAP storage mode<br /><br /> Enable proactive caching<br /><br /> Updates cache periodically, with a rebuild interval of 1 day|  
|**MOLAP**|Select to use the following storage and proactive caching settings:<br /><br /> MOLAP storage mode.|  
  
 **Custom setting**  
 Select to explicitly set storage mode, proactive caching, and notification options.  
  
 **Options**  
 Click to display the **Storage Options** dialog box to explicitly set storage mode, proactive caching, and notification options. For more information about the **Storage Options** dialog box, see [Storage Options Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](storage-options-dialog-box-analysis-services-multidimensional-data.md).  
  
## See Also  
 [Proactive Caching &#40;Partitions&#41;](multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md)   
 [Partition Properties Dialog Box &#40;SSMS&#41;](partition-properties-dialog-box-ssms.md)   
 [Selection &#40;Partition Properties Dialog Box&#41; &#40;SSMS&#41;](selection-partition-properties-dialog-box-ssms.md)   
 [General &#40;Partition Properties Dialog Box&#41; &#40;SSMS&#41;](general-partition-properties-dialog-box-ssms.md)   
 [Error Configuration for Cube, Partition, and Dimension Processing &#40;SSAS - Multidimensional&#41;](multidimensional-models/error-configuration-for-cube-partition-and-dimension-processing.md)  
  
  
