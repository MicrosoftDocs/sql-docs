---
title: "Create an Updatable Subscription (Transactional)"
description: Learn how to create an Updatable Subscription to a Transactional Publication for SQL Server. 
ms.custom: seo-lt-2019
ms.date: "11/20/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "updatable transactional subscriptions"
  - "updateable transactional subscriptions, SSMS"
ms.assetid: f9ef89ed-36f6-431b-8843-25d445ec137f
author: "MashaMSFT"
ms.author: "mathoma"
---
# Create an Updatable Subscription to a Transactional Publication
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
> [!NOTE]  
>  This feature remains supported in versions of [!INCLUDE[ssNoVersion_md](../../../includes/ssnoversion-md.md)] from 2012 through 2016.  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  

Transactional replication enables changes made at a Subscriber to be propagated back to the Publisher using either immediate or queued updating subscriptions. You can create an updating subscription programmatically using replication stored procedures. 
 
Configure updatable subscriptions on the **Updatable Subscriptions** page of the **New Subscription Wizard**. This page is only available if you have enabled a transactional publication for updatable subscriptions. For more information about enabling updatable subscriptions, see [Enable Updating Subscriptions for Transactional Publications](../../../relational-databases/replication/publish/enable-updating-subscriptions-for-transactional-publications.md).   
  
## Configure an updatable subscription from the Publisher  

1. Connect to the Publisher in Microsoft SQL Server Management Studio, and then expand the server node.
2. Expand the **Replication** folder, and then expand the **Local Publications** folder.
3. Right-click a transactional publication enabled for updating subscriptions, and then click **New Subscriptions**.
4. Follow pages in the wizard to specify options for the subscription, such as where the Distribution Agent should run.
5. On the **Updatable Subscriptions** page of the **New Subscription Wizard**, ensure **Replicate** is selected.
6. Select an option from the **Commit at Publisher** drop-down list:

    * To use immediate updating subscriptions, select **Simultaneously commit changes**. If you select this option, and the publication allows queued updating subscriptions (the default for publications created with the New Publication Wizard), the subscription property **update_mode** is set to **failover**. This mode allows you to switch to queued updating later if necessary.

    * To use queued updating subscriptions, select **Queue changes and commit when possible**. If you select this option, and the publication allows immediate updating subscriptions (the default for publications created with the New Publication Wizard), and the Subscriber is running SQL Server 2005 or a later version, the subscription property **update_mode** is set to queued failover. This mode allows you to switch to immediate updating later if necessary.

    For information about switching update modes, see [Switch Between Update Modes for an Updatable Transactional Subscription](../../../relational-databases/replication/administration/switch-between-update-modes-for-an-updatable-transactional-subscription.md).

