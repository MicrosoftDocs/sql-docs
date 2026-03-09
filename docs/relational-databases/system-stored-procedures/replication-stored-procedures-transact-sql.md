---
title: "Replication Stored Procedures (Transact-SQL)"
description: Reference guide for replication system stored procedures used to configure, manage, and monitor publications, subscriptions, and replication agents in SQL Server.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 02/24/2026
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
ai-usage: ai-assisted
helpviewer_keywords:
  - "system stored procedures [SQL Server], replication"
  - "replication stored procedures [SQL Server]"
  - "stored procedures [SQL Server replication]"
dev_langs:
  - "TSQL"
---
# Replication stored procedures (Transact-SQL)

[!INCLUDE [sql-asdb](../../includes/applies-to-version/sql-asdb.md)]

Replication system stored procedures are documented and available as a method for accomplishing one-time tasks, such as implementing replication, and for using in batch files and scripts.

For more information on how to program most of the common replication tasks using replication stored procedures, see [Replication System Stored Procedures Concepts](../replication/concepts/replication-system-stored-procedures-concepts.md).

> [!IMPORTANT]
> Only the replication stored procedures documented in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online are supported. Undocumented stored procedures are only for the use of internal replication components and shouldn't be used to administer replication.

## Publication and distribution setup

These procedures configure the replication infrastructure, including Distributors, Publishers, and distribution databases.

| Stored procedure | Description |
| --- | --- |
| [sp_adddistpublisher](sp-adddistpublisher-transact-sql.md) | Registers a Publisher at a Distributor and specifies the default snapshot folder for replication. |
| [sp_adddistributor](sp-adddistributor-transact-sql.md) | Configures a server as a Distributor for replication, specifying the password for the **distributor_admin** account. |
| [sp_adddistributiondb](sp-adddistributiondb-transact-sql.md) | Creates the distribution database on the Distributor with configurable retention periods. |
| [sp_addlogreader_agent](sp-addlogreader-agent-transact-sql.md) | Creates the Log Reader Agent job for a Transactional publication. |
| [sp_addpublication](sp-addpublication-transact-sql.md) | Creates a new Snapshot or Transactional publication in the publication database. |
| [sp_addpublication_snapshot](sp-addpublication-snapshot-transact-sql.md) | Creates the Snapshot Agent job for a publication. |
| [sp_addqreader_agent](sp-addqreader-agent-transact-sql.md) | Creates the Queue Reader Agent job for a Distributor that supports queued updating subscriptions. |
| [sp_changedistpublisher](sp-changedistpublisher-transact-sql.md) | Changes the properties of a Publisher registered at the Distributor. |
| [sp_changedistributiondb](sp-changedistributiondb-transact-sql.md) | Modifies properties of the distribution database, including retention periods. |
| [sp_changedistributor_password](sp-changedistributor-password-transact-sql.md) | Changes the password used for the connection between the Publisher and a remote Distributor. |
| [sp_changedistributor_property](sp-changedistributor-property-transact-sql.md) | Modifies Distributor properties such as the heartbeat interval for agent status checks. |
| [sp_changepublication](sp-changepublication-transact-sql.md) | Modifies properties of a Snapshot or Transactional publication. |
| [sp_changepublication_snapshot](sp-changepublication-snapshot-transact-sql.md) | Changes the security credentials or scheduling properties of the Snapshot Agent. |
| [sp_changelogreader_agent](sp-changelogreader-agent-transact-sql.md) | Changes the security properties of the Log Reader Agent. |
| [sp_changeqreader_agent](sp-changeqreader-agent-transact-sql.md) | Changes the security properties for the Queue Reader Agent. |
| [sp_changereplicationserverpasswords](sp-changereplicationserverpasswords-transact-sql.md) | Changes stored passwords for the Windows account or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login used by replication agents when connecting to servers in a replication topology.  |
| [sp_dropdistpublisher](sp-dropdistpublisher-transact-sql.md) | Removes a Publisher from the Distributor. |
| [sp_dropdistributiondb](sp-dropdistributiondb-transact-sql.md) | Drops a distribution database and its associated replication jobs. |
| [sp_dropdistributor](sp-dropdistributor-transact-sql.md) | Drops the Distributor by removing the distribution configuration. |
| [sp_droppublication](sp-droppublication-transact-sql.md) | Drops a publication and all articles associated with it. |
| [sp_get_distributor](sp-get-distributor-transact-sql.md) | Returns the Distributor installed on a server. |
| [sp_get_redirected_publisher](sp-get-redirected-publisher-transact-sql.md) | Returns the redirected Publisher for an availability group publisher database. |
| [sp_grant_publication_access](sp-grant-publication-access-transact-sql.md) | Adds a login to the publication access list. |
| [sp_helpdistpublisher](sp-helpdistpublisher-transact-sql.md) | Returns properties of Publishers registered at a Distributor. |
| [sp_helpdistributiondb](sp-helpdistributiondb-transact-sql.md) | Returns properties of a specified distribution database. |
| [sp_helpdistributor](sp-helpdistributor-transact-sql.md) | Returns information about the Distributor, distribution database, and working directory. |
| [sp_helpdistributor_properties](sp-helpdistributor-properties-transact-sql.md) | Returns Distributor properties such as heartbeat interval. |
| [sp_helplogreader_agent](sp-helplogreader-agent-transact-sql.md) | Returns properties of the Log Reader Agent job. |
| [sp_helppublication](sp-helppublication-transact-sql.md) | Returns information about a publication. |
| [sp_helppublication_snapshot](sp-helppublication-snapshot-transact-sql.md) | Returns information about the Snapshot Agent job for a publication. |
| [sp_helpqreader_agent](sp-helpqreader-agent-transact-sql.md) | Returns properties of the Queue Reader Agent. |
| [sp_helpreplicationdboption](sp-helpreplicationdboption-transact-sql.md) | Returns whether replication is enabled for a database. |
| [sp_helpreplicationoption](sp-helpreplicationoption-transact-sql.md) | Returns replication options enabled on a server. |
| [sp_publisherproperty](sp-publisherproperty-transact-sql.md) | Gets or sets Publisher properties for a non-SQL Server Publisher. |
| [sp_redirect_publisher](sp-redirect-publisher-transact-sql.md) | Specifies a redirected Publisher for an availability group publisher database. |
| [sp_replicationdboption](sp-replicationdboption-transact-sql.md) | Enables or disables replication publishing options for a database. |
| [sp_revoke_publication_access](sp-revoke-publication-access-transact-sql.md) | Removes a login from the publication access list. |
| [sp_validate_redirected_publisher](sp-validate-redirected-publisher-transact-sql.md) | Validates the redirected Publisher for an availability group. |
| [sp_validate_replica_hosts_as_publishers](sp-validate-replica-hosts-as-publishers-transact-sql.md) | Validates all replica hosts in an availability group for use as publishers. |

