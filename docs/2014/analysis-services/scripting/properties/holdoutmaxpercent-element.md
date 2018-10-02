---
title: "HoldoutMaxPercent Element | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
topic_type: 
  - "apiref"
f1_keywords: 
  - "HoldoutMaxPercent"
helpviewer_keywords: 
  - "HoldoutMaxPercent element"
ms.assetid: e375cc51-5f9d-4252-98a1-326ca0dbbf83
author: minewiskan
ms.author: owend
manager: craigg
---
# HoldoutMaxPercent Element
  Specifies the maximum percentage of cases in the data source that will be used for the holdout partition that contains the test set of a [MiningStructure](../objects/miningstructure-element-assl.md) element. The remaining cases are used for training. A value of 0 indicates that there is no limit to the number of cases that can be held out as the test set.  
  
## Syntax  
  
```xml  
  
<MiningStructure>  
   ...  
   <ddl100_100:HoldoutMaxPercent>...</ddl100_100:HoldoutMaxPercent>  
   ...  
</MiningStructure>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer between 0 and 99.|  
|Default value|30|  
|Cardinality|0-1: Optional element that can occur one time and one time only.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningStructure](../objects/miningstructure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If you specify values for both `HoldoutMaxPercent` and `HoldoutMaxCases`, the algorithm limits the test set to the smaller of the two values.  
  
 If `HoldoutMaxCases` is set to the default of 0, and a value has not been set for `HoldoutMaxPercent`, the algorithm uses the complete data set for training.  
  
 The new properties `HoldoutMaxCases`, `HoldoutMaxPercent`, `HoldoutSeed`, or `HoldoutActualSize` are available only in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] and later versions. Therefore, you must prefix these properties by using the new namespace as shown in the syntax description, or [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will return an error.  
  
> [!NOTE]  
>  In [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] did not support the use of holdout partitions on a mining structure. Therefore, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] Scripting Language (ASSL) statements that contain one of the holdout parameters, `HoldoutMaxCases`, `HoldoutMaxPercent`, `HoldoutSeed`, or `HoldoutActualSize`, cannot be used in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. If you use one of these holdout parameters in an ASSL statement in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will return an error.  
  
 The element that corresponds to the parent of `HoldoutMaxPercent` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructure>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)   
 [HoldoutMaxCases Element](holdoutmaxcases-element.md)   
 [HoldoutSeed Element](holdoutseed-element.md)   
 [HoldoutActualSize Element](holdoutactualsize-element.md)  
  
  
