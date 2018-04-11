---
title: "MembersWithData Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "MembersWithData Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MembersWithData"
helpviewer_keywords: 
  - "MembersWithData element"
ms.assetid: 845087a2-b12d-4344-a8be-85ca61155296
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MembersWithData Element (ASSL)
  Determines whether to display data members for non-leaf members in the parent attribute.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
   <MembersWithData>...</MembersWithData>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*NonLeafDataVisible*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `MembersWithData` element is used only by parent attributes (in other words, the value of the [Usage](../../../2014/analysis-services/dev-guide/usage-element-dimensionattribute-assl.md) element of the `DimensionAttribute` parent element is set to *Parent*) to determine whether to display data members for non-leaf members in the parent attribute. For more information about data members, see [Attributes in Parent-Child Hierarchies](../../../2014/analysis-services/multidimensional-models/parent-child-dimension-attributes.md).  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*NonLeafDataHidden*|Non-leaf data is hidden.|  
|*NonLeafDataVisible*|Non-leaf data is visible.|  
  
 The enumeration that corresponds to the allowed values for `MembersWithData` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MembersWithData>.  
  
## See Also  
 [MembersWithDataCaption Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/memberswithdatacaption-element-assl.md)   
 [DimensionAttribute Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  