---
title: "ClassifiedColumns Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ClassifiedColumns Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ClassifiedColumns"
helpviewer_keywords: 
  - "ClassifiedColumns element"
ms.assetid: f16b4f51-c38d-4601-98b8-1497dbf12d02
author: minewiskan
ms.author: owend
manager: craigg
---
# ClassifiedColumns Element (ASSL)
  Contains the collection of related columns that are classified by the [ScalarMiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningStructureColumn xsi:type="ScalarMiningStructureColumn">  
   ...  
   <ClassifiedColumns>  
      <ClassifiedColumnID>...</ClassifiedColumnID>  
   </ClassifiedColumns>  
   ...  
</MiningStructureColumn>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md) of type[ScalarMiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md)|  
|Child elements|[ClassifiedColumnID](../properties/id-element-assl.md)|  
  
## Remarks  
 The element that corresponds to the parent of `ClassifiedColumns` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [MiningStructureColumn Data Type &#40;ASSL&#41;](../data-type/miningstructurecolumn-data-type-assl.md)   
 [MiningStructure Element &#40;ASSL&#41;](../objects/miningstructure-element-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
