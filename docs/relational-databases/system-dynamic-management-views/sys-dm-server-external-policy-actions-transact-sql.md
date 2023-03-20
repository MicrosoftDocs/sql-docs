---
title: "sys.dm_server_external_policy_actions (Transact-SQL)"
description: Reference documentation to explain sys.dm_server_external_policy_actions (Transact-SQL) dynamic management view.
author: srdan-bozovic-msft
ms.author: srbozovi
ms.reviewer: randolphwest
ms.date: 03/09/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_server_external_policy_actions_TSQL"
  - "sys.dm_server_external_policy_actions"
  - "dm_server_external_policy_actions"
  - "sys.dm_server_external_policy_actions_TSQL"
helpviewer_keywords:
  - "sys.dm_server_external_policy_actions dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_server_external_policy_actions (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

List all available data actions, independently of them being used or not.

| Column name | Data type | Description |
| --- | --- | --- |
| **sql_action_id** | **int** | Internal ID for joining with other dynamic management views. Not used by external policy providers. |
| **action_namespace** | **nvarchar(256)** | The path or namespace on which the action type part applies to. |
| **action_type** | **varchar(20)** | Identifies the type of operation. |
| **action_provider_string** | **varchar(20)** | Identifies the resource provider that owns the action. Currently only supported value is `Microsoft.Sql`. |

## Permissions

Principals must have the **VIEW SERVER SECURITY STATE** permission.

## Next steps

- [sys.dm_server_external_policy_principal_assigned_actions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-server-external-policy-principal-assigned-actions-transact-sql.md)
- [Provision access by data owner for Azure SQL Database](/azure/purview/how-to-policies-data-owner-azure-sql-db)
- [Provision access by data owner for SQL Server on Azure Arc-enabled servers](/azure/purview/how-to-policies-data-owner-arc-sql-server)
