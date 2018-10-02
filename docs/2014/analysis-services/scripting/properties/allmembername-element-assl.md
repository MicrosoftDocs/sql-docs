---
title: "AllMemberName Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AllMemberName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AllMemberName"
helpviewer_keywords: 
  - "AllMemberName element"
ms.assetid: 5fda5563-cb7a-4402-82ad-3d6c432b6a7e
author: minewiskan
ms.author: owend
manager: craigg
---
# AllMemberName Element (ASSL)
  Contains the caption in the default language for the All member of a [Hierarchy](../objects/hierarchy-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Hierarchy>  
      ...  
      <AllMemberName>...</AllMemberName>  
   ...  
</Dimension>  
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
|Parent element|[Hierarchy](../objects/hierarchy-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `AllMemberName` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Hierarchy>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
