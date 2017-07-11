---
title: "AttributeRelationships Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AttributeRelationships Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MemberProperties"
helpviewer_keywords: 
  - "AttributeRelationships element"
ms.assetid: f2ff82f6-6a7f-481a-a1ef-014bef38face
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AttributeRelationships Element (ASSL)
  Contains the collection of [AttributeRelationship](../../../analysis-services/scripting/objects/attributerelationship-element-assl.md) elements for the attribute.  
  
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
|Parent elements|[Attribute](../../../analysis-services/scripting/objects/attribute-element-assl.md) of type [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|[AttributeRelationship](../../../analysis-services/scripting/objects/attributerelationship-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributeRelationshipCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  