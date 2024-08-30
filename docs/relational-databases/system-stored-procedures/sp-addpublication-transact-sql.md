---
title: "sp_addpublication (Transact-SQL)"
description: sp_addpublication creates a snapshot or transactional publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addpublication_TSQL"
  - "sp_addpublication"
helpviewer_keywords:
  - "sp_addpublication"
dev_langs:
  - "TSQL"
---
# sp_addpublication (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a snapshot or transactional publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addpublication
    [ @publication = ] N'publication'
    [ , [ @taskid = ] taskid ]
    [ , [ @restricted = ] N'restricted' ]
    [ , [ @sync_method = ] N'sync_method' ]
    [ , [ @repl_freq = ] N'repl_freq' ]
    [ , [ @description = ] N'description' ]
    [ , [ @status = ] N'status' ]
    [ , [ @independent_agent = ] N'independent_agent' ]
    [ , [ @immediate_sync = ] N'immediate_sync' ]
    [ , [ @enabled_for_internet = ] N'enabled_for_internet' ]
    [ , [ @allow_push = ] N'allow_push' ]
    [ , [ @allow_pull = ] N'allow_pull' ]
    [ , [ @allow_anonymous = ] N'allow_anonymous' ]
    [ , [ @allow_sync_tran = ] N'allow_sync_tran' ]
    [ , [ @autogen_sync_procs = ] N'autogen_sync_procs' ]
    [ , [ @retention = ] retention ]
    [ , [ @allow_queued_tran = ] N'allow_queued_tran' ]
    [ , [ @snapshot_in_defaultfolder = ] N'snapshot_in_defaultfolder' ]
    [ , [ @alt_snapshot_folder = ] N'alt_snapshot_folder' ]
    [ , [ @pre_snapshot_script = ] N'pre_snapshot_script' ]
    [ , [ @post_snapshot_script = ] N'post_snapshot_script' ]
    [ , [ @compress_snapshot = ] N'compress_snapshot' ]
    [ , [ @ftp_address = ] N'ftp_address' ]
    [ , [ @ftp_port = ] ftp_port ]
    [ , [ @ftp_subdirectory = ] N'ftp_subdirectory' ]
    [ , [ @ftp_login = ] N'ftp_login' ]
    [ , [ @ftp_password = ] N'ftp_password' ]
    [ , [ @allow_dts = ] N'allow_dts' ]
    [ , [ @allow_subscription_copy = ] N'allow_subscription_copy' ]
    [ , [ @conflict_policy = ] N'conflict_policy' ]
    [ , [ @centralized_conflicts = ] N'centralized_conflicts' ]
    [ , [ @conflict_retention = ] conflict_retention ]
    [ , [ @queue_type = ] N'queue_type' ]
    [ , [ @add_to_active_directory = ] N'add_to_active_directory' ]
    [ , [ @logreader_job_name = ] N'logreader_job_name' ]
    [ , [ @qreader_job_name = ] N'qreader_job_name' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @allow_initialize_from_backup = ] N'allow_initialize_from_backup' ]
    [ , [ @replicate_ddl = ] replicate_ddl ]
    [ , [ @enabled_for_p2p = ] N'enabled_for_p2p' ]
    [ , [ @publish_local_changes_only = ] N'publish_local_changes_only' ]
    [ , [ @enabled_for_het_sub = ] N'enabled_for_het_sub' ]
    [ , [ @p2p_conflictdetection = ] N'p2p_conflictdetection' ]
    [ , [ @p2p_originator_id = ] p2p_originator_id ]
    [ , [ @p2p_continue_onconflict = ] N'p2p_continue_onconflict' ]
    [ , [ @allow_partition_switch = ] N'allow_partition_switch' ]
    [ , [ @replicate_partition_switch = ] N'replicate_partition_switch' ]
    [ , [ @allow_drop = ] N'allow_drop' ]
    [ , [ @p2p_conflictdetection_policy = ] N'p2p_conflictdetection_policy' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to create. *@publication* is **sysname**, with no default. The name must be unique within the database.

#### [ @taskid = ] *taskid*

Supported for backward compatibility only; use [sp_addpublication_snapshot](sp-addpublication-snapshot-transact-sql.md).

#### [ @restricted = ] N'*restricted*'

Supported for backward compatibility only; use `default_access`.

#### [ @sync_method = ] N'*sync_method*'

The synchronization mode. *@sync_method* is **nvarchar(40)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `native` <sup>1</sup> | Produces native-mode bulk copy program output of all tables. |
| `character` | Produces character-mode bulk copy program output of all tables. **Note:** For an Oracle Publisher, `character` is valid only for snapshot replication. |
| `concurrent` <sup>1</sup> | Produces native-mode bulk copy program output of all tables but doesn't lock tables during the snapshot. Only supported for transactional publications. |
| `concurrent_c` | Produces character-mode bulk copy program output of all tables but doesn't lock tables during the snapshot. Only supported for transactional publications. |
| `database snapshot` | Produces native-mode bulk copy program output of all tables from a database snapshot. Database snapshots aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md). |
| `database snapshot character` | Produces character-mode bulk copy program output of all tables from a database snapshot. Database snapshots aren't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md). |
| `NULL` (default) | Defaults to `native` for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. For non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, defaults to `character` when the value of *@repl_freq* is `Snapshot` and to **concurrent_c** for all other cases. |

