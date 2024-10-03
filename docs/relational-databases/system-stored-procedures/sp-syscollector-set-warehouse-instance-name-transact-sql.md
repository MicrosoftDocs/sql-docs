---
title: "sp_syscollector_set_warehouse_instance_name (Transact-SQL)"
description: Specifies the instance name for the connection string used to connect to the management data warehouse.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_set_warehouse_instance_name_TSQL"
  - "sp_syscollector_set_warehouse_instance_name"
helpviewer_keywords:
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_set_warehouse_instance_name"
dev_langs:
  - "TSQL"
---
# sp_syscollector_set_warehouse_instance_name (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the instance name for the connection string used to connect to the management data warehouse.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_set_warehouse_instance_name [ [ @instance_name = ] N'instance_name' ]
[ ; ]
```

## Arguments

#### [ @instance_name = ] N'*instance_name*'

The instance name. *@instance_name* is **sysname**, and defaults to the local instance if `NULL`.

*@instance_name* must be the fully qualified instance name, which consists of the computer name and the instance name in the form `<computerName>\<instanceName>`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must disable the data collector before changing this data collector-wide configuration. This procedure fails if the data collector is enabled.

To view the current instance name, query the [syscollector_config_store](../system-catalog-views/syscollector-config-store-transact-sql.md) system view.

## Permissions

Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

The following example illustrates how to configure the data collector to use a management data warehouse instance on a remote server. In this example, the remote server is named `RemoteSERVER` and the database is installed on the default instance.

```sql
USE msdb;
GO
EXEC sp_syscollector_set_warehouse_instance_name N'RemoteSERVER'; -- the default instance is assumed on the remote server
GO
```

## Related content

- [Data collector stored procedures (Transact-SQL)](data-collector-stored-procedures-transact-sql.md)
- [syscollector_config_store (Transact-SQL)](../system-catalog-views/syscollector-config-store-transact-sql.md)
