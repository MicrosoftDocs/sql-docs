---
title: "OverrideBehavior Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "OverrideBehavior Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "OverrideBehavior element"
ms.assetid: 6a5b361a-6061-4b73-b1a7-1237fb77606c
author: minewiskan
ms.author: owend
manager: craigg
---
# OverrideBehavior Element (ASSL)
  Indicates the override behavior of the relationship described by an [AttributeRelationship](../objects/attributerelationship-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AttributeRelationship>  
   ...  
   <OverrideBehavior>...</OverrideBehavior>  
   ...  
</AttributeRelationship>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Strong*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AttributeRelationship](../objects/attributerelationship-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `OverrideBehavior` element determines how positioning on the related attribute affects the positioning on the attribute  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Strong*|Indicates the override behavior of the relationship described by an AttributeRelationship element. Dictates how positioning on one attribute affects the position of the other.|  
|*None*|No effect.|  
  
 The enumeration that corresponds to the allowed values for `OverrideBehavior` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.OverrideBehavior>.  
  
## See Also  
 [Attributes and Attribute Hierarchies](../../multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
