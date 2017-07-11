---
title: "Define Cube Attribute Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "cubes [Analysis Services], defining"
ms.assetid: 579ca818-f33d-4060-906d-c8bfee93bf99
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Define Cube Attribute Properties
  Cube attribute properties enable you to specify unique settings for dimension attributes in cube dimensions based on the same database dimension. The following table describes the properties of a cube attribute.  
  
|Property|Description|  
|--------------|-----------------|  
|**AggregationUsage**|Specifies how the Aggregation Design Wizard will design aggregations for the attribute. The default value is **Default**. This property can have the following values:<br /><br /> **Default**:<br />                    The Aggregation Design Wizard applies a default rule based on the type of attribute (Full for keys, Unrestricted for others).<br /><br /> **None**:<br />                    No aggregation for the cube should include this attribute.<br /><br /> **Unrestricted**:<br />                    No restrictions are placed on the Aggregation Design Wizard<br /><br /> **Full**:<br />                    Every aggregation for the cube must include this attribute.|  
|**AttributeHierarchyEnabled**|Identifies whether the attribute hierarchy is enables on this cube dimension. This allows attribute hierarchies to be disabled on specific cubes or dimension roles. This setting has no effect if the underlying attribute hierarchy is disabled. Default value is **True**.|  
|**OptimizedState**|Indicates whether the attribute hierarchy is optimized on this cube dimension. This allows attribute hierarchies to be optimized on specific cubes or dimension roles. This setting has no effect if the underlying attribute hierarchy is not optimized. The default value is **FullyOptimized**. This property can have the following values:<br /><br /> **FullyOptimized**: The instance builds indexes for the hierarchy to improve query performance. This is the default value.<br /><br /> **NotOptimized**:<br />                    The instance does not build additional indexes.|  
|**AttributeHierarchyVisible**|Indicates whether the attribute hierarchy is visible on this cube dimension. This allows attribute hierarchies to be visible on specific cubes or dimension roles. This setting has no effect if the underlying attribute hierarchy is not visible. The default value is **True**.|  
|**AttributeID**|Contains the unique identifier (ID) of the attribute.|  
  
## See Also  
 [Define Cube Dimension Properties](../../analysis-services/multidimensional-models/define-cube-dimension-properties.md)   
 [Define Cube Hierarchy Properties](../../analysis-services/multidimensional-models/define-cube-hierarchy-properties.md)  
  
  