---
description: "ALTER WORKLOAD GROUP (Transact-SQL)"
title: ALTER WORKLOAD GROUP (Transact-SQL) 
ms.custom: ""
ms.date: "05/05/2020"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_WORKLOAD_GROUP_TSQL"
  - "ALTER WORKLOAD GROUP"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER WORKLOAD GROUP statement"
ms.assetid: 957addce-feb0-4e54-893e-5faca3cd184c
author: CarlRabeler
ms.author: carlrab
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azure-sqldw-latest||=azuresqldb-mi-current"
---
# ALTER WORKLOAD GROUP (Transact-SQL)

[!INCLUDE[select-product](../../includes/select-product.md)]

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017||=sqlallproducts-allversions"

:::row:::
    :::column:::
        **_\* SQL Server \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Database<br />Managed Instance](alter-workload-group-transact-sql.md?view=azuresqldb-mi-current)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-workload-group-transact-sql.md?view=azure-sqldw-latest)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server and SQL Managed Instance

[!INCLUDE [ALTER WORKLOAD GROUP](../../includes/alter-workload-group.md)]
  
::: moniker-end
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"

:::row:::
    :::column:::
        [SQL Server](alter-workload-group-transact-sql.md?view=sql-server-2017)
    :::column-end:::
    :::column:::
        **_\* SQL Database<br />Managed Instance \*_** &nbsp;
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-workload-group-transact-sql.md?view=azure-sqldw-latest)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server and SQL Managed Instance

[!INCLUDE [ALTER WORKLOAD GROUP](../../includes/alter-workload-group.md)]

::: moniker-end
::: moniker range="=azure-sqldw-latest||=sqlallproducts-allversions"

:::row:::
    :::column:::
        [SQL Server](alter-workload-group-transact-sql.md?view=sql-server-2017)
    :::column-end:::
    :::column:::
        [SQL Database<br />Managed Instance](alter-workload-group-transact-sql.md?view=azuresqldb-mi-current)
    :::column-end:::
    :::column:::
        **_\* Azure Synapse<br />Analytics \*_** &nbsp;
    :::column-end:::
:::row-end:::

&nbsp;

## Azure Synapse Analytics

Alters an existing workload group.

See the `ALTER WORKLOAD GROUP` behavior section below for further details on how `ALTER WORKLOAD GROUP` behaves on a system with running and queued requests. 

Restrictions in place for [CREATE WORKLOAD GROUP](create-workload-group-transact-sql.md) also apply to `ALTER WORKLOAD GROUP`.  Prior to modifying parameters, query [sys.workload_management_workload_groups](../../relational-databases/system-catalog-views/sys-workload-management-workload-groups-transact-sql.md) to ensure the values are within acceptable ranges.

## Syntax

```syntaxsql
ALTER WORKLOAD GROUP group_name
 WITH
 ([       MIN_PERCENTAGE_RESOURCE = value ]
  [ [ , ] CAP_PERCENTAGE_RESOURCE = value ]
  [ [ , ] REQUEST_MIN_RESOURCE_GRANT_PERCENT = value ]
  [ [ , ] REQUEST_MAX_RESOURCE_GRANT_PERCENT = value ] 
  [ [ , ] IMPORTANCE = { LOW | BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH }]
  [ [ , ] QUERY_EXECUTION_TIMEOUT_SEC = value ] )
  [ ; ]
  ```

## Arguments

group_name  
Is the name of the existing user-defined workload group being altered.  group_name is not alterable. 

MIN_PERCENTAGE_RESOURCE = value  
Value is an integer range from 0 to 100.  When altering min_percentage_resource, the sum of min_percentage_resource across all workload groups cannot exceed 100.  Altering min_percentage_resource requires all running queries to complete in the workload group before the command will complete.  See the ALTER WORKLOAD GROUP Behavior section in this doc for further details.

