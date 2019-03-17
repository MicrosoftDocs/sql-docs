---
title: "Secure the Subscriber | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [SQL Server replication], security"
  - "Subscribers [SQL Server replication], security"
  - "security [SQL Server replication], Subscribers"
ms.assetid: c8f0d62a-8b5d-4a21-9aec-223da52bb708
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Secure the Subscriber
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Merge Agents and Distribution Agents connect to the Subscriber. These connections can be made under the context of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login or a Windows login. It is important to provide an appropriate login for these agents while following the principle of granting the minimal rights necessary and also protecting the storage of all passwords. For information about the permissions required for each agent, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).  
  
## Distribution Agent  
 There is either one Distribution Agent per subscription (an independent agent, the default for publications created in the New Publication Wizard) or one Distribution Agent per publication database and subscription database pair (a shared agent). T  
  
 To specify connection information for push subscriptions, see [Create a Push Subscription](../../../relational-databases/replication/create-a-push-subscription.md).  
  
 To specify connection information for pull subscriptions, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md)  
  
## Merge Agent  
 Each merge subscription has its own Merge Agent that connects to and updates both the Publisher and the Subscriber.  
  
 To specify connection information for push subscriptions, see [Create a Push Subscription](../../../relational-databases/replication/create-a-push-subscription.md).  
  
 To specify connection information for pull subscriptions, see [Create a Pull Subscription](../../../relational-databases/replication/create-a-pull-subscription.md).  
  
## Immediate Updating Subscriptions  
 When you configure an immediate updating subscription, you specify an account at the Subscriber under which connections to the Publisher are made. Connections are used by the triggers that fire at the Subscriber and propagate changes to the Publisher. There are three options available for the type of connection:  
  
-   A linked server that replication creates; the connection is made with the credentials you specify at configuration time.  
  
-   A linked server that replication creates; the connection is made with the credentials of the user making the change at the Subscriber.  
  
-   A linked server or remote server that you have already defined.  
  
> [!IMPORTANT]  
>  To specify connection information, use the stored procedure [sp_link_publication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md). You can also use the **Login for Updatable Subscriptions** page of the New Subscription Wizard, which calls **sp_link_publication**. Under certain conditions, this stored procedure can fail if the Subscriber is running [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] Service Pack 1 (SP1) or later, and the Publisher is running an earlier version. If the stored procedure fails in this scenario, upgrade the Publisher to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] SP1 or later.  
  
 For more information, see [Create an Updatable Subscription to a Transactional Publication](../../../relational-databases/replication/publish/create-an-updatable-subscription-to-a-transactional-publication.md) and [View and Modify Replication Security Settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
> [!IMPORTANT]  
>  The account specified for the connection should only be granted permission to insert, update, and delete data on the views that replication creates in the publication database; it should not be given any additional permissions. Grant permissions on views in the publication database that are named in the form **syncobj_***\<HexadecimalNumber>* to the account you configured at each Subscriber.  
  
## Queued Updating Subscriptions  
 When you configure queued updating subscriptions, there are two areas to keep in mind that relate to security:  
  
-   There is only one Queue Reader Agent for each Distributor. It is recommended that for each Distributor, you configure at most one publication that is enabled for queued updating subscriptions.  
  
-   The Queue Reader agent makes connections to the Distributor, Publisher, and each Subscriber:  
  
    -   The account under which the agent runs and makes connections to the Distributor is specified when you create the agent (if you use the New Publication Wizard, the agent is created when you create a publication that is enabled for updating subscriptions).  
  
    -   The account under which the agent makes connections to the Publisher is specified when you configure distribution for a Publisher. Specify the Windows account under which the agent runs or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] account.  
  
    -   The account under which the agent makes connections to the Subscriber is specified when you create the subscription.  
  
    > [!IMPORTANT]  
    >  Use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication for connections to Subscribers, and specify a different account for the connection to each Subscriber. If you use a pull subscription, replication always sets the connection to use Windows Authentication (for pull subscriptions, replication cannot access metadata at the Subscriber required to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication). In this case, change the connection to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication after the subscription is configured.  
  
     For more information, see How to: Create an Updating Subscription to a Transactional Publication (SQL Server Management Studio) and [View and Modify Replication Security Settings](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).  
  
## See Also  
 [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md)   
 [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md)   
 [Security and Protection &#40;Replication&#41;](../../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)  
  
  
