---
title: "Web Server Information"
description: "Web Server Information"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rep.newsubwizard.webserverinformation.f1"
---
# Web Server Information
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Web server information is required to use the Web synchronization option for merge replication. For information about configuring Web synchronization, see [Configure Web Synchronization](../../relational-databases/replication/configure-web-synchronization.md).  
  
## Options  
 **Web server address**  
 If you specified a Web server address in the **FTP Snapshot andInternet** page of the **Publication Properties** dialog box, it appears in this text box as a default. Either accept the default or enter a fully qualified Web server address for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Information Services (IIS) server that synchronizes this subscription.  
  
 **How will each Subscriber connect to the Web server?**  
 Specify the type of authentication used to connect to the Web server. It is recommended to use Basic Authentication for connections to the IIS server in conjunction with Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL). If you select Basic Authentication, enter the login and password that will be used to connect from the Subscriber to the IIS server.  
  
## See Also  
 [Create a Pull Subscription](../../relational-databases/replication/create-a-pull-subscription.md)   
 [View and Modify Pull Subscription Properties](../../relational-databases/replication/view-and-modify-pull-subscription-properties.md)   
 [Non-SQL Server Subscribers](../../relational-databases/replication/non-sql/non-sql-server-subscribers.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)   
 [Web Synchronization for Merge Replication](../../relational-databases/replication/web-synchronization-for-merge-replication.md)  
  
  