CAP_PERCENTAGE_RESOURCE = value  
Value is an integer range from 1 through 100.  The value for cap_percentage_resource must be greater than min_percentage_resource.  Altering cap_percentage_resource requires all running queries to complete in the workload group before the command will complete.  See the ALTER WORKLOAD GROUP Behavior section in this doc for further details. 

REQUEST_MIN_RESOURCE_GRANT_PERCENT = value  
Value is a decimal with a range between 0.75 to 100.00.  The value for request_min_resource_grant_percent needs to be a factor of min_percentage_resource and be less than cap_percentage_resource. 
  
REQUEST_MAX_RESOURCE_GRANT_PERCENT = value  
Value is a decimal and must be greater than request_min_resource_grant_percent.

IMPORTANCE = { LOW \|  BELOW_NORMAL \| NORMAL \| ABOVE_NORMAL \| HIGH }  
Alters the default importance of a request for the workload group.

QUERY_EXECUTION_TIMEOUT_SEC = value  
Alters the maximum time, in seconds, that a query can execute before it is canceled. Value must be 0 or a positive integer. The default setting for value is 0, which means unlimited.   

## Permissions

Requires CONTROL DATABASE permission

## Example

The below example checks the values in the catalog view for wgDataLoads and changes the values.

```sql
SELECT *
FROM sys.workload_management_workload_groups  
WHERE [name] = 'wgDataLoads'

ALTER WORKLOAD GROUP wgDataLoads WITH
( MIN_PERCENTAGE_RESOURCE            = 40
 ,CAP_PERCENTAGE_RESOURCE            = 80
 ,REQUEST_MIN_RESOURCE_GRANT_PERCENT = 10 )
 ```

## ALTER WORKLOAD GROUP behavior

At any point in time there are 3 types of requests in the system
- Requests which have not been classified yet.
- Requests which are classified - and waiting - for object locks or system resources.
- Requests which are classified - and running.

Based on the properties of a workload group being altered, the timing of when the settings take effect will differ.

**Importance or query_execution_timeout**
For the importance and query_execution_timeout properties, non-classified requests pick up the new config values.  Waiting and running requests execute with the old configuration.  The `ALTER WORKLOAD GROUP` request executes immediately regardless if there are running queries in the workload group.

**Request_min_resource_grant_percent or request_max_resource_grant_percent**
For request_min_resource_grant_percent and request_max_resource_grant_percent, running requests execute with the old configuration.  Waiting requests and non-classified requests pick up the new config values.  The `ALTER WORKLOAD GROUP` request executes immediately regardless if there are running queries in the workload group.

**Min_percentage_resource or cap_percentage_resource**
For min_percentage_resource and cap_percentage_resource, running requests execute with the old configuration.  Waiting requests and non-classified requests pick up the new config values. 

Changing min_percentage_resource and cap_percentage_resource requires draining of running requests in the workload group that is being altered.  When decreasing min_percentage_resource, the freed resources are returned to the share pool allowing requests from other workload groups the ability to utilize.  Conversely, increasing the min_percentage_resource will wait until requests utilizing only the needed resources from the shared pool to complete.  Alter workload group operation will have prioritized access to shared resources over other requests waiting to be executed on shared pool.  If the sum of min_percentage_resource exceeds 100%, the ALTER WORKLOAD GROUP request fails immediately. 

**Locking behavior**
Altering a workload group requires a global lock across all workload groups.  A request to alter a workload group would queue behind already submitted create or drop workload group requests.  If a batch of alter statements is submitted at once, they are processed in the order in which they are submitted.  

## See also

- [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](create-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](drop-workload-group-transact-sql.md)
- [sys.workload_management_workload_groups](../../relational-databases/system-catalog-views/sys-workload-management-workload-groups-transact-sql.md)
- [sys.dm_workload_management_workload_groups_stats](../../relational-databases/system-dynamic-management-views/sys-dm-workload-management-workload-group-stats-transact-sql.md)
- [Quickstart: Configure workload isolation using T-SQL](/azure/sql-data-warehouse/quickstart-configure-workload-isolation-tsql)

::: moniker-end