## Replication article management

These procedures define which database objects are published and configure article properties such as filtering.

| Stored procedure | Description |
| --- | --- |
| [sp_addarticle](sp-addarticle-transact-sql.md) | Adds an article (table, view, or stored procedure) to a Snapshot or Transactional publication. |
| [sp_articlecolumn](sp-articlecolumn-transact-sql.md) | Defines which columns to include or exclude from a published article for vertical filtering. |
| [sp_articlefilter](sp-articlefilter-transact-sql.md) | Creates a row filter for an article using a WHERE clause. |
| [sp_articleview](sp-articleview-transact-sql.md) | Creates the synchronization view for a filtered article. |
| [sp_changearticle](sp-changearticle-transact-sql.md) | Modifies properties of an existing article in a publication. |
| [sp_changearticlecolumndatatype](sp-changearticlecolumndatatype-transact-sql.md) | Changes the column data type mapping for an Oracle publication article. |
| [sp_check_dynamic_filters](sp-check-dynamic-filters-transact-sql.md) | Checks whether a Merge publication uses parameterized row filters. |
| [sp_check_for_sync_trigger](sp-check-for-sync-trigger-transact-sql.md) | Determines if a trigger is being called in the context of synchronization. |
| [sp_check_join_filter](sp-check-join-filter-transact-sql.md) | Validates a join filter between two tables to ensure it can be precomputed. |
| [sp_check_subset_filter](sp-check-subset-filter-transact-sql.md) | Validates a parameterized row filter clause to ensure it's valid. |
| [sp_droparticle](sp-droparticle-transact-sql.md) | Removes an article from a Snapshot or Transactional publication. |
| [sp_generatefilters](sp-generatefilters-transact-sql.md) | Creates filters for child articles based on foreign key relationships. |
| [sp_helparticle](sp-helparticle-transact-sql.md) | Displays properties of articles in a publication. |
| [sp_helparticlecolumns](sp-helparticlecolumns-transact-sql.md) | Returns all columns in the underlying base table for an article. |
| [sp_helparticledts](sp-helparticledts-transact-sql.md) | Returns information about custom task names used with DTS subscriptions. |

