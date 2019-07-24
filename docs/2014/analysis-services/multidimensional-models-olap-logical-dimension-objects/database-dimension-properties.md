---
title: "Database Dimension Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "metadata [Analysis Services]"
  - "dimensions [Analysis Services], characteristics"
  - "properties [Analysis Services], dimensions"
ms.assetid: 075548ef-08a3-413c-8ee0-4a074c276fcc
author: minewiskan
ms.author: owend
manager: craigg
---
# Database Dimension Properties
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the characteristics of a dimension are defined by the metadata for the dimension, based on the settings of various dimension properties, and on the attributes or hierarchies that are contained by the dimension. The following table describes the dimension properties in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
|Property|Description|  
|--------------|-----------------|  
|`AttributeAllMemberName`|Specifies the name of the All member for attributes in a dimension.|  
|`Collation`|Determines the collation used by the dimension.|  
|`CurrentStorageMode`|Contains the current storage mode for the dimension.|  
|`DependsOnDimension`|Contains the ID of another dimension on which the dimension depends, if any.|  
|`Description`|Contains the description of the dimension.|  
|`ErrorConfiguration`|Configurable error handling settings for handling of duplicate keys, unknown keys, error limits, action upon error detection, error log file, and null key handling.|  
|`ID`|Contains the unique identifier (ID) of the dimension.|  
|`Language`|Specifies the default language for the dimension.|  
|`MdxMissingMemberMode`|Determines how missing members are handled for Multidimensional Expressions (MDX) statements.|  
|`MiningModelID`|Contains the ID of the mining model with which the data mining dimension is associated. This property is applicable only if the dimension is a mining model dimension.|  
|`Name`|Specifies the name of the dimension.|  
|`ProactiveCaching`|Defines the proactive cache settings for the dimension.|  
|`ProcessingGroup`|Specifies the processing group. Values are ByAttribute or ByTable. Default is `ByAttribute`.|  
|`ProcessingMode`|Indicates whether [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] should index and aggregate during or after processing.|  
|`ProcessingPriority`|Determines the processing priority of the dimension during background operations such as lazy aggregation, indexing, or clustering.|  
|`Source`|Identifies the data source view to which the dimension is bound.|  
|`StorageMode`|Determines the storage mode for the dimension.|  
|`Type`|Specifies the type of the dimension.|  
|`UnknownMember`|Indicates whether the unknown member is visible.|  
|`UnknownMemberName`|Specifies the caption, in the default language of the dimension, for the unknown member of the dimension.|  
|`WriteEnabled`|Indicates whether dimension writebacks are available (subject to security permissions).|  
  
> [!NOTE]  
>  For more information about setting values for the ErrorConfiguration and UnknownMember properties when working with null values and other data integrity issues, see [Handling Data Integrity Issues in Analysis Services 2005](https://go.microsoft.com/fwlink/?LinkId=81891).  
  
## See Also  
 [Attributes and Attribute Hierarchies](attributes-and-attribute-hierarchies.md)   
 [User Hierarchies](user-hierarchies.md)   
 [Dimension Relationships](../multidimensional-models-olap-logical-cube-objects/dimension-relationships.md)   
 [Dimensions &#40;Analysis Services - Multidimensional Data&#41;](dimensions-analysis-services-multidimensional-data.md)  
  
  
