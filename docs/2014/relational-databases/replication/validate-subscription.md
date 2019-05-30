---
title: "Validate Subscription | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.validate.validateandresynch.f1"
helpviewer_keywords: 
  - "Validate Subscription dialog box"
ms.assetid: 74bdf5e1-b886-4284-b5fb-332bf79ae083
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Validate Subscription
  Use the **Validate Subscription** dialog box to specify that a subscription to a merge publication should be validated the next time the Merge Agent for the subscription runs. The results of validation are displayed in Replication Monitor. For more information, see [Validate Data at the Subscriber](validate-data-at-the-subscriber.md).  
  
 It is also possible to validate all subscriptions to a merge publication by right-clicking a publication in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and clicking **Validate All Subscriptions**.  
  
## Options  
 **Date of the last attempted validation**  
 The date of the last Merge Agent session that included subscription validation, whether or not that validation was successful.  
  
 **Date of the last successful validation**  
 The date of the last Merge Agent session that included a successful subscription validation.  
  
 **Validate this subscription**  
 Select to validate the subscription.  
  
 **Options**  
 Click to access the **Subscription Validation Options** dialog box, which allows you to specify whether to use row count validation or binary checksum validation.  
  
## See Also  
 [Validate Replicated Data](validate-data-at-the-subscriber.md)  
  
  
