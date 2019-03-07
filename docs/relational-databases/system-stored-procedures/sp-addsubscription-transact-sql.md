---
title: "sp_addsubscription (Transact-SQL) | Microsoft Docs"
ms.date: "10/28/2015"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.custom: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addsubscription"
  - "sp_addsubscription_TSQL"
helpviewer_keywords: 
  - "sp_addsubscription"
ms.assetid: 61ddf287-1fa0-4c1a-8657-ced50cebf0e0
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addsubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a subscription to a publication and sets the Subscriber status. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addsubscription [ @publication = ] 'publication'  
    [ , [ @article = ] 'article']  
    [ , [ @subscriber = ] 'subscriber' ]  
    [ , [ @destination_db = ] 'destination_db' ]  
        [ , [ @sync_type = ] 'sync_type' ]  
    [ , [ @status = ] 'status'  
        [ , [ @subscription_type = ] 'subscription_type' ]  
    [ , [ @update_mode = ] 'update_mode' ]  
    [ , [ @loopback_detection = ] 'loopback_detection' ]  
    [ , [ @frequency_type = ] frequency_type ]  
    [ , [ @frequency_interval = ] frequency_interval ]  
    [ , [ @frequency_relative_interval = ] frequency_relative_interval ]  
    [ , [ @frequency_recurrence_factor = ] frequency_recurrence_factor ]  
    [ , [ @frequency_subday = ] frequency_subday ]  
    [ , [ @frequency_subday_interval = ] frequency_subday_interval ]  
    [ , [ @active_start_time_of_day = ] active_start_time_of_day ]  
    [ , [ @active_end_time_of_day = ] active_end_time_of_day ]  
    [ , [ @active_start_date = ] active_start_date ]  
    [ , [ @active_end_date = ] active_end_date ]  
    [ , [ @optional_command_line = ] 'optional_command_line' ]  
    [ , [ @reserved = ] 'reserved' ]  
    [ , [ @enabled_for_syncmgr= ] 'enabled_for_syncmgr' ]  
    [ , [ @offloadagent= ] remote_agent_activation]  
    [ , [ @offloadserver= ] 'remote_agent_server_name' ]  
    [ , [ @dts_package_name= ] 'dts_package_name' ]  
    [ , [ @dts_package_password= ] 'dts_package_password' ]  
    [ , [ @dts_package_location= ] 'dts_package_location' ]  
    [ , [ @distribution_job_name= ] 'distribution_job_name' ]  
    [ , [ @publisher = ] 'publisher' ]  
    [ , [ @backupdevicetype = ] 'backupdevicetype' ]  
    [ , [ @backupdevicename = ] 'backupdevicename' ]  
    [ , [ @mediapassword = ] 'mediapassword' ]  
    [ , [ @password = ] 'password' ]  
    [ , [ @fileidhint = ] fileidhint ]  
    [ , [ @unload = ] unload ]  
    [ , [ @subscriptionlsn = ] subscriptionlsn ]  
    [ , [ @subscriptionstreams = ] subscriptionstreams ]  
    [ , [ @subscriber_type = ] subscriber_type ]  
    [ , [ @memory_optimized = ] memory_optimized ]  
```  
  
## Arguments  
 [ @publication=] '*publication*'  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ @article=] '*article*'  
 Is the article to which the publication is subscribed. *article* is **sysname**, with a default of all. If all, a subscription is added to all articles in that publication. Only values of all or NULL are supported for Oracle Publishers.  
  
 [ @subscriber=] '*subscriber*'  
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of NULL.  
  
 [ @destination_db=] '*destination_db*'  
 Is the name of the destination database in which to place replicated data. *destination_db* is **sysname**, with a default of NULL. When NULL, *destination_db* is set to the name of the publication database. For Oracle Publishers, *destination_db* must be specified. For a non-SQL Server Subscriber, specify a value of (default destination) for *destination_db*.  
  
 [ @sync_type=] '*sync_type*'  
 Is the subscription synchronization type. *sync_type* is **nvarchar(255)**, and can be one of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|none|Subscriber already has the schema and initial data for published tables.<br /><br /> Note: This option has been deprecated. Use replication support only instead.|  
|automatic (default)|Schema and initial data for published tables are transferred to the Subscriber first.|  
|replication support only|Provides automatic generation at the Subscriber of article custom stored procedures and triggers that support updating subscriptions, if appropriate. Assumes that the Subscriber already has the schema and initial data for published tables. When configuring a peer-to-peer transactional replication topology, ensure that the data at all nodes in the topology is identical. For more information, see [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md).<br /><br /> *Not supported for subscriptions to non-SQL Server publications.*|  
|initialize with backup|Schema and initial data for published tables are obtained from a backup of the publication database. Assumes that the Subscriber has access to a backup of the publication database. The location of the backup and media type for the backup are specified by *backupdevicename* and *backupdevicetype*. When using this option, a peer-to-peer transactional replication topology need not be quiesced during configuration.<br /><br /> *Not supported for subscriptions to non-SQL Server publications.*|  
|initialize from lsn|Used when you are adding a node to a peer-to-peer transactional replication topology. Used with @subscriptionlsn to make sure that all relevant transactions are replicated to the new node. Assumes that the Subscriber already has the schema and initial data for published tables. For more information, see [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md).|  
  
> [!NOTE]  
>  System tables and data are always transferred.  
  
 [ @status=] '*status*'  
 Is the subscription status. *status* is **sysname**, with a default value of NULL. When this parameter is not explicitly set, replication automatically sets it to one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|active|Subscription is initialized and ready to accept changes. This option is set when the value of *sync_type* is none, initialize with backup, or replication support only.|  
|subscribed|Subscription needs to be initialized. This option is set when the value of *sync_type* is automatic.|  
  
 [ @subscription_type=] '*subscription_type*'  
 Is the type of subscription. *subscription_type* is **nvarchar(4)**, with a default of push. Can be push or pull. The Distribution Agents of push subscriptions reside at the Distributor, and the Distribution Agents of pull subscriptions reside at the Subscriber. *subscription_type* can be pull to create a named pull subscription that is known to the Publisher. For more information, see [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md).  
  
> [!NOTE]  
>  Anonymous subscriptions do not need to use this stored procedure.  
  
 [ @update_mode=] '*update_mode*'  
 Is the type of update.*update_mode* is **nvarchar(30)**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|read only (default)|The subscription is read-only. The changes at the Subscriber are not sent to the Publisher.|  
|sync tran|Enables support for immediate updating subscriptions. Not supported for Oracle Publishers.|  
|queued tran|Enables the subscription for queued updating. Data modifications can be made at the Subscriber, stored in a queue, and then propagated to the Publisher. Not supported for Oracle Publishers.|  
|failover|Enables the subscription for immediate updating with queued updating as a failover. Data modifications can be made at the Subscriber and propagated to the Publisher immediately. If the Publisher and Subscriber are not connected, the updating mode can be changed so that data modifications made at the Subscriber are stored in a queue until the Subscriber and Publisher are reconnected. Not supported for Oracle Publishers.|  
|queued failover|Enables the subscription as a queued updating subscription with the ability to change to immediate updating mode. Data modifications can be made at the Subscriber and stored in a queue until a connection is established between the Subscriber and Publisher. When a continuous connection is established the updating mode can be changed to immediate updating. Not supported for Oracle Publishers.|  
  
 Note that the values synctran and queued tran are not allowed if the publication being subscribed to allows DTS.  
  
 [ @loopback_detection=] '*loopback_detection*'  
 Specifies if the Distribution Agent sends transactions that originated at the Subscriber back to the Subscriber. *loopback_detection* is **nvarchar(5)**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|true|Distribution Agent does not send transactions originated at the Subscriber back to the Subscriber. Used with bidirectional transactional replication. For more information, see [Bidirectional Transactional Replication](../../relational-databases/replication/transactional/bidirectional-transactional-replication.md).|  
|false|Distribution Agent sends transactions that originated at the Subscriber back to the Subscriber.|  
|NULL (default)|Automatically set to true for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber and false for a non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber.|  
  
 [ @frequency_type=] *frequency_type*  
 Is the frequency with which to schedule the distribution task. *frequency_type* is int, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|1|One time|  
|2|On demand|  
|4|Daily|  
|8|Weekly|  
|16|Monthly|  
|32|Monthly relative|  
|64 (default)|Autostart|  
|128|Recurring|  
  
 [ @frequency_interval=] *frequency_interval*  
 Is the value to apply to the frequency set by *frequency_type*. *frequency_interval* is **int**, with a default of NULL.  
  
 [ @frequency_relative_interval=] *frequency_relative_interval*  
 Is the date of the Distribution Agent. This parameter is used when *frequency_type* is set to 32 (monthly relative). *frequency_relative_interval* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|1|First|  
|2|Second|  
|4|Third|  
|8|Fourth|  
|16|Last|  
|NULL (default)||  
  
 [ @frequency_recurrence_factor=] *frequency_recurrence_factor*  
 Is the recurrence factor used by *frequency_type*. *frequency_recurrence_factor* is **int**, with a default of NULL.  
  
 [ @frequency_subday=] *frequency_subday*  
 Is how often, in minutes, to reschedule during the defined period. *frequency_subday* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|1|Once|  
|2|Second|  
|4|Minute|  
|8|Hour|  
|NULL||  
  
 [ @frequency_subday_interval=] *frequency_subday_interval*  
 Is the interval for *frequency_subday*. *frequency_subday_interval* is **int**, with a default of NULL.  
  
 [ @active_start_time_of_day=] *active_start_time_of_day*  
 Is the time of day when the Distribution Agent is first scheduled, formatted as HHMMSS. *active_start_time_of_day* is **int**, with a default of NULL.  
  
 [ @active_end_time_of_day=] *active_end_time_of_day*  
 Is the time of day when the Distribution Agent stops being scheduled, formatted as HHMMSS. *active_end_time_of_day* is **int**, with a default of NULL.  
  
 [ @active_start_date=] *active_start_date*  
 Is the date when the Distribution Agent is first scheduled, formatted as YYYYMMDD. *active_start_date* is **int**, with a default of NULL.  
  
 [ @active_end_date=] *active_end_date*  
 Is the date when the Distribution Agent stops being scheduled, formatted as YYYYMMDD. *active_end_date* is **int**, with a default of NULL.  
  
 [ @optional_command_line=] '*optional_command_line*'  
 Is the optional command prompt to execute. *optional_command_line* is **nvarchar(4000)**, with a default of NULL.  
  
 [ @reserved=] '*reserved*'  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ @enabled_for_syncmgr=] '*enabled_for_syncmgr*'  
 Is whether the subscription can be synchronized through [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Synchronization Manager. *enabled_for_syncmgr* is **nvarchar(5)**, with a default of FALSE. If false, the subscription is not registered with Windows Synchronization Manager. If true, the subscription is registered with Windows Synchronization Manager and can be synchronized without starting [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Not supported for Oracle Publishers.  
  
 [ @offloadagent= ] '*remote_agent_activation*'  
 Specifies that the agent can be activated remotely. *remote_agent_activation* is **bit** with a default of 0.  
  
> [!NOTE]  
>  This parameter has been deprecated and is only maintained for backward compatibility of scripts.  
  
 [ @offloadserver= ] '*remote_agent_server_name*'  
 Specifies the network name of server to be used for remote activation. *remote_agent_server_name*is **sysname**, with a default of NULL.  
  
 [ @dts_package_name= ] '*dts_package_name*'  
 Specifies the name of the Data Transformation Services (DTS) package. *dts_package_name* is a **sysname** with a default of NULL. For example, to specify a package of DTSPub_Package, the parameter would be `@dts_package_name = N'DTSPub_Package'`. This parameter is available for push subscriptions. To add DTS package information to a pull subscription, use sp_addpullsubscription_agent.  
  
 [ @dts_package_password= ] '*dts_package_password*'  
 Specifies the password on the package, if there is one. *dts_package_password* is **sysname** with a default of NULL.  
  
> [!NOTE]  
>  You must specify a password if *dts_package_name* is specified.  
  
 [ @dts_package_location= ] '*dts_package_location*'  
 Specifies the package location. *dts_package_location* is a **nvarchar(12)**, with a default of DISTRIBUTOR. The location of the package can be distributor or subscriber.  
  
 [ @distribution_job_name= ] '*distribution_job_name*'  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ @publisher= ] '*publisher*'  
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be specified for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
 [ @backupdevicetype= ] '*backupdevicetype*'  
 Specifies the type of backup device used when initializing a Subscriber from a backup. *backupdevicetype* is **nvarchar(20)**, and can be one of these values:  
  
|Value|Description|  
|-----------|-----------------|  
|logical (default)|The backup device is a logical device.|  
|disk|The backup device is disk drive.|  
|tape|The backup device is a tape drive|  
  
 *backupdevicetype* is only used when *sync_method*is set to initialize_with_backup.  
  
 [ @backupdevicename= ] '*backupdevicename*'  
 Specifies the name of the device used when initializing a Subscriber from a backup. *backupdevicename* is **nvarchar(1000)**, with a default of NULL.  
  
 [ @mediapassword= ] '*mediapassword*'  
 Specifies a password for the media set if a password was set when the media was formatted. *mediapassword* is **sysname**, with a default value of NULL.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 [ @password= ] '*password*'  
 Specifies a password for the backup if a password was set when the backup was created. *password*is **sysname**, with a default value of NULL.  
  
 [ @fileidhint= ] *fileidhint*  
 Identifies an ordinal value of the backup set to be restored. *fileidhint* is **int**, with a default value of NULL.  
  
 [ @unload= ] *unload*  
 Specifies if a tape backup device should be unloaded after the initialization from back is complete. *unload* is **bit**, with a default value of 1. 1 specifies that the tape should be unloaded. *unload* is only used when *backupdevicetype* is tape.  
  
 [ @subscriptionlsn= ] *subscriptionlsn*  
 Specifies the log sequence number (LSN) at which a subscription should start delivering changes to a node in a peer-to-peer transactional replication topology. Used with a @sync_type value of initialize from lsn to make sure that all relevant transactions are replicated to a new node. For more information, see [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md).  
  
 [ @subscriptionstreams= ] *subscriptionstreams*  
 Is the number of connections allowed per Distribution Agent to apply batches of changes in parallel to a Subscriber, while maintaining many of the transactional characteristics present when using a single thread. *subscriptionstreams* is **tinyint**, with a default value of NULL. A range of values from 1 to 64 is supported. This parameter is not supported for non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, Oracle Publishers or peer-to-peer subscriptions. Whenever subscription streams is used additional rows are added in the msreplication_subscriptions table (1 per stream) with an agent_id set to NULL.  
  
> [!NOTE]  
>  Subscriptionstreams do not work for articles configured to deliver [!INCLUDE[tsql](../../includes/tsql-md.md)]. To use subscriptionstreams, configure articles to deliver stored procedure calls instead.  
  
 [ @subscriber_type=] *subscriber_type*  
 Is the type of Subscriber. *subscriber_type* is **tinyint**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|0 (default)|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber|  
|1|ODBC data source server|  
|2|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Jet database|  
|3|OLE DB provider|  
  
 [ @memory_optimized=] *memory_optimized*  
 Indicates that  the subscription supports memory optimized tables. *memory_optimized* is **bit**, where 1 equals true (the subscription supports memory optimized tables).  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 sp_addsubscription is used in snapshot replication and transactional replication.  
  
 When sp_addsubscription is executed by a member of the sysadmin fixed server role to create a push subscription, the Distribution Agent job is implicitly created and runs under the SQL Server Agent service account. We recommend that you execute [sp_addpushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md) and specify the credentials of a different, agent-specific Windows account for @job_login and @job_password. For more information, see [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
 sp_addsubscription prevents ODBC and OLE DB Subscribers access to publications that:  
  
-   Were created with the native *sync_method* in the call to [sp_addpublication](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md).  
  
-   Contain articles that were added to the publication with the [sp_addarticle](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) stored procedure that had a *pre_creation_cmd* parameter value of 3 (truncate).  
  
-   Attempt to set *update_mode* to sync tran.  
  
-   Have an article configured to use parameterized statements.  
  
 In addition, if a publication has the *allow_queued_tran* option set to true (which enables queuing of changes at the Subscriber until they can be applied at the Publisher), the timestamp column in an article is scripted out as **timestamp**, and changes on that column are sent to the Subscriber. The Subscriber generates and updates the timestamp column value. For an ODBC or OLE DB Subscriber, sp_addsubscription fails if an attempt is made to subscribe to a publication that has *allow_queued_tran* set to true and articles with timestamp columns in it.  
  
 If a subscription does not use a DTS package, it cannot subscribe to a publication that is set to *allow_transformable_subscriptions*. If the table from the publication needs to be replicated to both a DTS subscription and non-DTS subscription, two separate publications have to be created: one for each type of subscription.  
  
 When selecting the **sync_type** options *replication support only*, *initialize with backup*, or *initialize from lsn*, the log reader agent must run after executing **sp_addsubscription**, so that the set-up scripts are written to the distribution database. The log reader agent must be running under an account that is a member of the **sysadmin** fixed server role. When the **sync_type** option is set to *Automatic*, no special log reader agent actions are required.  
  
## Permissions  
 Only members of the sysadmin fixed server role or db_owner fixed database role can execute sp_addsubscription. For pull subscriptions, users with logins in the publication access list can execute sp_addsubscription.  
  
## Example  
 [!code-sql[HowTo#sp_addtranpushsubscription_agent](../../relational-databases/replication/codesnippet/tsql/sp-addsubscription-trans_1.sql)]  
  
## See Also  
 [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md)   
 [Create a Subscription for a Non-SQL Server Subscriber](../../relational-databases/replication/create-a-subscription-for-a-non-sql-server-subscriber.md)   
 [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md)   
 [sp_addpushsubscription_agent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md)   
 [sp_changesubstatus &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changesubstatus-transact-sql.md)   
 [sp_dropsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscription-transact-sql.md)   
 [sp_helpsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
