---
title: "TuningTimeInMin Element (DTA)"
description: In the dta utility, the TuningTimeInMin element specifies the maximum length of a tuning session in minutes.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "TuningTimeInMin element"
ms.assetid: 4973d9ac-20fd-4ac3-bc9f-5d60e39fdb7d
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# TuningTimeInMin Element (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the maximum length of a tuning session in minutes.  
  
## Syntax  
  
```  
  
<DTAInput>  
...code removed...  
    <TuningOptions>  
      <TuningTimeInMin>...</TuningTimeInMin>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**unsignedInt**, unlimited length.|  
|**Default value**|480 minutes (8 hours).|  
|**Occurrence**|Required unless a value has been specified for the **NumberOfEvents** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[TuningOptions Element &#40;DTA&#41;](../../tools/dta/tuningoptions-element-dta.md)|  
|**Child elements**|None|  
  
## Example  
  
## Description  
 The following code example shows how to set 12 hours as the maximum tuning time:  
  
## Code  
  
```  
<DTAInput>  
  <Server>...</Server>  
  <Workload>...</Workload>  
  <TuningOptions>  
    <TuningTimeInMin>720</TuningTimeInMin>  
...code removed here...  
</DTAInput>  
```  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
