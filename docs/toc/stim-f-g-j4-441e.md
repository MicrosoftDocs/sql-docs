---
title: Initialize a Subscription with a Snapshot | Microsoft Docs
ms.date: 02/29/2020
ms.prod: sql
ms.technology: replication
ms.topic: conceptual
ms.author: genemi
author: MightyPen
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
ms.custom: at-gm-code=f-g-J4-441e
ROBOTS: NOINDEX, NOFOLLOW
ms.localizationpriority: "null"
localization_priority: "None"
---
# Initialize a Subscription with a Snapshot
[!INCLUDE[sql-asdbmi](../includes/applies-to-version/sql-asdbmi.md)]
  After a publication has been created, an initial snapshot is typically created and copied to the snapshot folder (this snapshot occurs by default for merge publications created with the New Publication Wizard). It's then applied to the Subscriber by the Distribution Agent (for transactional and snapshot publications) or the Merge Agent (for merge publications) during the initial synchronization of the subscription. The snapshot process depends on the type of publication:  
  
-   If the snapshot is for a snapshot publication, a transactional publication, or a merge publication that doesn't use parameterized filters, the snapshot contains the schema and data in bulk copy program (bcp) files, and as constraints, extended properties, indexes, triggers, and the system tables necessary for replication. For more information about creating and applying the snapshot, see [Create and Apply the Snapshot](../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
-   If the snapshot is for a merge publication that uses parameterized filters, the snapshot is created using a two-part process. First a schema snapshot is created that contains the replication scripts and the schema of the published objects, but not the data. Each subscription is then initialized with a snapshot that includes the scripts and schema copied from the schema snapshot and the data that belongs to the subscription's partition. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
 The snapshot consists of different files depending on the type of replication and the articles in your publication. These files are copied to the default snapshot folder specified when the Distributor was configured or the alternate snapshot folder specified when the publication was created.  
  
|Type of Replication|Common Snapshot Files|  
|-------------------------|---------------------------|  
|Snapshot Replication or Transactional Replication|schema (.sch); data (.bcp); constraints and indexes (.dri); constraints (.idx); triggers (.trg) for updating Subscribers only; compressed snapshot files (.cab).|  
|Merge Replication|schema (.sch); data (.bcp); constraints and indexes (.dri); triggers (.trg); system table data (.sys); conflict tables (.cft); compressed snapshot files (.cab).|  
  
 If the snapshot transfer is interrupted at any point, it will automatically resume and won't resend any files that have already been completely transferred. The unit of delivery for the Snapshot Agent is the bcp file for each publication article, so files that are partially delivered must be completely redelivered. However, resuming the snapshot can significantly reduce the amount of data transmitted and ensure timely snapshot delivery even if the connection is unreliable.  
  
## Snapshot Options  
 There are a number of options available when initializing a subscription with a snapshot. You can:  

_**(The remainder of the article has been cut from this test. You are done reading.)**_

&nbsp;
