---
title: "MSSQLSERVER_8689 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "8689 (Database Engine error)"
ms.assetid: 99467a32-6576-4272-a076-b16c06933f2a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_8689
    
## Details  
  
|||  
|-|-|  
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
  
## See Also  
 [Query Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-query)   
 [Plan Guides](../performance/plan-guides.md)  
  
  
