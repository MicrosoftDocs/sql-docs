---
title: "sp_replmonitorhelpmergesession (Transact-SQL)"
description: Returns information on past sessions for a given replication Merge Agent.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_replmonitorhelpmergesession_TSQL"
  - "sp_replmonitorhelpmergesession"
helpviewer_keywords:
  - "sp_replmonitorhelpmergesession"
dev_langs:
  - "TSQL"
---
# sp_replmonitorhelpmergesession (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information on past sessions for a given replication Merge Agent, with one row returned for each session that matches the filtering criterion. This stored procedure, which is used to monitor merge replication, is executed at the Distributor on the distribution database or at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_replmonitorhelpmergesession
    [ [ @agent_name = ] N'agent_name' ]
    [ , [ @hours = ] hours ]
    [ , [ @session_type = ] session_type ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @publication = ] N'publication' ]
[ ; ]
```

## Arguments

#### [ @agent_name = ] N'*agent_name*'

The name of the agent. *@agent_name* is **nvarchar(100)**, with no default.

#### [ @hours = ] *hours*

The range of time, in hours, for which historical agent session information is returned. *@hours* is **int**, and can be one of the following ranges.

| Value | Description |
| --- | --- |
| < `0` | Returns information on past agent runs, up to a maximum of 100 runs. |
| `0` (default) | Returns information on all past agent runs. |
| > `0` | Returns information on agent runs that occurred in the last *hours* number of hours. |

#### [ @session_type = ] *session_type*

Filters the result set based on the session end result. *@session_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` (default) | Agent sessions with a retry or succeed result. |
| `0` | Agent sessions with a failure result. |

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`. This parameter is used when executing `sp_replmonitorhelpmergesession` at the Subscriber.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with a default of `NULL`. This parameter is used when executing `sp_replmonitorhelpmergesession` at the Subscriber.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `NULL`. This parameter is used when executing `sp_replmonitorhelpmergesession` at the Subscriber.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `Session_id` | **int** | ID of the agent job session. |
| `Status` | **int** | Agent run status:<br /><br />`1` = Start<br /><br />`2` = Succeed<br /><br />`3` = In progress<br /><br />`4` = Idle<br /><br />`5` = Retry<br /><br />`6` = Fail |
| `StartTime` | **datetime** | Time agent job session began. |
| `EndTime` | **datetime** | Time agent job session was completed. |
| `Duration` | **int** | Cumulative duration, in seconds, of this job session. |
| `UploadedCommands` | **int** | Number of commands uploaded during the agent session. |
| `DownloadedCommands` | **int** | Number of commands downloaded during the agent session. |
| `ErrorMessages` | **int** | Number of error messages that were generated during the agent session. |
| `ErrorID` | **int** | ID of the error that occurred |
| `PercentageDone` | **decimal** | Estimated percent of the total changes that have already been delivered in an active session. |
| `TimeRemaining` | **int** | Estimated number of seconds left in an active session. |
| `CurrentPhase` | **int** | The current phase of an active session, and can be one of the following.<br /><br />`1` = Upload<br /><br />`2` = Download |
| `LastMessage` | **nvarchar(500)** | The last message logged by Merge Agent during the session. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_replmonitorhelpmergesession` is used to monitor merge replication.

When executed on the Subscriber, `sp_replmonitorhelpmergesession` only returns information on the last five Merge Agent sessions.

## Permissions

Only members of the **db_owner** or **replmonitor** fixed database role on the distribution database at the Distributor or on the subscription database at the Subscriber can execute `sp_replmonitorhelpmergesession`.

## Related content

- [Programmatically Monitor Replication](../replication/monitor/programmatically-monitor-replication.md)
