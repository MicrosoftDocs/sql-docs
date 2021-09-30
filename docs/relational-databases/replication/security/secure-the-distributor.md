---
description: "Secure the Distributor"
title: "Secure the Distributor | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "security [SQL Server replication], Distributors"
  - "Distributors [SQL Server replication], security"
ms.assetid: 76d78229-0ff2-4aa4-9b4e-ad97534c5296
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Secure the Distributor
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  The following replication agents connect to the Distributor: the Log Reader Agent, Snapshot Agent, Queue Reader Agent, Distribution Agent, and Merge Agent. It is important to provide an appropriate login for each of these agents while following the principle of granting the minimal rights necessary and also protecting the storage of all passwords:  
  
-   For information about managing logins and passwords, see [Manage Logins and Passwords in Replication](../../../relational-databases/replication/security/identity-and-access-control-replication.md).  
  
-   For detailed information about the permissions required for each agent, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).  
  
 In addition to managing logins and passwords appropriately, it is important to understand the role of the **repl_distributor** remote server link and the **distributor_admin** account.  
  
## Securing the Connection from the Publisher to the Distributor  
 To support the communication necessary when administrative stored procedures execute at the Publisher and update information at the Distributor, replication automatically configures the remote server **repl_distributor**. The **repl_distributor** remote server entry is used for communication to the distribution database regardless of whether the distribution database is contained within the Publisher instance (a local Distributor) or resides within a remote [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance (a remote Distributor).  
  
 When the distribution database is contained on a local instance, a random password is generated and configured automatically. When the distribution database is located on a remote instance, the administrator configures a shared password during setup of the Publisher and Distributor; this password is then used to provide authentication of traffic that traverses the **repl_distributor** link.  
  
 The Distributor uses the **repl_distributor** remote server entry to verify that the calling server is configured as a Publisher at the Distributor, validates the password supplied by the Publisher, and validates that the stored procedure is a replication stored procedure during execution.  
  
 The password configured for the **repl_distributor** remote server entry during setup is associated with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login, **distributor_admin**, which is added to the **sysadmin** fixed server role at the Distributor. The **distributor_admin** login is used by replication stored procedures when connecting to the Distributor.  
  
> [!NOTE]  
>  Do not change the password for the **distributor_admin** manually. Always use the **sp_changedistributor_password** stored procedure, or the **Distributor Properties** or **Update Replication Passwords** dialog boxes in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], because password changes are then applied to local publications automatically.

## Disabling the distributor_admin login

It is not recommended to disable the **distributor_admin** login. If the **distributor_admin** login at distributor is disabled, you may experience the following or additional errors.

When using remote distributor, disabling the **distributor_admin** login will encounter the following issues.

- The creation or deletion of publications will not be possible.
- It will not be possible to change the articles of an existing publication.
- It will not be possible to show the agent status using SSMS or replication monitor on the publisher.
- It will not be possible to create or delete subscriptions.
- The posting of tracer tokens via replication monitor or by executing **sys.sp_posttracertoken** will not be possible.
- Configuring a remote publisher at the distributor will not be possible.

When using a local distributor, disabling the **distributor_admin** login may not encounter any of the above issues but is still not a recommended practice.
  
## Snapshot Folder Security  
 Ensure that the snapshot share has read access granted to the account under which the Merge Agent (for merge replication) or Distribution Agent (for snapshot or transactional replication) runs and write access granted to the account under which the Snapshot Agent runs. For more information about the snapshot folder, see [Secure the Snapshot Folder](../../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
## See Also  
 [View and Modify Replication Security Settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md)   
 [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md)   
 [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)  
  
  
