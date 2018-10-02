---
title: "HoldoutActualSize Element | Microsoft Docs"
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
  - "HoldoutActualSize"
helpviewer_keywords: 
  - "HoldoutActualSize element"
ms.assetid: 606a6674-cedb-4cee-82d0-26589f084dd9
author: minewiskan
ms.author: owend
manager: craigg
---
# HoldoutActualSize Element
  Indicates the actual size, after processing, of the holdout partition that contains the test set of a [MiningStructure](../objects/miningstructure-element-assl.md) element. The remaining cases in the data set are used for training. This property is read-only.  
  
## Syntax  
  
```xml  
  
<MiningStructure>  
   ...  
   <ddl100_100:HoldoutActualSize>...</ddl100_100:HoldoutActualSize>  
   ...  
</MiningStructure  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Read-only integer value.|  
|Default value|Not applicable|  
|Cardinality|0-1: Optional element that can occur one time and only one time.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningStructure](../objects/miningstructure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value for `HoldoutActualSize` depends on the source data, and on the values for [HoldoutMaxCases](holdoutmaxcases-element.md), [HoldoutMaxPercent](holdoutmaxpercent-element.md), and [HoldoutSeed](holdoutseed-element.md). Therefore, the value for `HoldoutActualSize` is not available until after [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] processes the mining structure.  
  
 The element that corresponds to the parent of `HoldoutActualSize` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructure>.  
  
> [!NOTE]  
>  In [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] did not support the use of holdout partitions on a mining structure. Therefore, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] Scripting Language (ASSL) statements that contain one of the holdout parameters, `HoldoutMaxCases`, `HoldoutMaxPercent`, `HoldoutSeed`, or `HoldoutActualSize`, cannot be used in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. If you use one of these holdout parameters in an ASSL statement in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will return an error.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)   
 [HoldoutMaxCases Element](holdoutmaxcases-element.md)   
 [HoldoutMaxPercent Element](holdoutmaxpercent-element.md)   
 [HoldoutSeed Element](holdoutseed-element.md)  
  
  
