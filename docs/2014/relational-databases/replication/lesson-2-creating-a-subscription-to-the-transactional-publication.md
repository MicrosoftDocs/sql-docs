---
title: "Lesson 2: Creating a Subscription to the Transactional Publication | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: 5995b7d2-7c06-46f5-b96c-2bee879bcda2
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Lesson 2: Creating a Subscription to the Transactional Publication
  In this lesson, you will create a subscription using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. This lesson requires that you have completed the previous lesson, [Lesson 1: Publishing Data Using Transactional Replication](lesson-1-publishing-data-using-transactional-replication.md).  
  
### To create the subscription  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the **Local Publications** folder, right-click the **AdvWorksProductTrans** publication, and then click **New Subscriptions**.  
  
     The New Subscription Wizard launches.  
  
3.  On the Publication page, select **AdvWorksProductTrans**, and then click **Next**.  
  
4.  On the Distribution Agent Location page, select **Run all agents at the Distributor**, and then click **Next**.  
  
5.  On the Subscribers page, if the name of the Subscriber instance is not displayed, click **Add Subscriber**, click **Add SQL Server Subscriber**, enter the Subscriber instance name in the **Connect to Server** dialog box, and then click **Connect**.  
  
6.  On the Subscribers page, select the instance name of the Subscriber server, and select **\<New Database>** under **Subscription Database**.  
  
7.  On the **New Database** dialog box, enter **ProductReplica** in the **Database name** box, click **OK**, and then click **Next**.  
  
8.  In the **Distribution Agent Security** dialog box, click the ellipsis (**...**) button, enter \<_Machine_Name>_**\repl_distribution** in the **Process account** box, enter the password for this account, click **OK**, and then click **Next**.  
  
9. Click **Finish** to accept the default values on the remaining pages and complete the wizard.  
  
### Setting database permissions at the Subscriber  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand **Databases**, **ProductReplica**, and **Security**, right-click **Users**, and then select **New User**.  
  
2.  On the **General** page, in the **User type** list, select **Windows user**.  
  
3.  Select the **User name** box and click the ellipsis (...) button, in the **Enter the object name to select** box type <Machine_Name>**\repl_distribution**, click **Check Names**, and then click **OK**.  
  
4.  On the **Membership** page, in **Database role membership** area, select **db_owner**, and then click **OK** to create the user.  
  
### To view the synchronization status of the subscription  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the **Local Publications** folder, expand the **AdvWorksProductTrans** publication, right-click the subscription in the **ProductReplica** database, and then click **View Synchronization Status**.  
  
     The current synchronization status of the subscription is displayed.  
  
3.  If the subscription is not visible under **AdvWorksProductTrans**, press F5 to refresh the list.  
  
## Next Steps  
 You have successfully created a subscription to the transactional publication. Because the Distribution Agent for this subscription runs continuously, the subscription is initialized when it is created. Next, you will use tracer tokens to verify that changes are being replicated to the Subscriber and to determine latency. See [Lesson 3: Validating the Subscription and Measuring Latency](lesson-3-validating-the-subscription-and-measuring-latency.md).  
  
## See Also  
 [Initialize a Subscription with a Snapshot](initialize-a-subscription-with-a-snapshot.md)   
 [Create a Push Subscription](create-a-push-subscription.md)   
 [Subscribe to Publications](subscribe-to-publications.md)  
  
  
