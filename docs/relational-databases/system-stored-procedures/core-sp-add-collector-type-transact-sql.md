---
title: "core.sp_add_collector_type (Transact-SQL)"
description: "Adds a new entry to the core.supported_collector_types view in the management data warehouse database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_collector_type"
  - "sp_add_collector_type_TSQL"
helpviewer_keywords:
  - "core.sp_add_collector_type stored procedure"
  - "management data warehouse, data collector stored procedures"
  - "sp_add_collector_type"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# core.sp_add_collector_type (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a new entry to the `core.supported_collector_types` view in the management data warehouse database. The procedure must be executed in the context of the management data warehouse database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
core.sp_add_collector_type [ @collector_type_uid = ] 'collector_type_uid'
[ ; ]
```

## Arguments

#### [ @collector_type_uid = ] '*collector_type_uid*'

The GUID for the collector type. *@collector_type_uid* is **uniqueidentifier**, with no default value.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires membership in the **mdw_admin** (with EXECUTE permission) fixed database role.

## Examples

The following example adds the Generic T-SQL Query collector type to the `core.supported_collector_types` view. By default, the Generic T-SQL Query collector type already exists. Therefore, if you run this code on a default installation, you see a message that the collector type already exists.

This code runs successfully if you previously removed the Generic T-SQL Query collector type by using the `core.sp_remove_collector_type` stored procedure, and then wanted to re-add it as a registered collector type that can upload data to the management data warehouse.

```sql
USE <management_data_warehouse>;
GO

DECLARE @RC INT;
DECLARE @collector_type_uid UNIQUEIDENTIFIER;

SELECT @collector_type_uid = (
    SELECT collector_type_uid
    FROM msdb.dbo.syscollector_collector_types
    WHERE name = N'Generic T-SQL Query Collector Type'
);

EXECUTE @RC = core.sp_add_collector_type @collector_type_uid;
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data Collector stored procedures (Transact-SQL)](data-collector-stored-procedures-transact-sql.md)
- [Management Data Warehouse](../data-collection/management-data-warehouse.md)
