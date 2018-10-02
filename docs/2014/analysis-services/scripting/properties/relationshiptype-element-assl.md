---
title: "RelationshipType Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "RelationshipType Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "RelationshipType"
helpviewer_keywords: 
  - "RelationshipType element"
ms.assetid: 72e1ab0e-a95d-4ebe-857d-21de1bf9fe03
author: minewiskan
ms.author: owend
manager: craigg
---
# RelationshipType Element (ASSL)
  Indicates whether member relationships for an [AttributeRelationship](../objects/attributerelationship-element-assl.md) can be changed.  
  
## Syntax  
  
```xml  
  
<AttributeRelationship>  
   ...  
   <RelationshipType>...</RelationshipType>  
   ...  
</AttributeRelationship>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Flexible*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AttributeRelationship](../objects/attributerelationship-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Rigid*|The member relationships between an attribute and a related attribute cannot be changed.|  
|*Flexible*|The member relationships between an attribute and a related attribute can be changed.|  
  
 For example, if `ZipCode` cannot change from one `City` to another, the relationship from `ZipCode` to `City` is marked as *Rigid*.  
  
 The enumeration that corresponds to the allowed values for `RelationshipType` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.RelationshipType>.  
  
## See Also  
 [Attributes and Attribute Hierarchies](../../multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
