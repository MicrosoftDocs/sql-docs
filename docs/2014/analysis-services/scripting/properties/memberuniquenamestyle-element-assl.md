---
title: "MemberUniqueNameStyle Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MemberUniqueNameStyle Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "MemberUniqueNameStyle element"
ms.assetid: f0771c81-0127-4203-9501-ae4f864730fa
author: minewiskan
ms.author: owend
manager: craigg
---
# MemberUniqueNameStyle Element (ASSL)
  Determines how unique names are generated for members of hierarchies contained within the [CubeDimension](../data-type/dimension-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CubeDimension>  
   ...  
   <MemberUniqueNameStyle>...</MemberUniqueNameStyle>  
   ...  
</CubeDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Native*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CubeDimension](../data-type/dimension-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Native*|The instance automatically determines the unique names of members.|  
|*NamePath*|The instance generates a compound name consisting of each level and the caption of the member.|  
  
## Remarks  
 The element that corresponds to the parent of `MemberUniqueNameStyle` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeDimension>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../objects/cube-element-assl.md)   
 [Dimension Element &#40;ASSL&#41;](../objects/dimension-element-assl.md)   
 [CubeDimension Data Type &#40;ASSL&#41;](../data-type/dimension-data-type-assl.md)  
  
  