## Subscription management

These procedures create, modify, and remove subscriptions to publications, including push and pull subscriptions.

| Stored procedure | Description |
| --- | --- |
| [sp_addsubscription](sp-addsubscription-transact-sql.md) | Creates a subscription to a Snapshot or Transactional publication at the Publisher. |
| [sp_addsubscriber](sp-addsubscriber-transact-sql.md) | Registers a server as a Subscriber and configures Subscriber properties. |
| [sp_addsubscriber_schedule](sp-addsubscriber-schedule-transact-sql.md) | Adds a schedule for the Distribution Agent or Merge Agent. |
| [sp_addpullsubscription](sp-addpullsubscription-transact-sql.md) | Creates a pull subscription at the Subscriber for a Snapshot or Transactional publication. |
| [sp_addpullsubscription_agent](sp-addpullsubscription-agent-transact-sql.md) | Creates the Distribution Agent job for a pull subscription at the Subscriber. |
| [sp_addpushsubscription_agent](sp-addpushsubscription-agent-transact-sql.md) | Creates the Distribution Agent job for a push subscription at the Distributor. |
| [sp_addqueued_artinfo](sp-addqueued-artinfo-transact-sql.md) | Adds article information to the queue used by queued updating subscriptions. |
| [sp_addscriptexec](sp-addscriptexec-transact-sql.md) | Posts a SQL script to be executed at all Subscribers to a publication. |
| [sp_addsynctriggers](sp-addsynctriggers-transact-sql.md) | Creates synchronization triggers at the Subscriber for updatable subscriptions. |
| [sp_attachsubscription](sp-attachsubscription-transact-sql.md) | Attaches an existing subscription database to any Subscriber. |
| [sp_change_subscription_properties](sp-change-subscription-properties-transact-sql.md) | Changes security information and settings for a pull subscription. |
| [sp_changesubscriber](sp-changesubscriber-transact-sql.md) | Changes options for a Subscriber, including connection settings and schedules. |
| [sp_changesubscriber_schedule](sp-changesubscriber-schedule-transact-sql.md) | Changes the Distribution Agent or Merge Agent schedule for a Subscriber. |
| [sp_changesubscription](sp-changesubscription-transact-sql.md) | Modifies properties of a Snapshot or Transactional subscription. |
| [sp_changesubscriptiondtsinfo](sp-changesubscriptiondtsinfo-transact-sql.md) | Changes the DTS package properties of a transactional push subscription. |
| [sp_changesubstatus](sp-changesubstatus-transact-sql.md) | Changes the status of an existing subscription at the Publisher. |
| [sp_copysubscription](sp-copysubscription-transact-sql.md) | Copies a subscription database that has pull subscriptions but no push subscriptions. |
| [sp_droppullsubscription](sp-droppullsubscription-transact-sql.md) | Drops a pull subscription at the Subscriber database. |
| [sp_dropsubscriber](sp-dropsubscriber-transact-sql.md) | Removes the Subscriber designation from a registered server. |
| [sp_dropsubscription](sp-dropsubscription-transact-sql.md) | Removes a subscription to a Snapshot or Transactional publication. |
| [sp_expired_subscription_cleanup](sp-expired-subscription-cleanup-transact-sql.md) | Removes expired anonymous subscriptions from publications. |
| [sp_getqueuedrows](sp-getqueuedrows-transact-sql.md) | Returns rows from the Subscriber that have pending updates in the queue. |
| [sp_getsubscriptiondtspackagename](sp-getsubscriptiondtspackagename-transact-sql.md) | Returns the DTS package name for a subscription. |
| [sp_helpsubscriberinfo](sp-helpsubscriberinfo-transact-sql.md) | Returns information about a Subscriber. |
| [sp_helpsubscription](sp-helpsubscription-transact-sql.md) | Returns subscription information for a publication. |
| [sp_helpsubscription_properties](sp-helpsubscription-properties-transact-sql.md) | Returns security information for a subscription. |
| [sp_helppullsubscription](sp-helppullsubscription-transact-sql.md) | Returns information about pull subscriptions at the Subscriber. |
| [sp_link_publication](sp-link-publication-transact-sql.md) | Sets the configuration for immediate updating subscriptions when connecting to the Publisher. |
| [sp_refreshsubscriptions](sp-refreshsubscriptions-transact-sql.md) | Adds subscriptions to newly added articles for existing pull subscriptions. |
| [sp_reinitpullsubscription](sp-reinitpullsubscription-transact-sql.md) | Marks a pull subscription for reinitialization when the Distribution Agent next runs. |
| [sp_reinitsubscription](sp-reinitsubscription-transact-sql.md) | Marks a push subscription for reinitialization. |
| [sp_setreplfailovermode](sp-setreplfailovermode-transact-sql.md) | Sets the failover mode for an updatable subscription. |
| [sp_helpreplfailovermode](sp-helpreplfailovermode-transact-sql.md) | Returns the current failover mode of a subscription. |
| [sp_subscription_cleanup](sp-subscription-cleanup-transact-sql.md) | Removes metadata when a subscription is dropped from a Subscriber. |

