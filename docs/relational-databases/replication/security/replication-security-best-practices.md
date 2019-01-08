---
title: "Replication Security Best Practices | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "security [SQL Server replication], best practices"
  - "security [SQL Server replication], between domains"
  - "authentication [SQL Server replication]"
  - "Internet [SQL Server replication], security"
ms.assetid: 1ab2635d-0992-4c99-b17d-041d02ec9a7c
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Replication Security Best Practices
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Replication moves data in distributed environments ranging from intranets on a single domain to applications that access data between untrusted domains and over the Internet. It is important to understand the best approach for securing replication connections under these different circumstances.  
  
 The following information is relevant to replication in all environments:  
  
-   Encrypt the connections between computers in a replication topology using an industry standard method, such as Virtual Private Networks (VPN), Secure Sockets Layer (SSL), or IP Security (IPSEC). For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md). For information about using VPN and SSL for replicating data over the Internet, see [Securing Replication Over the Internet](../../../relational-databases/replication/security/securing-replication-over-the-internet.md).  
  
     If you use SSL to secure the connections between computers in a replication topology, specify a value of **1** or **2** for the **-EncryptionLevel** parameter of each replication agent (a value of **2** is recommended). A value of **1** specifies that encryption is used, but the agent does not verify that the SSL server certificate is signed by a trusted issuer; a value of **2** specifies that the certificate is verified. Agent parameters can be specified in agent profiles and on the command line. For more information, see:  
  
    -   [Work with Replication Agent Profiles](../../../relational-databases/replication/agents/work-with-replication-agent-profiles.md)  
  
    -   [View and Modify Replication Agent Command Prompt Parameters &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/agents/view-and-modify-replication-agent-command-prompt-parameters.md)  
  
    -   [Replication Agent Executables Concepts](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)  
  
-   Run each replication agent under a different Windows account, and use Windows Authentication for all replication agent connections. For more information about specifying accounts, see [Identity and access control for replication](../../../relational-databases/replication/security/identity-and-access-control-replication.md).  
  
-   Grant only the required permissions to each agent. For more information, see the "Permissions Required by Agents" section of [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).  
  
-   Ensure all Merge Agent and Distribution Agent accounts are in the publication access list (PAL). For more information, see [Secure the Publisher](../../../relational-databases/replication/security/secure-the-publisher.md).  
  
-   Follow the principle of least privilege by allowing accounts in the PAL only the permissions they need to perform replication tasks. Do not add the logins to any fixed server roles that are not required for replication.  
  
-   Configure the snapshot share to allow read access by all Merge Agents and Distribution Agents. In the case of snapshots for publications with parameterized filters, ensure that each folder is configured to allow access only to the appropriate Merge Agent accounts.  
  
-   Configure the snapshot share to allow write access by the Snapshot Agent.  
  
-   If you use pull subscriptions, use a network share rather than a local path for the snapshot folder.  
  
 If your replication topology includes computers that are not in the same domain or are in domains that do not have trust relationships with each other, you can use Windows Authentication or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication for the connections made by agents (For more information about domains, see the Windows documentation). It is recommended as a security best practice that you use Windows Authentication.  
  
-   To use Windows Authentication:  
  
    -   Add a local Windows account (not a domain account) for each agent at the appropriate nodes (use the same name and password at each node). For example, the Distribution Agent for a push subscription runs at the Distributor and makes connections to the Distributor and Subscriber. The Windows account for the Distribution Agent should be added to the Distributor and Subscriber.  
  
    -   Ensure that a given agent (for example the Distribution Agent for a subscription) runs under the same account at each computer.  
  
-   To use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication:  
  
    -   Add a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] account for each agent at the appropriate nodes (use the same account name and password at each node). For example, the Distribution Agent for a push subscription runs at the Distributor and makes connections to the Distributor and Subscriber. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] account for the Distribution Agent should be added to the Distributor and Subscriber.  
  
    -   Ensure that a given agent (for example the Distribution Agent for a subscription) makes connections under the same account at each computer.  
  
    -   In situations that require [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication, access to UNC snapshot shares is often not available (for example access might be blocked by a firewall). In this case, you can transfer the snapshot to Subscribers through file transfer protocol (FTP). For more information, see [Transfer Snapshots Through FTP](../../../relational-databases/replication//publish/deliver-a-snapshot-through-ftp.md).  
  
## See Also  
 [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md)   
 [Replication over the Internet](../../../relational-databases/replication/replication-over-the-internet.md)   
 [Secure the Subscriber](../../../relational-databases/replication/security/secure-the-subscriber.md)   
 [Secure the Distributor](../../../relational-databases/replication/security/secure-the-distributor.md)   
 [Secure the Publisher](../../../relational-databases/replication/security/secure-the-publisher.md)   
 [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)  
  
  
