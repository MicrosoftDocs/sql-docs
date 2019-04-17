---
title: "Database Element for Server (DTA) | Microsoft Docs"
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
  - "Database element"
ms.assetid: 5cd9a87a-af4b-45f3-8c18-f7fd7e7d3064
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Database Element for Server (DTA)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Specifies the database you want to tune on a specific server.  
  
## Syntax  
  
```  
  
<Server>  
...code removed here...  
    <Database>...</Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None.|  
|Default value|None.|  
|Occurrence|Required one or more times per **Server** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|Parent element|[Server Element &#40;DTA&#41;](../../tools/dta/server-element-dta.md)|  
|Child elements|[Name Element for Database &#40;DTA&#41;](../../tools/dta/name-element-for-database-dta.md)<br /><br /> [Schema Element for Database &#40;DTA&#41;](../../tools/dta/schema-element-for-database-dta.md)|  
  
## Remarks  
 This element is of the **DatabaseDetailsTypecomplexType** name in the Database Engine Tuning Advisor XML schema. Do not confuse this **Database** element with the one whose root parent is the **Configuration** element. For more information, see [Database Element for Configuration &#40;DTA&#41;](../../tools/dta/database-element-for-configuration-dta.md).  
  
## Example  
 For a usage example of the **Database** element, see [Server Element &#40;DTA&#41;](../../tools/dta/server-element-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