## Merge replication

These procedures are specific to merge replication, including Merge publications, articles, subscriptions, and conflict handling.

| Stored procedure | Description |
| --- | --- |
| [sp_addmergealternatepublisher](sp-addmergealternatepublisher-transact-sql.md) | Adds the ability for Subscribers to use an alternate synchronization partner. |
| [sp_addmergearticle](sp-addmergearticle-transact-sql.md) | Adds an article to a Merge publication with options for filtering and conflict resolution. |
| [sp_addmergefilter](sp-addmergefilter-transact-sql.md) | Defines a join filter or logical record relationship between articles in a Merge publication. |
| [sp_addmergepartition](sp-addmergepartition-transact-sql.md) | Creates a partition for parameterized row filters to enable pre-generated snapshots. |
| [sp_addmergepublication](sp-addmergepublication-transact-sql.md) | Creates a new Merge publication with options for parameterized filters. |
| [sp_addmergepullsubscription](sp-addmergepullsubscription-transact-sql.md) | Creates a pull subscription at the Subscriber for a Merge publication. |
| [sp_addmergepullsubscription_agent](sp-addmergepullsubscription-agent-transact-sql.md) | Creates the Merge Agent job for a pull subscription at the Subscriber. |
| [sp_addmergepushsubscription_agent](sp-addmergepushsubscription-agent-transact-sql.md) | Creates the Merge Agent job for a push subscription at the Distributor. |
| [sp_addmergesubscription](sp-addmergesubscription-transact-sql.md) | Creates a push or pull subscription to a Merge publication. |
| [sp_browsemergesnapshotfolder](sp-browsemergesnapshotfolder-transact-sql.md) | Returns the path to the most recent snapshot generated for a Merge publication. |
| [sp_changemergearticle](sp-changemergearticle-transact-sql.md) | Modifies properties of an existing article in a Merge publication. |
| [sp_changemergefilter](sp-changemergefilter-transact-sql.md) | Modifies an existing join filter or logical record relationship. |
| [sp_changemergepublication](sp-changemergepublication-transact-sql.md) | Modifies properties of a Merge publication. |
| [sp_changemergepullsubscription](sp-changemergepullsubscription-transact-sql.md) | Changes the properties of a merge pull subscription. |
| [sp_changemergesubscription](sp-changemergesubscription-transact-sql.md) | Changes properties of a merge push subscription. |
| [sp_copymergesnapshot](sp-copymergesnapshot-transact-sql.md) | Copies the snapshot folder to an alternate folder. |
| [sp_deletemergeconflictrow](sp-deletemergeconflictrow-transact-sql.md) | Deletes rows from a merge conflict table. |
| [sp_dropmergealternatepublisher](sp-dropmergealternatepublisher-transact-sql.md) | Removes an alternate Publisher from a Merge publication. |
| [sp_dropmergearticle](sp-dropmergearticle-transact-sql.md) | Removes an article from a Merge publication. |
| [sp_dropmergefilter](sp-dropmergefilter-transact-sql.md) | Drops a join filter from a Merge publication. |
| [sp_dropmergepartition](sp-dropmergepartition-transact-sql.md) | Removes a partition definition from a Merge publication with parameterized filters. |
| [sp_dropmergepublication](sp-dropmergepublication-transact-sql.md) | Removes a Merge publication and its associated Snapshot Agent. |
| [sp_dropmergepullsubscription](sp-dropmergepullsubscription-transact-sql.md) | Drops a Merge pull subscription at the Subscriber database. |
| [sp_dropmergesubscription](sp-dropmergesubscription-transact-sql.md) | Drops a subscription to a Merge publication and removes the associated Merge Agent. |
| [sp_getmergedeletetype](sp-getmergedeletetype-transact-sql.md) | Returns the type of merge delete operation. |
| [sp_helpmergealternatepublisher](sp-helpmergealternatepublisher-transact-sql.md) | Returns a list of servers configured as alternate Publishers. |
| [sp_helpmergearticle](sp-helpmergearticle-transact-sql.md) | Returns properties of articles in a Merge publication. |
| [sp_helpmergearticlecolumn](sp-helpmergearticlecolumn-transact-sql.md) | Returns the list of columns in a Merge publication article. |
| [sp_helpmergearticleconflicts](sp-helpmergearticleconflicts-transact-sql.md) | Returns conflict table information for articles that have experienced conflicts. |
| [sp_helpmergeconflictrows](sp-helpmergeconflictrows-transact-sql.md) | Returns the rows in the specified conflict table. |
| [sp_helpmergedeleteconflictrows](sp-helpmergedeleteconflictrows-transact-sql.md) | Returns data rows that lost delete conflicts. |
| [sp_helpmergefilter](sp-helpmergefilter-transact-sql.md) | Returns information about merge filters. |
| [sp_helpmergepartition](sp-helpmergepartition-transact-sql.md) | Returns partition information for a Merge publication. |
| [sp_helpmergepublication](sp-helpmergepublication-transact-sql.md) | Returns information about a Merge publication. |
| [sp_helpmergepullsubscription](sp-helpmergepullsubscription-transact-sql.md) | Returns information about Merge pull subscriptions. |
| [sp_helpmergesubscription](sp-helpmergesubscription-transact-sql.md) | Returns information about Merge subscriptions. |
| [sp_mergearticlecolumn](sp-mergearticlecolumn-transact-sql.md) | Partitions a Merge publication vertically by filtering columns. |
| [sp_mergecleanupmetadata](sp-mergecleanupmetadata-transact-sql.md) | Cleans up metadata in system tables after maintenance involving retention periods. |
| [sp_mergedummyupdate](sp-mergedummyupdate-transact-sql.md) | Marks a row for resending during the next merge synchronization. |
| [sp_mergemetadataretentioncleanup](sp-mergemetadataretentioncleanup-transact-sql.md) | Manually cleans up metadata in system tables based on retention periods. |
| [sp_mergesubscription_cleanup](sp-mergesubscription-cleanup-transact-sql.md) | Removes metadata after a merge push subscription is dropped. |
| [sp_reinitmergepullsubscription](sp-reinitmergepullsubscription-transact-sql.md) | Marks a merge pull subscription for reinitialization. |
| [sp_reinitmergesubscription](sp-reinitmergesubscription-transact-sql.md) | Marks a merge subscription for reinitialization. |
| [sp_resyncmergesubscription](sp-resyncmergesubscription-transact-sql.md) | Resynchronizes a merge subscription to a known validation state. |
| [sp_restoremergeidentityrange](sp-restoremergeidentityrange-transact-sql.md) | Updates identity range assignments after restoring a database. |
| [sp_showpendingchanges](sp-showpendingchanges-transact-sql.md) | Returns the pending changes waiting to be replicated. |
| [sp_showrowreplicainfo](sp-showrowreplicainfo-transact-sql.md) | Shows tracking information about a row in a merge article. |

