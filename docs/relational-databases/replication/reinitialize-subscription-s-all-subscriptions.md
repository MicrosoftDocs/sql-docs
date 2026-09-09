---
title: Reinitialize Subscriptions - All Subscriptions
description: Reinitialize Subscriptions marks every subscription to a publication for a fresh snapshot. Learn how to use the dialog box options in SQL Server replication.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.reinit.all.f1"
helpviewer_keywords:
  - "Reinitialize Subscription(s) dialog box"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Reinitialize Subscription(s) - All Subscriptions
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
The **Reinitialize Subscription(s)** dialog box allows you to mark all subscriptions to a publication for reinitialization. Reinitialization involves applying a snapshot to each Subscriber. The Distribution Agent performs reinitialization for subscriptions to transactional publications, and the Merge Agent performs reinitialization for subscriptions to merge publications.    
  
## Options  
 **Use the current snapshot**  
 Select to apply the current snapshot to each Subscriber the next time the Distribution Agent or Merge Agent runs for the subscription. If there's no valid snapshot available, you can't select this option.  
  
 **Use a new snapshot**  
 Select to reinitialize all subscriptions with a new snapshot. The Snapshot Agent generates the snapshot. You can apply the snapshot to each Subscriber only after the Snapshot Agent generates it. If the Snapshot Agent is set to run on a schedule, subscriptions aren't reinitialized until after the next scheduled Snapshot Agent run.  
  
 Select **Generate the new snapshot now** to start the Snapshot Agent immediately.  
  
 **Upload unsynchronized changes before reinitialization**  
 Merge replication only. Select to upload any pending changes from the subscription databases before the data at the Subscribers is overwritten with a snapshot.  
  
 If you add, drop, or change a parameterized filter, the reinitialization process can't upload pending changes at the Subscriber to the Publisher. To upload pending changes, synchronize all subscriptions before changing the filter.  
  
 **Mark for reinitialization**  
 Select to mark each subscription for reinitialization. After a valid snapshot is available, the next time the Distribution Agent or Merge Agent runs for the subscription, the snapshot is applied at the Subscriber.  
  
## Related content

- [Reinitialize Subscriptions](reinitialize-subscriptions.md)
