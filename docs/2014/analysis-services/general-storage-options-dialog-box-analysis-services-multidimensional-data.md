---
title: "General (Storage Options Dialog Box) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.partitiondesigner.partitionstoragesettings.setstorageoptions.storage.f1"
ms.assetid: ee1fac79-ae15-4c3c-9a98-33db04388817
author: minewiskan
ms.author: owend
manager: craigg
---
# General (Storage Options Dialog Box) (Analysis Services - Multidimensional Data)
  Use the **General** tab of the **Storage Options** dialog box in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to set the storage mode and proactive caching settings for a dimension, cube, measure group, or partition.  
  
> [!NOTE]  
>  You should be familiar with storage mode and proactive caching functionality before modifying these settings. For more information, see [Proactive Caching &#40;Partitions&#41;](multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Storage mode**|Selects the storage mode to use for the object.<br /><br /> **MOLAP**<br /> The object uses multidimensional OLAP (MOLAP) storage.<br /><br /> **HOLAP**<br /> The object uses hybrid OLAP (HOLAP) storage.<br /><br /> **ROLAP**<br /> The object uses relational OLAP (ROLAP) storage.|  
|**Enable proactive caching**|Enables proactive caching.<br /><br /> Note: If this option is not selected, all options except **Storage mode** are disabled.|  
|**Update the cache when data changes**|Uses the notification method selected in the **Notifications** tab to update the MOLAP image for the object whenever a notification is received. For more information about the **Notifications** tab, see [Notifications &#40;Storage Options Dialog Box&#41; &#40;Analysis Services - Multidimensional Data&#41;](notifications-storage-options-dialog-analysis-services-multidimensional-data.md).<br /><br /> Note: This option is disabled unless **Enable proactive caching** is selected.|  
|**Silence interval**|Sets the minimum interval and units of time in which the object has no activity before proactive caching starts to create the new MOLAP image for the object.<br /><br /> Note: This option is disabled unless **Update the cache when data changes** is selected.|  
|**Silence override interval**|Sets the maximum interval and units of time in which, after a notification is received for the object, proactive caching starts to create a new MOLAP image for the object, regardless of the current activity of the object. Notifications received after this interval has been reached do not cancel the MOLAP image process triggered by this interval.<br /><br /> Note: This option is disabled unless **Update the cache when data changes** is selected. Also note that this option should not be set if **Storage mode** is set to **HOLAP**.|  
|**Drop outdated cache**|Specifies the period between the start of creating a new MOLAP cache and the removal of the existing MOLAP cache.<br /><br /> Note: This option is disabled unless **Enable proactive caching** is selected. Also note that this option should not be set if **Storage mode** is set to HOLAP.|  
|**Latency**|Selects the interval and units of time for the period between the start of creating a new MOLAP cache and the removal of the existing MOLAP cache.<br /><br /> Note: This option is disabled unless **Drop outdated cache** is selected. Also note that this option should not be set if **Storage mode** is set to **HOLAP**.|  
|**Update the cache periodically**|Updates the MOLAP image on a regular basis, regardless of notification.<br /><br /> Note: This option is disabled unless **Enable proactive caching** is selected. Also note that this option should not be set if **Storage mode** is set to **HOLAP**.|  
|**Rebuild interval**|Selects the interval and units of time for the period that, after a new MOLAP image is created, [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] starts the MOLAP image process again for the object, regardless of notification. Notifications received after this interval has been reached do not cancel the MOLAP image process triggered by this interval.<br /><br /> Note: This option is disabled unless **Update the cache periodically** is selected. Also note that this option should not be set if **Storage mode** is set to **HOLAP**.|  
|**Bring online immediately**|Brings objects online immediately. If this option is set, objects use the underlying ROLAP storage for resolving queries while the MOLAP cache is being rebuilt. If this option is not set, objects are brought online only after the MOLAP cache for the object is complete.|  
|**Enable ROLAP aggregations**|Uses materialized views on the underlying data source to store aggregations.<br /><br /> Note: If the underlying data source does not support materialized views, an error will occur when the object is processed.|  
|**Apply settings to dimensions**|Applies storage mode and proactive caching settings to associated dimensions.|  
  
## See Also  
 [Storage Options Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](storage-options-dialog-box-analysis-services-multidimensional-data.md)   
 [Notifications &#40;Storage Options Dialog Box&#41; &#40;Analysis Services - Multidimensional Data&#41;](notifications-storage-options-dialog-analysis-services-multidimensional-data.md)  
  
  