## Replication agent profiles

These procedures manage agent profiles, which define the parameters used by replication agents.

| Stored procedure | Description |
| --- | --- |
| [sp_add_agent_parameter](sp-add-agent-parameter-transact-sql.md) | Adds a new parameter to an existing replication agent profile. |
| [sp_add_agent_profile](sp-add-agent-profile-transact-sql.md) | Creates a new agent profile for a replication agent type. |
| [sp_change_agent_parameter](sp-change-agent-parameter-transact-sql.md) | Changes the value of an existing parameter in an agent profile. |
| [sp_change_agent_profile](sp-change-agent-profile-transact-sql.md) | Modifies properties of an existing replication agent profile. |
| [sp_drop_agent_parameter](sp-drop-agent-parameter-transact-sql.md) | Removes a parameter from a replication agent profile. |
| [sp_drop_agent_profile](sp-drop-agent-profile-transact-sql.md) | Deletes a user-defined replication agent profile. |
| [sp_dropanonymousagent](sp-dropanonymousagent-transact-sql.md) | Drops an anonymous agent created for pull subscription monitoring. |
| [sp_getagentparameterlist](sp-getagentparameterlist-transact-sql.md) | Returns the list of valid parameters for a replication agent type. |
| [sp_help_agent_default](sp-help-agent-default-transact-sql.md) | Retrieves the default profile ID for the specified agent type. |
| [sp_help_agent_parameter](sp-help-agent-parameter-transact-sql.md) | Returns all parameters of a specified agent profile. |
| [sp_help_agent_profile](sp-help-agent-profile-transact-sql.md) | Returns information about replication agent profiles. |
| [sp_update_agent_profile](sp-update-agent-profile-transact-sql.md) | Updates the properties of a replication agent profile. |

