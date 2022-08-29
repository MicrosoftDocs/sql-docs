---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    21000    |    16    |    No    |    Cannot subscribe to an inactive publication.    |
|    21001    |    16    |    No    |    Cannot add a Distribution Agent at the Subscriber for a push subscription.    |
|    21002    |    16    |    No    |    The Distribution Agent for this subscription already exists (%s).    |
|    21003    |    16    |    No    |    Changing publication names is no longer supported.    |
|    21004    |    16    |    No    |    Cannot publish the database object '%s' because it is encrypted.    |
|    21005    |    10    |    No    |    For backward compatibility, sp_addpublisher can be used to add a Publisher for this Distributor. However, sp_adddistpublisher is more flexible.    |
|    21006    |    16    |    No    |    Cannot use sp_addpublisher to add a Publisher. Use sp_adddistpublisher.    |
|    21007    |    16    |    No    |    Cannot add the remote Distributor. Make sure that the local server is configured as a Publisher at the Distributor.    |
|    21008    |    16    |    No    |    Cannot uninstall the Distributor because there are Subscribers defined.    |
|    21009    |    16    |    No    |    The specified filter procedure is already associated with a table.    |
|    21010    |    16    |    No    |    Removed %ld replicated transactions consisting of %ld statements in %ld seconds (%ld rows/sec).    |
|    21011    |    16    |    No    |    Deactivated subscriptions.    |
|    21012    |    16    |    No    |    Cannot change the 'allow_push' property of the publication to "false". There are push subscriptions on the publication.    |
|    21013    |    16    |    No    |    Cannot change the 'allow_pull' property of the publication to "false". There are pull subscriptions on the publication.    |
|    21014    |    16    |    No    |    The \@optname parameter value must be 'transactional' or 'merge'.    |
|    21015    |    16    |    No    |    The replication option '%s' has been set to TRUE already.    |
|    21016    |    16    |    No    |    The replication option '%s' has been set to FALSE already.    |
|    21017    |    16    |    No    |    Cannot perform SQL Server 7.0 compatible checksum operation on a merge article that has a vertical or horizontal partition. Rowcount validation and SQL Server 2000 compatible binary checksum operation can be performed on this article.    |
|    21018    |    16    |    No    |    There are too many consecutive snapshot transactions in the distribution database. Run the Log Reader Agent again or clean up the distribution database.    |
|    21019    |    10    |    No    |    Distribution agent for subscription added.    |
|    21020    |    10    |    No    |    no comment specified.    |
|    21021    |    16    |    No    |    Drop the Distributor before you uninstall replication.    |
|    21022    |    16    |    No    |    If set 'immediate_sync' property of a publication to true then must also set 'independent_agent' property to true.    |
|    21023    |    16    |    No    |    '%s' is no longer supported.    |
|    21024    |    16    |    No    |    The stored procedure '%s' is already published as an incompatible type.    |
|    21025    |    16    |    No    |    The string being encrypted cannot have null characters.    |
|    21026    |    16    |    No    |    Cannot have an anonymous subscription on a publication that does not have an independent agent.    |
|    21027    |    16    |    No    |    '%s' replication stored procedures are not installed. You must reinstall SQL Server with Replication.    |
|    21028    |    16    |    No    |    Replication components are not installed on this server. Run SQL Server Setup again and select the option to install replication.    |
|    21029    |    16    |    No    |    Cannot drop a push subscription entry at the Subscriber unless \@drop_push is 'true'.    |
|    21030    |    16    |    No    |    Names of SQL Server replication agents cannot be changed.    |
|    21031    |    16    |    No    |    'post_script' is not supported for stored procedure articles.    |
|    21032    |    16    |    No    |    Could not subscribe because non-SQL Server Subscriber '%s' does not support 'sync tran' update mode.    |
|    21033    |    16    |    No    |    Cannot drop server '%s' as Distribution Publisher because there are databases enabled for replication on that server.    |
|    21034    |    16    |    No    |    Rows inserted or updated at the Subscriber cannot be outside the article partition.    |
|    21035    |    16    |    No    |    You have updated the Publisher property '%s' successfully.    |
|    21036    |    16    |    No    |    Another %s agent for the subscription or subscriptions is running, or the server is working on a previous request by the same agent.    |
|    21037    |    16    |    No    |    Invalid working directory '%s'.    |
|    21038    |    16    |    No    |    Windows Authentication is not supported by the server.    |
|    21039    |    16    |    No    |    The article '%s' contains the destination owner '%s'. Non-SQL Server Subscribers require articles to have the destination owner of NULL.    |
|    21040    |    16    |    No    |    Publication '%s' does not exist.    |
|    21041    |    16    |    No    |    A remote distribution Publisher is not allowed on this server version.    |
|    21042    |    16    |    No    |    The distribution Publisher property, 'distributor_password', has no usage and is not supported for a Distributor running on Windows NT 4.0.    |
|    21043    |    16    |    No    |    The Distributor is not installed.    |
|    21044    |    16    |    No    |    Cannot ignore the remote Distributor (\@ignore_remote_distributor cannot be 1) when enabling the database for publishing or merge publishing.    |
|    21045    |    16    |    No    |    Cannot uninstall the Distributor because there are databases enabled for publishing or merge publishing.    |
|    21046    |    16    |    No    |    Cannot change distribution Publisher property 'distribution_db' because the Publisher is using the current distribution database.    |
|    21047    |    16    |    No    |    Cannot drop the local distribution Publisher because there are Subscribers defined.    |
|    21048    |    16    |    No    |    Cannot add login '%s' to the publication access list because it does not have access to the distribution server '%s'.    |
|    21049    |    16    |    No    |    The login '%s' does not have access permission on publication '%s' because it is not in the publication access list.    |
|    21050    |    16    |    No    |    Only members of the sysadmin fixed server role or db_owner fixed database role can perform this operation. Contact an administrator with sufficient permissions to perform this operation.    |
|    21051    |    16    |    No    |    Could not subscribe because non-SQL Server Subscriber '%s' does not support custom stored procedures.    |
|    21052    |    16    |    No    |    Cannot write to the message queue for the queued updating subscription. Ensure that Microsoft Distributed Transaction Coordinator is running, and that the subscription is active and initialized. If the subscription uses Microsoft Message Queueing, ensure the proper permissions are set on the queue.    |
|    21053    |    16    |    No    |    Input property parameter is not valid. See SQL Server Books Online for a list of valid parameters for sp_changemergepublication.    |
|    21054    |    16    |    No    |    The trigger at the Subscriber could not execute commands at the Publisher over the linked server connection (triggers are used for Subscribers with updating subscriptions). Ensure sp_link_publication has been used to configure the linked server properly, and ensure that the login used to connect to the Publisher is in the publication access list.    |
|    21055    |    15    |    No    |    Invalid value for parameter %s specified for %s.    |
|    21056    |    16    |    No    |    The subscription to publication '%s' has expired or does not exist.    |
|    21057    |    16    |    No    |    Anonymous Subscribers cannot have updatable subscriptions.    |
|    21058    |    16    |    No    |    An updatable subscription to publication '%s' on Subscriber '%s' already exists.    |
|    21059    |    16    |    No    |    Cannot reinitialize subscriptions of non-immediate_sync publications.    |
|    21060    |    16    |    No    |    Could not subscribe because non-SQL Server Subscriber '%s' does not support parameterized statements.    |
|    21061    |    16    |    No    |    Invalid article status %d specified when adding article '%s'.    |
|    21062    |    16    |    No    |    The row size of table '%s' exceeds the replication limit of 6,000 bytes.    |
|    21063    |    16    |    No    |    Table '%s' cannot participate in updatable subscriptions because it is published for merge replication.    |
|    21064    |    16    |    No    |    The subscription is uninitialized or unavailable for immediate updating as it is marked for reinitialization. If using queued failover option, run Queue Reader Agent for subscription initialization. Try again after the (re)initialization completes.    |
|    21070    |    16    |    No    |    This subscription does not support automatic reinitialization (subscribed with the 'no sync' option). To reinitialize this subscription, you must drop and re-create the subscription.    |
|    21071    |    10    |    No    |    Cannot reinitialize article '%s' in subscription '%s:%s' to publication '%s' (subscribed with the 'no sync' option).    |
|    21072    |    16    |    No    |    The subscription has not been synchronized within the maximum retention period or it has been dropped at the Publisher. You must reinitialize the subscription to receive data.    |
|    21073    |    16    |    No    |    The publication specified does not exist.    |
|    21074    |    16    |    No    |    The subscription(s) have been marked inactive and must be reinitialized. NoSync subscriptions will need to be dropped and recreated.    |
|    21075    |    10    |    No    |    The initial snapshot for publication '%s' is not yet available.    |
|    21076    |    10    |    No    |    The initial snapshot for article '%s' is not yet available.    |
|    21077    |    10    |    No    |    Deactivated initial snapshot for anonymous publication(s). New subscriptions must wait for the next scheduled snapshot.    |
|    21078    |    16    |    No    |    Table '%s' does not exist in the Subscriber database.    |
|    21079    |    16    |    No    |    The RPC security information for the Publisher is missing or invalid. Use sp_link_publication to specify it.    |
|    21080    |    16    |    No    |    The 'msrepl_tran_version' column must be in the vertical partition of the article that is enabled for updatable subscriptions; it cannot be dropped.    |
|    21081    |    16    |    No    |    Server setting 'Allow triggers to be fired which fire other triggers (nested triggers)' must exist on updatable Subscribers.    |
|    21082    |    16    |    No    |    Database property 'IsRecursiveTriggersEnabled' has to be false for subscription databases at Subscribers that allow updatable subscriptions.    |
|    21083    |    16    |    No    |    Database compatibility level at immediate updating Subscribers cannot be less than 70.    |
|    21084    |    16    |    No    |    Publication '%s' does not allow anonymous subscriptions.    |
|    21085    |    16    |    No    |    The retention period must be less than the retention period for the distribution database.    |
|    21086    |    16    |    No    |    The retention period for the distribution database must be greater than the retention period of any existing non-merge publications.    |
|    21087    |    16    |    No    |    Client subscriptions and anonymous subscriptions cannot republish data. To republish data from this database, the subscription to the root Publisher must be a server subscription with a priority greater than 0. Drop the current subscription and create a server subscription.    |
|    21088    |    10    |    No    |    The initial snapshot for the publication is not yet available.    |
|    21089    |    16    |    No    |    Only members of the sysadmin fixed server role can perform this operation.    |
|    21090    |    16    |    No    |    Cannot upgrade merge replication metadata. Attempt the upgrade again by running the Merge Agent for the Subscriber or by running the Snapshot Agent for the Publisher.    |
|    21091    |    16    |    No    |    Global subscribers with priority 0 are not allowed to create merge publications.    |
|    21101    |    10    |    No    |    The custom command name %s specified for parameter %s will be ignored. A system generated name will be used instead. The publication allows %s and command names need not be specified.    |
|    21105    |    16    |    No    |    This edition of SQL Server cannot act as a Publisher or Distributor for replication.    |
|    21106    |    16    |    No    |    This edition of SQL Server does not support publications.    |
|    21107    |    16    |    No    |    '%ls' is not a table or view.    |
|    21108    |    16    |    No    |    This edition of SQL Server does not support transactional publications.    |
|    21109    |    16    |    No    |    The parameters \@xact_seqno_start and \@xact_seqno_end must be identical if \@command_id is specified.    |
|    21110    |    16    |    No    |    \@xact_seqno_start and \@publisher_database_id must be specified if \@command_id is specified.    |
|    21111    |    16    |    No    |    '%s' is not a valid parameter for the Snapshot Agent.    |
|    21112    |    16    |    No    |    '%s' is not a valid parameter for the Log Reader Agent.    |
|    21113    |    16    |    No    |    '%s' is not a valid parameter for the Distribution Agent.    |
|    21114    |    16    |    No    |    '%s' is not a valid parameter for the Merge Agent.    |
|    21115    |    16    |    No    |    %d is not a valid value for the '%s' parameter. The value must be a positive integer.    |
|    21116    |    16    |    No    |    '%s' is not a valid value for the '%s' parameter. The value must be 1, 2, or 3.    |
|    21117    |    16    |    No    |    '%s' is not a valid value for the '%s' parameter. The value must be 0, 1, or 2.    |
|    21118    |    16    |    No    |    '%s' is not a valid value for the '%s' parameter. The value must be greater than or equal to 0 and less than or equal to 10,000.    |
|    21119    |    16    |    No    |    %s is not a valid value for the '%s' parameter. The value must be a non-negative integer.    |
|    21120    |    16    |    No    |    Only members of the sysadmin fixed server role or db_owner fixed database role, or the owner of the subscription can drop subscription '%s' to publication '%s'.    |
|    21121    |    16    |    No    |    Only members of the sysadmin fixed server role and '%s' can drop the pull subscription to the publication '%s'.    |
|    21122    |    16    |    No    |    Cannot drop the distribution database '%s' because it is currently in use.    |
|    21123    |    16    |    No    |    The agent profile '%s' could not be found at the Distributor.    |
|    21124    |    16    |    No    |    Cannot find the table name or the table owner corresponding to the alternative table ID(nickname) '%d' in sysmergearticles.    |
|    21125    |    16    |    No    |    A table used in merge replication must have at least one non-computed column.    |
|    21126    |    16    |    No    |    Pull subscriptions cannot be created in the same database as the publication.    |
|    21127    |    16    |    No    |    Only global merge subscriptions can be added to database '%s'.    |
|    21128    |    16    |    No    |    Terminating immediate updating or queued updating INSERT trigger because it is not the first trigger to fire. Use sp_settriggerorder procedure to set the firing order for trigger '%s' to first.    |
|    21129    |    16    |    No    |    Terminating immediate updating or queued updating UPDATE trigger because it is not the first trigger to fire. Use sp_settriggerorder procedure to set the firing order for trigger '%s' to first.    |
|    21130    |    16    |    No    |    Terminating immediate updating or queued updating DELETE trigger because it is not the first trigger to fire. Use sp_settriggerorder procedure to set the firing order for trigger '%s' to first.    |
|    21131    |    16    |    No    |    There are existing subscriptions to heterogeneous publication '%s'. To add new articles, first drop the existing subscriptions to the publication.    |
|    21132    |    16    |    No    |    Cannot create transactional subscription to merge publication '%s'. The publication type should be either transactional(0) or snapshot(1) for this operation.    |
|    21133    |    16    |    No    |    Publication '%s' is not enabled to use an independent agent.    |
|    21134    |    16    |    No    |    The specified job ID must identify a Distribution Agent or a Merge Agent job.    |
|    21135    |    16    |    No    |    Detected inconsistencies in the replication agent table. The specified job ID does not correspond to an entry in '%ls'.    |
|    21136    |    16    |    No    |    Detected inconsistencies in the replication agent table. The specified job ID corresponds to multiple entries in '%ls'.    |
|    21137    |    16    |    No    |    This procedure supports only remote execution of push subscription agents.    |
|    21138    |    16    |    No    |    The 'offload_server' property cannot be the same as the Distributor name.    |
|    21139    |    16    |    No    |    Could not determine the Subscriber name for distributed agent execution.    |
|    21140    |    16    |    No    |    Agent execution cannot be distributed to a Subscriber that resides on the same server as the Distributor.    |
|    21141    |    16    |    No    |    The \@change_active flag may not be specified for articles with manual filters or views.    |
|    21142    |    16    |    No    |    The SQL Server '%s' could not obtain Windows group membership information for login '%s'. Verify that the Windows account has access to the domain of the login.    |
|    21143    |    16    |    No    |    The custom stored procedure schema option is invalid for a snapshot publication article.    |
|    21144    |    16    |    No    |    Cannot subscribe to publication of sync_type 'dump database' because the Subscriber has subscriptions to other publications.    |
|    21145    |    16    |    No    |    Cannot subscribe to publication %s because the Subscriber has a subscription to a publication of sync_type 'dump database'.    |
|    21146    |    16    |    No    |    \@use_ftp cannot be 'true' while \@alt_snapshot_folder is neither NULL nor empty.    |
|    21147    |    16    |    No    |    The '%s' database is not published for merge replication.    |
|    21148    |    16    |    No    |    Both \@subscriber and \@subscriberdb must be specified with non-null values simultaneously, or both must be left unspecified.    |
|    21149    |    16    |    No    |    The '%s' database is not published for transactional or snapshot replication.    |
|    21150    |    16    |    No    |    Unable to determine the snapshot folder for the specified subscription because the specified Subscriber is not known to the Distributor.    |
|    21151    |    16    |    No    |    Pre- and post-snapshot commands are not supported for a publication that may support non-SQL Server Subscribers by using the character-mode bcp as the synchronization method.    |
|    21152    |    16    |    No    |    Cannot create a subscription of sync_type 'none' to a publication using the 'concurrent' or 'concurrent_c' synchronization method.    |
|    21153    |    16    |    No    |    Cannot create article '%s'. All articles that are part of a concurrent synchronization publication must use stored procedures to apply changes to the Subscriber.    |
|    21154    |    16    |    No    |    Cannot change article '%s'. All articles that are part of a concurrent synchronization publication must use stored procedures to apply changes to the Subscriber.    |
|    21155    |    16    |    No    |    Cannot change article '%s'. articles that are part of a concurrent synchronization publication can not have ins_cmd/del_cmd which exceeds %d characters .    |
|    21156    |    16    |    No    |    The \@status parameter value must be 'initiated' or 'active'.    |
|    21157    |    16    |    No    |    The snapshot compression option can be enabled only for a publication having an alternate snapshot generation folder defined.    |
|    21158    |    16    |    No    |    For a publication to be enabled for the Internet, the 'ftp_address' property must not be null.    |
|    21159    |    16    |    No    |    If a publication is enabled for the Internet, the 'alt_snapshot_folder' property must be non-empty.    |
|    21160    |    16    |    No    |    The 'ftp_port' property must be a non-negative integer < 65536.    |
|    21161    |    16    |    No    |    Could not change the Publisher because the subscription has been dropped. Use sp_subscription_cleanup to clean up the triggers.    |
|    21162    |    16    |    No    |    It is invalid to exclude the rowguid column for the table from the partition.    |
|    21163    |    16    |    No    |    It is not possible to add column '%s' to article '%s' because the snapshot for publication '%s' has been run.    |
|    21164    |    16    |    No    |    Column '%s' cannot be included in a vertical partition because it is neither nullable nor defined with a default value.    |
|    21165    |    16    |    No    |    Column '%s' cannot be excluded from a vertical partition because it is neither nullable nor defined with a default value.    |
|    21166    |    16    |    No    |    Column '%s' does not exist.    |
|    21167    |    16    |    No    |    The specified job ID does not represent a %s agent job for any push subscription in this database.    |
|    21168    |    16    |    No    |    Only members of the sysadmin fixed server role, members of the db_owner fixed database role, and owners of subscriptions served by the specified replication agent job can modify the agent offload settings.    |
|    21169    |    16    |    No    |    Could not identify the Publisher '%s' at the Distributor '%s'. Make sure that the server '%s' is registered at the Distributor.    |
|    21170    |    16    |    No    |    The specified Subscriber cannot use transformable subscriptions using Data Transformation Services. Only SQL Server 2000, SQL Server 2005, and OLE DB Subscribers can use transformable subscriptions.    |
|    21171    |    16    |    No    |    Could not find package '%s' in msdb at server '%s'.    |
|    21172    |    16    |    No    |    The publication has to be in 'character', 'concurrent_c', or 'database snapshot character' bcp mode to allow DTS.    |
|    21173    |    16    |    No    |    The publication has to be 'independent_agent type' to allow DTS.    |
|    21174    |    16    |    No    |    Because this publication allows transformable subscriptions using DTS, it requires autogenerated stored procedures and parameterized commands, which are set using default value for the \@status.    |
|    21175    |    16    |    No    |    You cannot change the ins_cmd, upd_cmd, or del_cmd article properties because the publication allows Data Transformation Services or updatable subscriptions.    |
|    21176    |    16    |    No    |    Only members of the sysadmin fixed server role, db_owner fixed database role, or the creator of the subscription can change the subscription properties.    |
|    21177    |    16    |    No    |    Could not create column list because it is too long. Create the list manually.    |
|    21178    |    16    |    No    |    Data Transformation Services (DTS) properties cannot be set because the publication does not allow transformable subscriptions using DTS. To allow transformable subscriptions, you must drop the publication and then and re-create it, specifying that transformable subscriptions are allowed.    |
|    21179    |    16    |    No    |    Invalid \@dts_package_location parameter value. Valid options are 'Distributor' or 'Subscriber'.    |
|    21180    |    16    |    No    |    A publication that allows DTS cannot be enabled for updatable subscriptions.    |
|    21181    |    16    |    No    |    \@dts_package_name can be set for push subscriptions only.    |
|    21182    |    16    |    No    |    The \@agent_type parameter must be one of 'distribution', 'merge', or NULL.    |
|    21183    |    16    |    No    |    Invalid property name '%s'.    |
|    21184    |    16    |    No    |    %s parameter is incorrect: it should be '%s', '%s' or '%s'.    |
|    21185    |    16    |    No    |    The subscription is not initialized or not created for failover mode operations.    |
|    21186    |    16    |    No    |    Subscription for Publisher '%s' does not have a valid queue_id.    |
|    21187    |    16    |    No    |    The current mode is the same as the requested mode.    |
|    21188    |    10    |    No    |    Changed update mode from [%s] to [%s].    |
|    21189    |    16    |    No    |    The queue for this subscription with queue_id = '%s' is not empty. Run the Queue Reader Agent to make sure the queue is empty before setting mode from [queued] to [immediate].    |
|    21190    |    10    |    No    |    Overriding queue check for setting mode from [%s] to [%s].    |
|    21192    |    16    |    No    |    MSrepl_tran_version column is a predefined column used for replication and can be only of data type uniqueidentifier    |
|    21193    |    16    |    No    |    \@identity_range, \@pub_identity_range, or \@threshold cannot be NULL when \@identityrangemanagementoption is set to AUTO.    |
|    21194    |    16    |    No    |    Cannot support identity range management because this table does not have an identity column.    |
|    21195    |    16    |    No    |    A valid identity range is not available. Check the data type of the identity column.    |
|    21196    |    16    |    No    |    Identity automation failed.    |
|    21197    |    16    |    No    |    Failed to allocate new identity range.    |
|    21198    |    16    |    No    |    Schema replication failed.    |
|    21199    |    16    |    No    |    This change cannot take effect until you run the snapshot again.    |
|    21200    |    16    |    No    |    Publication '%s' does not exist.    |
|    21201    |    16    |    No    |    Dropping a column that is being used by a merge filter clause is not allowed.    |
|    21202    |    16    |    No    |    It is not possible to drop column '%s' to article '%s' because the snapshot for publication '%s' has already been run.    |
|    21203    |    10    |    No    |    Duplicate rows found in %s. Unique index not created.    |
|    21204    |    16    |    No    |    The publication '%s' does not allow subscription copy or its subscription has not been synchronized.    |
|    21205    |    16    |    No    |    The subscription cannot be attached because the publication does not allow subscription copies to synchronize changes.    |
|    21206    |    16    |    No    |    Cannot resolve load hint for object %d because the object is not a user table.    |
|    21207    |    16    |    No    |    Cannot find source object ID information for article %d.    |
|    21208    |    16    |    No    |    This step failed because column '%s' exists in the vertical partition.    |
|    21209    |    16    |    No    |    This step failed because column '%s' does not exist in the vertical partition.    |
|    21210    |    16    |    No    |    The publication must be immediate_sync type to allow subscription copy.    |
|    21211    |    16    |    No    |    The database is attached from a subscription copy file without using sp_attach_subscription. Drop the database and reattach it using sp_attach_subscription.    |
|    21212    |    16    |    No    |    Cannot copy subscription. Only single file subscription databases are supported for this operation.    |
|    21213    |    16    |    No    |    Subscribers cannot subscribe to publications that allow DTS without using a DTS package.    |
|    21214    |    16    |    No    |    Cannot create file '%s' because it already exists.    |
|    21215    |    16    |    No    |    An alternate synchronization partner can be configured only at the Publisher.    |
|    21216    |    16    |    No    |    Publisher '%s', publisher database '%s', and publication '%s' are not valid synchronization partners.    |
|    21217    |    10    |    No    |    Publication of '%s' data from Publisher '%s'.    |
|    21218    |    16    |    No    |    The creation_script property cannot be NULL if a schema option of 0x0000000000000000 is specified for the article.    |
|    21219    |    16    |    No    |    The specified source object must be a stored procedure object if it is published as a 'proc schema only' type article.    |
|    21220    |    16    |    No    |    Unable to add the article '%s' because a snapshot has been generated for the publication '%s'.    |
|    21221    |    16    |    No    |    The specified source object must be a view object if it is going to be as a 'view schema only' type article.    |
|    21222    |    16    |    No    |    The schema options available for a procedure, function, synonym, or aggregate schema article are: 0x00000001, 0x00000020, 0x00001000, 0x00002000, 0x00400000, 0x02000000, 0x08000000, 0x10000000, 0x20000000, 0x40000000, and 0x80000000.    |
|    21223    |    16    |    No    |    The \@pre_creation_command parameter for a schema only article must be either 'none' or 'drop'.    |
|    21224    |    16    |    No    |    '%s' is not a valid property for a schema only article.    |
|    21225    |    16    |    No    |    The 'offload_server' property cannot be NULL or empty if the pull subscription agent is to be enabled for remote activation.    |
|    21226    |    16    |    No    |    The database '%s' does not have a pull subscription to the specified publication.    |
|    21227    |    16    |    No    |    The 'offload_server' property cannot be the same as the Subscriber server name.    |
|    21228    |    16    |    No    |    The specified source object must be a user-defined function object if it is going to be published as a 'func schema only' type article.    |
|    21229    |    16    |    No    |    The schema options available for a view schema article are: 0x00000001, 0x00000010, 0x00000020, 0x00000040, 0x00000100, 0x00001000, 0x00002000, 0x00040000, 0x00100000, 0x00200000, 0x00400000, 0x00800000, 0x01000000, 0x08000000, 0x40000000, and 0x80000000.    |
|    21230    |    16    |    No    |    Do not call this stored procedure for schema change because the current database is not enabled for replication.    |
|    21231    |    16    |    No    |    Automatic identity range support is useful only for publications that allow updating subscribers.    |
|    21232    |    16    |    No    |    Identity range values must be positive integers that are greater than 1.    |
|    21233    |    16    |    No    |    Threshold value must be from 1 through 100.    |
|    21234    |    16    |    No    |    Cannot use the INSERT command because the table has an identity column. The insert custom stored procedure must be used to set 'identity_insert' settings at the Subscriber.    |
|    21235    |    16    |    No    |    Article property '%s' can be set only when the article uses automatic identity range management.    |
|    21236    |    16    |    No    |    The subscription(s) to Publisher '%s' does not allow subscription copy or it has not been synchronized.    |
|    21237    |    16    |    No    |    There is a push subscription to Publisher '%s'. Only pull and anonymous subscriptions can be copied.    |
|    21238    |    16    |    No    |    This database either is a publisher, or there is a push subscription to publication '%s'. Only pull and anonymous subscriptions can be copied.    |
|    21239    |    16    |    No    |    Cannot copy subscriptions because there is no synchronized subscription found in the database.    |
|    21240    |    16    |    No    |    The table '%s' is already published as another article with a different automatic identity support option.    |
|    21241    |    16    |    No    |    The threshold value should be from 1 through 100.    |
|    21242    |    16    |    No    |    Conflict table for article '%s' could not be created successfully.    |
|    21243    |    16    |    No    |    Publisher '%s', publication database '%s', and publication '%s' could not be added to the list of synchronization partners.    |
|    21244    |    16    |    No    |    Character mode publication does not support vertical filtering when the base table does not support column-level tracking.    |
|    21245    |    16    |    No    |    Table '%s' is not part of publication '%s'.    |
|    21246    |    16    |    No    |    This step failed because table '%s' is not part of any publication.    |
|    21247    |    16    |    No    |    Cannot create file at '%s'. Ensure the file path is valid.    |
|    21248    |    16    |    No    |    Cannot attach subscription file '%s'. Ensure the file path is valid and the file is updatable.    |
|    21249    |    16    |    No    |    OLE DB or ODBC Subscribers cannot subscribe to article '%s' in publication '%s' because the article has a timestamp column and the publication is 'allow_queued_tran' (allows queued updating subscriptions).    |
|    21250    |    16    |    No    |    Primary key column '%s' cannot be excluded from a vertical partition.    |
|    21251    |    16    |    No    |    Publisher '%s', publisher database '%s', publication '%s' could not be removed from the list of synchronization partners.    |
|    21252    |    16    |    No    |    It is invalid to remove the default Publisher '%s', publication database '%s', and publication '%s' from the list of synchronization partners    |
|    21253    |    16    |    No    |    Parameter '\@add_to_active_directory' cannot be set to TRUE because Active Directory client package is not installed properly on the machine where SQL Server is running.    |
|    21254    |    16    |    No    |    The Active Directory operation on publication '%s' could not be completed because Active Directory client package is not installed properly on the machine where SQL Server is running.    |
|    21255    |    16    |    No    |    Column '%s' already exists in table '%s'.    |
|    21256    |    16    |    No    |    A column used in filter clause '%s' either does not exist in the table '%s' or cannot be excluded from the current partition.    |
|    21257    |    16    |    No    |    Invalid property '%s' for article '%s'.    |
|    21258    |    16    |    No    |    You must first drop all existing merge publications to add an anonymous or local subscription to database '%s'.    |
|    21259    |    16    |    No    |    Invalid property value '%s'. See SQL Server Books Online for a list of valid parameters for sp_changemergearticle.    |
|    21260    |    16    |    No    |    Schema replication failed because database '%s' on server '%s' is not the original Publisher of table '%s'.    |
|    21261    |    16    |    No    |    The offload server must be specified if the agent for this subscription is to be offloaded for remote execution.    |
|    21262    |    16    |    No    |    Failed to drop column '%s' from the partition because a computed column is accessing it.    |
|    21263    |    16    |    No    |    Parameter '%s' cannot be NULL or an empty string.    |
|    21264    |    16    |    No    |    Column '%s' cannot be dropped from table '%s' because it is a primary key column.    |
|    21265    |    16    |    No    |    Column '%s' cannot be dropped from table '%s' because there is a unique index accessing this column.    |
|    21266    |    16    |    No    |    Cannot publish table '%s' for both a merge publication and a publication with the updatable subscribers option.    |
|    21267    |    10    |    No    |    Invalid value for queue type was specified. Valid values = (%s).    |
|    21268    |    10    |    No    |    Cannot change the parameter %s while there are subscriptions to the publication.    |
|    21269    |    16    |    No    |    Cannot add a computed column or a timestamp column to a vertical partition for a character mode publication.    |
|    21270    |    10    |    No    |    Queued snapshot publication property '%s' cannot have the value '%s'.    |
|    21272    |    16    |    No    |    Cannot clean up the meta data for publication '%s' because other publications are using one or more articles in this publication.    |
|    21273    |    16    |    No    |    You must upgrade the Subscriber to SQL Server 2000 to create updatable subscriptions when the Publisher is SQL Server 2000 or higher.    |
|    21274    |    16    |    No    |    Invalid publication name '%s'.    |
|    21275    |    16    |    No    |    Cannot publish the schema-bound view '%ls'. The value specified for the \@type parameter must be "indexed view schema only" (for snapshot or transactional replication) or "indexed view logbased" (for transactional replication only).    |
|    21276    |    16    |    No    |    The type must be 'table' or '( view \| indexed view \| proc \| func ) schema only'.    |
|    21277    |    16    |    No    |    Cannot publish the source object '%ls'. The value specified for the \@type parameter ("indexed view schema only" or "indexed view logbased") can be used only for indexed views. Either specify a value of "view schema only" for the \@type parameter, or modify the view to be schema bound with a unique clustered index.    |
|    21278    |    16    |    No    |    Cannot publish the source object '%ls'. The value specified for the \@type parameter ("indexed view logbased") requires that the view be schema bound with a unique clustered index. Either specify a value of "view schema only" for the \@type parameter, or modify the view to be schema bound with a unique clustered index.    |
|    21279    |    16    |    No    |    The 'schema_option' property for a merge article cannot be changed after a snapshot is generated for the publication. To change the 'schema_option' property of this article the corresponding merge publication must be dropped and re-created.    |
|    21280    |    16    |    No    |    Publication '%s' cannot be subscribed to by Subscriber database '%s' because it contains one or more articles that have been subscribed to by the same Subscriber database at transaction level.    |
|    21281    |    16    |    No    |    Publication '%s' cannot be subscribed to by Subscriber database '%s' because it contains one or more articles that have been subscribed to by the same Subscriber database at merge level.    |
|    21282    |    16    |    No    |    \@identity_range, \@pub_identity_range, and \@threshold must be NULL when \@identityrangemanagementoption is set to 'none' or 'manual'.    |
|    21283    |    16    |    No    |    Column '%s' of table '%s' cannot be excluded from a vertical partition because there is a computed column that depends on it.    |
|    21284    |    16    |    No    |    Failed to drop column '%s' from table '%s'.    |
|    21285    |    16    |    No    |    Failed to add column '%s' to table '%s'.    |
|    21286    |    16    |    No    |    Conflict table '%s' does not exist.    |
|    21287    |    16    |    No    |    The specified \@destination_folder is not a valid path of an existing folder.    |
|    21288    |    16    |    No    |    Could not create the snapshot directory structure in the specified \@destination_folder.    |
|    21289    |    16    |    No    |    Either the snapshot files have not been generated or they have been cleaned up.    |
|    21290    |    16    |    No    |    The identity range value provided has exceeded the maximum value allowed.    |
|    21291    |    16    |    No    |    The specified automatic identity support parameters conflict with the settings in another article.    |
|    21292    |    16    |    No    |    Object '%s' cannot be published twice in the same publication.    |
|    21293    |    10    |    No    |    Warning: adding updatable subscription for article '%s' may cause data inconsistency as the source table is already subscribed to '%s'    |
|    21294    |    16    |    No    |    Either \@publisher (and \@publisher_db) or \@subscriber (and \@subscriber_db) must be specified, but both cannot be specified.    |
|    21295    |    16    |    No    |    Publication '%s' does not contain any article that uses automatic identity range management.    |
|    21296    |    16    |    No    |    Parameter \@resync_type must be either 0, 1, 2.    |
|    21297    |    16    |    No    |    Invalid resync type. No validation has been performed for this subscription.    |
|    21298    |    16    |    No    |    Failed to resynchronize this subscription.    |
|    21299    |    16    |    No    |    Invalid Subscriber partition validation expression '%s'.    |
|    21300    |    10    |    No    |    The resolver information was specified without specifying the resolver to be used for article '%s'. The default resolver will be used.    |
|    21301    |    16    |    No    |    The resolver information should be specified while using the '%s' resolver.    |
|    21302    |    16    |    No    |    The resolver information should specify a column with data type, datetime, or smalldatetime while using the '%s' resolver.    |
|    21303    |    16    |    No    |    The article '%s' should enable column tracking to use the '%s' resolver. The default resolver will be used to resolve conflicts on this article.    |
|    21304    |    16    |    No    |    The merge triggers could not be created on the table '%s'.    |
|    21305    |    16    |    No    |    The schema change information could not be updated at the subscription database.    |
|    21306    |    16    |    No    |    The copy of the subscription could not be made because the subscription to publication '%s' has expired.    |
|    21307    |    16    |    No    |    The subscription could not be attached because the subscription to publication '%s' has expired.    |
|    21308    |    10    |    No    |    Rowcount validation profile.    |
|    21309    |    10    |    No    |    Profile used by the Merge Agent to perform rowcount validation.    |
|    21310    |    10    |    No    |    Rowcount and checksum validation profile.    |
|    21311    |    10    |    No    |    Profile used by the Merge Agent to perform rowcount and checksum validation.    |
|    21312    |    10    |    No    |    Cannot change this publication property because there are active subscriptions to this publication.    |
|    21313    |    10    |    No    |    Subscriber partition validation expression must be NULL for static publications.    |
|    21314    |    10    |    No    |    There must be one and only one of '%s' and '%s' that is not NULL.    |
|    21315    |    10    |    No    |    Failed to adjust Publisher identity range for table '%s'.    |
|    21316    |    10    |    No    |    Failed to adjust Publisher identity range for publication '%s'.    |
|    21317    |    10    |    No    |    A push subscription to the publication '%s' already exists. Use sp_mergesubscription_cleanup to drop defunct push subscriptions.    |
|    21318    |    10    |    No    |    Table '%s' must have at least one column that is included in the vertical partition.    |
|    21319    |    16    |    No    |    Could not find the Snapshot Agent command line for the specified publication. Check that a valid regular snapshot job exists on the distributor.    |
|    21320    |    16    |    No    |    The version of the Distributor cannot be lower than the version of the Publisher.    |
|    21321    |    16    |    No    |    The parameter \@dynamic_snapshot_location cannot be an empty string.    |
|    21322    |    16    |    No    |    This publication logs conflicts on both replicas. Pre-SQL Server 2005 subscribers will not honor this setting.    |
|    21323    |    16    |    No    |    A dynamic snapshot job can be scheduled only for a publication with dynamic filtering enabled.    |
|    21324    |    16    |    No    |    A Snapshot Agent must be added for the specified publication before a dynamic snapshot job can be scheduled.    |
|    21325    |    16    |    No    |    Could not find the Snapshot Agent ID for the specified publication.    |
|    21326    |    16    |    No    |    Could not find the dynamic snapshot job with a '%ls' of '%ls' for the specified publication.    |
|    21327    |    16    |    No    |    '%ls' is not a valid dynamic snapshot job name.    |
|    21328    |    16    |    No    |    The specified dynamic snapshot job name '%ls' is already in use. Try the operation again with a different job name.    |
|    21329    |    16    |    No    |    Only one of the parameters, \@dynamic_snapshot_jobid or \@dynamic_snapshot_jobname, can be specified with a nondefault value.    |
|    21330    |    16    |    No    |    Cannot create a sub-directory under the snapshot folder (%ls). Ensure that there is enough disk space available, and that the account under which the Snapshot Agent runs has permissions to create a sub-directory under the snapshot folder.    |
|    21331    |    16    |    No    |    Cannot copy user script file to the snapshot folder at the Distributor (%ls). Ensure that there is enough disk space available, and that the account under which the Snapshot Agent runs has permissions to write to the snapshot folder and its subdirectories.    |
|    21332    |    16    |    No    |    Failed to retrieve information about the publication : %ls. Check the name again.    |
|    21333    |    16    |    No    |    A generation that was expected to be in %s.dbo.MSmerge_genhistory could not be found. If this error occurred in a subscription database, reinitialize the subscription. If this error occurred in a publication database, restore the database from a backup.    |
|    21334    |    16    |    No    |    Cannot initialize Message Queuing-based subscription because the platform is not Message Queuing %s compliant    |
|    21335    |    16    |    No    |    Warning: column '%s' already exists in the vertical partition.    |
|    21336    |    16    |    No    |    Warning: column '%s' does not exist in the vertical partition.    |
|    21337    |    16    |    No    |    Invalid \@subscriber_type value. Valid options are 'local' and 'global'.    |
|    21338    |    16    |    No    |    Cannot execute sp_dropmergearticle if the publication has a Subscriber that is running on a version of SQL Server 2000 or earlier. Drop and re-create the publication without the article '%s' or set the publication compatibility level of publication '%s' to '90RTM' before calling sp_dropmergearticle.    |
|    21339    |    10    |    No    |    Warning: the publication uses a feature that is only supported only by subscribers running '%s' or higher.    |
|    21340    |    16    |    No    |    On Demand user script cannot be applied to the snapshot publication.    |
|    21341    |    16    |    No    |    \@dynamic_snapshot_location cannot be a non-empty string while \@alt_snapshot_folder is neither empty nor null.    |
|    21342    |    16    |    No    |    \@dynamic_snapshot_location cannot be a non-empty string while \@use_ftp is 'true'.    |
|    21343    |    16    |    No    |    Could not find stored procedure '%s'.    |
|    21344    |    16    |    No    |    Invalid value specified for %ls parameter.    |
|    21345    |    16    |    No    |    Excluding the last column in the partition is not allowed.    |
|    21346    |    16    |    No    |    Failed to change the owner of '%s' to '%s'.    |
|    21347    |    16    |    No    |    Column '%s' cannot be excluded from the vertical partitioning because there is a unique index accessing this column.    |
|    21348    |    16    |    No    |    Invalid property name '%s'.    |
|    21349    |    10    |    No    |    Warning: only Subscribers running SQL Server 7.0 Service Pack 2 or later can synchronize with publication '%s' because decentralized conflict logging is designated.    |
|    21350    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because a compressed snapshot is used.    |
|    21351    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because vertical filters are being used.    |
|    21352    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because schema replication is performed.    |
|    21353    |    10    |    No    |    Warning: only Subscribers running SQL Server 7.0 Service Pack 2 or later can synchronize with publication '%s' because publication wide reinitialization is performed.    |
|    21354    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because publication wide reinitialization is performed.    |
|    21355    |    10    |    No    |    Warning: only Subscribers running SQL Server 7.0 Service Pack 2 or later can synchronize with publication '%s' because merge metadata cleanup task is performed.    |
|    21356    |    10    |    No    |    Warning: only Subscribers running SQL Server 7.0 Service Pack 2 or later can synchronize with publication '%s' because publication wide validation task is performed.    |
|    21357    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because data types new in SQL Server 2000 exist in one of its articles.    |
|    21358    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because at least one timestamp column exists in one of its articles.    |
|    21359    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because automatic identity ranges are being used.    |
|    21360    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because a new article has been added to the publication after its snapshot has been generated.    |
|    21361    |    16    |    No    |    The specified \@agent_jobid is not a valid job id for a '%s' agent job.    |
|    21362    |    16    |    No    |    Merge filter '%s' does not exist.    |
|    21363    |    16    |    No    |    Failed to add publication '%s' to Active Directory. %s    |
|    21364    |    16    |    No    |    Could not add article '%s' because a snapshot is already generated. Set \@force_invalidate_snapshot to 1 to force this and invalidate the existing snapshot.    |
|    21365    |    16    |    No    |    Could not add article '%s' because there are active subscriptions. Set \@force_reinit_subscription to 1 to force this and reinitialize the active subscriptions.    |
|    21366    |    16    |    No    |    Could not add filter '%s' because a snapshot is already generated. Set \@force_invalidate_snapshot to 1 to force this and invalidate the existing snapshot.    |
|    21367    |    16    |    No    |    Could not add filter '%s' because there are active subscriptions. Set \@force_reinit_subscription to 1 to force this and reinitialize the active subscriptions.    |
|    21368    |    16    |    No    |    The specified offload server name contains the invalid character '%s'.    |
|    21369    |    16    |    No    |    Could not remove publication '%s' from Active Directory.    |
|    21370    |    16    |    No    |    The resync date specified '%s' is not a valid date.    |
|    21371    |    10    |    No    |    Could not propagate the change on publication '%s' to Active Directory.    |
|    21372    |    16    |    No    |    Cannot drop filter '%s' from publication '%s' because its snapshot has been run and this publication could have active subscriptions. Set \@force_reinit_subscription to 1 to reinitialize all subscriptions and drop the filter.    |
|    21373    |    11    |    No    |    Could not open database %s. Replication settings and system objects could not be upgraded. If the database is used for replication, run sp_vupgrade_replication in the [master] database when the database is available.    |
|    21374    |    10    |    No    |    Upgrading distribution settings and system objects in database %s.    |
|    21375    |    10    |    No    |    Upgrading publication settings and system objects in database %s.    |
|    21376    |    11    |    No    |    Could not open database %s. Replication settings and system objects could not be upgraded. If the database is used for replication, run sp_vupgrade_replication in the [master] database when the database is available.    |
|    21377    |    10    |    No    |    Upgrading subscription settings and system objects in database %s.    |
|    21378    |    16    |    No    |    Could not open distribution database %s because it is offline or being recovered. Replication settings and system objects could not be upgraded. Be sure this database is available and run sp_vupgrade_replication again.    |
|    21379    |    16    |    No    |    Cannot drop article '%s' from publication '%s' because a snapshot is already generated. Set \@force_invalidate_snapshot to 1 to force this and invalidate the existing snapshot.    |
|    21380    |    16    |    No    |    Cannot add timestamp column without forcing reinitialization. Set \@force_reinit_subscription to 1 to force reinitialization.    |
|    21381    |    16    |    No    |    Cannot add (drop) column to table '%s' because the table belongs to publication(s) with an active updatable subscription. Set \@force_reinit_subscription to 1 to force reinitialization.    |
|    21382    |    16    |    No    |    Cannot drop filter '%s' because a snapshot is already generated. Set \@force_invalidate_snapshot to 1 to force this and invalidate the existing snapshot.    |
|    21383    |    16    |    No    |    Cannot enable a merge publication on this server because the working directory of its Distributors is not using a UNC path.    |
|    21384    |    16    |    No    |    The specified subscription does not exist or has not been synchronized yet.    |
|    21385    |    16    |    No    |    Snapshot failed to process publication '%s'. Possibly due to active schema change activity or new articles being added.    |
|    21386    |    16    |    No    |    Schema change failed on object '%s'. Possibly due to active snapshot or other schema change activity.    |
|    21387    |    16    |    No    |    The expanded dynamic snapshot view definition of one of the articles exceeds the system limit of 3499 characters. Consider using the default mechanism instead of the dynamic snapshot for initializing the specified subscription.    |
|    21388    |    10    |    No    |    The concurrent snapshot for publication '%s' is not available because it has not been fully generated or the Log Reader Agent is not running to activate it. If generation of the concurrent snapshot was interrupted, the Snapshot Agent for the publication must be restarted until a complete snapshot is generated.    |
|    21389    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because column-level collation is scripted out with the article schema creation script.    |
|    21390    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because extended properties are scripted out with the article schema creation script.    |
|    21391    |    10    |    No    |    Warning: only Subscribers running SQL Server 2000 or later can synchronize with publication '%s' because it contains schema-only articles.    |
|    21392    |    16    |    No    |    Row filter(%s) is invalid for column partition(%s) for article '%s' in publication '%s'.    |
|    21393    |    16    |    No    |    Dropping row filter(%s) for article '%s' in '%s'. Reissue sp_articlefilter and sp_articleview to create a row filter.    |
|    21394    |    16    |    No    |    Invalid schema option specified for publication that allows updating subscribers. Need to set the schema option to include DRI constraints.    |
|    21395    |    10    |    No    |    This column cannot be included in a transactional publication because the column ID is greater than 255.    |
|    21396    |    16    |    No    |    The value specified for the \@type parameter of sp_addsubscriber or the \@subscriber_type parameter of sp_addsubscription is not valid. See SQL Server Books Online for a list of valid values.    |
|    21397    |    16    |    No    |    The transactions required for synchronizing the nosync subscription created from the specified backup are unavailable at the Distributor. Retry the operation again with a more up-to-date log, differential, or full database backup.    |
|    21398    |    16    |    No    |    Could not complete setting up the no-sync subscription at the Distributor while the distribution cleanup agent is running. The operation has a greater chance of success if the distribution cleanup agent is temporarily disabled.    |
|    21399    |    16    |    No    |    The transactions required for synchronizing the subscription with the specified log sequence number (LSN) are unavailable at the Distributor. Specify a higher LSN.    |
|    21400    |    16    |    No    |    Article property must be changed at the original Publisher of article '%s'.    |
|    21401    |    16    |    No    |    Article name cannot be 'all'.    |
|    21402    |    16    |    No    |    Incorrect value for parameter '%s'.    |
|    21403    |    10    |    No    |    The 'max_concurrent_dynamic_snapshots' publication property must be greater than or equal to zero.    |
|    21404    |    10    |    No    |    '%s' is not a valid value for the '%s' parameter. The value must be a positive integer greater than 300 or 0.    |
|    21405    |    10    |    No    |    '%s' is not a valid value for the '%s' parameter. The value must be an integer greater than or equal to %d.    |
|    21406    |    10    |    No    |    '%s' is not a valid value for the '%s' parameter. The value must be 0 or 1.    |
|    21407    |    16    |    No    |    Cannot create the subscription. If you specify a value of "initialize with backup" for the \@sync_type parameter, you must subscribe to all articles in the publication by specifying a value of "all" for the \@article parameter.    |
|    21408    |    16    |    No    |    Cannot create the subscription. You must specify a value of "Active" or "Subscribed" for the \@status parameter. This is because the value specified for the \@sync_type parameter is "initialize with backup" or "replication support only".    |
|    21409    |    16    |    No    |    Only one of parameters %s and %s can be set.    |
|    21410    |    16    |    No    |    Snapshot Agent startup message.    |
|    21411    |    16    |    No    |    Distribution Agent startup message.    |
|    21412    |    16    |    No    |    Merge Agent startup message.    |
|    21413    |    16    |    No    |    Failed to acquire the application lock indicating the front of the queue.    |
|    21414    |    10    |    No    |    Unexpected failure acquiring an application lock. Ensure that the account under which the Merge Agent runs is a member of the publication access list. If there is a lot of activity on the server, run the Merge Agent when there are more server resources available.    |
|    21415    |    10    |    No    |    Unexpected failure releasing an application lock. Ensure that the account under which the Merge Agent runs is a member of the publication access list. If there is a lot of activity on the server, run the Merge Agent when there are more server resources available.    |
|    21416    |    10    |    No    |    Property '%s' of article '%s' cannot be changed.    |
|    21417    |    10    |    No    |    Having a queue timeout value of over 12 hours is not allowed.    |
|    21419    |    10    |    No    |    Filter '%s' of article '%s' cannot be changed.    |
|    21420    |    10    |    No    |    Subscription property '%s' cannot be changed.    |
|    21421    |    10    |    No    |    Article '%s' cannot be dropped because there are other articles using it as a join article.    |
|    21422    |    16    |    No    |    Queue Reader Agent startup message.    |
|    21423    |    16    |    No    |    Either the publication '%s' does not exist or you do not have sufficient permissions to access it. Ensure that the publication exists and that the account under which the Merge Agent connects to the Publisher is included in the publication access list (PAL).    |
|    21424    |    16    |    No    |    The \@publisher parameter must be NULL for SQL Server publishers.    |
|    21425    |    16    |    No    |    The \@publisher parameter may not be NULL for heterogeneous publishers.    |
|    21426    |    16    |    No    |    No shared agent subscription exists for publication '%s' and the subscriber/subscriber database pair '%s'/'%s'.    |
|    21450    |    16    |    No    |    The replication %s could not be upgraded for %s databases. Ensure that %s is upgraded and execute %s again.    |
|    21451    |    16    |    No    |    The %s %s (%s) login (%s) password has been changed.    |
|    21452    |    10    |    No    |    Warning: The %s agent job has been implicitly created and will run under the SQL Server Agent Service Account.    |
|    21454    |    16    |    No    |    The internal procedure sp_MStran_is_snapshot_required must be executed at the Distributor if the \@run_at_distributor parameter has a value of 1. If the problem persists, contact Microsoft Customer Support Services.    |
|    21456    |    16    |    No    |    Value provided for parameter %s is not valid.    |
|    21460    |    16    |    No    |    The primary key for source table "%s" includes the timestamp column "%s". Cannot create the article for the specified publication because it allows updating Subscribers.    |
|    21481    |    16    |    No    |    Cannot create replication subscription(s) in the master database. Choose another database for creating subscriptions.    |
|    21482    |    16    |    No    |    %s can only be executed in the "%s" database.    |
|    21484    |    16    |    No    |    Article validation was requested for snapshot publication "%s". Article validation is only valid for transactional publications.    |
|    21485    |    16    |    No    |    Tracer tokens cannot be posted for a snapshot publication.    |
|    21486    |    16    |    No    |    An error occurred while logging the tracer token history information. The tracer token could not be posted.    |
|    21487    |    16    |    No    |    An error occurred while inserting the tracer token to the log. The tracer token could not be posted.    |
|    21488    |    16    |    No    |    No active subscriptions were found. The publication must have active subscriptions in order to post a tracer token.    |
|    21489    |    16    |    No    |    A database '%s' already exists. If you intend this to be your distribution database set \@existing_db = 1.    |
|    21490    |    16    |    No    |    The value specified for the %s parameter of sp_mergearticlecolumn must be '%s'. A value of 'true' is allowed only when this procedure is called by another replication procedure. Either set the value of the \@schema_replication parameter to 'false' or do not specify a value.    |
|    21499    |    16    |    No    |    The procedure %s failed to %s the resource %s. Server error = %d.    |
|    21500    |    10    |    No    |    Invalid subscription type is specified. A subscription to publication '%s' already exists in the database with a different subscription type.    |
|    21501    |    10    |    No    |    The supplied resolver information does not specify a valid column name to be used for conflict resolution by '%s'.    |
|    21502    |    10    |    No    |    The publication '%s' does not allow the subscription to synchronize to an alternate synchronization partner.    |
|    21503    |    10    |    No    |    Cleanup of merge meta data cannot be performed while merge processes are running. Retry this operation after the merge processes have completed.    |
|    21504    |    10    |    No    |    Cleanup of merge meta data at republisher '%s'.'%s' could not be performed because merge processes are propagating changes to the republisher. All subscriptions to this republisher must be reinitialized.    |
|    21505    |    10    |    No    |    Changes to publication '%s' cannot be merged because it has been marked inactive.    |
|    21506    |    10    |    No    |    sp_mergecompletecleanup cannot be executed before sp_mergepreparecleanup is executed. Use sp_mergepreparecleanup to initiate the first phase of merge meta data cleanup.    |
|    21507    |    10    |    No    |    All prerequisites for cleaning up merge meta data have been completed. Execute sp_mergecompletecleanup to initiate the final phase of merge meta data cleanup.    |
|    21508    |    10    |    No    |    Cleanup of merge meta data cannot be performed while merge processes are running. Cleanup will proceed after the merge processes have completed.    |
|    21509    |    10    |    No    |    Cleanup of merge meta data cannot be performed because some republishers have not quiesced their changes. Cleanup will proceed after all republishers have quiesced their changes.    |
|    21510    |    10    |    No    |    Data changes are not allowed while cleanup of merge meta data is in progress.    |
|    21511    |    10    |    No    |    Neither MSmerge_contents nor MSmerge_tombstone contain meta data for this row.    |
|    21512    |    18    |    No    |    %ls: The %ls parameter is shorter than the minimum required size.    |
|    21514    |    16    |    No    |    Cannot complete the requested operation in the subscription database because a snapshot is currently being delivered to the database. Perform the operation again at a later time. To stop the delivery of the snapshot, stop the Distribution Agent or Merge Agent associated with the subscription.    |
|    21515    |    18    |    No    |    Replication custom procedures will not be scripted because the specified publication '%s' is a snapshot publication.    |
|    21516    |    10    |    No    |    Transactional replication custom procedures for publication '%s' from database '%s':    |
|    21517    |    10    |    No    |    Replication custom procedures will not be scripted for article '%s' because the auto-generate custom procedures schema option is not enabled.    |
|    21518    |    10    |    No    |    Replication custom procedures for article '%s':    |
|    21519    |    10    |    No    |    Custom procedures will not be scripted for article update commands based on direct INSERT, UPDATE, or DELETE statements.    |
|    21520    |    10    |    No    |    Custom procedure will not be scripted because '%s' is not a recognized article update command syntax.    |
|    21521    |    16    |    No    |    Some generation values are above the upper limit of %d used in SQL Server 2000. Change the publication_compatibility_level of the publication to 90 to make this work.    |
|    21522    |    16    |    No    |    This article cannot use the '%s' feature because the publication compatibility level is less than 90. Use sp_changemergepublication to set the publication_compatibility_level of publication '%s' to '90RTM'.    |
|    21523    |    16    |    No    |    Adding column '%s' to table '%s' failed. Articles can have at most %d columns, including columns that have been filtered.    |
|    21525    |    16    |    No    |    A lightweight replica must be anonymous.    |
|    21526    |    16    |    No    |    Article '%s' already belongs to a subscription with a different value for the \@lightweight property.    |
|    21527    |    16    |    No    |    Publication '%s' cannot be added to database '%s', because a publication with a lower compatibility level already exists. All merge publications in a database must have the same compatibility level.    |
|    21528    |    16    |    No    |    Publication '%s' cannot be added to database '%s', because a publication with a higher compatibility level already exists. All merge publications in a database must have the same compatibiliy level.    |
|    21530    |    10    |    No    |    The schema change failed during execution of an internal replication procedure. For corrective action, see the other error messages that accompany this error message.    |
|    21531    |    10    |    No    |    The data definition language (DDL) command cannot be executed at the Subscriber. DDL commands can only be executed at the Publisher. In a republishing hierarchy, DDL commands can only be executed at the root Publisher, not at any of the republishing Subscribers.    |
|    21532    |    10    |    No    |    Cannot add a data definition language trigger for replicating '%.*ls' events.    |
|    21533    |    10    |    No    |    Cannot insert information into the schema change tracking table sysmergeschemachange.    |
|    21535    |    16    |    No    |    The article '%s' is already published in another publication, and is set to use nonoverlapping partitions with multiple subscribers per partition (\@partition_options = 2). This setting does not permit the article to be included in more than one publication.    |
|    21537    |    16    |    No    |    The column '%s' in table '%s' is involved in a foreign key relationship with a column in table '%s', but this column was not found in the specified join clause. A logical record relationship between these tables should include this column.    |
|    21538    |    16    |    No    |    Table '%s' cannot have table '%s' as a parent in a logical record relationship because it already has a different parent table. A logical record relationship allows only one parent table for a given child table.    |
|    21539    |    16    |    No    |    A logical record relationship, specified by the \@filter_type parameter, requires a one-to-one or a one-to-many join from the parent table to the child table. Either change the value of the \@filter_type parameter, or set the \@join_unique_key parameter to 1.    |
|    21540    |    16    |    No    |    You cannot drop a column defined as data type uniqueidentifier with the rowguidcol property because merge replication uses this column for tracking. To drop the column, you must first drop the table from all publications and subscriptions.    |
|    21541    |    16    |    No    |    Cannot complete ALTER TABLE command. Do not execute the command 'ALTER TABLE table_name DISABLE TRIGGER ALL' on a published table. Reissue multiple 'ALTER TABLE table_name DISABLE TRIGGER trigger_name' statements to disable each trigger individually on the given table.    |
|    21542    |    16    |    No    |    Encountered server error %d while executing <%s>.    |
|    21543    |    16    |    No    |    The schema for the article %s was either not generated properly or was not applied properly during initial synchronization. This may be due to permissions issues. Verify whether the object exists, and whether the necessary permissions are granted.    |
|    21544    |    10    |    No    |    Value specified for the publication property replicate_ddl is not valid. The value must be 1 or 0.    |
|    21545    |    16    |    No    |    You cannot disable a trigger used by merge replication on a published table. To drop the trigger, drop the table from the publication.    |
|    21546    |    16    |    No    |    Cannot replicate the ALTER TABLE command. It contains multiple DROP commands, including a DROP command for a column that is not included in all subscriptions to this article. Use a single DROP command in each ALTER TABLE command.    |
|    21547    |    16    |    No    |    Encountered server error %d while restoring the log for database %s.    |
|    21548    |    16    |    No    |    Cannot execute sp_change_subscription_properties. This stored procedure can only be used for publications that have at least one pull subscription.    |
|    21549    |    16    |    No    |    Cannot add the computed column '%s' to the publication. You must first add all columns on which this column depends; you cannot filter any of these colunmns from the article.    |
|    21550    |    16    |    No    |    Before you drop the column from the publication, all computed columns that depend on column '%s' must be dropped.    |
|    21551    |    16    |    No    |    Only members of the sysadmin or db_owner or db_ddladmin roles can perform this operation.    |
|    21552    |    16    |    No    |    Merge data definition language (DDL) error: Dropping a column used in a row filter or a join filter is not allowed. To drop a column used in a row filter, first change the row filter using sp_changemergearticle. To drop a column used in a join filter, first drop the join filter using sp_dropmergefilter.    |
|    21561    |    16    |    No    |    The value specified for parameter %s = %d is not valid.    |
|    21564    |    16    |    No    |    The table %s contains the column msrepl_tran_version, which is used by replication. The column is set to NULL, but it must be set to NOT NULL. Replication failed to alter this column, so you must drop the column and then add the table as an article again using sp_addarticle. Replication will then add the column to the table.    |
|    21567    |    16    |    No    |    The call format VCALL cannot be used for the specified article. VCALL format can be used only for articles in publications that allow updating subscriptions. If you do not require updating subscriptions, specify a different call format. If you do require updating subscriptions, you must drop the publication and re-create it to specify that updating subscriptions are allowed.    |
|    21569    |    16    |    No    |    The article %s in the publication %s does not have a valid conflict table entry in the system table sysarticleupdates. This entry is required for publications that allow queued updating subscriptions. Check for errors in the last run of the Snapshot Agent.    |
|    21570    |    16    |    No    |    Cannot create the logical record relationship. Table '%s' does not have a foreign key referencing table '%s'. A logical record relationship requires a foreign key relationship between the parent and child tables.    |
|    21571    |    16    |    No    |    Cannot create the logical record relationship in publication '%s'. The use_partition_groups option for the publication must be set to "true" in order to use logical records. Use sp_changemergepublication to set the option to "true".    |
|    21572    |    16    |    No    |    Cannot add a logical record relationship because the foreign key constraint '%s' on table '%s' is disabled. To create the logical record relationship, first enable the foreign key constraint.    |
|    21573    |    16    |    No    |    Cannot add a logical record relationship because the foreign key constraint '%s' on table '%s' is defined with the NOT FOR REPLICATION option. To add the logical record relationship, first drop the foreign key constraint, and then re-create it without the NOT FOR REPLICATION option.    |
|    21574    |    16    |    No    |    Cannot add a logical record relationship because the article '%s' is published in publication '%s', which has a compatibility level lower than 90RTM. Use sp_changemergepublication to set the publication_compatibility_level to 90RTM.    |
|    21575    |    16    |    No    |    The value specified for the property filter_type is not valid. Valid values are 1 (join filter only), 2 (logical record relation only), and 3 (join filter and logical record relation).    |
|    21576    |    16    |    No    |    Cannot add a logical record relationship between tables '%s' and '%s' because the foreign key column '%s' in table '%s' allows NULL values. Alter the column to disallow NULL values.    |
|    21578    |    16    |    No    |    In order to use partition_options of 2 (non overlapping partitions with multiple subscriptions per partition) or 3 (non overlapping partitions one subscription per partition) the publication '%s' must be enabled to use partition groups functionality. Use sp_changemergepublication to set 'use_partition_groups' to 'true'.    |
|    21579    |    16    |    No    |    Article "%s" in publication "%s" does not qualify for the partition option that you specified. You cannot specify a value of 2 or 3 (nonoverlapping partitions) for the \@partition_options parameter because the article is involved in multiple join filters. Either select a value of 0 or 1 for the \@partition_options parameter, or drop all but one of the join filters by using sp_dropmergefilter.    |
|    21580    |    16    |    No    |    Article "%s" in publication "%s" does not qualify for the partition option that you specified. You cannot specify a value of 2 or 3 (nonoverlapping partitions) for the \@partition_options parameter because the article is involved in both a row filter and a join filter. Either select a value of 0 or 1 for the \@partition_options parameter; drop the join filter by using sp_dropmergefilter; or change the row filter by using sp_changemergepublication.    |
|    21581    |    16    |    No    |    Article "%s" in publication "%s" does not qualify for the partition option that you specified. You cannot specify a value of 2 or 3 (nonoverlapping partitions) for the \@partition_options parameter because the article has a join filter with a join_unique_key value of 0. Either select a value of 0 or 1 for the \@partition_options parameter, or use sp_changemergefilter to specify a value of 1 for join_unique_key.    |
|    21582    |    16    |    No    |    Article "%s" in publication "%s" does not qualify for the partition option that you specified. You cannot specify a value of 2 or 3 (nonoverlapping partitions) for the \@partition_options parameter because the article has a direct or indirect join filter relationship with parent article "%s". The parent article does not use the same value for partition_options. Use sp_changemergepublication to change the value for one of the articles.    |
|    21583    |    16    |    No    |    Cannot update the column in article '%s'. The article has a value of 2 or 3 (nonoverlapping partitions) for the partition_options property, and the column is involved in a row filter and/or a join filter. In this situation, the column cannot be updated at a Subscriber or republisher; it must be updated at the top-level Publisher.    |
|    21584    |    16    |    No    |    Cannot insert the row for article '%s'. The row does not belong to the Subscriber's partition, and the article has a value of 2 or 3 (nonoverlapping partitions) for the partition_options property. Nonoverlapping partitions do not allow out-of-partition inserts.    |
|    21585    |    16    |    No    |    Cannot specify custom article ordering in publication '%s' because the publication has a compatibility level lower than 90RTM. Use sp_changemergepublication to set the publication_compatibility_level to 90RTM.    |
|    21597    |    16    |    No    |    The article includes only the rowguidcol column. You must publish at least one other column.    |
|    21598    |    16    |    No    |    Modifying DDL triggers created by replication is disallowed since these are required to track DDL changes.    |
|    21599    |    16    |    No    |    The parameters \@article and \@join_articlename cannot have the same value. Specify different articles for the two parameters; self-joins are not permitted.    |
|    21600    |    16    |    No    |    Non-SQL Server Publisher [%s] cannot be found. Execute sp_helpdistpublishers to view a list of available Publishers.    |
|    21601    |    16    |    No    |    The value of the parameter \@type must be 'logbased' for Oracle publications.    |
|    21603    |    16    |    No    |    The refresh of Oracle publisher '%s' by sp_refresh_heterogeneous_publisher was not successful. The Oracle publisher meta data has been retained in its failed state to help in diagnosing the cause of the failure. When the problem has been diagnosed and resolved, rerun sp_refresh_heterogeneous_publisher to complete the refresh.    |
|    21604    |    16    |    No    |    The non-SQL Server Publisher vendor is not valid. Attempt to add the Publisher again. If the problem persists, contact Microsoft Customer Support Services.    |
|    21605    |    16    |    No    |    Non-SQL Server Publishers must be configured in the context of the distribution database. Execute sp_adddistpublisher in the context of the distribution database.    |
|    21606    |    16    |    No    |    Parameter "%s" is for non-SQL Server Publishers only. The value of this parameter must be "%s" for a SQL Server Publisher.    |
|    21607    |    16    |    No    |    sp_refresh_heterogeneous_publisher was unable to obtain publisher information for Oracle publisher '%s'. sp_refresh_heterogeneous_publisher may only be called to refresh Oracle publishers currently defined at the distributor.    |
|    21608    |    16    |    No    |    Cannot use a value of TRUE for the parameter \@ignore_distributor. The value must be FALSE for a non-SQL Server Publisher.    |
|    21609    |    16    |    No    |    Non-SQL Server publications do not support updatable subscriptions. The properties allow_sync_tran and allow_queued_tran must be "false".    |
|    21610    |    16    |    No    |    The failed attempt by sp_refresh_heterogeneous_publisher to refresh publisher '%s' did not alter any meta data at the Oracle publisher. Make certain that the correct Oracle publisher has been identified and that the requirements for refreshing the Oracle publisher have been met.    |
|    21611    |    16    |    No    |    Cannot drop the distribution Publisher "%s" because it has publications defined. Drop the publications first.    |
|    21612    |    16    |    No    |    For non-SQL Server Publishers, the value of the \@sync_method parameter must be "character" or "concurrent_c".    |
|    21613    |    16    |    No    |    Constraint column '%s' not found in table '%s'.    |
|    21614    |    16    |    No    |    Index column '%s' not found in table '%s',    |
|    21615    |    16    |    No    |    Unable to find table information for article %s. Local distributor cache may be corrupt.    |
|    21616    |    16    |    No    |    Cannot find column [%s] in the article. Verify that the column exists in the underlying table, and that it is included in the article.    |
|    21617    |    16    |    No    |    Unable to run SQL*PLUS. Make certain that a current version of the Oracle client code is installed at the distributor. For addition information, see SQL Server Error 21617 in Troubleshooting Oracle Publishers in SQL Server Books Online.    |
|    21618    |    16    |    No    |    The Publisher '%s' does not exist. To view a list of Publishers, use the stored procedure sp_helpdistpublisher.    |
|    21619    |    16    |    No    |    Must provide both \@SelectColumnList and \@InsColumnList.    |
|    21620    |    16    |    No    |    The version of SQL*PLUS that is accessible through the system Path variable is not current enough to support Oracle publishing. Make certain that a current version of the Oracle client code is installed at the distributor. For addition information, see SQL Server Error 21620 in Troubleshooting Oracle Publishers in SQL Server Books Online.    |
|    21621    |    16    |    No    |    Unable to create the public synonym %s. Verify that the replication administrative user has been granted the CREATE SYNONYM permission.    |
|    21622    |    16    |    No    |    Unable to grant SELECT permission on the public synonym %s. Verify that the replication administrative user has sufficient permissions.    |
|    21623    |    16    |    No    |    Unable to update the public synonym 'MSSQLSERVERDISTRIBUTOR' to mark Oracle instance '%s' as a SQL Server publisher.    |
|    21624    |    16    |    No    |    Unable to locate the registered Oracle OLEDB provider, OraOLEDB.Oracle, at distributor '%s'. Make certain that a current version of the Oracle OLEDB provider is installed and registered at the distributor. For addition information, see SQL Server Error 21624 in Troubleshooting Oracle Publishers in SQL Server Books Online.    |
|    21625    |    16    |    No    |    Unable to update the publisher table HREPL_PUBLISHER at the Oracle instance '%s'.    |
|    21626    |    16    |    No    |    Unable to connect to Oracle database server '%s' using the Oracle OLEDB provider OraOLEDB.Oracle. For addition information, see SQL Server Error 21626 in Troubleshooting Oracle Publishers in SQL Server Books Online.    |
|    21627    |    16    |    No    |    Unable to connect to Oracle database server '%s' using the Microsoft OLEDB provider MSDAORA. For addition information, see SQL Server Error 21627 in Troubleshooting Oracle Publishers in SQL Server Books Online.    |
|    21628    |    16    |    No    |    Unable to update the registry of distributor '%s' to allow Oracle OLEDB provider OraOLEDB.Oracle to run in process with SQL Server. Make certain that current login is authorized to modify SQL Server owned registry keys. For addition information, see SQL Server Error 21628 in Troubleshooting Oracle Publishers in SQL Server Books Online.    |
|    21629    |    16    |    No    |    The CLSID registry key indicating that the Oracle OLEDB Provider for Oracle, OraOLEDB.Oracle, has been registered is not present at the distributor. Make certain that the Oracle OLEDB provider is installed and registered at the distributor. For addition information, see SQL Server Error 21629 in Troubleshooting Oracle Publishers in SQL Server Books Online.    |
|    21630    |    16    |    No    |    Unable to determine whether the table '%s' is still being published. Contact Customer Support Services.    |
|    21631    |    16    |    No    |    Cannot unpublish table '%s'; the remote call to the Oracle Publisher failed. Verify that the replication administrative user login can connect to the Oracle Publisher using SQL*PLUS. If you can connect but the problem persists, drop and reconfigure Oracle publishing.    |
|    21632    |    16    |    No    |    The parameter %s is not supported for non-SQL Server publications. The value specified for this parameter must be %s.    |
|    21633    |    16    |    No    |    The publication '%s' could not be added because non-SQL Server Publishers only support the \@sync_method parameter values "character" or "concurrent_c".    |
|    21634    |    16    |    No    |    The parameter %s does not support the value '%s' when using non-SQL Server publications. The value must be %s.    |
|    21635    |    16    |    No    |    An unsupported schema option combination was specified. Non-SQL Server publications only support the following schema options: 0x01, 0x02, 0x10, 0x40, 0x80, 0x4000, and 0x8000.    |
|    21637    |    16    |    No    |    %s is required for heterogeneous publications.    |
|    21638    |    16    |    No    |    You have specified a value of '%s' for the \@repl_freq parameter of sp_addpublication. For non-SQL Server publications, this requires one of the following values for the \@sync_method parameter: %s.    |
|    21639    |    16    |    No    |    Heterogeneous publishers can not use trusted connections, set \@trusted to false.    |
|    21640    |    16    |    No    |    Non-SQL Server Publishers do not support a value of 1 for the parameter \@thirdparty_flag. When executing the stored procedure sp_adddistpublisher, specify a value of 0 for the parameter.    |
|    21641    |    16    |    No    |    The "%s" parameter is for non-SQL Server Publishers only. It must be NULL for SQL Server Publishers.    |
|    21642    |    16    |    No    |    Heterogeneous publishers require a linked server. A linked server named '%s' already exists. Please remove linked server or choose a different publisher name.    |
|    21643    |    16    |    No    |    The value specified for the parameter '%s' must be MSSQLSERVER, ORACLE, or ORACLE GATEWAY.    |
|    21644    |    16    |    No    |    %s value of '%s' is not supported for heterogeneous subscribers, must be %s.    |
|    21645    |    16    |    No    |    The value '%s' is not a valid non-SQL Server Publisher type. For SQL Server 2005, the value must be ORACLE or ORACLE GATEWAY.    |
|    21646    |    16    |    No    |    The Oracle server [%s] is already defined as the Publisher [%s] on the Distributor [%s].[%s]. Drop the Publisher or drop the public synonym [%s].    |
|    21647    |    16    |    No    |    The Oracle Publisher support package could not be loaded. Drop the replication administrative user schema and re-create it; ensure it is granted the documented permissions.    |
|    21649    |    16    |    No    |    Cannot change the property '%s'. Non-SQL Server Publishers do not support this property.    |
|    21650    |    16    |    No    |    The value specified for \@rowcount_only for the article '%s' is not 1. For an article in a publication from a non-SQL Server Publisher, 1 is the only valid setting for this parameter.    |
|    21651    |    16    |    No    |    Failed to execute the HREPL.%s request to Oracle Publisher '%s'. Verify that the Oracle package code exists on the Publisher, and that the replication administrative user account has sufficient permissions.    |
|    21653    |    16    |    No    |    The database management system (DBMS) %s %s does not exist. Verify the supported DBMS and versions by querying msdb.dbo.MSdbms.    |
|    21654    |    16    |    No    |    The data type %s does not exist. Verify the supported data types and mappings by querying msdb.dbo.sysdatatypemappings.    |
|    21655    |    16    |    No    |    The data type %s already exists.    |
|    21656    |    16    |    No    |    The data type mapping for %s does not exist. Verify the list of available mappings by querying msdb.dbo.sysdatatypemappings.    |
|    21657    |    16    |    No    |    The data type mapping for %s already exists.    |
|    21658    |    16    |    No    |    The data type mapping does not exist. Verify the list of mappings by querying msdb.dbo.sysdatatypemappings.    |
|    21659    |    16    |    No    |    Cannot execute this procedure for a SQL Server Publisher. The Publisher must be a non-SQL Server Publisher.    |
|    21660    |    16    |    No    |    The value specified for the parameter \@full_or_fast for article '%s' must be 0, 1, or 2.    |
|    21661    |    16    |    No    |    The value specified for the \@shutdown_agent parameter for article '%s' must be 0 or 1.    |
|    21662    |    16    |    No    |    The source object [%s].[%s] on the non-SQL Server Publisher was either not found or is not supported. If the object exists, verify that it meets the requirements for being published.    |
|    21663    |    16    |    No    |    Cannot find a valid primary key for the source table [%s].[%s]. A valid primary key is required to publish the table. Add or correct the primary key definition on the source table.    |
|    21664    |    16    |    No    |    Index [%s] contains unique nullable column.    |
|    21665    |    16    |    No    |    Key [%s] contains unique nullable column.    |
|    21666    |    16    |    No    |    Cannot specify more than %d column names for the index or primary key because this exceeds the maximum number of columns supported by SQL Server. %d columns were specified.    |
|    21667    |    16    |    No    |    The index "%s" was not created. The index has a key length of at least %d bytes. The maximum key length supported by SQL Server is %d bytes.    |
|    21668    |    16    |    No    |    The constraint "%s" was not created because one or more columns in the constraint is not published. Either include all columns in the published article, or alter the constraint to remove columns that are not published.    |
|    21669    |    16    |    No    |    Column [%s] cannot be published because it uses an unsupported data type [%s]. View supported data types by querying msdb.dbo.sysdatatypemappings.    |
|    21670    |    16    |    No    |    Connection to server [%s] failed.    |
|    21671    |    16    |    No    |    Cannot execute the procedure. Administration of a non-SQL Server Publisher must be performed at the associated SQL Server Distributor. Execute the procedure at the Distributor.    |
|    21672    |    16    |    No    |    The login '%s' has insufficient authorization to execute this command.    |
|    21673    |    16    |    No    |    Test connection to publisher [%s] failed. Verify authentication information.    |
|    21674    |    16    |    No    |    Unable to update the linked server [%s] for the login [%s].    |
|    21675    |    16    |    No    |    Cannot specify more than %d indexes for a single table. %d indexes specified. Excess indexes have been ignored.    |
|    21676    |    16    |    No    |    Heterogeneous subscriber '%s' could not add a subscription for heterogeneous publication '%s' because publication sync method is not 'character', 'concurrent_c', or 'database snapshot character'.    |
|    21677    |    16    |    No    |    Heterogeneous publisher '%s' cannot be defined as a subscriber.    |
|    21678    |    16    |    No    |    The parameter "%s" can be set to "%s" only when "%s" is set to "%s".    |
|    21679    |    16    |    No    |    Peer-to-peer publications only support a '%s' parameter value of %s.    |
|    21680    |    16    |    No    |    The Distribution Agent was unable to update the cached log sequence numbers (LSNs) for Originator %s, OriginatorDB %s, OriginatorDBVersion %d, OriginatorPublicationID %d. Stop and restart the Distribution Agent. If the problem persists, contact Customer Support Services.    |
|    21681    |    16    |    No    |    The current user '%s' does not have a valid linked server login mapping for non-SQL Server Publisher [%s]. Replication connects to the Publisher through a linked server; use the stored procedure sp_addlinkedsrvlogin to map the user's login to this linked server.    |
|    21682    |    16    |    No    |    Cannot publish table [%s].[%s]. The replication administraive user requires an explicit SELECT grant, or a SELECT grant through PUBLIC, in order to publish this table. A role-based SELECT grant, if one exists, is not sufficient.    |
|    21683    |    16    |    No    |    Cannot verify administrator login privileges for Oracle Publisher %s. Verify connection information and ensure that you can connect to the Publisher through a tool like SQL*PLUS.    |
|    21684    |    16    |    No    |    The replication administrative user for Oracle Publisher "%s" has insufficient permissions. Refer to the script /MSSQL/Install/oracleadmin.sql for the required permissions.    |
|    21685    |    16    |    No    |    Request '%s' for Oracle schema filter for Oracle publisher '%s' failed.    |
|    21686    |    16    |    No    |    The operation "%s" is not valid. Valid operations are "add", "drop", and "help".    |
|    21687    |    16    |    No    |    Schema filters are supported only for Oracle Publishers. The Publisher "%s" is a "%s" Publisher.    |
|    21688    |    16    |    No    |    The current login '%s' is not in the publication access list (PAL) of any publication at Publisher '%s'. Use a login that is in the PAL, or add this login to the PAL.    |
|    21689    |    16    |    No    |    A NULL \@schema value is invalid for add and drop schema filter operations.    |
|    21690    |    10    |    No    |    The subscriber db cannot be the same as the publisher db when the subscriber is the same as the publisher    |
|    21691    |    10    |    No    |    sp_mergesubscription_cleanup should be called on the subscription database    |
|    21692    |    16    |    No    |    Failed to script the subscriber stored procedures for article '%s' in publication '%s'    |
|    21694    |    16    |    No    |    %s cannot be null or empty when %s is set to 0 (SQL Server Authentication). Specify a login or set security mode to 1 (Windows Authentication).    |
|    21695    |    10    |    No    |    The replication agent job '%s' was not removed because it has a non-standard name; manually remove the job when it is no longer in use.    |
|    21696    |    16    |    No    |    The stored procedure only applies to Oracle Publishers. The Publisher '%s' is a %s Publisher.    |
|    21698    |    16    |    No    |    The parameter '%s' is no longer supported.    |
|    21699    |    10    |    No    |    Unable to reuse view '%s' because it was not found. Re-creating all system table views. This is an informational message only. No user action is required.    |
|    21701    |    16    |    No    |    Microsoft SQL Server Additive Conflict Resolver    |
|    21702    |    16    |    No    |    Microsoft SQL Server Averaging Conflict Resolver    |
|    21703    |    16    |    No    |    Microsoft SQL Server DATETIME (Earlier Wins) Conflict Resolver    |
|    21704    |    16    |    No    |    Microsoft SQL Server DATETIME (Later Wins) Conflict Resolver    |
|    21705    |    16    |    No    |    Microsoft SQL Server Download Only Conflict Resolver    |
|    21706    |    16    |    No    |    Microsoft SQL Server Maximum Conflict Resolver    |
|    21707    |    16    |    No    |    Microsoft SQL Server Merge Text Columns Conflict Resolver    |
|    21708    |    16    |    No    |    Microsoft SQL Server Minimum Conflict Resolver    |
|    21709    |    16    |    No    |    Microsoft SQL Server Priority Column Resolver    |
|    21710    |    16    |    No    |    Microsoft SQL Server Subscriber Always Wins Conflict Resolver    |
|    21711    |    16    |    No    |    Microsoft SQL Server Upload Only Conflict Resolver    |
|    21712    |    16    |    No    |    Microsoft SQLServer Stored Procedure Resolver    |
|    21715    |    16    |    No    |    Cannot register the article resolver %s. This can occur if the account under which SQL Server is running does not have access to the distribution database. Add the class ID and the custom resolver name manually to the MSmerge_articleresolver table in the distribution database.    |
|    21717    |    16    |    No    |    The article resolver name cannot be an empty string or NULL. Specify a valid value for the \@article_resolver parameter.    |
|    21718    |    16    |    No    |    For a COM resolver, the \@resolver_clsid cannot be an empty string or NULL. Specify a valid value for \@resolver_clsid.    |
|    21719    |    10    |    No    |    The Subscriber '%s':'%s' was not marked for reinitialization at the Publisher because the subscription is either anonymous or not valid. Verify that valid values were specified for the \@subscriber and \@subscriber_db parameters of sp_reinitmergesubscription.    |
|    21720    |    16    |    No    |    Cannot find a job that matches the ID or name specified in the parameters \@dynamic_snapshot_jobid or \@dynamic_snapshot_jobname. Verify the values specified for those parameters.    |
|    21721    |    10    |    No    |    UserScripts    |
|    21722    |    16    |    No    |    Failed to add an extended trigger for replicating the '%.*ls' event.    |
|    21723    |    16    |    No    |    The value specified for the \@pubid parameter of procedure '%s' is not valid or is NULL. Verify that the Merge Agent is running correctly. Reinitalize the subscription if the problem persists.    |
|    21724    |    10    |    No    |    Cannot add the foreign key %s with the CASCADE option because table %s is published. Add the NOT FOR REPLICATION clause to the foreign key definition.    |
|    21725    |    16    |    No    |    Cannot alter the view. An indexed view replicated as a table cannot be altered to a nonindexed view. Drop the view from the publication before attempting to alter it.    |
|    21727    |    14    |    No    |    Cannot complete the replication operation. The security check for the current user is failing. Only members of the sysadmin fixed server role, or db_owner or db_ddladmin fixed database roles can perform this operation.    |
|    21728    |    16    |    No    |    The article can support logical record level conflict detection only if it uses logical record conflict resolution.    |
|    21729    |    16    |    No    |    The \@keep_partition_changes property cannot be set to "true." This is because the \@publication_compatibility_level property is set to 90RTM or higher and the \@use_partition_groups property is set to "true." Set a lower compatibility level or set the \@use_partition_groups property to "false."    |
|    21730    |    16    |    No    |    Table '%s' can not be replicated because it contains imprecise Primary Key column, please recreate table without 'persisted' clause and try again.    |
|    21731    |    16    |    No    |    Cannot add a constraint or default without an explicit name, because the table is included in a publication that replicates DDL events. Specify a unique name for the constraint and then reissue the DDL statement.    |
|    21732    |    16    |    No    |    Using Data Transformation Services (DTS) packages in replication requires a password that is not NULL or empty. Specify a valid value for parameter '%s'.    |
|    21733    |    16    |    No    |    Cannot open database %s. The upgrade of replication %s could not be performed. Run %s again from the %s database when the %s is accessible.    |
|    21734    |    16    |    No    |    Peer-to-peer publications do not support replicating timestamp columns as varbinary(8). You cannot add an article with this option, nor add or alter a table to include a timestamp column as varbinary(8).    |
|    21735    |    16    |    No    |    Source object [%s].[%s] is a temporary object and cannot be published.    |
|    21736    |    16    |    No    |    Unable to relocate the article log table to a different tablespace. Verify that the replication administrative user login can connect to the Oracle Publisher using SQL*PLUS. If you can connect, but the problem persists, it might be caused by insufficient permissions or insufficient space in the tablespace; check for any Oracle error messages.    |
|    21737    |    16    |    No    |    The property '%s' is not valid for '%s' Publishers.    |
|    21738    |    16    |    No    |    The property '%s' is not valid for %s publications.    |
|    21739    |    16    |    No    |    Cannot alter property '%s'. You must first call the stored procedure sp_articleview to initialize the article; the property can then be altered.    |
|    21740    |    16    |    No    |    Oracle subscriber '%s' not found. Loopback support cannot be checked.    |
|    21741    |    16    |    No    |    Unable to retrieve distributor information from Oracle publisher '%s'. Bi-directional publishing requires Oracle publisher to exist before the Oracle subscriber.    |
|    21742    |    16    |    No    |    The Oracle Publisher name is '%s' and the Oracle Subscriber name is '%s'. Bidirectional Oracle publishing requires the Oracle Publisher and Subscriber names to be the same.    |
|    21743    |    16    |    No    |    Unable to retrieve the originator information for the Oracle subscriber '%s'.    |
|    21744    |    16    |    No    |    Oracle bidirectional publishing requires parameter '%s' to have a value of '%s'.    |
|    21745    |    16    |    No    |    Cannot generate a filter view or procedure. Verify that the value specified for the \@filter_clause parameter of sp_addarticle can be added to the WHERE clause of a SELECT statement to produce a valid query.    |
|    21746    |    16    |    No    |    The '%s' character length must not exceed %d.    |
|    21747    |    16    |    No    |    Cannot establish a connection to the Oracle Publisher '%s'. Verify connection information and ensure that you can connect to the Publisher through a tool like SQL*PLUS.    |
|    21748    |    16    |    No    |    The article was dropped at the Distributor, but information at the Publisher '%s' was not dropped. No action is required; the information is cleaned up if the Publisher is dropped.    |
|    21749    |    16    |    No    |    The Publisher was dropped at the Distributor, but information on the Publisher '%s' was not dropped. Connect to the Oracle Publisher with SQL*PLUS and drop the replication administrative user.    |
|    21750    |    16    |    No    |    The table %s does not have a primary key, which is required for transactional replication. Create a primary key on the table.    |
|    21751    |    16    |    No    |    Cannot publish view %s as a table because it does not have a unique clustered index. Publish the view as a view, or add a unique clustered index.    |
|    21752    |    16    |    No    |    The current user %s does not have SELECT permission on the table %s. The user must have SELECT permission to retrieve rows at the Subscriber that have updates pending in the queue.    |
|    21753    |    16    |    No    |    The table %s, which is specified in the \@tablename parameter of sp_getqueuedrows, is not part of any active initialized queued subscription. Ensure your queued subscriptions are properly initialized by running the Snapshot Agent, Distribution Agent, and Queue Reader Agent.    |
|    21754    |    16    |    No    |    Processing has been terminated. The resultset for sp_getqueuedrows is larger than 16,000, the maximum size that the procedure can return. Run the Queue Reader Agent to flush the queue at the Subscriber before executing this procedure again.    |
|    21755    |    16    |    No    |    Failed to mark '%s' as a system object.    |
|    21756    |    16    |    No    |    Based on article settings, table %s should have an identity column, but it does not have one. Verify article settings with sp_helparticle and change them if necessary with sp_changearticle.    |
|    21757    |    16    |    No    |    The subscription is read-only. The publication that this subscription synchronizes with allows updates at the Subscriber, but a value of 'read-only' was specified for the \@update_mode parameter of sp_addsubscription. To allow updates, you must drop and then re-create the subscription, specifying a different value for \@update_mode.    |
|    21758    |    16    |    No    |    Cannot find a valid Queue Reader Agent ID for the subscription to Publisher %s, database %s, publication %s. The specified subscription to an updating Subscriber publication is not initialized. Run the Snapshot Agent, Distribution Agent, and Queue Reader Agent to initialize the subscription.    |
|    21759    |    16    |    No    |    Cannot add the column '%s' to the table '%s'. The table already contains the maximum number of columns allowed for an article in a merge publication (246 columns).    |
|    21760    |    11    |    No    |    Cannot execute the replication script in the 'master' database; the current session will be terminated. The script must be executed in the distribution database, and the master database cannot serve as the distribution database.    |
|    21761    |    20    |    No    |    Cannot execute the replication script; the current session will be terminated. Check for any errors returned by SQL Server during execution of the script.    |
|    21762    |    10    |    No    |    The distribution database '%s' has a compatibility level of %d, which is different from that of the master database. The two compatibility levels must be the same, so the distribution database level is being changed to %d. This is an informational message only. No user action is required.    |
|    21763    |    16    |    No    |    Message Queuing Service is not running. Start this service and retry the operation.    |
|    21764    |    16    |    No    |    Cannot create the publication. Specifying a value of 'msmq' for the parameter \@queue_type is supported only on Microsoft Windows NT platforms. Specify a value of 'sql' for this parameter.    |
|    21765    |    10    |    No    |    The column msrepl_tran_version has been predefined and allows NULLs. This column will be dropped and recreated to not allow NULLs for updating subscribers.    |
|    21766    |    16    |    No    |    Table %s contains an identity column that is marked as Not For Replication, but the \@identitymanagementoption parameter of sp_addarticle is set to 'none'. To support immediate updating subscriptions, specify a value of 'manual' or 'auto' for \@identitymanagementoption.    |
|    21767    |    10    |    No    |    Warning: The parameter '%s' is obsolete and is available only for backwards compatibility. It will not be available in future releases. Instead of this parameter, use the parameter '%s'.    |
|    21768    |    16    |    No    |    When executing sp_adddistributor for a remote Distributor, you must use a password. The password specified for the \@password parameter must be the same when the procedure is executed at the Publisher and at the Distributor.    |
|    21769    |    10    |    No    |    Custom data type mappings are not supported. You must validate the correctness of the mapping. If mappings are not compatible, errors will likely occur when moving data from the Publisher to the Subscriber.    |
|    21770    |    10    |    No    |    Data type mapping from '%s' to '%s' does not exist. Review source and destination data type, length, precision, scale, and nullability. Query the system table msdb.dbo.sysdatatypemappings for a list of supported mappings.    |
|    21771    |    16    |    No    |    %s is not within the supported range of %d and %d.    |
|    21772    |    16    |    No    |    Property "%s" requires the parameters \@force_invalidate_snapshot and \@force_reinit_subscription to be set to "true".    |
|    21773    |    10    |    No    |    The distribution database '%s' cannot be opened due to inaccessible files. The database will be dropped, but distribution database cleanup tasks will not occur. Check the database and server error logs for more information about why the database files cannot be accessed.    |
|    21774    |    16    |    No    |    This procedure is supported only for non-SQL Server Publishers. The Publisher '%s', on which you are executing the procedure, is a SQL Server Publisher.    |
|    21775    |    16    |    No    |    Failed to generate column bitmap for article '%s'.    |
|    21776    |    16    |    No    |    Failed to generate published column bitmap for article '%s'.    |
|    21777    |    16    |    No    |    Failed to generate article view name for article '%s'.    |
|    21778    |    16    |    No    |    Cannot add Publisher objects to the Oracle Publisher for article '%s'. Verify connection information and ensure that you can connect to the Publisher through a tool like SQL*PLUS. Ensure that the replication administrative user schema has the required permissions.    |
|    21779    |    10    |    No    |    Cannot use the specified data type mapping. The matching destination data type for source type %s cannot be found. Query the system table msdb.dbo.sysdatatypemappings for a list of supported mappings. Verify that the length, precision, scale, and nullability of the source type are correct.    |
|    21780    |    16    |    No    |    The non-SQL Server Publisher is missing one or more %s objects. Drop and re-create the Publisher and the replication administrative user schema.    |
|    21781    |    16    |    No    |    Unable to retrieve heterogeneous metadata. Verify connection information    |
|    21782    |    16    |    No    |    Cannot add primary key column '%s' to article '%s'. If the Publisher is a non-SQL Server Publisher, the primary key could have violated SQL Server limits for number and length of columns. For more information, see errors returned by sp_addarticle.    |
|    21783    |    16    |    No    |    Cannot add the Publisher triggers and the article log table to the Oracle Publisher for the article '%s'. Verify connection information and ensure that you can connect to the Publisher through a tool like SQL*PLUS. Ensure that the replication administrative user schema has the required permissions.    |
|    21784    |    16    |    No    |    You must specify a non-NULL value for the \@rowfilter parameter.    |
|    21785    |    16    |    No    |    Failure to query Oracle XactSet Job attributes for publisher '%s'.    |
|    21786    |    16    |    No    |    Failure to refresh Oracle XactSet Job for publisher '%s'.    |
|    21787    |    16    |    No    |    Failure to query Oracle Xact batching enabled flag for publisher '%s'.    |
|    21788    |    16    |    No    |    Invalid parameter passed to sp_IHSetXactBatching. The bit flag to enable/disable Xact batching must be 0 or 1.    |
|    21789    |    16    |    No    |    Failure to set the Oracle Xact batching enabled flag for publisher '%s'.    |
|    21790    |    16    |    No    |    Cannot publish the table '%s.%s' from the Publisher '%s'. Verify connection information and ensure that you can connect to the Publisher through a tool like SQL*PLUS. Ensure that the replication administrative user schema has the required permissions.    |
|    21791    |    16    |    No    |    The table '%s.%s' already appears in a transactional publication on Oracle Gateway Publisher '%s'. When using the Oracle Gateway option, a table published using transactional replication can only be included in one publication. To publish this table in more than one publication, you must reconfigure the Oracle Publisher to use the Oracle Complete option.    |
|    21792    |    16    |    No    |    The table '%s.%s' already appears in the transactional publication '%s' on Publisher '%s'. The Oracle Gateway publishing option (the default) allows a table to be included as an article in any number of snapshot publications, but only in one transactional publication. To publish a table in more than one transactional publication, use the Oracle Complete publishing option. To change publishing options, you must drop and reconfigure the Publisher.    |
|    21793    |    16    |    No    |    Non-SQL Server Publishers are supported only in the Enterprise and Developer editions of SQL Server. The edition of this instance is %s.    |
|    21794    |    16    |    No    |    The value specified for the \@propertyname parameter is not valid. Use one of the following values: %s.    |
|    21795    |    16    |    No    |    The value specified for property %s is not valid. Use one of the following values: %s.    |
|    21796    |    16    |    No    |    The property "xactsetjobinterval" must be assigned a value greater than or equal to 0.    |
|    21797    |    16    |    No    |    Cannot create the agent job. '%s' must be a valid Windows login in the form : 'MACHINE\Login' or 'DOMAIN\Login'. See the documentation for '%s'.    |
|    21798    |    16    |    No    |    Cannot execute the replication administrative procedure. The '%s' agent job must be added through '%s' before continuing. See the documentation for '%s'.    |
|    21799    |    16    |    No    |    The %s agent for Publisher (%s), database (%s), publication (%s) cannot be found. Create the agent with the appropriate procedure: sp_addpublication_snapshot, sp_addlogreader_agent, or sp_addqreader_agent.    |
|    21800    |    16    |    No    |    The common generation watermark is invalid at this replica since it does not exist or metadata for changes not yet propagated may have been cleaned up.    |
|    21801    |    16    |    No    |    The stored procedure sp_createagentparameter failed to add one or more parameters to the system table msdb.dbo.MSagentparameterlist. Check for any errors returned by sp_createagentparameter and errors returned by SQL Server during execution of sp_createagentparameter.    |
|    21802    |    16    |    No    |    The agent profile creation process cannot validate the specified agent parameter value. '%s' is not a valid value for the '%s' parameter. The value must be an integer less than or equal to '%d'. Verify that replication is installed properly.    |
|    21803    |    16    |    No    |    Cannot update agent parameter metadata. Replication could not insert parameter '%s' into table '%s'. Verify that replication is properly installed. Check errors returned by SQL Server during execution of sp_createagentparameter.    |
|    21804    |    16    |    No    |    The value '%d' specified for the \@agent_type parameter of sp_getagentparameterlist is not valid. Specify a valid value of 1, 2, 3, 4, or 9.    |
|    21805    |    16    |    No    |    The agent profile creation process cannot validate the specified agent parameter value. '%s' is not a valid value for the '%s' parameter. The value must be an integer. Verify that replication is installed properly and that sp_add_agent_parameter is invoked with a valid value.    |
|    21806    |    16    |    No    |    The agent profile creation process cannot validate the specified agent parameter value: the profile_id %d does not exist or it does not support the parameter %s. The value must be an integer. Verify that replication is installed properly and that sp_add_agent_parameter is invoked with a valid value.    |
|    21807    |    16    |    No    |    For a .NET Assembly Business Logic Handler, the \@resolver_clsid must be specified as NULL.    |
|    21808    |    16    |    No    |    For a .NET Assembly Business Logic Handler, the \@resolver_info must contain the class name in '%s' that implements the Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule interface.    |
|    21809    |    10    |    No    |    DDL replication is not enabled for database '%ls' because its compatibility level is less than 80.    |
|    21810    |    16    |    No    |    Identity column can only be added to a published table with 'Not For Replication' clause    |
|    21811    |    16    |    No    |    Cannot drop the column '%s' because it is used by replication: it is referenced in a filter or view by article '%s'. To drop the column, you must first remove the filter from the article.    |
|    21812    |    16    |    No    |    Cannot perform "Disable Trigger All" on table %s because it belongs to a publication that supports updatable subscriptions (replication adds triggers to tables for these types of publications). You may, however, disable user triggers individually. Specify an individual user trigger name to disable.    |
|    21813    |    16    |    No    |    Can not disable trigger %s on table %s because it is required by updatable publication.    |
|    21814    |    16    |    No    |    DDL replication failed to refresh custom procedures, please run "exec sp_register_custom_scripting 'CUSTOM_SCRIPT', your_script, '%s', '%s' "and try again    |
|    21815    |    16    |    No    |    Can not alter replicated object '%s' to 'with encrypted'.    |
|    21816    |    16    |    No    |    An invalid value was specified for parameter '%s'. The value must be '%s' when changing this property.    |
|    21817    |    16    |    No    |    The property '%s' is only valid for push subscriptions.    |
|    21818    |    10    |    No    |    The parameters for security, batch size, and scheduling have been deprecated and should no longer be used. For more information, see the "sp_addsubscriber" documentation.    |
|    21819    |    16    |    No    |    Cannot change the property '%s' . This property is only valid for subscriptions that allow updating at the Subscriber. The subscription against which the procedure was called does not allow updates at the Subscriber.    |
|    21820    |    16    |    No    |    Cannot write to the script file in the snapshot folder at the Distributor (%ls). Ensure that there is enough disk space available. Also ensure that the account under which the Snapshot Agent runs has permissions to write to the snapshot folder and its subdirectories.    |
|    21821    |    16    |    No    |    Specify one and only one of the parameters - %s or %s.    |
|    21822    |    16    |    No    |    Cannot perform %s on %s as entry already exists.    |
|    21823    |    16    |    No    |    Cannot perform %s on %s as entry does not exist.    |
|    21824    |    16    |    No    |    Can not add constraints to multiple columns because table %s is published but column %s is not in all active partitions, please use separate DDL statement.    |
|    21825    |    16    |    No    |    Can not drop constraints in the same DDL statement which drops columns from table %s because the table is published, please use separate DDL statement.    |
|    21826    |    16    |    No    |    The property '%s' is valid only for %s subscriptions. Use '%s' for %s subscriptions.    |
|    21827    |    16    |    No    |    The %s parameters have been deprecated and should no longer be used. For more information, see the '%s' documentation.    |
|    21828    |    16    |    No    |    The proxy account for jobstep_uid (%s) could not be found.    |
|    21830    |    16    |    No    |    You cannot specify schema_option 0x4 (script identity as identity rather than the base data type) for article '%s'. The value specified for the parameter \@identityrangemanagementoption is NONE. To replicate identity as identity, the value must be MANUAL or AUTO for publications that do not support queued updating subscriptions.    |
|    21831    |    16    |    No    |    The %s already exists. Use '%s' to change any settings/properties.    |
|    21832    |    16    |    No    |    Only members of the sysadmin fixed server role can perform this operation without specifying %s.    |
|    21833    |    16    |    No    |    An error occurred when creating a trace event at Oracle publisher '%s'. The trace event could not be posted.    |
|    21834    |    16    |    No    |    The primary key for '%s.%s' has %d columns. SQL Server supports a maximum of %d columns. Redefine the primary key so that it has no more than the maximum number of columns.    |
|    21835    |    16    |    No    |    The index for the primary key '%s.%s' is at least %d bytes. SQL Server supports a maximum key length of %d bytes. Reduce the number of columns in the primary key or redefine the columns to use smaller data types.    |
|    21836    |    16    |    No    |    The distribution agent must be run in single subscription stream mode prior to resetting the subscription xact_seqno.    |
|    21837    |    16    |    No    |    A replication agent job (%s) for this subscription already exists.    |
|    21838    |    16    |    No    |    The %s parameter(s) have been deprecated from this procedure. The value(s) should now be specified when calling '%s'.    |
|    21839    |    16    |    No    |    Article '%s' can not support schema_option 0x20 or 0x2000000000 because it contains computed column, check/default constraint or primary key which is based on CLR type column, change \@schema_option setting and try again.    |
|    21840    |    16    |    No    |    Can not add CLR type based computed column or check constraint to table '%s' because article '%s' supports schema_option 0x20.    |
|    21841    |    10    |    No    |    DDL replication is forcing reinitialization because either publication'%s' uses character mode bcp, or timestamp/identity column is being replicated as base type only for article '%s'.    |
|    21842    |    16    |    No    |    %s can only be specified/changed for heterogeneous publications when %s is set to %s.    |
|    21843    |    16    |    No    |    Article '%s' can not be added, an indexed view published as 'indexed view logbased' and a stored procedure in either form of 'proc exec' can not be published if their common base table is also published.    |
|    21844    |    16    |    No    |    Can not alter XML, CLR type or MAX type column because table is published and article '%s' supports the schema option to map this to a base column type.    |
|    21845    |    16    |    No    |    Can not alter procedure '%s' to depend on an indexed view published as 'indexed view logbased' or a base table because the indexed view depends on this table as well.    |
|    21846    |    16    |    No    |    Cannot find a distribution agent job for the specified transactional or snapshot push subscription.    |
|    21847    |    16    |    No    |    Cannot find a merge agent job for the specified merge push subscription.    |
|    21848    |    16    |    No    |    The specified pull subscription is not configured with a synchronization agent job.    |
|    21850    |    16    |    No    |    The property "%s" cannot be changed to "%s" after the value has already been set to "%s".    |
|    21851    |    16    |    No    |    Peer-to-peer publications only support a "%s" value of %s. Article "%s" currently has a "%s" value of %s. This value must be changed to continue.    |
|    21852    |    16    |    No    |    Peer-to-peer publications do not support %s. Article "%s" currently has %s. This must be changed to continue.    |
|    21853    |    10    |    No    |    Warning: The "%s" property for %s "%s" has been changed to "%s" because it is required by %s.    |
|    21854    |    10    |    No    |    Could not add new article to publication '%s' because of active schema change activities or a snapshot is being generated.    |
|    21855    |    16    |    No    |    The login %s provided in sp_link_publication is not mapped to any user in publishing database %s.    |
|    21856    |    16    |    No    |    For a .NET Assembly Business Logic Handler, the .NET Assembly name should be the name of a valid assembly in '%s' that contains the class that implements the Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule interface. Check the registration of the business logic handler to make sure the .NET Assembly name was specified correctly.    |
|    21857    |    10    |    No    |    Forcing re-initialization for article '%s' in publication '%s', the clustered index on indexed view '%s' may have been dropped by upgrade or restore process, please re-create the index and re-sync your data.    |
|    21858    |    16    |    No    |    Snapshot can not process article '%s' in publication '%s', the clustered index on indexed view '%s' may have been dropped by upgrade or restore process, please re-create the index and re-run snapshot.    |
|    21859    |    16    |    No    |    Cannot change subscription property '%s' because there is no entry for this subscription in the MSsubscription_properties table. Call sp_addmergepullsubscription_agent before changing this property.    |
|    21860    |    10    |    No    |    Table '%s' in database '%s' is subscribing to transactional queued publication and published for merge for uploading changes, this may cause non-convergence between transactional publisher and subscriber.    |
|    21861    |    16    |    No    |    The current operation was aborted because it would deactivate an article in a publication for which a snapshot was being generated.    |
|    [21862](../../relational-databases/errors-events/mssqlserver-21862-database-engine-error.md)    |    16    |    No    |    FILESTREAM columns cannot be published in a publication by using a synchronization method of either 'database snapshot' or 'database snapshot character'.    |
|    21863    |    16    |    No    |    Cannot add the SPARSE property to a column for the article '%s' because merge replication does not support sparse columns.    |
|    21864    |    16    |    No    |    Cannot publish the article '%s' or add the COLUMN_SET attribute set to its base table '%s' because replication does not support column sets.    |
|    21865    |    16    |    No    |    The '%s' publication property must be either 'true' or 'false'.    |
|    21866    |    16    |    No    |    The publication property '%s' can only be set to '%s' when the publication property '%s' is set to '%s'.    |
|    21867    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The table '%s' belongs to a publication which does not allow switching of partitions    |
|    21868    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The statement is not allowed because one or more of the tables in this statement is an article with a different destination table or owner.    |
|    21869    |    16    |    No    |    Cannot add filter between proposed parent article '%s' and proposed child article '%s' since this would introduce a cycle in the filter relationships. The proposed parent is already being filtered by the child.    |
|    [21871](../../relational-databases/errors-events/mssqlserver-21871-database-engine-error.md)    |        |        |    Publisher %s of database %s has not been redirected.    |
|    21879    |    16    |    No    |    Unable to query the redirected server '%s' for original publisher '%s' and publisher database '%s' to determine the name of the remote server; Error %d, Error message '%s'.    |
|    [21889](../../relational-databases/errors-events/mssqlserver-21889-database-engine-error.md)    |        |        |    The SQL Server instance '%s' is not a replication publisher. Run **sp_adddistributor** on SQL Server instance '%s' with distributor '%s' in order to enable the instance to host the publishing database '%s'. Make certain to specify the same login and password as that used for the original publisher.    |
|    [21892](../../relational-databases/errors-events/mssqlserver-21892-database-engine-error.md)    |        |        |    Unable to query sys.availability_replicas at the availability group primary associated with virtual network name '%s' for the server names of the member replicas: error = %d, error message = %s.'    |
|    [21893](../../relational-databases/errors-events/mssqlserver-21893-database-engine-error.md)    |        |        |    The subscribers ( %s ) of original publisher '%s' do not appear as remote servers at redirected publisher '%s'. Run **sp_addlinkedserver** at the redirected publisher to add these subscribers as remote servers.    |
|    [21898](../../relational-databases/errors-events/mssqlserver-21898-database-engine-error.md)    |        |        |    The publisher '%s' uses distribution database '%s' and not '%s' which is required in order to host the publishing database '%s'. Run **sp_changedistpublisher** at distributor '%s' to change the distribution database used by the publisher to '%s'.    |
|    [21899](../../relational-databases/errors-events/mssqlserver-21899-database-engine-error.md)    |        |        |    The query at the redirected publisher '%s' to determine whether there were sysserver entries for the subscribers of the original publisher '%s' failed with error '%d', error message '%s'.    |
