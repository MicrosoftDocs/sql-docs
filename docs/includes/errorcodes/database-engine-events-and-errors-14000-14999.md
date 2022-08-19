---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    14002    |    16    |    No    |    Could not find the 'Sync' subsystem with the task ID %ld.    |
|    14003    |    16    |    No    |    You must supply a publication name.    |
|    14004    |    16    |    No    |    %s must be in the current database.    |
|    14005    |    16    |    No    |    Cannot drop the publication because at least one subscription exists for this publication. Drop all subscriptions to the publication before attempting to drop the publication. If the problem persists, replication metadata might be incorrect; consult Books Online for troubleshooting information.    |
|    14006    |    16    |    No    |    Could not drop the publication.    |
|    14008    |    11    |    No    |    There are no publications.    |
|    14009    |    11    |    No    |    There are no articles for publication '%s'.    |
|    14010    |    16    |    No    |    The remote server '%s' is not defined as a subscription server. Ensure you specify the server name rather than a network alias.    |
|    14011    |    16    |    No    |    Unable to mark server '%s' as a non SQL Server subscriber.    |
|    14012    |    16    |    No    |    The \@status parameter value must be either 'active' or 'inactive'.    |
|    14013    |    16    |    No    |    This database is not enabled for publication.    |
|    14014    |    16    |    No    |    The synchronization method (\@sync_method) must be '[bcp] native', '[bcp] character', 'concurrent', 'concurrent_c', 'database snapshot', or 'database snapshot character'.    |
|    14015    |    16    |    No    |    The replication frequency (\@repl_freq) must be either 'continuous' or 'snapshot'.    |
|    14016    |    16    |    No    |    The publication '%s' already exists.    |
|    14017    |    16    |    No    |    Invalid \@restricted parameter value. Valid options are 'true' or 'false'.    |
|    14018    |    16    |    No    |    Could not create the publication.    |
|    14019    |    16    |    No    |    The \@operation parameter value must be either add, drop, or alter.    |
|    14020    |    16    |    No    |    Could not obtain the column ID for the specified column. Schema replication failed.    |
|    14021    |    16    |    No    |    The column was not added correctly to the article.    |
|    14022    |    16    |    No    |    The \@property parameter value must be either 'description', 'sync_object', 'type', 'ins_cmd', 'del_cmd', 'upd_cmd', 'filter', 'dest_table', 'dest_object', 'creation_script', 'pre_creation_cmd', 'status', 'schema_option', or 'destination_owner'.    |
|    14023    |    16    |    No    |    The type must be '[indexed view ]logbased[ (manualview\|manualfilter\|manualboth)]', '[serializable ]proc exec', or '(view\|indexed view\|proc\|func\|aggregate\|synonym) schema only'.    |
|    14024    |    16    |    No    |    The value of property 'subscriber_provider' cannot be NULL.    |
|    14025    |    10    |    No    |    Article update successful.    |
|    14026    |    16    |    No    |    The value of property 'subscriber_type is not a supported heterogeneous subscriber type. The value must be 1 (ODBC subscriber), or 3 (OLEDB subscriber).    |
|    14027    |    11    |    No    |    %s does not exist in the current database.    |
|    14028    |    16    |    No    |    Only user tables, materialized views, and stored procedures can be published as 'logbased' articles.    |
|    14029    |    16    |    No    |    The vertical partition switch must be either 'true' or 'false'.    |
|    14030    |    16    |    No    |    The article '%s' exists in publication '%s'.    |
|    14031    |    16    |    No    |    User tables and views are the only valid synchronization objects.    |
|    14032    |    16    |    No    |    The value of parameter %s cannot be 'all'. It is reserved by replication stored procedures.    |
|    14033    |    16    |    No    |    Could not change replication frequency because there are active subscriptions on the publication.    |
|    14034    |    16    |    No    |    The publication name (\@publication) cannot be the keyword 'all'.    |
|    14035    |    16    |    No    |    The replication option '%s' of database '%s' has already been set to true.    |
|    14036    |    16    |    No    |    Could not enable database for publishing.    |
|    14037    |    16    |    No    |    The replication option '%s' of database '%s' has been set to false.    |
|    14038    |    16    |    No    |    Could not disable database for publishing.    |
|    14039    |    16    |    No    |    Could not construct column clause for article view. Reduce the number of columns or create the view manually.    |
|    14040    |    16    |    No    |    The server '%s' is already a Subscriber.    |
|    14041    |    16    |    No    |    The '%s' property can only be changed if the publication is enabled for heterogeneous subscriptions. The publication is not enabled.    |
|    14042    |    16    |    No    |    Could not create Subscriber.    |
|    14043    |    16    |    No    |    The parameter %s passed to stored procedure %s cannot be NULL.    |
|    14044    |    16    |    No    |    Unable to clear subscriber status for the server.    |
|    14045    |    16    |    No    |    Unable to update subscriber_type in MSdistribution_agents table.    |
|    14046    |    16    |    No    |    Could not drop article. A subscription exists on it.    |
|    14047    |    16    |    No    |    Could not drop %s.    |
|    14048    |    16    |    No    |    The server '%s' is not a Subscriber.    |
|    14049    |    16    |    No    |    Stored procedures for replication are the only objects that can be used as a filter.    |
|    14050    |    11    |    No    |    No subscription is on this publication or article.    |
|    14051    |    16    |    No    |    The parameter value must be 'sync_type' or 'dest_db'.    |
|    14052    |    16    |    No    |    The \@sync_type parameter value must be "automatic", "none", "replication support only", "initialize with backup", or "initialize from lsn".    |
|    14053    |    16    |    No    |    The subscription could not be updated at this time.    |
|    14054    |    10    |    No    |    The subscription was updated successfully.    |
|    14055    |    10    |    No    |    The subscription does not exist.    |
|    14056    |    16    |    No    |    The subscription could not be dropped at this time.    |
|    14057    |    16    |    No    |    The subscription could not be created.    |
|    14058    |    16    |    No    |    Cannot create the subscription because the subscription already exists in the subscription database. Only one subscription to the same publication is allowed in each subscription database. Drop the subscription and add it again if necessary. If the problem persists, replication metadata might be incorrect; see Books Online for troubleshooting information.    |
|    14059    |    16    |    No    |    Materialized view articles cannot be created for publications with the properties allow_sync_tran, allow_queued_tran, or allow_dts.    |
|    14060    |    16    |    No    |    Subscriber parameters specifying provider properties must be NULL for SQL Server subscribers.    |
|    14061    |    16    |    No    |    The \@pre_creation_cmd parameter value must be 'none', 'drop', 'delete', or 'truncate'.    |
|    14062    |    10    |    No    |    The Subscriber was dropped.    |
|    14063    |    11    |    No    |    The remote server does not exist or has not been designated as a valid Subscriber.    |
|    14065    |    16    |    No    |    The \@status parameter value must be 'initiated', 'active', 'inactive', or 'subscribed'.    |
|    14066    |    16    |    No    |    The previous status must be 'active', 'inactive', or 'subscribed'.    |
|    14067    |    16    |    No    |    The status value is the same as the previous status value.    |
|    14068    |    16    |    No    |    The subscription status of the object could not be changed.    |
|    14069    |    16    |    No    |    Could not update sysarticles. The subscription status could not be changed.    |
|    14070    |    16    |    No    |    Could not update the distribution database subscription table. The subscription status could not be changed.    |
|    14071    |    16    |    No    |    Could not find the Distributor or the distribution database for the local server. The Distributor may not be installed, or the local server may not be configured as a Publisher at the Distributor.    |
|    14074    |    16    |    No    |    The server '%s' is already listed as a Publisher.    |
|    14075    |    16    |    No    |    The Publisher could not be created at this time.    |
|    14076    |    16    |    No    |    Could not grant replication login permission to '%s'.    |
|    14077    |    10    |    No    |    The publication was updated successfully.    |
|    14078    |    16    |    No    |    The parameter must be 'description', 'taskid', 'sync_method', 'status', 'repl_freq', 'restricted', 'retention', 'immediate_sync', 'enabled_for_internet', 'allow_push', 'allow_pull', 'allow_anonymous', or 'retention'.    |
|    14080    |    16    |    No    |    The remote server "%s" does not exist, or has not been designated as a valid Publisher, or you may not have permission to see available Publishers.    |
|    14085    |    16    |    No    |    The Subscriber information could not be obtained from the Distributor.    |
|    14088    |    16    |    No    |    The table '%s' must have a primary key to be published using the transaction-based method.    |
|    14089    |    16    |    No    |    The clustered index on materialized view '%s' may not contain nullable columns if it is to be published using the transaction-based method.    |
|    14090    |    16    |    No    |    Error evaluating article synchronization object after column drop. The filter clause for article '%s' must not reference the dropped column.    |
|    14091    |    16    |    No    |    The \@type parameter passed to sp_helpreplicationdb must be either 'pub' or 'sub'.    |
|    14092    |    16    |    No    |    Could not change article because there is an existing subscription to the article.    |
|    14093    |    16    |    No    |    Cannot grant or revoke access directly on publication '%s' because it uses the default publication access list.    |
|    14094    |    16    |    No    |    Could not subscribe to article '%s' because heterogeneous Subscriber '%s' does not support the \@pre_creation_cmd parameter value 'truncate'.    |
|    14095    |    16    |    No    |    The value for the \@sync_method parameter is not valid. Could not subscribe to publication '%s' because non-SQL Server Subscriber '%s' only supports values of 'character', 'bcp character', 'concurrent_c', and 'database snapshot character' for the \@sync_method parameter.    |
|    14096    |    16    |    No    |    The path and name of the table creation script must be specified if the \@pre_creation_cmd parameter value is 'drop'.    |
|    14097    |    16    |    No    |    The 'status' value must be 'no column names', 'include column names', 'string literals', 'parameters', 'DTS horizontal partitions' or 'no DTS horizontal partitions'.    |
|    14098    |    16    |    No    |    Cannot drop Distribution Publisher '%s'. The remote Publisher is using '%s' as Distributor. Disable publishing at the Publisher before attempting to drop this relationship.    |
|    14099    |    16    |    No    |    The server '%s' is already defined as a Distributor. To reconfigure the server as a Distributor, you must first uninstall the exisiting Distributor. Use the stored procedure sp_dropdistributor, or use the Disable Publishing and Distribution Wizard.    |
|    14100    |    16    |    No    |    Specify all articles when subscribing to a publication using concurrent snapshot processing.    |
|    14101    |    16    |    No    |    The publication '%s' already has a Snapshot Agent defined.    |
|    14102    |    16    |    No    |    Specify all articles when unsubscribing from a publication using concurrent snapshot processing.    |
|    14103    |    16    |    No    |    Invalid "%s" value. Valid values are "publisher", "subscriber", or "both".    |
|    14105    |    10    |    No    |    You have updated the distribution database property '%s' successfully.    |
|    14106    |    16    |    No    |    Distribution retention periods must be greater than or equal to 0.    |
|    14107    |    10    |    No    |    The \@max_distretention value must be larger than the \@min_distretention value.    |
|    14108    |    10    |    No    |    Removed %ld history records from %s.    |
|    14109    |    10    |    No    |    The \@security_mode parameter value must be 0 (SQL Server Authentication) or 1 (Windows Authentication).    |
|    14110    |    16    |    No    |    For stored procedure articles, the \@property parameter value must be 'description', 'dest_table', 'dest_object', 'creation_script', 'pre_creation_cmd', 'schema_option', or 'destination_owner'.    |
|    14111    |    16    |    No    |    The \@pre_creation_cmd parameter value must be 'none' or 'drop'.    |
|    14112    |    16    |    No    |    This procedure can be executed only against table-based articles.    |
|    14113    |    16    |    No    |    Could not execute '%s'. Check '%s' in the install directory.    |
|    14114    |    16    |    No    |    The server '%s' is not configured as a Distributor.    |
|    14115    |    16    |    No    |    The property parameter value must be %s.    |
|    14117    |    16    |    No    |    '%s' is not configured as a distribution database.    |
|    14118    |    16    |    No    |    A stored procedure can be published only as a 'serializable proc exec' article, a 'proc exec' article, or a 'proc schema only' article.    |
|    14119    |    16    |    No    |    Could not add the distribution database '%s'. This distribution database already exists.    |
|    14120    |    16    |    No    |    Could not drop the distribution database '%s'. This distributor database is associated with a Publisher.    |
|    14121    |    16    |    No    |    Could not drop the Distributor '%s'. This Distributor has associated distribution databases.    |
|    14122    |    16    |    No    |    The \@article parameter value must be 'all' for immediate_sync publications.    |
|    14123    |    16    |    No    |    The subscription \@sync_type parameter value 'manual' is no longer supported.    |
|    14124    |    16    |    No    |    A publication must have at least one article before a subscription to it can be created.    |
|    14126    |    16    |    No    |    You do not have the required permissions to complete the operation.    |
|    14128    |    16    |    No    |    Invalid \@subscription_type parameter value. Valid options are 'push' or 'pull'.    |
|    14129    |    16    |    No    |    The \@status parameter value must be NULL for 'automatic' sync_type when you add subscriptions to an immediate_sync publication.    |
|    14135    |    16    |    No    |    There is no subscription on Publisher '%s', publisher database '%s', publication '%s'.    |
|    14136    |    16    |    No    |    The keyword 'all' is reserved by replication stored procedures.    |
|    14137    |    16    |    No    |    The \@value parameter value must be either 'true' or 'false'.    |
|    14138    |    16    |    No    |    Invalid option name '%s'.    |
|    14139    |    16    |    No    |    The replication system table '%s' already exists.    |
|    14143    |    16    |    No    |    Cannot drop Distributor Publisher '%s'. There are Subscribers associated with it in the distribution database '%s'.    |
|    14144    |    16    |    No    |    Cannot drop Subscriber '%s'. There are subscriptions for it in the publication database '%s'.    |
|    14146    |    16    |    No    |    The article parameter '\@schema_option' cannot be NULL.    |
|    14147    |    16    |    No    |    Restricted publications are no longer supported.    |
|    14148    |    16    |    No    |    Invalid '%s' value. Valid values are 'true' or 'false'.    |
|    14149    |    10    |    No    |    Removed %ld replication history records in %s seconds (%ld row/secs).    |
|    14150    |    10    |    No    |    Replication-%s: agent %s succeeded. %s    |
|    14151    |    18    |    Yes    |    Replication-%s: agent %s failed. %s    |
|    14152    |    10    |    Yes    |    Replication-%s: agent %s scheduled for retry. %s    |
|    14153    |    10    |    No    |    Replication-%s: agent %s warning. %s    |
|    14154    |    16    |    No    |    The Distributor parameter must be '\@heartbeat_interval'.    |
|    14155    |    16    |    No    |    Invalid article ID specified for procedure script generation.    |
|    14156    |    16    |    No    |    The custom stored procedure calling the format for the %s command specified in the article definition does not match the %s format.    |
|    14157    |    10    |    Yes    |    The subscription created by Subscriber '%s' to publication '%s' has expired and has been dropped.    |
|    14158    |    10    |    No    |    Replication-%s: agent %s: %s.    |
|    14159    |    16    |    No    |    Could not change property '%s' for article '%s' because there is an existing subscription to the article.    |
|    14160    |    10    |    Yes    |    One or more subscriptions has exceeded the threshold [%s:%s] for the publication [%s]. Check the status of subscriptions to this publication and change the expiration threshold value if necessary.    |
|    14161    |    10    |    Yes    |    The threshold [%s:%s] for the publication [%s] has been set. Make sure that the logreader and distribution agents are running and can match the latency requirement.    |
|    14162    |    10    |    Yes    |    One or more subscriptions has exceeded the threshold [%s:%s] for the publication [%s]. Check the status of subscriptions to this publication and adjust the threshold value if necessary.    |
|    14163    |    10    |    Yes    |    One or more subscriptions has exceeded the threshold [%s:%s] for the publication [%s]. Check the status of subscriptions to this publication and adjust the threshold value if necessary.    |
|    14164    |    10    |    Yes    |    One or more subscriptions has exceeded the threshold [%s:%s] for the publication [%s]. Check the status of subscriptions to this publication and adjust the threshold value if necessary.    |
|    14165    |    10    |    Yes    |    One or more subscriptions has exceeded the threshold [%s:%s] for the publication [%s]. Check the status of subscriptions to this publication and adjust the threshold value if necessary.    |
|    14166    |    10    |    No    |    Disable publishing ignored error msg %d, severity %d, state %d: %s.    |
|    14167    |    10    |    No    |    Subscription expiration    |
|    14168    |    10    |    No    |    Transactional replication latency    |
|    14169    |    10    |    No    |    Long merge over dialup connection    |
|    14170    |    10    |    No    |    Long merge over LAN connection    |
|    14171    |    10    |    No    |    Slow merge over LAN connection    |
|    14172    |    10    |    No    |    Slow merge over dialup connection    |
|    14173    |    16    |    No    |    Failed to set publication parameter %s because it is not supported in Workgroup sku.    |
|    14196    |    10    |    No    |    The agent has never been run.    |
|    14197    |    10    |    No    |    Value of %s parameter should be in the set %s    |
|    14198    |    10    |    No    |    The value of the %s parameter should be in the range %s. Verify that the specified parameter value is correct.    |
|    14199    |    10    |    No    |    The specified job '%s' is not created for maintenance plans. Verify that the job has at least one step calling xp_sqlmaint.    |
|    14200    |    16    |    No    |    The specified '%s' is invalid.    |
|    14201    |    10    |    No    |    0 (all steps) ..    |
|    14202    |    10    |    No    |    before or after \@active_start_time    |
|    14203    |    10    |    No    |    sp_helplogins [excluding Windows NT groups]    |
|    14204    |    10    |    No    |    0 (non-idle), 1 (executing), 2 (waiting for thread), 3 (between retries), 4 (idle), 5 (suspended), 7 (performing completion actions|
|    14205    |    10    |    No    |    (unknown)    |
|    14206    |    10    |    No    |    0..n seconds    |
|    14207    |    10    |    No    |    -1 [no maximum], 0..n    |
|    14208    |    10    |    No    |    1..7 [1 = E-mail, 2 = Pager, 4 = NetSend]    |
|    14209    |    10    |    No    |    0..127 [1 = Sunday .. 64 = Saturday]    |
|    14210    |    10    |    No    |    notification    |
|    14211    |    10    |    No    |    server    |
|    14212    |    10    |    No    |    (all jobs)    |
|    14213    |    10    |    No    |    Core Job Details:    |
|    14214    |    10    |    No    |    Job Steps:    |
|    14215    |    10    |    No    |    Job Schedules:    |
|    14216    |    10    |    No    |    Job Target Servers:    |
|    14217    |    10    |    No    |    SQL Server Warning: '%s' has performed a forced defection of TSX server '%s'. Run sp_delete_targetserver at the MSX in order to complete the defection.    |
|    14218    |    10    |    No    |    hour    |
|    14219    |    10    |    No    |    minute    |
|    14220    |    10    |    No    |    second    |
|    14221    |    16    |    No    |    This job has one or more notifications to operators other than '%s'. The job cannot be targeted at remote servers as currently defined.    |
|    14222    |    16    |    No    |    Cannot rename the '%s' operator.    |
|    14223    |    16    |    No    |    Cannot modify or delete operator '%s' while this server is a %s.    |
|    14224    |    10    |    No    |    Warning: The server name given is not the current MSX server ('%s').    |
|    14225    |    16    |    No    |    Warning: Could not determine local machine name. This prevents MSX operations from being posted.    |
|    14226    |    10    |    No    |    %ld history entries purged.    |
|    14227    |    10    |    No    |    Server defected from MSX '%s'. %ld job(s) deleted.    |
|    14228    |    10    |    No    |    Server MSX enlistment changed from '%s' to '%s'.    |
|    14229    |    10    |    No    |    Server enlisted into MSX '%s'.    |
|    14230    |    10    |    No    |    SP_POST_MSX_OPERATION: %ld %s download instruction(s) posted.    |
|    14231    |    10    |    No    |    SP_POST_MSX_OPERATION Warning: The specified %s ('%s') is not involved in a multiserver job.    |
|    14232    |    16    |    No    |    Specify either a job_name, job_id, or an originating_server.    |
|    14233    |    16    |    No    |    Specify a valid job_id (or 0x00 for all jobs).    |
|    14234    |    16    |    No    |    The specified '%s' is invalid (valid values are returned by %s).    |
|    14235    |    16    |    No    |    The specified '%s' is invalid (valid values are greater than 0 but excluding %ld).    |
|    14236    |    10    |    No    |    Warning: Non-existent step referenced by %s.    |
|    14237    |    16    |    No    |    When an action of 'REASSIGN' is specified, the New Login parameter must also be supplied.    |
|    14238    |    10    |    No    |    %ld jobs deleted.    |
|    14239    |    10    |    No    |    %ld jobs reassigned to %s.    |
|    14240    |    10    |    No    |    Job applied to %ld new servers.    |
|    14241    |    10    |    No    |    Job removed from %ld servers.    |
|    14242    |    16    |    No    |    Only a system administrator can reassign ownership of a job.    |
|    14243    |    10    |    No    |    Job '%s' started successfully.    |
|    14245    |    16    |    No    |    Specify either the \@name, \@id, or \@loginname of the task(s) to be deleted.    |
|    14250    |    16    |    No    |    The specified %s is too long. It must contain no more than %ld characters.    |
|    14251    |    16    |    No    |    Cannot specify '%s' as the operator to be notified.    |
|    14252    |    16    |    No    |    Cannot perform this action on a job you do not own.    |
|    14253    |    10    |    No    |    %ld (of %ld) job(s) stopped successfully.    |
|    14254    |    10    |    No    |    Job '%s' stopped successfully.    |
|    14255    |    16    |    No    |    The owner ('%s') of this job is either an invalid login, or is not a valid user of database '%s'.    |
|    14256    |    16    |    No    |    Cannot start the job "%s" (ID %s) because it does not have any job server or servers defined. Associate the job with a job server by calling sp_add_jobserver.    |
|    14257    |    16    |    No    |    Cannot stop the job "%s" (ID %s) because it does not have any job server or servers defined. Associate the job with a job server by calling sp_add_jobserver.    |
|    14258    |    16    |    No    |    Cannot perform this operation while SQLServerAgent is starting. Try again later.    |
|    14260    |    16    |    No    |    You do not have sufficient permission to run this command. Contact your system administrator.    |
|    14261    |    16    |    No    |    The specified %s ('%s') already exists.    |
|    14262    |    16    |    No    |    The specified %s ('%s') does not exist.    |
|    14263    |    16    |    No    |    Target server '%s' is already a member of group '%s'.    |
|    14264    |    16    |    No    |    Target server '%s' is not a member of group '%s'.    |
|    14265    |    24    |    Yes    |    The MSSQLServer service terminated unexpectedly. Check the SQL Server error log and Windows System and Application event logs for possible causes.    |
|    14266    |    16    |    No    |    The specified '%s' is invalid (valid values are: %s).    |
|    14267    |    16    |    No    |    Cannot add a job to the '%s' job category.    |
|    14268    |    16    |    No    |    There are no jobs at this server that originated from server '%s'.    |
|    14269    |    16    |    No    |    Job '%s' is already targeted at server '%s'.    |
|    14270    |    16    |    No    |    Job '%s' is not currently targeted at server '%s'.    |
|    14271    |    16    |    No    |    A target server cannot be named '%s'.    |
|    14272    |    16    |    No    |    Object-type and object-name must be supplied as a pair.    |
|    14273    |    16    |    No    |    You must provide either \@job_id or \@job_name (and, optionally, \@schedule_name), or \@schedule_id.    |
|    14274    |    16    |    No    |    Cannot add, update, or delete a job (or its steps or schedules) that originated from an MSX server.    |
|    14275    |    16    |    No    |    The originating server must be either local server or MSX server.    |
|    14276    |    16    |    No    |    '%s' is a permanent %s category and cannot be deleted.    |
|    14277    |    16    |    No    |    The command script does not destroy all the objects that it creates. Revise the command script.    |
|    14278    |    16    |    No    |    The schedule for this job is invalid (reason: %s).    |
|    14279    |    16    |    No    |    Supply either \@job_name, \@job_id or \@originating_server.    |
|    14280    |    16    |    No    |    Supply either a job name (and job aspect), or one or more job filter parameters.    |
|    14281    |    10    |    No    |    Warning: The \@new_owner_login_name parameter is not necessary when specifying a 'DELETE' action.    |
|    14282    |    16    |    No    |    Supply either a date (created or last modified) and a data comparator, or no date parameters at all.    |
|    14283    |    16    |    No    |    Supply \@target_server_groups or \@target_servers, or both.    |
|    14284    |    16    |    No    |    Cannot specify a job ID for a new job. An ID will be assigned by the procedure.    |
|    14285    |    16    |    No    |    Cannot add a local job to a multiserver job category.    |
|    14286    |    16    |    No    |    Cannot add a multiserver job to a local job category.    |
|    14287    |    16    |    No    |    The '%s' supplied has an invalid %s.    |
|    14288    |    16    |    No    |    %s cannot be before %s.    |
|    14289    |    16    |    No    |    %s cannot contain '%s' characters.    |
|    14290    |    16    |    No    |    This job is currently targeted at the local server so cannot also be targeted at a remote server.    |
|    14291    |    16    |    No    |    This job is currently targeted at a remote server so cannot also be targeted at the local server.    |
|    14292    |    16    |    No    |    There are two or more tasks named '%s'. Specify %s instead of %s to uniquely identify the task.    |
|    14293    |    16    |    No    |    There are two or more jobs named '%s'. Specify %s instead of %s to uniquely identify the job.    |
|    14294    |    16    |    No    |    Supply either %s or %s to identify the job.    |
|    14295    |    16    |    No    |    Frequency Type 0x2 (OnDemand) is no longer supported.    |
|    14296    |    16    |    No    |    This server is already enlisted into MSX '%s'.    |
|    14297    |    16    |    No    |    Cannot enlist into the local machine.    |
|    14298    |    16    |    No    |    This server is not currently enlisted into an MSX.    |
|    14299    |    16    |    No    |    Server '%s' is an MSX. Cannot enlist one MSX into another MSX.    |
|    14301    |    16    |    No    |    Logins other than the current user can only be seen by members of the sysadmin role.    |
|    14303    |    16    |    No    |    Stored procedure '%s' failed to access registry key.    |
|    14304    |    16    |    No    |    Stored procedure '%s' can run only on Windows 2000 servers.    |
|    14305    |    16    |    No    |    Column '%.*ls' does not exist in table '%.*ls'.    |
|    14306    |    16    |    No    |    The target server (TSX) version is not compatible with the master server (MSX) version (%ld.%ld.%ld).    |
|    14307    |    16    |    No    |    Access to Integration Services package '%s' is denied.    |
|    14350    |    16    |    No    |    Cannot initialize COM library because CoInitialize failed.    |
|    14351    |    16    |    No    |    Cannot complete this operation because an unexpected error occurred.    |
|    14352    |    16    |    No    |    Cannot find Active Directory information in the registry for this SQL Server instance. Run sp_ActiveDirectory_SCP again.    |
|    14353    |    16    |    No    |    Cannot determine the service account for this SQL Server instance.    |
|    14354    |    16    |    No    |    Cannot start the MSSQLServerADHelper service. Verify that the service account for this SQL Server instance has the necessary permissions to start the MSSQLServerADHelper service.    |
|    14355    |    16    |    No    |    The MSSQLServerADHelper service is busy. Retry this operation later.    |
|    14356    |    16    |    No    |    The Windows Active Directory client is not installed properly on the computer where this SQL Server instance is running. LoadLibrary failed to load ACTIVEDS.DLL.    |
|    14357    |    16    |    No    |    Cannot list '%s' in Active Directory because the name is too long. Active Directory common names cannot exceed 64 characters.    |
|    14359    |    16    |    No    |    Active Directory directory service is not enabled on the network, or it is not supported by the operating system.    |
|    14360    |    16    |    No    |    %s is already configured as TSX machine    |
|    14362    |    16    |    No    |    The MSX server must be running the Standard or Enterprise edition of SQL Server    |
|    14363    |    16    |    No    |    The MSX server is not prepared for enlistments [there must be an operator named 'MSXOperator' defined at the MSX]    |
|    14364    |    16    |    No    |    The TSX server is not currently enlisted    |
|    14365    |    16    |    No    |    Specify a valid schedule_uid.    |
|    14366    |    16    |    No    |    Only members of sysadmin role can modify the owner of a schedule.    |
|    14367    |    16    |    No    |    One or more schedules were not deleted because they are being used by at least one other job. Use "sp_detach_schedule" to remove schedules from a job.    |
|    14368    |    16    |    No    |    Schedule "%s" was not deleted because it is being used by at least one other job. Use "sp_detach_schedule" to remove schedules from a job.    |
|    14369    |    16    |    No    |    The schedule ID "%s" is used by more than one job. Specify the job_id.    |
|    14370    |    16    |    No    |    The \@originating_server must be either the local server name or the master server (MSX) name for MSX jobs on a target server (TSX).    |
|    14371    |    16    |    No    |    There are two or more schedules named "%s". Specify %s instead of %s to uniquely identify the schedule.    |
|    14372    |    16    |    No    |    The schedule was not deleted because it is being used by one or more jobs.    |
|    14373    |    16    |    No    |    Supply either %s or %s to identify the schedule.    |
|    14374    |    16    |    No    |    The specified schedule name "%s" is not associated with the job "%s".    |
|    14375    |    16    |    No    |    More than one schedule named "%s" is attached to job "%s". Use "sp_update_schedule" to update schedules.    |
|    14376    |    16    |    No    |    More than one schedule named "%s" is attached to job "%s". Use "sp_detach_schedule" to remove schedules from a job.    |
|    14377    |    16    |    No    |    The schedule was not attached to the specified job. The schedule owner and the job owner must be the same or the operation must be performed by a sysadmin.    |
|    14378    |    16    |    No    |    \@sysadmin_only flag is no longer supported by SQLAgent and kept here only for backwards compatibility    |
|    14379    |    16    |    No    |    Table '%s' foreign key 'originating_server_id' does not have a matching value in the referenced view 'dbo.sysoriginatingservers_view'.    |
|    14380    |    16    |    No    |    Field 'originating_server_id' in table sysoriginatingservers is being referenced by either sysjobs or sysschedules.    |
|    14390    |    16    |    No    |    Only members of role sysadmin can specify the %s parameter.    |
|    14391    |    16    |    No    |    Only owner of a job or members of sysadmin role can detach a schedule.    |
|    14392    |    16    |    No    |    Only owner of a job or members of role sysadmin or SQLAgentOperatorRole can purge history of the job.    |
|    14393    |    16    |    No    |    Only owner of a job or members of role sysadmin or SQLAgentOperatorRole can start and stop the job.    |
|    14394    |    16    |    No    |    Only owner of a job schedule or members of sysadmin role can modify or delete the job schedule.    |
|    14395    |    16    |    No    |    '%s' is a member of sysadmin server role and cannot be granted to or revoked from the proxy. Members of sysadmin server role are allowed to use any proxy.    |
|    14396    |    16    |    No    |    Only members of sysadmin server role can modify multi-server jobs    |
|    14397    |    16    |    No    |    Only members of sysadmin server role can start/stop multi-server jobs    |
|    14398    |    16    |    No    |    Only members of sysadmin server role can create multi-server jobs    |
|    14410    |    16    |    No    |    You must supply either a plan_name or a plan_id.    |
|    14411    |    16    |    No    |    Cannot delete this plan. The plan contains enlisted databases.    |
|    14412    |    16    |    No    |    The destination database is already part of a log shipping plan.    |
|    14413    |    16    |    No    |    This database is already log shipping.    |
|    14414    |    16    |    No    |    A log shipping monitor is already defined.    |
|    14415    |    16    |    No    |    The user name cannot be null when using SQL Server authentication.    |
|    14416    |    16    |    No    |    This stored procedure must be run in msdb.    |
|    14417    |    16    |    No    |    Cannot delete the monitor server while databases are participating in log shipping.    |
|    14418    |    16    |    No    |    The specified \@backup_file_name was not created from database '%s'.    |
|    14419    |    16    |    No    |    The specified \@backup_file_name is not a database backup.    |
|    [14420](../../relational-databases/errors-events/mssqlserver-14420-database-engine-error.md)    |    16    |    Yes    |    The log shipping primary database %s.%s has backup threshold of %d minutes and has not performed a backup log operation for %d minutes. Check agent log and logshipping monitor information.    |
|    [14421](../../relational-databases/errors-events/mssqlserver-14421-database-engine-error.md)    |    16    |    Yes    |    The log shipping secondary database %s.%s has restore threshold of %d minutes and is out of sync. No restore was performed for %d minutes. Restored latency is %d minutes. Check agent log and logshipping monitor information.    |
|    14422    |    16    |    No    |    Supply either \@plan_id or \@plan_name.    |
|    14423    |    16    |    No    |    Other databases are enlisted on this plan and must be removed before the plan can be deleted.    |
|    14424    |    16    |    No    |    The database '%s' is already involved in log shipping.    |
|    14425    |    16    |    No    |    The database '%s' does not seem to be involved in log shipping.    |
|    14426    |    16    |    No    |    A log shipping monitor is already defined. Call sp_define_log_shipping_monitor with \@delete_existing = 1.    |
|    14427    |    16    |    No    |    A user name is necessary for SQL Server security.    |
|    14428    |    16    |    No    |    Could not remove the monitor as there are still databases involved in log shipping.    |
|    14429    |    16    |    No    |    There are still secondary servers attached to this primary.    |
|    14430    |    16    |    No    |    Destination path %s is not valid. Unable to list directory contents. Specify a valid destination path.    |
|    14440    |    16    |    No    |    Could not set single user mode.    |
|    14441    |    16    |    No    |    Role change succeeded.    |
|    14442    |    16    |    No    |    Role change failed.    |
|    14450    |    16    |    No    |    The specified \@backup_file_name was not taken from database '%s'.    |
|    14451    |    16    |    No    |    The specified \@backup_file_name is not a database backup.    |
|    14500    |    16    |    No    |    Supply either a non-zero message ID, non-zero severity, non-null performance condition, or non-null WMI namespace and query.    |
|    14501    |    16    |    No    |    An alert ('%s') has already been defined on this condition.    |
|    14502    |    16    |    No    |    The \@target_name parameter must be supplied when specifying an \@enum_type of 'TARGET'.    |
|    14503    |    16    |    No    |    The \@target_name parameter should not be supplied when specifying an \@enum_type of 'ALL' or 'ACTUAL'.    |
|    14504    |    16    |    No    |    '%s' is the fail-safe operator. You must make another operator the fail-safe operator before '%s' can be dropped.    |
|    14505    |    16    |    No    |    Specify a null %s when supplying a performance condition.    |
|    14506    |    16    |    No    |    Cannot set alerts on message ID %ld.    |
|    14507    |    16    |    No    |    A performance condition must be formatted as: 'object_name\|counter_name\|instance_name\|comparator(> or \< or =)\|numeric value'.    |
|    14508    |    16    |    No    |    Specify both \@wmi_namespace and \@wmi_query.    |
|    14509    |    16    |    No    |    Specify a valid %s when supplying a \@wmi_namespace.    |
|    14510    |    16    |    No    |    Specify a null %s when supplying a \@wmi_namespace.    |
|    14511    |    16    |    No    |    The \@wmi_query could not be executed in the \@wmi_namespace provided. Verify that an event class selected in the query exists in the namespace and that the query has the correct syntax.    |
|    14512    |    16    |    No    |    Specify a valid %s when supplying a \@wmi_query.    |
|    14513    |    10    |    No    |    Analysis query subsystem    |
|    14514    |    10    |    No    |    Analysis command subsystem    |
|    14515    |    16    |    No    |    Only a member of the sysadmin server role can add a job for a different owner with \@owner_login_name.    |
|    14516    |    16    |    No    |    Proxy (%d) is not allowed for subsystem "%s" and user "%s". Grant permission by calling sp_grant_proxy_to_subsystem or sp_grant_login_to_proxy.    |
|    14517    |    16    |    No    |    A proxy account is not allowed for a Transact-SQL subsystem.    |
|    14518    |    16    |    No    |    Cannot delete proxy (%d). It is used by at least one jobstep. Change this proxy for all jobsteps first.    |
|    14519    |    16    |    No    |    Only one of \@login_name, \@fixed_server_role, or \@msdb_role should be specified.    |
|    14520    |    16    |    No    |    %s is not a valid SQL Server standard login, Windows NT user, Windows NT group, or msdb database role.    |
|    14521    |    16    |    No    |    %s is not a valid SQL Server fixed server role, Windows NT user, or Windows NT group.    |
|    14522    |    16    |    No    |    '"%s" is not a valid role of an msdb database, Windows NT user, or Windows NT group.    |
|    14523    |    16    |    No    |    %s has not been granted permission to use proxy "%s".    |
|    14524    |    16    |    No    |    Supply either %s or %s.    |
|    14525    |    16    |    No    |    Only members of sysadmin role are allowed to update or delete jobs owned by a different login.    |
|    14526    |    16    |    No    |    The specified category "%s" does not exist for category class "%s".    |
|    14527    |    16    |    No    |    Job "%s" cannot be used by an alert. It should first be associated with a server by calling sp_add_jobserver.    |
|    14528    |    16    |    No    |    Job "%s" has no steps defined.    |
|    14529    |    16    |    No    |    Proxy "%s" is not a valid Windows user.    |
|    14530    |    16    |    No    |    The Transact-SQL subsystem cannot be executed under the context of a proxy account.    |
|    14531    |    16    |    No    |    Permission to access proxy already granted. Verify current permissions assignments.    |
|    14532    |    16    |    No    |    Supply both %s and %s, or none of them.    |
|    14533    |    16    |    No    |    Use either a proxy or user_domain, user_name, or user_password parameter.    |
|    14534    |    16    |    No    |    All user_domain, user_name, and user_password parameters should be defined.    |
|    14535    |    16    |    No    |    The user_domain, user_name, and user_password parameters can be specified only for replication subsystems.    |
|    14536    |    16    |    No    |    Only members of the sysadmin role can specify a "%s" parameter.    |
|    14537    |    16    |    No    |    Execution in the context of disabled proxy (proxy_id = %d) is not allowed. Contact your system administrator.    |
|    14538    |    10    |    No    |    SSIS package execution subsystem    |
|    14539    |    16    |    No    |    Only a Standard or Enterprise edition of SQL Server can be enlisted into an MSX.    |
|    14540    |    16    |    No    |    Only a SQL Server running on Microsoft Windows NT can be enlisted into an MSX.    |
|    14541    |    16    |    No    |    The version of the MSX (%s) is not recent enough to support this TSX. Version %s or later is required at the MSX.    |
|    14542    |    16    |    No    |    It is invalid for any TSQL step of a multiserver job to have a non-null %s value.    |
|    14543    |    16    |    No    |    Login '%s' owns one or more multiserver jobs. Ownership of these jobs can only be assigned to members of the %s role.    |
|    14544    |    16    |    No    |    This job is owned by '%s'. Only a job owned by a member of the %s role can be a multiserver job.    |
|    14545    |    16    |    No    |    The %s parameter is not valid for a job step of type '%s'.    |
|    14546    |    16    |    No    |    The %s parameter is not supported on Windows 95/98 platforms.    |
|    14547    |    10    |    No    |    Warning: This change will not be downloaded by the target server(s) until an %s for the job is posted using %s.    |
|    14548    |    10    |    No    |    Target server '%s' does not have any jobs assigned to it.    |
|    14549    |    10    |    No    |    (Description not requested.)    |
|    14550    |    10    |    No    |    Command-Line Subsystem    |
|    14551    |    10    |    No    |    Replication Snapshot Subsystem    |
|    14552    |    10    |    No    |    Replication Transaction-Log Reader Subsystem    |
|    14553    |    10    |    No    |    Replication Distribution Subsystem    |
|    14554    |    10    |    No    |    Replication Merge Subsystem    |
|    14555    |    10    |    No    |    Active Scripting Subsystem    |
|    14556    |    10    |    No    |    Transact-SQL Subsystem    |
|    14557    |    10    |    No    |    [Internal]    |
|    14558    |    10    |    No    |    (encrypted command)    |
|    14559    |    10    |    No    |    (append output file)    |
|    14560    |    10    |    No    |    (include results in history)    |
|    14561    |    10    |    No    |    (normal)    |
|    14562    |    10    |    No    |    (quit with success)    |
|    14563    |    10    |    No    |    (quit with failure)    |
|    14564    |    10    |    No    |    (goto next step)    |
|    14565    |    10    |    No    |    (goto step)    |
|    14566    |    10    |    No    |    (idle)    |
|    14567    |    10    |    No    |    (below normal)    |
|    14568    |    10    |    No    |    (above normal)    |
|    14569    |    10    |    No    |    (time critical)    |
|    14570    |    10    |    No    |    (Job outcome)    |
|    14571    |    10    |    No    |    No description available.    |
|    14572    |    10    |    No    |    \@freq_interval must be at least 1 for a daily job.    |
|    14573    |    10    |    No    |    \@freq_interval must be a valid day of the week bitmask [Sunday = 1 .. Saturday = 64] for a weekly job.    |
|    14574    |    10    |    No    |    \@freq_interval must be between 1 and 31 for a monthly job.    |
|    14575    |    10    |    No    |    \@freq_relative_interval must be one of 1st (0x1), 2nd (0x2), 3rd [0x4], 4th (0x8) or Last (0x10).    |
|    14576    |    10    |    No    |    \@freq_interval must be between 1 and 10 (1 = Sunday .. 7 = Saturday, 8 = Day, 9 = Weekday, 10 = Weekend-day) for a monthly-relative job.    |
|    14577    |    10    |    No    |    \@freq_recurrence_factor must be at least 1.    |
|    14578    |    10    |    No    |    Starts whenever the CPU usage has remained below %ld percent for %ld seconds.    |
|    14579    |    10    |    No    |    Automatically starts when SQLServerAgent starts.    |
|    14580    |    10    |    No    |    job    |
|    14581    |    10    |    No    |    Replication Transaction Queue Reader Subsystem    |
|    14582    |    16    |    No    |    Only a sysadmin can specify '\@output_file_name' parameter for a jobstep.    |
|    14583    |    16    |    No    |    Only a sysadmin can specify '\@database_user_name' parameter.    |
|    14585    |    16    |    No    |    Only the owner of DTS Package '%s' or a member of the sysadmin role may reassign its ownership.    |
|    14586    |    16    |    No    |    Only the owner of DTS Package '%s' or a member of the sysadmin role may create new versions of it.    |
|    14587    |    16    |    No    |    Only the owner of DTS Package '%s' or a member of the sysadmin role may drop it or any of its versions.    |
|    14588    |    10    |    No    |    ID.VersionID =    |
|    14589    |    10    |    No    |    [not specified]    |
|    14590    |    16    |    No    |    DTS Package '%s' already exists with a different ID in this category.    |
|    14591    |    16    |    No    |    SSIS folder '%s' already exists in the specified parent folder.    |
|    14592    |    16    |    No    |    DTS Category '%s' was found in multiple parent categories. You must uniquely specify the category to be dropped.    |
|    14593    |    16    |    No    |    SSIS folder '%s' contains packages and/or other folders. You must drop these first.    |
|    14594    |    10    |    No    |    DTS Package    |
|    14595    |    16    |    No    |    DTS Package '%s' exists in different categories. You must uniquely specify the package.    |
|    14596    |    16    |    No    |    DTS Package '%s' exists in another category.    |
|    14597    |    16    |    No    |    DTS Package ID '%s' already exists with a different name.    |
|    14598    |    16    |    No    |    Cannot drop the Local, Repository, or LocalDefault DTS categories.    |
|    14599    |    10    |    No    |    Name    |
|    14600    |    16    |    No    |    Proxy "%s" has not been granted permission to use subsystem "%s".    |
|    14601    |    16    |    No    |    Operator "%s" is not enabled and therefore cannot receive notifications.    |
|    14602    |    16    |    No    |    Operator "%s" does not have an e-mail address specified.    |
|    14603    |    16    |    No    |    Database Mail is not properly configured.    |
|    14604    |    16    |    No    |    Both %s parameters (id and name) cannot be NULL    |
|    14605    |    16    |    No    |    Both %s parameters (id and name) do not point to the same object    |
|    14606    |    16    |    No    |    %s id is not valid    |
|    14607    |    16    |    No    |    %s name is not valid    |
|    14608    |    16    |    No    |    Either %s or %s parameter needs to be supplied    |
|    14609    |    16    |    No    |    Mail database to user database association does not exist and therefore cannot be updated    |
|    14610    |    16    |    No    |    Either \@profile_name or \@description parameter needs to be specified for update    |
|    14611    |    16    |    No    |    Account sequence number must be supplied for update    |
|    14612    |    16    |    No    |    Each principal should have at least one default profile    |
|    14614    |    16    |    No    |    %s is not a valid mailserver_type    |
|    14615    |    16    |    No    |    The \@username parameter needs to be supplied if the \@password is supplied.    |
|    14616    |    16    |    No    |    Unable to retrieve the newly created credential [%s] from the credential store.    |
|    14617    |    16    |    No    |    Mail host database specified is invalid    |
|    14618    |    16    |    No    |    Parameter '%s' must be specified. This parameter cannot be NULL.    |
|    14619    |    16    |    No    |    Received an error on the Service Broker conversation with Database Mail. Database Mail may not be available, or may have encountered an error. Check the Database Mail error log for information.    |
|    14620    |    16    |    No    |    The Service Broker conversation to Database Mail ended without a response from Database Mail. Database Mail may not be available, or may have encountered an error. Check the Database Mail error log for more information.    |
|    14621    |    16    |    No    |    Parameter \@attachmentencoding does not support the value "%s". The attachment encoding must be "MIME".    |
|    14622    |    16    |    No    |    Parameter \@importance does not support the value "%s". Mail importance must be one of LOW, NORMAL, or HIGH.    |
|    14623    |    16    |    No    |    Parameter \@sensitivity does not support the value "%s". Mail sensitivity must be one of NORMAL, PERSONAL, PRIVATE, or CONFIDENTIAL.    |
|    14624    |    16    |    No    |    At least one of the following parameters must be specified. "%s".    |
|    [14265](../../relational-databases/errors-events/mssqlserver-14265-database-engine-error.md)    |    16    |    No    |    Parameter \@attach_query_result_as_file cannot be 1 (true) when no value is specified for parameter \@query. A query must be specified to attach the results of the query.    |
|    14626    |    16    |    No    |    Parameter \@mailformat does not support the value "%s". The mail format must be TEXT or HTML.    |
|    14627    |    16    |    No    |    Received error %d while sending a message as part of the %s operation. Database Mail may not be available, or may have encountered an error. Check the Database Mail error log for more information.    |
|    14628    |    16    |    No    |    The format of the parameter \@attachments is incorrect. The file names must be separated by a semicolon ";".    |
|    14629    |    16    |    No    |    There is no configuration parameter named "%s", or the value provided is not of the correct data type.    |
|    14630    |    16    |    No    |    Database Mail is not permitted to send files with the file extension %s.    |
|    14631    |    16    |    No    |    The current user ('%s') either does not have permission to access the database specified in the parameter \@execute_query_database or cannot impersonate the user specified in the parameter \@execute_query_as. Only members of the sysadmin fixed server role and members of the db_owner fixed database role can impersonate another user.    |
|    14632    |    16    |    No    |    The user name %s specified in \@execute_query_as is invalid. There is no user by that name.    |
|    14633    |    16    |    No    |    The database name "%s" specified in \@execute_query_database is invalid. There is no database by that name.    |
|    14634    |    10    |    No    |    Warning: %s'    |
|    14635    |    10    |    No    |    Mail queued.    |
|    14636    |    16    |    No    |    No global profile is configured. Specify a profile name in the \@profile_name parameter.    |
|    14637    |    10    |    No    |    Activation failure.    |
|    14638    |    10    |    No    |    Activation successful.    |
|    14639    |    10    |    No    |    The mail queue was started by login "%s".    |
|    14640    |    10    |    No    |    The mail queue stopped by login "%s".    |
|    14641    |    16    |    No    |    Mail not queued. Database Mail is stopped. Use sysmail_start_sp to start Database Mail.    |
|    14642    |    10    |    No    |    Default attachment encoding    |
|    14643    |    10    |    No    |    Default dialog lifetime    |
|    14644    |    10    |    No    |    Default maximum file size    |
|    14645    |    10    |    No    |    Extensions not allowed in outgoing mails    |
|    14646    |    10    |    No    |    Number of retry attempts for a mail server    |
|    14647    |    10    |    No    |    Delay between each retry attempt to mail server    |
|    14648    |    10    |    No    |    Minimum process lifetime in seconds    |
|    14649    |    16    |    No    |    Unable to test profile. Database Mail is stopped. Use sysmail_start_sp to start Database Mail.    |
|    14650    |    16    |    No    |    Service Broker message delivery is not enabled in this database. Use the ALTER DATABASE statement to enable Service Broker message delivery.    |
|    14651    |    16    |    No    |    Unable to test profile. Service Broker message delivery is not enabled in this database. Use the ALTER DATABASE statement to enable Service Broker message delivery.    |
|    14652    |    16    |    No    |    Invalid message received on the ExternalMailQueue. conversation_handle: %s. message_type_name: %s. message body: %s.    |
|    14653    |    16    |    No    |    Invalid %s value received on the ExternalMailQueue. conversation_handle: %s. message_type_name: %s. message body: %s.    |
|    14654    |    10    |    No    |    Unexpected message received on the ExternalMailQueue. conversation_handle: %s. message_type_name: %s. message body: %s.    |
|    14655    |    16    |    No    |    Invalid XML message format received on the ExternalMailQueue. conversation_handle: %s. message_type_name: %s. message body: %s.    |
|    14657    |    16    |    No    |    Mail not queued. Maximum number of mails per day (%ld) for login %s has been exceeded.    |
|    14658    |    16    |    No    |    Failed to retrieve SQLPath for syssubsystems population.    |
|    14659    |    16    |    No    |    Failed to retrieve VerSpecificRootDir for syssubsystems population.    |
|    14660    |    16    |    No    |    Database Compatibility Level is too low. Compatibility Level must be Version80 or higher.    |
|    14661    |    16    |    No    |    Query execution failed: %s    |
|    14662    |    10    |    No    |    mailitem_id on conversation %s was not found in the sysmail_send_retries table. This mail item will not be sent.    |
|    14663    |    10    |    No    |    Mail Id %d has exceeded the retry count. This mail item will not be sent.    |
|    14664    |    16    |    No    |    Database Mail logging level: normal - 1, extended - 2 (default), verbose - 3    |
|    14665    |    10    |    No    |    Mail items deletion is initiated by user "%s". %d items deleted.    |
|    14666    |    16    |    No    |    User name cannot be supplied when using default credentials    |
|    14667    |    16    |    No    |    Mail Id %d has been deleted from sysmail_mailitems table. This mail will not be sent.    |
|    14668    |    16    |    No    |    Deleting profile %s failed because there are some unsent emails associated with this profile, use force_delete option to force the deletion of the profile.    |
|    14670    |    16    |    No    |    Cannot delete the active collection set '%s'. Stop the collection set and then try to delete it again.    |
|    14671    |    16    |    No    |    Cannot update the name or the parameters of the collection item '%s' in the active collection set '%s'. Stop the collection set and then try to update the collection item again.    |
|    14672    |    16    |    No    |    Cannot delete the collection item '%s' in the active collection set '%s'. Stop the collection set and then try to delete the collection item again.    |
|    14673    |    16    |    No    |    Cannot delete the collector type '%s'. Delete all collection items associated with this collector type and then try to delete it again.    |
|    14674    |    16    |    No    |    Cannot upload data for the inactive collection set '%s'. Start the collection set and then try to upload the data again.    |
|    14675    |    16    |    No    |    Cannot update the name, target, proxy_id, logging_level, or collection_mode, or add collection item to the active collection set '%s'. Stop the collection set and then try to update it again.    |
|    14676    |    16    |    No    |    The user does not have permission to change '%s'. The user must be a member of data collector role '%s'.    |
|    14677    |    16    |    No    |    The user does not have permission to perform this operation. The user must be a member of data collector role '%s'.    |
|    14678    |    16    |    No    |    SQL Server Trace with id %d has been stopped and closed by external user. SQL Server Trace collector will attempt to re-create the trace.    |
|    14679    |    16    |    No    |    The specified %s (%s) is not valid in this data warehouse.    |
|    14680    |    16    |    No    |    Management Data Warehouse database can only be installed on an instance of SQL Server 2008 or higher.    |
|    14681    |    16    |    No    |    Cannot perform this procedure when the collector is disabled. Enable the collector and then try again.    |
|    14682    |    16    |    No    |    The state of the collection set has changed, but it will not start or stop until the collector is enabled.    |
|    14683    |    16    |    No    |    A collection set in cached mode requires a schedule.    |
|    14684    |    16    |    No    |    Caught error#: %d, Level: %d, State: %d, in Procedure: %s, Line: %d, with Message: %s    |
|    14685    |    16    |    No    |    Collection set: '%s' does not contain any collection items, so starting the collection set will have no effect.    |
|    14686    |    16    |    No    |    The MDWInstance and MDWDatabase parameters of the configuration store cannot be null.    |
|    14687    |    16    |    No    |    Invalid value (%d) of the \@cache_window parameter. Allowable values are: -1 (cache all upload data from previous upload failures), 0 (cache no upload data), N (cache data from N previous upload failures, where N >= 1)    |
|    14688    |    16    |    No    |    A collection set cannot start when SQL Server Agent is stopped. Start SQL Server Agent.    |
|    14689    |    16    |    No    |    A collection set cannot start if the management data warehouse is not configured. Run the instmdw.sql script to create and configure the management data warehouse.    |
|    14690    |    16    |    No    |    Cannot perform this procedure when the collector is enabled. Disable the collector and then try again.    |
|    14691    |    16    |    No    |    The status of the collector cannot be null. This may indicate an internal corruption in the collector configuration data.    |
|    14692    |    16    |    No    |    Insufficient privileges to start collection set: '%s'. Only a member of the 'sysadmin' fixed server role can start a collection set without a SQL Server Agent proxy. Attach a SQL Server Agent proxy to the collection set before retrying.    |
|    14693    |    16    |    No    |    A collection set cannot start without a schedule. Specify a schedule for the collection set.    |
|    14694    |    16    |    No    |    Cannot upload data on-demand for the collection set '%s' in non-cached mode.    |
|    14695    |    16    |    No    |    Cannot collect data on-demand for the collection set '%s' in cached mode.    |
|    14696    |    16    |    No    |    Cannot update or delete a system collection set, or add new collection items to it.    |
|    14697    |    16    |    No    |    Unable to convert showplan to XML. Error #%d on Line %d: %s    |
|    14698    |    10    |    No    |    PowerShell Subsystem    |
|    14700    |    10    |    No    |    Collects data about the disk and log usage for all databases.    |
|    14701    |    10    |    No    |    Disk Usage    |
|    14702    |    10    |    No    |    Disk Usage - Data Files    |
|    14703    |    10    |    No    |    Disk Usage - Log Files    |
|    14704    |    10    |    No    |    Collects top-level performance indicators for the computer and the Database Engine. Enables analysis of resource use, resource bottlenecks, and Database Engine activity.    |
|    14705    |    10    |    No    |    Server Activity    |
|    14706    |    10    |    No    |    Server Activity - DMV Snapshots    |
|    14707    |    10    |    No    |    Server Activity - Performance Counters    |
|    14708    |    10    |    No    |    Collects query statistics, T-SQL text, and query plans of most of the statements that affect performance. Enables analysis of poor performing queries in relation to overall SQL Server Database Engine activity.    |
|    14709    |    10    |    No    |    Query Statistics    |
|    14710    |    10    |    No    |    Query Statistics - Query Activity    |
|    14711    |    10    |    No    |    Changes to SQL dumper configuration will take effect when the collection set is restarted. To perform an immediate dump, use the dtutil /dump option.    |
|    14712    |    16    |    No    |    Only dbo or members of dc_admin can install or upgrade instmdw.sql. Contact an administrator with sufficient permissions to perform this operation.    |
|    14713    |    21    |    No    |    A management data warehouse cannot be installed to SQL Server Express Edition.    |
|    14714    |    16    |    No    |    Attempting to upgrade a Management Data Warehouse of newer version '%s' with an older version '%s'. Upgrade aborted.    |
