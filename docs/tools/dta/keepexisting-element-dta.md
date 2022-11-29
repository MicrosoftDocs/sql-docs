---
title: "KeepExisting Element (DTA)"
description: In the dta utility, the KeepExisting element specifies physical design structures that Database Engine Tuning Advisor retains when it generates recommendations.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "KeepExisting element"
ms.assetid: e67aae61-d06d-4a03-85ba-6516c3502dce
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# KeepExisting Element (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the physical design structures (indexes, indexed views, or partitioning) that Database Engine Tuning Advisor must retain when generating its recommendation.  
  
## Syntax  
  
```  
  
<DTAInput>  
...code removed...  
    <TuningOptions>  
      <KeepExisting>...</KeepExisting>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, length limit enforced by the server.|  
|**Allowed values**|**NONE**<br /> No existing structures.<br /><br /> **ALL**<br /> All existing structures.<br /><br /> **ALIGNED**<br /> All partition-aligned structures.<br /><br /> **CL_IDX**<br /> All clustered indexes on tables.<br /><br /> **IDX**<br /> All clustered and nonclustered indexes on tables.<br /><br /> Use only one of these values with this element.|  
|**Default value**|None.|  
|**Occurrence**|Optional. Can use only once for each **TuningOptions** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[TuningOptions Element &#40;DTA&#41;](../../tools/dta/tuningoptions-element-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example of this element, see the [Simple XML Input File Sample &#40;DTA&#41;](../../tools/dta/simple-xml-input-file-sample-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
