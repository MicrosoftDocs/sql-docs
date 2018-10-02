---
title: "Optionality Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Optionality Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Optionality element"
ms.assetid: 6cd2ef0a-6fbe-4462-ab27-4cdfeb33f8ab
author: minewiskan
ms.author: owend
manager: craigg
---
# Optionality Element (ASSL)
  Indicates the optionality of the members for an [AttributeRelationship](../objects/attributerelationship-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AttributeRelationship>  
   ...  
   <Optionality>...</OPtionality>  
   ...  
</AttributeRelationship>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Mandatory*|  
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
|*Mandatory*|Each member in the related attribute has to be associated with at least one member in the attribute that owns the `AttributeRelationship` element.|  
|*Optional*|Each member in the related attribute does not have to be associated with at least one member in the attribute that owns the `AttributeRelationship` element.|  
  
 The enumeration that corresponds to the allowed values for `Cardinality` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Optionality>.  
  
## See Also  
 [Attributes and Attribute Hierarchies](../../multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
