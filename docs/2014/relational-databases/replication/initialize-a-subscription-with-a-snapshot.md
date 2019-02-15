---
title: "Initialize a Subscription with a Snapshot | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "snapshots [SQL Server replication], initializing subscriptions"
  - "initializing subscriptions [SQL Server replication], snapshots"
ms.assetid: 77a9ade2-cdc0-4ae9-a02d-6e29d7c2ada0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Initialize a Subscription with a Snapshot
  After a publication has been created, an initial snapshot is typically created and copied to the snapshot folder (this occurs by default for merge publications created with the New Publication Wizard). It is then applied to the Subscriber by the Distribution Agent (for transactional and snapshot publications) or the Merge Agent (for merge publications) during the initial synchronization of the subscription. The snapshot process depends on the type of publication:  
  
-   If the snapshot is for a snapshot publication, a transactional publication, or a merge publication that doesn't use parameterized filters, the snapshot contains the schema and data in bulk copy program (bcp) files, as well as constraints, extended properties, indexes, triggers, and the system tables necessary for replication. For more information about creating and applying the snapshot, see [Create and Apply the Snapshot](create-and-apply-the-snapshot.md).  
  
-   If the snapshot is for a merge publication that uses parameterized filters, the snapshot is created using a two-part process. First a schema snapshot is created that contains the replication scripts and the schema of the published objects, but not the data. Each subscription is then initialized with a snapshot that includes the scripts and schema copied from the schema snapshot and the data that belongs to the subscription's partition. For more information, see [Snapshots for Merge Publications with Parameterized Filters](snapshots-for-merge-publications-with-parameterized-filters.md).  
  
 The snapshot consists of different files depending on the type of replication and the articles in your publication. These files are copied to the default snapshot folder specified when the Distributor was configured or the alternate snapshot folder specified when the publication was created.  
  
|Type of Replication|Common Snapshot Files|  
|-------------------------|---------------------------|  
|Snapshot Replication or Transactional Replication|schema (.sch); data (.bcp); constraints and indexes (.dri); constraints (.idx); triggers (.trg):for updating Subscribers only; compressed snapshot files (.cab).|  
|Merge Replication|schema (.sch); data (.bcp); constraints and indexes (.dri); triggers (.trg); system table data (.sys); conflict tables (.cft); compressed snapshot files (.cab).|  
  
 If the snapshot transfer is interrupted at any point, it will automatically resume and will not resend any files that have already been completely transferred. The unit of delivery for the Snapshot Agent is the bcp file for each publication article, so files that are partially delivered must be completely redelivered. However, resuming the snapshot can significantly reduce the amount of data transmitted and ensure timely snapshot delivery even if the connection is unreliable.  
  
## Snapshot Options  
 There are a number of options available when initializing a subscription with a snapshot. You can:  
  
-   Specify an alternate snapshot folder location instead of, or in addition, to the default snapshot folder location. For more information, see [Alternate Snapshot Folder Locations](alternate-snapshot-folder-locations.md).  
  
-   Compress snapshots for storage on removable media or for transfer over a slow network. For more information, see [Compressed Snapshots](compressed-snapshots.md).  
  
-   Execute Transact-SQL scripts before or after the snapshot is applied. For more information, see [Execute Scripts Before and After the Snapshot Is Applied](snapshot-options.md#execute-scripts-before-and-after-snapshot-is-applied).  
  
-   Transfer snapshot files using File Transfer Protocol (FTP). For more information, see [Transfer Snapshots Through FTP](transfer-snapshots-through-ftp.md).  
  
## See Also  
 [Initialize a Subscription](initialize-a-subscription.md)   
 [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md)  
  
  
