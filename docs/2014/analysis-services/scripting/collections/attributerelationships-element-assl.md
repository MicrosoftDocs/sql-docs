---
title: "AttributeRelationships Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AttributeRelationships Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MemberProperties"
helpviewer_keywords: 
  - "AttributeRelationships element"
ms.assetid: f2ff82f6-6a7f-481a-a1ef-014bef38face
author: minewiskan
ms.author: owend
manager: craigg
---
# AttributeRelationships Element (ASSL)
  Contains the collection of [AttributeRelationship](../objects/attributerelationship-element-assl.md) elements for the attribute.  
  
## Syntax  
  
```xml  
  
<Attribute xsi:type="DimensionAttribute">  
   ...  
   <AttributeRelationships>  
      <AttributeRelationship>...</AttributeRelationship>  
   </AttributeRelationships>  
   ...  
</Attribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attribute](../objects/attribute-element-assl.md) of type [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|[AttributeRelationship](../objects/attributerelationship-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributeRelationshipCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
