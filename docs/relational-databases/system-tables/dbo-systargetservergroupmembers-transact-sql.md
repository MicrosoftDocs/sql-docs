---
title: "dbo.systargetservergroupmembers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.component: "system-tables"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: system-objects
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.systargetservergroupmembers_TSQL"
  - "dbo.systargetservergroupmembers"
  - "systargetservergroupmembers"
  - "systargetservergroupmembers_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "systargetservergroupmembers system table"
ms.assetid: ee1b2ebd-03cb-4b91-a5d2-98d4d38f82ec
caps.latest.revision: 24
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.systargetservergroupmembers (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Records which target servers are currently enlisted in this multiserver group.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**servergroup_id**|**int**|Server group ID|  
|**server_id**|**int**|Server ID|  
  
  
