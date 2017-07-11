---
title: "Create Updatable Subscription to Transactional Publication | Microsoft Docs"
ms.custom: ""
ms.date: "07/21/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "updateable transactional subscriptions, T-SQL"
ms.assetid: a6e80857-0a69-4867-b6b7-f3629d00c312
caps.latest.revision: 6
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Create Updatable Subscription to Transactional Publication

> [!NOTE]  
>  This feature remains supported in versions of [!INCLUDE[ssNoVersion_md](../../../includes/ssnoversion-md.md)] from 2012 through 2016.  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
 
Transactional replication enables changes made at a Subscriber to be propagated back to the Publisher using either immediate or queued updating subscriptions. You can create an updating subscription programmatically using replication stored procedures. (Also, see [Create an Updatable Subscription to a Transactional Publication (Management Studio)](../../../relational-databases/replication/publish/create-an-updatable-subscription-to-a-transactional-publication.md).) 

## To create an immediate updating pull subscription ##

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
>  Connections made using Windows Integrated Authentication are always made using the Windows credentials specified by `@job_login` and `@job_password`. The Distribution Agent always makes the local connection to the Subscriber using Windows Integrated Authentication. By default, the agent connects to the Distributor using Windows Integrated Authentication. 
 
    * (Optional) A value of `0` for `@distributor_security_mode` and the Microsoft SQL Server login information for `@distributor_login` and `@distributor_password`, if you need to use SQL Server Authentication when connecting to the Distributor. 

    * A schedule for the Distribution Agent job for this subscription. 

5. At the Subscriber on the subscription database, execute [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md). Specify `@publisher`, `@publication`, the name of the publication database for `@publisher_db`, and one of the following values for `@security_mode`: 

    * `0` - Use SQL Server Authentication when making updates at the Publisher. This option requires you to specify a valid login at the Publisher for `@login` and `@password`.

    * `1` - Use the security context of the user making changes at the Subscriber when connecting to the Publisher. See [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md) for restrictions related to this security mode.

    * `2` - Use an existing, user-defined linked server login created using [sp_addlinkedserver](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md).

6. At the publisher, execute [sp_addsubscription](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) specifying `@publication`, `@subscriber`, `@destination_db`, a value of pull for `@subscription_type`, and the same value specified in step 3 for `@update_mode`.

This registers the pull subscription at the Publisher. 


## To create an immediate updating push subscription ##

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


## To create a queued updating pull subscription ##

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
>  Connections made using Windows Integrated Authentication are always made using the Windows credentials specified by `@job_login` and `@job_password`. The Distribution Agent always makes the local connection to the Subscriber using Windows Integrated Authentication. By default, the agent connects to the Distributor using Windows Integrated Authentication. 
 
    * (Optional) A value of `0` for `@distributor_security_mode` and the SQL Server login information for `@distributor_login` and `@distributor_password`, if you need to use SQL Server Authentication when connecting to the Distributor. 

    * A schedule for the Distribution Agent job for this subscription.

5. At the publisher, execute [sp_addsubscriber](../../../relational-databases/system-stored-procedures/sp-addsubscriber-transact-sql.md) to register the Subscriber at the Publisher, specifying `@publication`, `@subscriber`, `@destination_db`, a value of pull for `@subscription_type`, and the same value specified in step 3 for `@update_mode`.

This registers the pull subscription at the Publisher. 


## To create a queued updating push subscription ##

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
>  The queued failover option requires that the publication also be enabled for immediate updating subscriptions. To fail over to immediate updating, you must use [sp_link_publication](../../../relational-databases/system-stored-procedures/sp-link-publication-transact-sql.md) to define the credentials under which changes at the Subscriber are replicated to the Publisher.

4. At the Publisher, execute [sp_addpushsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md). Specify the following parameters:

    * `@subscriber`, `@subscriber_db`, and `@publication`. 

    * The Windows credentials under which the Distribution Agent at the Distributor runs for `@job_login` and `@job_password`. 

    > [!NOTE]  
>  Connections made using Windows Integrated Authentication are always made using the Windows credentials specified by `@job_login` and `@job_password`. The Distribution Agent always makes the local connection to the Distributor using Windows Integrated Authentication. By default, the agent connects to the Subscriber using Windows Integrated Authentication. 
 
    * (Optional) A value of `0` for `@subscriber_security_mode` and the SQL Server login information for `@subscriber_login` and `@subscriber_password`, if you need to use SQL Server Authentication when connecting to the Subscriber. 

    * A schedule for the Distribution Agent job for this subscription.


## Example ##

This example creates an immediate updating pull subscription to a publication that supports immediate updating subscriptions. Login and password values are supplied at runtime using sqlcmd scripting variables.

> [!NOTE]  
>  This script uses sqlcmd scripting variables. They are in the form `$(MyVariable)`. For information about how to use scripting variables on the command line and in SQL Server Management Studio, see the **Executing Replication Scripts** section in the topic [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md).

```
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

## See Also ##

[Updatable Subscriptions for Transactional Replication](../../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)

[Use sqlcmd with Scripting Variables](../../../relational-databases/scripting/sqlcmd-use-with-scripting-variables.md)

[Create an Updatable Subscription to a Transactional Publication (Management Studio)](../../../relational-databases/replication/publish/create-an-updatable-subscription-to-a-transactional-publication.md)

