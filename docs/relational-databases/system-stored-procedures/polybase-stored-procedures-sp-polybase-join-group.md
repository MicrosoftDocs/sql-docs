---
title: "sp_polybase_join_group"
description: "Adds a SQL Server instance as a compute node to a PolyBase group for scale-out computation."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: conceptual
f1_keywords:
  - "sp_polybase_join_group"
helpviewer_keywords:
  - "PolyBase"
dev_langs:
  - "TSQL"
---
# sp_polybase_join_group (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Adds a SQL Server instance as a compute node to a PolyBase group for scale-out computation.

The SQL Server instance must have the [PolyBase](../polybase/polybase-guide.md) feature installed. PolyBase enables the integration of non-SQL Server data sources, such as Hadoop and Azure Blob Storage. See also [sp_polybase_leave_group (Transact-SQL)](polybase-stored-procedures-sp-polybase-leave-group.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_polybase_join_group (
    @head_node_address = N'head_node_address'
    , @dms_control_channel_port = dms_control_channel_port
    , @head_node_sql_server_instance_name = 'head_node_sql_server_instance_name'
    )
[ ; ]
```

## Arguments

#### @head_node_address = N'*head_node_address*'

The name of the machine that hosts the SQL Server head node of the PolyBase scale-out group. *@head_node_address* is **nvarchar(255)**.

#### @dms_control_channel_port = *dms_control_channel_port*

The port where the control channel for the head node PolyBase Data Movement Service is running. *@dms_control_channel_port* is an **unsigned __int16**, with a range of `0` to `65535`. The default is `16450`.

#### @head_node_sql_server_instance_name = N'*head_node_sql_server_instance_name*'

The name of the head node SQL Server instance in the PolyBase scale-out group. *@head_node_sql_server_instance_name* is **nvarchar(16)**.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires CONTROL SERVER permission.

## Remarks

After running the stored procedure, shut down the PolyBase engine, and restart the PolyBase Data Movement Service on the machine. To verify, run the following DMV on the head node:

```sql
EXEC sys.dm_exec_compute_nodes;
```

## Examples

The example joins the current machine as a compute node to a PolyBase group. The name of the head node is `HST01` and the name of the SQL Server instance on the head node is `MSSQLSERVER`.

```sql
EXEC sp_polybase_join_group N'HST01', 16450, N'MSSQLSERVER';
```

## Related content

- [Get started with PolyBase](../polybase/polybase-guide.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
