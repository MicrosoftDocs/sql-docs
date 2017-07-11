---
title: "Schema Element for Database (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Schema element"
ms.assetid: d932e59c-953f-4ab4-934d-b6baf344835c
caps.latest.revision: 14
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Schema Element for Database (DTA)
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
  
  