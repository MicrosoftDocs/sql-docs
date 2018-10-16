---
title: "MembersWithDataCaption Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MembersWithDataCaption Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MembersWithDataCaption"
helpviewer_keywords: 
  - "MembersWithDataCaption element"
ms.assetid: a5d59efd-5d67-485b-a360-67d54a1fe394
author: minewiskan
ms.author: owend
manager: craigg
---
# MembersWithDataCaption Element (ASSL)
  Provides a template string that is used to create captions for system-generated data members.  
  
## Syntax  
  
```xml  
  
<AttributeTranslation> <!-- or DimensionAttribute -->  
   ...  
   <MembersWithDataCaption>...</MembersWithDataCaption>  
   ...  
</AttributeTranslation>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AttributeTranslation](../data-type/translation-data-type-assl.md), [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `MembersWithDataCaption` element is used only by parent attributes (in other words, the value of the [Usage](usage-element-dimensionattribute-assl.md) element of the `DimensionAttribute` parent element is set to *Parent*) to determine the caption of data members in the parent attribute. For more information about data members, see [Attributes in Parent-Child Hierarchies](../../multidimensional-models/parent-child-dimension-attributes.md).  
  
 The elements that correspond to the parents of `MembersWithDataCaption` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AttributeTranslation> and <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [MembersWithData Element &#40;ASSL&#41;](../objects/data-element-assl.md)   
 [AttributeTranslation Data Type &#40;ASSL&#41;](../data-type/translation-data-type-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
