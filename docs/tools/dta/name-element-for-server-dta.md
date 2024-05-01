---
title: "Name Element for Server (DTA)"
description: In the dta utility, the Name element for Server contains the name of the server where the databases you want to tune reside.
author: markingmyname
ms.author: maghan
ms.date: 03/01/2017
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
helpviewer_keywords:
  - "Name element"
dev_langs:
  - "XML"
---

# Name Element for Server (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
  
  
