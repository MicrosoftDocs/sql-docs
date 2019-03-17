---
title: "DTAInput Element (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "DTAInput element"
ms.assetid: 40c19abf-ded5-43de-be96-5b43b1b81b03
author: stevestein
ms.author: sstein
manager: craigg
---
# DTAInput Element (DTA)
  Contains the definition of XML input for Database Engine Tuning Advisor.  
  
## Syntax  
  
```  
  
<DTAXML>  
    <DTAInput>  
    ...code removed here...  
    </DTAInput>  
```  
  
## Element Characteristics  
  
|Characteristics|Description|  
|---------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Optional once per **DTAXML** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[DTAXML Element &#40;DTA&#41;](dtaxml-element-dta.md)|  
|**Child elements**|[Server Element &#40;DTA&#41;](server-element-dta.md)<br /><br /> [Workload Element &#40;DTA&#41;](workload-element-dta.md)<br /><br /> [TuningOptions Element &#40;DTA&#41;](tuningoptions-element-dta.md)<br /><br /> [Configuration Element &#40;DTA&#41;](configuration-element-dta.md)|  
  
## Remarks  
 This element is the root of the Database Engine Tuning Advisor input schema hierarchy. Input to Database Engine Tuning Advisor can be arguments that specify the servers whose databases you want to tune, workloads, tuning options, or a user-specified configuration.  
  
## Example  
 For a usage example of the **DTAInput** element, see [Simple XML Input File Sample &#40;DTA&#41;](simple-xml-input-file-sample-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