7. The **Login for Updatable Subscriptions** page is displayed for subscriptions that use immediate updating or have **update_mode** set to **queued failover**. On the **Login for Updatable Subscriptions** page, specify a linked server over which connections to the Publisher are made for immediate updating subscriptions. Connections are used by the triggers that fire at the Subscriber and propagate changes to the Publisher. Select one of the following options:

    * **Create a linked server that connects using SQL Server Authentication.** Select this option if you have not defined a remote server or linked server between the Subscriber and the Publisher. Replication creates a linked server for you. The account you specify must already exist at the Publisher.

    * **Use a linked server or remote server that you have already defined.** Select this option if you have defined a remote server or linked server between the Subscriber and the Publisher using [sp_addserver (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md), [sp_addlinkedserver (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md), SQL Server Management Studio, or another method.

    For information about the permissions required by the linked server account, see the **Queued Updating Subscriptions** of [enter link description here](../../../relational-databases/replication/security/secure-the-subscriber.md).

8. Complete the wizard.

## Configure an updatable subscription from the Subscriber


1. Connect to the Subscriber in SQL Server Management Studio, and then expand the server node.
2. Expand the **Replication** folder.
3. Right-click the **Local Subscriptions** folder, and then click **New Subscriptions**.
4. On the **Publication** page of the **New Subscription Wizard**, select **Find SQL Server Publisher** from the **Publisher** drop-down list.
5. Connect to the Publisher in the **Connect to Server** dialog box.
6. Select a transactional publication enabled for updating subscriptions on the **Publication** page.
7. Follow pages in the wizard to specify options for the subscription, such as where the Distribution Agent should run.
8. On the **Updatable Subscriptions** page of the New Subscription Wizard, ensure **Replicate** is selected.
9. Select an option from the **Commit at Publisher** drop-down list:

    * To use immediate updating subscriptions, select **Simultaneously commit changes**. If you select this option, and the publication allows queued updating subscriptions (the default for publications created with the New Publication Wizard), the subscription property **update_mode** is set to **failover**. This mode allows you to switch to queued updating later if necessary.

    * To use queued updating subscriptions, select **Queue changes and commit when possible**. If you select this option, and the publication allows immediate updating subscriptions (the default for publications created with the New Publication Wizard), and the Subscriber is running SQL Server 2005 or a later version, the subscription property **update_mode** is set to queued **failover**. This mode allows you to switch to immediate updating later if necessary.

    For information about switching update modes, see [Switch Between Update Modes for an Updatable Transactional Subscription](../../../relational-databases/replication/administration/switch-between-update-modes-for-an-updatable-transactional-subscription.md).

10. The **Login for Updatable Subscriptions** page is displayed for subscriptions that use immediate updating or have **update_mode** set to queued **failover**. On the **Login for Updatable Subscriptions** page, specify a linked server over which connections to the Publisher are made for immediate updating subscriptions. Connections are used by the triggers that fire at the Subscriber and propagate changes to the Publisher. Select one of the following options:

    * **Create a linked server that connects using SQL Server Authentication.** Select this option if you have not defined a remote server or linked server between the Subscriber and the Publisher. Replication creates a linked server for you. The account you specify must already exist at the Publisher.

    * **Use a linked server or remote server that you have already defined.** Select this option if you have defined a remote server or linked server between the Subscriber and the Publisher using [sp_addserver (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md), [sp_addlinkedserver (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md), SQL Server Management Studio, or another method.

    For information about the permissions required by the linked server account, see the **Queued Updating Subscriptions** of [enter link description here](../../../relational-databases/replication/security/secure-the-subscriber.md).

11. Complete the wizard.

## Create an immediate updating pull subscription ##

1. At the Publisher, verify that the publication supports immediate updating subscriptions by executing [sp_helppublication](../../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md). 

    * If the value of `allow_sync_tran` in the result set is `1`, the publication supports immediate updating subscriptions.
    * If the value of `allow_sync_tran` in the result set is `0`, the publication must be recreated with immediate updating subscriptions enabled.

2. At the Publisher, verify that the publication supports pull subscriptions by executing [sp_helppublication](../../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md). 

    * If the value of `allow_pull` in the result set is `1`, the publication supports pull subscriptions.
    * If the value of `allow_pull` is `0`, execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md), specifying `allow_pull` for `@property` and `true` for `@value`. 

3. At the Subscriber, execute [sp_addpullsubscription](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md). Specify `@publisher` and `@publication`, and one of the following values for `@update_mode`:

    * `sync tran` - enables the subscription for immediate updating.
    * `failover` - enables the subscription for immediate updating with queued updating as a failover option.
    > [!NOTE]  
    >  `failover` requires that the publication also be enabled for queued updating subscriptions. 
 
4. At the Subscriber, execute [sp_addpullsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md). Specify the following:

    * The `@publisher`, `@publisher_db`, and `@publication` parameters. 
    * The Microsoft Windows credentials under which the Distribution Agent at the Subscriber runs for `@job_login` and `@job_password`. 

    > [!NOTE]  
    > Connections made using Windows Integrated Authentication are always made using the Windows credentials specified by `@job_login` and `@job_password`. The Distribution Agent always makes the local connection to the Subscriber using Windows Integrated Authentication. By default, the agent connects to the Distributor using Windows Integrated Authentication. 
 
    * (Optional) A value of `0` for `@distributor_security_mode` and the Microsoft SQL Server login information for `@distributor_login` and `@distributor_password`, if you need to use SQL Server Authentication when connecting to the Distributor. 
    * A schedule for the Distribution Agent job for this subscription. 

5. At the Subscriber on the subscription database, execute [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md). Specify `@publisher`, `@publication`, the name of the publication database for `@publisher_db`, and one of the following values for `@security_mode`: 

    * `0` - Use SQL Server Authentication when making updates at the Publisher. This option requires you to specify a valid login at the Publisher for `@login` and `@password`.
    * `1` - Use the security context of the user making changes at the Subscriber when connecting to the Publisher. See [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md) for restrictions related to this security mode.
    * `2` - Use an existing, user-defined linked server login created using [sp_addlinkedserver](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md).

6. At the publisher, execute [sp_addsubscription](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) specifying `@publication`, `@subscriber`, `@destination_db`, a value of pull for `@subscription_type`, and the same value specified in step 3 for `@update_mode`. This registers the pull subscription at the Publisher. 


## Create an immediate updating push subscription ##

1. At the Publisher, verify that the publication supports immediate updating subscriptions by executing [sp_helppublication](../../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md). 

    * If the value of `allow_sync_tran` in the result set is `1`, the publication supports immediate updating subscriptions.
    * If the value of `allow_sync_tran` in the result set is `0`, the publication must be recreated with immediate updating subscriptions enabled.

2. At the Publisher, verify that the publication supports push subscriptions by executing [sp_helppublication](../../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md). 

    * If the value of `allow_push` in the result set is `1`, the publication supports push subscriptions.
    * If the value of `allow_push` is `0`, execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md), specifying `allow_push` for `@property` and `true` for `@value`. 

3. At the Publisher, execute [sp_addsubscription](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). Specify `@publication`, `@subscriber`, `@destination_db`, and one of the following values for `@update_mode`:

    * `sync tran` - enables support for immediate updating.
    * `failover` - enables support for immediate updating with queued updating as a failover option.

    > [!NOTE]  
    >  `failover` requires that the publication also be enabled for queued updating subscriptions. 
 
4. At the Publisher, execute [sp_addpushsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md). Specify the following parameters:

    * `@subscriber`, `@subscriber_db`, and `@publication`. 

    * The Windows credentials under which the Distribution Agent at the Distributor runs for `@job_login` and `@job_password`. 

    > [!NOTE]  
    >  Connections made using Windows Integrated Authentication are always made using the Windows credentials specified by `@job_login` and `@job_password`. The Distribution Agent always makes the local connection to the Distributor using Windows Integrated Authentication. By default, the agent will connect to the Subscriber using Windows Integrated Authentication. 

    * (Optional) A value of `0` for `@subscriber_security_mode` and the SQL Server login information for `@subscriber_login` and `@subscriber_password`, if you need to use SQL Server Authentication when connecting to the Subscriber. 
    * A schedule for the Distribution Agent job for this subscription.

5. At the Subscriber on the subscription database, execute [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md). Specify `@publisher`, `@publication`, the name of the publication database for `@publisher_db`, and one of the following values for `@security_mode`: 

     * `0` - Use SQL Server Authentication when making updates at the Publisher. This option requires you to specify a valid login at the Publisher for `@login` and `@password`.
     * `1` - Use the security context of the user making changes at the Subscriber when connecting to the Publisher. See [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md) for restrictions related to this security mode.
     * `2` - Use an existing, user-defined linked server login created using [sp_addlinkedserver](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md).


## Create a queued updating pull subscription

1. At the Publisher, verify that the publication supports queued updating subscriptions by executing [sp_helppublication](../../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md). 

    * If the value of `allow_queued_tran` in the result set is `1`, the publication supports immediate updating subscriptions.
    * If the value of `allow_queued_tran` in the result set is `0`, the publication must be recreated with queued updating subscriptions enabled.

2. At the Publisher, verify that the publication supports pull subscriptions by executing [sp_helppublication](../../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md). 

    * If the value of `allow_pull` in the result set is `1`, the publication supports pull subscriptions.
    * If the value of `allow_pull` is `0`, execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md), specifying `allow_pull` for `@property` and `true` for `@value`. 

3. At the Subscriber, execute [sp_addpullsubscription](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-transact-sql.md). Specify `@publisher` and `@publication`, and one of the following values for `@update_mode`:

    * `queued tran` - enables the subscription for queued updating.
    * `queued failover` - enables support for queued updating with immediate updating as a failover option.

    > [!NOTE]  
    >  `queued failover` requires that the publication also be enabled for immediate updating subscriptions. To fail over to immediate updating, you must use [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md) to define the credentials under which changes at the Subscriber are replicated to the Publisher.
 
4. At the Subscriber, execute [sp_addpullsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md). Specify the following parameters:

    * @publisher, `@publisher_db`, and `@publication`. 
    * The Windows credentials under which the Distribution Agent at the Subscriber runs for `@job_login` and `@job_password`. 

    > [!NOTE]  
    > Connections made using Windows Integrated Authentication are always made using the Windows credentials specified by `@job_login` and `@job_password`. The Distribution Agent always makes the local connection to the Subscriber using Windows Integrated Authentication. By default, the agent connects to the Distributor using Windows Integrated Authentication. 
 
    * (Optional) A value of `0` for `@distributor_security_mode` and the SQL Server login information for `@distributor_login` and `@distributor_password`, if you need to use SQL Server Authentication when connecting to the Distributor. 
    * A schedule for the Distribution Agent job for this subscription.

5. At the publisher, execute [sp_addsubscriber](../../../relational-databases/system-stored-procedures/sp-addsubscriber-transact-sql.md) to register the Subscriber at the Publisher, specifying `@publication`, `@subscriber`, `@destination_db`, a value of pull for `@subscription_type`, and the same value specified in step 3 for `@update_mode`. This registers the pull subscription at the Publisher. 


## Create a queued updating push subscription 

1. At the Publisher, verify that the publication supports queued updating subscriptions by executing [sp_helppublication](../../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md). 

    * If the value of allow_queued_tran in the result set is 1, the publication supports immediate updating subscriptions.
    * If the value of allow_queued_tran in the result set is 0, the publication must be recreated with queued updating subscriptions enabled. For more information, see How to: Enable Updating Subscriptions for Transactional Publications (Replication Transact-SQL Programming).

2. At the Publisher, verify that the publication supports push subscriptions by executing [sp_helppublication](../../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md). 

    * If the value of `allow_push` in the result set is `1`, the publication supports push subscriptions.
    * If the value of `allow_push` is `0`, execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md), specifying allow_push for `@property` and `true` for `@value`. 

