---
title: "Usage Element (MiningModelColumn) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Usage Element (MiningModelColumn)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Usage"
helpviewer_keywords: 
  - "Usage element"
ms.assetid: 435a857e-37a9-434e-9de1-354f1ff2993f
author: minewiskan
ms.author: owend
manager: craigg
---
# Usage Element (MiningModelColumn) (ASSL)
  Describes how the associated column in the parent [MiningStructure](../objects/miningstructure-element-assl.md) is used.  
  
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
|Parent element|[MiningModelColumn](../data-type/miningmodelcolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Key*|The column is a key column.|  
|*Input*|The column is an input column.|  
|*Predict*|The column is a prediction column.|  
|*PredictOnly*|The column is a prediction column only.|  
|*None*|The column is not used by the model. **Warning:**  When the value of Usage is "None", Analysis Services does not send any value to the server by default; hence, the Usage attribute is not included in the request/response.|  
  
 The enumeration that corresponds to the allowed values for `Usage` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelColumnUsages>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
