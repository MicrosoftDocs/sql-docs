---
title: "Cube Properties - Multidimensional Model Programming | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Cube Properties - Multidimensional Model Programming
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Cubes have a number of properties that you can set to affect cube-wide behavior. These properties are summarized in the following table.  
  
> [!NOTE]  
>  Some properties are set automatically when the cube is created and cannot be changed.  
  
 For more information about how to set cube properties, see [Cube Designer &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/a6692467-da88-4312-8b03-d812f2ae5a96).  
  
|Property|Description|  
|--------------|-----------------|  
|**AggregationPrefix**|Specifies the common prefix that is used for aggregation names.|  
|**Collation**|Specifies the locale identifier (LCID) and the comparison flag, separated by an underscore: for example, Latin1_General_C1_AS.|  
|**DefaultMeasure**|Contains a Multidimensional Expressions (MDX) expression that defines the default measure for the cube.|  
|**Description**|Provides a description of the cube, which may be exposed in client applications.|  
|**ErrorConfiguration**|Contains configurable error handling settings for handling of duplicate keys, unknown keys, error limits, action upon error detection, error log file, and null key handling.|  
|**EstimatedRows**|Specifies the number of estimated rows in the cube.|  
|**ID**|Contains the unique identifier (ID) of the cube.|  
|**Language**|Specifies the default language identifier of the cube.|  
|**Name**|Specifies the user-friendly name of the cube.|  
|**ProactiveCaching**|Defines proactive cache settings for the cube.|  
|**ProcessingMode**|Indicates whether indexing and aggregating should occur during or after processing. Options are **regular** or **lazy**.|  
|**ProcessingPriority**|Determines the processing priority of the cube during background operations, such as lazy aggregations and indexing. The default value is **0**.|  
|**ScriptCacheProcessingMode**|Indicates whether the script cache should be built during or after processing. Options are **regular** and **lazy**.|  
|**ScriptErrorHandlingMode**|Determines error handling. Options are **IgnoreNone** or **IgnoreAll**|  
|**Source**|Displays the data source view used for the cube.|  
|**StorageLocation**|Specifies the file system storage location for the cube. If none is specified, the location is inherited from the database that contains the cube object.|  
|**StorageMode**|Specifies the storage mode for the cube. Values are **MOLAP**, **ROLAP**, or **HOLAP**.|  
|**Visible**|Determines the visibility of the cube.|  
  
> [!NOTE]  
>  For more information about setting values for the ErrorConfiguration property when working with null values and other data integrity issues, see [Handling Data Integrity Issues in Analysis Services 2005](http://go.microsoft.com/fwlink/?LinkId=81891).  
  
## See Also  
 [Proactive Caching &#40;Partitions&#41;](../../analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md)  
  
  
