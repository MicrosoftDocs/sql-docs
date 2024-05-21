---
title: "sp_helpdistributiondb (Transact-SQL)"
description: sp_helpdistributiondb returns properties of the specified distribution database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpdistributiondb_TSQL"
  - "sp_helpdistributiondb"
helpviewer_keywords:
  - "sp_helpdistributiondb"
dev_langs:
  - "TSQL"
---
# sp_helpdistributiondb (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns properties of the specified distribution database. This stored procedure is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpdistributiondb [ [ @database = ] N'database' ]
[ ; ]
```

## Arguments

#### [ @database = ] N'*database*'

The database name for which properties are returned. *@database* is **sysname**, with a default of `%` for all databases associated with the Distributor, and on which the user has permissions.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Name of the distribution database. |
| `min_distretention` | **int** | Minimum retention period, in hours, before transactions are deleted. |
| `max_distretention` | **int** | Maximum retention period, in hours, before transactions are deleted. |
| `history retention` | **int** | Number of hours to retain history. |
| `history_cleanup_agent` | **sysname** | Name of the History Cleanup Agent. |
| `distribution_cleanup_agent` | **sysname** | Name of the Distribution Cleanup Agent. |
| `status` | **int** | Internal use only. |
| `data_folder` | **nvarchar(255)** | Name of the directory used to store the database files. |
| `data_file` | **nvarchar(255)** | Name of the database file. |
| `data_file_size` | **int** | Initial data file size in megabytes. |
| `log_folder` | **nvarchar(255)** | Name of the directory for the database log file. |
| `log_file` | **nvarchar(255)** | Name of the log file. |
| `log_file_size` | **int** | Initial log file size in megabytes. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpdistributiondb` is used in all types of replication.

## Permissions

Members of the **db_owner** fixed database role or the **replmonitor** role in a distribution database and users in the publication access list of a publication using the distribution database can execute `sp_helpdistributiondb` to return file-related information. Members of the **public** role can execute `sp_helpdistributiondb` to return non-file-related information for distribution databases to which they have access.

## Related content

- [View and Modify Distributor and Publisher Properties](../replication/view-and-modify-distributor-and-publisher-properties.md)
- [sp_adddistributiondb (Transact-SQL)](sp-adddistributiondb-transact-sql.md)
- [sp_changedistributiondb (Transact-SQL)](sp-changedistributiondb-transact-sql.md)
- [sp_dropdistributiondb (Transact-SQL)](sp-dropdistributiondb-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
