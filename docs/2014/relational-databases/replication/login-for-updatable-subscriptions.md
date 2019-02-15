---
title: "Login for Updatable Subscriptions | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.newsubwizard.updatablesubscriptionslogin.f1"
ms.assetid: 301ea227-0455-40ba-9009-d38f8676b325
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Login for Updatable Subscriptions
  If you selected **Replicate** on the **Updatable Subscriptions** page of this wizard, you must specify an account at the Subscriber under which connections to the Publisher are made for immediate updating subscriptions. Connections are used by the triggers that fire at the Subscriber and propagate changes to the Publisher. This account is required even if you selected **Queue changes and commit when possible** on the **Updatable Subscriptions** page, because by default the New Subscription Wizard configures queued updating with the ability to switch to immediate updating if required.  
  
> [!IMPORTANT]  
>  The account specified for the connection should only be granted permission to insert, update, and delete data on the views that replication creates in the publication database; it should not be given any additional permissions. Grant permissions on views in the publication database that are named in the form **syncobj_**_\<HexadecimalNumber>_ to the account you configured at each Subscriber.  
  
 There are three options available for the type of connection:  
  
-   A linked server or remote server that you have already defined.  
  
-   A linked server that replication creates; the connection is made with the credentials you specify in this wizard.  
  
-   A linked server that replication creates; the connection is made with the credentials of the user making the change at the Subscriber.  
  
 The first two options can be specified in this wizard. The last option can only be specified using [sp_link_publication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-link-publication-transact-sql); specify a value of **1** for the parameter **@security_mode**.  
  
## Options  
 **Create a linked server that connects using the following SQL Server Authentication login:**  
 Replication creates a linked server using the credentials specified in the **Login** and **Password** fields.  
  
 **Login**  
 Enter a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that has only the permissions described in this topic.  
  
 **Password**  
 Enter a strong password for the login specified in **Login**.  
  
 **Confirm Password**  
 Re-enter the password to confirm that it was entered correctly.  
  
 **Use a linked server or remote server that you have already defined.**  
 This option requires a linked server or remote server that you have already defined. For more information, see [Linked Servers &#40;Database Engine&#41;](../linked-servers/linked-servers-database-engine.md) and [Remote Servers](../../database-engine/configure-windows/remote-servers.md). Ensure that the login used for the linked server or remote server has a strong password and has only the permissions described in this topic.  
  
## See Also  
 [Create an Updatable Subscription to a Transactional Publication](publish/create-an-updatable-subscription-to-a-transactional-publication.md)   
 [View and Modify Replication Security Settings](security/view-and-modify-replication-security-settings.md)   
 [Updatable Subscriptions for Transactional Replication](transactional/updatable-subscriptions-for-transactional-replication.md)   
 [Subscribe to Publications](subscribe-to-publications.md)  
  
  
