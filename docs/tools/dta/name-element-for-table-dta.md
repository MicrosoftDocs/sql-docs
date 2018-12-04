---
title: "Name Element for Table (DTA) | Microsoft Docs"
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
ms.assetid: 422a755f-ee52-4863-b1aa-f4ef1b8fd0bb
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Name Element for Table (DTA)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
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
  
  
