---
title: "DatabaseToConnect Element (DTA)"
description: In the dta utility, DatabaseToConnect element specifies the first database to which Database Engine Tuning Advisor connects when tuning a workload.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "DatabaseToConnect element"
ms.assetid: 65153a66-3aee-4429-99b7-0816ac23c285
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# DatabaseToConnect Element (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the first database to which Database Engine Tuning Advisor connects when tuning a workload.  
  
## Syntax  
  
```  
  
    <TuningOptions>  
...code removed here...  
      <DatabaseToConnect>...</DatabaseToConnect>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, unlimited length.|  
|**Default value**|None.|  
|**Occurrence**|Optional. Can use once for each **TuningOptions** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[TuningOptions Element &#40;DTA&#41;](../../tools/dta/tuningoptions-element-dta.md)|  
|**Child elements**|None|  
  
## Remarks  
 Use **DatabaseToConnect** to specify the name of the first database to which you want Database Engine Tuning Advisor to connect when it starts the tuning session. You can specify only one database with this element. If multiple database names are specified, Database Engine Tuning Advisor returns an error.  
  
## Example  
 For a usage example, see the [XML Input File Sample with Inline Workload &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-inline-workload-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
