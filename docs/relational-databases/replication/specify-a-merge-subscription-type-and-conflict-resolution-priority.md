---
title: "Specify type & conflict resolution priority (Merge)"
description: Learn how to specify the type and conflict resolution policy used by a Merge Subscription for SQL Server. 
ms.custom: seo-lt-2019
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "merge replication conflict resolution [SQL Server replication], merge subscription resolvers"
  - "conflict resolution [SQL Server replication], merge replication"
ms.assetid: 2b828d83-2341-4924-b92a-4f81a22246c0
author: "MashaMSFT"
ms.author: "mathoma"
---
# Specify a Merge Subscription Type and Conflict Resolution Priority
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Specify a merge subscription type and conflict resolution priority on the **Subscription Type** page of the New Subscription Wizard. For more information about using this wizard, see [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md) and [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md).  
  
 Subscription type cannot be modified after a subscription is created, but priority can be modified for the server subscription type in the **Subscription Properties - \<Publisher>: \<PublicationDatabase>** dialog box. For more information about accessing this dialog box, see [View and Modify Push Subscription Properties](../../relational-databases/replication/view-and-modify-push-subscription-properties.md) and [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md).  
  
### To specify a merge subscription type and conflict resolution priority  
  
1.  On the **Subscription Type** page of the New Subscription Wizard, select **Client** or **Server** for the **Subscription Type** option.  
  
2.  If you select a subscription type of **Server**, also enter a value (0.00 to 99.99) for the **Priority for Conflict Resolution** option.  
  
### To modify the conflict resolution priority  
  
1.  In the **Subscription Properties - \<Publisher>: \<PublicationDatabase>** at the Publisher, enter a value (0.00 to 99.99) for the **Priority** option.  
  
2.  Select **OK**.
  
## See Also  
 [Advanced Merge Replication Conflict Detection and Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  
