---
title: "sp_configure_automatic_tuning (Transact-SQL)"
description: sp_configure_automatic_tuning changes the automatic tuning for a given query_id, to be allowed or skipped for consideration by automatic plan correction.
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 09/18/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sp_configure_automatic_tuning"
  - "sp_configure_TSQL"
helpviewer_keywords:
  - "sp_configure_automatic_tuning"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azuresqldb-current"
---
# sp_configure_automatic_tuning (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Changes the configuration for the [automatic plan correction](../automatic-tuning/automatic-tuning.md#automatic-plan-correction) (APC) component of the [automatic tuning](../automatic-tuning/automatic-tuning.md) feature. The configuration options apply to a given `query_id` which can be obtained from the [Query Store](../performance/monitoring-performance-by-using-the-query-store.md).

These options include the ability to allow a `query_id` to be allowed or skipped for APC consideration, or to configure APC to apply an additional extended, time-based plan regression check to that specific query. The configuration options are **not** mutually exclusive.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for [!INCLUDE [sqlserver2022-asmi](../../includes/applies-to-version/sqlserver2022-asmi.md)].

```syntaxsql
sp_configure_automatic_tuning
    [ @option = ] 'FORCE_LAST_GOOD_PLAN'
    , [ @type = ] 'type'
    [ , [ @type_value = ] N'type_value' ]
    , [ @option_value = ] { 'ON' | 'OFF' }
```

Syntax for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

```syntaxsql
sp_configure_automatic_tuning
    [ @option = ] { 'FORCE_LAST_GOOD_PLAN' | 'FORCE_LAST_GOOD_PLAN_EXTENDED_CHECK' }
    , [ @type = ] 'type'
    [ , [ @type_value = ] N'type_value' ]
    , [ @option_value = ] { 'ON' | 'OFF' }
```

## Arguments

#### [ @option = ] '*option*'

Specifies the name of the configuration option to be invoked. *@option* is **varchar(60)**, with no default, and can be one of these values.

| Value | Description |
| --- | --- |
| `FORCE_LAST_GOOD_PLAN` | Enables APC to identify execution plan choice regressions and to automatically fix the issue by forcing the last known good plan as recorded in the Query Store. See [What is execution plan choice regression?](../automatic-tuning/automatic-tuning.md#what-is-execution-plan-choice-regression) |
| `FORCE_LAST_GOOD_PLAN_EXTENDED_CHECK` | **Applies to:** [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] only. Instructs APC to use an additional time-based plan regression check, which occurs five minutes after a plan change is detected. This check allows APC to avoid biasing its regression checks for queries that execute quickly. With this option, APC takes into account query executions that might run longer, or are prone to timing out because of a plan change. |

#### [ @type = ] '*type*'

The type of object the configuration applies to. *@type* is **varchar(60)** with no default. Possible value is `QUERY`.

#### [ @type_value = ] N'*type_value*'

The query ID from Query Store that the configuration should be applied to. *@type_value* is **sysname** with no defaults.

#### [ @option_value = ] '*option_value*'

The desired state of the configuration setting. *@option_value* is **varchar(60)** with no defaults. Possible values are `ON` or `OFF`.

## Return code values

`0` (success) or `1` (failure).

## Check the current configuration values

You can also check to see which configuration options are set, by viewing the output of the `sys.database_automatic_tuning_configurations` catalog view. Changes to the catalog view are also written to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] error log.

## Permissions

Requires the `ALTER DATABASE` permission.

## Remarks

For [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)] CU 4 and later versions, the behavior of the `FORCE_LAST_GOOD_PLAN_EXTENDED_CHECK` configuration option can be applied to the entire [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance with global [trace flag 12656](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf12656). All queries eligible for Query Store capture have the additional time-based regression check logic applied.

## Examples

### A. Configure the Automatic Tuning (Force Last Good Plan option) to ignore a specific query

The following example shows how to configure automatic tuning to ignore a query if it's eligible for automatic plan forcing. This example uses a value of `422` as the `query_id` that was selected from the Query Store.

```sql
EXECUTE sys.sp_configure_automatic_tuning 'FORCE_LAST_GOOD_PLAN', 'QUERY', 422, 'ON';
```

### B. Configure the Automatic Tuning (Force Last Good Plan option) to ignore a specific query using named parameters

In this example, we can see all of the `query_id` results that are part of any update cursor statements, which are forced by the APC feature.

```sql
SELECT qry.query_id,
       pl.plan_forcing_type_desc,
       pl.is_forced_plan,
       pl.plan_id
FROM sys.query_store_plan AS pl
     INNER JOIN sys.query_store_query AS qry
         ON qry.query_id = pl.query_id
WHERE pl.query_plan LIKE '%StatementType="UPDATE CURSOR"%'
      AND pl.is_forced_plan > 0
      AND pl.plan_forcing_type = 2;
```

Based on the results of the previous query, the `query_id` with the value of `42` is a query that should be ignored by APC. We can use the named parameter version of the syntax for `sp_configure_automatic_tuning` as follows.

```sql
EXECUTE sys.sp_configure_automatic_tuning
    @option = 'FORCE_LAST_GOOD_PLAN',
    @type = 'QUERY',
    @type_value = 42,
    @option_value = 'OFF';
```

Check if the setting is applied.

```sql
SELECT *
FROM sys.database_automatic_tuning_configurations;
```

### C. Configure the Automatic Tuning (Force Last Good Plan option) to apply an extended, time-based plan regression check to a specific query

**Applies to:** [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

APC uses a time-based plan regression check, which occurs five minutes after a plan change is detected. This check allows APC to avoid biasing its regression checks for queries that execute quickly. APC takes into account query executions that might run longer, or are prone to timing out because of a plan change.

The following example shows how to configure automatic tuning to apply its extended check logic to a query if it's eligible for automatic plan forcing. This example is using a value of `442` as the `query_id` that was selected from the Query Store.

```sql
EXECUTE sys.sp_configure_automatic_tuning 'FORCE_LAST_GOOD_PLAN_EXTENDED_CHECK', 'QUERY', 442, 'ON';
```

## Related content

- [Automatic tuning](../automatic-tuning/automatic-tuning.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [sys.database_query_store_options (Transact-SQL)](../system-catalog-views/sys-database-query-store-options-transact-sql.md)
- [sys.dm_db_tuning_recommendations (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md)
- [sys.database_automatic_tuning_mode](../system-catalog-views/sys-database-automatic-tuning-mode-transact-sql.md)
- [sys.database_automatic_tuning_configuration (Transact-SQL)](../system-catalog-views/sys-database-automatic-tuning-configuration-transact-sql.md)
