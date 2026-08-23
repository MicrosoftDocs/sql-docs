---
title: "MSSQLSERVER_10507"
description: "MSSQLSERVER_10507"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "10507 (Database Engine error)"
---
# MSSQLSERVER_10507
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|10507|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_STMT_DOES_NOT_MATCH|  
|Message Text|Cannot create plan guide '%.\*ls' because the statement specified by **\@stmt** and **\@module_or_batch**, or by **\@plan_handle** and **\@statement_start_offset**, does not match any statement in the specified module or batch. Modify the values to match a statement in the module or batch.|  
  
## Explanation  
A statement in the specified module or batch could not be matched to the specified statement or statement offset value.  
  
## User Action  
Modify the specified parameter values to match a statement in the module or batch.  
  
## Related content

- [Plan Guides](../performance/plan-guides.md)
- [sys.sp_create_plan_guide (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-transact-sql.md)
- [sys.sp_create_plan_guide_from_handle (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)
