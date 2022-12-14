---
title: "Merge Replication | Microsoft Docs"
description: Merge replication uses a snapshot of the publication database objects and data, and then tracks modifications at the Publisher and Subscribers with triggers.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "merge replication [SQL Server replication], about merge replication"
  - "merge replication [SQL Server replication]"
ms.assetid: ff87c368-4c00-4e48-809d-ea752839551e
author: "MashaMSFT"
ms.author: "mathoma"
---
# Merge Replication
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Merge replication, like transactional replication, typically starts with a snapshot of the publication database objects and data. Subsequent data changes and schema modifications made at the Publisher and Subscribers are tracked with triggers. The Subscriber synchronizes with the Publisher when connected to the network and exchanges all rows that have changed between the Publisher and Subscriber since the last time synchronization occurred.  
  
 Merge replication is typically used in server-to-client environments. Merge replication is appropriate in any of the following situations:  
  
-   Multiple Subscribers might update the same data at various times and propagate those changes to the Publisher and to other Subscribers.  
  
-   Subscribers need to receive data, make changes offline, and later synchronize changes with the Publisher and other Subscribers.  
  
-   Each Subscriber requires a different partition of data.  
  
-   Conflicts might occur and, when they do, you need the ability to detect and resolve them.  
  
-   The application requires net data change rather than access to intermediate data states. For example, if a row changes five times at a Subscriber before it synchronizes with a Publisher, the row will change only once at the Publisher to reflect the net data change (that is, the fifth value).  
  
 Merge replication allows various sites to work autonomously and later merge updates into a single, uniform result. Because updates are made at more than one node, the same data may have been updated by the Publisher and by more than one Subscriber. Therefore, conflicts can occur when updates are merged and merge replication provides a number of ways to handle conflicts.  
  
 Merge replication is implemented by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Snapshot Agent and Merge Agent. If the publication is unfiltered or uses static filters, the Snapshot Agent creates a single snapshot. If the publication uses parameterized filters, the Snapshot Agent creates a snapshot for each partition of data. The Merge Agent applies the initial snapshots to the Subscribers. It also merges incremental data changes that occurred at the Publisher or Subscribers after the initial snapshot was created, and detects and resolves any conflicts according to rules you configure.  
  
 To track changes, merge replication (and transactional replication with queued updating subscriptions) must be able to uniquely identify every row in every published table. To accomplish this merge replication adds the column **rowguid** to every table, unless the table already has a column of data type **uniqueidentifier** with the **ROWGUIDCOL** property set (in which case this column is used). If the table is dropped from the publication, the **rowguid** column is removed; if an existing column was used for tracking, the column is not removed. A filter must not include the **rowguidcol** used by replication to identify rows. The **newid()** function is provided as a default for the **rowguid** column, however customers can provide a guid for each row if needed. However, do not provide value 00000000-0000-0000-0000-000000000000.  
  
 The following diagram shows the components used in merge replication.  
  
 ![Merge replication components and data flow](../../../relational-databases/replication/merge/media/merge.gif "Merge replication components and data flow")  
  
  
