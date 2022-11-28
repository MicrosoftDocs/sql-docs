---
description: "Secure the Publisher"
title: "Secure the Publisher | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "logins [SQL Server replication], publication access list"
  - "publications [SQL Server replication], publication access lists"
  - "publication access list (PAL)"
  - "PAL (publication access list)"
  - "Publishers [SQL Server replication], security"
  - "publications [SQL Server replication], security"
ms.assetid: 4513a18d-dd6e-407a-b009-49dc9432ec7e
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Secure the Publisher
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  
The following replication agents connect to the Publisher:  
  
-   Log Reader Agents
-   Snapshot Agent
-   Queue Reader Agent  
-   Merge Agent  
  
 We recommend that you provide an appropriate login for these agents, follow the principle of granting the minimal rights that are required, and protect the storage of all passwords. For information about the permissions that are required for each agent, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).  
  
 Besides appropriately managing logins and passwords, you should understand the role of the publication access list (PAL). The PAL is used to enable logins to access to publication data while restricting ad hoc access to the database at the Publisher.  
  
## Publication Access List  
 The PAL is the primary mechanism for securing publications at the Publisher. The PAL functions similarly to a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows access control list. When you create a publication, replication creates a PAL for the publication. The PAL can be configured to contain a list of logins and groups that are granted access to the publication. When an agent connects to the Publisher or Distributor and requests access to a publication, the authentication information in the PAL is compared to the Publisher login that the agent provides. This process provides additional security for the Publisher by preventing the Publisher and Distributor login from being used by a client tool to perform modifications on the Publisher directly.  
  
> [!NOTE]  
>  Replication creates a role on the Publisher for each publication to enforce PAL membership. The role has a name in the form **Msmerge_**_\<PublicationID>_ for merge replication and **MSReplPAL_**_\<PublicationDatabaseID>_**_**_\<PublicationID>_ for transactional and snapshot replication.  
  
 By default, the following logins are included in the PAL: the members of the **sysadmin** fixed server role at the time the publication is created and the login that is used to create the publication. By default, all logins that are members of the **sysadmin** fixed server role or the **db_owner** fixed database role on the publication database can subscribe to a publication without being explicitly added to the PAL.  
  
 When you are using the PAL, consider the following guidelines:  
  
-   You must associate the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login with a database user in the publication database before adding the login to the PAL.  
  
-   Follow the principle of least privilege by allowing logins in the PAL only the permissions the logins must have to perform replication tasks. Do not add the logins to any fixed database roles or server roles that are not required for replication. For more information about the permissions that are required, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md) and [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md).  
  
-   If a remote Distributor is used, accounts in the PAL must be available at both the Publisher and the Distributor. The account must be either a domain account or a local account that is defined at both servers. The passwords associated with both logins must be the same.  
  
-   If the PAL contains Windows accounts and the domain uses Active Directory, the account under which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] runs must have permissions to read from Active Directory. If you experience issues with Windows accounts, make sure that the account under which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] runs has sufficient permissions. For more information, see the Windows documentation.  
  
 To manage the PAL, see [Manage Logins in the Publication Access List](../../../relational-databases/replication/security/manage-logins-in-the-publication-access-list.md).  
  
## Snapshot Agent  
 There is one Snapshot Agent for each publication. For more information, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
## FTP Snapshot Delivery  
 If you specify that snapshots should be made available through an FTP share rather than a UNC share, you must specify a login and password when configuring FTP access. For more information, see [Deliver a Snapshot Through FTP](../../../relational-databases/replication/publish/deliver-a-snapshot-through-ftp.md).  
  
## Log Reader Agent  
 There is one Log Reader Agent for each database published for transactional replication. For more information, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
## Queue Reader Agent  
 There is one Queue Reader Agent for all Publishers and publications (that allow queued updating subscriptions) associated with a given Distributor. For more information, see [Enable Updating Subscriptions for Transactional Publications](../../../relational-databases/replication/publish/enable-updating-subscriptions-for-transactional-publications.md).  
  
## See Also  
 [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md)   
 [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md)   
 [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)  
  
  
