---
description: "MSSQLSERVER_10521"
title: "MSSQLSERVER_10521 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "10521 (Database Engine error)"
ms.assetid: ba2d7e44-207c-4428-b5f0-c975ac122c0d
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_10521
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|10521|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_PARAM_NEEDED|  
|Message Text|Cannot create plan guide '%.\*ls' because **\@type** was specified as '%ls' and the parameter '%ls' is NULL. This type requires a non-NULL value for the parameter. Specify a non-NULL value for the parameter, or change the type to one that allows a NULL value for the parameter.|  
  
## Explanation  
The type specified in **\@type** requires a non-NULL value for the specified parameter; however a NULL value was supplied.  
  
## User Action  
Specify a non-NULL value for the parameter, or change the type to one that allows a NULL value for the parameter.  
  
## See Also  
[sp_create_plan_guide &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)  
[Plan Guides](~/relational-databases/performance/plan-guides.md)  
[sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
  
