---
title: "Web Server Information | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.newsubwizard.webserverinformation.f1"
ms.assetid: 86d72275-45c7-459f-98cf-f5a366ed279c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Web Server Information
  Web server information is required to use the Web synchronization option for merge replication. For information about configuring Web synchronization, see [Configure Web Synchronization](configure-web-synchronization.md).  
  
## Options  
 **Web server address**  
 If you specified a Web server address in the **FTP Snapshot andInternet** page of the **Publication Properties** dialog box, it appears in this text box as a default. Either accept the default or enter a fully qualified Web server address for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Information Services (IIS) server that synchronizes this subscription.  
  
 **How will each Subscriber connect to the Web server?**  
 Specify the type of authentication used to connect to the Web server. It is recommended to use Basic Authentication for connections to the IIS server in conjunction with Secure Sockets Layer (SSL). If you select Basic Authentication, enter the login and password that will be used to connect from the Subscriber to the IIS server.  
  
## See Also  
 [Create a Pull Subscription](create-a-pull-subscription.md)   
 [View and Modify Pull Subscription Properties](view-and-modify-pull-subscription-properties.md)   
 [Non-SQL Server Subscribers](non-sql/non-sql-server-subscribers.md)   
 [Subscribe to Publications](subscribe-to-publications.md)   
 [Web Synchronization for Merge Replication](web-synchronization-for-merge-replication.md)  
  
  
