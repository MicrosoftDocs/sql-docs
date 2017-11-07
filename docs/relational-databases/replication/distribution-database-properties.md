---
title: "Distribution Database Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.rep.configdistwizard.distdbproperties.f1"
helpviewer_keywords: 
  - "Distribution Database Properties dialog box"
ms.assetid: 0f404ab9-1237-4936-8df5-888baab6a245
caps.latest.revision: 23
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Distribution Database Properties
  The **Distribution Database Properties** dialog box allows you to view a number of properties and to set the transaction retention period and history retention period for the database.  
  
## Options  
 **Name**  
 The name of the distribution database, which defaults to 'distribution' (read-only).  
  
 **File locations**  
 The location of the database file and the log file (read-only).  
  
 **Transaction retention period**  
 Also known as the distribution retention period. The length of time transactions are stored for transactional replication. For more information, see [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
 **History retention period**  
 The length of time history metadata is stored for all types of replication.  
  
 **Queue Reader Agent security**  
 The Queue Reader Agent is used by transactional replication with queued updating subscriptions. A Queue Reader Agent is created automatically if you select **Transactional publication with updating subscriptions** on the **Publication Type** page of the New Publication Wizard. Click **Security Settings…** to change the account under which the agent runs and makes connections to the Distributor.  
  
 A Queue Reader Agent can also be created by selecting **Create Queue Reader Agent** on this page (this option is disabled if the agent has already been created).  
  
 Additional connection information for the Queue Reader Agent is specified in two places:  
  
-   The agent connects to the Publisher using the credentials specified in the **Publisher Properties** dialog box, which is available from the **Publishers** page of the **Distributor Properties** dialog box.  
  
-   The agent connects to the Subscriber using the credentials specified for the Distribution Agent in the New Subscription Wizard.  
  
 For more information, see  [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
## See Also  
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)   
 [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md)   
 [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md)   
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)  
  
  