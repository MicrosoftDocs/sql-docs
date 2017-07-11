---
title: "MSSQLSERVER_10520 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "10520 (Database Engine error)"
ms.assetid: cc8799f1-5b90-4248-b209-e1d5087f9529
caps.latest.revision: 9
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_10520
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10520|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_PARAM_NOT_ALLOWED|  
|Message Text|Cannot create plan guide '%.*ls' because @type was specified as '%ls' and a non-NULL value is specified for the parameter '%ls'. This type requires a NULL value for the parameter. Specify NULL for the parameter, or change the type to one that allows a non-NULL value for the parameter.|  
  
## Explanation  
The type specified in @type requires a NULL value for the specified parameter, however a non-NULL value was supplied.  
  
## User Action  
Specify NULL for the parameter, or change the type to one that allows a non-NULL value for the parameter.  
  
## See Also  
[sp_create_plan_guide &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)  
[Plan Guides](~/relational-databases/performance/plan-guides.md)  
  
