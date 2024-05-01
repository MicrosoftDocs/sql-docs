---
title: "Name Element for Database (DTA)"
description: In the dta utility, the Name element for Database specifies the name of a database that you want to tune.
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

# Name Element for Database (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the name of a database that you want to tune.  
  
## Syntax  
  
```  
  
<Server>  
    <Database>  
        <Name>...</Name>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, unlimited length.|  
|**Default value**|None.|  
|**Occurrence**|Required once per **Database** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Database Element for Server &#40;DTA&#41;](../../tools/dta/database-element-for-server-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example of this element, see [Server Element &#40;DTA&#41;](../../tools/dta/server-element-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
