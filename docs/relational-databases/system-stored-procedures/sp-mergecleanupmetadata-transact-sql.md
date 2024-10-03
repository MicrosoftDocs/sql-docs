---
title: "sp_mergecleanupmetadata (Transact-SQL)"
description: "Only for use in replication topologies with servers running versions of SQL Server 2000 before Service Pack 1."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_mergecleanupmetadata_TSQL"
  - "sp_mergecleanupmetadata"
helpviewer_keywords:
  - "sp_mergecleanupmetadata"
dev_langs:
  - "TSQL"
---
# sp_mergecleanupmetadata (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Should only be used in replication topologies that include servers running versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] before [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] Service Pack 1. `sp_mergecleanupmetadata` allows administrators to clean up metadata in the `MSmerge_genhistory`, `MSmerge_contents`, and `MSmerge_tombstone` system tables. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_mergecleanupmetadata
    [ [ @publication = ] N'publication' ]
    [ , [ @reinitialize_subscriber = ] N'reinitialize_subscriber' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `%`, which cleans up metadata for all publications. The publication must already exist if explicitly specified.

#### [ @reinitialize_subscriber = ] N'*reinitialize_subscriber*'

Specifies whether to reinitialize the Subscriber. *@reinitialize_subscriber* is **nvarchar(5)**, with a default of `true`.

- If `true`, subscriptions are marked for reinitialization.
- If `false`, the subscriptions aren't marked for reinitialization.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_mergecleanupmetadata` should only be used in replication topologies that include servers running versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] before [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] Service Pack 1. Topologies that include only [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] Service Pack 1 or later should use automatic retention based metadata cleanup. When running this stored procedure, be aware of the necessary and potentially large growth of the log file on the computer on which the stored procedure is running.

After `sp_mergecleanupmetadata` is executed, by default, all subscriptions at the Subscribers of publications that have metadata stored in `MSmerge_genhistory`, `MSmerge_contents` and `MSmerge_tombstone` are marked for reinitialization, any pending changes at the Subscriber are lost, and the current snapshot is marked obsolete.

If there are multiple publications on a database, and any one of those publications uses an infinite publication retention period (*@retention* is `0`), running `sp_mergecleanupmetadata` doesn't clean up the merge replication change tracking metadata for the database. For this reason, use infinite publication retention with caution.

When executing this stored procedure, you can choose whether to reinitialize Subscribers by setting the *@reinitialize_subscriber* parameter to `true` (the default) or `false`. If `sp_mergecleanupmetadata` is executed with the *@reinitialize_subscriber* parameter set to `true`, a snapshot is reapplied at the Subscriber even if the subscription was created without an initial snapshot (for example, if the snapshot data and schema were manually applied or already existed at the Subscriber). Setting the parameter to `false` should be used with caution, because if the publication isn't reinitialized, you must ensure that data at the Publisher and Subscriber is synchronized.

Regardless of the value of *@reinitialize_subscriber*, `sp_mergecleanupmetadata` fails if there are ongoing merge processes that are attempting to upload changes to a Publisher or a republishing Subscriber at the time the stored procedure is invoked.

### Execute sp_mergecleanupmetadata with @reinitialize_subscriber = N'true'

1. It's recommended, but not required, that you stop all updates to the publication and subscription databases. If updates continue, any updates made at a Subscriber since the last merge are lost when the publication is reinitialized, but data convergence is maintained.

1. Execute a merge by running the Merge Agent. We recommend that you use the **-Validate** agent command line option at each Subscriber when you run the Merge Agent. If you're running continuous mode merges, see *Special Considerations for Continuous Mode Merges* later in this section.

1. After all merges are complete, execute `sp_mergecleanupmetadata`.

1. Execute `sp_reinitmergepullsubscription` on all subscribers using named or anonymous pull subscription to ensure data convergence.

1. If you're running continuous mode merges, see *Special Considerations for Continuous Mode Merges* later in this section.

1. Regenerate snapshot files for all merge publications involved at all levels. If you try to merge without regenerating the snapshot first, you receive a prompt to regenerate the snapshot.

1. Back up the publication database. Failure to do so can cause a merge failure after a restore of the publication database.

### Execute sp_mergecleanupmetadata with @reinitialize_subscriber = N'false'

1. Stop *all* updates to the publication and subscription databases.

1. Execute a merge by running the Merge Agent. We recommend that you use the `-Validate` agent command line option at each Subscriber when you run the Merge Agent. If you're running continuous mode merges, see [Special considerations for continuous mode merges](#special-considerations-for-continuous-mode-merges) later in this article.

1. After all merges are complete, execute `sp_mergecleanupmetadata`.

1. If you're running continuous mode merges, see *Special Considerations for Continuous Mode Merges* later in this section.

1. Regenerate snapshot files for all merge publications involved at all levels. If you try to merge without regenerating the snapshot first, you receive a prompt to regenerate the snapshot.

1. Back up the publication database. Failure to do so can cause a merge failure after a restore of the publication database.

### Special considerations for continuous mode merges

If you're running continuous mode merges, you must either:

- Stop the Merge Agent, and then perform another merge without the `-Continuous` parameter specified.

- Deactivate the publication with `sp_changemergepublication` to ensure that any continuous-mode merges that are polling for the publication status fail.

  ```sql
  EXEC central..sp_changemergepublication @publication = 'dynpart_pubn', @property = 'status', @value = 'inactive';
  ```

When you have completed step 3 of running `sp_mergecleanupmetadata`, resume continuous mode merges based on how you stopped them. Either:

- Add the **-Continuous** parameter back for the Merge Agent.

- Reactivate the publication with `sp_changemergepublication`.

  ```sql
  EXEC central..sp_changemergepublication @publication = 'dynpart_pubn', @property = 'status', @value = 'active'
  ```

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_mergecleanupmetadata`.

To use this stored procedure, the Publisher must be running [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]. The Subscribers must be running either [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 7.0, Service Pack 2.

## Related content

- [MSmerge_genhistory (Transact-SQL)](../system-tables/msmerge-genhistory-transact-sql.md)
- [MSmerge_contents (Transact-SQL)](../system-tables/msmerge-contents-transact-sql.md)
- [MSmerge_tombstone (Transact-SQL)](../system-tables/msmerge-tombstone-transact-sql.md)
