---
title: "MSSQL_ENG021330 | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "MSSQL_ENG021330 error"
ms.assetid: e2bb2e21-62a7-4689-b68b-bdfba3fdd985
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQL_ENG021330
    
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
 [Specify the Default Snapshot Location &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/specify-the-default-snapshot-location-sql-server-management-studio.md)   
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)   
 [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md)  
  
  