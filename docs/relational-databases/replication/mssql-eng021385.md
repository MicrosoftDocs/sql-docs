---
title: "MSSQL_ENG021385 | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "MSSQL_ENG021385 error"
ms.assetid: a2c0444f-d97b-4760-8905-3574791c2e26
caps.latest.revision: 12
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQL_ENG021385
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21385|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Snapshot failed to process publication '%s'. Possibly due to active schema change activity or new articles being added.|  
  
## Explanation  
 This error is raised if the Snapshot Agent starts running when there are ongoing changes to the publication database, including adding or dropping articles and performing schema changes on published objects.  
  
## User Action  
 Restart the Snapshot Agent after changes to the publication database are complete. For more information, see [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/agents/start-and-stop-a-replication-agent-sql-server-management-studio.md) and [Replication Agent Executables Concepts](../../relational-databases/replication/concepts/replication-agent-executables-concepts.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  