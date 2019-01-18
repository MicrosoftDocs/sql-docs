---
title: "Scripting Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "scripts [SQL Server replication], replication objects"
  - "merge replication scripting [SQL Server replication]"
  - "replication [SQL Server], scripting"
  - "snapshot replication [SQL Server], scripting"
  - "scripts [SQL Server replication]"
  - "transactional replication, scripting"
ms.assetid: e50fac44-54c0-470c-a4ea-9c111fa4322b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Scripting Replication
  All replication components in a topology should be scripted as part of a disaster recovery plan, and scripts can also be used to automate repetitive tasks. A script contains the Transact-SQL system stored procedures necessary to implement the replication component(s) scripted, such as a publication or subscription. Scripts can be created in a wizard (such as the New Publication Wizard) or in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] after you create a component. You can view, modify, and run the script using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or **sqlcmd**. Scripts can be stored with backup files to be used in case a replication topology must be reconfigured.  
  
 A component should be re-scripted if any property changes are made. If you use custom stored procedures with transactional replication, a copy of each procedure should be stored with the scripts; the copy should be updated if the procedure changes (procedures are typically updated due to schema changes or changing application requirements). For more information about custom procedures, see [Specify How Changes Are Propagated for Transactional Articles](transactional/transactional-articles-specify-how-changes-are-propagated.md).  
  
 For merge publications that use parameterized filters, publication scripts contain the stored procedure calls to create data partitions. The script provides a reference for the partitions created and a way in which to re-create one or more partitions if necessary.  
  
## Example of Automating a Task with Scripts  
 Consider [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)], which implements merge replication to distribute data to its remote sales force. A sales representative downloads all the data that pertains to the customers in her territory using pull subscriptions. When working offline, the sales representative updates data and enters new customers and orders. Because [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] has more than fifty sales representatives in different territories, it would be time-consuming to create the different subscriptions at each Subscriber with the New Subscription Wizard. Instead, the replication administrator can follow these steps:  
  
1.  Set up the necessary merge publications with partitions based on the sales representative or their territory.  
  
2.  Create a pull subscription for one Subscriber.  
  
3.  Generate a script based on that pull subscription.  
  
4.  Modify the script, changing such values as the name of the Subscriber.  
  
5.  Run the script at multiple Subscribers to generate the required pull subscriptions.  
  
## Script Replication Objects  
 Script replication objects from the replication wizards or from the **Replication** folder in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. If you script from the wizards, you can choose to create objects and script them, or you can choose only to script them.  
  
> [!IMPORTANT]  
>  All passwords are scripted as NULL. When possible, prompt users to enter security credentials at runtime. If you store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
 For more information about using the replication wizards, see:  
  
-   [Configure Publishing and Distribution](configure-publishing-and-distribution.md)  
  
-   [Create a Publication](publish/create-a-publication.md)  
  
-   [Create a Push Subscription](create-a-push-subscription.md)  
  
-   [Create a Pull Subscription](create-a-pull-subscription.md)  
  
#### To script an object from a replication wizard  
  
1.  On the **Wizard Actions** page of a wizard, select the check box appropriate for the wizard:  
  
    -   **Generate a script file with steps to create a publication**  
  
    -   **Generate a script file with steps to create the subscription(s)**  
  
    -   **Generate a script file with steps to configure distribution**  
  
2.  Specify options on the **Script File Properties** page.  
  
3.  Complete the wizard.  
  
#### To script an object from Management Studio  
  
1.  Connect to the Distributor, Publisher, or Subscriber in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Expand the **Replication** folder, and then expand the **Local Publications** folder or the **Local Subscriptions** folder.  
  
3.  Right-click a publication or subscription, and then click **Generate Scripts**.  
  
4.  Specify options in the **Generate SQL Script - \<ReplicationObject>** dialog box.  
  
5.  Click **Script to File**.  
  
6.  Enter a file name in the **Script File Location** dialog box, and then click **Save**. A status message is displayed.  
  
7.  Click **OK**, and then click **Close**.  
  
#### To script multiple objects from Management Studio  
  
1.  Connect to the Distributor, Publisher, or Subscriber in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
2.  Right-click the **Replication** folder, and then click **Generate Scripts**.  
  
3.  Specify options in the **Generate SQL Script** dialog box.  
  
4.  Click **Script to File**.  
  
5.  Enter a file name in the **Script File Location** dialog box, and then click **Save**. A status message is displayed.  
  
6.  Click **OK, and then** click **Close**.  
  
  
