---
title: "MemberNamesUnique Element (ASSL) | Microsoft Docs"
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
  - "MemberNamesUnique Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MemberNamesUnique"
helpviewer_keywords: 
  - "MemberNamesUnique element"
ms.assetid: bd4e75b2-4605-4ebc-a535-10f743eba08e
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent elements|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md),.[Hierarchy](../../../analysis-services/scripting/objects/hierarchy-element-assl.md)|  
|Child elements|None|  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  