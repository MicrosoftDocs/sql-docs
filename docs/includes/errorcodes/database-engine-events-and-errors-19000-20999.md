---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    19030    |    10    |    Yes    |    SQL Trace ID %d was started by login "%s".    |
|    19031    |    10    |    Yes    |    SQL Trace stopped. Trace ID = '%d'. Login Name = '%s'.    |
|    19032    |    10    |    Yes    |    SQL Trace was stopped due to server shutdown. Trace ID = '%d'. This is an informational message only; no user action is required.    |
|    19033    |    10    |    Yes    |    Server started with '-f' option. Auditing will not be started. This is an informational message only; no user action is required.    |
|    19034    |    21    |    Yes    |    Cannot start C2 audit trace. SQL Server is shutting down. Error = %ls    |
|    19035    |    16    |    Yes    |    OLE task allocator failed to initialize. Heterogeneous queries, distributed queries, and remote procedure calls are unavailable. Confirm that DCOM is properly installed and configured.    |
|    19036    |    10    |    Yes    |    The OLE DB initialization service failed to load. Reinstall Microsoft Data Access Components. If the problem persists, contact product support for the OLEDB provider.    |
|    19051    |    16    |    No    |    Unknown error occurred in the trace.    |
|    19052    |    16    |    No    |    The active trace must be stopped before modification.    |
|    19053    |    16    |    No    |    The trace event ID is not valid.    |
|    19054    |    16    |    No    |    The trace column ID is not valid.    |
|    19055    |    16    |    No    |    Filters with the same event column ID must be grouped together.    |
|    19056    |    16    |    No    |    The comparison operator in the filter is not valid.    |
|    19057    |    16    |    No    |    The boolean operator in the filter is not valid.    |
|    19058    |    16    |    No    |    The trace status is not valid.    |
|    19059    |    16    |    No    |    Could not find the requested trace.    |
|    19060    |    16    |    No    |    The trace option is not valid.    |
|    19061    |    16    |    No    |    Cannot remove SPID trace column.    |
|    19062    |    16    |    No    |    Could not create a trace file.    |
|    19063    |    16    |    No    |    Not enough memory was available for trace.    |
|    19064    |    16    |    No    |    The requested trace stop time has been already passed.    |
|    19065    |    16    |    No    |    The parameter is not valid.    |
|    19066    |    16    |    No    |    Cannot modify a restricted trace.    |
|    19067    |    16    |    No    |    Cannot create a new trace because the trace file path is found in the existing traces.    |
|    19068    |    16    |    No    |    The trace file path is not valid or not supported.    |
|    19069    |    16    |    No    |    The trace file name is not valid because it contains a rollover file number (NNN in C:\file_NNN) while the trace rollover option is enabled.    |
|    19070    |    16    |    No    |    The default trace cannot be stopped or modified. Use SP_CONFIGURE to turn it off.    |
|    19071    |    16    |    No    |    Stopping the trace because the current trace file is full and the rollover option is not specified.    |
|    19096    |    16    |    No    |    Fail to delete an old trace file '%ls'. Error = '%ls'.    |
|    19097    |    10    |    No    |    Windows error occurred while running %s. Error = %s.    |
|    19098    |    16    |    Yes    |    An error occurred starting the default trace. Cause: %ls Use sp_configure to turn off and then turn on the 'default trace enabled' advanced server configuration option.    |
|    19099    |    16    |    Yes    |    Trace ID '%d' was stopped because of an error. Cause: %ls. Restart the trace after correcting the problem.    |
|    19100    |    10    |    No    |    Initialization succeeded.    |
|    19101    |    10    |    No    |    Initialization failed with an infrastructure error. Check for previous errors.    |
|    19102    |    10    |    No    |    Unable to create a node listener object. Check for memory-related errors.    |
|    19103    |    10    |    No    |    An error occurred while starting shared memory support.    |
|    19104    |    10    |    No    |    All protocols are disabled.    |
|    19105    |    10    |    No    |    Unable to create a node listener object for a special instance. Check for memory-related errors.    |
|    19106    |    10    |    No    |    Unable to trim spaces in an IP address. Check TCP/IP protocol settings.    |
|    19107    |    10    |    No    |    'TcpKeepAlive' registry setting is the wrong type. Check TCP/IP protocol settings.    |
|    19108    |    10    |    No    |    Unable to retrieve 'TcpKeepAlive' registry setting. Check TCP/IP protocol settings.    |
|    19109    |    10    |    No    |    Unable to configure MDAC-compatibility TCP/IP port in registry.    |
|    19110    |    10    |    No    |    Unable to initialize the TCP/IP listener.    |
|    19111    |    10    |    No    |    Unable to open TCP/IP protocol configuration key in registry.    |
|    19112    |    10    |    No    |    Unable to retrieve TCP/IP protocol 'Enabled' registry setting.    |
|    19113    |    10    |    No    |    Unable to retrieve 'ListenOnAllIPs' TCP/IP registry setting.    |
|    19114    |    10    |    No    |    Unable to open TCP/IP protocol's 'IPAll' configuration key in registry.    |
|    19115    |    10    |    No    |    Unable to retrieve registry settings from TCP/IP protocol's 'IPAll' configuration key.    |
|    19116    |    10    |    No    |    Unable to obtain list size for IP addresses configured for listening in registry.    |
|    19117    |    10    |    No    |    Failed to allocate memory for IP addresses configured for listening. Check for memory-related errors.    |
|    19118    |    10    |    No    |    Unable to obtain list of IP addresses configured for listening in registry.    |
|    19119    |    10    |    No    |    Unable to open TCP/IP protocol's registry key for a specific IP address.    |
|    19120    |    10    |    No    |    Unable to retrieve 'Enabled' setting for a specific IP address.    |
|    19121    |    10    |    No    |    Unable to retrieve 'Active' setting for a specific IP address.    |
|    19122    |    10    |    No    |    Unable to retrieve 'IpAddress' value for a specific IP address.    |
|    19123    |    10    |    No    |    'IpAddress' registry value is the wrong type.    |
|    19124    |    10    |    No    |    Unable to retrieve registry settings for a specific IP address.    |
|    19125    |    10    |    No    |    Unable to deallocate structures representing registry key for a specific IP address.    |
|    19126    |    10    |    No    |    Unable to retrieve registry settings for cluster environment.    |
|    19127    |    10    |    No    |    Server is configured to listen on a specific IP address in a cluster environment.    |
|    19128    |    10    |    No    |    Unable to check for a duplicate IP address in the network.    |
|    19129    |    10    |    No    |    A duplicate IP address was detected in the network.    |
|    19130    |    10    |    No    |    Unable to open SQL Server Network Interface library configuration key in registry for Dedicated Administrator Connection settings.    |
|    19131    |    10    |    No    |    Unable to open Dedicated Administrator Connection configuration key in registry.    |
|    19132    |    10    |    No    |    Unable to open TCP/IP configuration key for Dedicated Administrator Connection in registry.    |
|    19133    |    10    |    No    |    Unable to retrieve dynamic TCP/IP ports registry settings for Dedicated Administrator Connection.    |
|    19134    |    10    |    No    |    No or more than one dynamic TCP/IP port is configured for Dedicated Administrator Connection in registry settings.    |
|    19135    |    10    |    No    |    Error starting Named Pipes support. Check protocol settings.    |
|    19136    |    10    |    No    |    An error occurred starting VIA support. Check protocol settings.    |
|    19137    |    10    |    No    |    Failed to allocate memory SSL listening structures. Check for memory-related errors.    |
|    19138    |    10    |    No    |    An error occurred while obtaining or using the certificate for SSL. Check settings in Configuration Manager.    |
|    19139    |    10    |    No    |    Unable to add listener endpoints. Check for memory-related errors.    |
|    19140    |    10    |    No    |    Unable to initialize the communication listeners.    |
|    19141    |    10    |    No    |    Unable to retrieve SQL Server Network Interface library settings for a special instance.    |
|    19142    |    10    |    No    |    Unable to retrieve SQL Server Network Interface library settings; the instance name is too long.    |
|    19143    |    10    |    No    |    Unable to initialize the Shared Memory listener.    |
|    19144    |    10    |    No    |    Unable to initialize the Named Pipes listener.    |
|    19145    |    10    |    No    |    Unable to configure MDAC-compatibility Named Pipes protocol pipe name in registry.    |
|    19146    |    10    |    No    |    Unable to initialize the VIA listener.    |
|    19147    |    10    |    No    |    Unable to initialize the HTTP listener.    |
|    19148    |    10    |    No    |    Unable to initialize SSL support.    |
|    19149    |    10    |    No    |    Unable to configure MDAC-compatibility protocol list in registry.    |
|    19150    |    10    |    No    |    Unable to open SQL Server Network Interface library configuration key in registry.    |
|    20001    |    10    |    No    |    There is no nickname for article '%s' in publication '%s'.    |
|    20002    |    10    |    No    |    The filter '%s' already exists for article '%s' in publication '%s'.    |
|    20003    |    10    |    No    |    Could not generate nickname for '%s'.    |
|    20004    |    10    |    No    |    Publication '%s' has the following property: '%s'. SQL Server subscribers below version '%s' will ignore this setting.    |
|    20005    |    18    |    No    |    %ls: Cannot convert parameter %ls: Resulting colv would have too many entries.    |
|    20006    |    16    |    No    |    Cannot make the change because the article might be in a publication that has anonymous or client subscriptions. Set \@force_reinit_subscription to 1 to acknowledge that such a subscription will be reinitialized.    |
|    20007    |    16    |    No    |    The system tables for merge replication could not be dropped successfully.    |
|    20008    |    16    |    No    |    The system tables for merge replication could not be created successfully.    |
|    20009    |    16    |    No    |    The article '%s' could not be added to the publication '%s'.    |
|    20010    |    16    |    No    |    The Snapshot Agent corresponding to the publication '%s' could not be dropped.    |
|    20011    |    16    |    No    |    Cannot set incompatible publication properties. The 'allow_anonymous' property of a publication depends on the 'immediate_sync' property.    |
|    20012    |    16    |    No    |    The subscription type '%s' is not allowed on publication '%s'.    |
|    20013    |    16    |    No    |    The publication property '%s' cannot be changed when there are subscriptions on it.    |
|    20014    |    16    |    No    |    Invalid \@schema_option value.    |
|    20015    |    16    |    No    |    Could not remove directory '%ls'. Check the security context of xp_cmdshell and close other processes that may be accessing the directory.    |
|    20016    |    16    |    No    |    Invalid \@subscription_type value. Valid values are 'pull' or 'anonymous'.    |
|    20017    |    16    |    No    |    The subscription on the Subscriber does not exist.    |
|    20018    |    16    |    No    |    The \@optional_command_line is too long. Use an agent definition file.    |
|    20019    |    16    |    No    |    Replication database option '%s' cannot be set unless the database is a publishing database or a distribution database.    |
|    20020    |    16    |    No    |    The article resolver supplied is either invalid or nonexistent.    |
|    20021    |    16    |    No    |    The subscription could not be found.    |
|    20022    |    16    |    No    |    This article has now settings to disable uploads and compensate_for_errors=true. However, local and anonymous subscribers will behave as if compensate_for_errors=false.    |
|    20023    |    16    |    No    |    Invalid \@subscriber_type value. Valid options are 'local', 'global', or 'anonymous'.    |
|    20024    |    16    |    No    |    The alt_snapshot_folder cannot be the same as the working directory.    |
|    20025    |    16    |    No    |    The publication name must be unique. The specified publication name '%s' has already been used.    |
|    20026    |    16    |    No    |    The publication '%s' does not exist.    |
|    20027    |    16    |    No    |    The article '%s' does not exist.    |
|    20028    |    16    |    No    |    The Distributor has not been installed correctly. Could not enable database for publishing.    |
|    20029    |    16    |    No    |    The Distributor has not been installed correctly. Could not disable database for publishing.    |
|    20030    |    16    |    No    |    The article '%s' already exists on another publication with a different column tracking option.    |
|    20031    |    16    |    No    |    Could not delete the row because it does not exist.    |
|    20032    |    16    |    No    |    '%s' is not defined as a Subscriber for '%s'.    |
|    20033    |    16    |    No    |    Invalid publication type.    |
|    20034    |    16    |    No    |    Publication '%s' does not support '%s' subscriptions.    |
|    20036    |    16    |    No    |    The Distributor has not been installed correctly.    |
|    20037    |    16    |    No    |    The article '%s' already exists in another publication with a different article resolver.    |
|    20038    |    16    |    No    |    The article filter could not be added to the article '%s' in the publication '%s'.    |
|    20039    |    16    |    No    |    The article filter could not be dropped from the article '%s' in the publication '%s'.    |
|    20040    |    16    |    No    |    Could not drop the article(s) from the publication '%s'.    |
|    20041    |    16    |    No    |    Transaction rolled back. Could not execute trigger. Retry your transaction.    |
|    20043    |    16    |    No    |    Could not change the article '%s' because the publication has already been activated.    |
|    20044    |    16    |    No    |    The priority property is invalid for local subscribers.    |
|    20045    |    16    |    No    |    You must supply an article name.    |
|    20046    |    16    |    No    |    The article does not exist.    |
|    20047    |    16    |    No    |    You are not authorized to perform this operation.    |
|    20048    |    16    |    No    |    To modify the priority of a subscription, run sp_changemergesubscription at the Publisher instead of using sp_changemergepullsubscription at subscriber. This is for backward compatibility only.    |
|    20049    |    16    |    No    |    The priority value should not be larger than 100.0.    |
|    20050    |    16    |    No    |    The retention period must be greater than or equal to 0, and it must not extend past December 31, 9999.    |
|    20051    |    16    |    No    |    The Subscriber is not registered.    |
|    20052    |    16    |    No    |    The \@metatype parameter value must be null, 1, 2, 5, or 6.    |
|    20053    |    16    |    No    |    An article with a different %s value already exists for object '%s'.    |
|    20054    |    16    |    No    |    Current database is not enabled for publishing.    |
|    20055    |    16    |    No    |    Table '%s' cannot be published for merge replication because it has a timestamp column.    |
|    20056    |    16    |    No    |    Table '%s' cannot be republished.    |
|    20057    |    16    |    No    |    The profile name '%s' already exists for the specified agent type.    |
|    20058    |    16    |    No    |    The \@agent_type must be 1 (Snapshot), 2 (Logreader), 3 (Distribution), or 4 (Merge)    |
|    20059    |    16    |    No    |    The \@profile_type must be 0 (System) or 1 (Custom)    |
|    20060    |    16    |    No    |    Compatibility level cannot be smaller than 60.    |
|    20061    |    16    |    No    |    The compatibility level of this database must be set to 70 or higher to be enabled for merge publishing.    |
|    20062    |    16    |    No    |    Updating columns with the rowguidcol property is not allowed.    |
|    20063    |    16    |    No    |    Table '%s' into which you are trying to insert, update, or delete data has been marked as read-only. Only the merge process can perform these operations.    |
|    20064    |    16    |    No    |    Cannot drop profile. Either it is not defined or it is defined as the default profile.    |
|    20065    |    16    |    No    |    Cannot drop profile because it is in use.    |
|    20066    |    16    |    No    |    Profile not defined.    |
|    20067    |    16    |    No    |    The parameter name '%s' already exists for the specified profile.    |
|    20068    |    16    |    No    |    The article cannot be created on table '%s' because it has more than %d columns.    |
|    20069    |    16    |    No    |    Cannot validate a merge article that uses looping join filters.    |
|    20070    |    16    |    No    |    Cannot update subscription row.    |
|    20072    |    16    |    No    |    Cannot update Subscriber information row.    |
|    20073    |    16    |    No    |    Articles can be added or changed only at the Publisher.    |
|    20074    |    16    |    No    |    Only a table object can be published as a "table" article for merge replication.    |
|    20075    |    16    |    No    |    The 'status' parameter value must be either 'active' or 'unsynced'.    |
|    20076    |    16    |    No    |    The \@sync_mode parameter value must be 'native' or 'character'.    |
|    20077    |    16    |    No    |    Problem encountered generating replica nickname.    |
|    20078    |    16    |    No    |    The \@property parameter value must be one of the following: 'sync_type', 'priority', 'description', 'subscriber_security_mode', 'subscriber_login', 'subscriber_password', 'publisher_security_mode', 'publisher_login', 'publisher_password', 'merge_job_login', or 'merge_job_password'.    |
|    20079    |    16    |    No    |    Invalid \@subscription_type parameter value. Valid options are 'push', 'pull', or 'both'.    |
|    20081    |    16    |    No    |    Publication property '%s' cannot be NULL.    |
|    20084    |    16    |    No    |    Publication '%s' cannot be subscribed to by Subscriber database '%s'.    |
|    20086    |    16    |    No    |    Publication '%s' does not support the nosync type because it contains a table that does not have a rowguidcol column.    |
|    20087    |    16    |    No    |    You cannot push an anonymous subscription.    |
|    20088    |    16    |    No    |    Only assign priorities that are greater than or equal to 0 and less than 100.    |
|    20089    |    16    |    No    |    Could not get license information correctly.    |
|    20090    |    16    |    No    |    Could not get version information correctly.    |
|    20091    |    16    |    No    |    sp_mergesubscription_cleanup is used to clean up push subscriptions. Use sp_dropmergepullsubscription to clean up pull or anonymous subscriptions.    |
|    20092    |    16    |    No    |    Table '%s' into which you are trying to insert, update, or delete data is currently being upgraded or initialized for merge replication. On the publisher data modifications are disallowed until the upgrade completes and snapshot has successfully run. On subscriber data modifications are disallowed until the upgrade completes or the initial snapshot has been successfully applied and it has synchronized with the publisher.    |
|    20093    |    16    |    No    |    Merge replication upgrade is not complete until the snapshot agent is run for the publisher and the merge agent run for all the subscribers.    |
|    20100    |    16    |    No    |    Cannot drop Subscriber '%s'. There are existing subscriptions.    |
|    20500    |    16    |    No    |    The updatable Subscriber stored procedure '%s' does not exist.    |
|    20501    |    16    |    No    |    Could not insert into sysarticleupdates using sp_articlecolumn.    |
|    20502    |    16    |    No    |    Invalid '%s' value. Valid values are 'read only', 'sync tran', 'queued tran', or 'failover'.    |
|    20503    |    16    |    No    |    Invalid '%s' value in '%s'. The publication is not enabled for '%s' updatable subscriptions.    |
|    20504    |    16    |    No    |    Immediate Updating Subscriptions: The xml values inserted/updated by Subscriber will be replicated as NULL to publisher.    |
|    20505    |    16    |    No    |    Could not drop synchronous update stored procedure '%s' in '%s'.    |
|    20506    |    16    |    No    |    Source table '%s' not found in '%s'.    |
|    20507    |    16    |    No    |    Table '%s' not found in '%s'.    |
|    20508    |    11    |    No    |    Updatable subscriptions: The text, ntext, or image values inserted at the Subscriber will be NULL.    |
|    20509    |    16    |    No    |    Updatable subscriptions: The text, ntext, or image values cannot be updated at the Subscriber.    |
|    20510    |    16    |    No    |    Updateable Subscriptions: Cannot update identity columns.    |
|    20511    |    16    |    No    |    Updateable Subscriptions: Cannot update timestamp columns.    |
|    20512    |    16    |    No    |    Updateable Subscriptions: Rolling back transaction.    |
|    20513    |    16    |    No    |    Database '%s' does not contain any replication metadata for a row whose ROWGUIDCOL matches the value specified for the \@rowguid parameter of sp_showrowreplicainfo. Verify that the value specified for \@rowguid parameter is correct.    |
|    20514    |    10    |    No    |    A rowcount validation request has been submitted to heterogeneous publisher %s for article %s of publication %s. Validation results will be posted to distribution history.    |
|    20515    |    16    |    No    |    Updateable Subscriptions: Rows do not match between Publisher and Subscriber. Run the Distribution Agent to refresh rows at the Subscriber.    |
|    20516    |    16    |    No    |    Updateable Subscriptions: Replicated data is not updatable.    |
|    20518    |    16    |    No    |    Updateable Subscriptions: INSERT and DELETE operations are not supported unless published table has a timestamp column.    |
|    20519    |    16    |    No    |    Updateable Subscriptions: INSERT operations on tables with identity or timestamp columns are not allowed unless a primary key is defined at the Subscriber.    |
|    20520    |    16    |    No    |    Updateable Subscriptions: UPDATE operations on tables with identity or timestamp columns are not allowed unless a primary key is defined at the Subscriber.    |
|    20521    |    16    |    No    |    sp_MSmark_proc_norepl: must be a member of the db_owner or sysadmin roles.    |
|    20522    |    16    |    No    |    sp_MSmark_proc_norepl: invalid object name '%s'.    |
|    20523    |    16    |    No    |    Could not validate the article '%s'. It is not activated.    |
|    20524    |    10    |    No    |    Table '%s' may be out of synchronization. Rowcounts (actual: %s, expected: %s). Rowcount method %d used (0 = Full, 1 = Fast).    |
|    20525    |    10    |    No    |    Table '%s' might be out of synchronization. Rowcounts (actual: %s, expected %s). Checksum values (actual: %s, expected: %s).    |
|    20526    |    10    |    No    |    Table '%s' passed rowcount (%s) validation. Rowcount method %d used (0 = Full, 1 = Fast).    |
|    20527    |    10    |    No    |    Table '%s' passed rowcount (%s) and checksum validation. Checksum is not compared for any text or image columns.    |
|    20528    |    10    |    No    |    Log Reader Agent startup message.    |
|    20529    |    10    |    No    |    Starting agent.    |
|    20530    |    10    |    No    |    Run agent.    |
|    20531    |    10    |    No    |    Detect nonlogged agent shutdown.    |
|    20532    |    10    |    No    |    Replication agent schedule.    |
|    20533    |    10    |    No    |    Replication agents checkup    |
|    20534    |    10    |    No    |    Detects replication agents that are not logging history actively.    |
|    20535    |    10    |    No    |    Removes replication agent history from the distribution database.    |
|    20536    |    10    |    No    |    Replication: agent failure    |
|    20537    |    10    |    No    |    Replication: agent retry    |
|    20538    |    10    |    No    |    Replication: expired subscription dropped    |
|    20539    |    10    |    No    |    Replication Warning: %s (Threshold: %s)    |
|    20540    |    10    |    No    |    Replication: agent success    |
|    20541    |    10    |    No    |    Removes replicated transactions from the distribution database.    |
|    20542    |    10    |    No    |    Detects and removes expired subscriptions from published databases.    |
|    20543    |    10    |    No    |    \@rowcount_only parameter must be the value 0,1, or 2. 0=7.0 compatible checksum. 1=only check rowcounts. 2=new checksum functionality introduced in version 8.0.    |
|    20545    |    10    |    No    |    Default agent profile    |
|    20546    |    10    |    No    |    Verbose history agent profile.    |
|    20547    |    10    |    No    |    Agent profile for detailed history logging.    |
|    20548    |    10    |    No    |    Slow link agent profile.    |
|    20549    |    10    |    No    |    Agent profile for low bandwidth connections.    |
|    20550    |    10    |    No    |    Windows Synchronization Manager profile    |
|    20551    |    10    |    No    |    Profile used by the Windows Synchronization Manager.    |
|    20552    |    10    |    No    |    Could not clean up the distribution transaction tables.    |
|    20553    |    10    |    No    |    Could not clean up the distribution history tables.    |
|    20554    |    10    |    No    |    The replication agent has not logged a progress message in %ld minutes. This might indicate an unresponsive agent or high system activity. Verify that records are being replicated to the destination and that connections to the Subscriber, Publisher, and Distributor are still active.    |
|    20555    |    10    |    No    |    6.x publication.    |
|    20556    |    10    |    No    |    Heartbeats detected for all running replication agents.    |
|    [20557](../../relational-databases/errors-events/mssqlserver-20557-database-engine-error.md)    |    10    |    Yes    |    Agent shutdown. For more information, see the SQL Server Agent job history for job '%s'.    |
|    20558    |    10    |    No    |    Table '%s' passed full rowcount validation after failing the fast check. DBCC UPDATEUSAGE will be initiated automatically.    |
|    20559    |    10    |    No    |    Conditional Fast Rowcount method requested without specifying an expected count. Fast method will be used.    |
|    20560    |    10    |    No    |    An expected checksum value was passed, but checksums will not be compared because rowcount-only checking was requested.    |
|    20561    |    10    |    No    |    Generated expected rowcount value of %s for %s.    |
|    20565    |    10    |    No    |    Replication: Subscriber has failed data validation    |
|    20566    |    10    |    No    |    Replication: Subscriber has passed data validation    |
|    20567    |    10    |    No    |    Agent history clean up: %s    |
|    20568    |    10    |    No    |    Distribution clean up: %s    |
|    20569    |    10    |    No    |    Expired subscription clean up    |
|    20570    |    10    |    No    |    Reinitialize subscriptions having data validation failures    |
|    20571    |    10    |    No    |    Reinitializes all subscriptions that have data validation failures.    |
|    [20572](../../relational-databases/errors-events/mssqlserver-20572-database-engine-error.md)    |    10    |    Yes    |    Subscriber '%s' subscription to article '%s' in publication '%s' has been reinitialized after a validation failure.    |
|    20573    |    10    |    No    |    Replication: Subscription reinitialized after validation failure    |
|    [20574](../../relational-databases/errors-events/mssqlserver-20574-database-engine-error.md)    |    10    |    Yes    |    Subscriber '%s' subscription to article '%s' in publication '%s' failed data validation.    |
|    20575    |    10    |    No    |    Subscriber '%s' subscription to article '%s' in publication '%s' passed data validation.    |
|    20576    |    10    |    No    |    Subscriber '%s' subscription to article '%s' in publication '%s' has been reinitialized after a synchronization failure.    |
|    20577    |    10    |    No    |    No entries were found in msdb..sysreplicationalerts.    |
|    20578    |    10    |    No    |    Replication: agent custom shutdown    |
|    20579    |    10    |    No    |    Generated expected rowcount value of %s and expected checksum value of %s for %s.    |
|    20580    |    10    |    No    |    Heartbeats not detected for some replication agents. The status of these agents have been changed to 'Failed'.    |
|    20581    |    10    |    No    |    Cannot drop server '%s' because it is used as a Distributor in replication.    |
|    20582    |    10    |    No    |    Cannot drop server '%s' because it is used as a Publisher in replication.    |
|    20583    |    10    |    No    |    Cannot drop server '%s' because it is used as a Subscriber in replication.    |
|    20584    |    10    |    No    |    Cannot drop server '%s' because it is used as a Subscriber to remote Publisher '%s' in replication.    |
|    20585    |    16    |    No    |    Validation Failure. Object '%s' does not exist.    |
|    20586    |    16    |    No    |    (default destination)    |
|    20587    |    16    |    No    |    Invalid '%s' value for stored procedure '%s'.    |
|    20588    |    16    |    No    |    The subscription is not initialized. Run the Distribution Agent first.    |
|    20589    |    10    |    No    |    Agent profile for replicated queued transaction reader.    |
|    20590    |    16    |    No    |    The article property 'status' cannot include bit 64, 'DTS horizontal partitions' because the publication does not allow data transformations.    |
|    20591    |    16    |    No    |    Only 'DTS horizontal partitions' and 'no DTS horizontal partitions' are valid 'status' values because the publication allows data transformations.    |
|    20592    |    16    |    No    |    'dts horizontal partitions' and 'no dts horizontal partitions' are not valid 'status' values because the publication does not allow data transformations.    |
|    20593    |    16    |    No    |    Cannot modify publication '%s'. The sync_method cannot be changed to 'native', or 'concurrent' because the publication is enabled for heterogeneous subscribers.    |
|    20594    |    16    |    No    |    A push subscription to the publication exists. Use sp_subscription_cleanup to drop defunct push subscriptions.    |
|    20595    |    16    |    No    |    Skipping error signaled.    |
|    20596    |    16    |    No    |    Only '%s' or members of db_owner can drop the anonymous agent.    |
|    20597    |    10    |    No    |    Dropped %d anonymous subscription(s).    |
|    20598    |    16    |    No    |    The row was not found at the Subscriber when applying the replicated command.    |
|    20599    |    16    |    No    |    Continue on data consistency errors.    |
|    20600    |    10    |    No    |    Agent profile for skipping data consistency errors. It can be used only by SQL Server Subscribers.    |
|    20601    |    10    |    No    |    Invalid value specified for agent parameter 'SkipErrors'.    |
|    20602    |    10    |    No    |    The value specified for agent parameter 'SkipErrors' is too long.    |
|    20603    |    10    |    No    |    The agent profile cannot be used by heterogeneous Subscribers.    |
|    20604    |    10    |    No    |    You do not have permissions to run agents for push subscriptions. Make sure that you specify the agent parameter 'SubscriptionType'.    |
|    20605    |    10    |    No    |    Invalidated the existing snapshot of the publication. Run the Snapshot Agent again to generate a new snapshot.    |
|    20606    |    10    |    No    |    Reinitialized subscription(s).    |
|    20607    |    10    |    No    |    Cannot make the change because a snapshot is already generated. Set \@force_invalidate_snapshot to 1 to force the change and invalidate the existing snapshot.    |
|    20608    |    10    |    No    |    Cannot make the change because there are active subscriptions. Set \@force_reinit_subscription to 1 to force the change and reinitialize the active subscriptions.    |
|    20609    |    16    |    No    |    Cannot attach subscription file '%s'. Make sure that it is a valid subscription copy file.    |
|    20610    |    16    |    No    |    Cannot run '%s' when the Log Reader Agent is replicating the database.    |
|    20611    |    16    |    No    |    Cannot add the article. Publications that allow transformable subscriptions with Data Transformation Services (DTS) can only include tables and indexed views that are published as tables.    |
|    20612    |    16    |    No    |    Checksum validation is not supported because the publication allows DTS. Use row count only validation.    |
|    20613    |    16    |    No    |    Validation is not supported for articles that are set up for DTS horizontal partitions.    |
|    20614    |    16    |    No    |    Validation is not supported for heterogeneous Subscribers.    |
|    20615    |    16    |    No    |    Unable to add a heterogeneous subscription to the publication. The publication is not enabled for heterogeneous subscriptions.    |
|    20616    |    10    |    No    |    High Volume Server-to-Server Profile    |
|    20617    |    10    |    No    |    Merge agent profile optimized for the high volume server-to-server synchronization scenario.    |
|    20618    |    16    |    No    |    You must have CREATE DATABASE permission to attach a subscription database.    |
|    20619    |    16    |    No    |    Server user '%s' is not a valid user in database '%s'. Add the user account or 'guest' user account into the database first.    |
|    20620    |    11    |    No    |    The security mode specified requires the server '%s' to be registered as a linked server. Use sp_addlinkedserver to add the server.    |
|    20621    |    11    |    No    |    Cannot copy a subscription database to an existing database.    |
|    20622    |    11    |    No    |    Replication database option 'sync with backup' cannot be set on the publishing database because the database is in Simple Recovery mode.    |
|    20623    |    11    |    No    |    You cannot validate article '%s' unless you have 'SELECT ALL' permission on table '%s'.    |
|    20624    |    16    |    No    |    The value specified for the \@login parameter is not valid. User '%s' is not a user in database '%s'. Add the user account to the database before attempting to execute the stored procedures sp_grant_publication_access or sp_revoke_publication_access.    |
|    20625    |    16    |    No    |    Cannot create the merge replication publication access list (PAL) database role for publication '%s'. This role is used by replication to control access to the publication. Verify that you have sufficient permissions to create roles in the publication database.    |
|    20626    |    16    |    No    |    Filter '%s' already exists in publication '%s'. Specify a unique name for the \@filtername parameter of sp_addmergefilter.    |
|    20627    |    16    |    No    |    Partition id has to be greater than or equal to 0.    |
|    20628    |    16    |    No    |    Failed to generate dynamic snapshot.    |
|    20629    |    16    |    No    |    Failed to get partition id information.    |
|    20630    |    16    |    No    |    Cannot create partitioned snapshot job. A job already exists for publication '%ls' that uses the values you specified for the \@suser_sname and/or \@host_name parameters of sp_adddynamicsnapshot_job. If the job that already exists is not working correctly, use sp_dropdynamicsnapshot_job to drop it and create a new one using sp_adddynamicsnapshot_job.    |
|    20631    |    16    |    No    |    Cannot find a location in which to generate a partitioned snapshot. Verify that the there is a valid snapshot folder specified for the publication. This can be the default folder associated with the Distributor or an alternate folder associated with the publication.    |
|    20632    |    16    |    No    |    Failed to create a dynamic snapshot job to generate the dynamic snapshot.    |
|    20633    |    16    |    No    |    Cannot start the partitioned snapshot job. Verify that SQL Server Agent is running on the Distributor.    |
|    20634    |    16    |    No    |    The root publication information could not be found on the republisher.    |
|    20635    |    16    |    No    |    A push subscription to '%ls' was found. Cannot add a pull subscription agent for a push subscription.    |
|    20636    |    16    |    No    |    Cannot generate merge replication stored procedures for article '%s'. Stored procedures are generated on the Publisher when the Snapshot Agent runs or when a data definition language action is performed; they are generated on the Subscriber when the snapshot is applied by the Merge Agent. Verify that the agents have the appropriate permissions to create procedures, and that the procedures do not already exist.    |
|    20637    |    10    |    No    |    The article order specified in the \@processing_order parameter of sp_addmergearticle does not reflect the primary key-foreign key relationships between published tables. Article '%s' references one or more articles that will be created after it is created. Change the processing_order property using sp_changemergearticle.    |
|    20638    |    10    |    No    |    Merge table articles do not support different values for the \@source_object and \@destination_object parameters of sp_addmergearticle. Either do not specify a value for \@destination_object, or specify the same value for both parameters.    |
|    20639    |    16    |    No    |    Cannot enable the publication to support non-SQL Server subscriptions because the publication is enabled for updatable subscriptions. To support non-SQL Server subscriptions, drop the existing publication and create a new one with the properties allow_sync_tran and allow_queued_tran set to 'false'.    |
|    20640    |    16    |    No    |    Cannot change enabled for heterogeneous subscriptions property while there are subscriptions to the publication.    |
|    20641    |    16    |    No    |    Failed to check if the subset_filterclause has a dynamic function in it.    |
|    20642    |    16    |    No    |    Cannot add article '%s' with one or more dynamic functions in the subset_filterclause '%s' to publication '%s' because the publication could have active subscriptions. Set \@force_reinit_subscription to 1 to add the article and reinitialize all active subscriptions.    |
|    20643    |    16    |    No    |    Cannot change the value of validate_subscriber_info for publication '%s' because the publication has active subscriptions. Set \@force_reinit_subscription to 1 to change the value and reinitialize all active subscriptions.    |
|    20644    |    16    |    No    |    Invalid value "%s" specified for the parameter \@identityrangemangementoption. Valid values are "auto", "manual", or "none".    |
|    20645    |    16    |    No    |    The property "%s" cannot be modified for publications that are enabled for non-SQL Server subscriptions.    |
|    20646    |    16    |    No    |    Peer-to-peer publications do not support %s. Change the value for parameter '%s'.    |
|    20647    |    16    |    No    |    Cannot modify property '%s'. The publication is used in a peer-to-peer topology, which does not allow this property to be modified after the publication is created.    |
|    20648    |    16    |    No    |    An article already exists for table "%s" with a different value for the \@delete_tracking property. The value must be the same for all publications in which the table is published. Use the stored procedures sp_helpmergearticle and sp_changemergearticle to view and modify the property in the other article(s).    |
|    20649    |    16    |    No    |    Publications enabled for heterogeneous subscriptions do not support %s. Please change the '%s' parameter value.    |
|    20650    |    16    |    No    |    Cannot enable data definition language (DDL) replication (a value of "true" for the \@replicate_ddl parameter) for publication "%s". This is because the compatibility level of the publication is lower than 90RTM. For new publications, in the stored procedure sp_addmergepublication, set the \@publication_compatibility_level parameter to 90RTM; for existing publications, use sp_changemergepublication.    |
|    20651    |    16    |    No    |    Publication "%s" "%s". Therefore the compatibility level of the publication cannot be set to lower than %d. To set the compatibility level lower, disable the feature and then call the stored procedure sp_changemergepublication to lower the compatibility level.    |
|    20652    |    16    |    No    |    Required metadata for publication '%s' could not be found in the sysmergeschemachange system table. Run the Snapshot Agent again.    |
|    20653    |    16    |    No    |    Cannot have a dynamic snapshot job with both dynamic_filter_login and dynamic_filter_hostname being NULL.    |
|    20654    |    16    |    No    |    Dynamic snapshots are only valid for merge publications.    |
|    20655    |    16    |    No    |    The partitioned snapshot process cannot complete. Cannot retrieve the maximum timestamp information from the MSsnapshot_history table in the distribution database. Ensure that a standard snapshot is up-to-date and available.    |
|    20656    |    16    |    No    |    The \@subset_filterclause parameter cannot reference a computed column.    |
|    20657    |    16    |    No    |    The value for the \@pub_identity_range parameter must be a multiple of the increment for the identity column. The increment for table "%s" and identity column "%s" is %s.    |
|    20658    |    16    |    No    |    The value for the \@identity_range parameter must be a multiple of the increment for the identity column. The increment for table "%s" and identity column "%s" is %s.    |
|    20659    |    11    |    No    |    The value of IDENT_CURRENT ("%s") is greater than the value in the max_used column of the MSmerge_identity_range system table.    |
|    20660    |    16    |    No    |    The republisher's republishing range obtained from its publisher is not large enough to allocate the specified \@pub_identity_range.    |
|    20661    |    16    |    No    |    The republisher's republishing range obtained from its publisher is not large enough to allocate the specified \@identity_range.    |
|    20662    |    16    |    No    |    The republisher does not have a range of identity values from the root Publisher '%s' that it can assign to its Subscribers. Ensure that the republisher has a server subscription to the publication at the root Publisher, and then run the Merge Agent to synchronize with the root Publisher.    |
|    20663    |    16    |    No    |    The identity range allocation entry for the Publisher could not be found in the system table MSmerge_identity_range. Ensure that the value for the \@identityrangemanagementoption property is "auto".    |
|    20664    |    16    |    No    |    The Publisher cannot be assigned a new range of identity values, because the values for the identity column's data type have all been used. Change the data type in the identity column.    |
|    20665    |    16    |    No    |    The republisher does not have a range of identity values from the root Publisher that it can assign to its Subscribers. Run the Merge Agent to synchronize with the root Publisher.    |
|    20666    |    16    |    No    |    Cannot refresh the identity range and/or the check constraint on the Publisher. Ensure the following: that the value in the identity column has not reached the maximum for the data type in the identity column; and that the user who made the last insert has the privileges to drop and re-create the check constraint.    |
|    20667    |    16    |    No    |    Cannot allocate an identity range for article "%s". The article is not enabled for automatic identity range management.    |
|    20668    |    16    |    No    |    Not enough range available to allocate a new range for a subscriber.    |
|    20669    |    16    |    No    |    Object referenced by the given \@article or \@artid '%s' could not be found.    |
|    20670    |    16    |    No    |    Cannot add, drop, or alter the identity range check constraint for table %s. This constraint is used by replication for automatic identity range management. This error typically occurs if the user who made the last insert in the table does not have permission to make schema changes on the table. If this error occurs at the Publisher, run sp_adjustpublisheridentityrange; if it occurs at the Subscriber, run the Merge Agent.    |
|    20671    |    16    |    No    |    Cannot find the identity range allocation entry for the Subscriber in the MSmerge_identity_range table. Reinitialize the subscription.    |
|    20672    |    16    |    No    |    A value for the parameter \@host_name was specified, but no articles in the publication use HOST_NAME() for parameterized filtering.    |
|    20673    |    16    |    No    |    A value for the parameter \@host_name was specified, but no articles in the publication use SUSER_SNAME() for parameterized filtering.    |
|    20674    |    16    |    No    |    The publication does not use dynamic filtering.    |
|    20675    |    16    |    No    |    The identity range values cannot be NULL.    |
|    20676    |    11    |    No    |    Cannot refresh the Publisher identity range for article "%s". Execute the stored procedure sp_adjustpublisheridentityrange to refresh the identity range.    |
|    20677    |    11    |    No    |    Cannot add article "%s" with automatic identity range management. The article is already published in a transactional publication with automatic identity range management.    |
|    20678    |    11    |    No    |    Could not find the regular snapshot job for the specified publication '%s'.    |
|    20679    |    11    |    No    |    Cannot execute the stored procedure sp_adjustpublisheridentityrange on the current database because the database is a republisher or a Subscriber. To adjust the identity range at a republisher or a Subscriber, synchronize with the root Publisher.    |
|    20680    |    16    |    No    |    Failed to get metadata for a batch of rows.    |
|    20681    |    10    |    No    |    Cannot specify a value of 1, 2, or 3 for the parameter \@partition_options because publication "%s" has a compatibility level lower than 90RTM. Use the stored procedure sp_changemergepublication to set publication_compatibility_level to 90RTM.    |
|    20682    |    10    |    No    |    Failed deletion of rows in batched delete attempt on table %s.    |
|    20683    |    16    |    No    |    Failed batched deletion on download only article %s.    |
|    20684    |    16    |    No    |    Deleted more rows than expected in the batched delete attempt on table %s. Stop and restart the Merge Agent.    |
|    20685    |    16    |    No    |    Cannot drop the filter '%s' . The filter specified for the \@filtername parameter cannot be found.    |
|    20686    |    16    |    No    |    Parameter '%s' cannot be NULL or empty when this procedure is run from a '%s' database.    |
|    20687    |    16    |    No    |    Parameter '%s' must be NULL when this procedure is not being run from a '%s' database.    |
|    20688    |    16    |    No    |    The tracer token ID (%d) could not be found for Publisher %s, database %s, publication %s. Use the stored procedure sp_helptracertokens to retrieve a list of valid tracer token IDs.    |
|    20689    |    16    |    No    |    The check for a Publisher needing a new identity range allocation failed on table %s. This check occurs every time the Merge Agent and Snapshot Agent run. Rerun the Merge Agent or Snapshot Agent.    |
|    20690    |    16    |    No    |    Cannot set up the Publisher identity range for table %s. Verify that appropriate ranges were specified when the article was created, and then rerun the Snapshot Agent.    |
|    20691    |    16    |    No    |    Merge replication upgrade of SQL Server 2005 metadata and triggers on the subscriber failed.    |
|    20692    |    16    |    No    |    One or more rows to be inserted in the batch insert procedure for table %s were present in MSmerge_tombstone; merge replication cannot use batch insert. This typically occurs when rows move from one partition to another. No action is required, but if this condition occurs frequently, verify that data is partitioned optimally. Batch insert can improve the performance of merge replication.    |
|    20693    |    16    |    No    |    One or more rows to be inserted in the batch insert procedure for table %s were present in MSmerge_contents; merge replication cannot use batch insert. This typically occurs when rows move from one partition to another. No action is required, but if this condition occurs frequently, verify that data is partitioned optimally. Batch insert can improve the performance of merge replication.    |
|    20694    |    16    |    No    |    One or more rows to be updated for table %s contain changes in the column %s, which is used in one or more filters; merge replication cannot use batch processing for these changes. No action is required, but if this condition occurs frequently, verify that data is partitioned optimally. Batch updates can improve the performance of merge replication.    |
|    20695    |    16    |    No    |    Only %ld out of %ld rows were updated in the batched update procedure for table %s; other rows could not be updated because they have been deleted. No action is required, but if this condition occurs frequently, determine if update-delete conflicts can be avoided. Batch updates can be helpful for performance.    |
|    20696    |    16    |    No    |    The object %s is marked as shipped by Microsoft (ms_shipped). It cannot be added as an article for merge replication.    |
|    20697    |    16    |    No    |    Cannot drop article %s from publication %s. In this publication, this is the only article that uses a parameterized filter. Dropping this article changes the publication to a static publication, which requires reinitialization of all Subscribers. To drop the article and reinitialize all active subscriptions, specify a value of 1 for the \@force_reinit_subscription parameter of sp_dropmergepublication.    |
|    20698    |    16    |    No    |    A value for the parameter \@host_name was not specified while publication uses HOST_NAME() for dynamic filtering.    |
|    20699    |    16    |    No    |    A value for the parameter \@suser_sname was not specified while publication uses SUSER_SNAME() for dynamic filtering.    |
|    20701    |    16    |    No    |    The dynamic snapshot job schedule could not be changed on the distributor.    |
|    20702    |    16    |    No    |    The dynamic snapshot job schedule could not be changed due to one or more errors.    |
|    20703    |    16    |    No    |    One or more rows inserted in table '%s' were out of partition while the table was published with 'partition_options' set to %d.    |
|    20704    |    16    |    No    |    The datatype of the identity column of table '%s' is tinyint. tinyint does not have enough numbers available for merge auto identity range. Change the identity column to have a larger datatype and add the merge article with merge auto identity range management.    |
|    20705    |    10    |    No    |    Cannot set \@conflict_logging to 'both' because publication '%s' has a compatibility level lower than 90. Set \@publication_compatibility_level to '90RTM' when creating the publication or use sp_changemergepublication to set publication_compatibility_level to '90RTM'.    |
|    20706    |    10    |    No    |    The max or min allowed identity numbers for the identity column could not be found for the given article.    |
|    20707    |    10    |    No    |    Failed to publish the article with identityrangemanagementoption set to 'auto' due to one or more errors.    |
|    20708    |    10    |    No    |    An article is not allowed to be part of a logical record when it has a custom business logic resolver.    |
|    20709    |    10    |    No    |    The merge process could not clean up the conflict table "%s" for publication "%s".    |
|    20710    |    16    |    No    |    Incorrect identity range allocation was detected when logging identity range allocation information on the distributor for publisher '%s', publisher_db '%s', publication '%s' and article '%s'.    |
|    20711    |    16    |    No    |    The dynamic filters property for publication '%s' has been incorrectly set. Use sp_changemergepublication to reset the value to true if the publication uses parameterized filters and false if it does not.    |
|    20712    |    16    |    No    |    Unable to acquire the replication merge administrative application lock for database '%s'. This could be due an active snapshot running while the schema change (DDL) or the administrative proc change was attempted.    |
|    20713    |    16    |    No    |    Replication merge admin stored procedure '%s' failed for publication '%s'. This could be due an active snapshot running while the admin proc was called.    |
|    20714    |    16    |    No    |    Failed to prepare article '%s' in publication '%s' for merge replication.    |
|    20715    |    16    |    No    |    Failed to create merge replication triggers for object '%s'.    |
|    20716    |    16    |    No    |    Failed to create publication views for merge replication publication '%s'.    |
|    20717    |    16    |    No    |    sp_addmergelogsettings failed to add log settings. If log settings already exist for this subscription then use sp_changemergelogsettings to change the settings or sp_dropmergelogsettings to remove the settings.    |
|    20718    |    16    |    No    |    Log settings do not exist for subscriber server '%s', subscriber db '%s', webserver '%s'. Use sp_addmergelogsettings to add the settings.    |
|    20719    |    16    |    No    |    sp_changemergelogsettings failed to update log settings. Check the parameter values.    |
|    20720    |    16    |    No    |    Log settings do not exist for subscriber server '%s', subscriber db '%s', webserver '%s'.    |
|    20721    |    16    |    No    |    sp_dropmergelogsettings failed to remove log settings.    |
|    20722    |    16    |    No    |    '%s' failed. The value for parameter '%s' is not valid. Valid values are \@support_options [0 - 5], \@log_severity [1 - 4], \@log_file_size [2,000,000 - 999,000,000], \@no_of_log_files [2 - 500], \@upload_interval [0 - 40320], \@delete_after_upload [0 - 1].    |
|    20723    |    16    |    No    |    Computed column "%s" can only be added to publication after its depending object "%s" is added.    |
|    20724    |    16    |    No    |    Could not find a valid command line for the dynamic snapshot job with job_id '%s' for publication '%s'.    |
|    20725    |    16    |    No    |    Unable to update the dynamic snapshot location for the dynamic snapshot job with job_id '%s' in publication '%s'.    |
|    20726    |    16    |    No    |    Failed to change the dynamic snapshot location in one or more dynamic snapshot jobs for the given publication.    |
|    20727    |    16    |    No    |    An invalid value was specified for parameter \@subscription_type. Valid values are 'push', 'pull', 'both', 'anonymous' or 'all'.    |
|    20728    |    16    |    No    |    Failed to restore the max allocated identity value for article '%s' in publication '%s'.    |
|    20729    |    16    |    No    |    The max identity value allocation for article '%s' in publication '%s' could not be found on the distributor.    |
|    20730    |    16    |    No    |    Setting \@upload_first to 'true' requires the publication to be at publication_compatibility_level of '80RTM' or higher. Use sp_changemergepublication to set publication_compatibility_level to '80RTM' or higher if you want to use this feature.    |
|    20731    |    16    |    No    |    This edition of SQL Server does not support publications. Dropping existing publications.    |
|    20732    |    10    |    No    |    Warning: Values of some of the flags specified in the 'schema_option' property are not compatible with the publication's compatibility level. The modified schema_option value of '%s' will be used instead.    |
|    20733    |    16    |    No    |    One or more rows updated in table '%s' were out of partition while the table was published with 'partition_options' set to %d.    |
|    20734    |    16    |    No    |    One or more rows deleted in table '%s' were out of partition while the table was published with 'partition_options' set to %d.    |
|    20735    |    16    |    No    |    Cannot add article '%s' to publication '%s'. The publication already contains 256 articles, which is the maximum.    |
|    20736    |    10    |    No    |    Warning: Values of some of the flags specified in the 'schema_option' property are not compatible with the publication's compatibility level. The modified schema_option value of '%s' will be used instead.    |
|    20737    |    10    |    No    |    Warning: To allow replication of FILESTREAM data to perform optimally and reduce memory utilization, the 'stream_blob_columns' property has been set to 'true'. To force FILESTREAM table articles to not use blob streaming, use sp_changemergearticle to set 'stream_blob_columns' to 'false'.    |
|    20738    |    11    |    No    |    Cannot add article '%s' with sparse column or column set to merge publication since merge replication does not support sparse columns and column set.    |
|    20739    |    16    |    No    |    The DDL operation is not supported for article '%s'. If the column in the DDL operation is enabled for FILESTREAM or is of type hierarchyid, geometry, geography, datetime2, date, time, or datetimeoffset, the publication compatibility level must be at least 100RTM. For DDL operations that involve FILESTREAM and hierarchyid columns, the snapshot mode must be native. Character mode, which is required for SQL Server Compact Subscribers, is not supported.    |
|    20800    |    16    |    No    |    Cannot reinitialize the article '%s' in subscription '%s:%s' to publication '%s'. The publication is enabled for peer-to-peer transactional replication, which does not allow subscriptions to be reinitialized with a snapshot. Drop and re-create the subscription instead.    |
|    20801    |    16    |    No    |    Cannot reinitialize the subscription. The publication is enabled for peer-to-peer transactional replication, which does not allow subscriptions to be reinitialized with a snapshot. Drop and re-create the subscription instead.    |
|    20802    |    16    |    No    |    Cannot publish objects from the replication administrative user schema [%s]. This schema owns all replication procedures and metadata tables, but it cannot own published objects. Use a different schema for objects that will be published.    |
|    20803    |    16    |    No    |    Peer-To-Peer topologies require identical articles in publications at all nodes prior to synchronizing. Articles in publication [%s].[%s].[%s] do not match articles in [%s].[%s].[%s].    |
|    20804    |    16    |    No    |    Articles can only be included in a single peer-to-peer publication. [%s].[%s] is already included in the peer-to-peer publication '%s'.    |
|    20805    |    16    |    No    |    Peer-to-peer topologies require identical publication names on each Publisher. You are attempting to republish object [%s].[%s] that is already being published in the peer-to-peer publication [%s].[%s].[%s].    |
|    20806    |    16    |    No    |    An error occurred while executing a peer-to-peer forwarding command. Contact Customer Support Services.    |
|    20807    |    16    |    No    |    No peers were found for %s:%s:%s. If you encounter this error when executing the stored procedure sp_requestpeerresponse, verify that subscriptions have been created before attempting to call the procedure again. If you encounter this error in other circumstances, contact Customer Support Services.    |
|    20808    |    16    |    No    |    The peer-to-peer publication '%s' does not exist. Execute sp_helppublication to view a list of publication names.    |
|    20809    |    16    |    No    |    Peer-To-Peer topologies require identical publication names on each publisher. The distribution agent for publication [%s].[%s].[%s] is attempting to synchronize articles that exist in publication [%s].[%s].[%s].    |
|    20810    |    16    |    No    |    The specified source object must be a user-defined aggregate object if it is published as an 'aggregate schema only' type article.    |
|    20811    |    16    |    No    |    Replication monitoring refresher for %s.    |
|    20812    |    16    |    No    |    The specified source object must be a synonym if it is published as a 'synonym schema only' type article.    |
|    20813    |    16    |    No    |    Only members of the sysadmin fixed server role can modify a %s that does not have a job with a proxy account defined.    |
|    20814    |    10    |    No    |    Distribution Profile for OLEDB streaming    |
|    20815    |    10    |    No    |    Distribution agent profile enabled for the processing LOB data using OLEDB streaming.    |
|    20816    |    10    |    No    |    Peer-To-Peer publishers are only supported on Enterprise class editions of SQL Server. This instance is %s.    |
|    20817    |    16    |    No    |    An error occurred during the execution of '%ls'. A call to '%ls' failed with error code: '%ld', return code: '%d'.    |
