---
title: "Publication Properties, Agent Security | Microsoft Docs"
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
  - "sql13.rep.newpubwizard.pubproperties.agentsecurity.f1"
ms.assetid: 03945aac-66f2-4370-b5d1-c1de694bc4c1
caps.latest.revision: 21
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Publication Properties, Agent Security
  The **Agent Security** page of the **Publication Properties** dialog box allows you to access the settings for the accounts under which the following agents run and make connections to the computers in a replication topology:  
  
-   The Snapshot Agent for all publications.  
  
-   The Log Reader Agent for all transactional publications. There is one Log Reader Agent for each database published for transactional replication. Changing the Log Reader Agent settings affects all transactional publications in a database.  
  
-   The Queue Reader Agent for transactional publications that allow queued updating subscriptions. There is one Queue Reader Agent for each distribution database. Changing the Queue Reader Agent settings affects all transactional publications with queued updating subscriptions that use the same distribution database. For more information about Queue Reader Agent security settings, see [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
 For more information about security settings and the permissions required by each agent, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
## Options  
 **Security settings** or **Create Agent**  
 If an agent job has been created, click **Security settings** to access a dialog box that allows you to change the security settings for an agent. If an agent job has not been created, click **Create Agent** to create one and specify security settings.  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md)   
 [Security and Protection &#40;Replication&#41;](../../relational-databases/replication/security/security-and-protection-replication.md)  
  
  