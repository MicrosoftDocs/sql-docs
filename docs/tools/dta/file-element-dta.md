---
title: "File Element (DTA)"
description: In the dta utility, the File element specifies a workload file, which includes Transact-SQL statements that execute for a database to tune.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "File element"
ms.assetid: 73dce835-9a80-4aef-8e0f-9dcf07dd5b80
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# File Element (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the workload file. A workload is a set of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that execute against a database or databases that you want to tune. Workload files can be [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts (.sql) or trace files (.trc). For more information, see[Start and Use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md).  
  
## Syntax  
  
```  
  
<DTAInput>  
  <Server>...</Server>  
  <Workload>  
    <File>...</File>  
  </Workload>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|Use the **string** data type to specify the directory path to your workload file. For example:<br /><br /> `<File>C:\Tuning\tun.sql</File>`<br /><br /> Note that length limit is enforced by the server.|  
|**Default value**|None.|  
|**Occurrence**|Required once if no other type of workload is specified. You must specify an **EventString**, a **File**, or a **Database** child element for the **Workload** parent, but only one type can be used. For example, if you specify a workload with the **File** element, then you cannot also specify a workload with the **Database** element in the same XML input file.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Workload Element &#40;DTA&#41;](../../tools/dta/workload-element-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example of this element, see [Simple XML Input File Sample &#40;DTA&#41;](../../tools/dta/simple-xml-input-file-sample-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
