---
title: "sp_helpdistributor_properties (Transact-SQL)"
description: "sp_helpdistributor_properties (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpdistributor_properties_TSQL"
  - "sp_helpdistributor_properties"
helpviewer_keywords:
  - "sp_helpdistributor_properties"
dev_langs:
  - "TSQL"
---
# sp_helpdistributor_properties (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns Distributor properties. This stored procedure is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpdistributor_properties
[ ; ]
```

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `heartbeat_interval` | **int** | The maximum number of minutes that an agent can go without logging a progress message. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpdistributor_properties` is used with all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role, members of the **db_owner** or **replmonitor** fixed database role on the distribution database, and users in the publication access list (PAL) for a publication that uses this Distributor can execute `sp_helpdistributor_properties`.

## Related content

- [sp_changedistributor_property (Transact-SQL)](sp-changedistributor-property-transact-sql.md)
