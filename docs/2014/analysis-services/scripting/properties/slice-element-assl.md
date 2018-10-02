---
title: "Slice Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Slice Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Slice"
helpviewer_keywords: 
  - "Slice element"
ms.assetid: 2c8c4107-c641-4724-bfa5-0c47e0ec8888
author: minewiskan
ms.author: owend
manager: craigg
---
# Slice Element (ASSL)
  Contains a Multidimensional Expressions (MDX) expression defining the slice contained in a partition.  
  
## Syntax  
  
```xml  
  
<Partition>  
      ...  
   <Slice>...</Slice>  
   ...  
</Partition>  
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
|Parent element|[Partition](../objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `Slice` element contains an MDX tuple expression or set expression that identifies the portion of the cube for which the partition stores information. The MDX expression is similar to the [StrToSet](/sql/mdx/strtoset-mdx) MDX function with the CONSTRAINED keyword, in that the expression cannot include MDX or user-defined functions.  
  
 The element that corresponds to the parent of `Slice` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
