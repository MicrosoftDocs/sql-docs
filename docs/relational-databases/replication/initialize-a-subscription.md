---
title: "Initialize a Subscription"
description: "Initialize a Subscription"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "snapshot replication [SQL Server], initializing subscriptions"
  - "transactional replication, initializing subscriptions"
  - "initializing subscriptions [SQL Server replication]"
  - "subscriptions [SQL Server replication], initializing"
  - "initializing subscriptions [SQL Server replication], about initializing subscriptions"
  - "merge replication [SQL Server replication], initializing subscriptions"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Initialize a Subscription
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Subscribers in a replication topology must be initialized, so that they have a copy of the schema from each article in the publication they have subscribed to and any replication objects that are required, such as stored procedures, triggers, and metadata tables. In addition, the Subscriber typically receives an initial dataset. The default initialization method uses a full snapshot that includes schema, replication objects, and data, but publications can also be initialized without a full snapshot.  
  
 For more information, see [Initialize a Subscription with a Snapshot](../../relational-databases/replication/initialize-a-subscription-with-a-snapshot.md) and [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
  
