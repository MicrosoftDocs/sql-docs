---
title: "Validate All Subscriptions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.validate.allsubscriptions.f1"
helpviewer_keywords: 
  - "Validate All Subscriptions dialog box"
ms.assetid: 32e31469-36e4-42d9-a57a-12388bfd229d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Validate All Subscriptions
  Use the **Validate All Subscriptions** dialog box to specify that all subscriptions to a merge publication should be validated the next time the Merge Agent for each subscription runs. The results of validation are displayed in Replication Monitor. For more information, see [Validate Data at the Subscriber](validate-data-at-the-subscriber.md).  
  
 It is also possible to validate a single subscription by right-clicking a subscription in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and clicking **Validate Subscription**.  
  
## Options  
 **Verify the row counts only**  
 Select to validate whether the table at the Subscriber has the same number of rows as the table at the Publisher. This method does not validate that the content of the rows matches. Row count validation provides a lightweight approach to validation that can make you aware of issues with your data.  
  
 **Verify the row counts and compare checksums to verify the row data**  
 In addition to taking a count of rows at the Publisher and Subscriber, a checksum of all the data is calculated using the binary checksum algorithm. If the row count fails, the checksum is not performed. This option is not valid for [!INCLUDE[ssEW](../../includes/ssew-md.md)].  
  
## See Also  
 [Validate Replicated Data](validate-data-at-the-subscriber.md)  
  
  
