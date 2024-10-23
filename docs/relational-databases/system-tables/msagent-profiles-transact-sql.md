---
title: "MSagent_profiles (Transact-SQL)"
description: The MSagent_profiles table contains one row for each defined replication agent profile.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 10/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSagent_profiles"
  - "MSagent_profiles_TSQL"
helpviewer_keywords:
  - "MSagent_profiles system table"
dev_langs:
  - "TSQL"
---
# MSagent_profiles (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `MSagent_profiles` table contains one row for each defined replication agent profile. This table is stored in the `msdb` database on the Distributor.

| Column name | Data type | Description |
| --- | --- | --- |
| `profile_id` | **int** | The profile ID. |
| `profile_name` | **sysname** | The unique profile name for agent type. |
| `agent_type` | **int** | The type of agent:<br /><br />`1` = Snapshot Agent<br />`2` = Log Reader Agent<br />`3` = Distribution Agent<br />`4` = Merge Agent<br />`9` = Queue Reader Agent |
| `type` | **int** | The type of profile:<br /><br />`0` = System<br />`1` = Custom |
| `description` | **nvarchar(3000)** | The description of the profile. |
| `def_profile` | **bit** | Specifies whether this profile is the default for this agent type. |

## Related content

- [Replication Tables (Transact-SQL)](replication-tables-transact-sql.md)
- [Replication Views (Transact-SQL)](../system-views/replication-views-transact-sql.md)
