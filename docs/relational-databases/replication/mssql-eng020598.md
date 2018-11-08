---
title: "MSSQL_ENG020598 | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG020598 error"
ms.assetid: 1c3032f2-23d1-4ead-94ee-16ac4d75cd0c
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# MSSQL_ENG020598
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|20598|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The row was not found at the Subscriber when applying the replicated command.|  
  
## Explanation  
 This error is raised in transactional replication if the Distribution Agent attempts to update a row at the Subscriber, but the row has been deleted or the primary key of the row has been changed. By default, Subscribers to transactional publications should be treated as read-only, because changes are not propagated back to the Publisher. For transactional replication, user changes should be made at the Subscriber only if updatable subscriptions or peer-to-peer replication is used. For information about these options, see [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md) and [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md).  
  
## User Action  
 **To resolve this problem:**  
  
1.  If replication must continue while you identify the source of the error, specify the parameter **-SkipErrors 20598** for the Distribution Agent. This allows the agent to skip changes that result in error 20598, while allowing other changes to be replicated.  
  
2.  Identify which rows at the Subscriber have been deleted or have a different primary key than the corresponding rows at the Publisher. You can use the [tablediff Utility](../../tools/tablediff-utility.md) to determine which rows are different in the publication and subscription databases. For information about using this utility with replicated databases, see [Compare Replicated Tables for Differences &#40;Replication Programming&#41;](../../relational-databases/replication/administration/compare-replicated-tables-for-differences-replication-programming.md).  
  
3.  Correct the rows at the Subscriber using the **tablediff** utility or another method.  
  
4.  (Optional) Remove the **-SkipErrors** parameter.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
