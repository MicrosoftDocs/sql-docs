---
title: "MSSQL_ENG021330 | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG021330 error"
ms.assetid: e2bb2e21-62a7-4689-b68b-bdfba3fdd985
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# MSSQL_ENG021330
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21330|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Failed to create a sub-directory under the replication working directory.(%ls)|  
  
## Explanation  
 This error can occur when a subscription is initialized manually, and there is a problem creating the directory in which replication scripts are stored. The error can be caused by a permissions issue: when a subscription is initialized without using a snapshot, the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service runs at the Publisher must have write permissions on the snapshot folder at the Distributor.  
  
## User Action  
 Ensure that the correct path has been specified for the snapshot folder and that the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service runs at the Publisher has sufficient permissions.  
  
## See Also  
 [Modify snapshot options](../../relational-databases/replication/snapshot-options.md)   
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)   
 [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md)  
  
  
