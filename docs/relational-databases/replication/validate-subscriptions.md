---
title: "Validate Subscriptions"
description: "Validate Subscriptions"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.validate.subscriptions.f1"
helpviewer_keywords:
  - "Validate Subscriptions dialog box"
monikerRange: "=azuresqldb-current||>=sql-server-2016"
---
# Validate Subscriptions
[!INCLUDE[sql-asdb](../../includes/applies-to-version/sql-asdb.md)]
  Use the **Validate Subscriptions** dialog box to specify that subscriptions to a transactional publication should be validated the next time the Distribution Agent for each subscription runs. The results of validation are displayed in Replication Monitor. For more information, see [Validate Data at the Subscriber](../../relational-databases/replication/validate-data-at-the-subscriber.md).  

[!INCLUDE[azure-sql-db-replication-supportability-note](../../includes/azure-sql-db-replication-supportability-note.md)]
  
## Options  
 **Validate all SQL Server subscriptions**  
 Select to validate data for all SQL Server subscriptions to this publication.  
  
 **Validate the following subscriptions**  
 Select if you do not want to validate all subscriptions. Select from the list the subscriptions you want to validate.  
  
 **Validation Options**  
 Click to access the **Subscription Validation Options** dialog box, which allows you to specify whether to use row count validation or binary checksum validation.  
  
## Related content

- [Validate Replicated Data](../../relational-databases/replication/validate-data-at-the-subscriber.md)
