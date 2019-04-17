---
title: "Login for Updatable Subscriptions | Microsoft Docs"
ms.custom: ""
ms.date: "08/25/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newsubwizard.updatablesubscriptionslogin.f1"
ms.assetid: 301ea227-0455-40ba-9009-d38f8676b325
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Login for Updatable Subscriptions
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  For immediate update, if you selected **Replicate** on the **Updatable Subscriptions** page of this wizard, you must specify an account with the Subscriber under which connections to the Publisher are made. 
  
 Connections are used by the triggers that fire at the Subscriber, and propagate changes to the Publisher. This account is required even if you selected **Queue changes and commit when possible** on the **Updatable Subscriptions** page. The New Subscription Wizard by default configures queued updating with the ability to switch to immediate updating if required.  
  
> **IMPORTANT!!** The account specified for the connection should only be granted permission to insert, update, and delete data on the views that replication creates in the publication database. It should not be given any additional permissions. Grant permissions on Views in the publication database named in the form **syncobj_**_\<HexadecimalNumber>_ to the account you configured at each Subscriber.  
  
 There are three options available for the type of connection:  
  
-   A linked server or remote server that you have already defined.  
  
-   A linked server that replication creates; the connection is made with the credentials you specify in this wizard.  
  
-   A linked server that replication creates; the connection is made with the credentials of the user making the change at the Subscriber.  
  
 The first two options can be specified in this wizard. The last option can only be specified using [sp_link_publication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md); specify a value of **1** for the parameter **@security_mode**.  
  
## Options  
 **Create a linked server that connects using the following SQL Server Authentication login:**  
 Replication creates a linked server using the credentials specified in the **Login** and **Password** fields.  
  
 **Login**  
 Enter a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that has only the permissions described in this topic.  
  
 **Password**  
 Enter a strong password for the login specified in **Login**.  
    
 **Use a linked server or remote server that you have already defined.**  
 This option requires a linked server or remote server that you have already defined. For more information, see [Linked Servers &#40;Database Engine&#41;](../../relational-databases/linked-servers/linked-servers-database-engine.md) and [Remote Servers](../../database-engine/configure-windows/remote-servers.md). Ensure that the login used for the linked server or remote server has a strong password and has only the permissions described in this topic.  
  
## See also  
 [Create an Updatable Subscription to a Transactional Publication](publish/create-an-updatable-subscription-to-a-transactional-publication.md)   
 [View and Modify Replication Security Settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md)   
 [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)  
  
  
