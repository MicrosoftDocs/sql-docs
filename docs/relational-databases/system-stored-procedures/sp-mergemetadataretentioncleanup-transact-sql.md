---
title: "sp_mergemetadataretentioncleanup (Transact-SQL)"
description: "Performs a manual cleanup of metadata in the MSmerge_* system tables."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_mergemetadataretentioncleanup"
  - "sp_mergemetadataretentioncleanup_TSQL"
helpviewer_keywords:
  - "sp_mergemetadataretentioncleanup"
dev_langs:
  - "TSQL"
---
# sp_mergemetadataretentioncleanup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Performs a manual cleanup of metadata in the [MSmerge_genhistory](../system-tables/msmerge-genhistory-transact-sql.md), [MSmerge_contents](../system-tables/msmerge-contents-transact-sql.md), [MSmerge_tombstone](../system-tables/msmerge-tombstone-transact-sql.md), [MSmerge_past_partition_mappings](../system-tables/msmerge-past-partition-mappings-transact-sql.md), and [MSmerge_current_partition_mappings](../system-tables/msmerge-current-partition-mappings.md) system tables. This stored procedure is executed at each Publisher and Subscriber in the topology.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_mergemetadataretentioncleanup
    [ [ @num_genhistory_rows = ] num_genhistory_rows OUTPUT ]
    [ , [ @num_contents_rows = ] num_contents_rows OUTPUT ]
    [ , [ @num_tombstone_rows = ] num_tombstone_rows OUTPUT ]
    [ , [ @aggressive_cleanup_only = ] aggressive_cleanup_only ]
[ ; ]
```

## Arguments

#### [ @num_genhistory_rows = ] *num_genhistory_rows* OUTPUT

Returns the number of rows cleaned-up from the [MSmerge_genhistory](../system-tables/msmerge-genhistory-transact-sql.md) table. *@num_genhistory_rows* is an OUTPUT parameter of type **int**, with a default of `0`.

#### [ @num_contents_rows = ] *num_contents_rows* OUTPUT

Returns the number of rows cleaned-up from the [MSmerge_contents](../system-tables/msmerge-contents-transact-sql.md) table. *@num_contents_rows* is an OUTPUT parameter of type **int**, with a default of `0`

#### [ @num_tombstone_rows = ] *num_tombstone_rows* OUTPUT

Returns the number of rows cleaned-up from the [MSmerge_tombstone](../system-tables/msmerge-tombstone-transact-sql.md) table. *@num_tombstone_rows* is an OUTPUT parameter of type **int**, with a default of `0`.

#### [ @aggressive_cleanup_only = ] *aggressive_cleanup_only*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

If there are multiple publications on a database, and any one of those publications uses an infinite publication retention period, running `sp_mergemetadataretentioncleanup` doesn't clean up the merge replication change tracking metadata for the database. For this reason, use infinite publication retention with caution. To determine if a publication has an infinite retention period, execute [sp_helpmergepublication (Transact-SQL)](sp-helpmergepublication-transact-sql.md) at the Publisher and note any publications in the result set with a value of `0` for `retention`.

## Permissions

Only members of the **db_owner** fixed database role or users in the publication access list for a published database can execute `sp_mergemetadataretentioncleanup`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