<sup>1</sup> Not supported for Oracle Publishers.

#### [ @repl_freq = ] N'*repl_freq*'

The type of replication frequency, *@repl_freq* is **nvarchar(10)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `continuous` (default) | The log reader agent runs continuously. For non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, this requires that *@sync_method* be set to `concurrent_c`. |
| `snapshot` | The log reader agent runs on a schedule. For non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, this requires that *@sync_method* be set to `character`. |

#### [ @description = ] N'*description*'

An optional description for the publication. *@description* is **nvarchar(255)**, with a default of `NULL`.

#### [ @status = ] N'*status*'

Specifies if publication data is available. *@status* is **nvarchar(8)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `active` | Publication data is available for Subscribers immediately. |
| `inactive` (default) | Publication data isn't available for Subscribers when the publication is first created (they can subscribe, but the subscriptions aren't processed). |

*Not supported for Oracle Publishers*.

#### [ @independent_agent = ] N'*independent_agent*'

Specifies if there's a stand-alone Distribution Agent for this publication. *@independent_agent* is **nvarchar(5)**, with a default of `false`.

- If `true`, there's a stand-alone Distribution Agent for this publication.
- If `false`, the publication uses a shared Distribution Agent, and each Publisher database/Subscriber database pair has a single, shared Agent.

#### [ @immediate_sync = ] N'*immediate_sync*'

Specifies if the synchronization files for the publication are created each time the Snapshot Agent runs. *@immediate_sync* is **nvarchar(5)**, with a default of `false`.

If `true`, the synchronization files are created or re-created each time the Snapshot Agent runs. Subscribers are able to get the synchronization files immediately if the Snapshot Agent has completed before the subscription is created. New subscriptions get the newest synchronization files generated by the most recent execution of the Snapshot Agent. *@independent_agent* must be `true` for *@immediate_sync* to be `true`. If `false`, the synchronization files are created only if there are new subscriptions. You must call [sp_addsubscription](sp-addsubscription-transact-sql.md) for each subscription when you incrementally add a new article to an existing publication. Subscribers can't receive the synchronization files after the subscription until the Snapshot Agents are started and completed.

#### [ @enabled_for_internet = ] N'*enabled_for_internet*'

Specifies if the publication is enabled for the Internet, and determines if file transfer protocol (FTP) can be used to transfer the snapshot files to a subscriber. *@enabled_for_internet* is **nvarchar(5)**, with a default of `false`. If `true`, the synchronization files for the publication are put into the `C:\Program Files\Microsoft SQL Server\MSSQL\MSSQL.x\Repldata\Ftp` directory. The user must create the `Ftp` directory.

#### [ @allow_push = ] N'*allow_push*'

Specifies if push subscriptions can be created for the given publication. *@allow_push* is **nvarchar(5)**, with a default of `true`, which allows push subscriptions on the publication.

#### [ @allow_pull = ] N'*allow_pull*'

Specifies if pull subscriptions can be created for the given publication. *@allow_pull* is **nvarchar(5)**, with a default of `false`. If `false`, pull subscriptions aren't allowed on the publication.

#### [ @allow_anonymous = ] N'*allow_anonymous*'

Specifies if anonymous subscriptions can be created for the given publication. *@allow_anonymous* is **nvarchar(5)**, with a default of `false`. If `true`, *@immediate_sync* must also be set to `true`. If `false`, anonymous subscriptions aren't allowed on the publication.

#### [ @allow_sync_tran = ] N'*allow_sync_tran*'

