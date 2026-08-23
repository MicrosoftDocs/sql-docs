---
title: "MSSQLSERVER_8712"
description: "MSSQLSERVER_8712"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "8712 (Database Engine error)"
---
# MSSQLSERVER_8712
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|8712|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|USEPLAN_ERR_NO_INDEX|  
|Message Text|Index '%.*ls', specified in the USE PLAN hint, does not exist. Specify an existing index, or create an index with the specified name.|  
  
## Explanation  
An index that is specified in the USE PLAN hint does not exist.  
  
## User Action  
Ensure all indexes that are specified in the USE PLAN hint exist.  
  
## Related content

- [Query hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md)
- [Plan Guides](../performance/plan-guides.md)
