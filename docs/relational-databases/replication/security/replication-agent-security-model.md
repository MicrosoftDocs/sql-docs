---
title: "Replication Agent Security Model | Microsoft Docs"
description: In SQL Server, the replication agent security model allows for fine-grained control over the accounts under which replication agents run and make connections.
ms.custom: ""
ms.date: "04/26/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Snapshot Agent, security"
  - "agents [SQL Server replication], security"
  - "Distribution Agent, security"
  - "logins [SQL Server replication], permissions for agents"
  - "security [SQL Server replication], agent security model"
  - "Log Reader Agent, security"
  - "Queue Reader Agent, security"
  - "Merge Agent, security"
  - "replication [SQL Server], agents and profiles"
ms.assetid: 6d09fc8d-843a-4a7a-9812-f093d99d8192
author: "MashaMSFT"
ms.author: "mathoma"
---
# Replication Agent Security Model
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  The replication agent security model allows for fine-grained control over the accounts under which replication agents run and make connections: A different account can be specified for each agent. For more information about how to specify accounts, see [Identity and access control for replication](../../../relational-databases/replication/security/identity-and-access-control-replication.md).  

The replication agent security model is a little bit different for Azure SQL Managed Instance, as there are no Windows accounts under which the agents will run. Instead, everything must be done through SQL Server authentication. 
  
> [!IMPORTANT]  
>  When a member of the **sysadmin** fixed server role configures replication, replication agents can be configured to impersonate the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent account. This is done by not specifying a login and password for a replication agent; however, we do not recommend this approach. Instead, as a security best practice, we recommend that you specify an account for each agent that has the minimum permissions that are described in the section "Permissions That Are Required by Agents" later in this topic.  
  
 Replication agents, like all executables, run under the context of a Windows account. The agents make Windows Integrated Security connections by using this account. The account under which the agent runs depends on how the agent is started:  
  
-   Starting the agent from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent job, the default: When a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent job is used to start a replication agent, the agent runs under the context of an account that you specify when you configure replication. For more information about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent and replication, see the section "Agent Security under SQL Server Agent" later in this topic. For information about the permissions that are required for the account under which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent runs, see [Configure SQL Server Agent](../../../ssms/agent/configure-sql-server-agent.md).  
  
-   Starting the agent from an MS-DOS command line, either directly or through a script: The agent runs under the context of the account of the user that is running the agent at the command line.  
  
-   Starting the agent from an application that uses Replication Management Objects (RMO) or an ActiveX control: The agent runs under the context of the application that is calling RMO or the ActiveX control.  
  
    > [!NOTE]  
    >  ActiveX controls are deprecated.  
  
 We recommend that connections be made under the context of Windows Integrated Security. For backward compatibility, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Security can also be used. For more information about best practices, see [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md).  
  
## Permissions That Are Required by Agents  
 The accounts under which agents run and make connections require a variety of permissions. These permissions are described in the following table. We recommend that each agent run under a different Windows account and the account should be granted only the required permissions. For information about the publication access list (PAL), which is relevant for a number of agents, see [Secure the Publisher](../../../relational-databases/replication/security/secure-the-publisher.md).  
  
> [!NOTE]  
>  User Account Control (UAC) in some Windows operating systems can prevent administrative access to the snapshot share. You must therefore explicitly grant snapshot share permissions to the Windows accounts that are used by the Snapshot Agent, Distribution Agent, and Merge Agent. You must do this even if the Windows accounts are members of the Administrators group. For more information, see [Secure the Snapshot Folder](../../../relational-databases/replication/security/secure-the-snapshot-folder.md).  
  
