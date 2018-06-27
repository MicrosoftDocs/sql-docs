---
title: "DiscretizationMethod Element (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# DiscretizationMethod Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
  
  
