---
title: "CubeAttribute Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CubeAttribute Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CubeAttribute"
helpviewer_keywords: 
  - "CubeAttribute data type"
ms.assetid: 114ffb44-460b-4971-b31b-dd844e960b81
author: minewiskan
ms.author: owend
manager: craigg
---
# CubeAttribute Data Type (ASSL)
  Defines a primitive data type that represents an attribute associated with a [Cube](../objects/cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CubeAttribute>  
   <AttributeID>...</AttributeID>  
   <AggregationUsage>...</AggregationUsage>  
   <AttributeHierarchyOptimizedState>...</AttributeHierarchyOptimizedState>  
   <AttributeHierarchyEnabled>...</AttributeHierarchyEnabled>  
   <AttributeHierarchyVisible>...</AttributeHierarchyVisible>  
   <Annotations>...</Annotations>  
</CubeAttribute>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[AggregationUsage](../properties/aggregationusage-element-assl.md), [Annotations](../collections/annotations-element-assl.md), [AttributeHierarchyEnabled](../properties/enabled-element-assl.md), [AttributeHierarchyOptimizedState](../properties/state-element-assl.md), [AttributeHierarchyVisible](../properties/visible-element-assl.md), [AttributeID](../properties/id-element-assl.md)|  
|Derived elements|[Attribute](../objects/attribute-element-assl.md) ([Attributes](../collections/attributes-element-assl.md) collection of [CubeDimension](dimension-data-type-assl.md))|  
  
## Remarks  
 The *AttributeHierarchyOptimizedState* element is not supported when running the service in DeploymentMode configuration property values of 1 or 2 (SharePoint or Tabular modes, used to run PowerPivot and tabular model databases).  
  
 An attribute cannot be added as a level of a hierarchy when the property, *AtttributeHierarchyEnabled*, is set to FALSE and the instance is operating under DeploymentMode 1 or 2 (SharePoint or Tabular server mode).  
  
 Attributes in the [CubeDimension](dimension-data-type-assl.md) element that are not explicitly included in the [Attributes](../collections/attributes-element-assl.md) collection become part of the collection with default values assigned to them. After attributes are added to the collection, the attributes can be returned by the [Discover](../../xmla/xml-elements-methods-discover.md) method.  
  
 The [AggregationUsage](../properties/aggregationusage-element-assl.md) element controls how [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] automatically designs aggregations for the attribute. The `AggregationUsage` element does not constrain any aggregations that are manually created for the cube.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeAttribute>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