|Agent|Permissions|  
|-----------|-----------------|  
|Snapshot Agent|The Windows account under which the agent runs is used when it makes connections to the Distributor. This account must:<br /><br /> -At minimum, be a member of the **db_owner** fixed database role in the distribution database.<br /><br /> -Have read, write, and modify permissions on the snapshot share.<br /><br /> <br /><br /> Note that the account that is used to *connect* to the Publisher must at minimum be a member of the **db_owner** fixed database role in the publication database.|  
|Log Reader Agent|The Windows account under which the agent runs is used when it makes connections to the Distributor. This account must at minimum be a member of the **db_owner** fixed database role in the distribution database.<br /><br /> The account that is used to connect to the Publisher must at minimum be a member of the **db_owner** fixed database role in the publication database.<br /><br /> When selecting the **sync_type** options *replication support only*, *initialize with backup*, or *initialize from lsn*, the log reader agent must run after executing **sp_addsubscription**, so that the set-up scripts are written to the distribution database. The log reader agent must be running under an account that is a member of the **sysadmin** fixed server role. When the **sync_type** option is set to *Automatic*, no special log reader agent actions are required.|  
|Distribution Agent for a push subscription|The Windows account under which the agent runs is used when it makes connections to the Distributor. This account must:<br /><br /> -At minimum be a member of the **db_owner** fixed database role in the distribution database.<br /><br /> -Be a member of the PAL.<br /><br /> -Have read permissions on the snapshot share.<br /><br /> -Have read permissions on the installation directory of the OLE DB provider for the Subscriber if the subscription is for a non-SQL Server Subscriber.<br /><br /> -When replicating LOB data, the distribution agent must have write permissions on the replication **C:\Program Files\Microsoft SQL Server\XX\COMfolder** where XX represents the instanceID.<br /><br /> <br /><br /> Note that the account that is used to *connect* to the Subscriber must at minimum be a member of the **db_owner** fixed database role in the subscription database, or have equivalent permissions if the subscription is for a non-SQL Server Subscriber.<br /><br /> Also note that when using `-subscriptionstreams >= 2` on the distribution agent you must also grant the **View Server State** permission on the subscribers to detect deadlocks.|  
|Distribution Agent for a pull subscription|The Windows account under which the agent runs is used when it makes connections to the Subscriber. This account must at minimum be a member of the **db_owner** fixed database role in the subscription database. The account that is used to connect to the Distributor must:<br /><br /> -Be a member of the PAL.<br /><br /> -Have read permissions on the snapshot share.<br /><br /> -When replicating LOB data, the distribution agent must have write permissions on the replication **C:\Program Files\Microsoft SQL Server\XX\COMfolder** where XX represents the instanceID.<br /><br /> <br /><br /> Note that when using `-subscriptionstreams >= 2` on the distribution agent you must also grant the **View Server State** permission on the subscribers to detect deadlocks.|  
|Merge Agent for a push subscription|The Windows account under which the agent runs is used when it makes connections to the Publisher and Distributor. This account must:<br /><br /> -At minimum be a member of the **db_owner** fixed database role in the distribution database.<br /><br /> -Be a member of the PAL.<br /><br /> -Be a login that is associated with a user with read/write permissions in the publication database.<br /><br /> -Have read permissions on the snapshot share.<br /><br /> <br /><br /> Note that the account used to *connect* to the Subscriber must at minimum be a member of the **db_owner** fixed database role in the subscription database.|  
|Merge Agent for a pull subscription|The Windows account under which the agent runs is used when it makes connections to the Subscriber. This account must at minimum be a member of the **db_owner** fixed database role in the subscription database. The account that is used to connect to the Publisher and Distributor must:<br /><br /> -Be a member of the PAL.<br /><br /> -Be a login associated with a user with read/write permissions in the publication database.<br /><br /> -Be a login associated with a user in the distribution database. The user can be the **Guest** user.<br /><br /> -Have read permissions on the snapshot share.|  
|Queue Reader Agent|The Windows account under which the agent runs is used when it makes connections to the Distributor. This account must at minimum be a member of the **db_owner** fixed database role in the distribution database.<br /><br /> The account that is used to connect to the Publisher must at minimum be a member of the **db_owner** fixed database role in the publication database.<br /><br /> The account that is used to connect to the Subscriber must at minimum be a member of the **db_owner** fixed database role in the subscription database.|  
  
## Agent Security Under SQL Server Agent  
 When you configure replication by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)] procedures, or RMO, a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent job is created by default for each agent. Agents then run under the context of a job step, regardless of whether they run continuously, on a schedule, or on demand. You can view these jobs under the **Jobs** folder in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]. The following table lists the job names.  
  
|Agent|Job name|  
|-----------|--------------|  
|Snapshot Agent|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<integer>**|  
|Snapshot Agent for a merge publication partition|**Dyn_\<Publisher>-\<PublicationDatabase>-\<Publication>-\<GUID>**|  
|Log Reader Agent|**\<Publisher>-\<PublicationDatabase>-\<integer>**|  
|Merge Agent for pull subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<SubscriptionDatabase>-\<integer>**|  
|Merge Agent for push subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<integer>**|  
|Distribution Agent for push subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<integer>**|  
|Distribution Agent for pull subscriptions|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<SubscriptionDatabase>-\<GUID>**|  
|Distribution Agent for push subscriptions to non-SQL Server Subscribers|**\<Publisher>-\<PublicationDatabase>-\<Publication>-\<Subscriber>-\<integer>**|  
|Queue Reader Agent|**[\<Distributor>].\<integer>**|  
  
 \*For push subscriptions to Oracle publications, the job name is **\<Publisher>-\<Publisher**> instead of **\<Publisher>-\<PublicationDatabase>**.  
  
 \*\*For pull subscriptions to Oracle publications, the job name is **\<Publisher>-\<DistributionDatabase**> instead of **\<Publisher>-\<PublicationDatabase>**.  
  
 When you configure replication, you specify accounts under which agents should run. However, all job steps run under the security context of a *proxy*; therefore, replication performs the following mappings internally for the agent accounts that you specify:  
  
-   The account is first mapped to a credential by using the [!INCLUDE[tsql](../../../includes/tsql-md.md)] [CREATE CREDENTIAL](../../../t-sql/statements/create-credential-transact-sql.md) statement. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent proxies use credentials to store information about Windows user accounts.  
  
-   The [sp_add_proxy](../../../relational-databases/system-stored-procedures/sp-add-proxy-transact-sql.md) stored procedure is called, and the credential is used to create a proxy..  
  
> [!NOTE]  
>  This information is provided to help you understand what is involved in running agents with the appropriate security context. You should not have to interact directly with the credentials or proxies that have been created.  
  
## See Also  
 [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md)   
 [View and modify replication security settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [Secure the Snapshot Folder](../../../relational-databases/replication/security/secure-the-snapshot-folder.md)  
  
  