Specifies if immediate-updating subscriptions are allowed on the publication. *@allow_sync_tran* is **nvarchar(5)**, with a default of `false`. `true` is *not supported for Oracle Publishers*.

#### [ @autogen_sync_procs = ] N'*autogen_sync_procs*'

Specifies if the synchronizing stored procedure for updating subscriptions is generated at the Publisher. *@autogen_sync_procs* is **nvarchar(5)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `true` | Set automatically when updating subscriptions is enabled. |
| `false` | Set automatically when updating subscriptions isn't enabled or for Oracle Publishers. |
| `NULL` (default) | Defaults to `true` when updating subscriptions is enabled and to `false` when updating subscriptions isn't enabled. |

> [!NOTE]  
> The user supplied value for *@autogen_sync_procs*will be overridden depending on the values specified for *@allow_queued_tran* and *@allow_sync_tran*.

#### [ @retention = ] *retention*

The retention period in hours for subscription activity. *@retention* is **int**, with a default of `336`. If a subscription isn't active within the retention period, it expires and is removed. The value can be greater than the maximum retention period of the distribution database used by the Publisher. If `0`, well-known subscriptions to the publication will never expire and be removed by the Expired Subscription Cleanup Agent.

#### [ @allow_queued_tran = ] N'*allow_queued_tran*'

Enables or disables queuing of changes at the Subscriber until they can be applied at the Publisher. *@allow_queued_tran* is **nvarchar(5)**, with a default of `false`.

- If `false`, changes at the Subscriber aren't queued.
- `true` is *not supported for Oracle Publishers*.

#### [ @snapshot_in_defaultfolder = ] N'*snapshot_in_defaultfolder*'

Specifies if snapshot files are stored in the default folder. *@snapshot_in_defaultfolder* is **nvarchar(5)**, with a default of `true`.

- If `true`, snapshot files can be found in the default folder.
- If `false`, snapshot files have been stored in the alternate location specified by *@alt_snapshot_folder*.

Alternate locations can be on another server, on a network drive, or on removable media (such as removable disks). You can also save the snapshot files to an FTP site, for retrieval by the Subscriber at a later time. This parameter can be true and still have a location in the *@alt_snapshot_folder* parameter. This combination specifies that the snapshot files will be stored in both the default and alternate locations.

#### [ @alt_snapshot_folder = ] N'*alt_snapshot_folder*'

Specifies the location of the alternate folder for the snapshot. *@alt_snapshot_folder* is **nvarchar(255)**, with a default of `NULL`.

#### [ @pre_snapshot_script = ] N'*pre_snapshot_script*'

Specifies a pointer to an **.sql** file location. *@pre_snapshot_script* is **nvarchar(255)**, with a default of `NULL`. The Distribution Agent will run the pre-snapshot script before running any of the replicated object scripts when applying a snapshot at a Subscriber. The script is executed in the security context used by the Distribution Agent when connecting to the subscription database.

#### [ @post_snapshot_script = ] N'*post_snapshot_script*'

Specifies a pointer to a `.sql` file location. *@post_snapshot_script* is **nvarchar(255)**, with a default of `NULL`. The Distribution Agent will run the post-snapshot script after all the other replicated object scripts and data have been applied during an initial synchronization. The script is executed in the security context used by the Distribution Agent when connecting to the subscription database.

#### [ @compress_snapshot = ] N'*compress_snapshot*'

Specifies that the snapshot that is written to the *@alt_snapshot_folder* location is to be compressed into the [!INCLUDE [msCoName](../../includes/msconame-md.md)] CAB format. *@compress_snapshot* is **nvarchar(5)**, with a default of `false`.

- `false` specifies that the snapshot aren't compressed.
- `true` specifies that the snapshot are compressed.

Snapshot files that are larger than 2 gigabytes (GB) can't be compressed. Compressed snapshot files are uncompressed at the location where the Distribution Agent runs; pull subscriptions are typically used with compressed snapshots so that files are uncompressed at the Subscriber. The snapshot in the default folder can't be compressed.

#### [ @ftp_address = ] N'*ftp_address*'

The network address of the FTP service for the Distributor. *@ftp_address* is **sysname**, with a default of `NULL`. Specifies where publication snapshot files are located for the Distribution Agent or Merge Agent of a subscriber to pick up. Since this property is stored for each publication, each publication can have a different *@ftp_address*. The publication must support propagating snapshots using FTP.

#### [ @ftp_port = ] *ftp_port*

