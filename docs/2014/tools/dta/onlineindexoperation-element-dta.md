---
title: "OnlineIndexOperation Element (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "OnlineIndexOperation element"
ms.assetid: 7c5614cd-09aa-4a59-9591-347aa7d36473
author: stevestein
ms.author: sstein
manager: craigg
---
# OnlineIndexOperation Element (DTA)
  Specifies whether the indexes, indexed views, or partitions that Database Engine Tuning Advisor recommends can be created online.  
  
## Syntax  
  
```  
  
<DTAInput>  
...code removed...  
    <TuningOptions>  
      <OnlineIndexOperation>...</OnlineIndexOperation>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|`string`, no maximum length.|  
|**Allowed values**|**OFF**<br /> No recommended physical design structures can be created online.<br /><br /> **ON**<br /> All recommended physical design structures can be created online.<br /><br /> **MIXED**<br /> Database Engine Tuning Advisor attempts to recommend physical design structures that can be created online when possible.<br /><br /> Use one of these values with this element. If indexes are created online, the keyword **ONLINE = ON** is appended to its object definition.|  
|**Default value**|None.|  
|**Occurrence**|Optional. If used, can only be used once for the `TuningOptions` element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[TuningOptions Element &#40;DTA&#41;](tuningoptions-element-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example of this element, see the [Simple XML Input File Sample &#40;DTA&#41;](simple-xml-input-file-sample-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
