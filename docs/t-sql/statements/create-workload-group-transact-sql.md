---
title: "CREATE WORKLOAD GROUP (Transact-SQL)"
description: CREATE WORKLOAD GROUP (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 05/04/2021
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "WORKLOAD GROUP"
  - "WORKLOAD_GROUP_TSQL"
  - "CREATE WORKLOAD GROUP"
  - "CREATE_WORKLOAD_GROUP_TSQL"
helpviewer_keywords:
  - "CREATE WORKLOAD GROUP statement"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest||=azuresqldb-mi-current"
---
# CREATE WORKLOAD GROUP (Transact-SQL)

[!INCLUDE [select-product](../includes/select-product.md)]

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017"

:::row:::
    :::column:::
        **_\* SQL Server \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Managed Instance](create-workload-group-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-workload-group-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server and SQL Managed Instance

[!INCLUDE [create-workload-group](../includes/create-workload-group.md)]

::: moniker-end
::: moniker range="=azuresqldb-mi-current"

:::row:::
    :::column:::
        [SQL Server](create-workload-group-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* SQL Managed Instance \*_** &nbsp;
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-workload-group-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server and SQL Managed Instance

[!INCLUDE [create-workload-group](../includes/create-workload-group.md)]

::: moniker-end
::: moniker range="=azure-sqldw-latest"

:::row:::
    :::column:::
        [SQL Server](create-workload-group-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](create-workload-group-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure Synapse<br />Analytics \*_** &nbsp;
    :::column-end:::
:::row-end:::

&nbsp;

## Azure Synapse Analytics

Creates a workload group. Workload groups are containers for a set of requests and are the basis for how workload management is configured on a system. Workload groups provide the ability to reserve resources for workload isolation, contain resources, define resources per request, and adhere to execution rules. Once the statement completes, the settings are in effect.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

```syntaxsql
CREATE WORKLOAD GROUP group_name
 WITH
 (   MIN_PERCENTAGE_RESOURCE = value 
   , CAP_PERCENTAGE_RESOURCE = value 
   , REQUEST_MIN_RESOURCE_GRANT_PERCENT = value
  [ [ , ] REQUEST_MAX_RESOURCE_GRANT_PERCENT = value ]
  [ [ , ] IMPORTANCE = { LOW | BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH } ]
  [ [ , ] QUERY_EXECUTION_TIMEOUT_SEC = value ] )
  [ ; ]

```

*group_name*</br>
Specifies the name by which the workload group is identified. *group_name* is a sysname. It can be up to 128 characters long and must be unique within the instance.

*MIN_PERCENTAGE_RESOURCE* = value</br>
Specifies a guaranteed minimum resource allocation for this workload group that is not shared with other workload groups. Memory is the only resource governed by this parameter.  *value* is an integer range from 0 to 100. The sum of min_percentage_resource across all workload groups cannot exceed 100. The value for min_percentage_resource cannot be greater than cap_percentage_resource. There are minimum effective values allowed per service level. See [Effective Values](#effective-values) for more details.

*CAP_PERCENTAGE_RESOURCE* = value</br>
Specifies the maximum resource utilization for all requests in a workload group. Both CPU and memory resources are capped by this parameter. The allowed integer range for value is 1 through 100. The value for cap_percentage_resource must be greater than min_percentage_resource. The effective value for cap_percentage_resource can be reduced if min_percentage_resource is configured greater than zero in other workload groups.

*REQUEST_MIN_RESOURCE_GRANT_PERCENT* = value</br>
Sets the minimum amount of resources allocated per request. Memory is the only resource governed by this parameter. *value* is a required parameter with a decimal range between 0.75 to 100.00. The value for request_min_resource_grant_percent must be a multiple of 0.25, must be a factor of min_percentage_resource, and be less than cap_percentage_resource. There are minimum effective values allowed per service level. See [Effective Values](#effective-values) for more details.

For example:

```sql
CREATE WORKLOAD GROUP wgSample 
WITH
  ( MIN_PERCENTAGE_RESOURCE = 26                -- integer value
    , REQUEST_MIN_RESOURCE_GRANT_PERCENT = 3.25 -- factor of 26 (guaranteed a minimum of 8 concurrency)
    , CAP_PERCENTAGE_RESOURCE = 100 )
```

Consider the values that are used for resource classes as a guideline for request_min_resource_grant_percent.  The table below contains resource allocations for Gen2.

|Resource Class|Percent of Resources|
|---|---|
|Smallrc|3%|
|Mediumrc|10%|
|Largerc|22%|
|Xlargerc|70%|

*REQUEST_MAX_RESOURCE_GRANT_PERCENT* = value</br>         
Sets the maximum amount of resources allocated per request. Memory is the only resource governed by this parameter. *value* is an optional decimal parameter with a default value equal to the request_min_resource_grant_percent. *value* must be greater than or equal to request_min_resource_grant_percent. When the value of request_max_resource_grant_percent is greater than request_min_resource_grant_percent and system resources are available, additional resources are allocated to a request.

*IMPORTANCE* = { LOW \| BELOW_NORMAL \| NORMAL \| ABOVE_NORMAL \| HIGH }</br>        
Specifies the default importance of a request for the workload group. Importance is one of the following, with NORMAL being the default:

- LOW
- BELOW_NORMAL
- NORMAL (default)
- ABOVE_NORMAL
- HIGH

Importance set at the workload group is a default importance for all requests in the workload group. A user can also set importance at the classifier level, which can override the workload group importance setting. This allows for differentiation of importance for requests within a workload group to get access to non-reserved resources quicker. When the sum of min_percentage_resource across workload groups is less than 100, there are non-reserved resources that are assigned on a basis of importance.

*QUERY_EXECUTION_TIMEOUT_SEC* = value</br>     
Specifies the maximum time, in seconds, that a query can execute before it is canceled. *value* must be 0 or a positive integer. The default setting for value is 0, which the query never times out. QUERY_EXECUTION_TIMEOUT_SEC counts once the query is in running state, not when the query is queued.

## Remarks

Workload groups corresponding to resource classes are created automatically for backward compatibility. These system defined workload groups cannot be dropped. An additional 8 user defined workload groups can be created.

If a workload group is created with `min_percentage_resource` greater than zero, the `CREATE WORKLOAD GROUP` statement will queue until there are enough resources to create the workload group.

## Effective Values

The parameters `min_percentage_resource`, `cap_percentage_resource`, `request_min_resource_grant_percent` and `request_max_resource_grant_percent` have effective values that are adjusted in the context of the current service level and the configuration of other workload groups.

The `request_min_resource_grant_percent` parameter has an effective value because there are minimum resources needed per query depending on the service level.  For example, at the lowest service level, DW100c, a minimum 25% resources per request is needed.  If the workload group is configured with 3% `request_min_resource_grant_percent` and `request_max_resource_grant_percent`, the effective values for both parameters adjusts to 25% when the instance is started.  If the instance is scaled up to DW1000c the configured and effective values for both parameters is 3% because 3% is the minimum supported value at that service level.  If the instance is scaled higher than DW1000c, the configured and effective values for both parameters will stay at 3%.  See the below table for further details on effective values at the different service levels.

|Service Level|Lowest effective value for REQUEST_MIN_RESOURCE_GRANT_PERCENT|Maximum concurrent queries|
|---|---|---|
|DW100c|25%|4|
|DW200c|12.5%|8|
|DW300c|8%|12|
|DW400c|6.25%|16|
|DW500c|5%|20|
|DW1000c|3%|32|
|DW1500c|3%|32|
|DW2000c|2%|48|
|DW2500c|2%|48|
|DW3000c|1.5%|64|
|DW5000c|1.5%|64|
|DW6000c|0.75%|128|
|DW7500c|0.75%|128|
|DW10000c|0.75%|128|
|DW15000c|0.75%|128|
|DW30000c|0.75%|128|

The `min_percentage_resource` parameter must be greater than or equal to the effective `request_min_resource_grant_percent`. A workload group with `min_percentage_resource` configured less than effective `min_percentage_resource` has the value adjusted to zero at run time. When this happens, the resources configured for `min_percentage_resource` are sharable across all workload groups. For example, the workload group `wgAdHoc` with a `min_percentage_resource` of 10% running at DW1000c would have an effective `min_percentage_resource` of 10% (3% is the minimum supported value at DW1000c). `wgAdhoc` at DW100c would have an effective min_percentage_resource of 0%. The 10% configured for `wgAdhoc` would be shared across all workload groups.

The `cap_percentage_resource` parameter also has an effective value. If a workload group `wgAdhoc` is configured with a `cap_percentage_resource` of 100% and another workload group `wgDashboards` is created with 25% `min_percentage_resource`, the effective `cap_percentage_resource` for `wgAdhoc` becomes 75%.

The easiest way to understand the run-time values for your workload groups is to query the system view [sys.dm_workload_management_workload_groups_stats](../../relational-databases/system-dynamic-management-views/sys-dm-workload-management-workload-group-stats-transact-sql.md).

## Permissions

Requires `CONTROL DATABASE` permission

## See also

- [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](drop-workload-group-transact-sql.md)
- [ALTER WORKLOAD GROUP &#40;Transact-SQL&#41;](alter-workload-group-transact-sql.md)
- [sys.workload_management_workload_groups](../../relational-databases/system-catalog-views/sys-workload-management-workload-groups-transact-sql.md)
- [sys.dm_workload_management_workload_groups_stats](../../relational-databases/system-dynamic-management-views/sys-dm-workload-management-workload-group-stats-transact-sql.md)
- [Quickstart: Configure workload isolation using T-SQL](/azure/sql-data-warehouse/quickstart-configure-workload-isolation-tsql)

::: moniker-end
