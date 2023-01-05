---
description: "Secure the Distributor"
title: "Secure the Distributor | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
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

 If the **distributor_admin** login is disabled at a remote Distributor, you may no longer be able to do the following:
 
 - Create or delete publications.
 - Change the articles of an existing publication. 
 - See the agent status using SQL Server Management Studio (SSMS) or Replication Monitor on the Publisher.
 - Create or delete subscriptions. 
 - Post tracer tokens by using Replication Monitor or by executing **sys.sp_posttracertoken**.
 - Configure a remote Publisher at the Distributor. 

As such, disabling the **distributor_admin** login at a remote Distributor is not recommended. While disabling the **distributor_admin** login on a local distributor may not impose the same limitations, it is still not a recommended practice. 

  
## Snapshot Folder Security  
 Ensure that the snapshot share has read access granted to the account under which the Merge Agent (for merge replication) or Distribution Agent (for snapshot or transactional replication) runs and write access granted to the account under which the Snapshot Agent runs. For more information about the snapshot folder, see [Secure the Snapshot Folder](../../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
## See Also  
 [View and Modify Replication Security Settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md)   
 [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md)   
 [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)  
  
  
