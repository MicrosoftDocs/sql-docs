---
title: "DiscretizationBucketCount Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DiscretizationBucketCount Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DiscretizationBucketCount"
helpviewer_keywords: 
  - "DiscretizationBucketCount element"
ms.assetid: 551a73ae-59e1-4079-a2d9-988df96b5e07
author: minewiskan
ms.author: owend
manager: craigg
---
# DiscretizationBucketCount Element (ASSL)
  Contains the number of buckets into which to discretize.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute> <!-- or ScalarMiningStructureColumn -->  
   ...  
   <DiscretizationBucketCount>...</DiscretizationBucketCount>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md), [ScalarMiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `DiscretizationBucketCount` element determines how many groups or "buckets" are created when values for the `DimensionAttribute` or `ScalarMiningStructureColumn` are discretized, or organized into a specific set of groups. If the element is not specified, or if zero is specified for the value of the element, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] creates an appropriate number of groups depending on the discretization method. For more information about discretization methods, see [Discretization Methods &#40;Data Mining&#41;](../../data-mining/discretization-methods-data-mining.md).  
  
 The elements that correspond to the parents of `DiscretizationBucketCount` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.DimensionAttribute> and <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [DiscretizationMethod Element &#40;ASSL&#41;](discretizationmethod-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
