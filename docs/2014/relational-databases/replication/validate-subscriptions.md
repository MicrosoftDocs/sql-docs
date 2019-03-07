---
title: "Validate Subscriptions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.validate.subscriptions.f1"
helpviewer_keywords: 
  - "Validate Subscriptions dialog box"
ms.assetid: 0ca39a35-f22c-46c5-82a4-342e34bf5d1b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Validate Subscriptions
  Use the **Validate Subscriptions** dialog box to specify that subscriptions to a transactional publication should be validated the next time the Distribution Agent for each subscription runs. The results of validation are displayed in Replication Monitor. For more information, see [Validate Data at the Subscriber](validate-data-at-the-subscriber.md).  
  
## Options  
 **Validate all SQL Server subscriptions**  
 Select to validate data for all SQL Server subscriptions to this publication.  
  
 **Validate the following subscriptions**  
 Select if you do not want to validate all subscriptions. Select from the list the subscriptions you want to validate.  
  
 **Validation Options**  
 Click to access the **Subscription Validation Options** dialog box, which allows you to specify whether to use row count validation or binary checksum validation.  
  
## See Also  
 [Validate Replicated Data](validate-data-at-the-subscriber.md)  
  
  
