---
description: "MSSQLSERVER_10531"
title: "MSSQLSERVER_10531 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "10531 (Database Engine error)"
ms.assetid: bb40e994-231c-44ce-933f-8d767fb2f450
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_10531
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|10531|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_NO_ELIGIBLE_STMT|  
|Message Text|Cannot create plan guide '%.*ls' from cache because the user does not have adequate permissions. Grant the VIEW SERVER STATE permission to the user creating the plan guide.|  
  
## Explanation  
The user does not have adequate permissions to create the specified plan guide from the plan cache.  
  
## User Action  
Grant VIEW SERVER STATE permission to the user creating the plan guide.  
  
## See Also  
[Plan Guides](~/relational-databases/performance/plan-guides.md)  
[sp_create_plan_guide &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)  
[sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
  
