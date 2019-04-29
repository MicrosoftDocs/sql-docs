---
title: "Configure Measure Group Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "properties [Analysis Services], measure groups"
ms.assetid: fa66bdb6-60b8-413c-ac2a-00e4d09f60a2
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure Measure Group Properties
  Measures groups have properties that enable you to define how measure groups function.  
  
## Measure Group Properties  
 Measure group properties determine behaviors for the entire measure group and set the default behaviors for certain properties of measures within a measure group.  
  
|Property|Definition|  
|--------------|----------------|  
|`AggregationPrefix`|Applies to ROLAP storage. Assigns a common prefix to the indexed views in SQL Server, used to store aggregations for the partitions associated with this measure group.|  
|`DataAggregation`|This property is reserved for future use and currently has no effect. Therefore, it is recommended that you do not modify this setting.|  
|`Description`|You can use this property to document the measure group.|  
|`ErrorConfiguration`|Configurable error handling settings for handling of duplicate keys, unknown keys, null keys, error limits, action upon error detection, and the error log file. See [Error Configuration for Cube, Partition, and Dimension Processing &#40;SSAS - Multidimensional&#41;](error-configuration-for-cube-partition-and-dimension-processing.md).|  
|`EstimatedRows`|Specifies the estimated number of rows in the fact table.|  
|`EstimatedSize`|Specifies the estimated size (in bytes) of the measure group.|  
|`ID`|Specifies the identifier of the object.|  
|`IgnoreUnrelatedDimensions`|Determines whether unrelated dimensions are forced to their top level when members of dimensions that are unrelated to the measure group are included in a query. Default setting is `True`.|  
|`Name`|Name of the measure. This property is read-only.|  
|`ProactiveCaching`|Configurable error handling settings for handling of duplicate keys, unknown keys, null keys, error limits, action upon error detection, and the error log file.|  
|`ProcessingMode`|Indicates whether indexing and aggregating should occur during or after processing. Options are Regular and LazyAggregations. LazyAggregations can be used to run aggregation as a background task.|  
|`ProcessingPriority`|Determines the processing priority of the cube during background operations, such as lazy aggregations and indexing. The default value is **0**.|  
|`StorageLocation`|The file system storage location for the measure group. If none is specified, the location is inherited from the cube that contains the measure group.|  
|`StorageMode`|The storage mode for the measure group. Available values are MOLAP, ROLAP, or HOLAP.|  
|`Type`|Specifies the type of the measure group.|  
  
  
