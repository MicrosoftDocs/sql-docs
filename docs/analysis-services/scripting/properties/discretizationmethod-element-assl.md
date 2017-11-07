---
title: "DiscretizationMethod Element (ASSL) | Microsoft Docs"
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
  - "DiscretizationMethod Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DiscretizationMethod"
helpviewer_keywords: 
  - "DiscretizationMethod element"
ms.assetid: 4cfe015f-ad6c-47e1-8aff-c9c7677867b1
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DiscretizationMethod Element (ASSL)
  Defines the method to be used for discretization.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute> <!-- or ScalarMiningStructureColumn -->  
   ...  
   <DiscretizationMethod>...</DiscretizationMethod>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*None*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md), [ScalarMiningStructureColumn](../../../analysis-services/scripting/data-type/scalarminingstructurecolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **DiscretizationMethod** element determines how values for the **DimensionAttribute** or **ScalarMiningStructureColumn** are discretized, or organized into a specific set of groups. For more information about discretization methods, see [Discretization Methods &#40;Data Mining&#41;](../../../analysis-services/data-mining/discretization-methods-data-mining.md).  
  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Automatic*|Equivalent to the AUTOMATIC discretization method for mining structure columns.|  
|*EqualAreas*|Equivalent to the EQUAL_AREAS discretization method for mining structure columns.|  
|*Clusters*|Equivalent to the CLUSTERS discretization method for mining structure columns.|  
|*Thresholds*|Equivalent to the THRESHOLDS discretization method for mining structure columns.|  
|*EqualRanges*|Equivalent to the EQUAL_RANGES discretization method for mining structure columns.|  
  
 The enumeration corresponding to the allowed values for **DiscretizationMethod** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DiscretizationMethod>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  