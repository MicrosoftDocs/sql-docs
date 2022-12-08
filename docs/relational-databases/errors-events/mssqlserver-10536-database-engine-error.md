---
description: "MSSQLSERVER_10536"
title: "MSSQLSERVER_10536 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "10536 (Database Engine error)"
ms.assetid: 9f97b41f-0ef8-4ad2-aec0-906a5d7522ba
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_10536
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|10536|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_TOO_MANY_STMTS|  
|Message Text|Cannot create plan guide '%.\*ls' because the batch or module corresponding to the specified **\@plan_handle** contains more than 1000 eligible statements. Create a plan guide for each statement in the batch or module by specifying a **statement_start_offset** value for each statement.|  
  
## Explanation  
The batch or module corresponding to the specified **\@plan_handle** contains more than 1000 eligible statements.  
  
## User Action  
Create a plan guide for each statement in the batch or module by specifying a **statement_start_offset** value for each statement.  
  
## See Also  
[sp_create_plan_guide &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)  
[Plan Guides](~/relational-databases/performance/plan-guides.md)  
[sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
  
