---
title: "Database Element for Workload (DTA)"
description: In the dta utility, the Database element for Workload, specifies the database where the workload trace table is located.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Database element"
ms.assetid: 112fca2a-37e5-4162-b2e7-b56eb8ab0c6f
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# Database Element for Workload (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the database where the workload trace table is located.  
  
## Syntax  
  
```  
  
<Workload>  
  <Database>  
   ...code removed here...  
  </Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Required once if no other type of workload is specified. You must specify an **EventString**, a **File**, or a **Database** child element for the **Workload** parent, but only one type can be used. For example, if you specify a workload with the **Database** element, you cannot also specify a workload with the **File** element in the same XML input file.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Workload Element &#40;DTA&#41;](../../tools/dta/workload-element-dta.md)|  
|**Child elements**|[Name Element for Database &#40;DTA&#41;](../../tools/dta/name-element-for-database-dta.md)<br /><br /> [Schema Element for Database &#40;DTA&#41;](../../tools/dta/schema-element-for-database-dta.md)|  
  
## Remarks  
 This element is of the **DatabaseDetailsTypecomplexType** name in the Database Engine Tuning Advisor XML schema. Do not confuse this **Database** element with the one whose root parent is the **Configuration** element. (See [Database Element for Configuration &#40;DTA&#41;](../../tools/dta/database-element-for-configuration-dta.md).)  
  
## Example  
 For a usage example of this **Database** element, see the code example in [Workload Element &#40;DTA&#41;](../../tools/dta/workload-element-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
