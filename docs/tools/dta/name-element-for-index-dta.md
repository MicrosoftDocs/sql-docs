---
title: "Name Element for Index (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Name element"
ms.assetid: 2300e9cf-f0a8-49e6-b1f5-45ffe03ccb5f
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Name Element for Index (DTA)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Specifies a name for an index in the user-specified configuration.  
  
## Syntax  
  
```  
  
<Create>  
    <Index>  
        <Name>...</Name>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, unlimited length.|  
|**Default value**|None.|  
|**Occurrence**|Required once for each **Index** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Index Element &#40;DTA&#41;](../../tools/dta/index-element-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example of this element, see [XML Input File Sample with User-specified Configuration &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-user-specified-configuration-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
