---
title: "Schema Element for Database (DTA)"
description: In the dta utility, the Schema element for Database specifies the schema of the database that you want to tune.
author: markingmyname
ms.author: maghan
ms.date: 03/01/2017
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
helpviewer_keywords:
  - "Schema element"
dev_langs:
  - "XML"
---

# Schema Element for Database (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the schema of the database that you want to tune.  
  
## Syntax  
  
```  
  
<Database>  
...code removed here...  
    <Schema>...</Schema>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Required once for the **Database** element that is specified under the **Server** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Database Element for Server &#40;DTA&#41;](../../tools/dta/database-element-for-server-dta.md)|  
|**Child elements**|[Name Element for Schema &#40;DTA&#41;](../../tools/dta/name-element-for-schema-dta.md)<br /><br /> [Table Element for Schema &#40;DTA&#41;](../../tools/dta/table-element-for-schema-dta.md)|  
  
## Example  
 For a usage example of this element, see [Server Element &#40;DTA&#41;](../../tools/dta/server-element-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