3. At the Publisher, execute [sp_addsubscription](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md). Specify `@publication`, `@subscriber`, `@destination_db`, and one of the following values for `@update_mode`:

    * `queued tran` - enables the subscription for queued updating.
    * `queued failover` - enables support for queued updating with immediate updating as a failover option.

    > [!NOTE]  
    > The queued failover option requires that the publication also be enabled for immediate updating subscriptions. To fail over to immediate updating, you must use [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md) to define the credentials under which changes at the Subscriber are replicated to the Publisher.

4. At the Publisher, execute [sp_addpushsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md). Specify the following parameters:

    * `@subscriber`, `@subscriber_db`, and `@publication`. 
    * The Windows credentials under which the Distribution Agent at the Distributor runs for `@job_login` and `@job_password`. 

    > [!NOTE]  
    > Connections made using Windows Integrated Authentication are always made using the Windows credentials specified by `@job_login` and `@job_password`. The Distribution Agent always makes the local connection to the Distributor using Windows Integrated Authentication. By default, the agent connects to the Subscriber using Windows Integrated Authentication. 
 
    * (Optional) A value of `0` for `@subscriber_security_mode` and the SQL Server login information for `@subscriber_login` and `@subscriber_password`, if you need to use SQL Server Authentication when connecting to the Subscriber. 
    * A schedule for the Distribution Agent job for this subscription.

