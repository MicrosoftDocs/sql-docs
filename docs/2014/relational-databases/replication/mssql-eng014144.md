---
title: "MSSQL_ENG014144 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG014144 error"
ms.assetid: fdc744d5-530e-48c4-9420-cca032fd482b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG014144
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14144|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Cannot drop Subscriber '%s'. There are subscriptions for it in the publication database '%s'.|  
  
## Explanation  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that is configured as a Subscriber cannot be removed from the role of Subscriber while there are active subscriptions configured for the instance.  
  
## User Action  
 Drop all associated subscriptions before attempting to change the Subscriber status of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance:  
  
1.  Execute [sp_helpsubscription &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql) in the publication database at the Publisher to find subscriptions.  
  
2.  Execute [sp_dropsubscription &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dropsubscription-transact-sql) in the publication database to drop subscriptions.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)   
 [Subscribe to Publications](subscribe-to-publications.md)  
  
  
