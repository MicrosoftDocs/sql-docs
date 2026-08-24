---
title: "MSSQLSERVER_8689"
description: "MSSQLSERVER_8689"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "8689 (Database Engine error)"
---
# MSSQLSERVER_8689
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|8689|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|USEPLAN_ERR_NO_DB|  
|Message Text|Database '%.*ls', specified in the USE PLAN hint, does not exist. Specify an existing database.|  
  
## Explanation  
A database that is specified in the USE PLAN hint does not exist.  
  
## User Action  
Ensure all databases that are specified in the USE PLAN hint exist.  
  
## Related content

- [Query hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md)
- [Plan Guides](../performance/plan-guides.md)
