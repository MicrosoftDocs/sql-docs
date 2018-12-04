---
title: "Name Element for Server (DTA) | Microsoft Docs"
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
ms.assetid: 4c94754d-6d62-4357-8ce7-f107ebf90c71
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Name Element for Server (DTA)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Contains the name of the server where the databases you want to tune reside.  
  
## Syntax  
  
```  
  
...code removed here...  
<Server>  
    <Name>...</Name>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, between 1 and 255 characters.|  
|**Default value**|None.|  
|**Occurrence**|Required once per **Server** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Server Element &#40;DTA&#41;](../../tools/dta/server-element-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For an example of how this **Name** element is used, see [Server Element &#40;DTA&#41;](../../tools/dta/server-element-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
