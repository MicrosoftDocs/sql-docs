---
title: "MSSQLSERVER_10534"
description: "MSSQLSERVER_10534"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "10534 (Database Engine error)"
---
# MSSQLSERVER_10534
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  
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
  
## Related content

- [Plan Guides](../performance/plan-guides.md)
- [sys.sp_create_plan_guide (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-transact-sql.md)
- [sys.sp_create_plan_guide_from_handle (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)
