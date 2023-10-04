---
title: "MSSQLSERVER_10534"
description: "MSSQLSERVER_10534"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "10534 (Database Engine error)"
---
# MSSQLSERVER_10534
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|10534|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_INVALID_PARAMS|  
|Message Text|Cannot create plan guide '%.\*ls' because the value specified for **\@params** is invalid. Specify the value in the form *parameter_name parameter_type*, or specify NULL.|  
  
## Explanation  
The value specified for **\@params** is invalid.  
  
## User Action  
Specify the value in the form *parameter_name parameter_type*, or specify NULL.  
  
## See Also  
[Plan Guides](~/relational-databases/performance/plan-guides.md)  
[sp_create_plan_guide &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)  
[sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
  
