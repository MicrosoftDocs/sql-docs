---
title: "FeatureSet Element (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "FeatureSet element"
ms.assetid: f2070c53-4a5c-4c11-ac38-96ee200c84f0
author: stevestein
ms.author: sstein
manager: craigg
---
# FeatureSet Element (DTA)
  Contains the physical design structures (indexes or indexed views) that you would like Database Engine Tuning Advisor to use during analysis.  
  
## Syntax  
  
```  
  
<DTAInput>  
...code removed...  
    <TuningOptions>  
      <FeatureSet>...</FeatureSet>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|`string`, no maximum length.|  
|**Allowed values**|**IDX_IV**<br /> Indexes and indexed views.<br /><br /> **IDX**<br /> Indexes only.<br /><br /> **IV**<br /> Indexed views only.<br /><br /> **NCL_IDX**<br /> Nonclustered indexes only.<br /><br /> Use one of these values with this element.|  
|**Default value**|**IDX**|  
|**Occurrence**|Required once for each `TuningOptions` element, unless the `DropOnlyMode` element is used. If `DropOnlyMode` is used, you cannot use `FeatureSet`. These elements are mutually exclusive.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[TuningOptions Element &#40;DTA&#41;](tuningoptions-element-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example of this element, see the [Simple XML Input File Sample &#40;DTA&#41;](simple-xml-input-file-sample-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
