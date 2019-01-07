---
title: "Lesson 1: Publishing Data Using Transactional Replication | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: 9c55aa3c-4664-41fc-943f-e817c31aad5e
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Lesson 1: Publishing Data Using Transactional Replication
  In this lesson, you will create a transactional publication using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to publish a filtered subset of the **Product** table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. You will also add the SQL Server login used by the Distribution Agent to the publication access list (PAL). Before starting this tutorial, you should have completed the previous tutorial, [Preparing the Server for Replication](tutorial-preparing-the-server-for-replication.md).  
  
### To create a publication and define articles  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, right-click the **Local Publications** folder, and click **New Publication**.  
  
     The Publication Configuration Wizard launches.  
  
3.  On the Publication Database page, select [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)], and then click **Next**.  
  
4.  On the Publication Type page, select **Transactional publication**, and then click **Next**.  
  
5.  On the Articles page, expand the **Tables** node, select the **Product** check box, then expand **Product** and clear the **ListPrice** and **StandardCost** check boxes. Click **Next**.  
  
6.  On the Filter Table Rows page, click **Add**.  
  
7.  In the **Add Filter** dialog box, click the **SafetyStockLevel** column, click the right arrow to add the column to the Filter statement WHERE clause of the filter query, and modify the WHERE clause as follows:  
  
    ```  
    WHERE [SafetyStockLevel] < 500  
    ```  
  
8.  Click **OK**, and then click **Next**.  
  
9. Select the **Create a snapshot immediately and keep the snapshot available to initialize subscriptions** check box, and click **Next**.  
  
10. On the Agent Security page, clear **Use the security settings from the Snapshot Agent** check box.  
  
11. Click **Security Settings** for the Snapshot Agent, enter \<_Machine_Name>_**\repl_snapshot** in the **Process account** box, supply the password for this account, and then click **OK**.  
  
12. Repeat the previous step to set repl_logreader as the process account for the Log Reader Agent, and then click **Finish**.  
  
13. On the Complete the Wizard page, type **AdvWorksProductTrans** in the **Publication name** box, and click **Finish**.  
  
14. After the publication is created, click **Close** to complete the wizard.  
  
### To view the status of snapshot generation  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the **Local Publications** folder, right-click **AdvWorksProductTrans**, and then click **View Snapshot Agent Status**.  
  
3.  The current status of the Snapshot Agent job for the publication is displayed. Verify that the snapshot job has succeeded before you continue to the next lesson.  
  
### To add the Distribution Agent login to the PAL  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the **Local Publications** folder, right-click **AdvWorksProductTrans**, and then click **Properties**.  
  
     The **Publication Properties** dialog box is displayed.  
  
3.  Select the **Publication Access List** page, and click **Add**.  
  
4.  \In the **Add Publication Access** dialog box, select _<Machine_Name>_**\repl_distribution** and click **OK**. Click **OK**.  
  
## Next Steps  
 You have successfully created the transactional publication. Next, you will subscribe to this publication. See [Lesson 2: Creating a Subscription to the Transactional Publication](lesson-2-creating-a-subscription-to-the-transactional-publication.md).  
  
## See Also  
 [Filter Published Data](publish/filter-published-data.md)   
 [Define an Article](publish/define-an-article.md)   
 [Create and Apply the Snapshot](create-and-apply-the-snapshot.md)  
  
  
