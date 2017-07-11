---
title: "Name Element for Database (DTA) | Microsoft Docs"
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
  - "Name element"
ms.assetid: e871c4fa-3b57-46cf-b4f8-e3be86f92dc4
caps.latest.revision: 15
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Name Element for Database (DTA)
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
  
  