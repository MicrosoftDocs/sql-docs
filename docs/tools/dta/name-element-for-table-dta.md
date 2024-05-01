---
title: "Name Element for Table (DTA)"
description: In the dta utility, the Name element for Table specifies a table name for tuning. This article describes that element.
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

# Name Element for Table (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies a table name for tuning.  
  
## Syntax  
  
```  
  
<Schema>  
    <Table>  
        <Name>...</Name>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, between 1 and 255 characters.|  
|**Default value**|None.|  
|**Occurrence**|Required. Once for each **Table** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Table Element for Schema &#40;DTA&#41;](../../tools/dta/table-element-for-schema-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example, see [Server Element &#40;DTA&#41;](../../tools/dta/server-element-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
