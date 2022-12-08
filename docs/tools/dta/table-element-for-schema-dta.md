---
title: "Table Element for Schema (DTA)"
description: In the dta utility, the Table element for Schema specifies the table for tuning. This article describes that element.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Table element [DTA]"
ms.assetid: a59e8319-05d1-47f3-af39-7d970ab8e7dc
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# Table Element for Schema (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the table for tuning.  
  
## Syntax  
  
```  
  
<Schema>  
...code removed here...  
    <Table>...</Table>  
```  
  
## Element Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|**NumberOfRows**|Optional. Integer that allows you to simulate tables of different sizes.|  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, between 1 and 255 characters.|  
|**Default value**|None.|  
|**Occurrence**|Optional. List as many tables as appropriate for your workload.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Schema Element for Database &#40;DTA&#41;](../../tools/dta/schema-element-for-database-dta.md)|  
|**Child elements**|[Name Element for Table &#40;DTA&#41;](../../tools/dta/name-element-for-table-dta.md)|  
  
## Remarks  
 If you do not specify a **Table** element, Database Engine Tuning Advisor will assume all tables on the specified database can be tuned.  
  
## Example  
 For a usage example, see [Server Element &#40;DTA&#41;](../../tools/dta/server-element-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
