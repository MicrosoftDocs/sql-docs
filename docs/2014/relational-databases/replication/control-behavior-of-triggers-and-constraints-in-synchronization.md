---
title: "Control the Behavior of Triggers and Constraints During Synchronization (Replication Transact-SQL Programming) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "identities [SQL Server replication]"
  - "constraints [SQL Server], replication"
  - "triggers [SQL Server], replication"
  - "triggers [SQL Server replication]"
  - "constraints [SQL Server replication]"
  - "NOT FOR REPLICATION option"
  - "NFR option"
ms.assetid: 7c4e0f0e-cadc-4c99-98f4-69799b9b356b
caps.latest.revision: 35
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Control the Behavior of Triggers and Constraints During Synchronization (Replication Transact-SQL Programming)
  During synchronization, replication agents execute [INSERT &#40;Transact-SQL&#41;](../Topic/INSERT%20\(Transact-SQL\).md), [UPDATE &#40;Transact-SQL&#41;](../Topic/UPDATE%20\(Transact-SQL\).md), and [DELETE &#40;Transact-SQL&#41;](../Topic/DELETE%20\(Transact-SQL\).md) statements on replicated tables, which can cause data manipulation language (DML) triggers on these tables to be executed. There are cases when you may need to prevent these triggers from firing or constraints from being enforced during synchronization. This behavior depends on how the trigger or constraint is created.  
  
### To prevent triggers from executing during synchronization  
  
1.  When creating a new trigger, specify the NOT FOR REPLICATION option of [CREATE TRIGGER &#40;Transact-SQL&#41;](../Topic/CREATE%20TRIGGER%20\(Transact-SQL\).md).  
  
2.  For an existing trigger, specify the NOT FOR REPLICATION option of [ALTER TRIGGER &#40;Transact-SQL&#41;](../Topic/ALTER%20TRIGGER%20\(Transact-SQL\).md).  
  
### To prevent constraints from being enforced during synchronization  
  
1.  When creating a new CHECK or FOREIGN KEY constraint, specify CHECK NOT FOR REPLICATION option in the constraint definition of [CREATE TABLE &#40;Transact-SQL&#41;](../Topic/CREATE%20TABLE%20\(Transact-SQL\).md).  
  
## See Also  
 [Create Tables &#40;Database Engine&#41;](../../../2014/database-engine/create-tables-database-engine.md)  
  
  