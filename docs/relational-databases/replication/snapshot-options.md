---
title: "Snapshot Options | Microsoft Docs"
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
  - "snapshot replication [SQL Server], options"
  - "snapshots [SQL Server replication], options"
ms.assetid: 759fab42-66c7-4541-a7a3-bb6fb868493c
caps.latest.revision: 28
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Snapshot Options
  There are a number of options available when initializing a subscription with a snapshot:  
  
-   Specify an alternate snapshot folder location instead of or in addition to the default snapshot folder location. For more information, see [Alternate Snapshot Folder Locations](../../relational-databases/replication/alternate-snapshot-folder-locations.md).  
  
-   Compress snapshots for storage on removable media or for transfer over a slow network. For more information, see [Compressed Snapshots](../../relational-databases/replication/compressed-snapshots.md).  
  
-   Execute [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts before or after the snapshot is applied. For more information, see [Execute Scripts Before and After the Snapshot Is Applied](../../relational-databases/replication/execute-scripts-before-and-after-the-snapshot-is-applied.md).  
  
-   Transfer snapshot files using File Transfer Protocol (FTP). For more information, see [Transfer Snapshots Through FTP](../../relational-databases/replication/transfer-snapshots-through-ftp.md).  
  
## See Also  
 [Initialize a Subscription with a Snapshot](../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md)  
  
  