---
title: "MSSQLSERVER_10521"
description: "MSSQLSERVER_10521"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "10521 (Database Engine error)"
---
# MSSQLSERVER_10521
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  
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
  
## Related content

- [sys.sp_create_plan_guide (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-transact-sql.md)
- [Plan Guides](../performance/plan-guides.md)
- [sys.sp_create_plan_guide_from_handle (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)
