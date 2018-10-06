---
title: "UnknownMemberName Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "UnknownMemberName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "UnknownMemberName"
helpviewer_keywords: 
  - "UnknownMemberName element"
ms.assetid: 54271336-ea9b-4270-ac3a-9658a5cff77b
author: minewiskan
ms.author: owend
manager: craigg
---
# UnknownMemberName Element (ASSL)
  Contains the caption, in the default language of the dimension, for the unknown member of the dimension.  
  
## Syntax  
  
```xml  
  
<Dimension>  
      ...  
   <UnknownMemberName>...</UnknownMemberName>  
   ...  
</Dimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|*Unknown*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Dimension](../objects/dimension-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `UnknownMemberName` element supplies the caption used for the unknown member. The member ID of the unknown member is *Dimension*.UnknownMember, where *Dimension* is the unique name of the dimension, and cannot be changed.  
  
 The element that corresponds to the parent of `UnknownMemberName` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
