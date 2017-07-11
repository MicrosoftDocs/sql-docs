---
title: "HoldoutSeed Element | Microsoft Docs"
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
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "HoldoutSeed"
helpviewer_keywords: 
  - "HoldoutSeed element"
ms.assetid: 6b608bb3-c075-4744-9722-f5fb9fa1cc7e
caps.latest.revision: 23
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# HoldoutSeed Element
  Specifies the seed for a repeatable holdout partition that contains the test set of a [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md) element. This seed ensures that the model content remains the same during reprocessing. If unspecified or set to 0, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] creates a seed by using a hashing algorithm on the name of the mining structure.  
  
## Syntax  
  
```xml  
  
<MiningStructure>  
   ...  
   <ddl100_100:HoldoutSeed>...</ddl100_100:HoldoutSeed>  
   ...  
</MiningStructure>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Long|  
|Default value|0|  
|Cardinality|0-1: Optional element that can occur one time and one time only.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 When you first create a mining structure, the ID and the name are the same. However, you can change the name of the mining structure. Therefore, if you want to ensure that the partition is repeatable, you should not rely on the seed created by the name, but should explicitly set a seed.  
  
 Additionally, when you create a copy of a mining structure by using the **EXPORT** statement, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will retain the name for the new mining structure but will automatically generate a new ID. Therefore, it is possible to have two mining structures that share the same name but have different IDs. Any two mining structures that have the same name will have the same seed. However, as the partitioning of the data also depends on the source data, the actual contents of the partitions in each structure may be different.  
  
 The new properties **HoldoutMaxCases**, **HoldoutMaxPercent**, **HoldoutSeed**, or **HoldoutActualSize** are available only in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] and later versions. Therefore, you must prefix these properties with the new namespace as shown in the syntax description, or [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will return an error.  
  
 **Note** In [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] did not support the use of holdout partitions on a mining structure. Therefore, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] Scripting Language (ASSL) statements that contain the holdout parameters **HoldoutMaxCases**, **HoldoutMaxPercent**, **HoldoutSeed**, or **HoldoutActualSize** cannot be used in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. If you use one of these holdout parameters in an ASSL statement in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will return an error.  
  
 The element that corresponds to the parent of **HoldoutSeed** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructure>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)   
 [HoldoutActualSize Element](../../../analysis-services/scripting/properties/holdoutactualsize-element.md)   
 [HoldoutMaxPercent Element](../../../analysis-services/scripting/properties/holdoutmaxpercent-element.md)   
 [HoldoutMaxCases Element](../../../analysis-services/scripting/properties/holdoutmaxcases-element.md)  
  
  