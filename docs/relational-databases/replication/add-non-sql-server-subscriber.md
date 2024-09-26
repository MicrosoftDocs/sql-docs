---
title: "Add Non-SQL Server Subscriber"
description: "Add Non-SQL Server Subscriber"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.newsubwizard.addnonsqlsubscriber.f1"
---
# Add Non-SQL Server Subscriber
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Replication supports creating push subscriptions to snapshot and transactional publications for Oracle and IBM Db2 Subscribers.  
  
## Options  
 **Type of Subscriber to add**  
 Select an Oracle Subscriber or IBM Db2 Subscriber. For more information about support for these Subscribers, see [Non-SQL Server Subscribers](../../relational-databases/replication/non-sql/non-sql-server-subscribers.md).  
  
 **Data source name**  
 The name used to locate the database on a network. Replication produces a connection string for the database using the data source name, combined with the login, password, and any connection options you specify in the **Distribution Agent Security** page in this wizard.  
  
> [!NOTE]
>  The data source name and connection string are not validated by [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] until the Distribution Agent attempts to initialize the subscription.  
  
## Related content

- [Create a Subscription for a Non-SQL Server Subscriber](../../relational-databases/replication/create-a-subscription-for-a-non-sql-server-subscriber.md)
- [Non-SQL Server Subscribers](../../relational-databases/replication/non-sql/non-sql-server-subscribers.md)
- [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)
