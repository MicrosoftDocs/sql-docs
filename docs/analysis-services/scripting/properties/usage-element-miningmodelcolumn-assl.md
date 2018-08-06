---
title: "Usage Element (MiningModelColumn) (ASSL) | Microsoft Docs"
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
# Usage Element (MiningModelColumn) (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Describes how the associated column in the parent [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md) is used.  
  
## Syntax  
  
```xml  
  
<MiningModelColumn>  
   ...  
   <Usage>...</Usage>  
   ...  
</MiningModelColumn>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningModelColumn](../../../analysis-services/scripting/data-type/miningmodelcolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Key*|The column is a key column.|  
|*Input*|The column is an input column.|  
|*Predict*|The column is a prediction column.|  
|*PredictOnly*|The column is a prediction column only.|  
|*None*|The column is not used by the model.<br /><br /> **\*\* Warning \*\*** When the value of Usage is "None", Analysis Services does not send any value to the server by default; hence, the Usage attribute is not included in the request/response.|  
  
 The enumeration that corresponds to the allowed values for **Usage** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelColumnUsages>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
