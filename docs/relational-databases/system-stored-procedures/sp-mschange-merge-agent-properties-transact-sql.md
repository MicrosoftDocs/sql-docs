---
title: "sp_MSchange_merge_agent_properties (Transact-SQL)"
description: "Changes the properties of a Merge Agent job that runs at a SQL Server 2005 or later version Distributor."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_MSchange_merge_agent_properties_TSQL"
  - "sp_MSchange_merge_agent_properties"
helpviewer_keywords:
  - "sp_MSchange_merge_agent_properties"
dev_langs:
  - "TSQL"
---
# sp_MSchange_merge_agent_properties (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the properties of a Merge Agent job that runs at a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] or later version Distributor. This stored procedure is used to change properties when the Publisher runs on an instance of [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]. This stored procedure is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_MSchange_merge_agent_properties
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    , [ @subscriber = ] N'subscriber'
    , [ @subscriber_db = ] N'subscriber_db'
    , [ @property = ] N'property'
    , [ @value = ] N'value'
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with no default.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with no default.

#### [ @property = ] N'*property*'

The publication property to change. *@property* is **sysname**, with no default.

#### [ @value = ] N'*value*'

The new property value. *@value* is **nvarchar(524)**, with no default.

This table describes the properties of the Merge Agent job that can be changed and restrictions on the values for those properties.

| Property | Value | Description |
| --- | --- | --- |
| `description` | | A brief description of the subscription. |
| `merge_job_login` | | Login for the Windows account under which the agent runs. |
| `merge_job_password` | | Password for the Windows account under which the agent job runs. |
| `publisher_login` | | Login to use when connecting to a Publisher to synchronize the subscription. |
| `publisher_password` | | Publisher password.<br /><br />[!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] |
| `publisher_security_mode` | `1` | Windows Authentication.<br /><br />[!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)] |
| | `0` | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `subscriber_login` | | Login to use when connecting to a Subscriber to synchronize the subscription. |
| `subscriber_password` | | Subscriber password.<br /><br />[!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] |
| `subscriber_security_mode` | `1` | Windows Authentication.<br /><br />[!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)] |
| | `0` | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |

After changing an agent login or password, you must stop and restart the agent before the change takes effect.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_MSchange_merge_agent_properties` is used in merge replication.

When the Publisher runs on an instance of [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] or later version, you should use [sp_changemergesubscription](sp-changemergesubscription-transact-sql.md) to change properties of a Merge Agent job that synchronizes a push subscription that runs at the Distributor.

## Permissions

Only members of the **sysadmin** fixed server role at the Distributor can execute `sp_MSchange_merge_agent_properties`.

## Related content

- [sp_addmergepushsubscription_agent (Transact-SQL)](sp-addmergepushsubscription-agent-transact-sql.md)
- [sp_addmergesubscription (Transact-SQL)](sp-addmergesubscription-transact-sql.md)
