---
title: "MSSQLSERVER_10533"
description: "MSSQLSERVER_10533"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "10533 (Database Engine error)"
---
# MSSQLSERVER_10533
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|10533|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_NAME_TOO_BIG|  
|Message Text|Cannot create plan guide '%.*ls' because the plan guide name exceeds 124 characters, the maximum number allowed. Specify a name that contains fewer than 125 characters.|  
  
## Explanation  
The plan guide name exceeds 124 characters, the maximum number allowed.  
  
## User Action  
Specify a name that contains fewer than 125 characters.  
  
## See Also  
[Plan Guides](~/relational-databases/performance/plan-guides.md)  
[sp_create_plan_guide &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)  
[sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
  
