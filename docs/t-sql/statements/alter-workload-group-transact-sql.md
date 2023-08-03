---
title: ALTER WORKLOAD GROUP (Transact-SQL)
description: "Changes an existing Resource Governor workload group configuration, and optionally assigns it to a Resource Governor resource pool."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/10/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_WORKLOAD_GROUP_TSQL"
  - "ALTER WORKLOAD GROUP"
helpviewer_keywords:
  - "ALTER WORKLOAD GROUP statement"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest||=azuresqldb-mi-current"
---
# ALTER WORKLOAD GROUP (Transact-SQL)

[!INCLUDE [select-product](../includes/select-product.md)]

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017"

:::row:::
    :::column:::
        **_\* SQL Server \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-workload-group-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-workload-group-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server and SQL Managed Instance

[!INCLUDE [alter-workload-group](../includes/alter-workload-group.md)]

::: moniker-end
::: moniker range="=azuresqldb-mi-current"

:::row:::
    :::column:::
        [SQL Server](alter-workload-group-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* SQL Managed Instance \*_** &nbsp;
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-workload-group-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server and SQL Managed Instance

[!INCLUDE [alter-workload-group](../includes/alter-workload-group.md)]

::: moniker-end
::: moniker range="=azure-sqldw-latest"

:::row:::
    :::column:::
        [SQL Server](alter-workload-group-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-workload-group-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure Synapse<br />Analytics \*_** &nbsp;
    :::column-end:::
:::row-end:::

&nbsp;

## Azure Synapse Analytics

Alters an existing workload group.

See the `ALTER WORKLOAD GROUP` behavior section below for further details on how `ALTER WORKLOAD GROUP` behaves on a system with running and queued requests.

Restrictions in place for [CREATE WORKLOAD GROUP](create-workload-group-transact-sql.md) also apply to `ALTER WORKLOAD GROUP`. Prior to modifying parameters, query [sys.workload_management_workload_groups](../../relational-databases/system-catalog-views/sys-workload-management-workload-groups-transact-sql.md) to ensure the values are within acceptable ranges.

## Syntax

```syntaxsql
ALTER WORKLOAD GROUP group_name
WITH
([ MIN_PERCENTAGE_RESOURCE = value ]
  [ [ , ] CAP_PERCENTAGE_RESOURCE = value ]
  [ [ , ] REQUEST_MIN_RESOURCE_GRANT_PERCENT = value ]
  [ [ , ] REQUEST_MAX_RESOURCE_GRANT_PERCENT = value ]
  [ [ , ] IMPORTANCE = { LOW | BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH }]
  [ [ , ] QUERY_EXECUTION_TIMEOUT_SEC = value ] )
  [ ; ]
  ```

## Arguments

#### *group_name*

Is the name of the existing user-defined workload group being altered. *group_name* isn't alterable.

#### MIN_PERCENTAGE_RESOURCE = *value*

*value* is an integer range from 0 to 100. When altering MIN_PERCENTAGE_RESOURCE, the sum of MIN_PERCENTAGE_RESOURCE across all workload groups can't exceed 100. Altering MIN_PERCENTAGE_RESOURCE requires all running queries to complete in the workload group before the command will complete. For more information, see the [ALTER WORKLOAD GROUP behavior](#alter-workload-group-behavior) section in this article.

#### CAP_PERCENTAGE_RESOURCE = *value*

*value* is an integer range from 1 through 100. The value for CAP_PERCENTAGE_RESOURCE must be greater than MIN_PERCENTAGE_RESOURCE. Altering CAP_PERCENTAGE_RESOURCE requires all running queries to complete in the workload group before the command will complete. For more information, see the [ALTER WORKLOAD GROUP behavior](#alter-workload-group-behavior) section in this article.

#### REQUEST_MIN_RESOURCE_GRANT_PERCENT = *value*

*value*  is a decimal with a range between 0.75 to 100.00. The value for REQUEST_MIN_RESOURCE_GRANT_PERCENT needs to be a factor of MIN_PERCENTAGE_RESOURCE and be less than CAP_PERCENTAGE_RESOURCE.

#### REQUEST_MAX_RESOURCE_GRANT_PERCENT = *value*

*value* is a decimal and must be greater than REQUEST_MIN_RESOURCE_GRANT_PERCENT.

#### IMPORTANCE = { LOW | BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH }

Alters the default importance of a request for the workload group.

#### QUERY_EXECUTION_TIMEOUT_SEC = value

Alters the maximum time, in seconds, that a query can execute before it's canceled. Value must be 0 or a positive integer. The default setting for value is 0, which means unlimited.

## Permissions

Requires **CONTROL DATABASE** permission.

## Example

The below example checks the values in the catalog view for a workload group named **wgDataLoads**, and changes the values.

```sql
SELECT *
FROM sys.workload_management_workload_groups
WHERE [name] = 'wgDataLoads'

ALTER WORKLOAD GROUP wgDataLoads WITH
( MIN_PERCENTAGE_RESOURCE            = 40
, CAP_PERCENTAGE_RESOURCE            = 80
, REQUEST_MIN_RESOURCE_GRANT_PERCENT = 10 )
```

## ALTER WORKLOAD GROUP behavior

At any point in time there are three types of requests in the system:

- Requests that haven't been classified yet.
- Requests that are classified, and waiting, for object locks or system resources.
- Requests that are classified, and running.

Based on the properties of a workload group being altered, the timing of when the settings take effect will differ.

#### Importance or query_execution_timeout

For the importance and query_execution_timeout properties, non-classified requests pick up the new config values. Waiting and running requests execute with the old configuration. The `ALTER WORKLOAD GROUP` request executes immediately regardless if there are running queries in the workload group.

#### REQUEST_MIN_RESOURCE_GRANT_PERCENT or REQUEST_MAX_RESOURCE_GRANT_PERCENT

For REQUEST_MIN_RESOURCE_GRANT_PERCENT and REQUEST_MAX_RESOURCE_GRANT_PERCENT, running requests execute with the old configuration. Waiting requests and non-classified requests pick up the new config values. The `ALTER WORKLOAD GROUP` request executes immediately regardless if there are running queries in the workload group.

#### MIN_PERCENTAGE_RESOURCE or CAP_PERCENTAGE_RESOURCE

For MIN_PERCENTAGE_RESOURCE and CAP_PERCENTAGE_RESOURCE, running requests execute with the old configuration. Waiting requests and non-classified requests pick up the new config values.

Changing MIN_PERCENTAGE_RESOURCE and CAP_PERCENTAGE_RESOURCE requires draining of running requests in the workload group that is being altered. When decreasing MIN_PERCENTAGE_RESOURCE, the freed resources are returned to the share pool allowing requests from other workload groups the ability to utilize. Conversely, increasing the MIN_PERCENTAGE_RESOURCE will wait until requests utilizing only the needed resources from the shared pool to complete. The `ALTER WORKLOAD GROUP` operation will have prioritized access to shared resources over other requests waiting to be executed on shared pool. If the sum of MIN_PERCENTAGE_RESOURCE exceeds 100%, the `ALTER WORKLOAD GROUP` request fails immediately.

#### Locking behavior

Altering a workload group requires a global lock across all workload groups. A request to alter a workload group would queue behind already submitted create or drop workload group requests. If a batch of alter statements is submitted at once, they're processed in the order in which they're submitted.

## See also

- [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](create-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](drop-workload-group-transact-sql.md)
- [sys.workload_management_workload_groups](../../relational-databases/system-catalog-views/sys-workload-management-workload-groups-transact-sql.md)
- [sys.dm_workload_management_workload_groups_stats](../../relational-databases/system-dynamic-management-views/sys-dm-workload-management-workload-group-stats-transact-sql.md)
- [Quickstart: Configure workload isolation using T-SQL](/azure/sql-data-warehouse/quickstart-configure-workload-isolation-tsql)

::: moniker-end
