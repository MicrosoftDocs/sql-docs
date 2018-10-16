---
title: "ClassifiedColumnID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ClassifiedColumnID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ClassifiedColumnID"
helpviewer_keywords: 
  - "ClassifiedColumnID element"
ms.assetid: c294b9c5-3ac2-4554-8ba8-d9f15d7e85c0
author: minewiskan
ms.author: owend
manager: craigg
---
# ClassifiedColumnID Element (ASSL)
  Contains the identifier (ID) of a related column that is classified by the [ScalarMiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ClassifiedColumns>  
   <ClassifiedColumnID>...</<ClassifiedColumnID>  
</ClassifiedColumns>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ClassifiedColumns](../collections/columns-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of the `ClassifiedColumns` collection in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [Content Element &#40;ASSL&#41;](content-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
