---
title: "MSSQLSERVER_10538"
description: "MSSQLSERVER_10538"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "10538 (Database Engine error)"
---
# MSSQLSERVER_10538
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|10538|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_INVALID_PLANGUIDE_HANDLE|  
|Message Text|Cannot find the plan guide either because the specified plan guide ID is NULL or invalid, or you do not have permission on the object referenced by the plan guide. Verify that the plan guide ID is valid, the current session is set to the correct database context, and you have either ALTER DATABASE permission or ALTER permission on the object referenced by the plan guide.|  
  
## Explanation  
The ID of the specified plan guide is NULL or invalid, or you do not have permission on the object referenced by the plan guide.  
  
## User Action  
Verify that the plan guide ID is valid, the current session is set to the correct database context, and you have ALTER DATABASE permission or ALTER permission on the object referenced by the plan guide.  
  
## Related content

- [sys.sp_create_plan_guide (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-transact-sql.md)
- [Plan Guides](../performance/plan-guides.md)
- [sys.sp_create_plan_guide_from_handle (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)
