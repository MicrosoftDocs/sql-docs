---
title: "SQL Server Replication Publisher Properties dialog box | Microsoft Docs"
ms.custom: ""
ms.date: "11/20/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.configdistwizard.distpubproperties.f1"
  - "sql13.rep.configdistwizard.pubproperties.general.f1"
  - "sql13.rep.configdistwizard.pubproperties.pubdb.f1"
  - "sql13.rep.configdistwizard.pubproperties.subscribers.f1"
ms.assetid: 98df1aea-0406-40bf-a917-4bd80464125c
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# SQL Server Replication Publisher Properties dialog box
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This topic describes the different options found within the Publisher properties dialog box. 

## General
  The **General** page of the **Publisher Properties** dialog box displays read-only information on the Distributor and distribution database that the Publisher uses. To change the Distributor or distribution database for a Publisher:  
  
1.  Disable publishing on the Publisher. For more information, see [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md).    
2.  Reconfigure publishing and distribution. For more information, see [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md).  
  
## Distributor 
The **Publisher Properties** dialog box allows you to view and modify properties associated with the relationship between the Publisher and its Distributor.  
  
### Options  
 **Agent Connection to the Publisher**  
 Specify the context under which the following agents make connections from the Distributor to the Publisher:  
  
-   The Queue Reader Agent for transactional publications that allow queued updating subscriptions.    
-   The Snapshot Agent and Log Reader Agent for Oracle publications.  
  
 Select **Impersonate agent process account** to make connections to the Publisher using the context of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account under which these agents run, or specify **SQL Server Authentication**, and then enter a value for **Login** and **Password**. It is recommended that you select **Impersonate agent process account**. For more information on agent security, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
 The Windows accounts under which these agents run are specified in the New Publication Wizard. These accounts can be changed:  
  
-   In the **Distributor Properties** dialog box for the Queue Reader Agent.    
-   In the **Publication Properties** dialog box for the Snapshot Agent and Log Reader Agent.  
  
 **Miscellaneous**  
 The properties **Publisher Type** and **Distribution Database Name** are read-only. The property **Default Snapshot Folder** can be changed. For more information about the snapshot folder, see [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md).  

## Publication Databases
  The **Publication Databases** page of the **Publisher Properties** dialog box allows a user in the **sysadmin** fixed server role to enable databases for replication. Enabling a database does not publish that database; rather, it allows any user in the **db_owner** fixed database role for that database to create one or more publications on the database.  
  
## Options  
 **Transactional**  
 Select this check box to allow users in the **db_owner** fixed database role to create snapshot publications or transactional publications in the database. 
  
 **Merge**  
 Select this check box to allow users in the **db_owner** fixed database role to create merge publications in the database.  
  

## Subcribers
  The **Subscribers** page of the **Publisher Properties** dialog box is used for Publishers running versions of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. The page allows you to enable Subscribers to receive data from publications on this Publisher. Enabling a Subscriber to receive data from this Publisher does not create subscriptions to publications on this Publisher. To create a subscription, you must use the New Subscription Wizard.  
  
### Options  
 **Subscribers**  
 The **Subscribers** property grid shows Subscribers that are enabled to receive data from publications on this Publisher. Click the properties button (**...**) next to a Subscriber to view and set additional properties.  
  
 **Add**  
 Click **Add** to add a Subscriber, and then click **Add SQL Server Subscriber** or **Add Non-SQL Server Subscriber**.  
  
## See Also  
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   


  
