---
title: "MiningModelingFlag Data Type (ASSL) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MiningModelingFlag Data Type (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Defines a primitive data type that represents the available modeling flags for a [ModelingFlag](../../../analysis-services/scripting/objects/modelingflag-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningModelingFlag>...</MiningModelingFlag>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|String (enumeration)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|[ModelingFlag](../../../analysis-services/scripting/objects/modelingflag-element-assl.md) ([ModelingFlags](../../../analysis-services/scripting/collections/modelingflags-element-assl.md) collection of [MiningModelColumn](../../../analysis-services/scripting/data-type/miningmodelcolumn-data-type-assl.md) or [ScalarMiningStructureColumn](../../../analysis-services/scripting/data-type/scalarminingstructurecolumn-data-type-assl.md))|  
  
## Remarks  
 The flag name may contain spaces. The natively-supported values are listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*MODEL_EXISTENCE_ONLY*|The column should be modeled as having two states, missing and nonmissing, regardless of the values in the column. This is particularly useful for columns in a nested table, where values are sparse across cases.|  
|*NOT NULL*|The column cannot accept NULL values.|  
|*REGRESSOR*|The column supplies regressor values for test cases.|  
  
 Additional provider-specific flags may be used if third-party OLE DB or data mining providers have been aggregated on the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 A closely related element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelingFlags>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