## Snapshot management

These procedures manage snapshot generation and delivery for replication.

| Stored procedure | Description |
| --- | --- |
| [sp_adddynamicsnapshot_job](sp-adddynamicsnapshot-job-transact-sql.md) | Creates a Snapshot Agent job that generates a filtered data snapshot for a Merge publication partition. |
| [sp_browsesnapshotfolder](sp-browsesnapshotfolder-transact-sql.md) | Returns the path to the most recent snapshot generated for a publication. |
| [sp_changedynamicsnapshot_job](sp-changedynamicsnapshot-job-transact-sql.md) | Changes security settings for the filtered snapshot job for a Merge publication partition. |
| [sp_copysnapshot](sp-copysnapshot-transact-sql.md) | Copies the snapshot folder to the specified folder. |
| [sp_dropdynamicsnapshot_job](sp-dropdynamicsnapshot-job-transact-sql.md) | Removes the parameterized snapshot job for a Merge publication partition. |
| [sp_helpdynamicsnapshot_job](sp-helpdynamicsnapshot-job-transact-sql.md) | Returns information about parameterized snapshot jobs. |
| [sp_resetsnapshotdeliveryprogress](sp-resetsnapshotdeliveryprogress-transact-sql.md) | Resets the snapshot delivery process for a pull subscription so it can be restarted. |
| [sp_startpublication_snapshot](sp-startpublication-snapshot-transact-sql.md) | Starts the Snapshot Agent job for a publication. |

## Monitoring and validation

These procedures monitor replication performance and validate data consistency.

| Stored procedure | Description |
| --- | --- |
| [sp_article_validation](sp-article-validation-transact-sql.md) | Initiates validation (row count or checksum) for a single article. |
| [sp_browsereplcmds](sp-browsereplcmds-transact-sql.md) | Returns a result set of replicated commands stored in the distribution database. |
| [sp_deletetracertokenhistory](sp-deletetracertokenhistory-transact-sql.md) | Removes tracer token history records from the distribution database. |
| [sp_deletepeerrequesthistory](sp-deletepeerrequesthistory-transact-sql.md) | Deletes request history for peer-to-peer replication status requests. |
| [sp_helptracertokenhistory](sp-helptracertokenhistory-transact-sql.md) | Returns detailed latency information for tracer tokens. |
| [sp_helptracertokens](sp-helptracertokens-transact-sql.md) | Returns information about tracer tokens that have been inserted into a publication. |
| [sp_helpsubscriptionerrors](sp-helpsubscriptionerrors-transact-sql.md) | Returns errors for transactional replication stored in the distribution database. |
| [sp_marksubscriptionvalidation](sp-marksubscriptionvalidation-transact-sql.md) | Marks the current open transaction as a subscription-level validation transaction. |
| [sp_posttracertoken](sp-posttracertoken-transact-sql.md) | Inserts a tracer token into the transaction log to measure replication latency. |
| [sp_publication_validation](sp-publication-validation-transact-sql.md) | Initiates validation for all articles in a Transactional publication. |
| [sp_replcounters](sp-replcounters-transact-sql.md) | Returns replication statistics for each published database. |
| [sp_replmonitorchangepublicationthreshold](sp-replmonitorchangepublicationthreshold-transact-sql.md) | Changes the monitoring threshold metric for a publication. |
| [sp_replmonitorhelpmergesession](sp-replmonitorhelpmergesession-transact-sql.md) | Returns information about past Merge Agent sessions. |
| [sp_replmonitorhelpmergesessiondetail](sp-replmonitorhelpmergesessiondetail-transact-sql.md) | Returns detailed, article-level information about a Merge Agent session. |
| [sp_replmonitorhelppublication](sp-replmonitorhelppublication-transact-sql.md) | Returns monitoring information about publications at the Distributor. |
| [sp_replmonitorhelppublicationthresholds](sp-replmonitorhelppublicationthresholds-transact-sql.md) | Returns the threshold metrics set for a monitored publication. |
| [sp_replmonitorhelppublisher](sp-replmonitorhelppublisher-transact-sql.md) | Returns current status information for one or more Publishers. |
| [sp_replmonitorhelpsubscription](sp-replmonitorhelpsubscription-transact-sql.md) | Returns monitoring information about subscriptions. |
| [sp_replmonitorsubscriptionpendingcmds](sp-replmonitorsubscriptionpendingcmds-transact-sql.md) | Returns the number of pending commands for a subscription. |
| [sp_replqueuemonitor](sp-replqueuemonitor-transact-sql.md) | Returns queue messages from the queue for a queued updating subscription. |
| [sp_table_validation](sp-table-validation-transact-sql.md) | Performs row count or checksum validation on a specified table. |
| [sp_validatemergepublication](sp-validatemergepublication-transact-sql.md) | Marks all subscriptions to a Merge publication for validation. |
| [sp_validatemergesubscription](sp-validatemergesubscription-transact-sql.md) | Marks a specific Merge subscription for validation. |

