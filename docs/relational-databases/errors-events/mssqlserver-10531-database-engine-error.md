---
title: "MSSQLSERVER_10531"
description: "MSSQLSERVER_10531"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "10531 (Database Engine error)"
---
# MSSQLSERVER_10531
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  
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
  
## Related content

- [Plan Guides](../performance/plan-guides.md)
- [sys.sp_create_plan_guide (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-transact-sql.md)
- [sys.sp_create_plan_guide_from_handle (Transact-SQL)](../system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)
