---
title: "MdxMissingMemberMode Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MdxMissingMemberMode Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "MdxMissingMemberMode element"
ms.assetid: aca6130b-5fb8-4fa1-af8b-8e1ef361926f
author: minewiskan
ms.author: owend
manager: craigg
---
# MdxMissingMemberMode Element (ASSL)
  Determines how missing members are handled for Multidimensional Expressions (MDX) statements.  
  
## Syntax  
  
```xml  
  
<Dimension>  
   ...  
   <MdxMissingMemberMode>...</MdxMissingMemberMode>  
   ...  
</Dimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Default*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Dimension](../data-type/dimension-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Ignore*|Missing members are ignored.|  
|*Error*|An error is raised if missing members are encountered.|  
|*Default*|Missing members are ignored.|  
  
 The element that corresponds to the parent of `MdxMissingMemberMode` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Multidimensional Expressions &#40;MDX&#41; Reference](/sql/mdx/multidimensional-expressions-mdx-reference)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
