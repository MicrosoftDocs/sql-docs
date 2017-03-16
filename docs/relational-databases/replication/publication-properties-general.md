---
title: "Publication Properties, General | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.rep.newpubwizard.pubproperties.general.f1"
ms.assetid: 7912362f-c4d6-4f60-bd39-dee1f656ed18
caps.latest.revision: 25
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Publication Properties, General
  The **General** page of the **Publication Properties** dialog box contains basic information on the publication, including name, description, and the subscription expiration policy.  
  
## Options  
 **Name**  
 The name of the publication (read-only).  
  
 **Database**  
 The name of the publication database (read-only).  
  
 **Description**  
 The description of the publication.  
  
 **Type**  
 The type of publication (read-only).  
  
 **Subscription expiration**  
 Select one of the options for subscription expiration: **Subscriptions never expire** or **Subscriptions expire**, with an explicit time period (**Interval**).  
  
 For snapshot and transactional publications, [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you accept the default of **Subscriptions never expire**.  
  
 For merge replication, [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you accept the default of **Subscriptions expire** and set as low a value as possible for **Interval**. As the subscription expiration period increases, so does the amount of metadata stored, which can affect performance. Balance the need for Subscribers to be disconnected or simply not to synchronize for an extended period against the potential performance issues of storing and processing a large amount of metadata.  
  
 For more information, see [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
 **Compatibility level**  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only; merge publications only. Select the minimum [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version required for Subscribers that synchronize with this publication. There are a number of rules associated with determining the compatibility level.  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  