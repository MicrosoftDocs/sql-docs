---
description: "MSSQLSERVER_10539"
title: "MSSQLSERVER_10539 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "10539 (Database Engine error)"
ms.assetid: 49c26ff7-18b8-4f07-a087-f45f63463b3b
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_10539
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|10539|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_NO_PLAN_FOR_STMT|  
|Message Text|Cannot create plan guide '%.*ls' from cache because a query plan is not available for the statement with start offset %d. This problem can occur if the statement depends on database objects that have not yet been created. Make sure that all necessary database objects exist, and execute the statement before creating the plan guide.|  
  
## Explanation  
A query plan is not available in the plan cache for the statement with the specified starting offset.  
  
## User Action  
Make sure that all necessary database objects exist, and execute the statement before creating the plan guide.  
  
## See Also  
[sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
  