The port number of the FTP service for the Distributor. *@ftp_port* is **int**, with a default of `21`. Specifies where the publication snapshot files are located for the Distribution Agent or Merge Agent of a subscriber to pick up. Since this property is stored for each publication, each publication can have its own *@ftp_port*.

#### [ @ftp_subdirectory = ] N'*ftp_subdirectory*'

Specifies where the snapshot files are available for the Distribution Agent or Merge Agent of subscriber to pick up if the publication supports propagating snapshots using FTP. *@ftp_subdirectory* is **nvarchar(255)**, with a default of `NULL`. Since this property is stored for each publication, each publication can have its own *@ftp_subdirctory* or choose to have no subdirectory, indicated with a `NULL` value.

#### [ @ftp_login = ] N'*ftp_login*'

The username used to connect to the FTP service. *@ftp_login* is **sysname**, with a default of `anonymous`.

#### [ @ftp_password = ] N'*ftp_password*'

The user password used to connect to the FTP service. *@ftp_password* is **sysname**, with a default of `NULL`.

#### [ @allow_dts = ] N'*allow_dts*'

Specifies that the publication allows data transformations. You can specify a DTS package when creating a subscription. *@allow_dts* is **nvarchar(5)**, with a default of `false`, which doesn't allow DTS transformations. When *@allow_dts* is true, *@sync_method* must be set to either `character` or `concurrent_c`.

`true` is *not supported for Oracle Publishers*.

#### [ @allow_subscription_copy = ] N'*allow_subscription_copy*'

Enables or disables the ability to copy the subscription databases that subscribe to this publication. *@allow_subscription_copy* is **nvarchar(5)**, with a default of `false`.

#### [ @conflict_policy = ] N'*conflict_policy*'

Specifies the conflict resolution policy followed when the queued updating subscriber option is used. *@conflict_policy* is **nvarchar(100)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `pub wins` | Publisher wins the conflict. |
| `sub reinit` | Reinitialize the subscription. |
| `sub wins` | Subscriber wins the conflict. |
| `NULL` (default) | If `NULL`, and the publication is a snapshot publication, the default policy becomes `sub reinit`. If `NULL` and the publication isn't a snapshot publication, the default becomes `pub wins`. |

*Not supported for Oracle Publishers*.

#### [ @centralized_conflicts = ] N'*centralized_conflicts*'

Specifies if conflict records are stored on the Publisher. *@centralized_conflicts* is **nvarchar(5)**, with a default of `NULL`.

- If `true`, conflict records are stored at the Publisher.
- If `false`, conflict records are stored at both the publisher and at the subscriber that caused the conflict.

*Not supported for Oracle Publishers*.

#### [ @conflict_retention = ] *conflict_retention*

Specifies the conflict retention period, in days. This is the period of time that conflict metadata is stored for peer-to-peer transactional replication and queued updating subscriptions. *@conflict_retention* is **int**, with a default of `14`.

*Not supported for Oracle Publishers*.

#### [ @queue_type = ] N'*queue_type*'

Specifies which type of queue is used. *@queue_type* is **nvarchar(10)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `sql` | Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to store transactions. |
| `NULL` (default) | Defaults to `sql`, which specifies to use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to store transactions. |

> [!NOTE]  
> Support for using [!INCLUDE [msCoName](../../includes/msconame-md.md)] Message Queuing is discontinued. Specifying a value of `msmq` will result in a warning, and replication will automatically set the value to `sql`.

*Not supported for Oracle Publishers*.

#### [ @add_to_active_directory = ] N'*add_to_active_directory*'

This parameter is deprecated and is only supported for the backward compatibility of scripts. You can no longer add publication information to the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Active Directory.

#### [ @logreader_job_name = ] N'*logreader_job_name*'

The name of an existing agent job. *@logreader_job_name* is **sysname**, with a default of `NULL`. This parameter is only specified when the Log Reader Agent uses an existing job instead of a new one being created.

#### [ @qreader_job_name = ] N'*qreader_job_name*'

The name of an existing agent job. *@qreader_job_name* is **sysname**, with a default of `NULL`. This parameter is only specified when the Queue Reader Agent uses an existing job instead of a new one being created.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when adding a publication to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

#### [ @allow_initialize_from_backup = ] N'*allow_initialize_from_backup*'

Indicates if Subscribers can initialize a subscription to this publication from a backup rather than an initial snapshot. *@allow_initialize_from_backup* is **nvarchar(5)**, and can be one of these values:

| Value | Description |
| --- | --- |
| `true` | Enables initialization from a backup. |
| `false` | Disables initialization from a backup. |
| `NULL` (default) | Defaults to `true` for a publication in a peer-to-peer replication topology and `false` for all other publications. |

For more information, see [Initialize a Transactional Subscription Without a Snapshot](../replication/initialize-a-transactional-subscription-without-a-snapshot.md).

> [!WARNING]  
> To avoid missing subscriber data, when using `sp_addpublication` with `@allow_initialize_from_backup = N'true'`, always use `@immediate_sync = N'true'`.

#### [ @replicate_ddl = ] *replicate_ddl*

Indicates if schema replication is supported for the publication. *@replicate_ddl* is **int**, with a default of `1` for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, and `0` for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.

- `1` indicates that data definition language (DDL) statements executed at the publisher are replicated.
- `0` indicates that DDL statements aren't replicated.

*Schema replication isn't supported for Oracle Publishers.*

For more information, see [Make Schema Changes on Publication Databases](../replication/publish/make-schema-changes-on-publication-databases.md).

The *@replicate_ddl* parameter is honored when a DDL statement adds a column. The *@replicate_ddl* parameter is ignored when a DDL statement alters or drops a column for the following reasons.

- When a column is dropped, `sysarticlecolumns` must be updated to prevent new DML statements from including the dropped column, which would cause the distribution agent to fail. The *@replicate_ddl* parameter is ignored because replication must always replicate the schema change.

- When a column is altered, the source data type or nullability might have changed, causing DML statements to contain a value that might not be compatible with the table at the subscriber. Such DML statements might cause distribution agent to fail. The *@replicate_ddl* parameter is ignored because replication must always replicate the schema change.

- When a DDL statement adds a new column, `sysarticlecolumns` doesn't include the new column. DML statements don't try to replicate data for the new column. The parameter is honored because either replicating or not replicating the DDL is acceptable.

#### [ @enabled_for_p2p = ] N'*enabled_for_p2p*'

Enables the publication to be used in a peer-to-peer replication topology. *@enabled_for_p2p* is **nvarchar(5)**, with a default of `false`. `true` indicates that the publication supports peer-to-peer replication. When setting *@enabled_for_p2p* to `true`, the following restrictions apply:

- *@allow_anonymous* must be `false`.
- *@allow_dts* must be `false`.
- *@allow_initialize_from_backup* must be `true`.
- *@allow_queued_tran* must be `false`.
- *@allow_sync_tran* must be `false`.
- *@conflict_policy* must be `false`.
- *@independent_agent* must be `true`.
- *@repl_freq* must be `continuous`.
- *@replicate_ddl* must be `1`.

For more information, see [Peer-to-Peer - Transactional Replication](../replication/transactional/peer-to-peer-transactional-replication.md).

#### [ @publish_local_changes_only = ] N'*publish_local_changes_only*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @enabled_for_het_sub = ] N'*enabled_for_het_sub*'

Enables the publication to support non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers. *@enabled_for_het_sub* is **nvarchar(5)**, with a default of `false`. A value of `true` means that the publication supports non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers. When *@enabled_for_het_sub* is `true`, the following restrictions apply:

- *@allow_initialize_from_backup* must be `false`.
- *@allow_push* must be `true`.
- *@allow_queued_tran* must be `false`.
- *@allow_subscription_copy* must be `false`.
- *@allow_sync_tran* must be `false`.
- *@autogen_sync_procs* must be `false`.
- *@conflict_policy* must be `NULL`.
- *@enabled_for_internet* must be `false`.
- *@enabled_for_p2p* must be `false`.
- *@ftp_address* must be `NULL`.
- *@ftp_subdirectory* must be `NULL`.
- *@ftp_password* must be `NULL`.
- *@pre_snapshot_script* must be `NULL`.
- *@post_snapshot_script* must be `NULL`.
- *@replicate_ddl* must be 0.
- *@qreader_job_name* must be `NULL`.
- *@queue_type* must be `NULL`.
- *@sync_method* can't be `native` or `concurrent`.

For more information, see [Non-SQL Server Subscribers](../replication/non-sql/non-sql-server-subscribers.md).

#### [ @p2p_conflictdetection = ] N'*p2p_conflictdetection*'

Enables the Distribution Agent to detect conflicts if the publication is enabled for peer-to-peer replication. *@p2p_conflictdetection* is **nvarchar(5)**, with a default of `false`. For more information, see [Peer-to-Peer - Conflict Detection in Peer-to-Peer Replication](../replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).

