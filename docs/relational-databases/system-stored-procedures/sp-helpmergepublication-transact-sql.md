---
title: "sp_helpmergepublication (Transact-SQL)"
description: "Returns information about a merge publication."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergepublication"
  - "sp_helpmergepublication_TSQL"
helpviewer_keywords:
  - "sp_helpmergepublication"
dev_langs:
  - "TSQL"
---
# sp_helpmergepublication (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about a merge publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergepublication
    [ [ @publication = ] N'publication' ]
    [ , [ @found = ] found OUTPUT ]
    [ , [ @publication_id = ] 'publication_id' OUTPUT ]
    [ , [ @reserved = ] N'reserved' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `%`, which returns information about all merge publications in the current database.

#### [ @found = ] *found* OUTPUT

A flag to indicate returning rows. *@found* is an OUTPUT parameter of type **int**.

- `1` indicates the publication is found.
- `0` indicates the publication isn't found.

#### [ @publication_id = ] '*publication_id*' OUTPUT

The publication identification number. *@publication_id* is an OUTPUT parameter of type **uniqueidentifier**.

#### [ @reserved = ] N'*reserved*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with a default of `NULL`.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `id` | **int** | Sequential order of the publication in the result set list. |
| `name` | **sysname** | Name of the publication. |
| `description` | **nvarchar(255)** | Description of the publication. |
| `status` | **tinyint** | Indicates when publication data is available. |
| `retention` | **int** | Amount of time to save metadata about changes for articles in the publication. The units for this time period can be days, weeks, months, or years. For information about units, see the retention_period_unit column. |
| `sync_mode` | **tinyint** | Synchronization mode of this publication:<br /><br />`0` = Native bulk copy program (**bcp** utility)<br /><br />`1` = Character bulk copy |
| `allow_push` | **int** | Determines whether push subscriptions can be created for the given publication. `0` means that a push subscription isn't allowed. |
| `allow_pull` | **int** | Determines whether pull subscriptions can be created for the given publication. `0` means that a pull subscription isn't allowed. |
| `allow_anonymous` | **int** | Determines whether anonymous subscriptions can be created for the given publication. `0` means that an anonymous subscription isn't allowed. |
| `centralized_conflicts` | **int** | Determines whether conflict records are stored on the given Publisher:<br /><br />`0` = conflict records are stored at both the publisher and at the subscriber that caused the conflict.<br /><br />`1` = all conflict records are stored at the Publisher. |
| `priority` | **float(8)** | Priority of the loop-back subscription. |
| `snapshot_ready` | **tinyint** | Indicates whether the snapshot of this publication is ready:<br /><br />`0` = Snapshot is ready for use.<br /><br />`1` = Snapshot isn't ready for use. |
| `publication_type` | **int** | Type of publication:<br /><br />`0` = Snapshot.<br /><br />`1` = Transactional.<br /><br />`2` = Merge. |
| `pubid` | **uniqueidentifier** | Unique identifier of this publication. |
| `snapshot_jobid` | **binary(16)** | Job ID of the Snapshot Agent. To obtain the entry for the snapshot job in the [sysjobs](../system-tables/dbo-sysjobs-transact-sql.md) system table, you must convert this hexadecimal value to **uniqueidentifier**. |
| `enabled_for_internet` | **int** | Determines whether the publication is enabled for the Internet. If `1`, the synchronization files for the publication are put into the `C:\Program Files\Microsoft SQL Server\MSSQL\Repldata\Ftp` directory. The user must create the File Transfer Protocol (FTP) directory. If `0`, the publication isn't enabled for Internet access. |
| `dynamic_filter` | **int** | Indicates whether a parameterized row filter is used. `0` means a parameterized row filter isn't used. |
| `has_subscription` | **bit** | Indicates whether the publication has any subscriptions. `0` means there are currently no subscriptions to this publication. |
| `snapshot_in_default_folder` | **bit** | Specifies if the snapshot files are stored in the default folder.<br /><br />If `1`, snapshot files can be found in the default folder.<br /><br />If `0`, snapshot files are stored in the alternate location specified by `alt_snapshot_folder`. Alternate locations can be on another server, on a network drive, or on a removable media (such as removable disks). You can also save the snapshot files to an FTP site, for retrieval by the Subscriber at a later time.<br /><br />**Note:** This parameter can be true and still have a location in the `alt_snapshot_folder` parameter. That combination specifies that the snapshot files are stored in both the default and alternate locations. |
| `alt_snapshot_folder` | **nvarchar(255)** | Specifies the location of the alternate folder for the snapshot. |
| `pre_snapshot_script` | **nvarchar(255)** | Specifies a pointer to an **.sql** file that the Merge Agent runs before any of the replicated object scripts when applying the snapshot at a Subscriber. |
| `post_snapshot_script` | **nvarchar(255)** | Specifies a pointer to an **.sql** file that the Merge Agent runs after all the other replicated object scripts and data have been applied during an initial synchronization. |
| `compress_snapshot` | **bit** | Specifies that the snapshot that is written to the `alt_snapshot_folder` location is compressed into the [!INCLUDE [msCoName](../../includes/msconame-md.md)] CAB format. |
| `ftp_address` | **sysname** | The network address of the FTP service for the Distributor. Specifies where publication snapshot files are located for the Merge Agent to pick up. |
| `ftp_port` | **int** | The port number of the FTP service for the Distributor. `ftp_port` has a default of `21`. Specifies where the publication snapshot files are located for the Merge Agent to pick up. |
| `ftp_subdirectory` | **nvarchar(255)** | Specifies where the snapshot files are available for the Merge Agent to pick up when the snapshot is delivered using FTP. |
| `ftp_login` | **sysname** | The username used to connect to the FTP service. |
| `conflict_retention` | **int** | Specifies the retention period, in days, for which conflicts are retained. After the specified number of days has passed, the conflict row is purged from the conflict table. |
| `keep_partition_changes` | **int** | Specifies whether synchronization optimization is occurring for this publication. `keep_partition_changes` has a default of `0`. A value of `0` means that synchronization isn't optimized, and the partitions sent to all Subscribers are verified when data changes in a partition.<br /><br />`1` means that synchronization is optimized, and only Subscribers having rows in the changed partition are affected.<br /><br />**Note:** By default, merge publications use precomputed partitions, which provide a greater degree of optimization than this option. For more information, see [Parameterized Filters - Parameterized Row Filters](../replication/merge/parameterized-filters-parameterized-row-filters.md) and [Parameterized Filters - Optimize for Precomputed Partitions](../replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md). |
| `allow_subscription_copy` | **int** | Specifies whether the ability to copy the subscription databases that subscribe to this publication has been enabled. A value of `0` means copying isn't allowed. |
| `allow_synctoalternate` | **int** | Specifies whether an alternate synchronization partner is allowed to synchronize with this Publisher. A value of `0` means a synchronization partner isn't allowed. |
| `validate_subscriber_info` | **nvarchar(500)** | Lists the functions that are being used to retrieve Subscriber information and validate the parameterized row filtering criteria on the Subscriber. Helps to verify that the information is partitioned consistently with each merge. |
| `backward_comp_level` | **int** | Database compatibility level, and can be one of the following values:<br /><br />`90` = [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]<br /><br />`90` = [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] SP1<br /><br />`90` = [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] SP2<br /><br />`100` = [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] |
| `publish_to_activedirectory` | **bit** | Specifies if the publication information is published to Active Directory. A value of `0` means the publication information isn't available from Active Directory.<br /><br />[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] You can no longer add publication information to Active Directory. |
| `max_concurrent_merge` | **int** | The number of concurrent merge processes. If `0`, there's no limit to the number of concurrent merge processes running at any given time. |
| `max_concurrent_dynamic_snapshots` | **int** | The maximum number of concurrent filtered data snapshot sessions that can be running against the merge publication. If `0`, there's no limit to the maximum number of concurrent filtered data snapshot sessions that can run simultaneously against the publication at any given time. |
| `use_partition_groups` | **int** | Determines if precomputed partitions are used. A value of `1` means that precomputed partitions are used. |
| `num_of_articles` | **int** | Number of articles in the publication. |
| `replicate_ddl` | **int** | If schema changes to published tables are replicated. A value of `1` means that schema changes are replicated. |
| `publication_number` | **smallint** | Number assigned to this publication. |
| `allow_subscriber_initiated_snapshot` | **bit** | Determines if Subscribers can initiate the filtered data snapshot generation process. A value of `1` means that Subscribers can initiate the snapshot process. |
| `allow_web_synchronization` | **bit** | Determines if the publication is enabled for Web synchronization. A value of `1` means that Web synchronization is enabled. |
| `web_synchronization_url` | **nvarchar(500)** | Internet URL that is used for Web synchronization. |
| `allow_partition_realignment` | **bit** | Determines whether deletes are sent to the subscriber when modification of the row on the publisher causes it to change its partition. A value of `1` means that deletes are sent to the Subscriber. For more information, see [sp_addmergepublication (Transact-SQL)](sp-addmergepublication-transact-sql.md). |
| `retention_period_unit` | **tinyint** | Defines the unit that is used when defining retention. This can be one of the following values:<br /><br />`0` = day<br /><br />`1` = week<br /><br />`2` = month<br /><br />`3` = year |
| `has_downloadonly_articles` | **bit** | Indicates if any articles that belong to the publication are download-only articles. A value of `1` indicates that there are download-only articles. |
| `decentralized_conflicts` | **int** | Indicates whether the conflict records are stored at the Subscriber that caused the conflict. A value of `0` indicates that conflict records aren't stored at the Subscriber. A value of `1` indicates that conflict records are stored at the Subscriber. |
| `generation_leveling_threshold` | **int** | Specifies the number of changes that are contained in a generation. A generation is a collection of changes that are delivered to a Publisher or Subscriber |
| `automatic_reinitialization_policy` | **bit** | Indicates whether changes are uploaded from the Subscriber before an automatic reinitialization occurs. A value of `1` indicates that changes are uploaded from the Subscriber before an automatic reinitialization occurs. A value of 0 indicates that changes aren't uploaded before an automatic reinitialization. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergepublication` is used in merge replication.

## Permissions

Members of the publication access list for a publication can execute `sp_helpmergepublication` for that publication. Members of the **db_owner** fixed database role on the publication database can execute `sp_helpmergepublication` for information on all publications.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-helpmergepublication-_1.sql":::

## Related content

- [View and Modify Publication Properties](../replication/publish/view-and-modify-publication-properties.md)
- [sp_addmergepublication (Transact-SQL)](sp-addmergepublication-transact-sql.md)
- [sp_changemergepublication (Transact-SQL)](sp-changemergepublication-transact-sql.md)
- [sp_dropmergepublication (Transact-SQL)](sp-dropmergepublication-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