## Set queued updating conflict resolution options 
Set conflict resolution options for publications that support queued updating subscriptions on the **Subscription Options** page of the **Publication Properties - \<Publication>** dialog box. For more information about accessing this dialog box, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
  
1.  On the **Subscription Options** page of the **Publication Properties - \<Publication>** dialog box, select one of the following values for the **Conflict resolution policy** option:  
  
    -   **Keep the Publisher change**    
    -   **Keep the Subscriber change**    
    -   **Reinitialize the subscription**  


## Example

This example creates an immediate updating pull subscription to a publication that supports immediate updating subscriptions. Login and password values are supplied at runtime using sqlcmd scripting variables.

> [!NOTE]  
>  This script uses sqlcmd scripting variables. They are in the form `$(MyVariable)`. For information about how to use scripting variables on the command line and in SQL Server Management Studio, see the **Executing Replication Scripts** section in the topic [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md).

```sql
-- Execute this batch at the Subscriber.
DECLARE @publication AS sysname;
DECLARE @publicationDB AS sysname;
DECLARE @publisher AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS nvarchar(512);
SET @publication = N'AdvWorksProductTran';
SET @publicationDB = N'AdventureWorks2008R2';
SET @publisher = $(PubServer);
SET @login = $(Login);
SET @password = $(Password);

-- At the subscription database, create a pull subscription to a transactional 
-- publication using immediate updating with queued updating as a failover.
EXEC sp_addpullsubscription 
    @publisher = @publisher, 
    @publication = @publication, 
    @publisher_db = @publicationDB, 
    @update_mode = N'failover', 
	@subscription_type = N'pull';

-- Add an agent job to synchronize the pull subscription, 
-- which uses Windows Authentication when connecting to the Distributor.
EXEC sp_addpullsubscription_agent 
    @publisher = @publisher, 
    @publisher_db = @publicationDB, 
    @publication = @publication,
    @job_login = @login,
    @job_password = @password; 

-- Add a Windows Authentication-based linked server that enables the 
-- Subscriber-side triggers to make updates at the Publisher. 
EXEC sp_link_publication 
    @publisher = @publisher, 
    @publication = @publication,
    @publisher_db = @publicationDB, 
    @security_mode = 0,
    @login = @login,
    @password = @password;
GO

USE AdventureWorks2008R2;
GO

-- Execute this batch at the Publisher.
DECLARE @publication AS sysname;
DECLARE @subscriptionDB AS sysname;
DECLARE @subscriber AS sysname;
SET @publication = N'AdvWorksProductTran'; 
SET @subscriptionDB = N'AdventureWorks2008R2Replica'; 
SET @subscriber = $(SubServer);

-- At the Publisher, register the subscription, using the defaults.
USE [AdventureWorks2008R2]
EXEC sp_addsubscription 
	@publication = @publication, 
	@subscriber = @subscriber, 
	@destination_db = @subscriptionDB, 
	@subscription_type = N'pull', 
	@update_mode = N'failover';
GO
``` 


## See Also

[Updatable Subscriptions for Transactional Replication](../../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)   
[Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md)   
[Use sqlcmd with Scripting Variables](../../../ssms/scripting/sqlcmd-use-with-scripting-variables.md)   


