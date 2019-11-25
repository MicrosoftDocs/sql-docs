---
title: "Cube Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "Collation property"
  - "ID property"
  - "ErrorConfiguration property"
  - "cubes [Analysis Services], properties"
  - "Description property"
  - "DefaultMeasure property"
  - "ProcessingMode property"
  - "AggregationPrefix property"
  - "EstimatedRows property"
  - "Visible property"
  - "StorageLocation property"
  - "StorageMode property"
  - "ScriptErrorHandlingMode property"
  - "Source property"
  - "ScriptCacheProcessingMode property"
  - "Language property"
  - "Name property"
  - "properties [Analysis Services], cubes"
  - "ProcessingPriority property"
  - "ProactiveCaching property"
ms.assetid: 72ca3387-620d-4473-8e23-7fe1f2b3d5bf
author: minewiskan
ms.author: owend
manager: craigg
---
# Cube Properties
  Cubes have a number of properties that you can set to affect cube-wide behavior. These properties are summarized in the following table.  
  
> [!NOTE]  
>  Some properties are set automatically when the cube is created and cannot be changed.  
  
 For more information about how to set cube properties, see [Cube Designer &#40;Analysis Services - Multidimensional Data&#41;](../cube-designer-analysis-services-multidimensional-data.md).  
  
|Property|Description|  
|--------------|-----------------|  
|`AggregationPrefix`|Specifies the common prefix that is used for aggregation names.|  
|`Collation`|Specifies the locale identifier (LCID) and the comparison flag, separated by an underscore: for example, Latin1_General_C1_AS.|  
|`DefaultMeasure`|Contains a Multidimensional Expressions (MDX) expression that defines the default measure for the cube.|  
|`Description`|Provides a description of the cube, which may be exposed in client applications.|  
|`ErrorConfiguration`|Contains configurable error handling settings for handling of duplicate keys, unknown keys, error limits, action upon error detection, error log file, and null key handling.|  
|`EstimatedRows`|Specifies the number of estimated rows in the cube.|  
|`ID`|Contains the unique identifier (ID) of the cube.|  
|`Language`|Specifies the default language identifier of the cube.|  
|`Name`|Specifies the user-friendly name of the cube.|  
|`ProactiveCaching`|Defines proactive cache settings for the cube.|  
|`ProcessingMode`|Indicates whether indexing and aggregating should occur during or after processing. Options are **regular** or `lazy`.|  
|`ProcessingPriority`|Determines the processing priority of the cube during background operations, such as lazy aggregations and indexing. The default value is **0**.|  
|`ScriptCacheProcessingMode`|Indicates whether the script cache should be built during or after processing. Options are **regular** and `lazy`.|  
|`ScriptErrorHandlingMode`|Determines error handling. Options are `IgnoreNone` or `IgnoreAll`|  
|`Source`|Displays the data source view used for the cube.|  
|`StorageLocation`|Specifies the file system storage location for the cube. If none is specified, the location is inherited from the database that contains the cube object.|  
|`StorageMode`|Specifies the storage mode for the cube. Values are `MOLAP`, `ROLAP`, or `HOLAP``.`|  
|`Visible`|Determines the visibility of the cube.|  
  
> [!NOTE]  
>  For more information about setting values for the ErrorConfiguration property when working with null values and other data integrity issues, see [Handling Data Integrity Issues in Analysis Services 2005](https://go.microsoft.com/fwlink/?LinkId=81891).  
  
## See Also  
 [Proactive Caching &#40;Partitions&#41;](partitions-proactive-caching.md)  
  
  