#### [ @p2p_originator_id = ] *p2p_originator_id*

Specifies an ID for a node in a peer-to-peer topology. *@p2p_originator_id* is **int**, with a default of `NULL`. This ID is used for conflict detection if *@p2p_conflictdetection* is set to TRUE. Specify a positive, nonzero ID that hasn't been used in the topology. For a list of IDs that are used, execute [sp_help_peerconflictdetection](sp-help-peerconflictdetection-transact-sql.md).

#### [ @p2p_continue_onconflict = ] N'*p2p_continue_onconflict*'

Determines whether the Distribution Agent continues to process changes after a conflict is detected. *@p2p_continue_onconflict* is **nvarchar(5)**, with a default of `false`.

> [!CAUTION]  
> We recommend that you use the default value of `false`. When this option is set to `true`, the Distribution Agent tries to converge data in the topology by applying the conflicting row from the node that's the highest originator ID. This method doesn't guarantee convergence. You should make sure that the topology is consistent after a conflict is detected. For more information, see "Handling Conflicts" in [Peer-to-Peer - Conflict Detection in Peer-to-Peer Replication](../replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).

#### [ @allow_partition_switch = ] N'*allow_partition_switch*'

Specifies whether `ALTER TABLE...SWITCH` statements can be executed against the published database. *@allow_partition_switch* is **nvarchar(5)**, with a default of `false`. For more information, see [Replicate Partitioned Tables and Indexes](../replication/publish/replicate-partitioned-tables-and-indexes.md).

#### [ @replicate_partition_switch = ] N'*replicate_partition_switch*'

Specifies whether `ALTER TABLE...SWITCH` statements that are executed against the published database should be replicated to Subscribers. *@replicate_partition_switch* is **nvarchar(5)**, with a default of `NULL`. This option is valid only if *@allow_partition_switch* is set to TRUE.

#### [ @allow_drop = ] N'*allow_drop*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @p2p_conflictdetection_policy = ] N'*p2p_conflictdetection_policy*'

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 13 and later versions.

*@p2p_conflictdetection_policy* is **nvarchar(12)**, and can be one of these values:

| Value | Description |
| --- | --- |
| `originatorid` (default) | Distribution agent detects the conflict and decides the winner based on the originator ID, if `p2p_continue_onconflict = N'true'`. Otherwise, it raises an error. |
| `lastwriter` | Distribution agent detects the conflict and decides the winner based on the datetime of the last writer if `p2p_continue_onconflict = N'true'`. Otherwise, it raises an error. |

> [!NOTE]  
> When you specify `originatorid`, conflict detection is the same as it was [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 12 and earlier versions. When you specify `lastwriter`, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] allows conflicts to resolve automatically based on the most recent write.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addpublication` is used in snapshot replication and transactional replication.

If multiple publications exist that publish the same database object, only publications with a *@replicate_ddl* value of `1` replicates `ALTER TABLE`, `ALTER VIEW`, `ALTER PROCEDURE`, `ALTER FUNCTION`, and `ALTER TRIGGER` DDL statements. However, an `ALTER TABLE DROP COLUMN` DDL statement is replicated by all publications that are publishing the dropped column.

With DDL replication enabled (*@replicate_ddl* = `1`) for a publication, in order to make nonreplicating DDL changes to the publication, [sp_changepublication](sp-changepublication-transact-sql.md) must first be executed to set *@replicate_ddl* to `0`. After the nonreplicating DDL statements have been issued, [sp_changepublication](sp-changepublication-transact-sql.md) can be run again to turn DDL replication back on.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addpublication-transa_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addpublication`. Windows authentication logins must have a user account in the database representing their Windows user account. A user account representing a Windows group isn't sufficient.

## Related content

- [sp_addlogreader_agent (Transact-SQL)](sp-addlogreader-agent-transact-sql.md)
- [sp_addpublication_snapshot (Transact-SQL)](sp-addpublication-snapshot-transact-sql.md)
- [sp_changepublication (Transact-SQL)](sp-changepublication-transact-sql.md)
- [sp_droppublication (Transact-SQL)](sp-droppublication-transact-sql.md)
- [sp_helppublication (Transact-SQL)](sp-helppublication-transact-sql.md)
- [sp_replicationdboption (Transact-SQL)](sp-replicationdboption-transact-sql.md)
- [Publish Data and Database Objects](../replication/publish/publish-data-and-database-objects.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
