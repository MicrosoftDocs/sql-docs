---
title: "HoldoutMaxCases Element | Microsoft Docs"
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
  - "HoldoutMaxCases"
helpviewer_keywords: 
  - "HoldoutMaxCases element"
ms.assetid: 58d94d10-e11e-4368-b3b8-dff23e1947cd
caps.latest.revision: 21
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# HoldoutMaxCases Element
  Specifies the maximum number of cases in the data source to be used for the holdout partition that contains the test set of a [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md) element. The remaining cases in the data set are used for training. A value of 0 indicates that there is no limit to the number of cases that can be held out as the test set.  
  
## Syntax  
  
```xml  
  
<MiningStructure>  
   ...  
   <ddl100_100:HoldoutMaxCases>...</ddl100_100:HoldoutMaxCases>  
   ...  
</MiningStructure>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer greater than 0.|  
|Default value|0|  
|Cardinality|0-1: Optional element that can occur one time and one time only.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If you specify values for both **HoldoutMaxPercent** and **HoldoutMaxCases**, the algorithm limits the test set to the smaller of the two values.  
  
 If **HoldoutMaxCases** is set to the default of 0, and a value has not been set for **HoldoutMaxPercent**, the algorithm uses the entire data set for training.  
  
 The new properties **HoldoutMaxCases**, **HoldoutMaxPercent**, **HoldoutSeed**, or **HoldoutActualSize** are available only in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] and later versions. Therefore, you must prefix these properties with the new namespace as shown in the syntax description, or [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will return an error.  
  
> [!NOTE]  
>  In [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] did not support the use of holdout partitions on a mining structure. Therefore, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] Scripting Language (ASSL) statements that contain one of the holdout parameters, **HoldoutMaxCases**, **HoldoutMaxPercent**, **HoldoutSeed**, or **HoldoutActualSize**, cannot be used in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. If you use one of these holdout parameters in an ASSL statement in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will return an error.  
  
 The element that corresponds to the parent of **HoldoutMaxCases** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructure>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)   
 [HoldoutMaxPercent Element](../../../analysis-services/scripting/properties/holdoutmaxpercent-element.md)   
 [HoldoutSeed Element](../../../analysis-services/scripting/properties/holdoutseed-element.md)   
 [HoldoutActualSize Element](../../../analysis-services/scripting/properties/holdoutactualsize-element.md)  
  
  