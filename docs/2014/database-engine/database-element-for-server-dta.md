---
title: "Database Element for Server (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Database element"
ms.assetid: 5cd9a87a-af4b-45f3-8c18-f7fd7e7d3064
caps.latest.revision: 16
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---
# Database Element for Server (DTA)
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
|Occurrence|Required one or more times per `Server` element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|Parent element|[Server Element &#40;DTA&#41;](../../2014/database-engine/server-element-dta.md)|  
|Child elements|[Name Element for Database &#40;DTA&#41;](../../2014/database-engine/name-element-for-database-dta.md)<br /><br /> [Schema Element for Database &#40;DTA&#41;](../../2014/database-engine/schema-element-for-database-dta.md)|  
  
## Remarks  
 This element is of the **DatabaseDetailsTypecomplexType** name in the Database Engine Tuning Advisor XML schema. Do not confuse this `Database` element with the one whose root parent is the `Configuration` element. For more information, see [Database Element for Configuration &#40;DTA&#41;](../../2014/database-engine/database-element-for-configuration-dta.md).  
  
## Example  
 For a usage example of the `Database` element, see [Server Element &#40;DTA&#41;](../../2014/database-engine/server-element-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../relational-databases/performance/database-engine-tuning-advisor.md)  
  
  