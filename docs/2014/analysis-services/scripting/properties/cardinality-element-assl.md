---
title: "Cardinality Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Cardinality Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Cardinality element"
ms.assetid: 60ac8a26-7c8b-4011-9b9b-a29863779428
author: minewiskan
ms.author: owend
manager: craigg
---
# Cardinality Element (ASSL)
  Indicates the cardinality of the relationship described by an [AttributeRelationship](../objects/attributerelationship-element-assl.md) or [RegularMeasureGroupDimension](../data-type/dimension-data-type-assl.md).  
  
## Syntax  
  
```xml  
  
<AttributeRelationship> <!-- or RegularMeasureGroupDimension -->  
   ...  
   <Cardinality>...</Cardinality>  
   ...  
</AttributeRelationship>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Many*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AttributeRelationship](../objects/attributerelationship-element-assl.md), [RegularMeasureGroupDimension](../data-type/dimension-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Many*|Many-to-one relationship|  
|*One*|One-to-one relationship|  
  
 The enumeration corresponding to the allowed values for `Cardinality` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Cardinality>.  
  
## See Also  
 [Attributes and Attribute Hierarchies](../../multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
