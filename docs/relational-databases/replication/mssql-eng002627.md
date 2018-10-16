---
title: "MSSQL_ENG002627 | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG002627 error"
ms.assetid: 7f4136ac-3784-4a41-a98c-8a02308e4883
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# MSSQL_ENG002627
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2627|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name|N/A|  
|Message Text|Violation of %ls constraint '%.*ls'. Cannot insert duplicate key in object '%.\*ls'.|  
  
## Explanation  
 This is a general error that can be raised regardless of whether a database is replicated. In replicated databases, the error is typically raised because primary keys have not been managed appropriately across the topology. In a distributed environment it is essential to ensure that the same value is not inserted into a primary key column or any other unique column at more than one node. Possible causes include the following:  
  
-   Inserts and updates to a row are occurring at more than one node. Merge replication and updatable subscriptions for transactional replication both provide conflict detection and resolution, but it is still preferable to insert or update a given row at only one node. Peer-to-peer transactional does not provide conflict detection and resolution; it requires that inserts and updates be partitioned.  
  
-   A row was inserted at a Subscriber that should be read-only. Subscribers to snapshot publications should be treated as read-only, as should Subscribers to transactional publications unless updatable subscriptions or peer-to-peer transactional replication is used.  
  
-   A table with an identity column is being used, but the column is not managed appropriately.  
  
## User Action  
 The action required depends on the reason the error was raised:  
  
-   Inserts and updates to a row are occurring at more than one node.  
  
     Regardless of the type of replication used, we recommend that you partition inserts and updates whenever possible, because this reduces the processing required for conflict detection and resolution. For peer-to-peer transactional replication, partitioning inserts and updates is required. For more information, see [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md).  
  
-   A row was inserted at a Subscriber that should be read-only.  
  
     Do not insert or update rows at the Subscriber unless you are using merge replication, transactional replication with updatable subscriptions, or peer-to-peer transactional replication.  
  
-   A table with an identity column is being used, but the column is not managed appropriately.  
  
     For merge replication and transactional replication with updatable subscriptions, identity columns should be managed automatically by replication. For peer-to-peer transactional replication, they must be managed manually. For more information, see [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)   
 [Merge Replication](../../relational-databases/replication/merge/merge-replication.md)   
 [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)   
 [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)  
  
  
