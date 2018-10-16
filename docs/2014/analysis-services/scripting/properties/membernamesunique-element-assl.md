---
title: "MemberNamesUnique Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MemberNamesUnique Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MemberNamesUnique"
helpviewer_keywords: 
  - "MemberNamesUnique element"
ms.assetid: bd4e75b2-4605-4ebc-a535-10f743eba08e
author: minewiskan
ms.author: owend
manager: craigg
---
# MemberNamesUnique Element (ASSL)
  Determines whether member names under the parent element must be unique.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute> <!-- or Hierarchy -->  
   ...  
   <MemberNamesUnique>   </MemberNamesUnique>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|False|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DimensionAttribute](../objects/hierarchy-element-assl.md)|  
|Child elements|None|  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
