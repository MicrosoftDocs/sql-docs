---
title: "Distributor Properties dialog box"
description: Describe the different pages within the 'Distributor Properties' dialog box in SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "11/20/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.configdistwizard.distdbproperties.f1"
  - "sql13.rep.configdistwizard.distproperties.general.f1"
  - "sql13.rep.configdistwizard.distproperties.publishers.f1"
ms.assetid: f643c7c3-f238-4835-b81e-2c2b3b53b23f
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# SQL Server Replication Distributor Properties dialog box 
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This page describes the pages found within the Distributor properties dialog box. 

## General
The **General** page of the **Distributor Properties** dialog box allows you to add and delete distribution databases and set distribution database properties.  
  
 The distribution database stores metadata and history data for all types of replication, and transactions for transactional replication. In many cases, a single distribution database is sufficient. But if multiple Publishers use a single Distributor, consider creating a distribution database for each Publisher. Doing so ensures that the data flowing through each distribution database is distinct.  

 **Databases**  
 The **Databases** property grid shows the name and retention properties of the distribution databases on the Distributor. **Transaction Retention** is the length of time transactions are stored for transactional replication (transaction retention is also known as distribution retention). **History Retention** is the length of time history metadata is stored for all types of replication. For more information about distribution retention, see [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
 Click the properties button (**...**) in the **Databases** property grid to launch the **Distribution Database Properties** dialog box.  
  
 **New**  
 Click to create a new distribution database.  
  
 **Delete**  
 Select an existing distribution database in the **Databases** property grid, and click **Delete** to delete the database. You cannot delete the distribution database if there is only one such database; each Distributor must have at least one distribution database. To delete all distribution databases, you must disable distribution on the computer. For more information, see [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md).  
  
 **Profile Defaults**  
 Click to access replication agent profiles in the **Agent Profiles** dialog box. For more information about profiles, see [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md).  

## Publishers
The **Publishers** page of the **Distributor Properties** dialog box allows you to enable Publishers to use this Distributor. You can also set properties associated with those Publishers. Be aware that enabling a Publisher to use this server as its remote Distributor does not make that server a Publisher. You must connect to the Publisher, configure it for publishing, and choose this server as the Distributor. You can configure the Publisher and choose a Distributor through the New Publication Wizard.  
  
 **Publishers**  
 Select the servers that are allowed to use this Distributor. Click the Properties button **(...)** next to a Publisher to view and set additional properties.  
  
 **Add**  
 If the server you want to allow is not listed, click **Add** to add a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher or Oracle Publisher to the list of available Publishers. If the server you add is the first server to use this Distributor as a remote Distributor, you are prompted to provide an administrative link password.  
  
 **Administrative link password**  
 Use to specify or update the password for the connection replication makes between the Publisher and the remote Distributor using the **distributor_admin** login:  
  
-   If the Distributor serves only as a local Distributor, this password is randomly generated and automatically configured.   
-   If the Distributor already has a remote Publisher, a password was initially supplied on this page or the **Distributor Password** page of the Configure Distribution Wizard.    
-   If you enable the first remote Publisher for this Distributor, you are prompted to enter a password.  For more information on security for Distributors, see [Secure the Distributor](../../relational-databases/replication/security/secure-the-distributor.md).  

## Distribution Database 
 The **Distribution Database Properties** dialog box allows you to view a number of properties and to set the transaction retention period and history retention period for the database.  
  
 **Name**  
 The name of the distribution database, which defaults to 'distribution' (read-only).  
  
 **File locations**  
 The location of the database file and the log file (read-only).  
  
 **Transaction retention period**  
 Also known as the distribution retention period. The length of time transactions are stored for transactional replication. For more information, see [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
 **History retention period**  
 The length of time history metadata is stored for all types of replication.  
  
 **Queue Reader Agent security**  
 The Queue Reader Agent is used by transactional replication with queued updating subscriptions. A Queue Reader Agent is created automatically if you select **Transactional publication with updating subscriptions** on the **Publication Type** page of the New Publication Wizard. Click **Security Settings…** to change the account under which the agent runs and makes connections to the Distributor.  
  
 A Queue Reader Agent can also be created by selecting **Create Queue Reader Agent** on this page (this option is disabled if the agent has already been created).  
  
 Additional connection information for the Queue Reader Agent is specified in two places:    
-   The agent connects to the Publisher using the credentials specified in the **Publisher Properties** dialog box, which is available from the **Publishers** page of the **Distributor Properties** dialog box.    
-   The agent connects to the Subscriber using the credentials specified for the Distribution Agent in the New Subscription Wizard.  For more information, see  [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md). 
  
## See Also  
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)   
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
  
  
