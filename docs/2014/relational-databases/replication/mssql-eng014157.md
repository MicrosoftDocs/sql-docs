---
title: "MSSQL_ENG014157 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG014157 error"
ms.assetid: 1a0890cf-d977-43e0-a2ba-9c5ff1a8f856
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG014157
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14157|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The subscription created by Subscriber '%s' to publication '%s' has expired and has been dropped.|  
  
## Explanation  
 A Subscriber must synchronize with the Publisher within the time specified in the publication retention period. If a Subscriber does not synchronize within this period, the subscription expires. For more information, see [Subscription Expiration and Deactivation](subscription-expiration-and-deactivation.md).  
  
## User Action  
 The subscription must be re-created and initialized before the Subscriber can begin receiving data changes again:  
  
-   For information about creating subscriptions, see [Subscribe to Publications](subscribe-to-publications.md).  
  
-   For information about initializing subscriptions, see [Initialize a Subscription](initialize-a-subscription.md).  
  
 If the subscription database contains changes that have not been synchronized with the Publisher, you can use the [tablediff Utility](../../tools/tablediff-utility.md) to determine which rows are different in the publication and subscription databases.  
  
 You can increase the publication retention period to avoid having subscriptions expire. Use caution in setting a high value, because this can result in more data and metadata being stored, which affects performance. For more information, see [Subscription Expiration and Deactivation](subscription-expiration-and-deactivation.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)  
  
  