## Peer-to-peer replication

These procedures are specific to peer-to-peer transactional replication.

| Stored procedure | Description |
| --- | --- |
| [sp_configure_peerconflictdetection](sp-configure-peerconflictdetection-transact-sql.md) | Configures conflict detection for a peer-to-peer Transactional publication. |
| [sp_gettopologyinfo](sp-gettopologyinfo-transact-sql.md) | Returns information about the peer-to-peer replication topology. |
| [sp_help_peerconflictdetection](sp-help-peerconflictdetection-transact-sql.md) | Returns information about the conflict detection setting for a publication. |
| [sp_helppeerrequests](sp-helppeerrequests-transact-sql.md) | Returns information about status requests received in a peer-to-peer topology. |
| [sp_helppeerresponses](sp-helppeerresponses-transact-sql.md) | Returns all responses to a status request in a peer-to-peer topology. |
| [sp_requestpeerresponse](sp-requestpeerresponse-transact-sql.md) | Requests a response from every other node in a peer-to-peer topology. |
| [sp_requestpeertopologyinfo](sp-requestpeertopologyinfo-transact-sql.md) | Gathers topology information about a peer-to-peer transactional replication topology. |

## Schema changes and maintenance

These procedures handle schema changes, scripting, and maintenance operations.

| Stored procedure | Description |
| --- | --- |
| [sp_addtabletocontents](sp-addtabletocontents-transact-sql.md) | Inserts tracking references for rows not currently included in merge tracking tables. |
| [sp_adjustpublisheridentityrange](sp-adjustpublisheridentityrange-transact-sql.md) | Adjusts the identity range on a publication and reallocates new ranges based on the threshold. |
| [sp_dsninfo](sp-dsninfo-transact-sql.md) | Returns ODBC or OLE DB data source information from the Distributor. |
| [sp_enumcustomresolvers](sp-enumcustomresolvers-transact-sql.md) | Returns a list of all available business logic handlers and custom resolvers. |
| [sp_enumdsn](sp-enumdsn-transact-sql.md) | Returns a list of ODBC and OLE DB data source names defined for the server. |
| [sp_enumeratependingschemachanges](sp-enumeratependingschemachanges-transact-sql.md) | Returns a list of all pending schema changes. |
| [sp_getdefaultdatatypemapping](sp-getdefaultdatatypemapping-transact-sql.md) | Returns the default mapping between SQL Server and a non-SQL Server database type. |
| [sp_helpdatatypemap](sp-helpdatatypemap-transact-sql.md) | Returns information about defined data type mappings between SQL Server and non-SQL Server databases. |
| [sp_helpxactsetjob](sp-helpxactsetjob-transact-sql.md) | Returns the job schedule for the Xactset job. |
| [sp_ivindexhasnullcols](sp-ivindexhasnullcols-transact-sql.md) | Validates that a clustered index on an indexed view has no nullable columns. |
| [sp_lookupcustomresolver](sp-lookupcustomresolver-transact-sql.md) | Returns information on a business logic handler or custom resolver. |
| [sp_markpendingschemachange](sp-markpendingschemachange-transact-sql.md) | Marks selected pending schema changes so they won't be replicated. |
| [sp_register_custom_scripting](sp-register-custom-scripting-transact-sql.md) | Registers a stored procedure to execute when a schema change occurs. |
| [sp_registercustomresolver](sp-registercustomresolver-transact-sql.md) | Registers a business logic handler or custom resolver for merge replication. |
| [sp_removedbreplication](sp-removedbreplication-transact-sql.md) | Removes all replication objects from a database. |
| [sp_removedistpublisherdbreplication](sp-removedistpublisherdbreplication-transact-sql.md) | Removes publishing metadata at the Distributor. |
| [sp_repladdcolumn](sp-repladdcolumn-transact-sql.md) | Adds a column to an existing table article that has been published. |
| [sp_replcmds](sp-replcmds-transact-sql.md) | Returns commands for transactions marked for replication from the log. |
| [sp_repldone](sp-repldone-transact-sql.md) | Updates the record that identifies the server's last distributed transaction. |
| [sp_repldropcolumn](sp-repldropcolumn-transact-sql.md) | Drops a column from an existing table article that has been published. |
| [sp_replflush](sp-replflush-transact-sql.md) | Flushes the article cache. |
| [sp_replication_agent_checkup](sp-replication-agent-checkup-transact-sql.md) | Checks each distribution database for replication agents that have no logged history. |
| [sp_replrestart](sp-replrestart-transact-sql.md) | Resets remote transactional replication. |
| [sp_replsetoriginator](sp-replsetoriginator-transact-sql.md) | Prevents loopback triggering in bidirectional transactional replication. |
| [sp_replshowcmds](sp-replshowcmds-transact-sql.md) | Returns the commands for transactions marked for replication in readable format. |
| [sp_repltrans](sp-repltrans-transact-sql.md) | Returns all the transactions pending replication in the publication database log. |
| [sp_restoredbreplication](sp-restoredbreplication-transact-sql.md) | Removes replication settings when restoring a database to a non-originating server. |
| [sp_schemafilter](sp-schemafilter-transact-sql.md) | Modifies the schema filter used when listing Oracle tables eligible for publishing. |
| [sp_script_synctran_commands](sp-script-synctran-commands-transact-sql.md) | Generates a script for sp_addsynctriggers calls to apply at Subscribers. |
| [sp_scriptdynamicupdproc](sp-scriptdynamicupdproc-transact-sql.md) | Generates the CREATE PROCEDURE statement for dynamic update stored procedures. |
| [sp_scriptpublicationcustomprocs](sp-scriptpublicationcustomprocs-transact-sql.md) | Scripts the custom procedures for all table articles in a publication. |
| [sp_scriptsubconflicttable](sp-scriptsubconflicttable-transact-sql.md) | Generates script for creating a conflict table at the Subscriber. |
| [sp_setdefaultdatatypemapping](sp-setdefaultdatatypemapping-transact-sql.md) | Marks an existing data type mapping as the default. |
| [sp_setsubscriptionxactseqno](sp-setsubscriptionxactseqno-transact-sql.md) | Specifies the last delivered transaction for troubleshooting. |
| [sp_unregister_custom_scripting](sp-unregister-custom-scripting-transact-sql.md) | Removes a user-defined custom stored procedure or script file registered for schema changes. |
| [sp_unregistercustomresolver](sp-unregistercustomresolver-transact-sql.md) | Unregisters a business logic module from merge replication. |
| [sp_vupgrade_mergeobjects](sp-vupgrade-mergeobjects-transact-sql.md) | Regenerates article-specific triggers, stored procedures, and views for merge replication. |
| [sp_vupgrade_replication](sp-vupgrade-replication-transact-sql.md) | Upgrades replication metadata when upgrading SQL Server. |

## Microsoft-internal procedures

These procedures modify agent properties at the Distributor and are used internally by replication agents.

| Stored procedure | Description |
| --- | --- |
| [sp_MSchange_distribution_agent_properties](sp-mschange-distribution-agent-properties-transact-sql.md) | Changes the properties of a Distribution Agent job running at the Distributor. |
| [sp_MSchange_logreader_agent_properties](sp-mschange-logreader-agent-properties-transact-sql.md) | Changes the properties of a Log Reader Agent job running at the Distributor. |
| [sp_MSchange_merge_agent_properties](sp-mschange-merge-agent-properties-transact-sql.md) | Changes the properties of a Merge Agent job running at the Distributor. |
| [sp_MSchange_snapshot_agent_properties](sp-mschange-snapshot-agent-properties-transact-sql.md) | Changes the properties of a Snapshot Agent job running at the Distributor. |

## Related content

- [Replication Management Objects Concepts](../replication/concepts/replication-management-objects-concepts.md)
- [Replication Programming Concepts](../replication/concepts/replication-programming-concepts.md)
