---
title: "Subscription Properties - Publisher | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.rep.newsubwizard.subproperties.publisher.f1"
helpviewer_keywords: 
  - "Subscription Properties dialog box"
ms.assetid: d4b2bc8b-0431-4331-8305-8992c96d0d34
caps.latest.revision: 22
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Subscription Properties - Publisher
  The **Subscription Properties** dialog box at the Publisher allows you to view and set properties for push subscriptions. You can also view some properties for pull subscriptions, but the **Subscriptions Properties** dialog box at the Subscriber displays additional properties and allows properties to be modified.  
  
 Each property in the **Subscription Properties** dialog box includes a description. Click a property to see its description displayed at the bottom of the dialog box. This topic provides additional information on a number of properties, most of which are displayed at the Publisher only for push subscriptions. The properties are grouped into the following categories:  
  
-   Properties that apply to all subscriptions.  
  
-   Properties that apply to transactional subscriptions.  
  
-   Properties that apply to merge subscriptions.  
  
 If an option is displayed as read-only, it can only be set when the subscription is created. If you want to set options that are not available in the New Subscription Wizard, create the subscription with stored procedures. For more information, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md) and [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md).  
  
## Options for all subscriptions  
 **Security**  
 Click the **Agent process account** row, and then click the properties button (**...**) to change the account under which the Distribution Agent or Merge Agent runs at the Distributor. To change the account under which the Distribution Agent or Merge Agent makes connections to the Subscriber, click **Subscriber connection**, and then click the properties button (**...**).  
  
 For more information about the permissions required for each agent, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
## Options for transactional subscriptions  
 **Prevent transaction looping**  
 Determines whether the Distribution Agent sends transactions that originated at the Subscriber back to the Subscriber. This option is used for bidirectional transactional replication. For more information, see [Bidirectional Transactional Replication](../../relational-databases/replication/transactional/bidirectional-transactional-replication.md).  
  
 **Updatable subscription**  
 Determines whether Subscriber changes are replicated back to the Publisher. Changes can be replicated using queued updating or immediate updating. The option **Subscriber update method** determines which method to use. For more information, see [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md).  
  
## Options for merge subscriptions  
 **Partition definition (HOST_NAME)**  
 For a publication that uses parameterized filters, merge replication evaluates one of two system functions (or both if the filter references both functions) during synchronization to determine the data that a Subscriber should receive: **SUSER_SNAME()** or **HOST_NAME()**. By default, **HOST_NAME()** returns the name of the computer on which the Merge Agent is running, but you can override this value in the New Subscription Wizard. For more information on parameterized filters and overriding **HOST_NAME()**, see [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
 **Subscription type** and **Priority**  
 Displays whether the subscription is a client or server subscription (this cannot be changed after the subscription has been created). Server subscriptions can republish data to other Subscribers and can be assigned a priority for conflict resolution.  
  
 If you selected a subscription type of server in the New Subscription Wizard, the Subscriber is given a priority that is used during conflict resolution.  
  
 **Resolve conflicts interactively**  
 Determines whether to use the Interactive Resolver user interface to resolve conflicts during merge synchronization. This requires a value of **Enable** for **Use Windows Synchronization Manager**. For more information, see [Interactive Conflict Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-interactive-resolution.md).  
  
## See Also  
 [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md)   
 [View and Modify Push Subscription Properties](../../relational-databases/replication/view-and-modify-push-subscription-properties.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  